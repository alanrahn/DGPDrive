
using System;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class GroupData
    {
        string _dbconnstr;
        int _reccount = 0;
        int _addcount = 0;
        int _skipcount = 0;

        public GroupData(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        private void AddGroup(string src_id,
                                string src_ms,
                                string groupname,
                                string groupdescrip)
        {
            try
            {
                _reccount++;

                GroupWrite groupWrite = new GroupWrite(_dbconnstr);
                string tmpresult = groupWrite.Replicate(src_id, src_ms, MasterDB.SourceDB, src_id, RecState.Active, AcctNames.DGPAdmin, groupname, groupdescrip, _dbconnstr);

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
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "GroupData.AddGroup", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ID range 120000
        /// </summary>
        public string AddSecData()
        {
            string src_ms = GenUtil.UnixTimeString(-3000);

            AddGroup("10000", src_ms, GroupNames.AdminData, "Data restricted to admin users.");
            AddGroup("10001", src_ms, GroupNames.PublicData, "Data that is pulblicly available.");
            AddGroup("10002", src_ms, GroupNames.TestData, "Data created during tests.");

            return "<p>Groups Core Data: " + _reccount.ToString() + " RECORDS, " + _addcount.ToString() + " Created, " + _skipcount.ToString() + " Skipped</p>";
        }


    }
}
