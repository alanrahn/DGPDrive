
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Data;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.Testing
{
    public partial class ucApiTestHarness : UserControl
    {
        public int MethCount { get; set; }
        public Dictionary<string, string> TestVars { get; set; }

        MainForm _main;

        string _testfilepath;
        string _testtype = "ALL";
        bool _upload = false;
        bool _runtest = true;

        public string TestFileDir { get; set; }

        public ucApiTestHarness(MainForm main)
        {
            InitializeComponent();

            try
            {
                _main = main;
                _main.SetMetrics(0, 0, 0, "", "");

                ResetBar();

                TestFileDir = Environment.CurrentDirectory;

                tscmbTestType.SelectedIndex = 0;
                tscmbIterations.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGPDrive", "ucApiTestHarness", ex);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGPDrive", "ucApiTestHarness.tsbtnBrowse_Click", ex);
                MessageBox.Show(ex.Message, "ucApiTestHarness.tsbtnBrowse_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetBar()
        {
            tspbTestRun.Maximum = dgvTestBatchFiles.SelectedRows.Count;
            tspbTestRun.Minimum = 1;
            tspbTestRun.Step = 1;
            tspbTestRun.Value = 1;
        }

        private void tsbtnRunTests_Click(object sender, EventArgs e)
        {
            try
            {
                _runtest = true;
                int recsuploaded = 0;
                bool verbose = false;

                // get iteration count value and multi-select test file count
                int iterations = Convert.ToInt32(tscmbIterations.SelectedItem);
                if (iterations == 1 && dgvTestBatchFiles.SelectedRows.Count == 1) verbose = true;

                TestUtil testUtil = new TestUtil();
                DataTable testresults = new DataTable();
                DataTable tmpresults = new DataTable();
                MsgUtil msgUtil = new MsgUtil();
                CmnUtil cmnUtil = new CmnUtil();

                Cursor std = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                tstbxLoopCount.Text = "0";
                Stopwatch testrun = new Stopwatch();
                testrun.Start();

                for (int i = 1; i <= iterations; i++)
                {
                    if (_runtest)
                    {
                        ResetBar();
                        testresults.Rows.Clear();
                        // create unique label for test run
                        string testrunname = Environment.MachineName + " : " + _main.UserName + " : " + DateTime.UtcNow.Ticks.ToString();

                        // run each test file in list of selected files
                        if (dgvTestBatchFiles.SelectedRows.Count > 0)
                        {
                            for (int j = 0; j < dgvTestBatchFiles.SelectedRows.Count; j++)
                            {
                                if (_runtest)
                                {
                                    TestVars = new Dictionary<string, string>();
                                    tmpresults.Rows.Clear();
                                    tmpresults = RunTests(dgvTestBatchFiles.SelectedRows[j].Cells["FilePath"].Value.ToString(), _main.UserName, _main.Password, _main.SvcUrl, verbose, testrunname, _testtype);

                                    // update the progressbar
                                    tspbTestRun.PerformStep();
                                    Application.DoEvents();

                                    // add test file results to results table
                                    if (tmpresults.Rows.Count > 0) testresults.Merge(tmpresults);

                                    // if upload is turned on, the results of each iteration are saved to the current location
                                    if (_upload)
                                    {
                                        string resrecs = cmnUtil.TableToXml(tmpresults, true);

                                        // upload batch of results from the test file (include testRun label)
                                        Dictionary<string, string> methparams = new Dictionary<string, string>();
                                        methparams.Add("ResultRecords", resrecs);

                                        string reqID = msgUtil.GetNewGID();
                                        string methList = msgUtil.CreateXMLMethod("TestResult.New.base", methparams);
                                        SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                                        string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

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
                                                RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGPDrive", "ucApiTestHarness.tsbtnRunTests_Click", "Test result error.", "Error uploading test results.");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }  // end of multi-file selection
                        }
                    }

                    // update the iteration count
                    tstbxLoopCount.Text = i.ToString();
                    Application.DoEvents();

                }  // end of iterations

                testrun.Stop();

                Cursor.Current = std;

                _main.SetMetrics(testresults.Rows.Count, testrun.Elapsed.TotalMilliseconds, 0, "DGPDrive", "ucApiTestHarness");

                // show test results datatable
                bool batch = true;
                if (dgvTestBatchFiles.SelectedRows.Count == 1) batch = false;

                // will only show the results of the last completed iteration of the test run
                frmTestResults testResults = new frmTestResults(testresults, batch, recsuploaded);
                testResults.Show();
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGPDrive", "ucApiTestHarness.tsbtnRunTests_Click", ex);
                MessageBox.Show(ex.Message, "ucApiTestHarness.tsbtnRunTests_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RunTests(string filePath, string userName, string passWord, string svcURL, bool verbose, string testRun, string testType)
        {
            TestFileInfo testFileInfo = new TestFileInfo();
            CmnUtil cmnUtil = new CmnUtil();
            MsgUtil msgUtil = new MsgUtil();
            TestUtil testUtil = new TestUtil();
            TestMsgInfo testMsgInfo;
            RespInfo respinfo;

            DataTable testResults = testUtil.GetResultTable();
            List<string> msgList = ReadTestFile(testFileInfo, filePath);
            MethCount += msgList.Count;

            // initialize test variable collection
            Dictionary<string, string> testVars = TestVars;

            foreach (string msg in msgList)
            {
                string msgxml = msg;
                string reqMsg = "";
                string eval = "";
                string evalinfo = "";

                testMsgInfo = new TestMsgInfo();
                respinfo = new RespInfo();
                Dictionary<string, ResInfo> methresults = new Dictionary<string, ResInfo>();

                // inner try/catch for each API method call in the test file
                try
                {
                    // replace standard variables in msgxml
                    msgxml = msgxml.Replace("{{TMUserName}}", userName);
                    msgxml = msgxml.Replace("{{TMPassword}}", passWord);
                    string newGID = cmnUtil.GetNewGID();
                    msgxml = msgxml.Replace("{{TMNewGID}}", newGID);

                    // replace testVars values in meth xml
                    if (testVars.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> testvar in testVars)
                        {
                            msgxml = msgxml.Replace("{{" + testvar.Key + "}}", testvar.Value);
                        }
                    }

                    // read return values from testmethod XML
                    ReadTestMessage(testMsgInfo, msgxml);
                    string reqid = cmnUtil.GetNewGID();

                    if (testType == "ALL" || testMsgInfo.MsgType == testType)
                    {

                        // sanitized version of the request message is created containing no account credentials (will be logged)
                        reqMsg = msgUtil.CreateXMLRequest("username", "password", reqid, testMsgInfo.MethXML);

                        // run the test (keep track of method latency and server duration)
                        Stopwatch methcall = new Stopwatch();
                        methcall.Start();

                        SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                        string respMsg = soapmsg.SOAPApiHelper(testMsgInfo.MethXML, reqid, testMsgInfo.UserName, testMsgInfo.Password, svcURL, _main.SvcPubKey);

                        methcall.Stop();

                        methresults = msgUtil.ReadResponseString(respinfo, respMsg);

                        evalinfo = "<TestMeth><TestMethName>" + testMsgInfo.MethName + "</TestMethName><TestMethDescrip>" + testMsgInfo.Descrip + "</TestMethDescrip>";

                        // evaluate API response
                        if (respinfo.Auth == testMsgInfo.ExpAuthCode)
                        {
                            eval = "PASS";
                        }
                        else
                        {
                            eval = "AUTH FAIL";

                            if (respinfo.Auth == LocState.Offline) break;
                        }

                        evalinfo += "<TestAuth><ExpAuth>" + testMsgInfo.ExpAuthCode + "</ExpAuth><ActAuth>" + respinfo.Auth + "</ActAuth></TestAuth><TestResultList>";

                        // for each result in testMethInfo, get the matching response result
                        foreach (TestResInfo testres in testMsgInfo.ResultList)
                        {
                            ResInfo res = msgUtil.GetResult(testres.RName, methresults);

                            bool rcode = false;
                            bool dtype = false;
                            bool rval = false;

                            // compare actual results to expected results
                            if (res != null)
                            {
                                if (testres.ExpRCode == res.RCode) rcode = true;
                                if (testres.ExpDType == res.DType) dtype = true;

                                if (testres.ExpRVal != null && testres.ExpRVal != "")
                                {
                                    if (res.RVal != null && res.RVal != "" && res.RVal.Contains(testres.ExpRVal)) rval = true;
                                }
                                else
                                {
                                    rval = true;
                                }

                                // add returned value as test variable only if a variable name has been specified
                                if (testres.VarName != null && testres.VarName != "" && res.RCode == APIResult.OK)
                                {
                                    if (!testVars.ContainsKey(testres.VarName)) testVars.Add(testres.VarName, res.RVal);
                                }

                                testres.EvalInfo += "<TestResult><TestResName>" + testres.RName + "</TestResName>";

                                // evaluate each result returned by method call and assign status value
                                if (rcode && dtype && rval)
                                {
                                    testres.TestEval = "PASS";
                                    testres.EvalInfo += "<TestResEval>PASS</TestResEval>";
                                }
                                else
                                {
                                    // if one result fails, the evaluation summary of a method call fails
                                    eval = "RES FAIL";
                                    testres.TestEval = "FAIL";
                                    testres.EvalInfo += "<TestResEval>FAIL</TestResEval>";
                                }

                                testres.EvalInfo += "<ExpRcode>" + testres.ExpRCode + "</ExpRcode>" +
                                                        "<ActRcode>" + res.RCode + "</ActRcode>" +
                                                        "<ExpDtype>" + testres.ExpDType + "</ExpDtype>" +
                                                        "<ActDtype>" + res.DType + "</ActDtype>" +
                                                        "<ExpRval>" + testres.ExpRVal + "</ExpRval></TestResult>";

                                evalinfo += testres.EvalInfo;
                            }
                            else
                            {
                                eval = "RESP FAIL";
                                evalinfo += "<TestResult><TestResName></TestResName>" +
                                            "<TestResEval>RESULT NOT FOUND</TestResEval>" +
                                            "<ExpRcode></ExpRcode>" +
                                            "<ActRcode></ActRcode>" +
                                            "<ExpDtype></ExpDtype>" +
                                            "<ActDtype></ActDtype>" +
                                            "<ExpRval></ExpRval></TestResult>";
                            }

                        } // end method result foreach

                        evalinfo += "</TestResultList></TestMeth>";

                        // create a result record and append to testResults table
                        DataRow dr = testResults.NewRow();
                        dr["TestRun"] = testRun;
                        dr["Eval"] = eval;
                        dr["EvalInfo"] = evalinfo;
                        dr["APIMethod"] = testMsgInfo.MethName;
                        dr["Descrip"] = testMsgInfo.Descrip;
                        dr["AuthCode"] = respinfo.Auth;
                        dr["AuthInfo"] = respinfo.Info;
                        dr["ExpAuthCode"] = testMsgInfo.ExpAuthCode;
                        dr["ClientMS"] = methcall.Elapsed.TotalMilliseconds.ToString();
                        dr["NetworkMS"] = (methcall.Elapsed.TotalMilliseconds - Convert.ToDouble(respinfo.SvrMS)).ToString();
                        dr["ServerMS"] = respinfo.SvrMS;
                        dr["UserName"] = userName;
                        dr["CompName"] = Environment.MachineName;
                        dr["TimeStamp"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        dr["SvcURL"] = svcURL;
                        dr["FileName"] = testFileInfo.TestName;
                        dr["ReqSize"] = reqMsg.Length.ToString();
                        dr["RespSize"] = respMsg.Length.ToString();

                        if (verbose)
                        {
                            dr["ReqXML"] = reqMsg;
                            dr["RespXML"] = respMsg;
                        }

                        testResults.Rows.Add(dr);
                    }
                }
                catch (Exception ex)
                {
                    // exception during API method call
                    DataRow dr = testResults.NewRow();
                    dr["TestRun"] = testRun;
                    dr["Eval"] = "EXCEPTION";
                    dr["EvalInfo"] = ex.Message;
                    dr["APIMethod"] = testMsgInfo.MethName;
                    dr["Descrip"] = testMsgInfo.Descrip;
                    dr["AuthCode"] = respinfo.Auth;
                    dr["AuthInfo"] = respinfo.Info;
                    dr["ExpAuthCode"] = testMsgInfo.ExpAuthCode;
                    dr["UserName"] = userName;
                    dr["CompName"] = Environment.MachineName;
                    dr["TimeStamp"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    dr["SvcURL"] = svcURL;
                    dr["FileName"] = testFileInfo.TestName;

                    testResults.Rows.Add(dr);
                }

            } // end test method foreach

            return testResults;
        }

        /// <summary>
        /// 
        /// </summary>
        private List<string> ReadTestFile(TestFileInfo testfileInfo, string filePath)
        {
            // get XML from test file
            string fileXml = File.ReadAllText(filePath);

            MsgUtil msgUtil = new MsgUtil();
            fileXml = fileXml.Replace("{{TGID}}", msgUtil.UnixTimeString());

            List<string> msglist = new List<string>();

            // the reader will break if these required elements are missing or out of order; any additional elements must be appended after the required elements
            using (XmlReader reader = XmlReader.Create(new StringReader(fileXml)))
            {
                reader.Read();
                reader.ReadToFollowing("TName");

                if (reader.LocalName.Equals("TName")) testfileInfo.TestName = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test file missing TName element.");

                reader.ReadToFollowing("TDescrip");

                if (reader.LocalName.Equals("TDescrip")) testfileInfo.Descrip = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test file missing TDescrip element.");

                reader.ReadToFollowing("TGID");

                if (reader.LocalName.Equals("TGID")) testfileInfo.TGID = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test file missing TGID element.");

                while (reader.Read())
                {
                    switch (reader.LocalName)
                    {
                        case "TMsg":
                            if (reader.IsStartElement())
                            {
                                msglist.Add(reader.ReadOuterXml());
                            }
                            break;
                    }
                }
            }

            return msglist;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadTestMessage(TestMsgInfo testMsgInfo, string testMsgStr)
        {
            List<TestResInfo> testresults = new List<TestResInfo>();

            using (XmlReader reader = XmlReader.Create(new StringReader(testMsgStr)))
            {
                reader.Read();
                reader.ReadToFollowing("TMUserName");

                if (reader.LocalName.Equals("TMUserName")) testMsgInfo.UserName = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test Request XML missing UserName element.");

                reader.ReadToFollowing("TMPassword");

                if (reader.LocalName.Equals("TMPassword")) testMsgInfo.Password = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test Request XML missing Password element.");

                reader.ReadToFollowing("TMName");

                if (reader.LocalName.Equals("TMName")) testMsgInfo.MethName = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test Request XML missing TMName element.");

                reader.ReadToFollowing("TMDescrip");

                if (reader.LocalName.Equals("TMDescrip")) testMsgInfo.Descrip = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test Request XML missing TMDescrip element.");

                reader.ReadToFollowing("TMExpAuthCode");

                if (reader.LocalName.Equals("TMExpAuthCode")) testMsgInfo.ExpAuthCode = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test Request XML missing TMExpAuthCode element.");

                reader.ReadToFollowing("TMMsgType");

                if (reader.LocalName.Equals("TMMsgType")) testMsgInfo.MsgType = reader.ReadElementContentAsString();
                else throw new InvalidDataException("Test Request XML missing TMMsgType element.");

                reader.ReadToFollowing("Meth");

                if (reader.LocalName.Equals("Meth")) testMsgInfo.MethXML = reader.ReadOuterXml();
                else throw new InvalidDataException("Test Request XML missing Meth element.");

                while (reader.Read())
                {
                    switch (reader.LocalName)
                    {
                        case "Result":
                            if (reader.IsStartElement())
                            {
                                TestResInfo testresInfo = new TestResInfo();

                                reader.ReadToFollowing("RName");

                                if (reader.LocalName.Equals("RName")) testresInfo.RName = reader.ReadElementContentAsString();
                                else throw new InvalidDataException("Test result XML missing RName element.");

                                reader.ReadToFollowing("ExpRCode");

                                if (reader.LocalName.Equals("ExpRCode")) testresInfo.ExpRCode = reader.ReadElementContentAsString();
                                else throw new InvalidDataException("Test result XML missing ExpRCode element.");

                                reader.ReadToFollowing("ExpDType");

                                if (reader.LocalName.Equals("ExpDType")) testresInfo.ExpDType = reader.ReadElementContentAsString();
                                else throw new InvalidDataException("Test result XML missing ExpDType element.");

                                reader.ReadToFollowing("ExpRVal");

                                if (reader.LocalName.Equals("ExpRVal")) testresInfo.ExpRVal = reader.ReadElementContentAsString();
                                else throw new InvalidDataException("Test result XML missing ExpRVal element.");

                                reader.ReadToFollowing("VarName");

                                if (reader.LocalName.Equals("VarName")) testresInfo.VarName = reader.ReadElementContentAsString();
                                else throw new InvalidDataException("Test result XML missing VarName element.");

                                testresults.Add(testresInfo);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            testMsgInfo.ResultList = testresults;
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

        private void tscmbTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _testtype = tscmbTestType.Text;
        }

        private void tsbtnStopRun_Click(object sender, EventArgs e)
        {
            _runtest = false;
        }
    }
}
