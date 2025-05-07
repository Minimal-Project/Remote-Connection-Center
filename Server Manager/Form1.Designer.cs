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
            flowPanelServers.BackColor = Color.White;
            flowPanelServers.Font = new Font("Segoe UI", 9F);
            flowPanelServers.Location = new Point(10, 94);
            flowPanelServers.Name = "flowPanelServers";
            flowPanelServers.Size = new Size(1216, 652);
            flowPanelServers.TabIndex = 1;
            flowPanelServers.Paint += flowPanelServers_Paint;
            // 
            // btnLoadConfig
            // 
            btnLoadConfig.Location = new Point(154, 5);
            btnLoadConfig.Name = "btnLoadConfig";
            btnLoadConfig.Size = new Size(147, 23);
            btnLoadConfig.TabIndex = 0;
            btnLoadConfig.Text = "Load Config";
            btnLoadConfig.UseVisualStyleBackColor = true;
            btnLoadConfig.Click += btnLoadConfig_Click;
            // 
            // btnEditConfig
            // 
            btnEditConfig.Location = new Point(3, 5);
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
            panel1.Controls.Add(btnLoadBackup);
            panel1.Controls.Add(btnBackupConfig);
            panel1.Controls.Add(btnEditConfig);
            panel1.Controls.Add(btnLoadConfig);
            panel1.Location = new Point(925, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(301, 65);
            panel1.TabIndex = 2;
            // 
            // btnLoadBackup
            // 
            btnLoadBackup.Location = new Point(3, 34);
            btnLoadBackup.Name = "btnLoadBackup";
            btnLoadBackup.Size = new Size(147, 23);
            btnLoadBackup.TabIndex = 3;
            btnLoadBackup.Text = "Load Backup";
            btnLoadBackup.UseVisualStyleBackColor = true;
            btnLoadBackup.Click += LoadBackup;
            // 
            // btnBackupConfig
            // 
            btnBackupConfig.Location = new Point(154, 34);
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
            pictureBox1.Location = new Point(14, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(217, 82);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Logo;
            pictureBox2.Location = new Point(237, 6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(84, 82);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1237, 757);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(flowPanelServers);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "RCC - Remote Connection Center";
            panel1.ResumeLayout(false);
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
    }
}
