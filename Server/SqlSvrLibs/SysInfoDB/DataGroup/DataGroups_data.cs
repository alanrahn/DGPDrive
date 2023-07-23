using System;

using ApiUtil;
using SysInfoDB.APIUser;


namespace SysInfoDB.DataGroups
{
    public static class DataGroupID
    {
        // DataGroup - 4,000,000
        public const int TestDataID = 4000000;
        public const int PublicDataID = 4000010;
        public const int AdminDataID = 4000020;
    }


    /// <summary>
    /// primarily used by the DGP application to partition and restrict access to segments of the shared data
    /// </summary>
    public class DataGroups_data
    {
        string _dbconnstr;
        int reccount = 0;
        int scancount = 0;
        int skipcount = 0;

        public DataGroups_data(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        public string AddCoreData()
        {
            MsgUtil msgUtil = new MsgUtil();
            string src_ms = msgUtil.UnixTimeString(-10);

            AddGroup(DataGroupID.TestDataID, src_ms, DataGroupID.TestDataID.ToString(), "TestData", "group used to partition test data");
            AddGroup(DataGroupID.PublicDataID, src_ms, DataGroupID.PublicDataID.ToString(), "PublicData", "group for publicly accessible data");
            AddGroup(DataGroupID.AdminDataID, src_ms, DataGroupID.AdminDataID.ToString(), "AdminData", "group for administrator data");

            return "<p>DataGroups Core Data: " + reccount.ToString() + " RECORDS, " + scancount.ToString() + " Created, " + skipcount.ToString() + " Skipped</p>";
        }

        private void AddGroup(long rowid,
                                string row_ms,
                                string recgid,
                                string groupname,
                                string groupdescrip)
        {
            try
            {
                reccount++;

                DataGroups_write_dml group_Dml = new DataGroups_write_dml(_dbconnstr);
                string tmpresult = group_Dml.Replicate(rowid.ToString(), row_ms, SysInfoMaster.SourceDB, recgid, RecState.Active, APIUserID.SysAdminID.ToString(), groupname, groupdescrip, _dbconnstr);

                if (tmpresult == "OK")
                {
                    scancount++;
                }
                else
                {
                    skipcount++;
                    string msg = "row_id: " + rowid + ", rec_gid: " + recgid + ", GroupName: " + groupname;
                    RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "DataGroups_data.AddMethod", "CLIENT", "INFO", "Skipped DataGroup", msg);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "DataGroups_data.AddMethod", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }
    }
}
