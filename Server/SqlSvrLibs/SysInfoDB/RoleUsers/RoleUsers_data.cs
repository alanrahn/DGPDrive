using System;

using ApiUtil;
using SysInfoDB.APIRoles;
using SysInfoDB.APIUser;

namespace SysInfoDB.RoleUsers
{
    public class RoleUsers_data
    {
        string _dbconnstr;
        int reccount = 0;
        int scancount = 0;
        int skipcount = 0;

        public RoleUsers_data(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        public string AddCoreData()
        {
            MsgUtil msgUtil = new MsgUtil();
            string src_ms = msgUtil.UnixTimeString(-3000);

            // GroupUser - 7,000,000
            AddRoleUser(7000000, src_ms, "7000000", APIRolesID.SysInfoAdminID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000010, src_ms, "7000010", APIRolesID.SysWorkAdminID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000020, src_ms, "7000020", APIRolesID.SysMetricsAdminID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000030, src_ms, "7000030", APIRolesID.DefaultUserID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000040, src_ms, "7000040", APIRolesID.TestingID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000080, src_ms, "7000080", APIRolesID.SysMetricsUserID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000060, src_ms, "7000060", APIRolesID.RemoteMetricsID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000050, src_ms, "7000050", APIRolesID.FileStoreAdminID.ToString(), APIUserID.SysAdminID.ToString());
            AddRoleUser(7000070, src_ms, "7000070", APIRolesID.FileStoreUserID.ToString(), APIUserID.SysAdminID.ToString());

            AddRoleUser(7000100, src_ms, "7000100", APIRolesID.DefaultUserID.ToString(), APIUserID.GuestID.ToString());

            return "<p>RoleUsers Core Data: " + reccount.ToString() + " RECORDS, " + scancount.ToString() + " Created, " + skipcount.ToString() + " Skipped</p>";
        }

        private void AddRoleUser(long rowid,
                                string row_ms,
                                string recgid,
                                string rolegid,
                                string usergid)
        {
            try
            {
                reccount++;

                RoleUsers_dml roleuser_Dml = new RoleUsers_dml(_dbconnstr);
                string tmpresult = roleuser_Dml.Replicate(rowid.ToString(), row_ms, SysInfoMaster.SourceDB, recgid, RecState.Active, APIUserID.SysAdminID.ToString(), rolegid, usergid, _dbconnstr);

                if (tmpresult == "OK")
                {
                    scancount++;
                }
                else
                {
                    skipcount++;
                    string msg = "row_id: " + rowid + ", rec_gid: " + recgid + ", RoleID: " + rolegid + ", UserID: " + usergid;
                    RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "RoleUsers_data.AddMethod", "CLIENT", "INFO", "Skipped RoleMethod", msg);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "RoleUsers_data.AddMethod", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }
    }
}
