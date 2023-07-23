using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Http;

using CliUtil;

namespace DGPDrive.Screens.Security
{
    public partial class frmUserGroups : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        ucApiUsers _parent;
        string _usergid;
        string _username;

        public frmUserGroups(HttpClient httpClient, MainForm mainForm, ucApiUsers parentForm, string userGID, string userName)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _parent = parentForm;
                _usergid = userGID;
                _username = userName;

                if (_usergid != null && _usergid != "")
                {
                    _usergid = userGID;
                    _username = userName;
                    this.Text = "User DataGroup Access: " + _username;
                    PopulateGrids();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmUserGroups", ex);
                MessageBox.Show(ex.Message, "frmUserGroups", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateGrids()
        {
            try
            {
                dgvAssignedGroups.DataSource = null;
                dgvAvailableGroups.DataSource = null;

                Dictionary<string, string> methsearchparams = new Dictionary<string, string>();
                methsearchparams.Add(APIUserFields.UserGID, _usergid);
                methsearchparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                MsgUtil msgUtil = new MsgUtil();
                string methlist = msgUtil.CreateXMLMethod("GroupUser.GetAssigned.base", methsearchparams);
                methlist += msgUtil.CreateXMLMethod("GroupUser.GetAvailable.base", methsearchparams);
                string reqID = msgUtil.GetNewGID();

                string respmsg = msgUtil.ApiMethHelper(methlist,
                                                        reqID,
                                                        _main.UserName,
                                                        _main.Password,
                                                        _httpClient,
                                                        _main.SvcUrl);

                if (respmsg != "")
                {
                    // read response message results
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    if (respinfo.Auth.ToUpper() == AuthState.OK)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        ResInfo assignedresult = msgUtil.GetResult("GroupUser.GetAssigned.base_DEFAULT", methresults);
                        if (assignedresult != null && assignedresult.RCode == APIResult.OK && assignedresult.DType == APIData.DataTable)
                        {
                            DataTable assignedTbl = cmnUtil.XmlToTable(assignedresult.RVal);
                            if (assignedTbl.Rows.Count > 0)
                            {
                                dgvAssignedGroups.DataSource = assignedTbl;
                            }
                        }

                        ResInfo availableresult = msgUtil.GetResult("GroupUser.GetAvailable.base_DEFAULT", methresults);
                        if (availableresult != null && availableresult.RCode == APIResult.OK && availableresult.DType == APIData.DataTable)
                        {
                            DataTable availableTbl = cmnUtil.XmlToTable(availableresult.RVal);
                            if (availableTbl.Rows.Count > 0)
                            {
                                dgvAvailableGroups.DataSource = availableTbl;
                            }
                        }
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmUserGroups.PopulateGrids", "Authentication error.", "Authorization error = " + respinfo.Auth);
                        MessageBox.Show(respinfo.Info, "frmUserGroups.PopulateGrids", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmUserGroups.PopulateGrids", ex);
                MessageBox.Show(ex.Message, "frmUserGroups.PopulateGrids", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// add a method to the role
        /// </summary>
        private void dgvAvailableGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // add selected method
                this.dgvAvailableGroups.Rows[e.RowIndex].Selected = true;
                string accesslevel = AccessLevel.ReadOnly;
                if (rbtnReadWrite.Checked) accesslevel = AccessLevel.ReadWrite;

                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(DataGroupFields.GroupGID, dgvAvailableGroups.SelectedRows[0].Cells[DataGroupFields.GroupGID].Value.ToString());
                methparams.Add(APIUserFields.UserGID, _usergid);
                methparams.Add(GroupUserFields.AccessLevel, accesslevel);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("GroupUser.Assign.base", methparams);
                string respmsg = msgUtil.ApiMethHelper(methList,
                                                                reqID,
                                                                _main.UserName,
                                                                _main.Password,
                                                                _httpClient,
                                                                _main.SvcUrl);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("GroupUser.Assign.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        // populate grids
                        PopulateGrids();
                    }
                    else
                    {
                        MessageBox.Show("Unable to assign datagroup access to the user.", "Assign DataGroup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmUserGroups.dgvAvailableGroups_CellContentDoubleClick", ex);
                MessageBox.Show(ex.Message, "frmUserGroups.dgvAvailableGroups_CellContentDoubleClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// remove a method from the role
        /// </summary>
        private void dgvAssignedGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // remove selected method
                this.dgvAssignedGroups.Rows[e.RowIndex].Selected = true;

                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(GroupUserFields.GroupUserGID, dgvAssignedGroups.SelectedRows[0].Cells[GroupUserFields.GroupUserGID].Value.ToString());
                methparams.Add(DataGroupFields.GroupGID, dgvAssignedGroups.SelectedRows[0].Cells[DataGroupFields.GroupGID].Value.ToString());
                methparams.Add(GroupUserFields.AccessLevel, dgvAssignedGroups.SelectedRows[0].Cells[GroupUserFields.AccessLevel].Value.ToString());
                methparams.Add(APIUserFields.UserGID, _usergid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("GroupUser.Remove.base", methparams);
                string respmsg = msgUtil.ApiMethHelper(methList,
                                                            reqID,
                                                            _main.UserName,
                                                            _main.Password,
                                                            _httpClient,
                                                            _main.SvcUrl);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("GroupUser.Remove.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        // populate grids
                        PopulateGrids();
                    }
                    else
                    {
                        MessageBox.Show("Unable to remove the datagroup access from the user.", "Remove DataGroup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmUserGroups.dgvAssignedGroups_CellContentDoubleClick", ex);
                MessageBox.Show(ex.Message, "frmUserGroups:dgvAssignedGroups_CellContentDoubleClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
