
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctWrite
    {
        string _connstr;

        public AcctWrite(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// low-level method called by the UserProc methods - generally should not be called directly by API mapper methods
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string rec_acct,
                                string new_row_ms,
                                string edit_ms,
                                string acctname,
                                string sectoken,
                                string accttype,
                                string acctstate,
                                string expiration,
                                string firstname,
                                string middlename,
                                string lastname,
                                string acctemail,
                                string methlist,
                                string grpreadlist,
                                string grpwritelist,
                                string methlimit)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                string rec_state = RecState.Active;
                string targ_state = RecState.Active;
                switch (actiontype)
                {
                    case DBAction.Insert:

                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM DGPAcct WHERE(AcctName = @AcctName AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert DGPAcct (rec_gid, row_ms, rec_state, rec_acct, src_ms, AcctName, SecToken, AcctType, AcctState, ExpDate, FirstName, MiddleName, LastName, AcctEmail, MethList, ReadList, WriteList, MethLimit)" +
                                            " Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @row_ms, @AcctName, @SecToken, @AcctType, @AcctState, @ExpDate, @FirstName, @MiddleName, @LastName, @AcctEmail, @MethList, @ReadList, @WriteList, @MethLimit) END;";
                        break;

                    case DBAction.Update:
                    case DBAction.Delete:
                    case DBAction.Recover:

                        if (actiontype == DBAction.Delete) rec_state = RecState.Deleted;
                        if (actiontype == DBAction.Recover) targ_state = RecState.Deleted;

                        sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;
                        sqlCmd.Parameters.Add("@TargetState", SqlDbType.VarChar, 10).Value = targ_state;

                        sqlCmd.CommandText = "UPDATE DGPAcct SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @TargetState AND row_ms = @edit_ms;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM DGPAcct WHERE UserName = @UserName AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert DGPAcct (rec_gid, row_ms, rec_state, rec_acct, src_ms, AcctName, SecToken, AcctType, AcctState, ExpDate, FirstName, MiddleName, LastName, AcctEmail, MethList, ReadList, WriteList, MethLimit)" +
                                            " Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @row_ms, @AcctName, @SecToken, @AcctType, @AcctState, @ExpDate, @FirstName, @MiddleName, @LastName, @AcctEmail, @MethList, @ReadList, @WriteList, @MethLimit) END; END";
                        break;
                }

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add(CmnFields.row_ms_prm, SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = rec_state;
                sqlCmd.Parameters.Add(CmnFields.rec_acct_prm, SqlDbType.VarChar, 50).Value = rec_acct;

                sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctname.ToLower();
                sqlCmd.Parameters.Add(DGPAcct.SecToken_prm, SqlDbType.VarChar, 250).Value = sectoken;
                sqlCmd.Parameters.Add(DGPAcct.AcctType_prm, SqlDbType.VarChar, 10).Value = accttype;
                sqlCmd.Parameters.Add(DGPAcct.AcctState_prm, SqlDbType.VarChar, 10).Value = acctstate;
                sqlCmd.Parameters.Add(DGPAcct.ExpDate_prm, SqlDbType.BigInt).Value = Convert.ToInt64(expiration);
                sqlCmd.Parameters.Add(DGPAcct.FirstName_prm, SqlDbType.VarChar, 50).Value = firstname;
                sqlCmd.Parameters.Add(DGPAcct.MiddleName_prm, SqlDbType.VarChar, 50).Value = middlename;
                sqlCmd.Parameters.Add(DGPAcct.LastName_prm, SqlDbType.VarChar, 50).Value = lastname;
                sqlCmd.Parameters.Add(DGPAcct.AcctEmail_prm, SqlDbType.VarChar, 250).Value = acctemail;
                sqlCmd.Parameters.Add(DGPAcct.MethList_prm, SqlDbType.VarChar, -1).Value = methlist;
                sqlCmd.Parameters.Add(DGPAcct.ReadList_prm, SqlDbType.VarChar, 500).Value = grpreadlist;
                sqlCmd.Parameters.Add(DGPAcct.WriteList_prm, SqlDbType.VarChar, 500).Value = grpwritelist;
                sqlCmd.Parameters.Add(DGPAcct.MethLimit_prm, SqlDbType.Int).Value = Convert.ToInt32(methlimit);

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReplicaWrite(sqlCmd, _connstr, actiontype);
            }

            return strresult;
        }

        /// <summary>
        /// row_id of replicated record is passed in as src_id ... row_id becomes src_id when replicated
        /// replication does not decrypt the password value, and replicates it in encrypted form
        /// </summary>
        public string Replicate(string src_id,
                                string src_ms,
                                string rec_dbname,
                                string rec_gid,
                                string rec_state,
                                string rec_acct,
                                string acctname,
                                string sectoken,
                                string accttype,
                                string acctstate,
                                string expdate,
                                string firstname,
                                string middlename,
                                string lastname,
                                string acctemail,
                                string methlist,
                                string grpreadlist,
                                string grpwritelist,
                                string methlimit,
                                string connstr)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add(CmnFields.src_id_prm, SqlDbType.BigInt).Value = Convert.ToInt64(src_id);
                sqlCmd.Parameters.Add(CmnFields.src_ms_prm, SqlDbType.BigInt).Value = Convert.ToInt64(src_ms);
                sqlCmd.Parameters.Add(CmnFields.row_ms_prm, SqlDbType.BigInt).Value = GenUtil.UnixTimeLong();
                sqlCmd.Parameters.Add(CmnFields.rec_dbname_prm, SqlDbType.VarChar, 50).Value = rec_dbname;
                sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = rec_state;
                sqlCmd.Parameters.Add(CmnFields.rec_acct_prm, SqlDbType.VarChar, 50).Value = rec_acct;

                sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctname;
                sqlCmd.Parameters.Add(DGPAcct.SecToken_prm, SqlDbType.VarChar, 250).Value = sectoken;
                sqlCmd.Parameters.Add(DGPAcct.AcctType_prm, SqlDbType.VarChar, 10).Value = accttype;
                sqlCmd.Parameters.Add(DGPAcct.AcctState_prm, SqlDbType.VarChar, 10).Value = acctstate;
                sqlCmd.Parameters.Add(DGPAcct.ExpDate_prm, SqlDbType.BigInt).Value = Convert.ToInt64(expdate);
                sqlCmd.Parameters.Add(DGPAcct.FirstName_prm, SqlDbType.VarChar, 50).Value = firstname;
                sqlCmd.Parameters.Add(DGPAcct.MiddleName_prm, SqlDbType.VarChar, 50).Value = middlename;
                sqlCmd.Parameters.Add(DGPAcct.LastName_prm, SqlDbType.VarChar, 50).Value = lastname;
                sqlCmd.Parameters.Add(DGPAcct.AcctEmail_prm, SqlDbType.VarChar, 250).Value = acctemail;
                sqlCmd.Parameters.Add(DGPAcct.MethList_prm, SqlDbType.VarChar, -1).Value = methlist;
                sqlCmd.Parameters.Add(DGPAcct.ReadList_prm, SqlDbType.VarChar, 500).Value = grpreadlist;
                sqlCmd.Parameters.Add(DGPAcct.WriteList_prm, SqlDbType.VarChar, 500).Value = grpwritelist;
                sqlCmd.Parameters.Add(DGPAcct.MethLimit_prm, SqlDbType.Int).Value = Convert.ToInt32(methlimit);

                string dupcmd = "SELECT * FROM DGPAcct WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (AcctName = @AcctName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert DGPAcct (rec_gid, row_ms, rec_state, rec_acct, src_id, src_ms, rec_dbname, AcctName, SecToken, AcctType, AcctState, ExpDate, FirstName, MiddleName, LastName, AcctEmail, MethList, ReadList, WriteList, MethLimit)" +
                                    " Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @src_id, @src_ms, @rec_dbname, @AcctName, @SecToken, @AcctType, @AcctState, @ExpDate, @FirstName, @MiddleName, @LastName, @AcctEmail, @MethList, @ReadList, @WriteList, @MethLimit);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, DGPSecTables.DGPAcct, dupcmd, insertcmd, connstr);
            }

            return strresult;
        }
    }
}
