using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using SysMetricsDB;

namespace DGPMetricsAPI.DGPMetrics
{
    public class DGPMetrics_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public DGPMetrics_mapper(string connStr)
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
                    DGPMetrics_dml dgpMetrics_Dml = new DGPMetrics_dml(_connstr);
                    results = dgpMetrics_Dml.GetByID(row_gid);

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
                string appname = msgUtil.GetParamValue(LogFields.AppName, methparams);
                string formname = msgUtil.GetParamValue(LogFields.FormName, methparams);
                string websvcname = msgUtil.GetParamValue(LogFields.WebSvcName, methparams);

                DGPMetrics_dml dgpMetrics_Dml = new DGPMetrics_dml(_connstr);
                string rowcount = dgpMetrics_Dml.GetCount(appname, formname, websvcname);

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
                string appname = msgUtil.GetParamValue(LogFields.AppName, methparams);
                string formname = msgUtil.GetParamValue(LogFields.FormName, methparams);
                string websvcname = msgUtil.GetParamValue(LogFields.WebSvcName, methparams);

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
                    DGPMetrics_dml dgpMetrics_Dml = new DGPMetrics_dml(_connstr);
                    results = dgpMetrics_Dml.GetSearch(pagenum, _pagesize, appname, formname, websvcname);

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
                    DGPMetrics_dml dgpMetrics_Dml = new DGPMetrics_dml(_connstr);
                    results = dgpMetrics_Dml.GetAll();

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
        public string AddDGPMetric(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();
                string row_gid = cmnUtil.GetNewGID();

                string username = userinfo.UserName;
                string compname = msgUtil.GetParamValue(LogFields.CompName, methparams);
                string appname = msgUtil.GetParamValue(LogFields.AppName, methparams);
                string formname = msgUtil.GetParamValue(LogFields.FormName, methparams);
                string svcname = msgUtil.GetParamValue(LogFields.WebSvcName, methparams);
                string svcver = msgUtil.GetParamValue(LogFields.WebSvcVer, methparams);
                string clienttime = msgUtil.GetParamValue(LogFields.ClientTime, methparams);
                string methcount = msgUtil.GetParamValue(LogFields.MethodCount, methparams);
                string clientms = msgUtil.GetParamValue(LogFields.EndToEndMS, methparams);
                string networkms = msgUtil.GetParamValue(LogFields.NetworkMS, methparams);
                string serverms = msgUtil.GetParamValue(LogFields.ServerMS, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (username == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing UserName; ";
                }
                if (compname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing CompName; ";
                }
                if (appname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing AppName; ";
                }
                if (formname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FormName; ";
                }
                if (svcname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing WebSvcName; ";
                }
                if (svcver == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing WebSvcVer; ";
                }
                if (clienttime == "") // datetime
                {
                    reqFields = false;
                    reqFieldNames += "Missing ClientTime; ";
                }
                else
                {
                    var isDate = DateTime.TryParse(clienttime, out DateTime d);
                    if (!isDate)
                    {
                        reqFields = false;
                        reqFieldNames += "ClientTime must be a valid DateTime; ";
                    }
                }
                if (methcount == "") // int
                {
                    reqFields = false;
                    reqFieldNames += "Missing MethodCount; ";
                }
                else
                {
                    var isNumeric = int.TryParse(methcount, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "MethodCount must be an int; ";
                    }
                }
                if (clientms == "") // double
                {
                    reqFields = false;
                    reqFieldNames += "Missing EndToEndMS; ";
                }
                else
                {
                    var isNumeric = double.TryParse(clientms, out double n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "EndToEndMS must be a double; ";
                    }
                }
                if (networkms == "") // double
                {
                    reqFields = false;
                    reqFieldNames += "Missing NetworkMS; ";
                }
                else
                {
                    var isNumeric = double.TryParse(networkms, out double n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "NetworkMS must be a double; ";
                    }
                }
                if (serverms == "") // double
                {
                    reqFields = false;
                    reqFieldNames += "Missing ServerMS; ";
                }
                else
                {
                    var isNumeric = double.TryParse(serverms, out double n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "ServerMS must be a double; ";
                    }
                }

                if (reqFields)
                {

                    DGPMetrics_dml dgpMetrics_Dml = new DGPMetrics_dml(_connstr);
                    string tmpcode = dgpMetrics_Dml.Write(row_gid,
                                                                username,
                                                                compname,
                                                                appname,
                                                                formname,
                                                                svcname,
                                                                svcver,
                                                                clienttime,
                                                                methcount,
                                                                clientms,
                                                                networkms,
                                                                serverms);

                    if (tmpcode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, row_gid);
                    }
                    else
                    {
                        string errmsg = "A problem occurred saving a new DGPMetrics record.";
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, errmsg);
                        ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Replication error.", errmsg);
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
        public string AddServerMetric(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string username = userinfo.UserName;
                string svcname = ConfigurationManager.AppSettings[LogFields.WebSvcName].ToString();
                string svcver = ConfigurationManager.AppSettings["WebSvcVersion"].ToString();

                // NOTE: requires ASP.NET domain account to be added to the Performance Monitor Users group on the computer
                ServerMetricsProc serverMetrics = new ServerMetricsProc(_connstr);
                string tmpcode = serverMetrics.StoreServerMetrics(username, svcname, svcver);

                if (tmpcode == APIResult.OK)
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, tmpcode);
                }
                else
                {
                    string errmsg = "A problem occurred saving a new ServerMetrics record.";
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, errmsg);
                    ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Replication error.", errmsg);
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
                    DGPMetrics_dml metrics_Dml = new DGPMetrics_dml(_connstr);
                    string tmpcode = metrics_Dml.Delete(row_gid);

                    if (tmpcode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, tmpcode);
                    }
                    else
                    {
                        string errmsg = "A problem occurred deleting a DGPMetrics record.";
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
