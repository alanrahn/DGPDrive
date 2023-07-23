using System;
using System.Text;
using System.Data;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB.GroupUsers;
using SysInfoDB.RoleMethods;
using SysInfoDB.RoleUsers;

namespace SysInfoDB.APIUser
{
    public class UserProc
    {
        string _connstr;

        public UserProc(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// used for authentication and authorization
        /// </summary>
        public UserInfo GetUser(string userName, string svcKey)
        {
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = userName;
            userinfo.MethodLimit = 0;

            APIUser_read_dml users = new APIUser_read_dml(_connstr);

            DataTable queryresults = users.GetByName(userName);

            // if account record is found
            if (queryresults != null && queryresults.Rows.Count > 0)
            {
                if (queryresults.Rows.Count == 1)
                {
                    // record found, populate userinfo object
                    DataRow user = queryresults.Rows[0];
 
                    // user account found
                    userinfo.UserGID = user["rec_gid"].ToString();
                    userinfo.RowDate = user["row_ms"].ToString();
                    userinfo.Password = user["Password"].ToString();
                    userinfo.AcctState = user["AccountState"].ToString().ToUpper();
                    userinfo.AcctType = user["AccountType"].ToString().ToUpper();
                    userinfo.MethList = user["MethodList"].ToString();
                    userinfo.ReadList = user["ReadList"].ToString();
                    userinfo.WriteList = user["WriteList"].ToString();
                    userinfo.MethodLimit = Convert.ToInt32(user["MethodLimit"].ToString());

                    MsgUtil msgUtil = new MsgUtil();
                    long currentTime = msgUtil.UnixTimeLong();
                    long expiration = Convert.ToInt64(user["ExpireDate"]);

                    // if account is enabled...
                    if (userinfo.AcctState == AcctState.Enabled)
                    {
                        if (userinfo.AcctType == AcctType.System)
                        {
                            // system accounts never expire and have no rate limit
                            userinfo.AuthState = AuthState.OK;
                            userinfo.MethodLimit = 0;
                        }
                        else if (currentTime < expiration)
                        {
                            // account has not expired, set authentication to OK
                            userinfo.AuthState = AuthState.OK;
                        }
                        else
                        {
                            // account has expired, allow access to reset password method only
                            userinfo.AuthState = AuthState.Expired;
                        }
                    }
                    else
                    {
                        // account is disabled
                        userinfo.AuthState = AuthState.Disabled;
                    }
                }
                else if (queryresults.Rows.Count > 1)
                {
                    userinfo.AuthState = AuthState.Duplicate;
                }
            }
            else
            {
                userinfo.AuthState = AuthState.Nomatch;
            }

            return userinfo;
        }

        /// <summary>
        /// used for authentication and authorization
        /// </summary>
        public string NewUser(string newgid,
                                string rec_user,
                                string new_row_ms,
                                string username, 
                                string password,
                                string accttype,
                                string acctstate,
                                string expiration,
                                string firstname,
                                string middlename,
                                string lastname,
                                string email,
                                string methodlimit,
                                string svckeyversion)
        {
            string result = APIResult.Error;

            // create new user record
            APIUser_write_dml users_write_Dml = new APIUser_write_dml(_connstr);

            result = users_write_Dml.Write(ReplicaAction.Insert,
                                    newgid,
                                    rec_user,
                                    new_row_ms,
                                    "",
                                    username,
                                    password,
                                    accttype,
                                    acctstate,
                                    expiration,
                                    firstname,
                                    middlename,
                                    lastname,
                                    email,
                                    "UserSelf.Login.base,UserSelf.ChangePassword.base,Test.EchoTest.base",
                                    "4000010",
                                    "4000010",
                                    methodlimit);

            if (result != null && result == APIResult.OK)
            {
                // assign default role
                RoleUsers_dml roleUsers_Dml = new RoleUsers_dml(_connstr);
                CmnUtil cmnUtil = new CmnUtil();
                string newroleusergid = cmnUtil.GetNewGID();
                result = roleUsers_Dml.Write(ReplicaAction.Insert, newroleusergid, rec_user, APIRoles.APIRolesID.DefaultUserID.ToString(), newgid);

                if (result == null || result != APIResult.OK)
                {
                    result = "ERROR: problem assigning default role to new user record.";
                }
            }
            else
            {
                result = "ERROR: problem creating new user record.";
            }

            return result;
        }

        /// <summary>
        /// updates the authorization data cached in each user record
        /// </summary>
        public string UpdateUserAuthorization(string userGID)
        {
            string result = "";
            string methodlist = "";
            string readlist = "";
            string writelist = "";

            // get existing user record
            APIUser_read_dml users_read_Dml = new APIUser_read_dml(_connstr);
            DataTable usertbl = users_read_Dml.GetByID(userGID);

            if (usertbl.Rows.Count > 0)
            {
                DataRow userrow = usertbl.Rows[0];

                // get user method list
                methodlist = GetMethodList(userGID);

                // get user group list (split into read list and write list)
                string grouplists = GetGroupLists(userGID);
                if (grouplists != null && grouplists != "")
                {
                    string[] grouplist = grouplists.Split('|');
                    readlist = grouplist[0];
                    writelist = grouplist[1];
                }

                // only update record if values have changed
                if (!string.Equals(userrow["MethodList"].ToString(), methodlist, StringComparison.OrdinalIgnoreCase) || !string.Equals(userrow["ReadList"].ToString(), readlist, StringComparison.OrdinalIgnoreCase) || !string.Equals(userrow["WriteList"].ToString(), writelist, StringComparison.OrdinalIgnoreCase))
                {
                    MsgUtil msgUtil = new MsgUtil();
                    string new_row_ms = msgUtil.UnixTimeString();

                    // update record
                    APIUser_write_dml users_write_Dml = new APIUser_write_dml(_connstr);
                    string tmp = users_write_Dml.Write(ReplicaAction.Update,
                                    userrow["rec_gid"].ToString(),
                                    userrow["rec_user"].ToString(),
                                    new_row_ms,
                                    userrow["row_ms"].ToString(),
                                    userrow["UserName"].ToString(),
                                    userrow["Password"].ToString(),
                                    userrow["AccountType"].ToString(),
                                    userrow["AccountState"].ToString(),
                                    userrow["ExpireDate"].ToString(),
                                    userrow["FirstName"].ToString(),
                                    userrow["MiddleName"].ToString(),
                                    userrow["LastName"].ToString(),
                                    userrow["Email"].ToString(),
                                    methodlist,
                                    readlist,
                                    writelist,
                                    userrow["MethodLimit"].ToString());
                }

                result = APIResult.OK;
            }
            else
            {
                result = APIResult.Error + ": User " + userGID + " not found";
            }

            return result;
        }

        // <summary>
        /// only allows certain fields to be updated - the other values are reused from the previous version of the record
        /// </summary>
        public string UpdateUser(string actiontype,
                                string rec_gid,
                                string rec_user,
                                string new_row_ms,
                                string accttype,
                                string acctstate,
                                string expiration,
                                string firstname,
                                string middlename,
                                string lastname,
                                string email,
                                string methodlimit,
                                string svckeyversion)
        {
            string result = "";

            // get existing user record
            APIUser_read_dml users_read_Dml = new APIUser_read_dml(_connstr);
            DataTable usertbl = users_read_Dml.GetByID(rec_gid);

            if (usertbl.Rows.Count > 0)
            {
                DataRow userrow = usertbl.Rows[0];
                APIUser_write_dml users_write_Dml = new APIUser_write_dml(_connstr);
                result = users_write_Dml.Write(actiontype,
                                    rec_gid,
                                    rec_user,
                                    new_row_ms,
                                    userrow["row_ms"].ToString(),
                                    userrow["UserName"].ToString(),
                                    userrow["Password"].ToString(),
                                    accttype,
                                    acctstate,
                                    expiration,
                                    firstname,
                                    middlename,
                                    lastname,
                                    email,
                                    userrow["MethodList"].ToString(),
                                    userrow["ReadList"].ToString(),
                                    userrow["WriteList"].ToString(),
                                    methodlimit);

            }
            else
            {
                result = APIResult.Error + ": User " + rec_gid + " not found";
            }

            return result;
        }

        // <summary>
        /// recovering a deleted record inserts a new version, while recovering an edited record uses update logic
        /// </summary>
        public string RecoverUser(string action,
                                string rec_gid,
                                string row_id,
                                string new_row_ms)
        {
            string result = "";

            // get existing user record
            APIUser_read_dml users_read_Dml = new APIUser_read_dml(_connstr);
            DataTable usertbl = users_read_Dml.RecoverByID(rec_gid, row_id);

            if (usertbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow userrow = usertbl.Rows[0];
                string edit_ms = userrow["row_ms"].ToString();

                if (action == ReplicaAction.Update)
                {
                    DataTable tmptbl = users_read_Dml.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];

                        edit_ms = tmprow["row_ms"].ToString();
                    }
                }

                APIUser_write_dml users_write_Dml = new APIUser_write_dml(_connstr);
                result = users_write_Dml.Write(action,
                                    userrow["rec_gid"].ToString(),
                                    userrow["rec_user"].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    userrow["UserName"].ToString(),
                                    userrow["Password"].ToString(),
                                    userrow["AccountType"].ToString(),
                                    userrow["AccountState"].ToString(),
                                    userrow["ExpireDate"].ToString(),
                                    userrow["FirstName"].ToString(),
                                    userrow["MiddleName"].ToString(),
                                    userrow["LastName"].ToString(),
                                    userrow["Email"].ToString(),
                                    userrow["MethodList"].ToString(),
                                    userrow["ReadList"].ToString(),
                                    userrow["WriteList"].ToString(),
                                    userrow["MethodLimit"].ToString());
            }
            else
            {
                result = APIResult.Error + ": User " + row_id + " not found";
            }

