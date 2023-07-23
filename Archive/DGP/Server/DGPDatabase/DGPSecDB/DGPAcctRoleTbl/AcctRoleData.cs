
using System;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctRoleData
    {
        string _dbconnstr;
        int _reccount = 0;
        int _addcount = 0;
        int _skipcount = 0;

        public AcctRoleData(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        private void AddAcctRole(string src_id,
                                string src_ms,
                                string acctname,
                                string rolename)
        {
            try
            {
                _reccount++;

                AcctRoleWrite acctRoleWrite = new AcctRoleWrite(_dbconnstr);
                string tmpresult = acctRoleWrite.Replicate(src_id, src_ms, MasterDB.SourceDB, src_id, RecState.Active, AcctNames.DGPAdmin, rolename, acctname, _dbconnstr);

                if (tmpresult == MethResult.OK)
                {
                    _addcount++;
                }
                else
                {
                    _skipcount++;
                }
            }
            catch (Exception ex)
            {
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "AcctRoleData.AddAcctRole", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ID range 120000
        /// </summary>
        public string AddSecData()
        {
            string src_ms = GenUtil.UnixTimeString(-3000);

            AddAcctRole("10000", src_ms, AcctNames.DGPAdmin, RoleNames.AdminRole);
            AddAcctRole("10001", src_ms, AcctNames.DGPAdmin, RoleNames.DefaultRole);
            AddAcctRole("10002", src_ms, AcctNames.DGPAdmin, RoleNames.DriveAdminRole);
            AddAcctRole("10003", src_ms, AcctNames.DGPAdmin, RoleNames.TestRole);
            AddAcctRole("10004", src_ms, AcctNames.DGPAdmin, RoleNames.RemoteMonitorRole);
            AddAcctRole("10005", src_ms, AcctNames.DGPGuest, RoleNames.DefaultRole);

            return "<p>AcctRole Core Data: " + _reccount.ToString() + " RECORDS, " + _addcount.ToString() + " Created, " + _skipcount.ToString() + " Skipped</p>";
        }

    }
}
