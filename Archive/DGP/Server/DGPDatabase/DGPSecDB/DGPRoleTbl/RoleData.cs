
using System;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class RoleData
    {
        string _dbconnstr;
        int _reccount = 0;
        int _addcount = 0;
        int _skipcount = 0;

        public RoleData(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        private void AddRole(string src_id,
                                string src_ms,
                                string rolename,
                                string roledescrip)
        {
            try
            {
                _reccount++;

                RoleWrite roleWrite = new RoleWrite(_dbconnstr);
                string tmpresult = roleWrite.Replicate(src_id, src_ms, MasterDB.SourceDB, src_id, RecState.Active, AcctNames.DGPAdmin, rolename, roledescrip, _dbconnstr);

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
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "RoleData.AddRole", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ID range 120000
        /// </summary>
        public string AddSecData()
        {
            string src_ms = GenUtil.UnixTimeString(-3000);

            AddRole("10000", src_ms, RoleNames.AdminRole, "Admin role with access to all API methods.");
            AddRole("10001", src_ms, RoleNames.DefaultRole, "Base API methods needed by all user accounts.");
            AddRole("10002", src_ms, RoleNames.DriveAdminRole, "Admin role with access to all of the DGPDrive API methods.");
            AddRole("10003", src_ms, RoleNames.DriveUserRole, "Standard role with access to some of the DGPDrive API methods.");
            AddRole("10004", src_ms, RoleNames.RemoteMonitorRole, "Enables the collection of remote end-to-end metrics.");
            AddRole("10004", src_ms, RoleNames.TestRole, "Admin role with access to all API methods for testing.");

            return "<p>Roles Core Data: " + _reccount.ToString() + " RECORDS, " + _addcount.ToString() + " Created, " + _skipcount.ToString() + " Skipped</p>";
        }
    }
}
