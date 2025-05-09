namespace Server_Manager
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            flowPanelServers = new FlowLayoutPanel();
            btnLoadConfig = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btnEditConfig = new Button();
            panel1 = new Panel();
            txtSearch = new TextBox();
            btnLoadBackup = new Button();
            btnBackupConfig = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // flowPanelServers
            // 
            flowPanelServers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowPanelServers.AutoScroll = true;
            flowPanelServers.AutoScrollMargin = new Size(0, 10);
            flowPanelServers.AutoScrollMinSize = new Size(0, 5);
            flowPanelServers.BackColor = Color.White;
            flowPanelServers.Font = new Font("Segoe UI", 9F);
            flowPanelServers.Location = new Point(10, 108);
            flowPanelServers.Name = "flowPanelServers";
            flowPanelServers.Size = new Size(1233, 638);
            flowPanelServers.TabIndex = 1;
            flowPanelServers.Paint += flowPanelServers_Paint;
            // 
            // btnLoadConfig
            // 
            btnLoadConfig.Location = new Point(151, 32);
            btnLoadConfig.Name = "btnLoadConfig";
            btnLoadConfig.Size = new Size(147, 23);
            btnLoadConfig.TabIndex = 0;
            btnLoadConfig.Text = "Load Config";
            btnLoadConfig.UseVisualStyleBackColor = true;
            btnLoadConfig.Click += btnLoadConfig_Click;
            // 
            // btnEditConfig
            // 
            btnEditConfig.Location = new Point(0, 32);
            btnEditConfig.Name = "btnEditConfig";
            btnEditConfig.Size = new Size(147, 23);
            btnEditConfig.TabIndex = 1;
            btnEditConfig.Text = "Edit Config";
            btnEditConfig.UseVisualStyleBackColor = true;
            btnEditConfig.Click += btnEditConfig_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(btnLoadBackup);
            panel1.Controls.Add(btnBackupConfig);
            panel1.Controls.Add(btnEditConfig);
            panel1.Controls.Add(btnLoadConfig);
            panel1.Location = new Point(942, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(301, 96);
            panel1.TabIndex = 2;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(3, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(295, 23);
            txtSearch.TabIndex = 5;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnLoadBackup
            // 
            btnLoadBackup.Location = new Point(0, 61);
            btnLoadBackup.Name = "btnLoadBackup";
            btnLoadBackup.Size = new Size(147, 23);
            btnLoadBackup.TabIndex = 3;
            btnLoadBackup.Text = "Load Backup";
            btnLoadBackup.UseVisualStyleBackColor = true;
            btnLoadBackup.Click += LoadBackup;
            // 
            // btnBackupConfig
            // 
            btnBackupConfig.Location = new Point(151, 61);
            btnBackupConfig.Name = "btnBackupConfig";
            btnBackupConfig.Size = new Size(147, 23);
            btnBackupConfig.TabIndex = 2;
            btnBackupConfig.Text = "Backup Config";
            btnBackupConfig.UseVisualStyleBackColor = true;
            btnBackupConfig.Click += InputBackupConfig;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ReHoGaBanner3_0_Transparent;
            pictureBox1.Location = new Point(4, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(251, 96);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.rcc_mP4O8xLXoZTXz80j__1_;
            pictureBox2.Location = new Point(261, 6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(102, 96);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1254, 757);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(flowPanelServers);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(665, 500);
            Name = "Form1";
            Text = "RCC - Remote Connection Center";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowPanelServers;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button btnLoadConfig;
        private Button btnEditConfig;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Button btnBackupConfig;
        private Button btnLoadBackup;
        private PictureBox pictureBox2;
        private TextBox txtSearch;
    }
}
