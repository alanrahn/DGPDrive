using System;

using ApiUtil;
using SysInfoDB.DataGroups;
using SysInfoDB.APIUser;

namespace SysInfoDB.GroupUsers
{
    public class GroupUsers_data
    {
        string _dbconnstr;
        int reccount = 0;
        int scancount = 0;
        int skipcount = 0;

        public GroupUsers_data(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        public string AddCoreData()
        {
            MsgUtil msgUtil = new MsgUtil();
            string src_ms = msgUtil.UnixTimeString(-3000);

            // GroupUser - 5,000,000
            AddGroupUser(5000000, src_ms, "5000000", DataGroupID.TestDataID.ToString(), APIUserID.SysAdminID.ToString(), AccessLevel.ReadWrite);
            AddGroupUser(5000010, src_ms, "5000010", DataGroupID.PublicDataID.ToString(), APIUserID.SysAdminID.ToString(), AccessLevel.ReadWrite);
            AddGroupUser(5000020, src_ms, "5000030", DataGroupID.AdminDataID.ToString(), APIUserID.SysAdminID.ToString(), AccessLevel.ReadWrite);

            AddGroupUser(5000100, src_ms, "5000100", DataGroupID.PublicDataID.ToString(), APIUserID.GuestID.ToString(), AccessLevel.ReadOnly);

            return "<p>GroupUsers Core Data: " + reccount.ToString() + " RECORDS, " + scancount.ToString() + " Created, " + skipcount.ToString() + " Skipped</p>";
        }

        private void AddGroupUser(long rowid,
                                string row_ms,
                                string recgid,
                                string groupgid,
                                string usergid,
                                string accesslevel)
        {
            reccount++;

            GroupUsers_dml dataread_Dml = new GroupUsers_dml(_dbconnstr);
            string tmpresult = dataread_Dml.Replicate(rowid.ToString(), row_ms, SysInfoMaster.SourceDB, recgid, RecState.Active, APIUserID.SysAdminID.ToString(), groupgid, usergid, accesslevel, _dbconnstr);

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

