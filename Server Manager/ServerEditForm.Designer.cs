namespace Server_Manager
{
    partial class ServerEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerEditForm));
            cmbMode = new ComboBox();
            txtName = new TextBox();
            txtHost = new TextBox();
            txtUrl = new TextBox();
            txtUser = new TextBox();
            numPort = new NumericUpDown();
            btnOk = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            SuspendLayout();
            // 
            // cmbMode
            // 
            cmbMode.FormattingEnabled = true;
            cmbMode.Location = new Point(12, 12);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(264, 33);
            cmbMode.TabIndex = 0;
            cmbMode.Text = "SSH / RDP / WEB";
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 51);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Server Name";
            txtName.Size = new Size(264, 31);
            txtName.TabIndex = 1;
            // 
            // txtHost
            // 
            txtHost.Location = new Point(12, 88);
            txtHost.Name = "txtHost";
            txtHost.PlaceholderText = "IP/Hostname";
            txtHost.Size = new Size(264, 31);
            txtHost.TabIndex = 2;
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(12, 125);
            txtUrl.Name = "txtUrl";
            txtUrl.PlaceholderText = "URL";
            txtUrl.Size = new Size(264, 31);
            txtUrl.TabIndex = 3;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(12, 125);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "User";
            txtUser.Size = new Size(264, 31);
            txtUser.TabIndex = 4;
            // 
            // numPort
            // 
            numPort.Location = new Point(12, 162);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(264, 31);
            numPort.TabIndex = 5;
            numPort.Value = new decimal(new int[] { 22, 0, 0, 0 });
            // 
            // btnOk
            // 
            btnOk.Location = new Point(145, 199);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(131, 35);
            btnOk.TabIndex = 6;
            btnOk.Text = "Save";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 199);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(131, 35);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ServerEditForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(285, 242);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(numPort);
            Controls.Add(txtUser);
            Controls.Add(txtUrl);
            Controls.Add(txtHost);
            Controls.Add(txtName);
            Controls.Add(cmbMode);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ServerEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Server Edit";
            Load += ServerEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbMode;
        private TextBox txtName;
        private TextBox txtHost;
        private TextBox txtUrl;
        private TextBox txtUser;
        private NumericUpDown numPort;
        private Button btnOk;
        private Button btnCancel;
    }
}