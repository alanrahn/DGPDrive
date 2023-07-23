using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Net.Http;

using ApiUtil.DataClasses;

namespace ApiUtil
{
    public class TestUtil
    {
        public int MethCount { get; set; }
        //public Dictionary<string, string> TestVars { get; set; }

        public TestUtil()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetTestFileList(string sourceDir)
        {
            DataTable fileTbl = new DataTable();
            fileTbl.Columns.Add("FileName", typeof(string));
            fileTbl.Columns.Add("FileSize", typeof(string));
            fileTbl.Columns.Add("FileDate", typeof(string));
            fileTbl.Columns.Add("FilePath", typeof(string));
            fileTbl.Columns.Add("Info", typeof(string));

            foreach (string fistr in Directory.EnumerateFiles(sourceDir, "*.xml", SearchOption.AllDirectories))
            {
                try
                {
                    if (File.Exists(fistr))
                    {
                        FileInfo fi = new FileInfo(fistr);

                        DataRow dr = fileTbl.NewRow();
                        dr["FileName"] = fi.Name;
                        dr["FileSize"] = fi.Length.ToString();
                        dr["FileDate"] = fi.CreationTime.ToString();
                        dr["FilePath"] = fi.FullName;
                        dr["Info"] = "";

                        fileTbl.Rows.Add(dr);
                    }
                }
                catch (UnauthorizedAccessException unAuth)
                {
                    // add unauthorized message to file list
                    DataRow exc = fileTbl.NewRow();
                    exc["FileName"] = fistr;
                    exc["Info"] = unAuth.Message;
                    fileTbl.Rows.Add(exc);
                }
            }

            return fileTbl;
        }

        /// <summary>
        /// 
        /// </summary>
        //public DataTable RunTests(string filePath, string userName, string passWord, string svcURL, bool verbose, string testRun, string testType)
        //{
        //    TestFileInfo testFileInfo = new TestFileInfo();
        //    CmnUtil cmnUtil = new CmnUtil();
        //    MsgUtil msgUtil = new MsgUtil();
        //    TestMsgInfo testMsgInfo;
        //    RespInfo respinfo;

        //    DataTable testResults = GetResultTable();
        //    List<string> msgList = ReadTestFile(testFileInfo, filePath);
        //    MethCount += msgList.Count;

        //    // initialize test variable collection
        //    Dictionary<string, string> testVars = TestVars;

        //    foreach (string msg in msgList)
        //    {
        //        string msgxml = msg;
        //        string reqMsg = "";
        //        string respMsg = "";
        //        string eval = "";
        //        string evalinfo = "";

        //        testMsgInfo = new TestMsgInfo();
        //        respinfo = new RespInfo();
        //        Dictionary<string, ResInfo> methresults = new Dictionary<string, ResInfo>();

        //        // inner try/catch for each API method call in the test file
        //        try
        //        {
        //            // replace standard variables in msgxml
        //            msgxml = msgxml.Replace("{{TMUserName}}", userName);
        //            msgxml = msgxml.Replace("{{TMPassword}}", passWord);
        //            string newGID = cmnUtil.GetNewGID();
        //            msgxml = msgxml.Replace("{{TMNewGID}}", newGID);

        //            // replace testVars values in meth xml
        //            if (testVars.Count > 0)
        //            {
        //                foreach (KeyValuePair<string, string> testvar in testVars)
        //                {
        //                    msgxml = msgxml.Replace("{{" + testvar.Key + "}}", testvar.Value);
        //                }
        //            }

        //            // read return values from testmethod XML
        //            ReadTestMessage(testMsgInfo, msgxml);
        //            string reqid = cmnUtil.GetNewGID();

        //            if (testType == "ALL" || testMsgInfo.MsgType == testType)
        //            {

        //                // sanitized version of the request message is created containing no account credentials (will be logged)
        //                reqMsg = msgUtil.CreateXMLRequest("username", "password", reqid, testMsgInfo.MethXML);

        //                // run the test (keep track of method latency and server duration)
        //                Stopwatch methcall = new Stopwatch();
        //                methcall.Start();

