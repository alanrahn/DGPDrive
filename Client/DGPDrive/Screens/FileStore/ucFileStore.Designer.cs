namespace DGPDrive.Screens.FileStore
{
    partial class ucFileStore
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFileStore));
            this.DGPSplitContainer = new System.Windows.Forms.SplitContainer();
            this.DGPTabControl = new System.Windows.Forms.TabControl();
            this.browseTabPage = new System.Windows.Forms.TabPage();
            this.treeFolders = new System.Windows.Forms.TreeView();
            this.folderContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshSubfoldersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTabPage = new System.Windows.Forms.TabPage();
            this.grpbxFileInfo = new System.Windows.Forms.GroupBox();
            this.btnSearchFileInfo = new System.Windows.Forms.Button();
            this.btnClearFileInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.cmbFileExt = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFileState = new System.Windows.Forms.ComboBox();
            this.dtpFileDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.tagsTabPage = new System.Windows.Forms.TabPage();
            this.grpbxTags = new System.Windows.Forms.GroupBox();
            this.tbxSelectedTag = new System.Windows.Forms.TextBox();
            this.btnGetTags = new System.Windows.Forms.Button();
            this.tbxFilterTagName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearchTags = new System.Windows.Forms.Button();
            this.dgvTagList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFileList = new System.Windows.Forms.DataGridView();
            this.filesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileTagsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFavoritesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFavoriteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFileInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFileInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoverDeletedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsFileList = new System.Windows.Forms.ToolStrip();
            this.tsbtnLast = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNext = new System.Windows.Forms.ToolStripButton();
            this.tslblPages = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPrev = new System.Windows.Forms.ToolStripButton();
            this.tsbtnFirst = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tscmbOrder = new System.Windows.Forms.ToolStripComboBox();
            this.tscmbSortBy = new System.Windows.Forms.ToolStripComboBox();
            this.tslblFolderPath = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DGPSplitContainer)).BeginInit();
            this.DGPSplitContainer.Panel1.SuspendLayout();
            this.DGPSplitContainer.Panel2.SuspendLayout();
            this.DGPSplitContainer.SuspendLayout();
            this.DGPTabControl.SuspendLayout();
            this.browseTabPage.SuspendLayout();
            this.folderContextMenu.SuspendLayout();
            this.searchTabPage.SuspendLayout();
            this.grpbxFileInfo.SuspendLayout();
            this.tagsTabPage.SuspendLayout();
            this.grpbxTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTagList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileList)).BeginInit();
            this.filesContextMenu.SuspendLayout();
            this.tsFileList.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGPSplitContainer
            // 
            this.DGPSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGPSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.DGPSplitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.DGPSplitContainer.Name = "DGPSplitContainer";
            // 
            // DGPSplitContainer.Panel1
            // 
            this.DGPSplitContainer.Panel1.Controls.Add(this.DGPTabControl);
            this.DGPSplitContainer.Panel1MinSize = 300;
            // 
            // DGPSplitContainer.Panel2
            // 
            this.DGPSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.DGPSplitContainer.Panel2MinSize = 500;
            this.DGPSplitContainer.Size = new System.Drawing.Size(2200, 1300);
            this.DGPSplitContainer.SplitterDistance = 600;
            this.DGPSplitContainer.TabIndex = 0;
            // 
            // DGPTabControl
            // 
            this.DGPTabControl.Controls.Add(this.browseTabPage);
            this.DGPTabControl.Controls.Add(this.searchTabPage);
            this.DGPTabControl.Controls.Add(this.tagsTabPage);
            this.DGPTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGPTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGPTabControl.Location = new System.Drawing.Point(0, 0);
            this.DGPTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.DGPTabControl.Name = "DGPTabControl";
            this.DGPTabControl.Padding = new System.Drawing.Point(6, 6);
            this.DGPTabControl.SelectedIndex = 0;
            this.DGPTabControl.Size = new System.Drawing.Size(600, 1300);
            this.DGPTabControl.TabIndex = 0;
            this.DGPTabControl.SelectedIndexChanged += new System.EventHandler(this.DGPTabControl_SelectedIndexChanged);
            // 
            // browseTabPage
            // 
            this.browseTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.browseTabPage.Controls.Add(this.treeFolders);
            this.browseTabPage.ForeColor = System.Drawing.Color.Black;
            this.browseTabPage.Location = new System.Drawing.Point(8, 47);
            this.browseTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.browseTabPage.Name = "browseTabPage";
            this.browseTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.browseTabPage.Size = new System.Drawing.Size(584, 1245);
            this.browseTabPage.TabIndex = 0;
            this.browseTabPage.Text = "  Folders  ";
            // 
            // treeFolders
            // 
            this.treeFolders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeFolders.ContextMenuStrip = this.folderContextMenu;
            this.treeFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFolders.ForeColor = System.Drawing.Color.LightGray;
            this.treeFolders.FullRowSelect = true;
            this.treeFolders.Indent = 24;
            this.treeFolders.ItemHeight = 24;
            this.treeFolders.LineColor = System.Drawing.Color.LightGray;
            this.treeFolders.Location = new System.Drawing.Point(4, 4);
            this.treeFolders.Margin = new System.Windows.Forms.Padding(0);
            this.treeFolders.MinimumSize = new System.Drawing.Size(514, 836);
            this.treeFolders.Name = "treeFolders";
            this.treeFolders.ShowNodeToolTips = true;
            this.treeFolders.Size = new System.Drawing.Size(576, 1237);
            this.treeFolders.TabIndex = 0;
            this.treeFolders.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFolders_NodeMouseClick);
            this.treeFolders.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFolders_NodeMouseClick);
            this.treeFolders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeFolders_MouseDown);
            // 
            // folderContextMenu
            // 
            this.folderContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.folderContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewFileMenuItem,
            this.subFolderMenuItem,
            this.refreshSubfoldersMenuItem,
            this.editFolderMenuItem});
            this.folderContextMenu.Name = "folderContextMenu";
            this.folderContextMenu.Size = new System.Drawing.Size(315, 156);
            // 
            // addNewFileMenuItem
            // 
            this.addNewFileMenuItem.Name = "addNewFileMenuItem";
            this.addNewFileMenuItem.Size = new System.Drawing.Size(314, 38);
            this.addNewFileMenuItem.Text = "Upload New File...";
            this.addNewFileMenuItem.Click += new System.EventHandler(this.addNewFileMenuItem_Click);
            // 
            // subFolderMenuItem
            // 
            this.subFolderMenuItem.Name = "subFolderMenuItem";
            this.subFolderMenuItem.Size = new System.Drawing.Size(314, 38);
            this.subFolderMenuItem.Text = "Add New Subfolder...";
            this.subFolderMenuItem.Click += new System.EventHandler(this.subFolderMenuItem_Click);
            // 
            // refreshSubfoldersMenuItem
            // 
            this.refreshSubfoldersMenuItem.Name = "refreshSubfoldersMenuItem";
            this.refreshSubfoldersMenuItem.Size = new System.Drawing.Size(314, 38);
            this.refreshSubfoldersMenuItem.Text = "Refresh Subfolders";
            this.refreshSubfoldersMenuItem.Click += new System.EventHandler(this.refreshSubfoldersMenuItem_Click);
            // 
            // editFolderMenuItem
            // 
            this.editFolderMenuItem.Name = "editFolderMenuItem";
            this.editFolderMenuItem.Size = new System.Drawing.Size(314, 38);
            this.editFolderMenuItem.Text = "Edit Folder...";
            this.editFolderMenuItem.Click += new System.EventHandler(this.editFolderMenuItem_Click);
            // 
            // searchTabPage
            // 
            this.searchTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.searchTabPage.Controls.Add(this.grpbxFileInfo);
            this.searchTabPage.ForeColor = System.Drawing.Color.Black;
            this.searchTabPage.Location = new System.Drawing.Point(8, 47);
            this.searchTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.searchTabPage.Name = "searchTabPage";
            this.searchTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.searchTabPage.Size = new System.Drawing.Size(584, 1245);
            this.searchTabPage.TabIndex = 1;
            this.searchTabPage.Text = "  Metadata  ";
            // 
            // grpbxFileInfo
            // 
            this.grpbxFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbxFileInfo.Controls.Add(this.btnSearchFileInfo);
            this.grpbxFileInfo.Controls.Add(this.btnClearFileInfo);
            this.grpbxFileInfo.Controls.Add(this.label1);
            this.grpbxFileInfo.Controls.Add(this.label2);
            this.grpbxFileInfo.Controls.Add(this.tbxFileName);
            this.grpbxFileInfo.Controls.Add(this.cmbFileExt);
            this.grpbxFileInfo.Controls.Add(this.label4);
            this.grpbxFileInfo.Controls.Add(this.cmbFileState);
            this.grpbxFileInfo.Controls.Add(this.dtpFileDate);
            this.grpbxFileInfo.Controls.Add(this.label3);
            this.grpbxFileInfo.ForeColor = System.Drawing.Color.LightGray;
            this.grpbxFileInfo.Location = new System.Drawing.Point(24, 8);
            this.grpbxFileInfo.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxFileInfo.Name = "grpbxFileInfo";
            this.grpbxFileInfo.Padding = new System.Windows.Forms.Padding(4);
            this.grpbxFileInfo.Size = new System.Drawing.Size(528, 340);
            this.grpbxFileInfo.TabIndex = 32;
            this.grpbxFileInfo.TabStop = false;
            this.grpbxFileInfo.Text = "Search by File Metadata";
            // 
            // btnSearchFileInfo
            // 
            this.btnSearchFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchFileInfo.AutoSize = true;
            this.btnSearchFileInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSearchFileInfo.ForeColor = System.Drawing.Color.Black;
            this.btnSearchFileInfo.Location = new System.Drawing.Point(352, 254);
            this.btnSearchFileInfo.Margin = new System.Windows.Forms.Padding(6);
            this.btnSearchFileInfo.Name = "btnSearchFileInfo";
            this.btnSearchFileInfo.Size = new System.Drawing.Size(142, 39);
            this.btnSearchFileInfo.TabIndex = 6;
            this.btnSearchFileInfo.Text = "Search";
            this.btnSearchFileInfo.UseVisualStyleBackColor = false;
            this.btnSearchFileInfo.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClearFileInfo
            // 
            this.btnClearFileInfo.AutoSize = true;
            this.btnClearFileInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClearFileInfo.ForeColor = System.Drawing.Color.Black;
            this.btnClearFileInfo.Location = new System.Drawing.Point(120, 254);
            this.btnClearFileInfo.Margin = new System.Windows.Forms.Padding(6);
            this.btnClearFileInfo.Name = "btnClearFileInfo";
            this.btnClearFileInfo.Size = new System.Drawing.Size(112, 39);
            this.btnClearFileInfo.TabIndex = 5;
            this.btnClearFileInfo.Text = "Clear";
            this.btnClearFileInfo.UseVisualStyleBackColor = false;
            this.btnClearFileInfo.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(24, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 29);
            this.label1.TabIndex = 11;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(24, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ext.";
            // 
            // tbxFileName
            // 
            this.tbxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFileName.BackColor = System.Drawing.Color.Silver;
            this.tbxFileName.Location = new System.Drawing.Point(120, 44);
            this.tbxFileName.Margin = new System.Windows.Forms.Padding(4);
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(374, 35);
            this.tbxFileName.TabIndex = 1;
            // 
            // cmbFileExt
            // 
            this.cmbFileExt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFileExt.BackColor = System.Drawing.Color.Silver;
            this.cmbFileExt.FormattingEnabled = true;
            this.cmbFileExt.Location = new System.Drawing.Point(120, 94);
            this.cmbFileExt.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFileExt.Name = "cmbFileExt";
            this.cmbFileExt.Size = new System.Drawing.Size(374, 37);
            this.cmbFileExt.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(24, 154);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 29);
            this.label4.TabIndex = 19;
            this.label4.Text = "Date";
            // 
            // cmbFileState
            // 
            this.cmbFileState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFileState.BackColor = System.Drawing.Color.Silver;
            this.cmbFileState.FormattingEnabled = true;
            this.cmbFileState.ItemHeight = 29;
            this.cmbFileState.Items.AddRange(new object[] {
            "ACTIVE",
            "DELETED"});
            this.cmbFileState.Location = new System.Drawing.Point(120, 198);
            this.cmbFileState.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFileState.Name = "cmbFileState";
            this.cmbFileState.Size = new System.Drawing.Size(374, 37);
            this.cmbFileState.TabIndex = 15;
            this.cmbFileState.SelectedIndexChanged += new System.EventHandler(this.cmbFileState_SelectedIndexChanged);
            // 
            // dtpFileDate
            // 
            this.dtpFileDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFileDate.CalendarMonthBackground = System.Drawing.Color.Silver;
            this.dtpFileDate.Checked = false;
            this.dtpFileDate.Location = new System.Drawing.Point(120, 148);
            this.dtpFileDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFileDate.Name = "dtpFileDate";
            this.dtpFileDate.ShowCheckBox = true;
            this.dtpFileDate.Size = new System.Drawing.Size(374, 35);
            this.dtpFileDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(24, 204);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "State";
            // 
            // tagsTabPage
            // 
            this.tagsTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tagsTabPage.Controls.Add(this.grpbxTags);
            this.tagsTabPage.Location = new System.Drawing.Point(8, 47);
            this.tagsTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.tagsTabPage.Name = "tagsTabPage";
            this.tagsTabPage.Size = new System.Drawing.Size(584, 1245);
            this.tagsTabPage.TabIndex = 2;
            this.tagsTabPage.Text = "  Tags  ";
            // 
            // grpbxTags
            // 
            this.grpbxTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbxTags.Controls.Add(this.tbxSelectedTag);
            this.grpbxTags.Controls.Add(this.btnGetTags);
            this.grpbxTags.Controls.Add(this.tbxFilterTagName);
            this.grpbxTags.Controls.Add(this.label6);
            this.grpbxTags.Controls.Add(this.label5);
            this.grpbxTags.Controls.Add(this.btnSearchTags);
            this.grpbxTags.Controls.Add(this.dgvTagList);
            this.grpbxTags.ForeColor = System.Drawing.Color.LightGray;
            this.grpbxTags.Location = new System.Drawing.Point(24, 8);
            this.grpbxTags.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxTags.Name = "grpbxTags";
            this.grpbxTags.Padding = new System.Windows.Forms.Padding(4);
            this.grpbxTags.Size = new System.Drawing.Size(528, 1156);
            this.grpbxTags.TabIndex = 34;
            this.grpbxTags.TabStop = false;
            this.grpbxTags.Text = "Search by File Tags";
            // 
            // tbxSelectedTag
            // 
            this.tbxSelectedTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSelectedTag.BackColor = System.Drawing.Color.Silver;
            this.tbxSelectedTag.Location = new System.Drawing.Point(28, 72);
            this.tbxSelectedTag.Margin = new System.Windows.Forms.Padding(4);
            this.tbxSelectedTag.Name = "tbxSelectedTag";
            this.tbxSelectedTag.Size = new System.Drawing.Size(300, 35);
            this.tbxSelectedTag.TabIndex = 42;
            // 
            // btnGetTags
            // 
            this.btnGetTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetTags.AutoSize = true;
            this.btnGetTags.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGetTags.ForeColor = System.Drawing.Color.Black;
            this.btnGetTags.Location = new System.Drawing.Point(330, 1079);
            this.btnGetTags.Margin = new System.Windows.Forms.Padding(6);
            this.btnGetTags.Name = "btnGetTags";
            this.btnGetTags.Size = new System.Drawing.Size(164, 39);
            this.btnGetTags.TabIndex = 41;
            this.btnGetTags.Text = "Filter List";
            this.btnGetTags.UseVisualStyleBackColor = false;
            this.btnGetTags.Click += new System.EventHandler(this.btnGetTags_Click);
            // 
            // tbxFilterTagName
            // 
            this.tbxFilterTagName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFilterTagName.BackColor = System.Drawing.Color.Silver;
            this.tbxFilterTagName.Location = new System.Drawing.Point(28, 1080);
            this.tbxFilterTagName.Margin = new System.Windows.Forms.Padding(4);
            this.tbxFilterTagName.Name = "tbxFilterTagName";
            this.tbxFilterTagName.Size = new System.Drawing.Size(276, 35);
            this.tbxFilterTagName.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.LightGray;
            this.label6.Location = new System.Drawing.Point(24, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 29);
            this.label6.TabIndex = 39;
            this.label6.Text = "Tag List";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(24, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 29);
            this.label5.TabIndex = 38;
            this.label5.Text = "Selected Tag";
            // 
            // btnSearchTags
            // 
            this.btnSearchTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchTags.AutoSize = true;
            this.btnSearchTags.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSearchTags.ForeColor = System.Drawing.Color.Black;
            this.btnSearchTags.Location = new System.Drawing.Point(364, 72);
            this.btnSearchTags.Margin = new System.Windows.Forms.Padding(6);
            this.btnSearchTags.Name = "btnSearchTags";
            this.btnSearchTags.Size = new System.Drawing.Size(130, 39);
            this.btnSearchTags.TabIndex = 20;
            this.btnSearchTags.Text = "Search";
            this.btnSearchTags.UseVisualStyleBackColor = false;
            this.btnSearchTags.Click += new System.EventHandler(this.btnSearchTags_Click);
            // 
            // dgvTagList
            // 
            this.dgvTagList.AllowUserToAddRows = false;
            this.dgvTagList.AllowUserToDeleteRows = false;
            this.dgvTagList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTagList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTagList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTagList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvTagList.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTagList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTagList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTagList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTagList.EnableHeadersVisualStyles = false;
            this.dgvTagList.GridColor = System.Drawing.Color.Black;
            this.dgvTagList.Location = new System.Drawing.Point(28, 164);
            this.dgvTagList.Margin = new System.Windows.Forms.Padding(6);
            this.dgvTagList.MultiSelect = false;
            this.dgvTagList.Name = "dgvTagList";
            this.dgvTagList.ReadOnly = true;
            this.dgvTagList.RowHeadersWidth = 10;
            this.dgvTagList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTagList.ShowEditingIcon = false;
            this.dgvTagList.Size = new System.Drawing.Size(470, 900);
            this.dgvTagList.TabIndex = 31;
            this.dgvTagList.TabStop = false;
            this.dgvTagList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTagList_CellDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvFileList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsFileList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1596, 1300);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // dgvFileList
            // 
            this.dgvFileList.AllowUserToAddRows = false;
            this.dgvFileList.AllowUserToDeleteRows = false;
            this.dgvFileList.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvFileList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFileList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFileList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvFileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileList.ContextMenuStrip = this.filesContextMenu;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFileList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFileList.EnableHeadersVisualStyles = false;
            this.dgvFileList.GridColor = System.Drawing.Color.Black;
            this.dgvFileList.Location = new System.Drawing.Point(0, 60);
            this.dgvFileList.Margin = new System.Windows.Forms.Padding(0);
            this.dgvFileList.MultiSelect = false;
            this.dgvFileList.Name = "dgvFileList";
            this.dgvFileList.ReadOnly = true;
            this.dgvFileList.RowHeadersWidth = 10;
            this.dgvFileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFileList.ShowEditingIcon = false;
            this.dgvFileList.Size = new System.Drawing.Size(1596, 1240);
            this.dgvFileList.TabIndex = 5;
            this.dgvFileList.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFileList_CellMouseUp);
            // 
            // filesContextMenu
            // 
            this.filesContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.filesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadFileMenuItem,
            this.fileTagsMenuItem,
            this.addFavoritesMenuItem,
            this.removeFavoriteMenuItem,
            this.uploadFileMenuItem,
            this.editFileInfoMenuItem,
            this.copyFileInfoMenuItem,
            this.historyMenuItem,
            this.recoverDeletedMenuItem});
            this.filesContextMenu.Name = "filesContextMenu";
            this.filesContextMenu.Size = new System.Drawing.Size(340, 346);
            // 
            // downloadFileMenuItem
            // 
            this.downloadFileMenuItem.Name = "downloadFileMenuItem";
            this.downloadFileMenuItem.Size = new System.Drawing.Size(339, 38);
            this.downloadFileMenuItem.Text = "Download File...";
            this.downloadFileMenuItem.Click += new System.EventHandler(this.downloadMenuItem_Click);
            // 
            // fileTagsMenuItem
            // 
            this.fileTagsMenuItem.Name = "fileTagsMenuItem";
            this.fileTagsMenuItem.Size = new System.Drawing.Size(339, 38);
            this.fileTagsMenuItem.Text = "File Tags...";
            this.fileTagsMenuItem.Click += new System.EventHandler(this.fileTagsMenuItem_Click);
            // 
            // addFavoritesMenuItem
            // 
            this.addFavoritesMenuItem.Name = "addFavoritesMenuItem";
            this.addFavoritesMenuItem.Size = new System.Drawing.Size(339, 38);
            this.addFavoritesMenuItem.Text = "Add To Favorites";
            this.addFavoritesMenuItem.Click += new System.EventHandler(this.addFavoritesMenuItem_Click);
            // 
            // removeFavoriteMenuItem
            // 
            this.removeFavoriteMenuItem.Name = "removeFavoriteMenuItem";
            this.removeFavoriteMenuItem.Size = new System.Drawing.Size(339, 38);
            this.removeFavoriteMenuItem.Text = "Remove From Favorites";
            this.removeFavoriteMenuItem.Click += new System.EventHandler(this.removeFavoriteMenuItem_Click);
            // 
            // uploadFileMenuItem
            // 
            this.uploadFileMenuItem.Name = "uploadFileMenuItem";
            this.uploadFileMenuItem.Size = new System.Drawing.Size(339, 38);
            this.uploadFileMenuItem.Text = "Upload New Version...";
            this.uploadFileMenuItem.Click += new System.EventHandler(this.uploadMenuItem_Click);
            // 
            // editFileInfoMenuItem
            // 
            this.editFileInfoMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.editFileInfoMenuItem.Name = "editFileInfoMenuItem";
            this.editFileInfoMenuItem.Size = new System.Drawing.Size(339, 38);
            this.editFileInfoMenuItem.Text = "Edit File Info...";
            this.editFileInfoMenuItem.Click += new System.EventHandler(this.editFileInfoMenuItem_Click);
            // 
            // copyFileInfoMenuItem
            // 
            this.copyFileInfoMenuItem.Name = "copyFileInfoMenuItem";
            this.copyFileInfoMenuItem.Size = new System.Drawing.Size(339, 38);
            this.copyFileInfoMenuItem.Text = "Copy File Info";
            this.copyFileInfoMenuItem.Visible = false;
            this.copyFileInfoMenuItem.Click += new System.EventHandler(this.copyFileInfoMenuItem_Click);
            // 
            // historyMenuItem
            // 
            this.historyMenuItem.Name = "historyMenuItem";
            this.historyMenuItem.Size = new System.Drawing.Size(339, 38);
            this.historyMenuItem.Text = "File History...";
            this.historyMenuItem.Click += new System.EventHandler(this.historyMenuItem_Click);
            // 
            // recoverDeletedMenuItem
            // 
            this.recoverDeletedMenuItem.Name = "recoverDeletedMenuItem";
            this.recoverDeletedMenuItem.Size = new System.Drawing.Size(339, 38);
            this.recoverDeletedMenuItem.Text = "Recover Deleted File";
            this.recoverDeletedMenuItem.Click += new System.EventHandler(this.recoverDeletedMenuItem_Click);
            // 
            // tsFileList
            // 
            this.tsFileList.AllowMerge = false;
            this.tsFileList.AutoSize = false;
            this.tsFileList.BackColor = System.Drawing.Color.DarkGray;
            this.tsFileList.CanOverflow = false;
            this.tsFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsFileList.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsFileList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFileList.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsFileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnLast,
            this.tsbtnNext,
            this.tslblPages,
            this.tsbtnPrev,
            this.tsbtnFirst,
            this.tsbtnRefresh,
            this.tscmbOrder,
            this.tscmbSortBy,
            this.tslblFolderPath});
            this.tsFileList.Location = new System.Drawing.Point(2, 2);
            this.tsFileList.Margin = new System.Windows.Forms.Padding(2);
            this.tsFileList.Name = "tsFileList";
            this.tsFileList.Padding = new System.Windows.Forms.Padding(2);
            this.tsFileList.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsFileList.Size = new System.Drawing.Size(1592, 56);
            this.tsFileList.Stretch = true;
            this.tsFileList.TabIndex = 6;
            this.tsFileList.Text = "toolStrip1";
            this.tsFileList.Resize += new System.EventHandler(this.tsFileList_Resize);
            // 
            // tsbtnLast
            // 
            this.tsbtnLast.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnLast.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnLast.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLast.Image")));
            this.tsbtnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLast.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnLast.Name = "tsbtnLast";
            this.tsbtnLast.Size = new System.Drawing.Size(46, 42);
            this.tsbtnLast.Text = ">|";
            this.tsbtnLast.ToolTipText = "Last page";
            this.tsbtnLast.Click += new System.EventHandler(this.tsbtnLast_Click);
            // 
            // tsbtnNext
            // 
            this.tsbtnNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNext.Image")));
            this.tsbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNext.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnNext.Name = "tsbtnNext";
            this.tsbtnNext.Size = new System.Drawing.Size(46, 42);
            this.tsbtnNext.Text = ">";
            this.tsbtnNext.ToolTipText = "Next page";
            this.tsbtnNext.Click += new System.EventHandler(this.tsbtnNext_Click);
            // 
            // tslblPages
            // 
            this.tslblPages.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslblPages.Margin = new System.Windows.Forms.Padding(2);
            this.tslblPages.Name = "tslblPages";
            this.tslblPages.Size = new System.Drawing.Size(135, 48);
            this.tslblPages.Text = "Page 0 of 0";
            // 
            // tsbtnPrev
            // 
            this.tsbtnPrev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnPrev.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrev.Image")));
            this.tsbtnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrev.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnPrev.Name = "tsbtnPrev";
            this.tsbtnPrev.Size = new System.Drawing.Size(46, 42);
            this.tsbtnPrev.Text = "<";
            this.tsbtnPrev.ToolTipText = "Previous page";
            this.tsbtnPrev.Click += new System.EventHandler(this.tsbtnPrev_Click);
            // 
            // tsbtnFirst
            // 
            this.tsbtnFirst.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnFirst.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnFirst.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFirst.Image")));
            this.tsbtnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFirst.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tsbtnFirst.Name = "tsbtnFirst";
            this.tsbtnFirst.Size = new System.Drawing.Size(46, 42);
            this.tsbtnFirst.Tag = "";
            this.tsbtnFirst.Text = "|<";
            this.tsbtnFirst.ToolTipText = "First page";
            this.tsbtnFirst.Click += new System.EventHandler(this.tsbtnFirst_Click);
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnRefresh.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnRefresh.ForeColor = System.Drawing.Color.Black;
            this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Margin = new System.Windows.Forms.Padding(20, 5, 20, 5);
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(98, 42);
            this.tsbtnRefresh.Text = "Refresh";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // tscmbOrder
            // 
            this.tscmbOrder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tscmbOrder.BackColor = System.Drawing.Color.Silver;
            this.tscmbOrder.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscmbOrder.Items.AddRange(new object[] {
            "ASC",
            "DESC"});
            this.tscmbOrder.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbOrder.Name = "tscmbOrder";
            this.tscmbOrder.Size = new System.Drawing.Size(146, 48);
            // 
            // tscmbSortBy
            // 
            this.tscmbSortBy.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tscmbSortBy.BackColor = System.Drawing.Color.Silver;
            this.tscmbSortBy.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscmbSortBy.Items.AddRange(new object[] {
            "Name",
            "Type",
            "Date"});
            this.tscmbSortBy.Margin = new System.Windows.Forms.Padding(2);
            this.tscmbSortBy.Name = "tscmbSortBy";
            this.tscmbSortBy.Size = new System.Drawing.Size(150, 48);
            this.tscmbSortBy.ToolTipText = "Sort By";
            // 
            // tslblFolderPath
            // 
            this.tslblFolderPath.Name = "tslblFolderPath";
            this.tslblFolderPath.Size = new System.Drawing.Size(0, 46);
            // 
            // ucFileStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.DGPSplitContainer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucFileStore";
            this.Size = new System.Drawing.Size(2200, 1300);
            this.DGPSplitContainer.Panel1.ResumeLayout(false);
            this.DGPSplitContainer.Panel2.ResumeLayout(false);
            this.DGPSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGPSplitContainer)).EndInit();
            this.DGPSplitContainer.ResumeLayout(false);
            this.DGPTabControl.ResumeLayout(false);
            this.browseTabPage.ResumeLayout(false);
            this.folderContextMenu.ResumeLayout(false);
            this.searchTabPage.ResumeLayout(false);
            this.grpbxFileInfo.ResumeLayout(false);
            this.grpbxFileInfo.PerformLayout();
            this.tagsTabPage.ResumeLayout(false);
            this.grpbxTags.ResumeLayout(false);
            this.grpbxTags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTagList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileList)).EndInit();
            this.filesContextMenu.ResumeLayout(false);
            this.tsFileList.ResumeLayout(false);
            this.tsFileList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer DGPSplitContainer;
        private System.Windows.Forms.TabControl DGPTabControl;
        private System.Windows.Forms.TabPage browseTabPage;
        private System.Windows.Forms.TreeView treeFolders;
        private System.Windows.Forms.TabPage searchTabPage;
        private System.Windows.Forms.DataGridView dgvFileList;
        private System.Windows.Forms.ToolStrip tsFileList;
        private System.Windows.Forms.ToolStripButton tsbtnLast;
        private System.Windows.Forms.ToolStripButton tsbtnNext;
        private System.Windows.Forms.ToolStripLabel tslblPages;
        private System.Windows.Forms.ToolStripButton tsbtnPrev;
        private System.Windows.Forms.ToolStripButton tsbtnFirst;
        private System.Windows.Forms.ContextMenuStrip folderContextMenu;
        private System.Windows.Forms.ContextMenuStrip filesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem downloadFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recoverDeletedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshSubfoldersMenuItem;
        private System.Windows.Forms.ToolStripComboBox tscmbSortBy;
        private System.Windows.Forms.ToolStripComboBox tscmbOrder;
        private System.Windows.Forms.Button btnSearchFileInfo;
        private System.Windows.Forms.Button btnClearFileInfo;
        private System.Windows.Forms.ComboBox cmbFileState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFileExt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.ToolStripMenuItem editFileInfoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFileInfoMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFileDate;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ToolStripMenuItem removeFavoriteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFavoritesMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem fileTagsMenuItem;
        private System.Windows.Forms.GroupBox grpbxFileInfo;
        private System.Windows.Forms.ToolStripLabel tslblFolderPath;
        private System.Windows.Forms.TabPage tagsTabPage;
        private System.Windows.Forms.GroupBox grpbxTags;
        private System.Windows.Forms.TextBox tbxSelectedTag;
        private System.Windows.Forms.Button btnGetTags;
        private System.Windows.Forms.TextBox tbxFilterTagName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearchTags;
        private System.Windows.Forms.DataGridView dgvTagList;
    }
}
