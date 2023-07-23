using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;
using System.Net.Http;

using CliUtil;

namespace DGPDrive.Screens.Security
{
    public partial class frmRoleMethods : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        ucApiRoles _parent;
        string _rolegid;
        string _rolename;
        string _apiname;

        string assignMethod = "RoleMethod.GetAssigned.base";
        string availMethod = "RoleMethod.GetAvailable.base";

        public frmRoleMethods(HttpClient httpClient, MainForm mainForm, ucApiRoles parentForm, string roleGID, string roleName)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _parent = parentForm;
                _rolegid = roleGID;
                _rolename = roleName;

                if (_rolegid != null && _rolegid != "")
                {
                    _rolegid = roleGID;
                    _rolename = roleName;
                    this.Text = "RoleMethods: " + _rolename;

                    PopulateApiList();
                    PopulateGrids();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmRoleMethods", ex);
                MessageBox.Show(ex.Message, "frmRoleMethods", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateApiList()
        {
            dgvApiList.DataSource = null;

            Dictionary<string, string> methparams = new Dictionary<string, string>();
            methparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

            MsgUtil msgUtil = new MsgUtil();
            string reqID = msgUtil.GetNewGID();
            string methList = msgUtil.CreateXMLMethod("APIMethod.GetAPIList.base", methparams);
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

                ResInfo methresult = msgUtil.GetResult("APIMethod.GetAPIList.base_DEFAULT", methresults);

                if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                {
                    CmnUtil cmnUtil = new CmnUtil();
                    DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                    if (methtable.Rows.Count > 0)
                    {
                        dgvApiList.DataSource = methtable;
                        this.dgvApiList.Rows[0].Selected = true;
                        _apiname = dgvApiList.SelectedRows[0].Cells[APIMethodFields.APIName].Value.ToString();
                    }
                    else
                    {
                        MessageBox.Show(methresult.RVal, "RoleMethods:PopulateAPIList", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No response message was returned", "Respons Message Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateGrids()
        {
            try
            {
                dgvAssignedMethods.DataSource = null;
                dgvAvailableMethods.DataSource = null;

                Dictionary<string, string> methsearchparams = new Dictionary<string, string>();
                methsearchparams.Add(APIRoleFields.RoleGID, _rolegid);
                methsearchparams.Add(APIMethodFields.APIName, _apiname);
                methsearchparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                MsgUtil msgUtil = new MsgUtil();
                string methlist = msgUtil.CreateXMLMethod(assignMethod, methsearchparams);
                methlist += msgUtil.CreateXMLMethod(availMethod, methsearchparams);

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

                    CmnUtil cmnUtil = new CmnUtil();
                    if (respinfo.Auth.ToUpper() == AuthState.OK)
                    {
                        ResInfo assignedresult = msgUtil.GetResult(assignMethod + "_" + MethReturn.Default, methresults);
                        if (assignedresult != null && assignedresult.RCode == APIResult.OK && assignedresult.DType == APIData.DataTable)
                        {
                            DataTable assignedTbl = cmnUtil.XmlToTable(assignedresult.RVal);
                            if (assignedTbl.Rows.Count > 0)
                            {
                                dgvAssignedMethods.DataSource = assignedTbl;
                            }
                        }

                        ResInfo availableresult = msgUtil.GetResult(availMethod + "_" + MethReturn.Default, methresults);
                        if (availableresult != null && availableresult.RCode == APIResult.OK && availableresult.DType == APIData.DataTable)
                        {
                            DataTable availableTbl = cmnUtil.XmlToTable(availableresult.RVal);
                            if (availableTbl.Rows.Count > 0)
                            {
                                dgvAvailableMethods.DataSource = availableTbl;
                            }
                        }
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmRoleMethods.PopulateGrids", "Authentication error.", "Authorization error = " + respinfo.Auth);
                        MessageBox.Show(respinfo.Info, "frmRoleMethods.PopulateGrids", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmRoleMethods.PopulateGrids", ex);
                MessageBox.Show(ex.Message, "frmRoleMethods.PopulateGrids", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// add a method to the role
        /// </summary>
        private void dgvAvailMethods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // add selected method
                this.dgvAvailableMethods.Rows[e.RowIndex].Selected = true;

                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(APIMethodFields.MethodGID, dgvAvailableMethods.SelectedRows[0].Cells[APIMethodFields.MethodGID].Value.ToString());
                methparams.Add(APIRoleFields.RoleGID, _rolegid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("RoleMethod.Assign.base", methparams);
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

                    ResInfo methresult = msgUtil.GetResult("RoleMethod.Assign.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        // populate grids
                        PopulateGrids();
                    }
                    else
                    {
                        MessageBox.Show("Unable to assign method to the role.", "Assign Method Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmRoleMethods.dgvAvailMethods_CellDoubleClick", ex);
                MessageBox.Show(ex.Message, "frmRoleMethods.dgvAvailMethods_CellDoubleClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// remove a method from the role
        /// </summary>
        private void dgvAssignedMethods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // remove selected method
                this.dgvAssignedMethods.Rows[e.RowIndex].Selected = true;

                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(RoleMethodFields.RoleMethodGID, dgvAssignedMethods.SelectedRows[0].Cells[RoleMethodFields.RoleMethodGID].Value.ToString());
                methparams.Add(APIMethodFields.MethodGID, dgvAssignedMethods.SelectedRows[0].Cells[APIMethodFields.MethodGID].Value.ToString());
                methparams.Add(APIRoleFields.RoleGID, _rolegid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("RoleMethod.Remove.base", methparams);
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

                    ResInfo methresult = msgUtil.GetResult("RoleMethod.Remove.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        // populate grids
                        PopulateGrids();
                    }
                    else
                    {
                        MessageBox.Show("Unable to remove the method from the role.", "Remove Method Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmRoleMethods.dgvAssignedMethods_CellDoubleClick", ex);
                MessageBox.Show(ex.Message, "frmRoleMethods.dgvAssignedMethods_CellDoubleClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvApiList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvApiList.Rows[e.RowIndex].Selected = true;
            _apiname = dgvApiList.SelectedRows[0].Cells[APIMethodFields.APIName].Value.ToString();
            PopulateGrids();
        }
    }
}
