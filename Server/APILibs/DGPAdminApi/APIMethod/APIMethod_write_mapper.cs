using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB.APIMethods;
using SysMetricsDB;

namespace DGPAdminAPI.APIMethod
{
    public class APIMethod_write_mapper
    {
        string _connstr;
        string _appname;

        public APIMethod_write_mapper(string connStr)
        {
            _connstr = connStr;
            _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
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

                string methodgid = cmnUtil.GetNewGID();
                string new_row_ms = msgUtil.UnixTimeString();

                string APIName = msgUtil.GetParamValue(APIMethodFields.APIName, methparams);
                string MethodName = msgUtil.GetParamValue(APIMethodFields.MethodName, methparams);
                string VersionName = msgUtil.GetParamValue(APIMethodFields.VersionName, methparams);
                string MethodDescrip = msgUtil.GetParamValue(APIMethodFields.MethodDescrip, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (APIName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing APIName; ";
                }
                if (MethodName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MethodName; ";
                }
                if (VersionName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing VersionName; ";
                }
                if (MethodDescrip == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MethodDescrip; ";
                }

                if (reqFields)
                {
                    APIMethods_write_dml methods_write_Dml = new APIMethods_write_dml(_connstr);
                    string rescode = methods_write_Dml.Write(ReplicaAction.Insert, methodgid, userinfo.UserGID, new_row_ms, "", APIName, MethodName, VersionName, MethodDescrip);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, methodgid);
                        resultxml += msgUtil.CreateXMLResult(methodname, "RowMS", APIResult.OK, APIData.Text, new_row_ms);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
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
                // APIName, MethodName and VersionName are immutable once created - can only edit the description value
                string APIName = "";
                string MethodName = "";
                string VersionName = "";

                string methodgid = msgUtil.GetParamValue(APIMethodFields.MethodGID, methparams);
                string MethodDescrip = msgUtil.GetParamValue(APIMethodFields.MethodDescrip, methparams);
                string edit_ms = msgUtil.GetParamValue(CommonFields.row_ms, methparams);
                string new_row_ms = msgUtil.UnixTimeString();

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (methodgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MethodGID; ";
                }
                if (MethodDescrip == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MethodDescrip; ";
                }
                if (edit_ms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_ms; ";
                }

                if (reqFields)
                {
                    APIMethods_read_dml methods_read_Dml = new APIMethods_read_dml(_connstr);
                    DataTable methodrec = methods_read_Dml.GetByID(methodgid);

                    if (methodrec.Rows.Count > 0)
                    {
                        foreach (DataRow dr in methodrec.Rows)
                        {
                            APIName = dr[APIMethodFields.APIName].ToString();
                            MethodName = dr[APIMethodFields.MethodName].ToString();
                            VersionName = dr[APIMethodFields.VersionName].ToString();
                        }

                        APIMethods_write_dml methods_write_Dml = new APIMethods_write_dml(_connstr);
                        string rescode = methods_write_Dml.Write(action, methodgid, userinfo.UserGID, new_row_ms, edit_ms, APIName, MethodName, VersionName, MethodDescrip);

                        if (rescode != null && rescode == APIResult.OK)
                        {
                            resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, methodname + ": successful.");
                            resultxml += msgUtil.CreateXMLResult(methodname, "RowMS", APIResult.OK, APIData.Text, new_row_ms);
                        }
                        else
                        {
                            resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
                        }
                    }
                    else
                    {
                        // unable to find existing record for GID value)
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": No existing record found for GID " + methodgid);
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
        public string Recover(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string action = msgUtil.GetParamValue(CommonFields.Action, methparams);
                string recgid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
                string rowid = msgUtil.GetParamValue(CommonFields.row_id, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (action == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing action value; ";
                }
                if (recgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid value; ";
                }
                if (rowid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_id value; ";
                }

                if (reqFields)
                {
                    string new_row_ms = msgUtil.UnixTimeString();
                    MethodProc methodProc = new MethodProc(_connstr);
                    string rescode = methodProc.RecoverMethod(action, recgid, rowid, new_row_ms);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, new_row_ms);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
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
        public string Replicate(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();
            string newplaceholder = "";

            try
            {
                string srcrecs = msgUtil.GetParamValue(CommonFields.SourceRecords, methparams);
                string placeholder = msgUtil.GetParamValue(WorkFields.StartID, methparams);
                newplaceholder = placeholder;

                bool reqFields = true;
                string reqFieldNames = "";

                if (srcrecs == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SourceRecords value; ";
                }
                if (placeholder == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing StartID value; ";
                }

                if (reqFields)
                {
                    CmnUtil cmnUtil = new CmnUtil();
                    DataTable srcRecs = cmnUtil.XmlToTable(srcrecs);

                    if (srcRecs.Rows.Count > 0)
                    {
                        APIMethods_write_dml methods_write_Dml = new APIMethods_write_dml(_connstr);

                        string procstate = "";
                        foreach (DataRow dr in srcRecs.Rows)
                        {
                            string row_id = dr[CommonFields.row_id].ToString();
                            string rec_dbname = dr[CommonFields.rec_dbname].ToString();
                            string src_ms = dr[CommonFields.src_ms].ToString();
                            string rec_gid = dr[CommonFields.rec_gid].ToString();
                            string rec_state = dr[CommonFields.rec_state].ToString();

                            string APIName = dr[APIMethodFields.APIName].ToString();
                            string MethodName = dr[APIMethodFields.MethodName].ToString();
                            string VersionName = dr[APIMethodFields.VersionName].ToString();
                            string MethodDescrip = dr[APIMethodFields.MethodDescrip].ToString();

                            procstate = methods_write_Dml.Replicate(row_id, src_ms, rec_dbname, rec_gid, rec_state, userinfo.UserGID, APIName, MethodName, VersionName, MethodDescrip, _connstr);

                            if (procstate == APIResult.OK || procstate == APIResult.Done)
                            {
                                newplaceholder = row_id;
                            }
                            else
                            {
                                string errmsg = "APIMethod replication error for row " + row_id + " : " + procstate;
                                ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Replication error.", errmsg);
                                break;
                            }
                        }

                        // return new placeholder value (errors will prevent the new placeholder value from advancing until corrected)
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, procstate, APIData.Text, newplaceholder);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, newplaceholder);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, newplaceholder);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, newplaceholder);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }
    }
}
