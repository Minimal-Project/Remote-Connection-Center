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
            lblName.Size = new Size(0, 32);
            lblName.TabIndex = 0;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(10, 40);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(0, 25);
            lblInfo.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(10, 73);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(100, 35);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.Click += btnConnect_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(238, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(65, 25);
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
            Size = new Size(325, 120);
            Load += ServerCard_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblStatus;
    }
}