        //                //SOAPMsgUtil soapmsg = new SOAPMsgUtil();
        //                //string respMsg = soapmsg.SOAPApiHelper(testMsgInfo.MethXML, reqid, testMsgInfo.UserName, testMsgInfo.Password, svcURL, _main.SvcPubKey);

        //                methcall.Stop();

        //                methresults = msgUtil.ReadResponseString(respinfo, respMsg);

        //                evalinfo = "<TestMeth><TestMethName>" + testMsgInfo.MethName + "</TestMethName><TestMethDescrip>" + testMsgInfo.Descrip + "</TestMethDescrip>";

        //                // evaluate API response
        //                if (respinfo.Auth == testMsgInfo.ExpAuthCode)
        //                {
        //                    eval = "PASS";
        //                }
        //                else
        //                {
        //                    eval = "AUTH FAIL";

        //                    if (respinfo.Auth == LocState.Offline) break;
        //                }

        //                evalinfo += "<TestAuth><ExpAuth>" + testMsgInfo.ExpAuthCode + "</ExpAuth><ActAuth>" + respinfo.Auth + "</ActAuth></TestAuth><TestResultList>";

        //                // for each result in testMethInfo, get the matching response result
        //                foreach (TestResInfo testres in testMsgInfo.ResultList)
        //                {
        //                    ResInfo res = msgUtil.GetResult(testres.RName, methresults);

        //                    bool rcode = false;
        //                    bool dtype = false;
        //                    bool rval = false;

        //                    // compare actual results to expected results
        //                    if (res != null)
        //                    {
        //                        if (testres.ExpRCode == res.RCode) rcode = true;
        //                        if (testres.ExpDType == res.DType) dtype = true;

        //                        if (testres.ExpRVal != null && testres.ExpRVal != "")
        //                        {
        //                            if (res.RVal != null && res.RVal != "" && res.RVal.Contains(testres.ExpRVal)) rval = true;
        //                        }
        //                        else
        //                        {
        //                            rval = true;
        //                        }

        //                        // add returned value as test variable only if a variable name has been specified
        //                        if (testres.VarName != null && testres.VarName != "" && res.RCode == APIResult.OK)
        //                        {
        //                            if (!testVars.ContainsKey(testres.VarName)) testVars.Add(testres.VarName, res.RVal);
        //                        }

        //                        testres.EvalInfo += "<TestResult><TestResName>" + testres.RName + "</TestResName>";

        //                        // evaluate each result returned by method call and assign status value
        //                        if (rcode && dtype && rval)
        //                        {
        //                            testres.TestEval = "PASS";
        //                            testres.EvalInfo += "<TestResEval>PASS</TestResEval>";
        //                        }
        //                        else
        //                        {
        //                            // if one result fails, the evaluation summary of a method call fails
        //                            eval = "RES FAIL";
        //                            testres.TestEval = "FAIL";
        //                            testres.EvalInfo += "<TestResEval>FAIL</TestResEval>";
        //                        }

        //                        testres.EvalInfo += "<ExpRcode>" + testres.ExpRCode + "</ExpRcode>" +
        //                                                "<ActRcode>" + res.RCode + "</ActRcode>" +
        //                                                "<ExpDtype>" + testres.ExpDType + "</ExpDtype>" +
        //                                                "<ActDtype>" + res.DType + "</ActDtype>" +
        //                                                "<ExpRval>" + testres.ExpRVal + "</ExpRval></TestResult>";

        //                        evalinfo += testres.EvalInfo;
        //                    }
        //                    else
        //                    {
        //                        eval = "RESP FAIL";
        //                        evalinfo += "<TestResult><TestResName></TestResName>" +
        //                                    "<TestResEval>RESULT NOT FOUND</TestResEval>" +
        //                                    "<ExpRcode></ExpRcode>" +
        //                                    "<ActRcode></ActRcode>" +
        //                                    "<ExpDtype></ExpDtype>" +
        //                                    "<ActDtype></ActDtype>" +
        //                                    "<ExpRval></ExpRval></TestResult>";
        //                    }

        //                } // end method result foreach

        //                evalinfo += "</TestResultList></TestMeth>";

