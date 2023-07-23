using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.FileStore
{
    public partial class frmEditFolder : Form
    {
        MainForm _main;
        ucFileStore _parent;
        DGPNode _folder;
        bool _hasSubFolders;
        bool _hasFiles;

        public frmEditFolder(MainForm mainForm, ucFileStore parentForm, DGPNode selNode, string action, bool subFolders, bool linkedFiles)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
                _parent = parentForm;
                _folder = selNode;
                _hasSubFolders = subFolders;
                _hasFiles = linkedFiles;

                EnableControls();

                // bind table to combo box
                cmbxDataGroups.DataSource = _parent.WriteGroups;
                cmbxDataGroups.DisplayMember = "GroupName";
                cmbxDataGroups.ValueMember = "GroupGID";

                if (action == ReplicaAction.Insert)
                {
                    // add new sub folder below selected folder
                    this.Text = "Add SubFolder";
                    tbxGlobalID.Text = "";
                    tbxParentGID.Enabled = false;
                    tbxParentGID.Text = _folder.FolderGID;
                    tbxDisplayOrder.Text = "0";
                    btnDelete.Enabled = false;
                }
                else
                {
                    // update data of selected folder
                    this.Text = "Edit Folder";
                    tbxGlobalID.Text = _folder.FolderGID;
                    tbxParentGID.Enabled = true;
                    tbxParentGID.Text = _folder.ParentGID;
                    tbxFolderName.Text = _folder.FolderName;
                    tbxDisplayOrder.Text = _folder.DisplayOrder;
                    cmbxDataGroups.SelectedValue = _folder.GroupGID;
                    cmbxDataGroups.Enabled = false;
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditFolder", ex);
                MessageBox.Show(ex.Message, "frmEditFolder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isValid = false;
            bool isNew = false;

            try
            {
                string apimethcall = "";
                Dictionary<string, string> methparams = new Dictionary<string, string>();

                if (tbxGlobalID.Text == null || tbxGlobalID.Text == "")
                {
                    // add new folder record (adds child node to selected node)
                    isNew = true;
                    apimethcall = "Folder.AddSubFolder.base";
                    isValid = CheckFields("NEW");
                }
                else
                {
                    // edit existing folder record (edits selected node)
                    apimethcall = "Folder.Save.base";
                    methparams.Add("FolderGID", _folder.FolderGID);
                    methparams.Add(CommonFields.row_ms, _folder.row_ms);
                    isValid = CheckFields("UPDATE");
                }

                if (isValid)
                {
                    methparams.Add("GroupGID", cmbxDataGroups.SelectedValue.ToString());
                    methparams.Add("ParentGID", tbxParentGID.Text);
                    methparams.Add("FolderName", tbxFolderName.Text);
                    methparams.Add("DisplayOrder", tbxDisplayOrder.Text);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod(apimethcall, methparams);
                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult(apimethcall + "_" + MethReturn.Default, methresults);

                        if (methresult.RCode.ToUpper() == APIResult.OK)
                        {
                            if (isNew)
                            {
                                // update the current selected node
                                _parent.RefreshFolder();
                            }
                            else
                            {
                                // update the parent node of the selected node
                                _parent.RefreshParentFolder();
                            }

                            // close the dialog
                            this.Close();
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
                else
                {
                    MessageBox.Show("You must provide values for all required fields", "Required Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditFolder:btnSave_Click", ex);
                MessageBox.Show(ex.Message, "frmEditFolder:btnSave_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //  cannot delete if any subfolders, cannot delete if any linked files
                if (!_hasSubFolders && !_hasFiles)
                {
                    if (CheckFields("delete"))
                    {
                        Dictionary<string, string> methparams = new Dictionary<string, string>();
                        methparams.Add("FolderGID", _folder.FolderGID);
                        methparams.Add("GroupGID", _folder.GroupGID);
                        methparams.Add("ParentGID", _folder.ParentGID);
                        methparams.Add("FolderName", _folder.FolderName);
                        methparams.Add("DisplayOrder", _folder.DisplayOrder);
                        methparams.Add(CommonFields.row_ms, _folder.row_ms);

                        MsgUtil msgUtil = new MsgUtil();
                        string reqID = msgUtil.GetNewGID();
                        string methList = msgUtil.CreateXMLMethod("Folder.Delete.base", methparams);
                        SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                        string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                        if (respmsg != "")
                        {
                            RespInfo respinfo = new RespInfo();
                            Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                            ResInfo methresult = msgUtil.GetResult("Folder.Delete.base_DEFAULT", methresults);

                            if (methresult.RCode.ToUpper() == APIResult.OK)
                            {
                                // update the child nodes of the parent
                                _parent.RefreshParentFolder();

                                // close the dialog
                                this.Close();
                            }
                            else
                            {
                                // error saving method info: display error message
                                MessageBox.Show(methresult.RVal, methresult.RCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No response message was returned", "Respons Message Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must provide values for all required fields", "Required Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (_hasSubFolders)
                    {
                        MessageBox.Show("You cannot delete a folder that has subfolders", "Folder Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("You cannot delete a folder that contains files", "Folder Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditFolder:btnDelete_Click", ex);
                MessageBox.Show(ex.Message, "frmEditFolder:btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // *********************************************************** //
        // *********************************************************** //

        private void EnableControls()
        {
            tbxGlobalID.Enabled = true;
            tbxParentGID.Enabled = true;
            tbxFolderName.Enabled = true;
            cmbxDataGroups.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
        }

        private bool CheckFields(string action)
        {
            bool fieldsOK = true;

            if (action == "SAVE")
            {
                if (tbxGlobalID.Text == null || tbxGlobalID.Text == "") fieldsOK = false;
            }

            if (tbxParentGID.Text == null || tbxParentGID.Text == "") fieldsOK = false;
            if (tbxFolderName.Text == null || tbxFolderName.Text == "") fieldsOK = false;
            if (cmbxDataGroups.Text == null || cmbxDataGroups.Text == "") fieldsOK = false;

            return fieldsOK;
        }
    }
}
