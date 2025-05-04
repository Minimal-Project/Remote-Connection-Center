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

        public event Action<ServerConfig> OnConnect;

        public ServerCard(ServerConfig config)
        {
            InitializeComponent();
            this.config = config;
            lblName.Text = config.Name;
            lblInfo.Text = $"{config.User}@{config.Host}";
            btnConnect.Click += (s, e) => OnConnect?.Invoke(config);
        }
        public ServerCard()
        {
            InitializeComponent();
        }

        private void ServerCard_Load(object sender, EventArgs e)
        {

        }
    }
}
