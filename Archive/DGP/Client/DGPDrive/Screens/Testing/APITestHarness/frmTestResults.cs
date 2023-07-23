using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

using CliUtil;

namespace DGPDrive.Screens.Testing
{
    public partial class frmTestResults : Form
    {
        string _uploaded;
        string _evalinfo;
        string _reqmsg;
        string _respmsg;

        public frmTestResults(DataTable testResults, bool batchRun, int uploaded)
        {
            InitializeComponent();

            _uploaded = uploaded.ToString();
            PopulateGrid(testResults, batchRun);
        }

        private void PopulateGrid(DataTable testResults, bool batch)
        {
            dgvTestResults.DataSource = null;

            if (testResults.Rows.Count > 0)
            {
                dgvTestResults.DataSource = testResults;

                if (batch)
                {
                    viewReqMenuItem.Enabled = false;
                    viewRespMenuItem.Enabled = false;
                }
                else
                {
                    viewReqMenuItem.Enabled = true;
                    viewRespMenuItem.Enabled = true;
                }
            }

            tslblUploaded.Text = _uploaded;
        }

        private void tsbtnSaveCSV_Click(object sender, System.EventArgs e)
        {
            CommonOpenFileDialog locdialog = new CommonOpenFileDialog();
            locdialog.Title = "Save TestResult CSV File";
            locdialog.InitialDirectory = Environment.CurrentDirectory;
            locdialog.Filters.Add(new CommonFileDialogFilter("TestResult Files", "*.csv"));
            locdialog.DefaultExtension = ".csv";
            locdialog.EnsurePathExists = true;
            locdialog.EnsureValidNames = true;
            locdialog.IsFolderPicker = false;
            locdialog.Multiselect = false;

            if (locdialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                using (TextWriter tw = File.CreateText(locdialog.FileName))
                {
                    DataTable dtbl = dgvTestResults.DataSource as DataTable;
                    TestUtil testUtil = new TestUtil();

                    bool success = testUtil.WriteDataTable(dtbl, tw, true);
                }
            }
        }

        private void evalInfoMenuItem_Click(object sender, System.EventArgs e)
        {
            string inputxml = "<?xml version=\"1.0\"?>\n" + _evalinfo;
            frmViewXML viewXML = new frmViewXML(inputxml, "EVALINFO");
            viewXML.ShowDialog();
        }

        private void viewReqMenuItem_Click(object sender, System.EventArgs e)
        {
            string inputxml = "<?xml version=\"1.0\"?>\n" + _reqmsg;
            frmViewXML viewXML = new frmViewXML(inputxml, "REQUEST");
            viewXML.ShowDialog();
        }

        private void viewRespMenuItem_Click(object sender, System.EventArgs e)
        {
            string inputxml = "<?xml version=\"1.0\"?>\n" + _respmsg;
            frmViewXML viewXML = new frmViewXML(inputxml, "RESPONSE");
            viewXML.ShowDialog();
        }

        private void dgvTestResults_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int passed = 0;
            int failed = 0;
            double clienttotal = 0;
            double networktotal = 0;
            double servertotal = 0;

            string tmpclient = "";
            string tmpnetwork = "";
            string tmpserver = "";
            foreach (DataGridViewRow dgvr in dgvTestResults.Rows)
            {
                if (dgvr.Cells["Eval"].Value.ToString() == "PASS")
                {
                    passed++;
                    dgvr.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    tmpclient = dgvr.Cells["ClientMS"].Value.ToString();
                    tmpnetwork = dgvr.Cells["NetworkMS"].Value.ToString();
                    tmpserver = dgvr.Cells["ServerMS"].Value.ToString();
                    clienttotal += Convert.ToDouble(tmpclient);
                    networktotal += Convert.ToDouble(tmpnetwork);
                    servertotal += Convert.ToDouble(tmpserver);
                }
                else
                {
                    failed++;
                    dgvr.DefaultCellStyle.BackColor = Color.DarkSalmon;
                }
            }

            tslblMethCount.Text = dgvTestResults.Rows.Count.ToString();
            tslblPassed.Text = passed.ToString();
            tslblFailed.Text = failed.ToString();
            tslblClient.Text = Math.Round((clienttotal / dgvTestResults.Rows.Count), 2).ToString();
            tslblServer.Text = Math.Round((servertotal / dgvTestResults.Rows.Count), 2).ToString();

            dgvTestResults.Rows[0].Selected = false;
        }

        private void dgvTestResults_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvTestResults.ClearSelection();
                this.dgvTestResults.Rows[e.RowIndex].Selected = true;

                DataTable table = dgvTestResults.DataSource as DataTable;
                DataRow row = table.NewRow();
                row = ((DataRowView)dgvTestResults.SelectedRows[0].DataBoundItem).Row;

                _evalinfo = row["EvalInfo"].ToString();
                _reqmsg = row["ReqXML"].ToString();
                _respmsg = row["RespXML"].ToString();
            }
        }
        
    }
}
