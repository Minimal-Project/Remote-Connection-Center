namespace Server_Manager
{
    partial class ServerCard
    {
        private Label lblName;
        private Label lblInfo;
        private Button btnConnect;

        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.lblInfo = new Label();
            this.btnConnect = new Button();

            this.SuspendLayout();

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(10, 10);
            this.lblName.Size = new System.Drawing.Size(100, 21);

            // lblInfo
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(10, 40);
            this.lblInfo.Size = new System.Drawing.Size(200, 15);

            // btnConnect
            this.btnConnect.Location = new System.Drawing.Point(10, 65);
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.Text = "Verbinden";

            // ServerCard
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnConnect);
            this.Size = new System.Drawing.Size(300, 100);
            this.BorderStyle = BorderStyle.FixedSingle;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