            return result;
        }

        // <summary>
        /// only allows certain fields to be updated - the other values are reused from the previous version of the record
        /// </summary>
        public string UpdateSelf(string rec_gid,
                                string new_row_ms,
                                string firstname,
                                string middlename,
                                string lastname,
                                string email)
        {
            string result = "";

            // get existing user record
            APIUser_read_dml users = new APIUser_read_dml(_connstr);
            DataTable usertbl = users.GetByID(rec_gid);

            if (usertbl.Rows.Count > 0)
            {
                DataRow userrow = usertbl.Rows[0];

                APIUser_write_dml users_write_Dml = new APIUser_write_dml(_connstr);
                result = users_write_Dml.Write(ReplicaAction.Update,
                                    rec_gid,
                                    rec_gid,
                                    new_row_ms,
                                    userrow["row_ms"].ToString(),
                                    userrow["UserName"].ToString(),
                                    userrow["Password"].ToString(),
                                    userrow["AccountType"].ToString(),
                                    userrow["AccountState"].ToString(),
                                    userrow["ExpireDate"].ToString(),
                                    firstname,
                                    middlename,
                                    lastname,
                                    email,
                                    userrow["MethodList"].ToString(),
                                    userrow["ReadList"].ToString(),
                                    userrow["WriteList"].ToString(),
                                    userrow["MethodLimit"].ToString());
            }
            else
            {
                result = APIResult.Error + ": User " + rec_gid + " not found";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ChangePassword(string new_row_ms, string username, string password, string expiredate)
        {
            string result = "";

            CmnUtil cmnUtil = new CmnUtil();
            string pwordcheck = cmnUtil.PasswordCheck(password);

            if (pwordcheck == APIResult.OK)
            {
                // get existing user record
                string uname = username.ToLower();
                APIUser_read_dml users_read_Dml = new APIUser_read_dml(_connstr);
                DataTable usertbl = users_read_Dml.GetByName(uname);

                if (usertbl.Rows.Count > 0)
                {
                    DataRow userrow = usertbl.Rows[0];

                    string combinedpword = uname + password;

                    // update record
                    APIUser_write_dml users_write_Dml = new APIUser_write_dml(_connstr);
                    result = users_write_Dml.Write(ReplicaAction.Update,
                                    userrow["rec_gid"].ToString(),
                                    userrow["rec_user"].ToString(),
                                    new_row_ms,
                                    userrow["row_ms"].ToString(),
                                    userrow["UserName"].ToString(),
                                    combinedpword,
                                    userrow["AccountType"].ToString(),
                                    userrow["AccountState"].ToString(),
                                    expiredate,
                                    userrow["FirstName"].ToString(),
                                    userrow["MiddleName"].ToString(),
                                    userrow["LastName"].ToString(),
                                    userrow["Email"].ToString(),
                                    userrow["MethodList"].ToString(),
                                    userrow["ReadList"].ToString(),
                                    userrow["WriteList"].ToString(),
                                    userrow["MethodLimit"].ToString());
                }
                else
                {
                    result = APIResult.Error + ": User " + username + " not found";
                }
            }
            else
            {
                result = APIResult.Error + ": User " + username + " - new password failed password check";
            }

            return result;
        }

        /// <summary>
        /// updates only the accountstate field of a user record
        /// </summary>
        public string DisableAccount(string userGID)
        {
            string result = "";

            // get existing user record
            APIUser_read_dml users_read_Dml = new APIUser_read_dml(_connstr);
            DataTable usertbl = users_read_Dml.GetByID(userGID);

            if (usertbl.Rows.Count > 0)
            {
                DataRow userrow = usertbl.Rows[0];

                MsgUtil msgUtil = new MsgUtil();
                string new_row_ms = msgUtil.UnixTimeString();

                // update record
                APIUser_write_dml users_write_Dml = new APIUser_write_dml(_connstr);
                result = users_write_Dml.Write(ReplicaAction.Update,
                                userrow["rec_gid"].ToString(),
                                userrow["rec_user"].ToString(),
                                new_row_ms,
                                userrow["row_ms"].ToString(),
                                userrow["UserName"].ToString(),
                                userrow["Password"].ToString(),
                                userrow["AccountType"].ToString(),
                                AcctState.Disabled,
                                userrow["ExpireDate"].ToString(),
                                userrow["FirstName"].ToString(),
                                userrow["MiddleName"].ToString(),
                                userrow["LastName"].ToString(),
                                userrow["Email"].ToString(),
                                userrow["MethodList"].ToString(),
                                userrow["ReadList"].ToString(),
                                userrow["WriteList"].ToString(),
                                userrow["MethodLimit"].ToString());
            }
            else
            {
                result = APIResult.Error + ": User " + userGID + " not found";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetMethodList(string userGID)
        {
            string methlist = "";
            string delim;

            // get user method list
            RoleMethods_dml rolemethods = new RoleMethods_dml(_connstr);
            DataTable methtbl = rolemethods.GetUserMethods(userGID);
            if (methtbl.Rows.Count > 0)
            {
                delim = "";
                StringBuilder methsb = new StringBuilder();
                for (int i = 0; i < methtbl.Rows.Count; i++)
                {
                    DataRow meth = methtbl.Rows[i];
                    string methodname = meth["APIName"].ToString() + "." + meth["MethodName"].ToString() + "." + meth["VersionName"].ToString();
                    methsb.Append(delim + methodname);
                    delim = ",";
                }
                methlist = methsb.ToString();
            }

            return methlist;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetGroupLists(string userGID)
        {
            string grouplists = "";
            string groupreadlist = "";
            string groupwritelist = "";

            GroupUsers_dml groupuser = new GroupUsers_dml(_connstr);
            DataTable grptable = groupuser.GetAssigned(userGID);
            if (grptable.Rows.Count > 0)
            {
                string readdelim = "";
                string writedelim = "";
                foreach (DataRow group in grptable.Rows)
                {
                    groupreadlist += readdelim + "'" + group["GroupGID"].ToString() + "'";
                    readdelim = ",";

                    if (group["AccessLevel"].ToString() == "READWRITE")
                    {
                        groupwritelist += writedelim + "'" + group["GroupGID"].ToString() + "'";
                        writedelim = ",";
                    }
                }
            }

            grouplists = groupreadlist + "|" + groupwritelist;

            return grouplists;
        }

    }
}
