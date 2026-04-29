using System.Windows.Forms;
using Server_Manager.Theme;

namespace Server_Manager
{
    public partial class ServerEditForm : Form
    {
        public ServerConfig Result { get; private set; }

        public ServerEditForm(ServerConfig config)
        {
            InitializeComponent();
            ThemeManager.ApplyTo(this);
            Result = config;

            cmbMode.Items.AddRange(new[] { "SSH", "RDP", "WEB" });
            cmbMode.SelectedItem = config.Mode ?? "SSH";
            cmbMode.SelectedIndexChanged += (s, e) => UpdateFieldVisibility();

            txtName.Text = config.Name;
            txtHost.Text = config.Host;
            txtUrl.Text = config.Url ?? "";
            txtUser.Text = config.User;
            numPort.Minimum = 1;
            numPort.Maximum = 65535;
            numPort.Value = config.Port;

            btnOk.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnOk.Click += (s, e) => Apply();

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            UpdateFieldVisibility();
        }

        private void ServerEditForm_Load(object sender, System.EventArgs e) { }

        private void UpdateFieldVisibility()
        {
            bool isWeb = cmbMode.SelectedItem?.ToString() == "WEB";
            bool isSsh = cmbMode.SelectedItem?.ToString() == "SSH";
            bool isRdp = cmbMode.SelectedItem?.ToString() == "RDP";

            txtUrl.Visible = isWeb;
            txtUser.Visible = isSsh || isRdp;
        }

        private void Apply()
        {
            Result.Name = txtName.Text.Trim();
            Result.Host = txtHost.Text.Trim();
            Result.Url = txtUrl.Text.Trim();
            Result.User = txtUser.Text.Trim();
            Result.Port = (int)numPort.Value;
            Result.Mode = cmbMode.SelectedItem?.ToString() ?? "SSH";
        }
    }
}