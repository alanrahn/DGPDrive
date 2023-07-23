using System;

using ApiUtil;
using SysInfoDB.APIUser;

namespace SysInfoDB.APIRoles
{
    public static class APIRolesID
    {
        // APIRoles - 2,000,000
        public const int SysInfoAdminID = 2000000;
        public const int SysWorkAdminID = 2000010;
        public const int SysMetricsAdminID = 2000020;
        public const int DefaultUserID = 2000030;
        public const int FileStoreAdminID = 2000040;
        public const int FileStoreUserID = 2000050;
        public const int TestingID = 2000060;
        public const int RemoteMetricsID = 2000070;
        public const int SysMetricsUserID = 2000080;
    }

    public class APIRoles_data
    {
        string _dbconnstr;
        int reccount = 0;
        int scancount = 0;
        int skipcount = 0;

        public APIRoles_data(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        public string AddCoreData()
        {
            MsgUtil msgUtil = new MsgUtil();
            string src_ms = msgUtil.UnixTimeString(-3000);

            AddRole(APIRolesID.SysInfoAdminID, src_ms, APIRolesID.SysInfoAdminID.ToString(), "SecurityAdmin", "Admin role with access to the SysInfo API methods.");
            AddRole(APIRolesID.SysWorkAdminID, src_ms, APIRolesID.SysWorkAdminID.ToString(), "WorkAdmin", "Admin role with access to the SysWork API methods.");
            AddRole(APIRolesID.SysMetricsAdminID, src_ms, APIRolesID.SysMetricsAdminID.ToString(), "MetricsAdmin", "Admin role with access to the SysMetrics API methods.");
            AddRole(APIRolesID.DefaultUserID, src_ms, APIRolesID.DefaultUserID.ToString(), "DefaultUser", "Base API methods needed by all user accounts.");
            AddRole(APIRolesID.FileStoreAdminID, src_ms, APIRolesID.FileStoreAdminID.ToString(), "FileStoreAdmin", "Admin role with access to all of the FileStore API methods.");
            AddRole(APIRolesID.FileStoreUserID, src_ms, APIRolesID.FileStoreUserID.ToString(), "FileStoreUser", "Standard role with access to some of the FileStore API methods.");
            AddRole(APIRolesID.TestingID, src_ms, APIRolesID.TestingID.ToString(), "Testing", "Role that has access to testing API methods.");
            AddRole(APIRolesID.RemoteMetricsID, src_ms, APIRolesID.RemoteMetricsID.ToString(), "RemoteMetrics", "Enables the collection of remote end-to-end metrics.");
            AddRole(APIRolesID.SysMetricsUserID, src_ms, APIRolesID.SysMetricsUserID.ToString(), "SysMetricsUser", "Enables the collection of remote log entries.");

            return "<p>Roles Core Data: " + reccount.ToString() + " RECORDS, " + scancount.ToString() + " Created, " + skipcount.ToString() + " Skipped</p>";
        }

        private void AddRole(long rowid,
                                string row_ms,
                                string recgid,
                                string rolename,
                                string roledescrip)
        {
            try
            {
                reccount++;

                APIRoles_write_dml roles_Dml = new APIRoles_write_dml(_dbconnstr);
                string tmpresult = roles_Dml.Replicate(rowid.ToString(), row_ms, SysInfoMaster.SourceDB, recgid, RecState.Active, APIUserID.SysAdminID.ToString(), rolename, roledescrip, _dbconnstr);

                if (tmpresult == "OK")
                {
                    scancount++;
                }
                else
                {
                    skipcount++;
                    string msg = "row_id: " + rowid + ", rec_gid: " + recgid + ", RoleName: " + rolename;
                    RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "APIRoles_data.AddMethod", "CLIENT", "INFO", "Skipped Role", msg);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "APIRoles_data.AddMethod", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

    }
}
