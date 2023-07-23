using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;

using ApiUtil;
using ApiUtil.DataClasses;
using System.Diagnostics;

namespace DGPDrive.Screens.Testing
{
    public partial class frmDGPDriveMetrics : Form
    {
        MainForm _main;

        public decimal TotalRows { get; set; }
        public decimal PageSize { get; set; }
        public decimal TotalPages { get; set; }
        public int PageNum { get; set; }
        public string SearchState { get; set; }
        public string GID { get; set; }

        string _row_id;

        public frmDGPDriveMetrics(MainForm mainForm)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
                //_main.SetMetrics(0, 0, 0, "", "");

                tscmbSearchCol.SelectedIndex = 0;
                tscmbSortOrder.SelectedIndex = 0;

                SetPageSize();
                PageNum = 0;
                SetPageLabel(0, 0);
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmDGPMetrics", ex);
                MessageBox.Show(ex.Message, "frmDGPMetrics", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string methList = msgUtil.CreateXMLMethod("DGPMetrics.GetPageSize.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("DGPMetrics.GetPageSize.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        PageSize = Convert.ToDecimal(methresult.RVal);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmDGPMetrics.SetPageSize", "PageSize Error", "Error querying for the frmDGPMetrics page size.");
                        MessageBox.Show("Error querying for the frmDGPMetrics page size", "frmDGPMetrics.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmDGPMetrics.SetPageSize", ex);
                MessageBox.Show(ex.Message, "frmDGPMetrics.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Search()
        {
            string countMethod = "DGPMetrics.GetCount.base";
            string searchMethod = "DGPMetrics.GetSearch.base";

            try
            {
                Dictionary<string, string> searchParams = new Dictionary<string, string>();
                searchParams.Add(CommonFields.PageNum, PageNum.ToString());
                searchParams.Add(CommonFields.SortOrder, tscmbSortOrder.Text);
                searchParams.Add(CommonFields.rec_state, SearchState);
                searchParams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                switch (tscmbSearchCol.Text)
                {
                    case LogFields.AppName:
                        searchParams.Add(LogFields.AppName, tstbxSearchVal.Text);
                        break;

                    case LogFields.FormName:
                        searchParams.Add(LogFields.FormName, tstbxSearchVal.Text);
                        break;

                    case LogFields.WebSvcName:
                        searchParams.Add(LogFields.WebSvcName, tstbxSearchVal.Text);
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
                    _main.SetMetrics(2, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "DGP", "frmLatticMetrics");

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
                                    DataTable methList = cmnUtil.XmlToTable(search.RVal);

                                    if (methList.Rows.Count > 0)
                                    {
                                        SetPageLabel(PageNum + 1, (int)TotalPages);

                                        dgvDGPMetrics.DataSource = methList.DefaultView;
                                        dgvDGPMetrics.Columns[WorkFields.ServerTime].FillWeight = 25;
                                        dgvDGPMetrics.Columns[WorkFields.ServerTime].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss.fff";
                                    }
                                    else
                                    {
                                        dgvDGPMetrics.DataSource = null;
                                    }
                                }
                                else
                                {
                                    dgvDGPMetrics.DataSource = null;
                                }
                            }
                            else
                            {
                                dgvDGPMetrics.DataSource = null;
                                MessageBox.Show(search.RCode + " : " + search.RVal, "frmDGPMetrics Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dgvDGPMetrics.DataSource = null;
                            MessageBox.Show(count.RCode + " : " + count.RVal, "frmDGPMetrics Count", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // show error message
                        string errmsg = "The following error occurred: " + respinfo.Auth + " - " + respinfo.Info;
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmDGPMetrics.GetSearch", "Authentication error.", errmsg);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmDGPMetrics.GetSearch", ex);
                MessageBox.Show(ex.Message, "frmDGPMetrics.GetSearch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvDGPMetrics.DataSource = null;
            //_main.SetMetrics(0, 0, 0, "", "");
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

        private void tsbtnSearch_Click(object sender, System.EventArgs e)
        {
            Search();
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

        private void dgvDGPErrors_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvDGPMetrics.Rows[e.RowIndex].Selected = true;
                GID = dgvDGPMetrics.SelectedRows[0].Cells[CommonFields.row_gid].Value.ToString();
                _row_id = dgvDGPMetrics.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();
            }
        }

    }
}
