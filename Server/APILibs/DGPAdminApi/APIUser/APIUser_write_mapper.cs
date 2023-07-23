using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB.APIUser;
using SysMetricsDB;

namespace DGPAdminAPI.User
{
    public class APIUser_write_mapper
    {
        string _connstr;
        string _appname;

        public APIUser_write_mapper(string connStr)
        {
            _connstr = connStr;
            _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
        }

        /// <summary>
        /// New method can directly call the low level apiuser_dml.Write method
        /// </summary>
        public string New(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();

                bool reqFields = true;
                string reqFieldNames = "";
                string pwordcheck = "ERROR";

                string Password = msgUtil.GetParamValue(APIUserFields.Password, methparams);

                if (Password == null || Password == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Password; ";
                }
                else
                {
                    pwordcheck = cmnUtil.PasswordCheck(Password);
                }

                if (pwordcheck == APIResult.OK)
                {
                    string newgid = cmnUtil.GetNewGID();
                    string new_row_ms = msgUtil.UnixTimeString();

                    string UserName = msgUtil.GetParamValue(APIUserFields.UserName, methparams);
                    string FirstName = msgUtil.GetParamValue(APIUserFields.FirstName, methparams);
                    string MiddleName = msgUtil.GetParamValue(APIUserFields.MiddleName, methparams);
                    string LastName = msgUtil.GetParamValue(APIUserFields.LastName, methparams);
                    string Email = msgUtil.GetParamValue(APIUserFields.Email, methparams);
                    string AccountType = msgUtil.GetParamValue(APIUserFields.AccountType, methparams);
                    string AccountState = msgUtil.GetParamValue(APIUserFields.AccountState, methparams);
                    string Expiration = msgUtil.GetParamValue(APIUserFields.ExpireDate, methparams);
                    string MethodLimit = msgUtil.GetParamValue(APIUserFields.MethodLimit, methparams);

                    string svcKeyVersion = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();

                    // test for required input parameters
                    if (UserName == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing UserName; ";
                    }
                    if (FirstName == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing FirstName; ";
                    }
                    if (LastName == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing LastName; ";
                    }
                    if (Email == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing Email; ";
                    }
                    if (AccountType == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing AccountType; ";
                    }
                    if (AccountState == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing AccountState; ";
                    }
                    if (Expiration == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing Expiration; ";
                    }
                    else
                    {
                        var isNumeric = long.TryParse(Expiration, out long n);
                        if (!isNumeric)
                        {
                            reqFields = false;
                            reqFieldNames += "Expiration must be a long; ";
                        }
                    }
                    if (MethodLimit == "")
                    {
                        reqFields = false;
                        reqFieldNames += "Missing MethodLimit; ";
                    }
                    else
                    {
                        var isNumeric = int.TryParse(MethodLimit, out int n);
                        if (!isNumeric)
                        {
                            reqFields = false;
                            reqFieldNames += "MethodLimit must be an int; ";
                        }
                    }

                    if (reqFields)
                    {
                        // combine lowercase UserName and case-sensitive Password values
                        string pword = UserName.ToLower() + Password;

                        UserProc userProc = new UserProc(_connstr);
                        string rescode = userProc.NewUser(newgid, userinfo.UserGID, new_row_ms, UserName, pword, AccountType.ToUpper(), AccountState.ToUpper(), Expiration, FirstName, MiddleName, LastName, Email, MethodLimit, svcKeyVersion);

                        if (rescode != null && rescode == APIResult.OK)
                        {
                            resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, newgid);
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
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " Password Check: " + pwordcheck);
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
        /// cannot change username, password, methodlist, readlist, writelist (call the userproc.Save method)
        /// </summary>
        public string Save(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();

                string new_row_ms = msgUtil.UnixTimeString();

                bool reqFields = true;
                string reqFieldNames = "";

                string recgid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
                string FirstName = msgUtil.GetParamValue(APIUserFields.FirstName, methparams);
                string MiddleName = msgUtil.GetParamValue(APIUserFields.MiddleName, methparams);
                string LastName = msgUtil.GetParamValue(APIUserFields.LastName, methparams);
                string Email = msgUtil.GetParamValue(APIUserFields.Email, methparams);
                string AccountType = msgUtil.GetParamValue(APIUserFields.AccountType, methparams);
                string AccountState = msgUtil.GetParamValue(APIUserFields.AccountState, methparams);
                string Expiration = msgUtil.GetParamValue(APIUserFields.ExpireDate, methparams);
                string MethodLimit = msgUtil.GetParamValue(APIUserFields.MethodLimit, methparams);

                string svcKeyVersion = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();

                // test for required input parameters
                if (recgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }
                if (FirstName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FirstName; ";
                }
                if (LastName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing LastName; ";
                }
                if (Email == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Email; ";
                }
                if (AccountType == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing AccountType; ";
                }
                if (AccountState == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing AccountState; ";
                }
                if (Expiration == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Expiration; ";
                }
                if (MethodLimit == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MethodLimit; ";
                }

                if (reqFields)
                {
                    // call the userproc method to only allow certain fields to be updated
                    UserProc userProc = new UserProc(_connstr);
                    string rescode = userProc.UpdateUser(action, recgid, userinfo.UserGID, new_row_ms, AccountType.ToUpper(), AccountState.ToUpper(), Expiration, FirstName, MiddleName, LastName, Email, MethodLimit, svcKeyVersion);

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
        /// cannot change username, password, methodlist, readlist, writelist (call the userproc.Save method)
        /// </summary>
        public string Recover(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();

                bool reqFields = true;
                string reqFieldNames = "";

                string action = msgUtil.GetParamValue(CommonFields.Action, methparams);
                string recgid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
                string rowid = msgUtil.GetParamValue(CommonFields.row_id, methparams);

                // test for required input parameters
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
                    // call the userproc method to only allow certain fields to be updated
                    string new_row_ms = msgUtil.UnixTimeString();
                    UserProc userProc = new UserProc(_connstr);
                    string rescode = userProc.RecoverUser(action, recgid, rowid, new_row_ms);

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
        public string ChangePassword(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string username = msgUtil.GetParamValue(APIUserFields.UserName, methparams);
                string password = msgUtil.GetParamValue(APIUserFields.Password, methparams);

                bool reqFields = true;
                string reqFieldNames = "";

                if (username == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing UserName; ";
                }
                if (password == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Password; ";
                }

                if (reqFields)
                {
                    CmnUtil cmnUtil = new CmnUtil();
                    string pwordcheck = cmnUtil.PasswordCheck(password);

                    if (pwordcheck == "OK")
                    {
                        string new_row_ms = msgUtil.UnixTimeString();
                        string expiredays = ConfigurationManager.AppSettings["ExpireDays"].ToString();
                        string expiredate = msgUtil.UnixTimeString(Convert.ToInt32(expiredays));

                        UserProc userProc = new UserProc(_connstr);
                        string rescode = userProc.ChangePassword(new_row_ms, username, password, expiredate);

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
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, pwordcheck);
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
                    reqFieldNames += "Missing Placeholder value; ";
                }

                if (reqFields)
                {
                    CmnUtil cmnUtil = new CmnUtil();
                    DataTable srcRecs = cmnUtil.XmlToTable(srcrecs);

                    if (srcRecs.Rows.Count > 0)
                    {
                        APIUser_write_dml user_write_Dml = new APIUser_write_dml(_connstr);

                        string procstate = "";
                        foreach (DataRow dr in srcRecs.Rows)
                        {
                            string row_id = dr[CommonFields.row_id].ToString();
                            string rec_dbname = dr[CommonFields.rec_dbname].ToString();
                            string src_ms = dr[CommonFields.src_ms].ToString();
                            string rec_gid = dr[CommonFields.rec_gid].ToString();
                            string rec_state = dr[CommonFields.rec_state].ToString();
                            string UserName = dr[APIUserFields.UserName].ToString();
                            string Password = dr[APIUserFields.Password].ToString();
                            string AccountType = dr[APIUserFields.AccountType].ToString();
                            string AccountState = dr[APIUserFields.AccountState].ToString();
                            string ExpireDate = dr[APIUserFields.ExpireDate].ToString();
                            string FirstName = dr[APIUserFields.FirstName].ToString();
                            string MiddleName = dr[APIUserFields.MiddleName].ToString();
                            string LastName = dr[APIUserFields.LastName].ToString();
                            string Email = dr[APIUserFields.Email].ToString();
                            string MethodList = dr[APIUserFields.MethodList].ToString();
                            string ReadList = dr[APIUserFields.ReadList].ToString();
                            string WriteList = dr[APIUserFields.WriteList].ToString();
                            string MethodLimit = dr[APIUserFields.MethodLimit].ToString();
                            string SvcKeyVersion = dr[APIUserFields.SvcKeyVersion].ToString();

                            procstate = user_write_Dml.Replicate(row_id, src_ms, rec_dbname, rec_gid, rec_state, userinfo.UserGID, UserName, Password, AccountType, AccountState, ExpireDate, FirstName, MiddleName, LastName, Email, MethodList, ReadList, WriteList, MethodLimit, SvcKeyVersion, _connstr);

                            if (procstate == APIResult.OK || procstate == APIResult.Done)
                            {
                                newplaceholder = row_id;
                            }
                            else
                            {
                                string errmsg = "APIUser replication error for row " + row_id + " : " + procstate;
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
