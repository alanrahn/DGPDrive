using System;
using System.Configuration;

using ApiUtil;

namespace SysInfoDB.APIUser
{
    public static class APIUserID
    {
        // APIUser - 3,000,000
        public const int SysAdminID = 3000000;
        public const int GuestID = 3000010;
    }

    public class APIUser_data
    {
        string _dbconnstr;
        string _svckey;
        string _keyversion;
        int reccount = 0;
        int scancount = 0;
        int skipcount = 0;

        EncryptUtil encryptUtil = new EncryptUtil();

        public APIUser_data(string dbConnStr, string svcKey, string keyVersion)
        {
            _dbconnstr = dbConnStr;
            _svckey = svcKey;
            _keyversion = keyVersion;
        }

        public string AddCoreData(string adminUserName, string adminPassword)
        {
            MsgUtil msgUtil = new MsgUtil();
            string src_ms = msgUtil.UnixTimeString(-3000);
            string expire_ms = msgUtil.UnixTimeString(3000);

            AddUser(APIUserID.SysAdminID, src_ms, APIUserID.SysAdminID.ToString(), adminUserName, adminPassword, "0", expire_ms);
            AddUser(APIUserID.GuestID, src_ms, APIUserID.GuestID.ToString(), "Guest", "P@ssw0rd", "0", expire_ms);

            return "<p>APIUsers Core Data: " + reccount.ToString() + " RECORDS, " + scancount.ToString() + " Created, " + skipcount.ToString() + " Skipped</p>";
        }

        private void AddUser(long rowID, string src_ms, string userGID, string userName, string passWord, string methLimit, string expireDate)
        {
            string combinedPword = userName.ToLower() + passWord;
            string encpword = encryptUtil.EncryptString(combinedPword, _svckey);

            // get user method list
            UserProc userProc = new UserProc(_dbconnstr);
            string methodlist = userProc.GetMethodList(userGID);

            // get user group list (split into read list and write list)
            string grouplists = userProc.GetGroupLists(userGID);
            string[] grouplist = grouplists.Split('|');
            string groupreadlist = grouplist[0];
            string groupwritelist = grouplist[1];

            StoreUser(rowID.ToString(), src_ms, userGID, userName, encpword, "core", "", "user", AcctType.System, expireDate, methodlist, groupreadlist, groupwritelist, methLimit, _keyversion);
        }

        private void StoreUser(string rowid,
                                string rec_ms,
                                string recgid,
                                string username,
                                string password,
                                string firstname,
                                string middlename,
                                string lastname, 
                                string accttype,
                                string expiredate,
                                string methlist,
                                string grpreadlist,
                                string grpwritelist,
                                string methlimit,
                                string svckeyversion)
        {
            reccount++;

            APIUser_write_dml users_Dml = new APIUser_write_dml(_dbconnstr);
            string tmpresult = users_Dml.Replicate(rowid, 
                                                    rec_ms, 
                                                    SysInfoMaster.SourceDB, 
                                                    recgid, 
                                                    RecState.Active, 
                                                    APIUserID.SysAdminID.ToString(), 
                                                    username, 
                                                    password,
                                                    accttype,
                                                    AcctState.Enabled, 
                                                    expiredate,
                                                    firstname,
                                                    middlename,
                                                    lastname,
                                                    "donotreply@DGP.com",
                                                    methlist, 
                                                    grpreadlist, 
                                                    grpwritelist, 
                                                    methlimit, 
                                                    svckeyversion,
                                                    _dbconnstr);

            if (tmpresult == "OK")
            {
                scancount++;
            }
            else
            {
                skipcount++;
            }
        }

    }
}
