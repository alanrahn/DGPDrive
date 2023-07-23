namespace DGPDrive.Screens.Connect
{
    partial class frmConnect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnect));
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbxFilePath = new System.Windows.Forms.TextBox();
            this.dgvSysList = new System.Windows.Forms.DataGridView();
            this.dgvLocList = new System.Windows.Forms.DataGridView();
            this.brwsResults = new System.Windows.Forms.WebBrowser();
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxEPURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvEPList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSelectSystem = new System.Windows.Forms.Label();
            this.lblSelectLocation = new System.Windows.Forms.Label();
            this.lblSelectEndpoint = new System.Windows.Forms.Label();
            this.lblSelectedURL = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblResults = new System.Windows.Forms.Label();
            this.lblConnect = new System.Windows.Forms.Label();
            this.ckbxAutoClose = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSysList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEPList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(270, 23);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(60, 26);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbxFilePath
            // 
            this.tbxFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFilePath.BackColor = System.Drawing.Color.Silver;
            this.tbxFilePath.ForeColor = System.Drawing.Color.Black;
            this.tbxFilePath.Location = new System.Drawing.Point(14, 28);
            this.tbxFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.tbxFilePath.Name = "tbxFilePath";
            this.tbxFilePath.Size = new System.Drawing.Size(244, 20);
            this.tbxFilePath.TabIndex = 3;
            // 
            // dgvSysList
            // 
            this.dgvSysList.AllowUserToAddRows = false;
            this.dgvSysList.AllowUserToDeleteRows = false;
            this.dgvSysList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvSysList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSysList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSysList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvSysList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSysList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSysList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSysList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSysList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvSysList.EnableHeadersVisualStyles = false;
            this.dgvSysList.GridColor = System.Drawing.Color.Black;
            this.dgvSysList.Location = new System.Drawing.Point(14, 76);
            this.dgvSysList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvSysList.MultiSelect = false;
            this.dgvSysList.Name = "dgvSysList";
            this.dgvSysList.ReadOnly = true;
            this.dgvSysList.RowHeadersWidth = 10;
            this.dgvSysList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSysList.ShowEditingIcon = false;
            this.dgvSysList.Size = new System.Drawing.Size(316, 38);
            this.dgvSysList.TabIndex = 4;
            this.dgvSysList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSysList_CellContentClick);
            // 
            // dgvLocList
            // 
            this.dgvLocList.AllowUserToAddRows = false;
            this.dgvLocList.AllowUserToDeleteRows = false;
            this.dgvLocList.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLocList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLocList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvLocList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLocList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvLocList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvLocList.EnableHeadersVisualStyles = false;
            this.dgvLocList.GridColor = System.Drawing.Color.Black;
            this.dgvLocList.Location = new System.Drawing.Point(14, 142);
            this.dgvLocList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvLocList.MultiSelect = false;
            this.dgvLocList.Name = "dgvLocList";
            this.dgvLocList.ReadOnly = true;
            this.dgvLocList.RowHeadersWidth = 10;
            this.dgvLocList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocList.ShowEditingIcon = false;
            this.dgvLocList.Size = new System.Drawing.Size(316, 52);
            this.dgvLocList.TabIndex = 4;
            this.dgvLocList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocList_CellContentClick);
            // 
            // brwsResults
            // 
            this.brwsResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brwsResults.Location = new System.Drawing.Point(0, 0);
            this.brwsResults.Margin = new System.Windows.Forms.Padding(2);
            this.brwsResults.MinimumSize = new System.Drawing.Size(10, 10);
            this.brwsResults.Name = "brwsResults";
            this.brwsResults.Size = new System.Drawing.Size(266, 110);
            this.brwsResults.TabIndex = 21;
            // 
            // tbxUserName
            // 
            this.tbxUserName.BackColor = System.Drawing.Color.Silver;
            this.tbxUserName.ForeColor = System.Drawing.Color.Black;
            this.tbxUserName.Location = new System.Drawing.Point(429, 83);
            this.tbxUserName.Margin = new System.Windows.Forms.Padding(2);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(193, 20);
            this.tbxUserName.TabIndex = 11;
            // 
            // tbxPassword
            // 
            this.tbxPassword.BackColor = System.Drawing.Color.Silver;
            this.tbxPassword.ForeColor = System.Drawing.Color.Black;
            this.tbxPassword.Location = new System.Drawing.Point(429, 111);
            this.tbxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(193, 20);
            this.tbxPassword.TabIndex = 13;
            this.tbxPassword.TextChanged += new System.EventHandler(this.tbxPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(352, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "UserName";
            // 
            // tbxEPURL
            // 
            this.tbxEPURL.BackColor = System.Drawing.Color.Silver;
            this.tbxEPURL.ForeColor = System.Drawing.Color.Black;
            this.tbxEPURL.Location = new System.Drawing.Point(354, 28);
            this.tbxEPURL.Margin = new System.Windows.Forms.Padding(2);
            this.tbxEPURL.Name = "tbxEPURL";
            this.tbxEPURL.Size = new System.Drawing.Size(268, 20);
            this.tbxEPURL.TabIndex = 10;
            this.tbxEPURL.TextChanged += new System.EventHandler(this.tbxLocURL_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(352, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Password";
            // 
            // btnConnect
            // 
            this.btnConnect.AutoSize = true;
            this.btnConnect.BackColor = System.Drawing.Color.Gainsboro;
            this.btnConnect.Enabled = false;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(562, 142);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(60, 26);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "Login";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(430, 142);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 26);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvEPList
            // 
            this.dgvEPList.AllowUserToAddRows = false;
            this.dgvEPList.AllowUserToDeleteRows = false;
            this.dgvEPList.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvEPList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvEPList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEPList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvEPList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEPList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvEPList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEPList.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvEPList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvEPList.EnableHeadersVisualStyles = false;
            this.dgvEPList.GridColor = System.Drawing.Color.Black;
            this.dgvEPList.Location = new System.Drawing.Point(14, 220);
            this.dgvEPList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvEPList.MultiSelect = false;
            this.dgvEPList.Name = "dgvEPList";
            this.dgvEPList.ReadOnly = true;
            this.dgvEPList.RowHeadersWidth = 10;
            this.dgvEPList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEPList.ShowEditingIcon = false;
            this.dgvEPList.Size = new System.Drawing.Size(316, 83);
            this.dgvEPList.TabIndex = 4;
            this.dgvEPList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNetList_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select a System List File";
            // 
            // lblSelectSystem
            // 
            this.lblSelectSystem.AutoSize = true;
            this.lblSelectSystem.Enabled = false;
            this.lblSelectSystem.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblSelectSystem.Location = new System.Drawing.Point(12, 60);
            this.lblSelectSystem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectSystem.Name = "lblSelectSystem";
            this.lblSelectSystem.Size = new System.Drawing.Size(83, 13);
            this.lblSelectSystem.TabIndex = 9;
            this.lblSelectSystem.Text = "Select a System";
            // 
            // lblSelectLocation
            // 
            this.lblSelectLocation.AutoSize = true;
            this.lblSelectLocation.Enabled = false;
            this.lblSelectLocation.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblSelectLocation.Location = new System.Drawing.Point(12, 126);
            this.lblSelectLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectLocation.Name = "lblSelectLocation";
            this.lblSelectLocation.Size = new System.Drawing.Size(90, 13);
            this.lblSelectLocation.TabIndex = 10;
            this.lblSelectLocation.Text = "Select a Location";
            // 
            // lblSelectEndpoint
            // 
            this.lblSelectEndpoint.AutoSize = true;
            this.lblSelectEndpoint.Enabled = false;
            this.lblSelectEndpoint.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblSelectEndpoint.Location = new System.Drawing.Point(12, 205);
            this.lblSelectEndpoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectEndpoint.Name = "lblSelectEndpoint";
            this.lblSelectEndpoint.Size = new System.Drawing.Size(71, 13);
            this.lblSelectEndpoint.TabIndex = 11;
            this.lblSelectEndpoint.Text = "Select a URL";
            // 
            // lblSelectedURL
            // 
            this.lblSelectedURL.AutoSize = true;
            this.lblSelectedURL.Enabled = false;
            this.lblSelectedURL.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblSelectedURL.Location = new System.Drawing.Point(352, 12);
            this.lblSelectedURL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectedURL.Name = "lblSelectedURL";
            this.lblSelectedURL.Size = new System.Drawing.Size(74, 13);
            this.lblSelectedURL.TabIndex = 12;
            this.lblSelectedURL.Text = "Selected URL";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.brwsResults);
            this.panel1.Location = new System.Drawing.Point(354, 192);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 112);
            this.panel1.TabIndex = 25;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Enabled = false;
            this.lblResults.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblResults.Location = new System.Drawing.Point(353, 176);
            this.lblResults.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(42, 13);
            this.lblResults.TabIndex = 26;
            this.lblResults.Text = "Results";
            // 
            // lblConnect
            // 
            this.lblConnect.AutoSize = true;
            this.lblConnect.Enabled = false;
            this.lblConnect.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblConnect.Location = new System.Drawing.Point(352, 60);
            this.lblConnect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(114, 13);
            this.lblConnect.TabIndex = 27;
            this.lblConnect.Text = "Connect to the System";
            // 
            // ckbxAutoClose
            // 
            this.ckbxAutoClose.AutoSize = true;
            this.ckbxAutoClose.Checked = true;
            this.ckbxAutoClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbxAutoClose.ForeColor = System.Drawing.Color.LightGray;
            this.ckbxAutoClose.Location = new System.Drawing.Point(544, 59);
            this.ckbxAutoClose.Margin = new System.Windows.Forms.Padding(2);
            this.ckbxAutoClose.Name = "ckbxAutoClose";
            this.ckbxAutoClose.Size = new System.Drawing.Size(74, 17);
            this.ckbxAutoClose.TabIndex = 128;
            this.ckbxAutoClose.Text = "Hide Form";
            this.ckbxAutoClose.UseVisualStyleBackColor = true;
            // 
            // frmConnect
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(636, 318);
            this.Controls.Add(this.ckbxAutoClose);
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblSelectedURL);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblSelectEndpoint);
            this.Controls.Add(this.dgvEPList);
            this.Controls.Add(this.lblSelectLocation);
            this.Controls.Add(this.dgvLocList);
            this.Controls.Add(this.lblSelectSystem);
            this.Controls.Add(this.tbxEPURL);
            this.Controls.Add(this.dgvSysList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbxFilePath);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to Lattice";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSysList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEPList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbxFilePath;
        private System.Windows.Forms.DataGridView dgvSysList;
        private System.Windows.Forms.DataGridView dgvLocList;
        private System.Windows.Forms.TextBox tbxUserName;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxEPURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.WebBrowser brwsResults;
        private System.Windows.Forms.DataGridView dgvEPList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSelectSystem;
        private System.Windows.Forms.Label lblSelectLocation;
        private System.Windows.Forms.Label lblSelectEndpoint;
        private System.Windows.Forms.Label lblSelectedURL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.CheckBox ckbxAutoClose;
    }
}