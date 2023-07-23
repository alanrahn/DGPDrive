using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;
using System.Data;
using System.Diagnostics;

using ApiUtil;
using ApiUtil.DataClasses;
using System.Drawing;

namespace DGPDrive.Screens.FileStore
{
    public partial class ucFileStore : UserControl
    {
        MainForm _main;
        TreeNode _selectedNode;
        DGPNode _selNodeInfo;

        public string SearchMode { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public decimal PageSize { get; set; }
        public string SearchState { get; set; }

        //public string GID { get; set; }
        public decimal TotalRows { get; set; }
        public decimal TotalPages { get; set; }
        public int PageNum { get; set; }

        public string UploadDir { get; set; }
        public string DownloadDir { get; set; }

        public DataTable ReadGroups { get; set; }
        public DataTable WriteGroups { get; set; }

        // values from selected record of file data grid
        string _shardname;
        string _foldergid;
        string _filegid;
        string _filename;
        string _filedescrip;
        string _fileext;
        int _filesize;
        string _row_ms;
        DateTime _filedate;
        string _filehash;
        string _fileversion;
        string _taggid;
        string _folderpath;
        int _segsize;
        int _totalseg;
        bool _datagroups;

        public ucFileStore(MainForm main)
        {
            InitializeComponent();

            try
            {
                _main = main;
                _main.SetMetrics(0, 0, 0, "", "");

                UploadDir = Environment.CurrentDirectory;
                DownloadDir = Environment.CurrentDirectory;

                // pagination info used for file list gridview
                SetPageSize();
                PageNum = 0;
                SetPageLabel(0, 0);

                ImageList folderList = new ImageList();
                folderList.ColorDepth = ColorDepth.Depth16Bit;
                folderList.Images.Add(Properties.Resources.folder_grey, Color.White);
                folderList.Images.Add(Properties.Resources.folder_yellow, Color.White);
                folderList.Images.Add(Properties.Resources.folder_brown, Color.White);
                folderList.ImageSize = new Size(16, 16);

                treeFolders.ImageList = folderList;
                treeFolders.ImageIndex = 0;
                treeFolders.SelectedImageIndex = 0;

                // adjust folder padding for high DPI screens
                Rectangle r = Screen.GetBounds(_main);
                if (r.Width > 1920)
                {
                    treeFolders.ItemHeight = 32;
                    folderList.ImageSize = new Size(24, 24);
                }
                else
                {
                    treeFolders.ItemHeight = 20;
                    folderList.ImageSize = new Size(16, 16);
                }

                tscmbSortBy.SelectedIndex = 0;
                SortBy = tscmbSortBy.Text;
                tscmbOrder.SelectedIndex = 0;
                SortOrder = tscmbOrder.Text;

                _datagroups = false;
                PopulateGroups();

                InitializeTreeview();
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore", ex);
                MessageBox.Show(ex.Message, "ucFileStore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetPageSize()
        {
            try
            {
                // lookup method data using global id value
                Dictionary<string, string> methparams = new Dictionary<string, string>();

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("File.GetPageSize.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("File.GetPageSize.base_DEFAULT", methresults);
                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        PageSize = Convert.ToDecimal(methresult.RVal);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.SetPageSize", "", "");
                        MessageBox.Show("Error querying for the ucFileStore page size", "ucFileStore.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.SetPageSize", ex);
                MessageBox.Show(ex.Message, "ucFileStore.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateGroups()
        {
            try
            {
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add("SchemaFlag", "true");

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("UserSelf.GetUserGroupList.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("UserSelf.GetUserGroupList.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable grptable = cmnUtil.XmlToTable(methresult.RVal);
                        if (grptable.Rows.Count > 0)
                        {
                            ReadGroups = grptable;
                            WriteGroups = grptable.Clone();
                        }

                        // remove read-only groups from writegroups table
                        if (grptable.Rows.Count > 0)
                        {
                            foreach (DataRow group in grptable.Rows)
                            {
                                if (group["AccessLevel"].ToString() == "READWRITE")
                                {
                                    WriteGroups.ImportRow(group);
                                }
                            }
                        }
                    }

                    _datagroups = true;
                }
                else
                {
                    // error getting user group info: display error message
                    MessageBox.Show("A problem occurred in the query for the user account DataGroup membership.", "DataGroup Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.PopulateGroups", ex);
                MessageBox.Show(ex.Message, "ucFileStore.PopulateGroups", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeTreeview()
        {
            try
            {
                if (_datagroups)
                {
                    // add home node
                    treeFolders.BeginUpdate();
                    treeFolders.Nodes.Clear();

                    DGPNode favnode = new DGPNode();
                    favnode.FolderName = "Favorites";
                    favnode.DisplayOrder = "0";
                    favnode.ParentGID = "0";
                    favnode.FolderGID = "10000000";
                    favnode.GroupGID = "0";
                    favnode.AccessLevel = AccessLevel.ReadOnly;

                    TreeNode DGPFav = new TreeNode(favnode.FolderName);
                    DGPFav.ImageIndex = 2;
                    DGPFav.SelectedImageIndex = 2;
                    DGPFav.Tag = favnode;
                    treeFolders.Nodes.Add(DGPFav);

                    DGPNode homenode = new DGPNode();
                    homenode.FolderName = "Home";
                    homenode.DisplayOrder = "1";
                    homenode.ParentGID = "0";
                    homenode.FolderGID = "10000001";
                    homenode.GroupGID = "0";
                    homenode.AccessLevel = AccessLevel.ReadWrite;

                    TreeNode DGPHome = new TreeNode(homenode.FolderName);
                    DGPHome.ImageIndex = 1;
                    DGPHome.SelectedImageIndex = 1;
                    DGPHome.Tag = homenode;
                    treeFolders.Nodes.Add(DGPHome);

                    // select home node in tree and then query for top-level sub folders
                    treeFolders.SelectedNode = DGPHome;
                    tslblFolderPath.Text = DGPHome.FullPath;
                    GetSubFolders();

                    treeFolders.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.InitializeTreeview", ex);
                MessageBox.Show(ex.Message, "ucFileStore.InitializeTreeview", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// this method expands a parent folder to show any sub folders
        /// (and queries for sub folders if it is the first time it has been expanded)
        /// </summary>
        public void GetSubFolders()
        {
            try
            {
                if (_datagroups)
                {
                    _selectedNode = treeFolders.SelectedNode;
                    _selNodeInfo = (DGPNode)_selectedNode.Tag;

                    if (treeFolders.SelectedNode.Text != "Favorites")
                    {
                        // query for sub folders
                        Dictionary<string, string> methparams = new Dictionary<string, string>();
                        methparams.Add("FolderGID", _selNodeInfo.FolderGID);
                        methparams.Add("SchemaFlag", BoolVal.TRUE);

                        MsgUtil msgUtil = new MsgUtil();
                        string reqID = msgUtil.GetNewGID();
                        string methList = msgUtil.CreateXMLMethod("Folder.GetUserSubFolders.base", methparams);
                        SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                        string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                        if (respmsg != "")
                        {
                            RespInfo respinfo = new RespInfo();
                            Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                            ResInfo methresult = msgUtil.GetResult("Folder.GetUserSubFolders.base_DEFAULT", methresults);

                            if (methresult != null && (methresult.RCode == APIResult.OK || methresult.RCode == APIResult.Empty))
                            {
                                treeFolders.BeginUpdate();

                                // clear any existing sub folders
                                treeFolders.SelectedNode.Nodes.Clear();

                                if (methresult.DType == APIData.DataTable)
                                {
                                    CmnUtil cmnUtil = new CmnUtil();
                                    DataTable fldtable = cmnUtil.XmlToTable(methresult.RVal);
                                    if (fldtable.Rows.Count > 0)
                                    {
                                        foreach (DataRow folder in fldtable.Rows)
                                        {
                                            // create DGP node
                                            DGPNode DGPNode = new DGPNode();
                                            DGPNode.ParentGID = folder[FolderFields.ParentGID].ToString();
                                            DGPNode.GroupGID = folder[FolderFields.GroupGID].ToString();
                                            DGPNode.AccessLevel = msgUtil.CheckAccessLevel(_main.WriteList, DGPNode.GroupGID);
                                            DGPNode.FolderGID = folder[CommonFields.rec_gid].ToString();
                                            DGPNode.FolderName = folder[FolderFields.FolderName].ToString();
                                            DGPNode.DisplayOrder = folder[FolderFields.DisplayOrder].ToString();
                                            DGPNode.row_ms = folder[CommonFields.row_ms].ToString();

                                            // create new tree node using DGP node
                                            TreeNode treeNode = new TreeNode(DGPNode.FolderName);
                                            if (DGPNode.AccessLevel == AccessLevel.ReadWrite)
                                            {
                                                treeNode.ImageIndex = 1;
                                                treeNode.SelectedImageIndex = 1;
                                            }
                                            else
                                            {
                                                treeNode.ImageIndex = 0;
                                                treeNode.SelectedImageIndex = 0;
                                            }
                                            treeNode.ToolTipText = GetGroupName(DGPNode.GroupGID);
                                            treeNode.Tag = DGPNode;

                                            treeFolders.SelectedNode.Nodes.Add(treeNode);
                                        }
                                    }
                                }
                            }

                            treeFolders.EndUpdate();
                        }
                        else
                        {
                            MessageBox.Show("No response message was returned", "Respons Message Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.GetSubFolders", ex);
                MessageBox.Show(ex.Message, "ucFileStore.GetSubFolders", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetGroupName(string groupGID)
        {
            string groupName = "";

            try
            {
                if (ReadGroups != null && ReadGroups.Rows.Count > 0)
                {
                    foreach (DataRow dr in ReadGroups.Rows)
                    {
                        if (dr["GroupGID"].ToString() == groupGID)
                        {
                            groupName = dr["GroupName"].ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Empty user group membership list.", "User Group Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.GetGroupName", ex);
                MessageBox.Show(ex.Message, "ucFileStore.GetGroupName", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return groupName;
        }

        /// <summary>
        /// repopulate subfolders of current selected node (used when adding new subfolders to a node)
        /// </summary>
        public void RefreshFolder()
        {
            GetSubFolders();

            FileSearch();

            treeFolders.SelectedNode.Expand();
        }

        /// <summary>
        /// set parent of current selected node as selected and then repopulate subfolders (used when editing existing node record)
        /// </summary>
        public void RefreshParentFolder()
        {
            if (treeFolders.SelectedNode.Parent != null && treeFolders.SelectedNode.Parent.GetType() == typeof(TreeNode))
            {
                TreeNode parentNode = treeFolders.SelectedNode.Parent;
                treeFolders.SelectedNode = parentNode;

                GetSubFolders();

                FileSearch();

                treeFolders.SelectedNode.Expand();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetTotalPages()
        {
            decimal tmp = TotalRows / PageSize;
            TotalPages = Convert.ToInt32(Math.Ceiling(tmp));
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetPageLabel(int pageNum, int totalPages)
        {
            tslblPages.Text = "Page " + pageNum.ToString() + " of " + totalPages.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearPageInfo()
        {
            TotalRows = 0;
            PageNum = 0;
            TotalPages = 0;
            SetPageLabel(0, 0);
            dgvFileList.DataSource = null;
            _main.SetMetrics(0, 0, 0, "", "");
        }

        /// <summary>
        /// 
        /// </summary>
        private void treeFolders_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // right click on folder selects node and modifies folder context menu
                if (e.Button == MouseButtons.Right)
                {
                    treeFolders.SelectedNode = treeFolders.GetNodeAt(e.X, e.Y);

                    TreeNode selectedNode = treeFolders.SelectedNode;
                    _selectedNode = treeFolders.SelectedNode;
                    _selNodeInfo = (DGPNode)_selectedNode.Tag;
                    tslblFolderPath.Text = _selectedNode.FullPath;

                    if (_selNodeInfo.AccessLevel == AccessLevel.ReadOnly)
                    {
                        // disable folder context menu
                        DisableFolderMenu();

                        // allow adding subfolders to the home folder regardless of access level
                        if (_selectedNode.Text == "Home")
                        {
                            subFolderMenuItem.Visible = true;
                            editFolderMenuItem.Visible = false;
                            addNewFileMenuItem.Visible = false;
                        }
                    }
                    else
                    {
                        // enable functionality for accounts with write access
                        EnableFolderMenu();

                        // allow adding subfolders to the home folder regardless of access level
                        if (_selectedNode.Text == "Home")
                        {
                            subFolderMenuItem.Visible = true;
                            editFolderMenuItem.Visible = false;
                            addNewFileMenuItem.Visible = false;
                        }
                    }
                }
                else
                {
                    ClearPageInfo();

                    treeFolders.SelectedNode = treeFolders.GetNodeAt(e.X, e.Y);

                    TreeNode selectedNode = treeFolders.SelectedNode;
                    _selectedNode = treeFolders.SelectedNode;
                    _selNodeInfo = (DGPNode)_selectedNode.Tag;
                    tslblFolderPath.Text = _selectedNode.FullPath;

                    // only populate active files when searching by folder
                    SearchState = RecState.Active;

                    if (_selectedNode.Text == "Favorites")
                    {
                        SearchMode = SearchModeVal.ByFavorite;
                        DisableFolderMenu();
                    }
                    else
                    {
                        SearchMode = SearchModeVal.ByFolder;
                        GetSubFolders();
                    }

                    FileSearch();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.treeFolders_MouseDown", ex);
                MessageBox.Show(ex.Message, "ucFileStore.treeFolders_MouseDown", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void treeFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                //ClearPageInfo();

                //treeFolders.SelectedNode = treeFolders.GetNodeAt(e.X, e.Y);

                //TreeNode selectedNode = treeFolders.SelectedNode;
                //_selectedNode = treeFolders.SelectedNode;
                //_selNodeInfo = (DGPNode)_selectedNode.Tag;
                //tslblFolderPath.Text = _selectedNode.FullPath;

                //// only populate active files when searching by folder
                //SearchState = RecState.Active;

                //if (_selectedNode.Text == "Favorites")
                //{
                //    SearchMode = SearchModeVal.ByFavorite;
                //    DisableFolderMenu();
                //}
                //else
                //{
                //    SearchMode = SearchModeVal.ByFolder;
                //    GetSubFolders();
                //}

                //FileSearch();
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.treeFolders_NodeMouseClick", ex);
                MessageBox.Show(ex.Message, "ucFileStore.treeFolders_NodeMouseClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisableFolderMenu()
        {
            // disable context menu items
            subFolderMenuItem.Visible = false;
            editFolderMenuItem.Visible = false;
            addNewFileMenuItem.Visible = false;
            refreshSubfoldersMenuItem.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void EnableFolderMenu()
        {
            // disable context menu items
            subFolderMenuItem.Visible = true;
            editFolderMenuItem.Visible = true;
            addNewFileMenuItem.Visible = true;
            refreshSubfoldersMenuItem.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void subFolderMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // show modal sub folder data entry form
                if (treeFolders.SelectedNode.Text != "Favorites")
                {
                    frmEditFolder editFolder = new frmEditFolder(_main, this, _selNodeInfo, ReplicaAction.Insert, false, false);
                    editFolder.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.subFolderMenuItem_Click", ex);
                MessageBox.Show(ex.Message, "ucFileStore.subFolderMenuItem_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void editFolderMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeFolders.SelectedNode.Text != "Favorites")
                {
                    // folders cannot be deleted if they have any subfolders or linked files
                    bool subFolders = false;
                    if (treeFolders.SelectedNode.Nodes.Count > 0) subFolders = true;

                    bool linkedFiles = false;
                    if (_selNodeInfo.FileCount > 0) linkedFiles = true;

                    // show modal sub folder data entry form
                    frmEditFolder editFolder = new frmEditFolder(_main, this, _selNodeInfo, ReplicaAction.Update, subFolders, linkedFiles);
                    editFolder.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.editFolderMenuItem_Click", ex);
                MessageBox.Show(ex.Message, "ucFileStore.editFolderMenuItem_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void refreshSubfoldersMenuItem_Click(object sender, EventArgs e)
        {
            // repopulate child nodes of selected folder
            RefreshFolder();
        }

        /// <summary>
        /// 
        /// </summary>
        private void addNewFileMenuItem_Click(object sender, EventArgs e)
        {
            frmFileUpload2 fileUpload = new frmFileUpload2(_main, this, _selNodeInfo, tslblFolderPath.Text, ReplicaAction.Insert, "", "", "", "", "1");
            fileUpload.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DGPTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tslblFolderPath.Text = "";
                ClearPageInfo();

                if (DGPTabControl.SelectedIndex == 0)
                {
                    // Folder tab
                    SearchState = RecState.Active;
                    dgvFileList.DataSource = null;

                    if (treeFolders.SelectedNode.Text == "Favorites")
                    {
                        SearchMode = SearchModeVal.ByFavorite;
                    }
                    else
                    {
                        SearchMode = SearchModeVal.ByFolder;
                    }
                }
                else if (DGPTabControl.SelectedIndex == 1)
                {
                    // File metadata search tab
                    dgvFileList.DataSource = null;

                    // metadata allows searching for deleted files: get search record state from metadata combobox
                    SearchState = cmbFileState.Text;

                    cmbFileExt.Text = "*";

                    // populate the file extension combo box
                    Dictionary<string, string> methparams = new Dictionary<string, string>();
                    methparams.Add("SchemaFlag", BoolVal.TRUE);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("File.GetExtensionList.base", methparams);
                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult("File.GetExtensionList.base_DEFAULT", methresults);

                        if (methresult != null && (methresult.RCode == APIResult.OK || methresult.RCode == APIResult.Empty))
                        {
                            if (methresult.DType == APIData.DataTable)
                            {
                                CmnUtil cmnUtil = new CmnUtil();
                                DataTable exttable = cmnUtil.XmlToTable(methresult.RVal);
                                if (exttable.Rows.Count > 0)
                                {
                                    cmbFileExt.Items.Add("*");
                                    foreach (DataRow dr in exttable.Rows)
                                    {
                                        cmbFileExt.Items.Add(dr["FileExt"].ToString());
                                    }

                                    cmbFileExt.SelectedIndex = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No response message was returned", "Respons Message Error");
                    }

                    cmbFileState.SelectedIndex = 0;
                }
                else if (DGPTabControl.SelectedIndex == 2)
                {
                    // File tag search tab
                    SearchState = RecState.Active;
                    dgvFileList.DataSource = null;

                    // populate the tag list grid
                    FilterTags();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.DGPTabControl_SelectedIndexChanged", ex);
                MessageBox.Show(ex.Message, "ucFileStore.DGPTabControl_SelectedIndexChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// process to populate the file grid using several different queries (API method calls)
        /// </summary>
        public void FileSearch()
        {
            string countMethod = "";
            string searchMethod = "";

            try
            {
                Dictionary<string, string> searchParams = new Dictionary<string, string>();
                switch (SearchMode)
                {
                    case SearchModeVal.ByFavorite:
                        countMethod = "File.GetCountByFavorite.base";
                        searchMethod = "File.GetFilesByFavorite.base";
                        searchParams.Add(APIUserFields.UserGID, _main.UserGID);
                        break;

                    case SearchModeVal.ByFolder:
                        countMethod = "File.GetCountByFolder.base";
                        searchMethod = "File.GetFilesByFolder.base";
                        searchParams.Add(FolderFields.FolderGID, _selNodeInfo.FolderGID);
                        break;

                    case SearchModeVal.ByMetadata:
                        countMethod = "File.GetCountByMetadata.base";
                        searchMethod = "File.GetFilesByMetadata.base";
                        SetSearchState();
                        searchParams.Add(FileFields.FileName, tbxFileName.Text);
                        searchParams.Add(FileFields.FileExt, cmbFileExt.Text);
                        if (dtpFileDate.Checked)
                        {
                            searchParams.Add(FileFields.FileDate, dtpFileDate.Text);
                        }
                        else
                        {
                            searchParams.Add(FileFields.FileDate, "");
                        }
                        break;

                    case SearchModeVal.ByTags:
                        countMethod = "File.GetCountByTag.base";
                        searchMethod = "File.GetFilesByTag.base";
                        searchParams.Add(TagFields.TagGID, _taggid);
                        break;
                }

                searchParams.Add(CommonFields.PageNum, PageNum.ToString());
                searchParams.Add(CommonFields.PageSize, PageSize.ToString());
                searchParams.Add(CommonFields.rec_state, SearchState);
                searchParams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);
                searchParams.Add(FileFields.SortBy, tscmbSortBy.Text);
                searchParams.Add(FileFields.SortOrder, tscmbOrder.Text);

                MsgUtil msgUtil = new MsgUtil();
                string methlist = msgUtil.CreateXMLMethod(countMethod, searchParams);
                methlist += msgUtil.CreateXMLMethod(searchMethod, searchParams);
                string reqID = msgUtil.GetNewGID();

                Stopwatch sw = new Stopwatch();
                sw.Start();

                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methlist, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                sw.Stop();

                if (respmsg != "")
                {
                    // read response message results
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    _main.SetMetrics(2, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "DGP", "ucFileStore");

                    if (respinfo.Auth.ToUpper() == AuthState.OK)
                    {
                        ResInfo count = msgUtil.GetResult(countMethod + "_" + MethReturn.Default, methresults);

                        if (count != null && count.RCode.ToUpper() == APIResult.OK)
                        {
                            TotalRows = Convert.ToInt32(count.RVal);
                            SetTotalPages();
                            if (SearchMode == SearchModeVal.ByFolder) _selNodeInfo.FileCount = Convert.ToInt32(TotalRows);

                            ResInfo search = msgUtil.GetResult(searchMethod + "_" + MethReturn.Default, methresults);

                            if (search.RCode.ToUpper() == APIResult.OK || search.RCode.ToUpper() == APIResult.Empty)
                            {
                                if (search.DType == APIData.DataTable)
                                {
                                    // convert xml to datatable
                                    CmnUtil cmnUtil = new CmnUtil();
                                    DataTable fileList = cmnUtil.XmlToTable(search.RVal);

                                    if (fileList.Rows.Count > 0)
                                    {
                                        SetPageLabel(PageNum + 1, (int)TotalPages);

                                        DataColumn access = fileList.Columns.Add("RW");
                                        access.SetOrdinal(0);

                                        // indicate which records are read only vs writable
                                        foreach (DataRow dr in fileList.Rows)
                                        {
                                            if (_main.WriteList.IndexOf(dr["GroupGID"].ToString(), 0) != -1)
                                            {
                                                dr["RW"] = "W";
                                            }
                                            else
                                            {
                                                dr["RW"] = "R";
                                            }
                                        }

                                        dgvFileList.DataSource = fileList.DefaultView;
                                    }
                                    else
                                    {
                                        dgvFileList.DataSource = null;
                                    }
                                }
                                else
                                {
                                    dgvFileList.DataSource = null;
                                }
                            }
                            else
                            {
                                dgvFileList.DataSource = null;
                                MessageBox.Show(search.RCode + " : " + search.RVal, searchMethod, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dgvFileList.DataSource = null;
                            MessageBox.Show(count.RCode + " : " + count.RVal, countMethod, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // show error message
                        string errmsg = "The following error occurred: " + respinfo.Auth + " - " + respinfo.Info;
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.FileSearch", "Authentication error.", errmsg);
                        MessageBox.Show(errmsg, "API Request Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.FileSearch", ex);
                MessageBox.Show(ex.Message, "ucFileStore.FileSearch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void tsFileList_Resize(object sender, EventArgs e)
        {
            // adjust the folder path textbox to stretch with the size of the toolstrip
            tslblFolderPath.Width = tsFileList.Width - 500;
        }

        /// <summary>
        /// 
        /// </summary>
        private void downloadMenuItem_Click(object sender, EventArgs e)
        {
            // instantiate the file download form with the values of the selected record
            frmFileDownload2 fileDownload = new frmFileDownload2(_main, this, _shardname, _filegid, _fileversion, _filename, _fileext, _filedate, _filesize.ToString(), _filehash, _segsize.ToString(), _totalseg.ToString());
            fileDownload.ShowDialog();
        }

        /// <summary>
        /// this version of upload replaces an existing file with a new version of that file
        /// </summary>
        private void uploadMenuItem_Click(object sender, EventArgs e)
        {
            frmFileUpload2 fileUpload = new frmFileUpload2(_main, this, _selNodeInfo, _folderpath, ReplicaAction.Update, _filegid, _filename, _filedescrip, _row_ms, _fileversion);
            fileUpload.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void historyMenuItem_Click(object sender, EventArgs e)
        {
            frmFileHist fileHist = new frmFileHist(_main, this, _filegid, _filename);
            fileHist.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void editFileInfoMenuItem_Click(object sender, EventArgs e)
        {
            frmEditFile editFile = new frmEditFile(_main, this, _filegid, _filename, _filedescrip, _row_ms);
            editFile.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void recoverDeletedMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // read values from selected row in grid
                string recgid = dgvFileList.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
                string rowid = dgvFileList.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();

                // call process method to recover edited record
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add("Action", ReplicaAction.Recover);
                methparams.Add(CommonFields.rec_gid, recgid);
                methparams.Add(CommonFields.row_id, rowid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("File.Recover.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("File.Recover.base_DEFAULT", methresults);

                    if (methresult.RCode.ToUpper() == APIResult.OK)
                    {
                        // update the grid
                        FileSearch();
                    }
                    else
                    {
                        // error saving user info: display error message
                        MessageBox.Show(methresult.RVal, methresult.RCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.recoverDeletedMenuItem_Click", ex);
                MessageBox.Show(ex.Message, "ucFileStore.recoverDeletedMenuItem_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void cmbFileState_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSearchState();
            dgvFileList.DataSource = null;
        }

        private void SetSearchState()
        {
            // search state combobox is only visible when the search tab is selected
            if (SearchMode == SearchModeVal.ByMetadata)
            {
                SearchState = cmbFileState.Text;
            }
        }

        /// <summary>
        /// right click on a file grid record will use the values for file download, file upload, etc.
        /// </summary>
        private void dgvFileList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.dgvFileList.Rows[e.RowIndex].Selected = true;
                    _shardname = dgvFileList.SelectedRows[0].Cells[FileFields.ShardName].Value.ToString();
                    _foldergid = dgvFileList.SelectedRows[0].Cells[FolderFields.FolderGID].Value.ToString();
                    _filegid = dgvFileList.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
                    _row_ms = dgvFileList.SelectedRows[0].Cells[CommonFields.row_ms].Value.ToString();
                    _filename = dgvFileList.SelectedRows[0].Cells[FileFields.FileName].Value.ToString();
                    _filedescrip = dgvFileList.SelectedRows[0].Cells[FileFields.FileDescrip].Value.ToString();
                    _fileext = dgvFileList.SelectedRows[0].Cells[FileFields.FileExt].Value.ToString();
                    _filehash = dgvFileList.SelectedRows[0].Cells[FileFields.FileHash].Value.ToString();
                    _fileversion = dgvFileList.SelectedRows[0].Cells[FileFields.FileVersion].Value.ToString();
                    _filesize = Convert.ToInt32(dgvFileList.SelectedRows[0].Cells[FileFields.FileSize].Value);
                    _filedate = Convert.ToDateTime(dgvFileList.SelectedRows[0].Cells[FileFields.FileDate].Value);
                    _folderpath = dgvFileList.SelectedRows[0].Cells[FolderFields.FolderPath].Value.ToString();
                    _segsize = Convert.ToInt32(dgvFileList.SelectedRows[0].Cells[FileFields.SegSize].Value.ToString());
                    _totalseg = Convert.ToInt32(dgvFileList.SelectedRows[0].Cells[FileFields.TotalSeg].Value.ToString());

                    string rw = dgvFileList.SelectedRows[0].Cells["RW"].Value.ToString();

                    ResetFileMenu();
                    switch (SearchMode)
                    {
                        case SearchModeVal.ByFavorite:
                            removeFavoriteMenuItem.Visible = true;
                            addFavoritesMenuItem.Visible = false;
                            break;

                        case SearchModeVal.ByFolder:

                            break;

                        case SearchModeVal.ByMetadata:
                            if (rw == "W" && SearchState == RecState.Deleted)
                            {
                                recoverDeletedMenuItem.Visible = true;
                            }
                            break;

                        case SearchModeVal.ByTags:

                            break;
                    }

                    if (rw == "W" && SearchState != RecState.Deleted)
                    {
                        editFileInfoMenuItem.Visible = true;
                        historyMenuItem.Visible = true;
                        uploadFileMenuItem.Visible = true;
                        fileTagsMenuItem.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.dgvFileList_CellMouseUp", ex);
                MessageBox.Show(ex.Message, "ucFileStore.dgvFileList_CellMouseUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetFileMenu()
        {
            downloadFileMenuItem.Visible = false;
            addFavoritesMenuItem.Visible = false;
            copyFileInfoMenuItem.Visible = false;
            editFileInfoMenuItem.Visible = false;
            historyMenuItem.Visible = false;
            uploadFileMenuItem.Visible = false;
            recoverDeletedMenuItem.Visible = false;
            removeFavoriteMenuItem.Visible = false;
            fileTagsMenuItem.Visible = false;

            if (SearchState != RecState.Deleted)
            {
                downloadFileMenuItem.Visible = true;
                addFavoritesMenuItem.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void tscmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPageInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void tsbtnFirst_Click(object sender, EventArgs e)
        {
            PageNum = 0;
            FileSearch();
        }

        /// <summary>
        /// 
        /// </summary>
        private void tsbtnPrev_Click(object sender, EventArgs e)
        {
            if (PageNum > 0) PageNum--;
            FileSearch();
        }

        /// <summary>
        /// 
        /// </summary>
        private void tsbtnNext_Click(object sender, EventArgs e)
        {
            if (PageNum < TotalPages) PageNum++;
            FileSearch();
        }

        /// <summary>
        /// 
        /// </summary>
        private void tsbtnLast_Click(object sender, EventArgs e)
        {
            PageNum = Convert.ToInt32(TotalPages - 1);
            FileSearch();
        }

        /// <summary>
        /// 
        /// </summary>
        private void copyFileInfoMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("File Name: " + _filename + _fileext  + "\nGlobalID : " + _filegid);
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMode = SearchModeVal.ByMetadata;
            ClearPageInfo();
            FileSearch();
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // clear search form
            tbxFileName.Text = "";
            cmbFileExt.SelectedIndex = -1;
            dtpFileDate.Checked = false;
            cmbFileState.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            FileSearch();
        }

        /// <summary>
        /// 
        /// </summary>
        private void removeFavoriteMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // remove FileGID from favorites for UserGID
                string FavoriteGID = dgvFileList.SelectedRows[0].Cells[FavoriteFields.FavoriteGID].Value.ToString();
                string FileGID = dgvFileList.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();

                // call process method to recover edited record
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(FavoriteFields.FavoriteGID, FavoriteGID);
                methparams.Add(FileFields.FileGID, FileGID);
                methparams.Add(APIUserFields.UserGID, _main.UserGID);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("Favorite.Remove.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("Favorite.Remove.base_DEFAULT", methresults);

                    if (methresult.RCode.ToUpper() == APIResult.OK)
                    {
                        // update the grid
                        FileSearch();
                    }
                    else
                    {
                        // error saving user info: display error message
                        MessageBox.Show(methresult.RVal, methresult.RCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.removeFavoriteMenuItem_Click", ex);
                MessageBox.Show(ex.Message, "ucFileStore.removeFavoriteMenuItem_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void addFavoritesMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // add FileGID to favorites for UserGID (insure no duplicates)
                string recgid = dgvFileList.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();

                // call process method to assign new favorite
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(FileFields.FileGID, recgid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("Favorite.Assign.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("Favorite.Assign.base_DEFAULT", methresults);

                    if (methresult.RCode.ToUpper() == APIResult.OK)
                    {
                        // update the grid
                        FileSearch();
                    }
                    else
                    {
                        // error saving user info: display error message
                        MessageBox.Show(methresult.RVal, methresult.RCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.addFavoritesMenuItem_Click", ex);
                MessageBox.Show(ex.Message, "ucFileStore.addFavoritesMenuItem_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileTagsMenuItem_Click(object sender, EventArgs e)
        {
            frmFileTags fileTags = new frmFileTags(_main, this, _filegid, _filename);
            fileTags.ShowDialog();
        }

        private void btnSearchTags_Click(object sender, EventArgs e)
        {
            SearchMode = SearchModeVal.ByTags;
            ClearPageInfo();

            if (tbxSelectedTag.Text != null && tbxSelectedTag.Text != "")
            {
                FileSearch();
            }
            else
            {
                MessageBox.Show("You must select a tag to search by tags.", "Tag Search Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGetTags_Click(object sender, EventArgs e)
        {
            FilterTags();
        }

        private void FilterTags()
        {
            try
            {
                Dictionary<string, string> tagparams = new Dictionary<string, string>();
                tagparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);
                tagparams.Add(TagFields.TagName, tbxFilterTagName.Text);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("Tag.FilterByName.base", tagparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("Tag.FilterByName.base_DEFAULT", methresults);

                    if (methresult.RCode.ToUpper() == APIResult.OK || methresult.RCode.ToUpper() == APIResult.Empty)
                    {
                        if (methresult.DType == APIData.DataTable)
                        {
                            // convert xml to datatable
                            CmnUtil cmnUtil = new CmnUtil();
                            DataTable tagList = cmnUtil.XmlToTable(methresult.RVal);

                            if (tagList.Rows.Count > 0)
                            {
                                dgvTagList.DataSource = tagList.DefaultView;
                            }
                            else
                            {
                                dgvTagList.DataSource = null;
                            }
                        }
                        else
                        {
                            dgvTagList.DataSource = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show(methresult.RVal, "ucFileStore:btnGetTags_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.FilterTags", ex);
                MessageBox.Show(ex.Message, "ucFileStore.FilterTags", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTagList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.dgvTagList.Rows[e.RowIndex].Selected = true;
                _taggid = dgvTagList.SelectedRows[0].Cells["rec_gid"].Value.ToString();
                tbxSelectedTag.Text = dgvTagList.SelectedRows[0].Cells["TagName"].Value.ToString();
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.dgvTagList_CellDoubleClick", ex);
                MessageBox.Show(ex.Message, "ucFileStore.dgvTagList_CellDoubleClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
