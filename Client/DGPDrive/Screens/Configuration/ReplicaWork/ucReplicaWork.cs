
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;
using System.Data;
using System.Diagnostics;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.Configuration
{
    public partial class ucReplicaWork : UserControl, IDriveUC
    {
        MainForm _main;

        public decimal TotalRows { get; set; }
        public decimal PageSize { get; set; }
        public decimal TotalPages { get; set; }
        public int PageNum { get; set; }
        public string SearchState { get; set; }
        public string GID { get; set; }

        string _schematable;

        public ucReplicaWork(MainForm main)
        {
            InitializeComponent();

            try
            {
                _main = main;
                _main.SetMetrics(0, 0, 0, "", "");

                tscmbPageSize.SelectedIndex = 0;
                SetPageSize(Convert.ToDecimal(tscmbPageSize.Text));
                PageNum = 0;
                SetPageLabel(0,0);
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucReplicaWork", ex);
                MessageBox.Show(ex.Message, "ucReplicaWork", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string methList = msgUtil.CreateXMLMethod("ReplicaWork.GetPageSize.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("ReplicaWork.GetPageSize.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        PageSize = Convert.ToDecimal(methresult.RVal);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucReplicaWork.SetPageSize", "", "");
                        MessageBox.Show("Error querying for the ucReplicaWork page size", "ucReplicaWork.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucReplicaWork.SetPageSize", ex);
                MessageBox.Show(ex.Message, "ucFucReplicaWorkileStore.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ************************************************************************************* //
        // Public Methods ********************************************************************* //
        // ************************************************************************************* //


        /// <summary>
        /// Query for paginated LatestWork records
        /// </summary>
        public void Search()
        {
            try
            {
                Dictionary<string, string> searchParams = new Dictionary<string, string>();
                searchParams.Add(CommonFields.PageNum, PageNum.ToString());
                searchParams.Add(CommonFields.PageSize, PageSize.ToString());
                searchParams.Add(CommonFields.rec_state, SearchState);
                searchParams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                if (tstbxSearchVal.Text != null && tstbxSearchVal.Text != "")
                {
                    searchParams.Add(WorkFields.SchemaTable, tstbxSearchVal.Text);
                }

                MsgUtil msgUtil = new MsgUtil();
                string methlist = msgUtil.CreateXMLMethod("ReplicaWork.GetCount.base", searchParams);
                methlist += msgUtil.CreateXMLMethod("ReplicaWork.GetSearch.base", searchParams);

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

                    _main.SetMetrics(2, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "DGP", "ucReplicaWork");

                    if (respinfo.Auth.ToUpper() == AuthState.OK)
                    {
                        ResInfo count = msgUtil.GetResult("ReplicaWork.GetCount.base_DEFAULT", methresults);

                        if (count != null && count.RCode.ToUpper() == APIResult.OK)
                        {
                            TotalRows = Convert.ToInt32(count.RVal);
                            SetTotalPages();

                            ResInfo search = msgUtil.GetResult("ReplicaWork.GetSearch.base_DEFAULT", methresults);

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

                                        dgvReplicaWork.DataSource = methList.DefaultView;
                                        dgvReplicaWork.Columns[WorkFields.SchemaTable].FillWeight = 15;
                                        dgvReplicaWork.Columns[WorkFields.SrcURL].FillWeight = 15;
                                        dgvReplicaWork.Columns[WorkFields.SrcMethod].FillWeight = 15;
                                        dgvReplicaWork.Columns[WorkFields.DestURL].FillWeight = 15;
                                        dgvReplicaWork.Columns[WorkFields.DestMethod].FillWeight = 15;
                                    }
                                    else
                                    {
                                        dgvReplicaWork.DataSource = null;
                                    }
                                }
                                else
                                {
                                    dgvReplicaWork.DataSource = null;
                                }
                            }
                            else
                            {
                                dgvReplicaWork.DataSource = null;
                                MessageBox.Show(search.RCode + " : " + search.RVal, "ucReplicaWork.GetSearch.base", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dgvReplicaWork.DataSource = null;
                            MessageBox.Show(count.RCode + " : " + count.RVal, "ucReplicaWork.GetCount.base", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // show error message
                        string errmsg = "The following error occurred: " + respinfo.Auth + " - " + respinfo.Info;
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucReplicaWork.GetSearch", "Authentication error.", errmsg);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucReplicaWork.GetSearch", ex);
                MessageBox.Show(ex.Message, "GetSearch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetPageSize(decimal pageSize)
        {
            PageSize = pageSize;
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
            dgvReplicaWork.DataSource = null;
            _main.SetMetrics(0, 0, 0, "", "");
        }

        // ************************************************************************************* //
        // Event Handlers ********************************************************************** //
        // ************************************************************************************* //

        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            tstbxSearchVal.Text = "";
            ClearPageInfo();
        }

        private void tscmbSearchCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPageInfo();
        }

        private void tscmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPageInfo();
            SetPageSize(Convert.ToDecimal(tscmbPageSize.Text));
        }

        private void tsbtnSearch_Click(object sender, System.EventArgs e)
        {
            Search();
        }

        private void txbtnNew_Click(object sender, System.EventArgs e)
        {
            frmEditReplica editReplica = new frmEditReplica(_main, this, "");
            editReplica.ShowDialog();
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

        private void dgvReplicaWork_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvReplicaWork.Rows[e.RowIndex].Selected = true;
                GID = dgvReplicaWork.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
                _schematable = dgvReplicaWork.SelectedRows[0].Cells[WorkFields.SchemaTable].Value.ToString();
            }
        }

        // ************************************************************************************* //
        // Context Menu ************************************************************************ //
        // ************************************************************************************* //

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            frmEditReplica editReplica = new frmEditReplica(_main, this, GID);
            editReplica.ShowDialog();
        }

        private void cloneMenuItem_Click(object sender, EventArgs e)
        {
            // create a copy of the selected record, with disabled runstate
            try
            {
                Dictionary<string, string> methparams = new Dictionary<string, string>();

                methparams.Add(CommonFields.rec_gid, GID);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("ReplicaWork.Clone.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("ReplicaWork.Clone.base_DEFAULT", methresults);

                    if (methresult.RCode.ToUpper() == APIResult.OK)
                    {
                        // update the parent form and then close the dialog
                        Search();
                    }
                    else
                    {
                        // error saving method info: display error message
                        MessageBox.Show(methresult.RVal, methresult.RCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditReplica:btnDelete_Click", ex);
                MessageBox.Show(ex.Message, "frmEditReplica:btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
