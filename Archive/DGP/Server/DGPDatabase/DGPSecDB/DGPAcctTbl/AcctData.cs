
using System;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctData
    {
        string _dbconnstr;
        int _reccount = 0;
        int _addcount = 0;
        int _skipcount = 0;

        public AcctData(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        private void AddAcct(string src_id,
                                string src_ms,
                                string acctname,
                                string sectoken,
                                string accttype,
                                string acctstate,
                                string expdate,
                                string firstname,
                                string middlename,
                                string lastname,
                                string acctemail)
        {
            try
            {
                _reccount++;

                AcctWrite acctWrite = new AcctWrite(_dbconnstr);
                string tmpresult = acctWrite.Replicate(src_id, src_ms, MasterDB.SourceDB, src_id, RecState.Active, AcctNames.DGPAdmin, acctname, sectoken, AcctType.Admin, AcctState.Enabled, expdate, firstname, middlename, lastname, acctemail, "", "", "", "0", _dbconnstr);

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
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "AcctData.AddAcct", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ID range 120000
        /// </summary>
        public string AddSecData()
        {
            string src_ms = GenUtil.UnixTimeString(-3000);

            // create admin account
            string adminhash = EncUtil.GetSHA256Hash(AcctNames.DGPAdmin + "P@ssw0rd");
            AddAcct("10000", src_ms, AcctNames.DGPAdmin, adminhash, AcctType.Admin, AcctState.Enabled, "expdate", "DGP", "", "Admin", AcctNames.DGPAdmin + "@email.com");

            // create guest account
            string guesthash = EncUtil.GetSHA256Hash(AcctNames.DGPGuest + "P@ssw0rd");
            AddAcct("10001", src_ms, AcctNames.DGPGuest, guesthash, AcctType.System, AcctState.Enabled, "expdate", "DGP", "", "Guest", AcctNames.DGPGuest + "@email.com");

            return "<p>Account Core Data: " + _reccount.ToString() + " RECORDS, " + _addcount.ToString() + " Created, " + _skipcount.ToString() + " Skipped</p>";
        }
    }
}
