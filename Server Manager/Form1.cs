using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server_Manager.Config;
using Server_Manager.Theme;

namespace Server_Manager
{
    public partial class Form1 : Form
    {
        private const int StatusIntervalMs = 10_000;
        private const int ProbeTimeoutMs = 1500;

        private List<ServerConfig> servers = new();

        private System.Windows.Forms.Timer? statusTimer;
        private CancellationTokenSource probeCts = new();

        public Form1()
        {
            InitializeComponent();

            ThemeManager.InitializeFromDisk();

            chkDark.CheckedChanged -= chkDark_CheckedChanged;
            chkDark.Checked = ThemeManager.CurrentMode == ThemeMode.Dark;
            chkDark.CheckedChanged += chkDark_CheckedChanged;

            ThemeManager.ApplyTo(this);

            flowPanelServers.AutoScroll = true;
            flowPanelServers.HorizontalScroll.Enabled = false;
            flowPanelServers.HorizontalScroll.Visible = false;
            flowPanelServers.VerticalScroll.Visible = true;
            flowPanelServers.VerticalScroll.Enabled = true;
            flowPanelServers.AutoScrollMinSize = new Size(0, flowPanelServers.Height + 1);

            FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            statusTimer = new System.Windows.Forms.Timer { Interval = StatusIntervalMs };
            statusTimer.Tick += (s, args) => RefreshStatuses();
            statusTimer.Start();

            LoadServers();
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            statusTimer?.Stop();
            statusTimer?.Dispose();
            statusTimer = null;

            probeCts.Cancel();
        }

        // ---------------------------------------------------------------- config

