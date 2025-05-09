namespace Server_Manager
{
    partial class ServerCard
    {
        private Label lblName;
        private Label lblInfo;
        private Button btnConnect;

        private void InitializeComponent()
        {
            lblName = new Label();
            lblInfo = new Label();
            btnConnect = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblName.Location = new Point(10, 10);
            lblName.Name = "lblName";
            lblName.Size = new Size(0, 21);
            lblName.TabIndex = 0;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(10, 40);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(0, 15);
            lblInfo.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(10, 65);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(238, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(43, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Offline";
            // 
            // ServerCard
            // 
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblStatus);
            Controls.Add(lblName);
            Controls.Add(lblInfo);
            Controls.Add(btnConnect);
            Name = "ServerCard";
            Size = new Size(298, 98);
            Load += ServerCard_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblStatus;
    }
}
