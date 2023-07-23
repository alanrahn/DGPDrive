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
    public class ReplicaWork_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public ReplicaWork_mapper(string connStr)
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
                    reqFieldNames += "Missing WorkGID; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    results = repWork_Dml.GetByID(rec_gid);

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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    results = repWork_Dml.GetAll();

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
                string SchemaTable = msgUtil.GetParamValue(WorkFields.SchemaTable, methparams);

                ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);

                string latestcount = repWork_Dml.GetCount(SchemaTable);

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
                //string recstate = msgUtil.GetParamValue(CommonFields.rec_state, methparams);

                // search values (optional)
                string SchemaTable = msgUtil.GetParamValue(WorkFields.SchemaTable, methparams);

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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    results = repWork_Dml.GetSearch(pagenum, _pagesize, SchemaTable);

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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    results = repWork_Dml.QueueScan();

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
                string claimedBy= msgUtil.GetParamValue(WorkFields.ClaimedBy, methparams);
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
                if (claimedBy == "")
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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    results = repWork_Dml.ClaimReplicaWorkRecs(network, claimedBy);

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
                string placeholder = msgUtil.GetParamValue(WorkFields.StartID, methparams);
                string runstate = msgUtil.GetParamValue(WorkFields.RunState, methparams);
                string statemsg = msgUtil.GetParamValue(WorkFields.StateMsg, methparams);
                string claimedby = msgUtil.GetParamValue(WorkFields.ClaimedBy, methparams);
                string claimid = msgUtil.GetParamValue(WorkFields.ClaimID, methparams);
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
                if (placeholder == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing StartID; ";
                }
                if (runstate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RunState; ";
                }
                if (claimedby == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ClaimedBy; ";
                }
                if (claimid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ClaimID; ";
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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    string rescode = repWork_Dml.ReleaseReplicaWorkRec(rec_gid, nextRun, runstate, statemsg, placeholder, claimedby, claimid, procstate, resetcount);

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
                string SchemaType = msgUtil.GetParamValue(WorkFields.SchemaType, methparams);
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
                string BatchSize = msgUtil.GetParamValue(WorkFields.BatchSize, methparams);
                string IntervalMS = msgUtil.GetParamValue(WorkFields.IntervalMS, methparams);
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
                if (SchemaType == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaType; ";
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
                if (BatchSize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing BatchSize; ";
                }
                if (IntervalMS == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing IntervalMS; ";
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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    string rescode = repWork_Dml.New(rec_gid,
                                                    userinfo.UserGID,
                                                    Network,
                                                    SchemaType,
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
                                                    BatchSize,
                                                    IntervalMS,
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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    string rescode = repWork_Dml.Clone(rec_gid, userinfo.UserGID, newgid);

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
                string SchemaType = msgUtil.GetParamValue(WorkFields.SchemaType, methparams);
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
                string BatchSize = msgUtil.GetParamValue(WorkFields.BatchSize, methparams);
                string IntervalMS = msgUtil.GetParamValue(WorkFields.IntervalMS, methparams);
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
                    reqFieldNames += "Missing WorkGID; ";
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
                if (BatchSize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing BatchSize; ";
                }
                if (IntervalMS == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing IntervalMS; ";
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
                    ReplicaWork_dml repWork_Dml = new ReplicaWork_dml(_connstr);
                    string rescode = repWork_Dml.Save(action,
                                                    rec_gid,
                                                    userinfo.UserGID,
                                                    Network,
                                                    SchemaType,
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
                                                    BatchSize,
                                                    IntervalMS,
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
