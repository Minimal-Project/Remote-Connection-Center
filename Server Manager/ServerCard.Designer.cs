namespace Server_Manager
{
    partial class ServerCard
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            lblName = new Label();
            lblInfo = new Label();
            btnConnect = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(19, 11);
            lblName.Name = "lblName";
            lblName.Size = new Size(74, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Server Name";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(19, 36);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(63, 15);
            lblInfo.TabIndex = 1;
            lblInfo.Text = "Server Info";
            // 
            // btnConnect
            // 
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Location = new Point(19, 54);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(84, 23);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            // 
            // ServerCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnConnect);
            Controls.Add(lblInfo);
            Controls.Add(lblName);
            Name = "ServerCard";
            Size = new Size(255, 100);
            Load += ServerCard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblInfo;
        private Button btnConnect;
    }
}
