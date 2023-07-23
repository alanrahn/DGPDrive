
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;
using System.Data;
using System.Diagnostics;

using CliUtil;

namespace DGPDrive.Screens.Configuration
{
    public partial class ucGeneralWork : UserControl, ILatticeUC
    {
        MainForm _main;
        HttpClient _httpClient;

        public decimal TotalRows { get; set; }
        public decimal PageSize { get; set; }
        public decimal TotalPages { get; set; }
        public int PageNum { get; set; }
        public string SearchState { get; set; }
        public string GID { get; set; }


        public ucGeneralWork(MainForm main, HttpClient httpClient)
        {
            InitializeComponent();

            try
            {
                _main = main;
                _httpClient = httpClient;
                _main.SetMetrics(0, 0, 0, "", "");

                tscmbPageSize.SelectedIndex = 0;
                SetPageSize(Convert.ToDecimal(tscmbPageSize.Text));
                PageNum = 0;
                SetPageLabel(0,0);
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucGeneralWork", ex);
                MessageBox.Show(ex.Message, "ucGeneralWork", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string methList = msgUtil.CreateXMLMethod("GeneralWork.GetPageSize.base", methparams);
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

                    ResInfo methresult = msgUtil.GetResult("GeneralWork.GetByID.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        PageSize = Convert.ToDecimal(methresult.RVal);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucGeneralWork.SetPageSize", "Page Size error", methresult.RVal);
                        MessageBox.Show("Error querying for the ucGeneralWork page size", "ucGeneralWork.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucGeneralWork.SetPageSize", ex);
                MessageBox.Show(ex.Message, "ucGeneralWork.SetPageSize", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string methlist = msgUtil.CreateXMLMethod("GeneralWork.GetCount.base", searchParams);
                methlist += msgUtil.CreateXMLMethod("GeneralWork.GetSearch.base", searchParams);
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

                    _main.SetMetrics(2, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "Lattice", "ucGeneralWork");

                    if (respinfo.Auth.ToUpper() == AuthState.OK)
                    {
                        ResInfo count = msgUtil.GetResult("GeneralWork.GetCount.base_DEFAULT", methresults);

                        if (count != null && count.RCode.ToUpper() == APIResult.OK)
                        {
                            TotalRows = Convert.ToInt32(count.RVal);
                            SetTotalPages();

                            ResInfo search = msgUtil.GetResult("GeneralWork.GetSearch.base_DEFAULT", methresults);

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

                                        dgvGeneralWork.DataSource = methList.DefaultView;
                                        dgvGeneralWork.Columns[WorkFields.SchemaTable].FillWeight = 15;
                                        dgvGeneralWork.Columns[WorkFields.SrcURL].FillWeight = 15;
                                        dgvGeneralWork.Columns[WorkFields.SrcMethod].FillWeight = 15;
                                        dgvGeneralWork.Columns[WorkFields.DestURL].FillWeight = 15;
                                        dgvGeneralWork.Columns[WorkFields.DestMethod].FillWeight = 15;
                                    }
                                    else
                                    {
                                        dgvGeneralWork.DataSource = null;
                                    }
                                }
                                else
                                {
                                    dgvGeneralWork.DataSource = null;
                                }
                            }
                            else
                            {
                                dgvGeneralWork.DataSource = null;
                                MessageBox.Show(search.RCode + " : " + search.RVal, "ucGeneralWork.GetSearch.base", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dgvGeneralWork.DataSource = null;
                            MessageBox.Show(count.RCode + " : " + count.RVal, "ucGeneralWork.GetCount.base", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // show error message
                        string errmsg = "The following error occurred: " + respinfo.Auth + " - " + respinfo.Info;
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucGeneralWork.GetSearch", "Authentication error.", errmsg);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucGeneralWork.GetSearch", ex);
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
            dgvGeneralWork.DataSource = null;
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
            frmEditGeneral editGeneral = new frmEditGeneral(_httpClient, _main, this, "");
            editGeneral.ShowDialog();
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

        private void dgvGeneralWork_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvGeneralWork.Rows[e.RowIndex].Selected = true;
                GID = dgvGeneralWork.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
            }
        }

        // ************************************************************************************* //
        // Context Menu ************************************************************************ //
        // ************************************************************************************* //

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            frmEditGeneral editGeneral = new frmEditGeneral(_httpClient, _main, this, GID);
            editGeneral.ShowDialog();
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
                string methList = msgUtil.CreateXMLMethod("GeneralWork.Clone.base", methparams);
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

                    ResInfo methresult = msgUtil.GetResult("GeneralWork.Clone.base_DEFAULT", methresults);

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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmEditReplica:btnDelete_Click", ex);
                MessageBox.Show(ex.Message, "frmEditReplica:btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
