using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB.APIRoles;
using SysMetricsDB;

namespace DGPAdminAPI.Role
{
    public class APIRole_write_mapper
    {
        string _connstr;
        string _appname;

        public APIRole_write_mapper(string connStr)
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

                string rolegid = cmnUtil.GetNewGID();
                string new_row_ms = msgUtil.UnixTimeString();

                string RoleName = msgUtil.GetParamValue(APIRoleFields.RoleName, methparams);
                string RoleDescrip = msgUtil.GetParamValue(APIRoleFields.RoleDescrip, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (RoleName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RoleName; ";
                }
                if (RoleDescrip == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RoleDescrip; ";
                }

                if (reqFields)
                {
                    APIRoles_write_dml roles_write_Dml = new APIRoles_write_dml(_connstr);
                    string rescode = roles_write_Dml.Write(ReplicaAction.Insert, rolegid, new_row_ms, "", userinfo.UserGID, RoleName, RoleDescrip);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, rolegid);
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
                string rolegid = msgUtil.GetParamValue(APIRoleFields.RoleGID, methparams);
                string RoleName = msgUtil.GetParamValue(APIRoleFields.RoleName, methparams);
                string RoleDescrip = msgUtil.GetParamValue(APIRoleFields.RoleDescrip, methparams);
                string edit_ms = msgUtil.GetParamValue(CommonFields.row_ms, methparams);
                string new_row_ms = msgUtil.UnixTimeString();

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (rolegid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RoleGID; ";
                }
                if (RoleName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RoleName; ";
                }
                if (RoleDescrip == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing RoleDescrip; ";
                }
                if (edit_ms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_ms; ";
                }

                if (reqFields)
                {
                    APIRoles_write_dml roles_write_Dml = new APIRoles_write_dml(_connstr);
                    string rescode = roles_write_Dml.Write(action, rolegid, new_row_ms, edit_ms, userinfo.UserGID, RoleName, RoleDescrip);

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

                if (recgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }
                if (rowid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_id; ";
                }
                if (action == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing action value; ";
                }

                if (reqFields)
                {
                    string new_row_ms = msgUtil.UnixTimeString();
                    RoleProc roleProc = new RoleProc(_connstr);
                    string rescode = roleProc.RecoverRole(action, recgid, rowid, new_row_ms);

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
                        APIRoles_write_dml roles_write_Dml = new APIRoles_write_dml(_connstr);

                        string procstate = "";
                        foreach (DataRow dr in srcRecs.Rows)
                        {
                            string row_id = dr[CommonFields.row_id].ToString();
                            string rec_dbname = dr[CommonFields.rec_dbname].ToString();
                            string src_ms = dr[CommonFields.src_ms].ToString();
                            string rec_gid = dr[CommonFields.rec_gid].ToString();
                            string rec_state = dr[CommonFields.rec_state].ToString();
                            string RoleName = dr[APIRoleFields.RoleName].ToString();
                            string RoleDescrip = dr[APIRoleFields.RoleDescrip].ToString();

                            procstate = roles_write_Dml.Replicate(row_id, src_ms, rec_dbname, rec_gid, rec_state, userinfo.UserGID, RoleName, RoleDescrip, _connstr);

                            if (procstate == APIResult.OK || procstate == APIResult.Done)
                            {
                                newplaceholder = row_id;
                            }
                            else
                            {
                                string errmsg = "APIRole replication error for row " + row_id + " : " + procstate;
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
