
using System;
using System.Text;
using System.Data;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctProc
    {
        string _connstr;

        public AcctProc(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// used for authentication and authorization
        /// </summary>
        public AcctInfo CheckUser(string secToken)
        {
            AcctInfo acctInfo = new AcctInfo();
            acctInfo.MethLimit = 0;

            AcctRead acctRead = new AcctRead(_connstr);
            DataTable accts = acctRead.GetByToken(secToken);

            // if account record is found
            if (accts != null && accts.Rows.Count > 0)
            {
                if (accts.Rows.Count == 1)
                {
                    // record found, populate acctdata object
                    DataRow acct = accts.Rows[0];

                    // user account found
                    acctInfo.AcctGID = acct[CmnFields.rec_gid].ToString();
                    acctInfo.AcctName = acct[DGPAcct.AcctName].ToString();
                    acctInfo.SecToken = acct[DGPAcct.SecToken].ToString();
                    acctInfo.AcctState = acct[DGPAcct.AcctState].ToString().ToUpper();
                    acctInfo.AcctType = acct[DGPAcct.AcctType].ToString().ToUpper();
                    acctInfo.MethList = acct[DGPAcct.MethList].ToString();
                    acctInfo.ReadList = acct[DGPAcct.ReadList].ToString();
                    acctInfo.WriteList = acct[DGPAcct.WriteList].ToString();
                    acctInfo.MethLimit = Convert.ToInt32(acct[DGPAcct.MethLimit].ToString());

                    long currentTime = GenUtil.UnixTimeLong();
                    long expiration = Convert.ToInt64(acct[DGPAcct.ExpDate]);

                    // if account is enabled...
                    if (acctInfo.AcctState == AcctState.Enabled)
                    {
                        if (acctInfo.AcctType == AcctType.System)
                        {
                            // system accounts never expire and have no rate limit
                            acctInfo.AuthState = MethResult.OK;
                            acctInfo.MethLimit = 0;
                        }
                        else if (currentTime < expiration)
                        {
                            // account has not expired, set authentication to OK
                            acctInfo.AuthState = MethResult.OK;
                        }
                        else
                        {
                            // account has expired, allow access to reset password method only
                            acctInfo.AuthState = AuthState.Expired;
                        }
                    }
                    else
                    {
                        // account is disabled
                        acctInfo.AuthState = AuthState.Disabled;
                    }
                }
                else if (accts.Rows.Count > 1)
                {
                    acctInfo.AuthState = AuthState.Duplicate;
                }
            }
            else
            {
                acctInfo.AuthState = AuthState.Nomatch;
            }

            return acctInfo;
        }

        /// <summary>
        /// create a new user with default role and group membership
        /// (methlist will be updated when the user logs into the system for the first time)
        /// </summary>
        public string NewUser(string newgid,
                                string rec_acct,
                                string new_row_ms,
                                string acctname,
                                string sectoken,
                                string accttype,
                                string acctstate,
                                string expiration,
                                string firstname,
                                string middlename,
                                string lastname,
                                string email,
                                string methlimit)
        {
            string result = MethResult.Error;

            // create new user record
            AcctWrite acctWrite = new AcctWrite(_connstr);
            result = acctWrite.Write(DBAction.Insert,
                                    newgid,
                                    rec_acct,
                                    new_row_ms,
                                    "",
                                    acctname,
                                    sectoken,
                                    accttype,
                                    acctstate,
                                    expiration,
                                    firstname,
                                    middlename,
                                    lastname,
                                    email,
                                    UserSelfAPI.Login_base,
                                    GroupNames.PublicData,
                                    GroupNames.NoData,
                                    methlimit);

            return result;
        }

        /// <summary>
        /// updates the authorization data cached in each user record
        /// </summary>
        public string UpdateAuthorization(string acctGID)
        {
            string result = "";
            string methodlist = "";
            string readlist = "";
            string writelist = "";

            // get existing user record
            AcctRead acctRead = new AcctRead(_connstr);
            DataTable usertbl = acctRead.GetByID(acctGID);

            if (usertbl.Rows.Count > 0)
            {
                DataRow acctrow = usertbl.Rows[0];

                // get user method list
                methodlist = GetMethodList(acctGID);

                // get user group list (split into read list and write list)
                string grouplists = GetGroupLists(acctGID);
                if (grouplists != null && grouplists != "")
                {
                    string[] grouplist = grouplists.Split('|');
                    readlist = grouplist[0];
                    writelist = grouplist[1];
                }

                // only update record if values have changed
                if (!string.Equals(acctrow[DGPAcct.MethList].ToString(), methodlist, StringComparison.OrdinalIgnoreCase) || !string.Equals(acctrow["ReadList"].ToString(), readlist, StringComparison.OrdinalIgnoreCase) || !string.Equals(acctrow["WriteList"].ToString(), writelist, StringComparison.OrdinalIgnoreCase))
                {
                    string new_row_ms = GenUtil.UnixTimeString();

                    // update record
                    AcctWrite acctWrite = new AcctWrite(_connstr);
                    string tmp = acctWrite.Write(DBAction.Update,
                                                acctrow[CmnFields.rec_gid].ToString(),
                                                acctrow[CmnFields.rec_acct].ToString(),
                                                new_row_ms,
                                                acctrow[CmnFields.row_ms].ToString(),
                                                acctrow[DGPAcct.AcctName].ToString(),
                                                acctrow[DGPAcct.SecToken].ToString(),
                                                acctrow[DGPAcct.AcctType].ToString(),
                                                acctrow[DGPAcct.AcctState].ToString(),
                                                acctrow[DGPAcct.ExpDate].ToString(),
                                                acctrow[DGPAcct.FirstName].ToString(),
                                                acctrow[DGPAcct.MiddleName].ToString(),
                                                acctrow[DGPAcct.LastName].ToString(),
                                                acctrow[DGPAcct.AcctEmail].ToString(),
                                                methodlist,
                                                readlist,
                                                writelist,
                                                acctrow[DGPAcct.MethLimit].ToString());
                }

                result = MethResult.OK;
            }
            else
            {
                result = MethResult.Error + ": Account " + acctGID + " not found";
            }

            return result;
        }

        // <summary>
        /// only allows certain fields to be updated - the other values are reused from the previous version of the record
        /// </summary>
        public string UpdateUser(string actiontype,
                                string rec_gid,
                                string rec_acct,
                                string new_row_ms,
                                string accttype,
                                string acctstate,
                                string expiration,
                                string firstname,
                                string middlename,
                                string lastname,
                                string email,
                                string methodlimit)
        {
            string result = "";

            // get existing user record
            AcctRead acctRead = new AcctRead(_connstr);
            DataTable accttbl = acctRead.GetByID(rec_gid);

            if (accttbl.Rows.Count > 0)
            {
                DataRow acctrow = accttbl.Rows[0];
                AcctWrite acctWrite = new AcctWrite(_connstr);
                result = acctWrite.Write(actiontype,
                                        rec_gid,
                                        rec_acct,
                                        new_row_ms,
                                        acctrow[CmnFields.row_ms].ToString(),
                                        acctrow[DGPAcct.AcctName].ToString(),
                                        acctrow[DGPAcct.SecToken].ToString(),
                                        accttype,
                                        acctstate,
                                        expiration,
                                        firstname,
                                        middlename,
                                        lastname,
                                        email,
                                        acctrow[DGPAcct.MethList].ToString(),
                                        acctrow[DGPAcct.ReadList].ToString(),
                                        acctrow[DGPAcct.WriteList].ToString(),
                                        methodlimit);

            }
            else
            {
                result = MethResult.Error + ": Account " + rec_gid + " not found";
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
            AcctRead acctRead = new AcctRead(_connstr);
            DataTable accttbl = acctRead.RecoverByID(rec_gid, row_id);

            if (accttbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow acctrow = accttbl.Rows[0];
                string edit_ms = acctrow[CmnFields.row_ms].ToString();

                if (action == DBAction.Update)
                {
                    DataTable tmptbl = acctRead.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];

                        edit_ms = tmprow[CmnFields.row_ms].ToString();
                    }
                }

                AcctWrite acctWrite = new AcctWrite(_connstr);
                result = acctWrite.Write(action,
                                        acctrow[CmnFields.rec_gid].ToString(),
                                        acctrow[CmnFields.rec_acct].ToString(),
                                        new_row_ms,
                                        edit_ms,
                                        acctrow[DGPAcct.AcctName].ToString(),
                                        acctrow[DGPAcct.SecToken].ToString(),
                                        acctrow[DGPAcct.AcctType].ToString(),
                                        acctrow[DGPAcct.AcctState].ToString(),
                                        acctrow[DGPAcct.ExpDate].ToString(),
                                        acctrow[DGPAcct.FirstName].ToString(),
                                        acctrow[DGPAcct.MiddleName].ToString(),
                                        acctrow[DGPAcct.LastName].ToString(),
                                        acctrow[DGPAcct.AcctEmail].ToString(),
                                        acctrow[DGPAcct.MethList].ToString(),
                                        acctrow[DGPAcct.ReadList].ToString(),
                                        acctrow[DGPAcct.WriteList].ToString(),
                                        acctrow[DGPAcct.MethLimit].ToString());
            }
            else
            {
                result = MethResult.Error + ": Account " + row_id + " not found";
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
            AcctRead acctRead = new AcctRead(_connstr);
            DataTable accttbl = acctRead.GetByID(rec_gid);

            if (accttbl.Rows.Count > 0)
            {
                DataRow acctrow = accttbl.Rows[0];

                AcctWrite acctWrite = new AcctWrite(_connstr);
                result = acctWrite.Write(DBAction.Update,
                                        rec_gid,
                                        rec_gid,
                                        new_row_ms,
                                        acctrow[CmnFields.row_ms].ToString(),
                                        acctrow[DGPAcct.AcctName].ToString(),
                                        acctrow[DGPAcct.SecToken].ToString(),
                                        acctrow[DGPAcct.AcctType].ToString(),
                                        acctrow[DGPAcct.AcctState].ToString(),
                                        acctrow[DGPAcct.ExpDate].ToString(),
                                        firstname,
                                        middlename,
                                        lastname,
                                        email,
                                        acctrow[DGPAcct.MethList].ToString(),
                                        acctrow[DGPAcct.ReadList].ToString(),
                                        acctrow[DGPAcct.WriteList].ToString(),
                                        acctrow[DGPAcct.MethLimit].ToString());
            }
            else
            {
                result = MethResult.Error + ": Account " + rec_gid + " not found";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ChangePassword(string new_row_ms, string acctname, string password, string expiredate)
        {
            string result = "";

            string pwordcheck = GenUtil.PasswordCheck(password);

            if (pwordcheck == MethResult.OK)
            {
                // get existing user record
                AcctRead acctRead = new AcctRead(_connstr);
                DataTable accttbl = acctRead.GetByName(acctname);

                if (accttbl.Rows.Count > 0)
                {
                    DataRow acctrow = accttbl.Rows[0];

                    string combinedtext = acctname.ToLower() + password;
                    string accthash = EncUtil.GetSHA256Hash(combinedtext);

                    // update record
                    AcctWrite acctWrite = new AcctWrite(_connstr);
                    result = acctWrite.Write(DBAction.Update,
                                            acctrow[CmnFields.rec_gid].ToString(),
                                            acctrow[CmnFields.rec_acct].ToString(),
                                            new_row_ms,
                                            acctrow[CmnFields.row_ms].ToString(),
                                            acctrow[DGPAcct.AcctName].ToString(),
                                            accthash,
                                            acctrow[DGPAcct.AcctType].ToString(),
                                            acctrow[DGPAcct.AcctState].ToString(),
                                            expiredate,
                                            acctrow[DGPAcct.FirstName].ToString(),
                                            acctrow[DGPAcct.MiddleName].ToString(),
                                            acctrow[DGPAcct.LastName].ToString(),
                                            acctrow[DGPAcct.AcctEmail].ToString(),
                                            acctrow[DGPAcct.MethList].ToString(),
                                            acctrow[DGPAcct.ReadList].ToString(),
                                            acctrow[DGPAcct.WriteList].ToString(),
                                            acctrow[DGPAcct.MethLimit].ToString());
                }
                else
                {
                    result = MethResult.Error + ": Account " + acctname + " not found";
                }
            }
            else
            {
                result = MethResult.Error + ": Account " + acctname + " - new password failed password check";
            }

            return result;
        }

        /// <summary>
        /// updates only the accountstate field of a user record
        /// </summary>
        public string DisableAccount(string acctGID)
        {
            string result = "";

            // get existing user record
            AcctRead acctRead = new AcctRead(_connstr);
            DataTable accttbl = acctRead.GetByID(acctGID);

            if (accttbl.Rows.Count > 0)
            {
                DataRow acctrow = accttbl.Rows[0];

                string new_row_ms = GenUtil.UnixTimeString();

                // update record
                AcctWrite acctWrite = new AcctWrite(_connstr);
                result = acctWrite.Write(DBAction.Update,
                                        acctrow[CmnFields.rec_gid].ToString(),
                                        acctrow[CmnFields.rec_acct].ToString(),
                                        new_row_ms,
                                        acctrow[CmnFields.row_ms].ToString(),
                                        acctrow[DGPAcct.AcctName].ToString(),
                                        acctrow[DGPAcct.SecToken].ToString(),
                                        acctrow[DGPAcct.AcctType].ToString(),
                                        AcctState.Disabled,
                                        acctrow[DGPAcct.ExpDate].ToString(),
                                        acctrow[DGPAcct.FirstName].ToString(),
                                        acctrow[DGPAcct.MiddleName].ToString(),
                                        acctrow[DGPAcct.LastName].ToString(),
                                        acctrow[DGPAcct.AcctEmail].ToString(),
                                        acctrow[DGPAcct.MethList].ToString(),
                                        acctrow[DGPAcct.ReadList].ToString(),
                                        acctrow[DGPAcct.WriteList].ToString(),
                                        acctrow[DGPAcct.MethLimit].ToString());
            }
            else
            {
                result = MethResult.Error + ": Account " + acctGID + " not found";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetMethodList(string acctGID)
        {
            string methlist = "";
            string delim;

            // get user method list
            RoleMethRead rolemeth = new RoleMethRead(_connstr);
            DataTable methtbl = rolemeth.GetAcctMethods(acctGID);

            if (methtbl.Rows.Count > 0)
            {
                delim = "";
                StringBuilder methsb = new StringBuilder();
                for (int i = 0; i < methtbl.Rows.Count; i++)
                {
                    DataRow meth = methtbl.Rows[i];
                    methsb.Append(delim + meth["FullName"].ToString());
                    delim = ",";
                }
                methlist = methsb.ToString();
            }

            return methlist;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetGroupLists(string acctGID)
        {
            string grouplists = "";
            string groupreadlist = "";
            string groupwritelist = "";

            AcctGroupRead acctgroup = new AcctGroupRead(_connstr);
            DataTable grptable = acctgroup.GetAssigned(acctGID);
            if (grptable.Rows.Count > 0)
            {
                string readdelim = "";
                string writedelim = "";
                foreach (DataRow group in grptable.Rows)
                {
                    groupreadlist += readdelim + "'" + group["GroupName"].ToString() + "'";
                    readdelim = ",";

                    if (group["AccLev"].ToString() == "READWRITE")
                    {
                        groupwritelist += writedelim + "'" + group["GroupName"].ToString() + "'";
                        writedelim = ",";
                    }
                }
            }

            grouplists = groupreadlist + "|" + groupwritelist;

            return grouplists;
        }
    }
}
