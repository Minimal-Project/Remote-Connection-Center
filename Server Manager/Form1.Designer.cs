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
            flowPanelServers = new FlowLayoutPanel();
            btnLoadConfig = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btnEditConfig = new Button();
            SuspendLayout();

            flowPanelServers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowPanelServers.BackColor = Color.White;
            flowPanelServers.Font = new Font("Segoe UI", 9F);
            flowPanelServers.Location = new Point(10, 45);
            flowPanelServers.Name = "flowPanelServers";
            flowPanelServers.Size = new Size(1200, 700);
            flowPanelServers.TabIndex = 1;
            flowPanelServers.Paint += flowPanelServers_Paint;
 
            btnLoadConfig.Location = new Point(1063, 12);
            btnLoadConfig.Name = "btnLoadConfig";
            btnLoadConfig.Size = new Size(147, 23);
            btnLoadConfig.TabIndex = 0;
            btnLoadConfig.Text = "Load Config";
            btnLoadConfig.UseVisualStyleBackColor = true;
            btnLoadConfig.Click += btnLoadConfig_Click;

            btnEditConfig.Location = new Point(910, 12);
            btnEditConfig.Name = "btnEditConfig";
            btnEditConfig.Size = new Size(147, 23);
            btnEditConfig.TabIndex = 1;
            btnEditConfig.Text = "Edit Config";
            btnEditConfig.UseVisualStyleBackColor = true;
            btnEditConfig.Click += btnEditConfig_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1221, 756);
            Controls.Add(btnEditConfig);
            Controls.Add(btnLoadConfig);
            Controls.Add(flowPanelServers);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowPanelServers;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button btnLoadConfig;
        private Button btnEditConfig;
    }
}
