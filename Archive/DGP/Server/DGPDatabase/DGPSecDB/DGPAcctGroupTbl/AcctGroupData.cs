
using System;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctGroupData
    {
        string _dbconnstr;
        int _reccount = 0;
        int _addcount = 0;
        int _skipcount = 0;

        public AcctGroupData(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        private void AddAcctGroup(string src_id,
                                string src_ms,
                                string acctname,
                                string groupname,
                                string acclev)
        {
            try
            {
                _reccount++;

                AcctGroupWrite acctGroupWrite = new AcctGroupWrite(_dbconnstr);
                string tmpresult = acctGroupWrite.Replicate(src_id, src_ms, MasterDB.SourceDB, src_id, RecState.Active, AcctNames.DGPAdmin, groupname, acctname, acclev, _dbconnstr);

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
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "AcctGroupData.AddAcctGroup", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AddSecData()
        {
            string src_ms = GenUtil.UnixTimeString(-3000);

            AddAcctGroup("10000", src_ms, AcctNames.DGPAdmin, GroupNames.AdminData, AccLev.ReadWrite);
            AddAcctGroup("10001", src_ms, AcctNames.DGPGuest, GroupNames.PublicData, AccLev.ReadOnly);

            return "<p>AcctGroup Core Data: " + _reccount.ToString() + " RECORDS, " + _addcount.ToString() + " Created, " + _skipcount.ToString() + " Skipped</p>";
        }

    }
}
