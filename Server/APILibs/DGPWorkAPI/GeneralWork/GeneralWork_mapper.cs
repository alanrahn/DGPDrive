using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using SysWorkDB;
using SysMetricsDB;

namespace DGPWorkAPI
{
    public class GeneralWork_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public GeneralWork_mapper(string connStr)
        {
            _connstr = connStr;
            _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetPageSize(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, _pagesize);
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string rec_gid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }
                if (rec_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    results = genWork_Dml.GetByID(rec_gid);

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetAll(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    results = genWork_Dml.GetAll();

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                // optional search parameter
                string srcdbname = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);

                GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);

                string latestcount = genWork_Dml.GetCount(srcdbname);

                if (latestcount != null && latestcount != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, latestcount);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
                }

            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSearch(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                // pagination values
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string pagenum = msgUtil.GetParamValue(CommonFields.PageNum, methparams);

                // search values (optional)
                string srcdbname = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }
                if (pagenum == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing PageNum; ";
                }
                else
                {
                    var isNumeric = int.TryParse(pagenum, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "PageNum must be an int; ";
                    }
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    results = genWork_Dml.GetSearch(pagenum, _pagesize, srcdbname);

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string QueueScan(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    results = genWork_Dml.QueueScan();

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public string ClaimWorkRecords(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string claimToken = msgUtil.GetParamValue(WorkFields.ClaimedBy, methparams);
                string network = msgUtil.GetParamValue(WorkFields.Network, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }
                if (claimToken == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ClaimedBy; ";
                }
                if (network == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Network; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    results = genWork_Dml.ClaimReplicaWorkRecs(network, claimToken);

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReleaseWorkRecord(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                // incorrect - calculate nextrun on server from input values
                string rec_gid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
                string nextRun = msgUtil.GetParamValue(WorkFields.NextRun, methparams);
                string runstate = msgUtil.GetParamValue(WorkFields.RunState, methparams);
                string statemsg = msgUtil.GetParamValue(WorkFields.StateMsg, methparams);
                string claimedby = msgUtil.GetParamValue(WorkFields.ClaimedBy, methparams);
                string procstate = msgUtil.GetParamValue(WorkFields.ProcState, methparams);
                string resetcount = msgUtil.GetParamValue(WorkFields.ResetCount, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (rec_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }
                if (nextRun == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing NextRun; ";
                }
                if (runstate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RunState; ";
                }
                if (statemsg == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing StateMsg; ";
                }
                if (claimedby == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ClaimedBy; ";
                }
                if (procstate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ProcState; ";
                }
                if (resetcount == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ResetCount; ";
                }

                if (reqFields)
                {
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    string rescode = genWork_Dml.ReleaseGeneralWorkRec(rec_gid, nextRun, runstate, statemsg, claimedby, procstate, resetcount);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, rescode);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, "");
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string New(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();

                string rec_gid = cmnUtil.GetNewGID();

                string Network = msgUtil.GetParamValue(WorkFields.Network, methparams);
                string SchemaTable = msgUtil.GetParamValue(WorkFields.SchemaTable, methparams);
                string SrcDBName = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string WorkType = msgUtil.GetParamValue(WorkFields.WorkType, methparams);
                string SrcURL = msgUtil.GetParamValue(WorkFields.SrcURL, methparams);
                string SrcMethod = msgUtil.GetParamValue(WorkFields.SrcMethod, methparams);
                string DestURL = msgUtil.GetParamValue(WorkFields.DestURL, methparams);
                string DestMethod = msgUtil.GetParamValue(WorkFields.DestMethod, methparams);
                string StartID = msgUtil.GetParamValue(WorkFields.StartID, methparams);
                string FinalID = msgUtil.GetParamValue(WorkFields.FinalID, methparams);
                string IntervalType = msgUtil.GetParamValue(WorkFields.IntervalType, methparams);
                string IntervalVal = msgUtil.GetParamValue(WorkFields.IntervalVal, methparams);
                string NextRun = msgUtil.GetParamValue(WorkFields.NextRun, methparams);
                string RunState = msgUtil.GetParamValue(WorkFields.RunState, methparams);
                string Logging = msgUtil.GetParamValue(WorkFields.Logging, methparams);
                string MaxDurMS = msgUtil.GetParamValue(WorkFields.MaxDurMS, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (Network == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Network; ";
                }
                if (SchemaTable == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaTable; ";
                }
                if (SrcDBName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcDBName; ";
                }
                if (WorkType == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing WorkType; ";
                }
                if (SrcURL == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcURL; ";
                }
                if (SrcMethod == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcMethod; ";
                }
                if (DestURL == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DestURL; ";
                }
                if (DestMethod == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DestMethod; ";
                }
                if (StartID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing StartID; ";
                }
                if (FinalID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FinalID; ";
                }
                if (IntervalType == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing IntervalType; ";
                }
                if (IntervalVal == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing IntervalVal; ";
                }
                if (NextRun == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ProcState; ";
                }
                if (RunState == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing NextRun; ";
                }
                if (Logging == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Logging; ";
                }
                if (MaxDurMS == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MaxDurMS; ";
                }

                if (reqFields)
                {
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    string rescode = genWork_Dml.New(rec_gid,
                                                    userinfo.UserGID,
                                                    Network,
                                                    SchemaTable,
                                                    SrcDBName,
                                                    ShardName,
                                                    WorkType,
                                                    SrcURL,
                                                    SrcMethod,
                                                    DestURL,
                                                    DestMethod,
                                                    StartID,
                                                    FinalID,
                                                    IntervalType,
                                                    IntervalVal,
                                                    NextRun,
                                                    RunState,
                                                    Logging,
                                                    MaxDurMS);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, rec_gid);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, rescode);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Clone(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string rec_gid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (rec_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }

                if (reqFields)
                {
                    CmnUtil cmnUtil = new CmnUtil();
                    string newgid = cmnUtil.GetNewGID();
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    string rescode = genWork_Dml.Clone(rec_gid, userinfo.UserGID, newgid);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, newgid);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, rescode);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Save(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string rec_gid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);

                string Network = msgUtil.GetParamValue(WorkFields.Network, methparams);
                string SchemaTable = msgUtil.GetParamValue(WorkFields.SchemaTable, methparams);
                string SrcDBName = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string WorkType = msgUtil.GetParamValue(WorkFields.WorkType, methparams);
                string SrcURL = msgUtil.GetParamValue(WorkFields.SrcURL, methparams);
                string SrcMethod = msgUtil.GetParamValue(WorkFields.SrcMethod, methparams);
                string DestURL = msgUtil.GetParamValue(WorkFields.DestURL, methparams);
                string DestMethod = msgUtil.GetParamValue(WorkFields.DestMethod, methparams);
                string StartID = msgUtil.GetParamValue(WorkFields.StartID, methparams);
                string FinalID = msgUtil.GetParamValue(WorkFields.FinalID, methparams);
                string IntervalType = msgUtil.GetParamValue(WorkFields.IntervalType, methparams);
                string IntervalVal = msgUtil.GetParamValue(WorkFields.IntervalVal, methparams);
                string NextRun = msgUtil.GetParamValue(WorkFields.NextRun, methparams);
                string RunState = msgUtil.GetParamValue(WorkFields.RunState, methparams);
                string Logging = msgUtil.GetParamValue(WorkFields.Logging, methparams);
                string MaxDurMS = msgUtil.GetParamValue(WorkFields.MaxDurMS, methparams);
                string ResetCount = msgUtil.GetParamValue(WorkFields.ResetCount, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (rec_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }
                if (Network == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Network; ";
                }
                if (SchemaTable == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaTable; ";
                }
                if (SrcDBName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcDBName; ";
                }
                if (WorkType == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing WorkType; ";
                }
                if (SrcURL == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcURL; ";
                }
                if (SrcMethod == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcMethod; ";
                }
                if (DestURL == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DestURL; ";
                }
                if (DestMethod == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DestMethod; ";
                }
                if (StartID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing StartID; ";
                }
                if (FinalID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FinalID; ";
                }
                if (IntervalType == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing IntervalType; ";
                }
                if (IntervalVal == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing IntervalVal; ";
                }
                if (NextRun == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing NextRun; ";
                }
                if (RunState == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RunState; ";
                }
                if (Logging == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Logging; ";
                }
                if (MaxDurMS == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MaxDurMS; ";
                }
                if (ResetCount == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ResetCount; ";
                }

                if (reqFields)
                {
                    GeneralWork_dml genWork_Dml = new GeneralWork_dml(_connstr);
                    string rescode = genWork_Dml.Save(action,
                                                    rec_gid,
                                                    userinfo.UserGID,
                                                    Network,
                                                    SchemaTable,
                                                    SrcDBName,
                                                    ShardName,
                                                    WorkType,
                                                    SrcURL,
                                                    SrcMethod,
                                                    DestURL,
                                                    DestMethod,
                                                    StartID,
                                                    FinalID,
                                                    IntervalType,
                                                    IntervalVal,
                                                    NextRun,
                                                    RunState,
                                                    Logging,
                                                    MaxDurMS,
                                                    ResetCount);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, rescode);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, rescode);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }
    }
}
