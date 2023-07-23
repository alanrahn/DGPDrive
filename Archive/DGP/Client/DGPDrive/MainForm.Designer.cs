namespace DGPDrive
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.connectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rsaKeysMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filestoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testEVLoggingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apimethodsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apirolesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apiusersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datagroupsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repWorkScheduleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genWorkScheduleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apiTestingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.errorLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testResultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoWorkLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.latticeMetricsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.CountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblMethCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.DividerLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblLatency = new System.Windows.Forms.ToolStripStatusLabel();
            this.DividerLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ServerStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerConnect = new System.Windows.Forms.Timer(this.components);
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.Color.Silver;
            this.menuMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectMenuItem,
            this.helpMenuItem,
            this.storageMenuItem,
            this.userMenuItem,
            this.securityMenuItem,
            this.configurationMenuItem,
            this.testingMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1924, 42);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // connectMenuItem
            // 
            this.connectMenuItem.Name = "connectMenuItem";
            this.connectMenuItem.Size = new System.Drawing.Size(124, 38);
            this.connectMenuItem.Text = "Connect";
            this.connectMenuItem.Click += new System.EventHandler(this.connectMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.utilityMenuItem,
            this.rsaKeysMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(85, 38);
            this.helpMenuItem.Text = "Help";
            // 
            // utilityMenuItem
            // 
            this.utilityMenuItem.Name = "utilityMenuItem";
            this.utilityMenuItem.Size = new System.Drawing.Size(359, 44);
            this.utilityMenuItem.Text = "Utility";
            this.utilityMenuItem.Click += new System.EventHandler(this.utilityMenuItem_Click);
            // 
            // rsaKeysMenuItem
            // 
            this.rsaKeysMenuItem.Name = "rsaKeysMenuItem";
            this.rsaKeysMenuItem.Size = new System.Drawing.Size(359, 44);
            this.rsaKeysMenuItem.Text = "RSA Keys";
            this.rsaKeysMenuItem.Click += new System.EventHandler(this.rsaKeysMenuItem_Click);
            // 
            // storageMenuItem
            // 
            this.storageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filestoreMenuItem,
            this.tagsMenuItem});
            this.storageMenuItem.Name = "storageMenuItem";
            this.storageMenuItem.Size = new System.Drawing.Size(116, 38);
            this.storageMenuItem.Text = "Storage";
            this.storageMenuItem.Visible = false;
            // 
            // filestoreMenuItem
            // 
            this.filestoreMenuItem.Name = "filestoreMenuItem";
            this.filestoreMenuItem.Size = new System.Drawing.Size(241, 44);
            this.filestoreMenuItem.Text = "FileStore";
            this.filestoreMenuItem.Click += new System.EventHandler(this.filestoreMenuItem_Click);
            // 
            // tagsMenuItem
            // 
            this.tagsMenuItem.Name = "tagsMenuItem";
            this.tagsMenuItem.Size = new System.Drawing.Size(241, 44);
            this.tagsMenuItem.Text = "Tags";
            this.tagsMenuItem.Click += new System.EventHandler(this.tagsMenuItem_Click);
            // 
            // userMenuItem
            // 
            this.userMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordMenuItem,
            this.editProfileMenuItem,
            this.testEVLoggingMenuItem});
            this.userMenuItem.Name = "userMenuItem";
            this.userMenuItem.Size = new System.Drawing.Size(82, 38);
            this.userMenuItem.Text = "User";
            this.userMenuItem.Visible = false;
            // 
            // changePasswordMenuItem
            // 
            this.changePasswordMenuItem.Name = "changePasswordMenuItem";
            this.changePasswordMenuItem.Size = new System.Drawing.Size(342, 44);
            this.changePasswordMenuItem.Text = "Change Password";
            this.changePasswordMenuItem.Click += new System.EventHandler(this.changePasswordMenuItem_Click);
            // 
            // editProfileMenuItem
            // 
            this.editProfileMenuItem.Name = "editProfileMenuItem";
            this.editProfileMenuItem.Size = new System.Drawing.Size(342, 44);
            this.editProfileMenuItem.Text = "Edit Profile";
            this.editProfileMenuItem.Click += new System.EventHandler(this.editProfileMenuItem_Click);
            // 
            // testEVLoggingMenuItem
            // 
            this.testEVLoggingMenuItem.Name = "testEVLoggingMenuItem";
            this.testEVLoggingMenuItem.Size = new System.Drawing.Size(342, 44);
            this.testEVLoggingMenuItem.Text = "Test Error Logging";
            this.testEVLoggingMenuItem.Click += new System.EventHandler(this.testLoggingMenuItem_Click);
            // 
            // securityMenuItem
            // 
            this.securityMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apimethodsMenuItem,
            this.apirolesMenuItem,
            this.apiusersMenuItem,
            this.datagroupsMenuItem});
            this.securityMenuItem.Name = "securityMenuItem";
            this.securityMenuItem.Size = new System.Drawing.Size(120, 38);
            this.securityMenuItem.Text = "Security";
            this.securityMenuItem.Visible = false;
            // 
            // apimethodsMenuItem
            // 
            this.apimethodsMenuItem.Name = "apimethodsMenuItem";
            this.apimethodsMenuItem.Size = new System.Drawing.Size(285, 44);
            this.apimethodsMenuItem.Text = "API Methods";
            this.apimethodsMenuItem.Click += new System.EventHandler(this.methodsMenuItem_Click);
            // 
            // apirolesMenuItem
            // 
            this.apirolesMenuItem.Name = "apirolesMenuItem";
            this.apirolesMenuItem.Size = new System.Drawing.Size(285, 44);
            this.apirolesMenuItem.Text = "API Roles";
            this.apirolesMenuItem.Click += new System.EventHandler(this.rolesMenuItem_Click);
            // 
            // apiusersMenuItem
            // 
            this.apiusersMenuItem.Name = "apiusersMenuItem";
            this.apiusersMenuItem.Size = new System.Drawing.Size(285, 44);
            this.apiusersMenuItem.Text = "API Users";
            this.apiusersMenuItem.Click += new System.EventHandler(this.usersMenuItem_Click);
            // 
            // datagroupsMenuItem
            // 
            this.datagroupsMenuItem.Name = "datagroupsMenuItem";
            this.datagroupsMenuItem.Size = new System.Drawing.Size(285, 44);
            this.datagroupsMenuItem.Text = "Data Groups";
            this.datagroupsMenuItem.Click += new System.EventHandler(this.dataGroupsMenuItem_Click);
            // 
            // configurationMenuItem
            // 
            this.configurationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repWorkScheduleMenuItem,
            this.genWorkScheduleMenuItem});
            this.configurationMenuItem.Name = "configurationMenuItem";
            this.configurationMenuItem.Size = new System.Drawing.Size(182, 38);
            this.configurationMenuItem.Text = "Configuration";
            this.configurationMenuItem.Visible = false;
            // 
            // repWorkScheduleMenuItem
            // 
            this.repWorkScheduleMenuItem.Name = "repWorkScheduleMenuItem";
            this.repWorkScheduleMenuItem.Size = new System.Drawing.Size(391, 44);
            this.repWorkScheduleMenuItem.Text = "ReplicaWork Schedule";
            this.repWorkScheduleMenuItem.Click += new System.EventHandler(this.replicaScheduleMenuItem_Click);
            // 
            // genWorkScheduleMenuItem
            // 
            this.genWorkScheduleMenuItem.Name = "genWorkScheduleMenuItem";
            this.genWorkScheduleMenuItem.Size = new System.Drawing.Size(391, 44);
            this.genWorkScheduleMenuItem.Text = "GeneralWork Schedule";
            this.genWorkScheduleMenuItem.Click += new System.EventHandler(this.genWorkScheduleMenuItem_Click);
            // 
            // testingMenuItem
            // 
            this.testingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apiTestingMenuItem,
            this.toolStripSeparator1,
            this.errorLogMenuItem,
            this.testResultsMenuItem,
            this.autoWorkLogMenuItem,
            this.latticeMetricsMenuItem});
            this.testingMenuItem.Name = "testingMenuItem";
            this.testingMenuItem.Size = new System.Drawing.Size(111, 38);
            this.testingMenuItem.Text = "Testing";
            this.testingMenuItem.Visible = false;
            // 
            // apiTestingMenuItem
            // 
            this.apiTestingMenuItem.Name = "apiTestingMenuItem";
            this.apiTestingMenuItem.Size = new System.Drawing.Size(303, 44);
            this.apiTestingMenuItem.Text = "API Testing";
            this.apiTestingMenuItem.Click += new System.EventHandler(this.testHarnessMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(300, 6);
            // 
            // errorLogMenuItem
            // 
            this.errorLogMenuItem.Name = "errorLogMenuItem";
            this.errorLogMenuItem.Size = new System.Drawing.Size(303, 44);
            this.errorLogMenuItem.Text = "Error Log";
            this.errorLogMenuItem.Click += new System.EventHandler(this.errorLogMenuItem_Click);
            // 
            // testResultsMenuItem
            // 
            this.testResultsMenuItem.Name = "testResultsMenuItem";
            this.testResultsMenuItem.Size = new System.Drawing.Size(303, 44);
            this.testResultsMenuItem.Text = "Test Results";
            this.testResultsMenuItem.Click += new System.EventHandler(this.testResultsMenuItem_Click);
            // 
            // autoWorkLogMenuItem
            // 
            this.autoWorkLogMenuItem.Name = "autoWorkLogMenuItem";
            this.autoWorkLogMenuItem.Size = new System.Drawing.Size(303, 44);
            this.autoWorkLogMenuItem.Text = "AutoWork Log";
            this.autoWorkLogMenuItem.Click += new System.EventHandler(this.autoWorkLogMenuItem_Click);
            // 
            // latticeMetricsMenuItem
            // 
            this.latticeMetricsMenuItem.Name = "latticeMetricsMenuItem";
            this.latticeMetricsMenuItem.Size = new System.Drawing.Size(303, 44);
            this.latticeMetricsMenuItem.Text = "Lattice Metrics";
            this.latticeMetricsMenuItem.Click += new System.EventHandler(this.latticeMetricsMenuItem_Click);
            // 
            // statusMain
            // 
            this.statusMain.BackColor = System.Drawing.Color.Silver;
            this.statusMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CountStatusLabel,
            this.tslblMethCount,
            this.DividerLabel3,
            this.TimeStatusLabel,
            this.tslblLatency,
            this.DividerLabel4,
            this.ServerStatusLabel,
            this.tslblServer});
            this.statusMain.Location = new System.Drawing.Point(0, 1018);
            this.statusMain.Name = "statusMain";
            this.statusMain.Padding = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.statusMain.Size = new System.Drawing.Size(1924, 42);
            this.statusMain.TabIndex = 1;
            this.statusMain.Text = "statusStrip1";
            // 
            // CountStatusLabel
            // 
            this.CountStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.CountStatusLabel.Name = "CountStatusLabel";
            this.CountStatusLabel.Size = new System.Drawing.Size(108, 32);
            this.CountStatusLabel.Text = "Methods:";
            // 
            // tslblMethCount
            // 
            this.tslblMethCount.Name = "tslblMethCount";
            this.tslblMethCount.Size = new System.Drawing.Size(28, 32);
            this.tslblMethCount.Text = "0";
            // 
            // DividerLabel3
            // 
            this.DividerLabel3.Name = "DividerLabel3";
            this.DividerLabel3.Size = new System.Drawing.Size(21, 32);
            this.DividerLabel3.Text = "|";
            // 
            // TimeStatusLabel
            // 
            this.TimeStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TimeStatusLabel.Name = "TimeStatusLabel";
            this.TimeStatusLabel.Size = new System.Drawing.Size(112, 32);
            this.TimeStatusLabel.Text = "ClientMS:";
            // 
            // tslblLatency
            // 
            this.tslblLatency.Name = "tslblLatency";
            this.tslblLatency.Size = new System.Drawing.Size(28, 32);
            this.tslblLatency.Text = "0";
            // 
            // DividerLabel4
            // 
            this.DividerLabel4.Name = "DividerLabel4";
            this.DividerLabel4.Size = new System.Drawing.Size(21, 32);
            this.DividerLabel4.Text = "|";
            // 
            // ServerStatusLabel
            // 
            this.ServerStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.ServerStatusLabel.Name = "ServerStatusLabel";
            this.ServerStatusLabel.Size = new System.Drawing.Size(117, 32);
            this.ServerStatusLabel.Text = "ServerMS:";
            // 
            // tslblServer
            // 
            this.tslblServer.Name = "tslblServer";
            this.tslblServer.Size = new System.Drawing.Size(28, 32);
            this.tslblServer.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1924, 1060);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(3564, 1797);
            this.MinimumSize = new System.Drawing.Size(1846, 1047);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DGP Lattice Beta";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem connectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datagroupsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apimethodsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apirolesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apiusersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apiTestingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel CountStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tslblMethCount;
        private System.Windows.Forms.ToolStripStatusLabel DividerLabel3;
        private System.Windows.Forms.ToolStripStatusLabel TimeStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tslblLatency;
        private System.Windows.Forms.ToolStripStatusLabel DividerLabel4;
        private System.Windows.Forms.ToolStripStatusLabel ServerStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tslblServer;
        private System.Windows.Forms.Timer timerConnect;
        private System.Windows.Forms.ToolStripMenuItem userMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editProfileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repWorkScheduleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genWorkScheduleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testEVLoggingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filestoreMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem testResultsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoWorkLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem latticeMetricsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rsaKeysMenuItem;
    }
}