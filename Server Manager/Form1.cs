using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Renci.SshNet;


namespace Server_Manager
{
    public partial class Form1 : Form
    {
        private List<ServerConfig> server = new List<ServerConfig>();
        public Form1()
        {
            InitializeComponent();
            LoadServers();
            PopulateServerCards();
        }

        private void LoadServers()
        {
            server.Add(new ServerConfig
            {
                Name = "ReHoSchi",
                Host = "192.168.31.181",
                User = "root",
                Port = 22,
                Password = ""
            });

            server.Add(new ServerConfig
            {
                Name = "StatusWebsite",
                Host = "192.168.31.116",
                User = "root",
                Port = 22,
                Password = ""
            });
            server.Add(new ServerConfig
            {
                Name = "Error",
                Host = "192.168.31.115",
                User = "root",
                Port = 22,
                Password = ""
            });
        }
        private void PopulateServerCards()
        {
            flowPanelServers.Controls.Clear();
            foreach (var server in server)
            {
                var card = new ServerCard(server);
                card.OnConnect += ConnectToServer;
                flowPanelServers.Controls.Add(card);
            }
        }
        private void ConnectToServer(ServerConfig server)
        {
            using var client = new SshClient(server.Host, server.Port, server.User, server.Password);
            try
            {
                client.Connect();
                MessageBox.Show($"Connected to {server.Name}", "SSH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                client.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to {server.Name}: {ex.Message}", "SSH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}


