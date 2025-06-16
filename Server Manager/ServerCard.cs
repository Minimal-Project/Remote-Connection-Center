using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Manager
{
    public partial class ServerCard : UserControl
    {
        private ServerConfig config;
        private System.Windows.Forms.Timer statusTimer;

        public event Action<ServerConfig, ServerCard>? OnStatusCheckRequested;
        public event Action<ServerConfig>? OnConnect;

        public ServerCard(ServerConfig config)
        {
            InitializeComponent();
            this.config = config;

            lblName.Text = config.Name;
            lblInfo.Text = config.Mode.ToUpper() switch
            {
                "SSH" => $"{config.Host}@{config.Port}",
                "RDP" => $"{config.Host}:{config.Port}",
                "WEB" => !string.IsNullOrWhiteSpace(config.Url) ? $"{config.Url}:{config.Port}" : $"{config.Host}:{config.Port}",
                _ => config.Host
            };

            btnConnect.Click += (s, e) => OnConnect?.Invoke(config);

            statusTimer = new System.Windows.Forms.Timer();
            statusTimer.Interval = 1000;
            statusTimer.Tick += (s, e) => OnStatusCheckRequested?.Invoke(config, this);
            statusTimer.Start();
        }

        public void UpdateStatus(bool isOnline)
        {
            lblStatus.Text = isOnline ? "Online" : "Offline";
            lblStatus.ForeColor = isOnline ? Color.Green : Color.Red;
        }

        private void ServerCard_Load(object sender, EventArgs e)
        {

        }

        private void ServerCard_Load_1(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

        }
    }
}
