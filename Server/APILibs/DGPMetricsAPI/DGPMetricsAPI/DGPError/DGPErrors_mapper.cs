using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using SysMetricsDB;

namespace DGPMetricsAPI.DGPErrors
{
    public class DGPErrors_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public DGPErrors_mapper(string connStr)
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
                    DGPErrors_dml dgpErrors_Dml = new DGPErrors_dml(_connstr);
                    results = dgpErrors_Dml.GetByID(row_gid);

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
                string classname = msgUtil.GetParamValue(LogFields.ClassName, methparams);
                string errlevel = msgUtil.GetParamValue(LogFields.ErrLevel, methparams);

                DGPErrors_dml dgperrors_Dml = new DGPErrors_dml(_connstr);
                string rowcount = dgperrors_Dml.GetCount(appname, classname, errlevel);

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
                string classname = msgUtil.GetParamValue(LogFields.ClassName, methparams);
                string errlevel = msgUtil.GetParamValue(LogFields.ErrLevel, methparams);

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
                    DGPErrors_dml dgperrors_Dml = new DGPErrors_dml(_connstr);
                    results = dgperrors_Dml.GetSearch(pagenum, _pagesize, appname, classname, errlevel);

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
        public string GetErrData(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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
                    DGPErrors_dml dgpErrors_Dml = new DGPErrors_dml(_connstr);
                    results = dgpErrors_Dml.GetErrData(row_gid);

                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, results);
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
                    DGPErrors_dml dgpErrors_Dml = new DGPErrors_dml(_connstr);
                    results = dgpErrors_Dml.GetAll();

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
        public string AddDGPError(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();
                string row_gid = cmnUtil.GetNewGID();

                string username = msgUtil.GetParamValue(APIUserFields.UserName, methparams);
                string compname = msgUtil.GetParamValue(LogFields.CompName, methparams);
                string appname = msgUtil.GetParamValue(LogFields.AppName, methparams);
                string classname = msgUtil.GetParamValue(LogFields.ClassName, methparams);
                string errloc = msgUtil.GetParamValue(LogFields.MsgLoc, methparams);
                string errlevel = msgUtil.GetParamValue(LogFields.ErrLevel, methparams);
                string errmessage = msgUtil.GetParamValue(LogFields.ErrMessage, methparams);
                string errdata = msgUtil.GetParamValue(LogFields.ErrData, methparams);

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
                if (classname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ClassName; ";
                }
                if (errloc == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MsgLoc; ";
                }
                if (errlevel == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ErrLevel; ";
                }
                if (errmessage == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ErrMessage; ";
                }
                if (errdata == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ErrData; ";
                }

                if (reqFields)
                {
                    DGPErrors_dml dgpErrors_Dml = new DGPErrors_dml(_connstr);
                    string tmpcode = dgpErrors_Dml.Write(row_gid,
                                                        username,
                                                        compname,
                                                        appname,
                                                        classname,
                                                        errloc,
                                                        errlevel,
                                                        errmessage,
                                                        errdata);

                    if (tmpcode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, row_gid);
                    }
                    else
                    {
                        string errmsg = "A problem occurred adding a new DGPErrors record.";
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
                    DGPErrors_dml errors_Dml = new DGPErrors_dml(_connstr);
                    string tmpcode = errors_Dml.Delete(row_gid);

                    if (tmpcode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, tmpcode);
                    }
                    else
                    {
                        string errmsg = "A problem occurred deleting a DGPErrors record.";
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
