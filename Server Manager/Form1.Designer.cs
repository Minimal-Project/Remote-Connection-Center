namespace Server_Manager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowPanelServers = new FlowLayoutPanel();
            btnLoadConfig = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // flowPanelServers
            // 
            flowPanelServers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowPanelServers.BackColor = Color.White;
            flowPanelServers.Font = new Font("Segoe UI", 9F);
            flowPanelServers.Location = new Point(10, 45);
            flowPanelServers.Name = "flowPanelServers";
            flowPanelServers.Size = new Size(1200, 700);
            flowPanelServers.TabIndex = 1;
            flowPanelServers.Paint += flowPanelServers_Paint;
            // 
            // btnLoadConfig
            // 
            btnLoadConfig.Location = new Point(1062, 12);
            btnLoadConfig.Name = "btnLoadConfig";
            btnLoadConfig.Size = new Size(147, 23);
            btnLoadConfig.TabIndex = 0;
            btnLoadConfig.Text = "Load Config";
            btnLoadConfig.UseVisualStyleBackColor = true;
            btnLoadConfig.Click += btnLoadConfig_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1221, 756);
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
    }
}