        private void LoadServers()
        {
            if (!ServerConfigStore.TryLoad(out var loaded, out var error))
            {
                MessageBox.Show($"Loading error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            servers = loaded;
            txtSearch.Clear();
            RenderCards(servers);
        }

        private void btnLoadConfig_Click(object sender, EventArgs e) => LoadServers();

        private void btnEditConfig_Click(object sender, EventArgs e)
        {
            using var dlg = new ConfigEditorForm();
            if (dlg.ShowDialog(this) == DialogResult.OK)
                LoadServers();
        }

        private void InputBackupConfig(object sender, EventArgs e)
        {
            if (!ServerConfigStore.TryBackup(out var backupPath, out var error))
            {
                MessageBox.Show($"Error when backing up the config: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Config was backed up as:\n{backupPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadBackup(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Backup File",
                Filter = "BAK Files (*.bak)|*.bak|All files (*.*)|*.*",
                InitialDirectory = Directory.Exists(AppPaths.BaseDir) ? AppPaths.BaseDir : Application.StartupPath,
            };

            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            if (!ServerConfigStore.TryRestore(openFileDialog.FileName, out var error))
            {
                MessageBox.Show($"Error when loading the backup: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadServers();
            MessageBox.Show($"Config was loaded from:\n{openFileDialog.FileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ---------------------------------------------------------------- cards

        private void RenderCards(IEnumerable<ServerConfig> items)
        {
            CancelProbes();
            ClearServerCards();

            var list = items.ToList();

            flowPanelServers.SuspendLayout();
            try
            {
                if (list.Count == 0)
                {
                    flowPanelServers.Controls.Add(BuildEmptyState());
                    return;
                }

                foreach (var server in list)
                {
                    var card = new ServerCard(server);
                    card.OnConnect += ConnectToServer;
                    flowPanelServers.Controls.Add(card);
                }
            }
            finally
            {
                flowPanelServers.ResumeLayout(true);
            }

            RefreshStatuses();
        }

        private void ClearServerCards()
        {
            foreach (Control c in flowPanelServers.Controls.Cast<Control>().ToList())
            {
                if (c is ServerCard card)
                    card.OnConnect -= ConnectToServer;

                c.Dispose();
            }

            flowPanelServers.Controls.Clear();
        }

        private Label BuildEmptyState()
        {
            var p = ThemeManager.Palette;

            return new Label
            {
                AutoSize = true,
                Margin = new Padding(16),
                BackColor = Color.Transparent,
                ForeColor = p.UnknownFore,
                Font = new Font("Segoe UI", 11F),
                Text = servers.Count == 0
                    ? "No servers configured yet. Use \"Edit Config\" to add one."
                      + $"\r\n\r\nConfig file: {ServerConfigStore.FilePath}"
                    : "No servers match your search."
            };
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => FilterServerCards(txtSearch.Text);

        private void FilterServerCards(string searchTerm)
        {
            searchTerm = searchTerm.Trim();

            if (searchTerm.Length == 0)
            {
                RenderCards(servers);
                return;
            }

            RenderCards(servers.Where(s =>
                ContainsText(s.Name, searchTerm) ||
                ContainsText(s.Host, searchTerm) ||
                ContainsText(s.User, searchTerm) ||
                ContainsText(s.Url, searchTerm)));
        }

        private static bool ContainsText(string? value, string term) =>
            !string.IsNullOrEmpty(value) && value.Contains(term, StringComparison.OrdinalIgnoreCase);

        // ---------------------------------------------------------------- status

        private void CancelProbes()
        {
            probeCts.Cancel();
            probeCts.Dispose();
            probeCts = new CancellationTokenSource();
        }

        private void RefreshStatuses()
        {
            var token = probeCts.Token;

            foreach (var card in flowPanelServers.Controls.OfType<ServerCard>())
                _ = ProbeAsync(card, token);
        }

        private async Task ProbeAsync(ServerCard card, CancellationToken token)
        {
            bool online = await CheckReachableAsync(card.Config, token).ConfigureAwait(false);

            if (token.IsCancellationRequested || !IsHandleCreated || IsDisposed) return;

            try
            {
                BeginInvoke(() =>
                {
                    if (card.IsDisposed) return;
                    card.UpdateStatus(online);
                });
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        private static async Task<bool> CheckReachableAsync(ServerConfig config, CancellationToken token)
        {
            string host = config.ResolveHostName();
            if (!ServerConfig.IsValidHostName(host)) return false;

            try
            {
                using var tcp = new TcpClient();
                using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
                timeout.CancelAfter(ProbeTimeoutMs);

                await tcp.ConnectAsync(host, config.Port, timeout.Token).ConfigureAwait(false);
                return tcp.Connected;
            }
            catch
            {
                return false;
            }
        }

        // ---------------------------------------------------------------- connect

        private void ConnectToServer(ServerConfig server)
        {
            if (server.IsMode("SSH")) StartSsh(server);
            else if (server.IsMode("RDP")) StartRdp(server);
            else if (server.IsMode("WEB")) StartWeb(server);
            else MessageBox.Show($"Unknown connection mode: {server.Mode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void StartSsh(ServerConfig server)
        {
            if (!EnsureValidHost(server.Host)) return;

            if (!ServerConfig.IsValidUserName(server.User))
            {
                MessageBox.Show($"\"{server.User}\" is not a valid user name.", "Invalid server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var psi = new ProcessStartInfo { FileName = "ssh.exe", UseShellExecute = true };
            psi.ArgumentList.Add($"{server.User}@{server.Host}");
            psi.ArgumentList.Add("-p");
            psi.ArgumentList.Add(server.Port.ToString(CultureInfo.InvariantCulture));

            Start(psi, "SSH", "Make sure the Windows OpenSSH client is installed.");
        }

        private void StartRdp(ServerConfig server)
        {
            if (!EnsureValidHost(server.Host)) return;

            var psi = new ProcessStartInfo { FileName = "mstsc.exe", UseShellExecute = true };
            psi.ArgumentList.Add($"/v:{server.Host}:{server.Port.ToString(CultureInfo.InvariantCulture)}");

            Start(psi, "RDP", null);
        }

        private void StartWeb(ServerConfig server)
        {
            string raw = (!string.IsNullOrWhiteSpace(server.Url) ? server.Url! : server.Host).Trim();
            if (raw.Length == 0)
            {
                MessageBox.Show("No valid URL or Host specified for WEB mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string host = server.ResolveHostName();
            if (!EnsureValidHost(host)) return;

            Uri.TryCreate(raw.Contains("://") ? raw : "rcc://" + raw, UriKind.Absolute, out var parsed);

            string scheme = parsed?.Scheme is "http" or "https"
                ? parsed.Scheme
                : server.Port == 443 ? "https" : "http";

            string path = parsed is null || parsed.PathAndQuery == "/" ? "" : parsed.PathAndQuery;

            var psi = new ProcessStartInfo
            {
                FileName = $"{scheme}://{host}:{server.Port.ToString(CultureInfo.InvariantCulture)}{path}",
                UseShellExecute = true,
            };

            Start(psi, "web", null);
        }

        private static bool EnsureValidHost(string host)
        {
            if (ServerConfig.IsValidHostName(host)) return true;

            MessageBox.Show(
                $"\"{host}\" is not a valid host name.",
                "Invalid server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private static void Start(ProcessStartInfo psi, string what, string? hint)
        {
            try
            {
                Process.Start(psi);
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 2 && hint is not null)
            {
                MessageBox.Show(
                    $"Error when starting the {what} connection: {ex.Message}\n\n{hint}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error when starting the {what} connection: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------------- theme

        private void chkDark_CheckedChanged(object? sender, EventArgs e)
        {
            ThemeManager.SetMode(chkDark.Checked ? ThemeMode.Dark : ThemeMode.Light, persist: true);
            ThemeManager.ApplyTo(this);
        }

        // ---------------------------------------------------------------- designer stubs

        private void flowPanelServers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
