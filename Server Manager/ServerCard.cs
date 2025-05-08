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

        public event Action<ServerConfig>? OnConnect;

        public ServerCard(ServerConfig config)
        {
            InitializeComponent();
            this.config = config;
            lblName.Text = config.Name;
            string infoText = config.Mode.ToUpper() switch
            {
                "SSH" => $"{config.Host}@{config.Port}",
                "RDP" => $"{config.Host}:{config.Port}",
                "WEB" => 
                    !string.IsNullOrWhiteSpace(config.Url)
                        ? $"{config.Url}:{config.Port}"
                        : $"{config.Host}:{config.Port}",

                _ => config.Host
            };
            lblInfo.Text = infoText;

            btnConnect.Click += (s, e) => OnConnect?.Invoke(config);
        }

        private void ServerCard_Load(object sender, EventArgs e)
        {

        }
    }
}
