using System;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;

using CliUtil;

namespace DGPDrive.Screens.FileStore
{
    public partial class frmFileHist : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        ucFileStore _parent;
        string _filegid;
        string _filename;

        public frmFileHist(HttpClient httpClient, MainForm mainForm, ucFileStore parentForm, string fileGID, string fileName)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _parent = parentForm;
                _filegid = fileGID;
                _filename = fileName;

                lblFileName.Text = _filename;

                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(CommonFields.SchemaFlag, "true");
                methparams.Add(FileFields.FileGID, _filegid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("File.GetHistory.base", methparams);
                string respmsg = msgUtil.ApiMethHelper(methList,
                                                            reqID,
                                                            _main.UserName,
                                                            _main.Password,
                                                            httpClient,
                                                            _main.SvcUrl);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("File.GetHistory.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                        if (methtable.Rows.Count > 0)
                        {
                            dgvFileHistory.DataSource = methtable;
                            if (_parent.SearchState == RecState.Active) btnRecoverSelected.Enabled = true;
                        }
                        else
                        {
                            dgvFileHistory.DataSource = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmFileHist", ex);
                MessageBox.Show(ex.Message, "frmFileHist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecoverSelected_Click(object sender, EventArgs e)
        {
            // read values from selected row in grid
            string recgid = dgvFileHistory.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
            string rowid = dgvFileHistory.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();

            // call process method to recover edited record
            Dictionary<string, string> methparams = new Dictionary<string, string>();
            methparams.Add("Action", ReplicaAction.Update);
            methparams.Add(CommonFields.rec_gid, recgid);
            methparams.Add(CommonFields.row_id, rowid);

            MsgUtil msgUtil = new MsgUtil();
            string reqID = msgUtil.GetNewGID();
            string methList = msgUtil.CreateXMLMethod("File.Recover.base", methparams);
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

                ResInfo methresult = msgUtil.GetResult("File.Recover.base_DEFAULT", methresults);

                if (methresult.RCode.ToUpper() == APIResult.OK)
                {
                    // update the parent form and then close the dialog
                    _parent.FileSearch();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownloadSelected_Click(object sender, EventArgs e)
        {
            // get values from selected grid record
            string row_id = dgvFileHistory.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();
            string _row_ms = dgvFileHistory.SelectedRows[0].Cells[CommonFields.row_ms].Value.ToString();
            string _fileext = dgvFileHistory.SelectedRows[0].Cells[FileFields.FileExt].Value.ToString();
            string _filehash = dgvFileHistory.SelectedRows[0].Cells[FileFields.FileHash].Value.ToString();
            string _version = dgvFileHistory.SelectedRows[0].Cells[FileFields.FileVersion].Value.ToString();
            string _shardname = dgvFileHistory.SelectedRows[0].Cells[FileFields.ShardName].Value.ToString();
            string _filesize = dgvFileHistory.SelectedRows[0].Cells[FileFields.FileSize].Value.ToString();
            string _segsize = dgvFileHistory.SelectedRows[0].Cells[FileFields.SegSize].Value.ToString();
            string _totalseg = dgvFileHistory.SelectedRows[0].Cells[FileFields.TotalSeg].Value.ToString();
            DateTime _filedate = Convert.ToDateTime(dgvFileHistory.SelectedRows[0].Cells[FileFields.FileDate].Value);

            // instantiate the file download form with the values of the selected record
            frmFileDownload fileDownload = new frmFileDownload(_httpClient, _main, _parent, _shardname, _filegid, _version, _filename, _fileext, _filedate, _filesize, _filehash, _segsize, _totalseg);
            fileDownload.ShowDialog();
        }

        private void dgvFileHistory_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgvFileHistory.Rows[e.RowIndex].Selected = true;
        }
    }
}
