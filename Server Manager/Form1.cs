using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Windows.Forms;

namespace Server_Manager
{
    public partial class Form1 : Form
    {
        private List<ServerConfig> servers = new();

        public Form1()
        {
            InitializeComponent();
            PostInit();
            InitDummyServers();
            PopulateServerCards();

            flowPanelServers.AutoScroll = true;
            flowPanelServers.HorizontalScroll.Enabled = false;
            flowPanelServers.HorizontalScroll.Visible = false;
            flowPanelServers.VerticalScroll.Visible = true;
            flowPanelServers.VerticalScroll.Enabled = true;
            flowPanelServers.AutoScrollMinSize = new Size(0, flowPanelServers.Height + 1);
        }

        public async Task PostInit()
        {
            await Task.Delay(250);
            btnLoadConfig_Click(null, null);
        }

        private void InitDummyServers()
        {
            for (int i = 0; i < 14; i++)
            {
                servers.Add(new ServerConfig
                {
                    Name = $"Server {i + 1}",
                    Host = "0.0.0.0",
                    Port = 22,
                    User = "user"
                });
            }
        }

        private void PopulateServerCards()
        {
            flowPanelServers.Controls.Clear();
            foreach (var server in servers)
            {
                server.IsOnline = null;
                var card = new ServerCard(server);
                card.OnConnect += ConnectToServer;
                card.OnStatusCheckRequested += async (cfg, c) =>
                {
                    bool online = await CheckPingAsync(cfg);
                    Invoke(() => c.UpdateStatus(online));
                };
                flowPanelServers.Controls.Add(card);

                _ = UpdateServerStatusAsync(server, card);
            }
        }

        private async Task UpdateServerStatusAsync(ServerConfig server, ServerCard card)
        {
            bool isOnline = await CheckPingAsync(server);
            server.IsOnline = isOnline;

            Invoke(() =>
            {
                card.UpdateStatus(isOnline);
            });
        }

        private void ConnectToServer(ServerConfig server)
        {
            if (server.Mode.ToUpper() == "SSH")
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/k ssh {server.User}@{server.Host} -p {server.Port}",
                        UseShellExecute = true,
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when starting the SSH connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (server.Mode.ToUpper() == "RDP")
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "mstsc.exe",
                        Arguments = $"/v:{server.Host}:{server.Port}",
                        UseShellExecute = true,
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when starting the RDP connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (server.Mode?.ToUpper() == "WEB")
            {
                try
                {
                    string address = !string.IsNullOrWhiteSpace(server.Url) ? server.Url : server.Host;

                    if (string.IsNullOrWhiteSpace(address))
                    {
                        MessageBox.Show("No valid URL or Host specified for WEB mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string scheme = server.Port == 443 ? "https" : "http";

                    string url = $"{scheme}://{address}:{server.Port}";

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when starting the web connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Unknown connection mode: {server.Mode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnEditConfig_Click(object sender, EventArgs e)
        {
            string configFilePath = "servers.json";

            if (File.Exists(configFilePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = configFilePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("servers.json not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            if (!File.Exists("servers.json"))
            {
                MessageBox.Show("servers.json not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var json = File.ReadAllText("servers.json");
                var loaded = JsonSerializer.Deserialize<List<ServerConfig>>(json);
                if (loaded != null)
                {
                    servers = loaded;
                    PopulateServerCards();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loading error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void flowPanelServers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void InputBackupConfig(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = "servers.json";
                string backupFile = $"servers.bak";
                if (!File.Exists(sourceFile))
                {
                    MessageBox.Show($"Config was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                File.Copy(sourceFile, backupFile, overwrite: true);
                MessageBox.Show($"Config was backed up as:\n{backupFile}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when backing up the config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBackup(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Backup File";
            openFileDialog.Filter = "BAK Files (*.bak)|*.bak|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Application.StartupPath;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(openFileDialog.FileName, "servers.json", overwrite: true);

                    var json = File.ReadAllText("servers.json");
                    var loaded = JsonSerializer.Deserialize<List<ServerConfig>>(json);

                    if (loaded != null)
                    {
                        servers = loaded;
                        PopulateServerCards();
                        MessageBox.Show($"Config was loaded from:\n{openFileDialog.FileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when loading the backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task<bool> CheckPingAsync(ServerConfig config)
        {
            try
            {
                string target = config.Mode.ToUpper() switch
                {
                    "WEB" => !string.IsNullOrWhiteSpace(config.Url) ? config.Url : config.Host,
                    _ => config.Host
                };
                target = target.Replace("http://", "").Replace("https://", "").Replace("www.", "");
                if (target.Contains("/"))
                    target = target.Split('/')[0];

                using Ping ping = new Ping();
                PingReply reply = await ping.SendPingAsync(target, 1000);

                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterServerCards(txtSearch.Text);
        }

        private void FilterServerCards(string searchTerm)
        {
            searchTerm = searchTerm.Trim().ToLower();
            flowPanelServers.Controls.Clear();

            var filteredServers = servers.Where(s =>
                string.IsNullOrWhiteSpace(searchTerm) ||
                s.Name.ToLower().Contains(searchTerm) ||
                s.Host.ToLower().Contains(searchTerm) ||
                s.User.ToLower().Contains(searchTerm) ||
                (!string.IsNullOrWhiteSpace(s.Url) && s.Url.ToLower().Contains(searchTerm))).ToList();


            foreach (var server in filteredServers)
            {
                var card = new ServerCard(server);
                card.OnConnect += ConnectToServer;
                flowPanelServers.Controls.Add(card);
                _ = UpdateServerStatusAsync(server, card);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
