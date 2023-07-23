
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

using CliUtil;

namespace DGPDrive.Screens.Testing
{
    public partial class ucApiTestHarness : UserControl
    {
        MainForm _main;
        HttpClient _httpClient;

        string _testfilepath;
        bool _upload = false;

        public string TestFileDir { get; set; }

        public ucApiTestHarness(MainForm main, HttpClient httpClient)
        {
            InitializeComponent();

            try
            {
                _main = main;
                _httpClient = httpClient;
                _main.SetMetrics(0, 0, 0, "", "");

                ResetBar();

                TestFileDir = Environment.CurrentDirectory;
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucApiTestHarness", ex);
                MessageBox.Show(ex.Message, "ucApiTestHarness", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ************************************************************************************* //
        // Event Handlers ********************************************************************** //
        // ************************************************************************************* //

        private void tsbtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                CommonOpenFileDialog browsedialog = new CommonOpenFileDialog();
                browsedialog.Title = "Select Test File Directory";
                browsedialog.ResetUserSelections();
                browsedialog.InitialDirectory = TestFileDir;
                browsedialog.IsFolderPicker = true;

                if (browsedialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    tstbxDirPath.Text = browsedialog.FileName;
                    tstbxDirPath.ToolTipText = browsedialog.FileName;
                    TestFileDir = Path.GetDirectoryName(browsedialog.FileName);

                    TestUtil testUtil = new TestUtil();
                    DataTable batchFiles = testUtil.GetTestFileList(tstbxDirPath.Text);

                    if (batchFiles.Rows.Count > 0)
                    {
                        dgvTestBatchFiles.DataSource = batchFiles;
                        dgvTestBatchFiles.Columns["FileSize"].Width = 150;
                        dgvTestBatchFiles.Columns["FileDate"].Width = 250;
                    }
                    else
                    {
                        dgvTestBatchFiles.DataSource = null;
                    }

                    ResetBar();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucApiTestHarness.tsbtnBrowse_Click", ex);
                MessageBox.Show(ex.Message, "ucApiTestHarness.tsbtnBrowse_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetBar()
        {
            tspbTestRun.Maximum = 100;
            tspbTestRun.Minimum = 1;
            tspbTestRun.Step = 1;
            tspbTestRun.Value = 1;
        }

        private void tsbtnRunTests_Click(object sender, EventArgs e)
        {
            try
            {
                ResetBar();
                int recsuploaded = 0;

                // verbose mode is not available for batch execution of test files
                bool verbose = true;
                if (dgvTestBatchFiles.SelectedRows.Count > 1) verbose = false;

                TestUtil testUtil = new TestUtil();
                DataTable testresults = new DataTable();

                Cursor std = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                Stopwatch testrun = new Stopwatch();
                testrun.Start();

                // run each test file in list of selected files
                if (dgvTestBatchFiles.SelectedRows.Count > 0)
                {
                    MsgUtil msgUtil = new MsgUtil();
                    CmnUtil cmnUtil = new CmnUtil();

                    // create unique label for test run
                    string testrunname = Environment.MachineName + " : " + _main.UserName + " : " + DateTime.UtcNow.Ticks.ToString();

                    tspbTestRun.Maximum = dgvTestBatchFiles.SelectedRows.Count;

                    for (int i = 0; i < dgvTestBatchFiles.SelectedRows.Count; i++)
                    {
                        testUtil.TestVars = new Dictionary<string, string>();
                        DataTable tmpresults = testUtil.RunTests(dgvTestBatchFiles.SelectedRows[i].Cells["FilePath"].Value.ToString(), _main.UserName, _main.Password, _httpClient, _main.SvcUrl, verbose, testrunname);

                        // update the progressbar
                        tspbTestRun.PerformStep();
                        Application.DoEvents();

                        // add test file results to results table
                        if (tmpresults.Rows.Count > 0) testresults.Merge(tmpresults);

                        if (_upload)
                        {
                            string resrecs = cmnUtil.TableToXml(tmpresults, true);

                            // upload batch of results from the test file (include testRun label)
                            Dictionary<string, string> methparams = new Dictionary<string, string>();
                            methparams.Add("ResultRecords", resrecs);

                            string reqID = msgUtil.GetNewGID();
                            string methList = msgUtil.CreateXMLMethod("TestResult.New.base", methparams);
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

                                ResInfo methresult = msgUtil.GetResult("TestResult.New.base_DEFAULT", methresults);

                                if (methresult != null && methresult.RCode == APIResult.OK)
                                {
                                    recsuploaded += tmpresults.Rows.Count;
                                }
                                else
                                {
                                    RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucApiTestHarness.tsbtnRunTests_Click", "Test result error.", "Error uploading test results.");
                                }
                            }
                        }
                    }
                }

                testrun.Stop();

                Cursor.Current = std;

                _main.SetMetrics(testresults.Rows.Count, testrun.Elapsed.TotalMilliseconds, 0, "Lattice", "ucApiTestHarness");

                // show test results datatable
                bool batch = true;
                if (dgvTestBatchFiles.SelectedRows.Count == 1) batch = false;

                frmTestResults testResults = new frmTestResults(testresults, batch, recsuploaded);
                testResults.Show();
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "ucApiTestHarness.tsbtnRunTests_Click", ex);
                MessageBox.Show(ex.Message, "ucApiTestHarness.tsbtnRunTests_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTestBatchFiles_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvTestBatchFiles.ClearSelection();
                this.dgvTestBatchFiles.Rows[e.RowIndex].Selected = true;

                DataTable table = dgvTestBatchFiles.DataSource as DataTable;
                DataRow row = table.NewRow();
                row = ((DataRowView)dgvTestBatchFiles.SelectedRows[0].DataBoundItem).Row;

                _testfilepath = row["FilePath"].ToString();
            }
        }


        // ************************************************************************************* //
        // Context Menu ************************************************************************ //
        // ************************************************************************************* //

        private void viewfileMenuItem_Click(object sender, EventArgs e)
        {
            string fileXml = File.ReadAllText(_testfilepath);

            string inputxml = "<?xml version=\"1.0\"?>\n" + fileXml;
            frmViewXML viewXML = new frmViewXML(inputxml, "TESTFILE");
            viewXML.ShowDialog();
        }

        private void tsbtnUpload_Click(object sender, EventArgs e)
        {
            if (_upload)
            {
                _upload = false;
                tsbtnUpload.Text = "Upload: OFF";
            }
            else
            {
                _upload = true;
                tsbtnUpload.Text = "Upload: ON";
            }
        }
    }
}
