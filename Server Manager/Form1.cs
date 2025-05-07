using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Renci.SshNet;

namespace Server_Manager
{
    public partial class Form1 : Form
    {
        private List<ServerConfig> servers = new();

        public Form1()
        {
            InitializeComponent();
            InitDummyServers();
            PopulateServerCards();
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
                var card = new ServerCard(server);
                card.OnConnect += ConnectToServer;
                flowPanelServers.Controls.Add(card);
            }
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

                    MessageBox.Show($"Connected with {server.Name} via CMD (SSH)", "SSH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when starting the SSH connection: { ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show($"Connected with {server.Name} via RDP", "RDP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when starting the RDP connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
