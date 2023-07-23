using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB.GroupUsers;
using SysInfoDB.APIUser;
using SysInfoDB.RoleUsers;
using SysMetricsDB;

namespace DGPAdminAPI.Self
{
    public class UserSelf_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public UserSelf_mapper(string connStr)
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
        public string Login(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                // clear cached userinfo object if it exists
                if (HttpContext.Current.Cache[userinfo.UserName] != null)
                {
                    HttpContext.Current.Cache.Remove(userinfo.UserName);
                }

                // update the authorization data cached in the user record
                UserProc userProc = new UserProc(_connstr);
                string result = userProc.UpdateUserAuthorization(userinfo.UserGID);

                // create consolidated collection of response values
                string resptime = msgUtil.UnixTimeString();
                EncryptUtil encryptUtil = new EncryptUtil();
                string resphash = encryptUtil.GetHMACHash(userinfo.Password, resptime);

                RoleUsers_dml apiRoles_Dml = new RoleUsers_dml(_connstr);
                DataTable rolelist = apiRoles_Dml.GetAssigned(userinfo.UserGID);

                string roles = "";
                if (rolelist.Rows.Count > 0)
                {
                    string delim = "";
                    foreach (DataRow dr in rolelist.Rows)
                    {
                        roles += delim + dr["RoleName"].ToString();
                        delim = ",";
                    }
                }

                string loginResult = "<LoginResult><WebSvcName>" + ConfigurationManager.AppSettings["WebSvcName"].ToString() + "</WebSvcName>" +
                                    "<WebSvcVer>" + ConfigurationManager.AppSettings["WebSvcVersion"].ToString() + "</WebSvcVer>" +
                                    "<LocState>" + ConfigurationManager.AppSettings["LocState"].ToString() + "</LocState>" +
                                    "<MaxFileSize>" + ConfigurationManager.AppSettings["MaxFileSize"].ToString() + "</MaxFileSize>" +
                                    "<MaxSegSize>" + ConfigurationManager.AppSettings["MaxSegSize"].ToString() + "</MaxSegSize>" +
                                    "<MinPwordLen>" + ConfigurationManager.AppSettings["PasswordLength"].ToString() + "</MinPwordLen>" +
                                    "<RespTime>" + resptime + "</RespTime>" +
                                    "<RespToken>" + resphash + "</RespToken>" +
                                    "<UserGID>" + userinfo.UserGID + "</UserGID>" + 
                                    "<ReadGroups>" + userinfo.ReadList + "</ReadGroups>" +
                                    "<WriteGroups>" + userinfo.WriteList + "</WriteGroups>" +
                                    "<UserRoles>" + roles + "</UserRoles></LoginResult>";

                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, loginResult);
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
        public string GetRoles(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                RoleUsers_dml apiRoles_Dml = new RoleUsers_dml(_connstr);
                DataTable rolelist = apiRoles_Dml.GetAssigned(userinfo.UserGID);

                if (rolelist.Rows.Count > 0)
                {
                    string roles = "";
                    string delim = "";
                    foreach (DataRow dr in rolelist.Rows)
                    {
                        roles += delim + dr["RoleName"].ToString();
                        delim = ",";
                    }

                    resultxml += msgUtil.CreateXMLResult(methodname, "RoleList", APIResult.OK, APIData.Text, roles);
                }
                else
                {
                    resultxml += msgUtil.CreateXMLResult(methodname, "RoleList", APIResult.Empty, APIData.Text, "");
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
        public string GetGroups(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                resultxml = msgUtil.CreateXMLResult(methodname, "ReadList", APIResult.OK, APIData.Text, userinfo.ReadList);
                resultxml += msgUtil.CreateXMLResult(methodname, "WriteList", APIResult.OK, APIData.Text, userinfo.WriteList);
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
        public string GetUserGroupList(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
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
                    GroupUsers_dml groupuser = new GroupUsers_dml(_connstr);
                    DataTable grptable = groupuser.GetAssigned(userinfo.UserGID);

                    if (grptable.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string groupstr = cmnUtil.TableToXml(grptable, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, groupstr);
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
        public string GetInfo(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
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
                    APIUser_read_dml apiUser_Dml = new APIUser_read_dml(_connstr);
                    DataTable usertbl = apiUser_Dml.GetByID(userinfo.UserGID);

                    if (usertbl != null && usertbl.Rows.Count > 0)
                    {
                        usertbl.Columns.Remove(APIUserFields.Password);
                        usertbl.Columns.Remove(APIUserFields.AccountType);
                        usertbl.Columns.Remove(APIUserFields.AccountState);
                        usertbl.Columns.Remove(APIUserFields.MethodList);
                        usertbl.Columns.Remove(APIUserFields.ReadList);
                        usertbl.Columns.Remove(APIUserFields.WriteList);
                        usertbl.Columns.Remove(APIUserFields.MethodLimit);
                       
                        CmnUtil cmnUtil = new CmnUtil();
                        string userstr = cmnUtil.TableToXml(usertbl, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, userstr);
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
        public string Save(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string FirstName = msgUtil.GetParamValue(APIUserFields.FirstName, methparams);
                string MiddleName = msgUtil.GetParamValue(APIUserFields.MiddleName, methparams);
                string LastName = msgUtil.GetParamValue(APIUserFields.LastName, methparams);
                string Email = msgUtil.GetParamValue(APIUserFields.Email, methparams);

                bool reqFields = true;
                string reqFieldNames = "";

                if (FirstName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FirstName; ";
                }
                if (MiddleName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing MiddleName; ";
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

                if (reqFields)
                {
                    string new_row_ms = msgUtil.UnixTimeString();
                    UserProc userProc = new UserProc(_connstr);
                    string rescode = userProc.UpdateSelf(userinfo.UserGID, new_row_ms, FirstName, MiddleName, LastName, Email);

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
        /// user can only change their own password
        /// </summary>
        public string ChangePassword(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string username = userinfo.UserName;
                string password = msgUtil.GetParamValue(APIUserFields.Password, methparams);

                bool reqFields = true;
                string reqFieldNames = "";

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

    }
}
