
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;
using System.Data;
using System.Diagnostics;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.Security
{
    public partial class ucApiUsers : UserControl, IDriveUC
    {
        MainForm _main;

        public decimal TotalRows { get; set; }
        public decimal PageSize { get; set; }
        public decimal TotalPages { get; set; }
        public int PageNum { get; set; }
        public string SearchState { get; set; }
        public string GID { get; set; }

        string _username;
        string _firstname;
        string _lastname;

        public ucApiUsers(MainForm main)
        {
            InitializeComponent();

            try
            {
                _main = main;
                _main.SetMetrics(0, 0, 0, "", "");

                tscmbSortOrder.SelectedIndex = 0;
                tscmbState.SelectedIndex = 0;

                SetPageSize();
                SearchState = tscmbState.Text;
                PageNum = 0;
                SetPageLabel(0,0);
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucApiUsers", ex);
                MessageBox.Show(ex.Message, "ucApiUsers", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string methList = msgUtil.CreateXMLMethod("APIUser.GetPageSize.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("APIUser.GetPageSize.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        PageSize = Convert.ToDecimal(methresult.RVal);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucApiUsers.SetPageSize", "", "");
                        MessageBox.Show("Error querying for the ucApiUsers page size", "ucApiUsers.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucApiUsers.SetPageSize", ex);
                MessageBox.Show(ex.Message, "ucApiUsers.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ************************************************************************************* //
        // Public Methods ********************************************************************* //
        // ************************************************************************************* //


        /// <summary>
        /// Batch of methods to get the count of total rows and a page of search results
        /// </summary>
        public void Search()
        {
            string countMethod = "APIUser.GetCount.base";
            string searchMethod = "APIUser.GetSearch.base";

            try
            {
                Dictionary<string, string> searchParams = new Dictionary<string, string>();
                searchParams.Add(CommonFields.PageNum, PageNum.ToString());
                searchParams.Add(CommonFields.SortOrder, tscmbSortOrder.Text);
                searchParams.Add(CommonFields.rec_state, SearchState);
                searchParams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                switch (tscmbSearchCol.Text)
                {
                    case APIUserFields.UserName:
                        searchParams.Add(APIUserFields.UserName, tstbxSearchVal.Text);
                        break;

                    case APIUserFields.FirstName:
                        searchParams.Add(APIUserFields.FirstName, tstbxSearchVal.Text);
                        break;

                    case APIUserFields.LastName:
                        searchParams.Add(APIUserFields.LastName, tstbxSearchVal.Text);
                        break;
                }

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
                    _main.SetMetrics(2, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "DGP", "ucApiUser");

                    if (respinfo.Auth.ToUpper() == AuthState.OK)
                    {
                        ResInfo count = msgUtil.GetResult(countMethod + "_" + MethReturn.Default, methresults);

                        if (count != null && count.RCode.ToUpper() == APIResult.OK)
                        {
                            TotalRows = Convert.ToInt32(count.RVal);
                            SetTotalPages();

                            ResInfo search = msgUtil.GetResult(searchMethod + "_" + MethReturn.Default, methresults);

                            if (search.RCode.ToUpper() == APIResult.OK || search.RCode.ToUpper() == APIResult.Empty)
                            {
                                if (search.DType == APIData.DataTable)
                                {
                                    // convert xml to datatable
                                    CmnUtil cmnUtil = new CmnUtil();
                                    DataTable userList = cmnUtil.XmlToTable(search.RVal);

                                    if (userList.Rows.Count > 0)
                                    {
                                        SetPageLabel(PageNum + 1, (int)TotalPages);

                                        dgvApiUsers.DataSource = userList.DefaultView;
                                        dgvApiUsers.Columns["Email"].Width = 200;
                                    }
                                    else
                                    {
                                        dgvApiUsers.DataSource = null;
                                    }
                                }
                                else
                                {
                                    dgvApiUsers.DataSource = null;
                                }
                            }
                            else
                            {
                                dgvApiUsers.DataSource = null;
                                MessageBox.Show(search.RCode + " : " + search.RVal, "APIUser Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dgvApiUsers.DataSource = null;
                            MessageBox.Show(count.RCode + " : " + count.RVal, "APIUser Count", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // show error message
                        string errmsg = "The following error occurred: " + respinfo.Auth + " - " + respinfo.Info;
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucApiUser.GetSearch", "Authentication error.", errmsg);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucApiUser.GetSearch", ex);
                MessageBox.Show(ex.Message, "ucApiUser.GetSearch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvApiUsers.DataSource = null;
            _main.SetMetrics(0, 0, 0, "", "");
        }

        // ************************************************************************************* //
        // Event Handlers ********************************************************************** //
        // ************************************************************************************* //

        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            tscmbSearchCol.SelectedIndex = 0;
            tscmbSearchCol.Text = "";
            tstbxSearchVal.Text = "";
            ClearPageInfo();
        }

        private void tscmbSearchCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPageInfo();
        }

        private void tscmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPageInfo();
        }

        private void tscmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPageInfo();
            SearchState = tscmbState.Text;
            if (SearchState == RecState.Deleted)
            {
                recoverDeletedMenuItem.Enabled = true;
            }
            else
            {
                recoverDeletedMenuItem.Enabled = false;
            }
        }

        private void tsbtnSearch_Click(object sender, System.EventArgs e)
        {
            Search();
        }

        private void txbtnNew_Click(object sender, System.EventArgs e)
        {
            frmEditUser editUser = new frmEditUser(_main, this, "");
            editUser.ShowDialog();
        }

        private void tsbtnFirst_Click(object sender, EventArgs e)
        {
            PageNum = 0;
            Search();
        }

        private void tsbtnPrev_Click(object sender, EventArgs e)
        {
            if (PageNum > 0) PageNum--;
            Search();
        }

        private void tsbtnNext_Click(object sender, EventArgs e)
        {
            if (PageNum < TotalPages) PageNum++;
            Search();
        }

        private void tsbtnLast_Click(object sender, EventArgs e)
        {
            PageNum = Convert.ToInt32(TotalPages - 1);
            Search();
        }

        private void dgvApiUsers_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvApiUsers.Rows[e.RowIndex].Selected = true;
                GID = dgvApiUsers.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
                _username = dgvApiUsers.SelectedRows[0].Cells[APIUserFields.UserName].Value.ToString();
                _firstname = dgvApiUsers.SelectedRows[0].Cells[APIUserFields.FirstName].Value.ToString();
                _lastname = dgvApiUsers.SelectedRows[0].Cells[APIUserFields.LastName].Value.ToString();
            }
        }
  
        // ************************************************************************************* //
        // Context Menu ************************************************************************ //
        // ************************************************************************************* //

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            frmEditUser editUser = new frmEditUser(_main, this, GID);
            editUser.ShowDialog();
        }

        private void userRolesMenuItem_Click(object sender, EventArgs e)
        {
            frmUserRoles userRoles = new frmUserRoles(_main, this, GID, _username);
            userRoles.ShowDialog();
        }

        private void readAccessMenuItem_Click(object sender, EventArgs e)
        {
            frmUserGroups userGroup = new frmUserGroups(_main, this, GID, _username);
            userGroup.ShowDialog();
        }

        private void userMethodsMenuItem_Click(object sender, EventArgs e)
        {
            frmUserMethods userMethods = new frmUserMethods(_main, this, GID, _username);
            userMethods.ShowDialog();
        }

        private void adminChangePasswordMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword adminChangePassword = new frmChangePassword(_main, _username);
            adminChangePassword.ShowDialog();
        }

        private void recoverDeletedMenuItem_Click(object sender, EventArgs e)
        {
            // read values from selected row in grid
            string recgid = dgvApiUsers.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
            string rowid = dgvApiUsers.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();

            // call process method to recover edited record
            Dictionary<string, string> methparams = new Dictionary<string, string>();
            methparams.Add("Action", ReplicaAction.Recover);
            methparams.Add(CommonFields.rec_gid, recgid);
            methparams.Add(CommonFields.row_id, rowid);

            MsgUtil msgUtil = new MsgUtil();
            string reqID = msgUtil.GetNewGID();
            string methList = msgUtil.CreateXMLMethod("APIUser.Recover.base", methparams);
            SOAPMsgUtil soapmsg = new SOAPMsgUtil();
            string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

            if (respmsg != "")
            {
                RespInfo respinfo = new RespInfo();
                Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                ResInfo methresult = msgUtil.GetResult("APIUser.Recover.base_DEFAULT", methresults);

                if (methresult.RCode.ToUpper() == APIResult.OK)
                {
                    // update the grid
                    Search();
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

        private void viewHistoryMenuItem_Click(object sender, EventArgs e)
        {
            frmUserHist userHist = new frmUserHist(_main, this, GID, _username);
            userHist.ShowDialog();
        }
    }
}
