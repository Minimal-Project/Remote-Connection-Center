using System;
using System.Windows.Forms;
using Server_Manager.Theme;

namespace Server_Manager
{
    public partial class ServerCard : UserControl, IThemedControl
    {
        private readonly ServerConfig config;
        private bool themeHooked;

        public event Action<ServerConfig>? OnConnect;

        public ServerConfig Config => config;

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

            ThemeManager.ThemeChanged += OnThemeChanged;
            themeHooked = true;

            ApplyTheme();
        }

        /// <summary>
        ///  Renders <paramref name="isOnline"/>; null means the probe has not resolved yet.
        /// </summary>
        public void UpdateStatus(bool? isOnline)
        {
            config.IsOnline = isOnline;
            RenderStatus();
        }

        private void RenderStatus()
        {
            var p = ThemeManager.Palette;

            (lblStatus.Text, lblStatus.ForeColor) = config.IsOnline switch
            {
                true => ("Online", p.OnlineFore),
                false => ("Offline", p.OfflineFore),
                null => ("Checking...", p.UnknownFore)
            };
        }

        public void ApplyTheme()
        {
            var p = ThemeManager.Palette;

            BackColor = p.Surface;
            ForeColor = p.Fore;
            ThemeManager.ApplyToChildren(this);

            RenderStatus();
        }

        private void OnThemeChanged(object? sender, EventArgs e)
        {
            if (IsDisposed) return;
            ApplyTheme();
        }

        private void btnConnect_Click(object sender, EventArgs e) => OnConnect?.Invoke(config);

        private void ServerCard_Load_1(object sender, EventArgs e)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && themeHooked)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
                themeHooked = false;
            }

            base.Dispose(disposing);
        }
    }
}
