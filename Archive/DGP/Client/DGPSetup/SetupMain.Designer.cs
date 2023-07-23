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
            this.btnRunScan = new System.Windows.Forms.Button();
            this.grpbxDBHost = new System.Windows.Forms.GroupBox();
            this.btnHelpFile = new System.Windows.Forms.Button();
            this.btnHostConnect = new System.Windows.Forms.Button();
            this.tbxDBAdminPword = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbxDBAdminUser = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbxHostName = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvHostDBList = new System.Windows.Forms.DataGridView();
            this.lblSelectDatabase = new System.Windows.Forms.Label();
            this.webbrResults = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbxMethTest = new System.Windows.Forms.CheckBox();
            this.grpbxDBHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHostDBList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRunScan
            // 
            this.btnRunScan.BackColor = System.Drawing.Color.LightGray;
            this.btnRunScan.Enabled = false;
            this.btnRunScan.ForeColor = System.Drawing.Color.Black;
            this.btnRunScan.Location = new System.Drawing.Point(228, 344);
            this.btnRunScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnRunScan.Name = "btnRunScan";
            this.btnRunScan.Size = new System.Drawing.Size(91, 22);
            this.btnRunScan.TabIndex = 15;
            this.btnRunScan.Text = "Run Scan";
            this.btnRunScan.UseVisualStyleBackColor = false;
            this.btnRunScan.Click += new System.EventHandler(this.btnRunScan_Click);
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
            this.grpbxDBHost.Margin = new System.Windows.Forms.Padding(2);
            this.grpbxDBHost.Name = "grpbxDBHost";
            this.grpbxDBHost.Padding = new System.Windows.Forms.Padding(2);
            this.grpbxDBHost.Size = new System.Drawing.Size(306, 132);
            this.grpbxDBHost.TabIndex = 17;
            this.grpbxDBHost.TabStop = false;
            this.grpbxDBHost.Text = "Connect to SQL Server";
            // 
            // btnHelpFile
            // 
            this.btnHelpFile.BackColor = System.Drawing.Color.LightGray;
            this.btnHelpFile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHelpFile.Location = new System.Drawing.Point(12, 96);
            this.btnHelpFile.Margin = new System.Windows.Forms.Padding(2);
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
            this.btnHostConnect.Location = new System.Drawing.Point(131, 96);
            this.btnHostConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnHostConnect.Name = "btnHostConnect";
            this.btnHostConnect.Size = new System.Drawing.Size(162, 22);
            this.btnHostConnect.TabIndex = 6;
            this.btnHostConnect.Text = "Connect to DB";
            this.btnHostConnect.UseVisualStyleBackColor = false;
            this.btnHostConnect.Click += new System.EventHandler(this.btnHostConnect_Click);
            // 
            // tbxDBAdminPword
            // 
            this.tbxDBAdminPword.BackColor = System.Drawing.Color.LightGray;
            this.tbxDBAdminPword.ForeColor = System.Drawing.Color.Black;
            this.tbxDBAdminPword.Location = new System.Drawing.Point(131, 64);
            this.tbxDBAdminPword.Margin = new System.Windows.Forms.Padding(2);
            this.tbxDBAdminPword.Name = "tbxDBAdminPword";
            this.tbxDBAdminPword.PasswordChar = '*';
            this.tbxDBAdminPword.Size = new System.Drawing.Size(162, 20);
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
            this.tbxDBAdminUser.Location = new System.Drawing.Point(131, 40);
            this.tbxDBAdminUser.Margin = new System.Windows.Forms.Padding(2);
            this.tbxDBAdminUser.Name = "tbxDBAdminUser";
            this.tbxDBAdminUser.Size = new System.Drawing.Size(162, 20);
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
            this.tbxHostName.Location = new System.Drawing.Point(131, 16);
            this.tbxHostName.Margin = new System.Windows.Forms.Padding(2);
            this.tbxHostName.Name = "tbxHostName";
            this.tbxHostName.Size = new System.Drawing.Size(162, 20);
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
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightGray;
            this.btnClear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClear.Location = new System.Drawing.Point(13, 344);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(77, 22);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnHostClear_Click);
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
            this.dgvHostDBList.Size = new System.Drawing.Size(306, 160);
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
            this.lblSelectDatabase.Size = new System.Drawing.Size(97, 13);
            this.lblSelectDatabase.TabIndex = 19;
            this.lblSelectDatabase.Text = "Select Database(s)";
            // 
            // webbrResults
            // 
            this.webbrResults.Location = new System.Drawing.Point(338, 27);
            this.webbrResults.MinimumSize = new System.Drawing.Size(20, 20);
            this.webbrResults.Name = "webbrResults";
            this.webbrResults.Size = new System.Drawing.Size(431, 339);
            this.webbrResults.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(335, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Results";
            // 
            // ckbxMethTest
            // 
            this.ckbxMethTest.AutoSize = true;
            this.ckbxMethTest.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.ckbxMethTest.Location = new System.Drawing.Point(118, 349);
            this.ckbxMethTest.Name = "ckbxMethTest";
            this.ckbxMethTest.Size = new System.Drawing.Size(91, 17);
            this.ckbxMethTest.TabIndex = 22;
            this.ckbxMethTest.Text = "Test Methods";
            this.ckbxMethTest.UseVisualStyleBackColor = true;
            // 
            // SetupMain
            // 
            this.AcceptButton = this.btnHostConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(788, 382);
            this.Controls.Add(this.ckbxMethTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webbrResults);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRunScan);
            this.Controls.Add(this.lblSelectDatabase);
            this.Controls.Add(this.dgvHostDBList);
            this.Controls.Add(this.grpbxDBHost);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "SetupMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DGP Lattice Database Setup";
            this.grpbxDBHost.ResumeLayout(false);
            this.grpbxDBHost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHostDBList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRunScan;
        private System.Windows.Forms.GroupBox grpbxDBHost;
        private System.Windows.Forms.Button btnHostConnect;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox tbxDBAdminPword;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbxDBAdminUser;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxHostName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnHelpFile;
        private System.Windows.Forms.DataGridView dgvHostDBList;
        private System.Windows.Forms.Label lblSelectDatabase;
        private System.Windows.Forms.WebBrowser webbrResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbxMethTest;
    }
}