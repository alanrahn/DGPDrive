namespace DGPSetup
{
    partial class SetupMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupMain));
            this.tbxDBConnStr = new System.Windows.Forms.TextBox();
            this.btnBuildTables = new System.Windows.Forms.Button();
            this.cbxSchemas = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxDGPAdminPword = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxDGPAdminUser = new System.Windows.Forms.TextBox();
            this.grpbxDBHost = new System.Windows.Forms.GroupBox();
            this.btnHelpFile = new System.Windows.Forms.Button();
            this.btnHostConnect = new System.Windows.Forms.Button();
            this.tbxDBAdminPword = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbxDBAdminUser = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbxHostName = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnHostClear = new System.Windows.Forms.Button();
            this.grpbxSysInfo = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxKeyVersion = new System.Windows.Forms.TextBox();
            this.btnNewKey = new System.Windows.Forms.Button();
            this.tbxSvcKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHostDBList = new System.Windows.Forms.DataGridView();
            this.lblSelectDatabase = new System.Windows.Forms.Label();
            this.lblSelectedConnStr = new System.Windows.Forms.Label();
            this.lblSelectSchema = new System.Windows.Forms.Label();
            this.grpbxDBHost.SuspendLayout();
            this.grpbxSysInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHostDBList)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxDBConnStr
            // 
            this.tbxDBConnStr.BackColor = System.Drawing.Color.LightGray;
            this.tbxDBConnStr.ForeColor = System.Drawing.Color.Black;
            this.tbxDBConnStr.Location = new System.Drawing.Point(326, 234);
            this.tbxDBConnStr.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDBConnStr.Multiline = true;
            this.tbxDBConnStr.Name = "tbxDBConnStr";
            this.tbxDBConnStr.Size = new System.Drawing.Size(306, 64);
            this.tbxDBConnStr.TabIndex = 14;
            this.tbxDBConnStr.TabStop = false;
            // 
            // btnBuildTables
            // 
            this.btnBuildTables.BackColor = System.Drawing.Color.LightGray;
            this.btnBuildTables.Enabled = false;
            this.btnBuildTables.ForeColor = System.Drawing.Color.Black;
            this.btnBuildTables.Location = new System.Drawing.Point(468, 308);
            this.btnBuildTables.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBuildTables.Name = "btnBuildTables";
            this.btnBuildTables.Size = new System.Drawing.Size(164, 22);
            this.btnBuildTables.TabIndex = 15;
            this.btnBuildTables.Text = "Update DB Schema/Data";
            this.btnBuildTables.UseVisualStyleBackColor = false;
            this.btnBuildTables.Click += new System.EventHandler(this.btnBuildTables_Click);
            // 
            // cbxSchemas
            // 
            this.cbxSchemas.BackColor = System.Drawing.Color.LightGray;
            this.cbxSchemas.Enabled = false;
            this.cbxSchemas.ForeColor = System.Drawing.Color.Black;
            this.cbxSchemas.FormattingEnabled = true;
            this.cbxSchemas.Items.AddRange(new object[] {
            "DGPSysInfo",
            "DGPSysWork",
            "DGPSysMetrics",
            "DGPFileStore",
            "DGPFileShard"});
            this.cbxSchemas.Location = new System.Drawing.Point(326, 27);
            this.cbxSchemas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxSchemas.Name = "cbxSchemas";
            this.cbxSchemas.Size = new System.Drawing.Size(306, 21);
            this.cbxSchemas.TabIndex = 8;
            this.cbxSchemas.SelectedIndexChanged += new System.EventHandler(this.cbxSchemas_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.DarkGray;
            this.label12.Location = new System.Drawing.Point(13, 47);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Admin Password";
            // 
            // tbxDGPAdminPword
            // 
            this.tbxDGPAdminPword.BackColor = System.Drawing.Color.LightGray;
            this.tbxDGPAdminPword.ForeColor = System.Drawing.Color.Black;
            this.tbxDGPAdminPword.Location = new System.Drawing.Point(142, 44);
            this.tbxDGPAdminPword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDGPAdminPword.Name = "tbxDGPAdminPword";
            this.tbxDGPAdminPword.PasswordChar = '*';
            this.tbxDGPAdminPword.Size = new System.Drawing.Size(151, 20);
            this.tbxDGPAdminPword.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.ForeColor = System.Drawing.Color.DarkGray;
            this.label14.Location = new System.Drawing.Point(13, 23);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Admin Username";
            // 
            // tbxDGPAdminUser
            // 
            this.tbxDGPAdminUser.BackColor = System.Drawing.Color.LightGray;
            this.tbxDGPAdminUser.ForeColor = System.Drawing.Color.Black;
            this.tbxDGPAdminUser.Location = new System.Drawing.Point(142, 20);
            this.tbxDGPAdminUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDGPAdminUser.Name = "tbxDGPAdminUser";
            this.tbxDGPAdminUser.Size = new System.Drawing.Size(151, 20);
            this.tbxDGPAdminUser.TabIndex = 9;
            // 
            // grpbxDBHost
            // 
            this.grpbxDBHost.BackColor = System.Drawing.Color.Transparent;
            this.grpbxDBHost.Controls.Add(this.btnHelpFile);
            this.grpbxDBHost.Controls.Add(this.btnHostConnect);
            this.grpbxDBHost.Controls.Add(this.tbxDBAdminPword);
            this.grpbxDBHost.Controls.Add(this.label16);
            this.grpbxDBHost.Controls.Add(this.tbxDBAdminUser);
            this.grpbxDBHost.Controls.Add(this.label17);
            this.grpbxDBHost.Controls.Add(this.tbxHostName);
            this.grpbxDBHost.Controls.Add(this.label18);
            this.grpbxDBHost.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.grpbxDBHost.Location = new System.Drawing.Point(13, 11);
            this.grpbxDBHost.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbxDBHost.Name = "grpbxDBHost";
            this.grpbxDBHost.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbxDBHost.Size = new System.Drawing.Size(297, 132);
            this.grpbxDBHost.TabIndex = 17;
            this.grpbxDBHost.TabStop = false;
            this.grpbxDBHost.Text = "Connect to SQL Server";
            // 
            // btnHelpFile
            // 
            this.btnHelpFile.BackColor = System.Drawing.Color.LightGray;
            this.btnHelpFile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHelpFile.Location = new System.Drawing.Point(12, 96);
            this.btnHelpFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHelpFile.Name = "btnHelpFile";
            this.btnHelpFile.Size = new System.Drawing.Size(108, 22);
            this.btnHelpFile.TabIndex = 4;
            this.btnHelpFile.Text = "View Help File";
            this.btnHelpFile.UseVisualStyleBackColor = false;
            this.btnHelpFile.Click += new System.EventHandler(this.btnHelpFile_Click);
            // 
            // btnHostConnect
            // 
            this.btnHostConnect.BackColor = System.Drawing.Color.LightGray;
            this.btnHostConnect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHostConnect.Location = new System.Drawing.Point(138, 96);
            this.btnHostConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHostConnect.Name = "btnHostConnect";
            this.btnHostConnect.Size = new System.Drawing.Size(145, 22);
            this.btnHostConnect.TabIndex = 6;
            this.btnHostConnect.Text = "Connect to DB";
            this.btnHostConnect.UseVisualStyleBackColor = false;
            this.btnHostConnect.Click += new System.EventHandler(this.btnHostConnect_Click);
            // 
            // tbxDBAdminPword
            // 
            this.tbxDBAdminPword.BackColor = System.Drawing.Color.LightGray;
            this.tbxDBAdminPword.ForeColor = System.Drawing.Color.Black;
            this.tbxDBAdminPword.Location = new System.Drawing.Point(138, 64);
            this.tbxDBAdminPword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDBAdminPword.Name = "tbxDBAdminPword";
            this.tbxDBAdminPword.PasswordChar = '*';
            this.tbxDBAdminPword.Size = new System.Drawing.Size(145, 20);
            this.tbxDBAdminPword.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.DarkGray;
            this.label16.Location = new System.Drawing.Point(9, 67);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "SQL Server Password";
            // 
            // tbxDBAdminUser
            // 
            this.tbxDBAdminUser.BackColor = System.Drawing.Color.LightGray;
            this.tbxDBAdminUser.ForeColor = System.Drawing.Color.Black;
            this.tbxDBAdminUser.Location = new System.Drawing.Point(138, 40);
            this.tbxDBAdminUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDBAdminUser.Name = "tbxDBAdminUser";
            this.tbxDBAdminUser.Size = new System.Drawing.Size(145, 20);
            this.tbxDBAdminUser.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.DarkGray;
            this.label17.Location = new System.Drawing.Point(9, 43);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(115, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "SQL Server UserName";
            // 
            // tbxHostName
            // 
            this.tbxHostName.BackColor = System.Drawing.Color.LightGray;
            this.tbxHostName.ForeColor = System.Drawing.Color.Black;
            this.tbxHostName.Location = new System.Drawing.Point(138, 16);
            this.tbxHostName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxHostName.Name = "tbxHostName";
            this.tbxHostName.Size = new System.Drawing.Size(145, 20);
            this.tbxHostName.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.DarkGray;
            this.label18.Location = new System.Drawing.Point(9, 19);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(118, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "SQL Server Host Name";
            // 
            // btnHostClear
            // 
            this.btnHostClear.BackColor = System.Drawing.Color.LightGray;
            this.btnHostClear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHostClear.Location = new System.Drawing.Point(326, 308);
            this.btnHostClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHostClear.Name = "btnHostClear";
            this.btnHostClear.Size = new System.Drawing.Size(77, 22);
            this.btnHostClear.TabIndex = 5;
            this.btnHostClear.Text = "Clear";
            this.btnHostClear.UseVisualStyleBackColor = false;
            this.btnHostClear.Click += new System.EventHandler(this.btnHostClear_Click);
            // 
            // grpbxSysInfo
            // 
            this.grpbxSysInfo.BackColor = System.Drawing.Color.Transparent;
            this.grpbxSysInfo.Controls.Add(this.label14);
            this.grpbxSysInfo.Controls.Add(this.label3);
            this.grpbxSysInfo.Controls.Add(this.label12);
            this.grpbxSysInfo.Controls.Add(this.tbxKeyVersion);
            this.grpbxSysInfo.Controls.Add(this.tbxDGPAdminUser);
            this.grpbxSysInfo.Controls.Add(this.btnNewKey);
            this.grpbxSysInfo.Controls.Add(this.tbxDGPAdminPword);
            this.grpbxSysInfo.Controls.Add(this.tbxSvcKey);
            this.grpbxSysInfo.Controls.Add(this.label1);
            this.grpbxSysInfo.Enabled = false;
            this.grpbxSysInfo.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.grpbxSysInfo.Location = new System.Drawing.Point(326, 55);
            this.grpbxSysInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbxSysInfo.Name = "grpbxSysInfo";
            this.grpbxSysInfo.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbxSysInfo.Size = new System.Drawing.Size(306, 154);
            this.grpbxSysInfo.TabIndex = 21;
            this.grpbxSysInfo.TabStop = false;
            this.grpbxSysInfo.Text = "(optional) SysInfo Values";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Key Version";
            // 
            // tbxKeyVersion
            // 
            this.tbxKeyVersion.BackColor = System.Drawing.Color.LightGray;
            this.tbxKeyVersion.ForeColor = System.Drawing.Color.Black;
            this.tbxKeyVersion.Location = new System.Drawing.Point(142, 122);
            this.tbxKeyVersion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxKeyVersion.Name = "tbxKeyVersion";
            this.tbxKeyVersion.Size = new System.Drawing.Size(151, 20);
            this.tbxKeyVersion.TabIndex = 13;
            this.tbxKeyVersion.TextChanged += new System.EventHandler(this.tbxKeyVersion_TextChanged);
            // 
            // btnNewKey
            // 
            this.btnNewKey.BackColor = System.Drawing.Color.LightGray;
            this.btnNewKey.ForeColor = System.Drawing.Color.Black;
            this.btnNewKey.Location = new System.Drawing.Point(16, 96);
            this.btnNewKey.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNewKey.Name = "btnNewKey";
            this.btnNewKey.Size = new System.Drawing.Size(117, 22);
            this.btnNewKey.TabIndex = 11;
            this.btnNewKey.Text = "Create New Key";
            this.btnNewKey.UseVisualStyleBackColor = false;
            this.btnNewKey.Click += new System.EventHandler(this.btnNewKey_Click);
            // 
            // tbxSvcKey
            // 
            this.tbxSvcKey.BackColor = System.Drawing.Color.LightGray;
            this.tbxSvcKey.ForeColor = System.Drawing.Color.Black;
            this.tbxSvcKey.Location = new System.Drawing.Point(142, 68);
            this.tbxSvcKey.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxSvcKey.Multiline = true;
            this.tbxSvcKey.Name = "tbxSvcKey";
            this.tbxSvcKey.Size = new System.Drawing.Size(151, 50);
            this.tbxSvcKey.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(13, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Web Service Key";
            // 
            // dgvHostDBList
            // 
            this.dgvHostDBList.AllowUserToAddRows = false;
            this.dgvHostDBList.AllowUserToDeleteRows = false;
            this.dgvHostDBList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvHostDBList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHostDBList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHostDBList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvHostDBList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHostDBList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHostDBList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHostDBList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHostDBList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvHostDBList.Enabled = false;
            this.dgvHostDBList.EnableHeadersVisualStyles = false;
            this.dgvHostDBList.GridColor = System.Drawing.Color.Black;
            this.dgvHostDBList.Location = new System.Drawing.Point(13, 170);
            this.dgvHostDBList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvHostDBList.MultiSelect = false;
            this.dgvHostDBList.Name = "dgvHostDBList";
            this.dgvHostDBList.ReadOnly = true;
            this.dgvHostDBList.RowHeadersWidth = 10;
            this.dgvHostDBList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHostDBList.ShowEditingIcon = false;
            this.dgvHostDBList.Size = new System.Drawing.Size(297, 160);
            this.dgvHostDBList.TabIndex = 7;
            this.dgvHostDBList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHostDBList_CellDoubleClick);
            this.dgvHostDBList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHostDBList_CellDoubleClick);
            // 
            // lblSelectDatabase
            // 
            this.lblSelectDatabase.AutoSize = true;
            this.lblSelectDatabase.Enabled = false;
            this.lblSelectDatabase.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblSelectDatabase.Location = new System.Drawing.Point(10, 152);
            this.lblSelectDatabase.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectDatabase.Name = "lblSelectDatabase";
            this.lblSelectDatabase.Size = new System.Drawing.Size(157, 13);
            this.lblSelectDatabase.TabIndex = 19;
            this.lblSelectDatabase.Text = "Select Database to be Updated";
            // 
            // lblSelectedConnStr
            // 
            this.lblSelectedConnStr.AutoSize = true;
            this.lblSelectedConnStr.Enabled = false;
            this.lblSelectedConnStr.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblSelectedConnStr.Location = new System.Drawing.Point(323, 219);
            this.lblSelectedConnStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectedConnStr.Name = "lblSelectedConnStr";
            this.lblSelectedConnStr.Size = new System.Drawing.Size(142, 13);
            this.lblSelectedConnStr.TabIndex = 20;
            this.lblSelectedConnStr.Text = "ADO.NET Connection String";
            // 
            // lblSelectSchema
            // 
            this.lblSelectSchema.AutoSize = true;
            this.lblSelectSchema.Enabled = false;
            this.lblSelectSchema.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblSelectSchema.Location = new System.Drawing.Point(323, 11);
            this.lblSelectSchema.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectSchema.Name = "lblSelectSchema";
            this.lblSelectSchema.Size = new System.Drawing.Size(170, 13);
            this.lblSelectSchema.TabIndex = 22;
            this.lblSelectSchema.Text = "Select the Matching DGP Schema";
            // 
            // SetupMain
            // 
            this.AcceptButton = this.btnHostConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(649, 346);
            this.Controls.Add(this.lblSelectSchema);
            this.Controls.Add(this.grpbxSysInfo);
            this.Controls.Add(this.btnHostClear);
            this.Controls.Add(this.cbxSchemas);
            this.Controls.Add(this.tbxDBConnStr);
            this.Controls.Add(this.btnBuildTables);
            this.Controls.Add(this.lblSelectedConnStr);
            this.Controls.Add(this.lblSelectDatabase);
            this.Controls.Add(this.dgvHostDBList);
            this.Controls.Add(this.grpbxDBHost);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "SetupMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DGPDrive Database Setup";
            this.grpbxDBHost.ResumeLayout(false);
            this.grpbxDBHost.PerformLayout();
            this.grpbxSysInfo.ResumeLayout(false);
            this.grpbxSysInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHostDBList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBuildTables;
        private System.Windows.Forms.ComboBox cbxSchemas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxDGPAdminPword;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxDGPAdminUser;
        private System.Windows.Forms.GroupBox grpbxDBHost;
        private System.Windows.Forms.Button btnHostConnect;
        private System.Windows.Forms.Button btnHostClear;
        private System.Windows.Forms.TextBox tbxDBAdminPword;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbxDBAdminUser;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxHostName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbxDBConnStr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSvcKey;
        private System.Windows.Forms.Button btnNewKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxKeyVersion;
        private System.Windows.Forms.Button btnHelpFile;
        private System.Windows.Forms.GroupBox grpbxSysInfo;
        private System.Windows.Forms.DataGridView dgvHostDBList;
        private System.Windows.Forms.Label lblSelectDatabase;
        private System.Windows.Forms.Label lblSelectedConnStr;
        private System.Windows.Forms.Label lblSelectSchema;
    }
}