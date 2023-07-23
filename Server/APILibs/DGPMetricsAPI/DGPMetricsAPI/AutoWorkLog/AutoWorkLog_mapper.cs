using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using SysMetricsDB;

namespace DGPMetricsAPI
{
    public class AutoWorkLog_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public AutoWorkLog_mapper(string connStr)
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
                string row_gid = msgUtil.GetParamValue(CommonFields.row_gid, methparams);

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
                if (row_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_gid; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    AutoWorkLog_dml autoWorkLog_Dml = new AutoWorkLog_dml(_connstr);
                    results = autoWorkLog_Dml.GetByID(row_gid);

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
                    AutoWorkLog_dml autoWorkLog_Dml = new AutoWorkLog_dml(_connstr);
                    results = autoWorkLog_Dml.GetAll();

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
                string worktype = msgUtil.GetParamValue(WorkFields.WorkType, methparams);
                string runstate = msgUtil.GetParamValue(WorkFields.RunState, methparams);
                string procstate = msgUtil.GetParamValue(WorkFields.ProcState, methparams);

                AutoWorkLog_dml autowork_dml = new AutoWorkLog_dml(_connstr);
                string rowcount = autowork_dml.GetCount(worktype, runstate, procstate);

                if (rowcount != null && rowcount != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, rowcount);
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
                string worktype = msgUtil.GetParamValue(WorkFields.WorkType, methparams);
                string runstate = msgUtil.GetParamValue(WorkFields.RunState, methparams);
                string procstate = msgUtil.GetParamValue(WorkFields.ProcState, methparams);

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
                    AutoWorkLog_dml autowork_dml = new AutoWorkLog_dml(_connstr);
                    results = autowork_dml.GetSearch(pagenum, _pagesize, worktype, runstate, procstate);

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
        public string GetProcSteps(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            string results = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string row_gid = msgUtil.GetParamValue(CommonFields.row_gid, methparams);

                bool reqFields = true;
                string reqFieldNames = "";

                if (row_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_gid;";
                }

                if (reqFields)
                {
                    AutoWorkLog_dml autowork_dml = new AutoWorkLog_dml(_connstr);
                    results = autowork_dml.GetProcSteps(row_gid);

                    if (results != null && results != "")
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, results);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, "Problem querying for ProcSteps data.");
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
        public string New(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();
                string row_gid = cmnUtil.GetNewGID();

                string worktype = msgUtil.GetParamValue(WorkFields.WorkType, methparams);
                string compname = msgUtil.GetParamValue(LogFields.CompName, methparams);
                string claimedby = msgUtil.GetParamValue(WorkFields.ClaimedBy, methparams);
                string durationms = msgUtil.GetParamValue(LogFields.DurationMS, methparams);
                string maxdurms = msgUtil.GetParamValue(WorkFields.MaxDurMS, methparams);
                string runstate = msgUtil.GetParamValue(WorkFields.RunState, methparams);
                string statemsg = msgUtil.GetParamValue(WorkFields.StateMsg, methparams);
                string procstate = msgUtil.GetParamValue(WorkFields.ProcState, methparams);
                string procsteps = msgUtil.GetParamValue(WorkFields.ProcSteps, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (worktype == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing WorkType; ";
                }
                if (compname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing CompName; ";
                }
                if (claimedby == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ClaimedBy; ";
                }
                if (durationms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DurationMS; ";
                }
                if (maxdurms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MaxDurMS; ";
                }
                //if (runstate == "")
                //{
                //    reqFields = false;
                //    reqFieldNames += "Missing RunState; ";
                //}
                //if (statemsg == "")
                //{
                //    reqFields = false;
                //    reqFieldNames += "Missing StateMsg; ";
                //}
                if (procstate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ProcState; ";
                }
                if (procsteps == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ProcSteps; ";
                }

                if (reqFields)
                {
                    AutoWorkLog_dml autoWorkLog_Dml = new AutoWorkLog_dml(_connstr);
                    string tmpcode = autoWorkLog_Dml.Write(row_gid,
                                                            worktype,
                                                            compname,
                                                            claimedby,
                                                            durationms,
                                                            maxdurms,
                                                            runstate,
                                                            statemsg,
                                                            procstate,
                                                            procsteps);

                    if (tmpcode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, row_gid);
                    }
                    else
                    {
                        string errmsg = "A problem occurred saving a new AutoWorkLog record.";
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, errmsg);
                        ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Log insert error.", errmsg);
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
        /// Hard delete
        /// </summary>
        public string Delete(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string row_gid = msgUtil.GetParamValue(CommonFields.row_gid, methparams);

                bool reqFields = true;
                string reqFieldNames = "";

                if (row_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_gid;";
                }

                if (reqFields)
                {
                    AutoWorkLog_dml autoWorkLog_Dml = new AutoWorkLog_dml(_connstr);
                    string tmpcode = autoWorkLog_Dml.Delete(row_gid);

                    if (tmpcode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, tmpcode);
                    }
                    else
                    {
                        string errmsg = "A problem occurred deleting a AutoWorkLog record.";
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, errmsg);
                        ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Log delete error.", errmsg);
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