        //                // create a result record and append to testResults table
        //                DataRow dr = testResults.NewRow();
        //                dr["TestRun"] = testRun;
        //                dr["Eval"] = eval;
        //                dr["EvalInfo"] = evalinfo;
        //                dr["APIMethod"] = testMsgInfo.MethName;
        //                dr["Descrip"] = testMsgInfo.Descrip;
        //                dr["AuthCode"] = respinfo.Auth;
        //                dr["AuthInfo"] = respinfo.Info;
        //                dr["ExpAuthCode"] = testMsgInfo.ExpAuthCode;
        //                dr["ClientMS"] = methcall.Elapsed.TotalMilliseconds.ToString();
        //                dr["NetworkMS"] = (methcall.Elapsed.TotalMilliseconds - Convert.ToDouble(respinfo.SvrMS)).ToString();
        //                dr["ServerMS"] = respinfo.SvrMS;
        //                dr["UserName"] = userName;
        //                dr["CompName"] = Environment.MachineName;
        //                dr["TimeStamp"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //                dr["SvcURL"] = svcURL;
        //                dr["FileName"] = testFileInfo.TestName;
        //                dr["ReqSize"] = reqMsg.Length.ToString();
        //                dr["RespSize"] = respMsg.Length.ToString();

        //                if (verbose)
        //                {
        //                    dr["ReqXML"] = reqMsg;
        //                    dr["RespXML"] = respMsg;
        //                }

        //                testResults.Rows.Add(dr);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // exception during API method call
        //            DataRow dr = testResults.NewRow();
        //            dr["TestRun"] = testRun;
        //            dr["Eval"] = "EXCEPTION";
        //            dr["EvalInfo"] = ex.Message;
        //            dr["APIMethod"] = testMsgInfo.MethName;
        //            dr["Descrip"] = testMsgInfo.Descrip;
        //            dr["AuthCode"] = respinfo.Auth;
        //            dr["AuthInfo"] = respinfo.Info;
        //            dr["ExpAuthCode"] = testMsgInfo.ExpAuthCode;
        //            dr["UserName"] = userName;
        //            dr["CompName"] = Environment.MachineName;
        //            dr["TimeStamp"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //            dr["SvcURL"] = svcURL;
        //            dr["FileName"] = testFileInfo.TestName;

        //            testResults.Rows.Add(dr);
        //        }

        //    } // end test method foreach

        //    return testResults;
        //}

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

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetResultTable()
        {
            DataTable resultTbl = new DataTable();

            resultTbl.Columns.Add("TestRun", typeof(string));
            resultTbl.Columns.Add("Eval", typeof(string));
            resultTbl.Columns.Add("EvalInfo", typeof(string));
            resultTbl.Columns.Add("APIMethod", typeof(string));
            resultTbl.Columns.Add("Descrip", typeof(string));
            resultTbl.Columns.Add("AuthCode", typeof(string));
            resultTbl.Columns.Add("AuthInfo", typeof(string));
            resultTbl.Columns.Add("ExpAuthCode", typeof(string));
            resultTbl.Columns.Add("ClientMS", typeof(string));
            resultTbl.Columns.Add("NetworkMS", typeof(string));
            resultTbl.Columns.Add("ServerMS", typeof(string));
            resultTbl.Columns.Add("CompName", typeof(string));
            resultTbl.Columns.Add("TimeStamp", typeof(string));
            resultTbl.Columns.Add("ReqXML", typeof(string));
            resultTbl.Columns.Add("ReqSize", typeof(string));
            resultTbl.Columns.Add("RespXML", typeof(string));
            resultTbl.Columns.Add("RespSize", typeof(string));
            resultTbl.Columns.Add("UserName", typeof(string));
            resultTbl.Columns.Add("HostName", typeof(string));
            resultTbl.Columns.Add("SvcURL", typeof(string));
            resultTbl.Columns.Add("FileName", typeof(string));

            return resultTbl;
        }

        /// <summary>
        /// Thomas Corey, CodeProject (Writing a DataTable to a CSV file - 8 Oct 2013)
        /// </summary>
        public bool WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            bool result = false;
            CmnUtil cmnUtil = new CmnUtil();

            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(cmnUtil.QuoteValue(column.ColumnName));
                }

                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }

            string[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => cmnUtil.QuoteValue(o.ToString())).ToArray();
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
            result = true;

            return result;
        }

    }
}
