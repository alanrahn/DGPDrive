using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Diagnostics;

using CliUtil;

namespace DGPDrive.Screens.Testing
{
    public partial class frmDGPErrorLog : Form
    {
        MainForm _main;
        HttpClient _httpClient;

        public decimal TotalRows { get; set; }
        public decimal PageSize { get; set; }
        public decimal TotalPages { get; set; }
        public int PageNum { get; set; }
        public string SearchState { get; set; }
        public string GID { get; set; }

        string _row_id;

        public frmDGPErrorLog(HttpClient httpClient, MainForm mainForm)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
                _httpClient = httpClient;
                //_main.SetMetrics(0, 0, 0, "", "");

                tscmbSearchCol.SelectedIndex = -1;

                SetPageSize();
                PageNum = 0;
                SetPageLabel(0, 0);
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmDGPErrors", ex);
                MessageBox.Show(ex.Message, "frmDGPErrors", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string methList = msgUtil.CreateXMLMethod("DGPError.GetPageSize.base", methparams);

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

                    ResInfo methresult = msgUtil.GetResult("DGPError.GetPageSize.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        PageSize = Convert.ToDecimal(methresult.RVal);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmDGPErrors.SetPageSize", "PageSize Error", "Error querying for the frmDGPErrors page size.");
                        MessageBox.Show("Error querying for the frmDGPErrors page size", "frmDGPErrors.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmDGPErrors.SetPageSize", ex);
                MessageBox.Show(ex.Message, "frmDGPErrors.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Search()
        {
            string countMethod = "DGPError.GetCount.base";
            string searchMethod = "DGPError.GetSearch.base";

            try
            {
                Dictionary<string, string> searchParams = new Dictionary<string, string>();
                searchParams.Add(CommonFields.PageNum, PageNum.ToString());
                searchParams.Add(CommonFields.rec_state, SearchState);
                searchParams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                switch (tscmbSearchCol.Text)
                {
                    case LogFields.AppName:
                        searchParams.Add(LogFields.AppName, tstbxSearchVal.Text);
                        break;

                    case LogFields.ClassName:
                        searchParams.Add(LogFields.ClassName, tstbxSearchVal.Text);
                        break;

                    case LogFields.ErrLevel:
                        searchParams.Add(LogFields.ErrLevel, tstbxSearchVal.Text);
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();
                string methlist = msgUtil.CreateXMLMethod(countMethod, searchParams);
                methlist += msgUtil.CreateXMLMethod(searchMethod, searchParams);
                string reqID = msgUtil.GetNewGID();

                Stopwatch sw = new Stopwatch();
                sw.Start();
                string respmsg = msgUtil.ApiMethHelper(methlist,
                                                        reqID,
                                                        _main.UserName,
                                                        _main.Password,
                                                        _httpClient,
                                                        _main.SvcUrl);

                sw.Stop();

                if (respmsg != "")
                {
                    // read response message results
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);
                    _main.SetMetrics(2, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "Lattice", "frmDGPErrorLog");

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

                                        dgvDGPErrors.DataSource = methList.DefaultView;
                                        dgvDGPErrors.Columns[LogFields.LogTime].FillWeight = 25;
                                        dgvDGPErrors.Columns[LogFields.LogTime].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss.fff";
                                    }
                                    else
                                    {
                                        dgvDGPErrors.DataSource = null;
                                    }
                                }
                                else
                                {
                                    dgvDGPErrors.DataSource = null;
                                }
                            }
                            else
                            {
                                dgvDGPErrors.DataSource = null;
                                MessageBox.Show(search.RCode + " : " + search.RVal, "frmDGPErrors Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dgvDGPErrors.DataSource = null;
                            MessageBox.Show(count.RCode + " : " + count.RVal, "frmDGPErrors Count", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // show error message
                        string errmsg = "The following error occurred: " + respinfo.Auth + " - " + respinfo.Info;
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmDGPErrors.GetSearch", "Authentication error.", errmsg);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmDGPErrors.GetSearch", ex);
                MessageBox.Show(ex.Message, "frmDGPErrors.GetSearch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvDGPErrors.DataSource = null;
            //_main.SetMetrics(0, 0, 0, "", "");
        }

        // ************************************************************************************* //
        // Event Handlers ********************************************************************** //
        // ************************************************************************************* //

        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            tscmbSearchCol.SelectedIndex = -1;
            tstbxSearchVal.Text = "";
            rtbxErrDetail.Text = "";
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
            rtbxErrDetail.Text = "";
            Search();
        }

        private void tsbtnFirst_Click(object sender, EventArgs e)
        {
            PageNum = 0;
            rtbxErrDetail.Text = "";
            Search();
        }

        private void tsbtnPrev_Click(object sender, EventArgs e)
        {
            if (PageNum > 0) PageNum--;
            rtbxErrDetail.Text = "";
            Search();
        }

        private void tsbtnNext_Click(object sender, EventArgs e)
        {
            if (PageNum < TotalPages) PageNum++;
            rtbxErrDetail.Text = "";
            Search();
        }

        private void tsbtnLast_Click(object sender, EventArgs e)
        {
            PageNum = Convert.ToInt32(TotalPages - 1);
            rtbxErrDetail.Text = "";
            Search();
        }

        private void dgvDGPErrors_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvDGPErrors.Rows[e.RowIndex].Selected = true;
                GID = dgvDGPErrors.SelectedRows[0].Cells[CommonFields.row_gid].Value.ToString();
                _row_id = dgvDGPErrors.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();
            }
        }

        private void dgvDGPErrors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // populate detail text box
            this.dgvDGPErrors.Rows[e.RowIndex].Selected = true;
            GID = dgvDGPErrors.SelectedRows[0].Cells[CommonFields.row_gid].Value.ToString();

            try
            {
                // lookup method data using global id value
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(CommonFields.row_gid, GID);
                methparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("DGPError.GetErrData.base", methparams);
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

                    ResInfo methresult = msgUtil.GetResult("DGPError.GetErrData.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        rtbxErrDetail.Text = methresult.RVal;
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmDGPErrors.dgvDGPErrors_CellClick", "ErrData Error", "Error querying for the frmDGPErrors ErrData content.");
                        MessageBox.Show("Error querying for the frmDGPErrors ErrData content", "frmDGPErrors.dgvDGPErrors_CellClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmDGPErrors.dgvDGPErrors_CellClick", ex);
                MessageBox.Show(ex.Message, "frmDGPErrors.dgvDGPErrors_CellClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
