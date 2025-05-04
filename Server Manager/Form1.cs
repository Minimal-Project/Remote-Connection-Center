using System;
using System.Collections.Generic;
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
                    User = "user",
                    Password = ""
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
            try
            {
                using var client = new SshClient(server.Host, server.Port, server.User, server.Password);
                client.Connect();
                MessageBox.Show($"Verbunden mit {server.Name}", "SSH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                client.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Verbindung fehlgeschlagen: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            if (!File.Exists("servers.json"))
            {
                MessageBox.Show("Die Datei servers.json wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Fehler beim Laden: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void flowPanelServers_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
