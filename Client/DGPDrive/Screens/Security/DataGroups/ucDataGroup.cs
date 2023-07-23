
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
    public partial class ucDataGroup : UserControl, IDriveUC
    {
        MainForm _main;

        public decimal TotalRows { get; set; }
        public decimal PageSize { get; set; }
        public decimal TotalPages { get; set; }
        public int PageNum { get; set; }
        public string SearchState { get; set; }
        public string GID { get; set; }

        string _groupname;

        public ucDataGroup(MainForm main)
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucDataGroup", ex);
                MessageBox.Show(ex.Message, "ucDataGroup", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string methList = msgUtil.CreateXMLMethod("DataGroup.GetPageSize.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("DataGroup.GetPageSize.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        PageSize = Convert.ToDecimal(methresult.RVal);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucDataGroup.SetPageSize", "", "");
                        MessageBox.Show("Error querying for the ucDataGroup page size", "ucDataGroup.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucDataGroup.SetPageSize", ex);
                MessageBox.Show(ex.Message, "ucDataGroup.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                Dictionary<string, string> searchParams = new Dictionary<string, string>();
                searchParams.Add(CommonFields.PageNum, PageNum.ToString());
                searchParams.Add(CommonFields.SortOrder, tscmbSortOrder.Text);
                searchParams.Add(CommonFields.rec_state, SearchState);
                searchParams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                switch (tscmbSearchCol.Text)
                {
                    case "GroupName":
                        searchParams.Add("GroupName", tstbxSearchVal.Text);
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();
                string methlist = msgUtil.CreateXMLMethod("DataGroup.GetCount.base", searchParams);
                methlist += msgUtil.CreateXMLMethod("DataGroup.GetSearch.base", searchParams);
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
                    _main.SetMetrics(2, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "DGP", "ucDataGroup");

                    if (respinfo.Auth.ToUpper() == AuthState.OK)
                    {
                        ResInfo count = msgUtil.GetResult("DataGroup.GetCount.base_DEFAULT", methresults);

                        if (count != null && count.RCode.ToUpper() == APIResult.OK)
                        {
                            TotalRows = Convert.ToInt32(count.RVal);
                            SetTotalPages();

                            ResInfo search = msgUtil.GetResult("DataGroup.GetSearch.base_DEFAULT", methresults);

                            if (search.RCode.ToUpper() == APIResult.OK || search.RCode.ToUpper() == APIResult.Empty)
                            {
                                if (search.DType == APIData.DataTable)
                                {
                                    // convert xml to datatable
                                    CmnUtil cmnUtil = new CmnUtil();
                                    DataTable methList = cmnUtil.XmlToTable(search.RVal);

                                    if (methList.Rows.Count > 0)
                                    {
                                        SetPageLabel(PageNum + 1, (int)TotalPages);

                                        dgvDataGroups.DataSource = methList.DefaultView;
                                        dgvDataGroups.Columns["GroupName"].FillWeight = 25;
                                        dgvDataGroups.Columns["GroupDescrip"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                        dgvDataGroups.Columns["rec_gid"].FillWeight = 25;
                                    }
                                    else
                                    {
                                        dgvDataGroups.DataSource = null;
                                    }
                                }
                                else
                                {
                                    dgvDataGroups.DataSource = null;
                                }
                            }
                            else
                            {
                                dgvDataGroups.DataSource = null;
                                MessageBox.Show(search.RCode + " : " + search.RVal, "DataGroup.GetSearch.base", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dgvDataGroups.DataSource = null;
                            MessageBox.Show(count.RCode + " : " + count.RVal, "DataGroup.GetCount.base", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // show error message
                        string errmsg = "The following error occurred: " + respinfo.Auth + " - " + respinfo.Info;
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucDataGroup.GetSearch", "Authentication error.", errmsg);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucDataGroup.GetSearch", ex);
                MessageBox.Show(ex.Message, "ucDataGroup.GetSearch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvDataGroups.DataSource = null;
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
            frmEditGroup editGroup = new frmEditGroup(_main, this, "");
            editGroup.ShowDialog();
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

        private void dgvDataGroups_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvDataGroups.Rows[e.RowIndex].Selected = true;
                GID = dgvDataGroups.SelectedRows[0].Cells["rec_gid"].Value.ToString();
                _groupname = dgvDataGroups.SelectedRows[0].Cells["GroupName"].Value.ToString();
            }
        }

        // ************************************************************************************* //
        // Context Menu ************************************************************************ //
        // ************************************************************************************* //

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            frmEditGroup editGroup = new frmEditGroup(_main, this, GID);
            editGroup.ShowDialog();
        }

        private void recoverDeletedMenuItem_Click(object sender, EventArgs e)
        {
            // read values from selected row in grid
            string recgid = dgvDataGroups.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
            string rowid = dgvDataGroups.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();

            // call process method to recover edited record
            Dictionary<string, string> methparams = new Dictionary<string, string>();
            methparams.Add("Action", ReplicaAction.Recover);
            methparams.Add(CommonFields.rec_gid, recgid);
            methparams.Add(CommonFields.row_id, rowid);

            MsgUtil msgUtil = new MsgUtil();
            string reqID = msgUtil.GetNewGID();
            string methList = msgUtil.CreateXMLMethod("DataGroup.Recover.base", methparams);
            SOAPMsgUtil soapmsg = new SOAPMsgUtil();
            string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

            if (respmsg != "")
            {
                RespInfo respinfo = new RespInfo();
                Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                ResInfo methresult = msgUtil.GetResult("DataGroup.Recover.base_DEFAULT", methresults);

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

        }

        private void viewHistoryMenuItem_Click(object sender, EventArgs e)
        {
            frmGroupHist groupHist = new frmGroupHist(_main, this, GID, _groupname);
            groupHist.ShowDialog();
        }
    }
}
