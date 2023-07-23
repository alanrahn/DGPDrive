
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class RoleMethWrite
    {
        string _connstr;

        public RoleMethWrite(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string rec_acct,
                                string rolename,
                                string methname)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string rec_state = RecState.Active;
                switch (actiontype)
                {
                    case DBAction.Insert:
                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM DGPRoleMeth WHERE (RoleName = @RoleName AND MethName = @MethName AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert DGPRoleMeth (rec_gid, row_ms, rec_state, rec_acct, src_ms, RoleName, MethName) Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @row_ms, @RoleName, @MethName) END;";
                        break;

                    case DBAction.Delete:
                        rec_state = RecState.Deleted;

                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;

                        sqlCmd.CommandText = "UPDATE DGPRoleMeth SET rec_state = @RecStateEdited WHERE RoleName = @RoleName AND MethName = @MethName AND rec_state = @RecStateActive;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM DGPRoleMeth WHERE RoleName = @RoleName AND MethName = @MethName AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert DGPRoleMeth (rec_gid, row_ms, rec_state, rec_user, src_ms, RoleName, MethName) Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @row_ms, @RoleName, @MethName) END; END";
                        break;
                }

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add(CmnFields.row_ms_prm, SqlDbType.BigInt).Value = GenUtil.UnixTimeLong();
                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = rec_state;
                sqlCmd.Parameters.Add(CmnFields.rec_acct_prm, SqlDbType.VarChar, 50).Value = rec_acct;

                sqlCmd.Parameters.Add(DGPRole.RoleName_prm, SqlDbType.VarChar, 50).Value = rolename;
                sqlCmd.Parameters.Add(DGPMeth.MethName_prm, SqlDbType.VarChar, 100).Value = methname;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReplicaWrite(sqlCmd, _connstr, actiontype);
            }

            return strresult;
        }

        /// <summary>
        /// row_id of replicated record is passed in as src_id ... row_id becomes src_id when replicated
        /// </summary>
        public string Replicate(string src_id,
                                string src_ms,
                                string rec_dbname,
                                string rec_gid,
                                string rec_state,
                                string rec_acct,
                                string rolename,
                                string methname,
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

                sqlCmd.Parameters.Add(DGPRole.RoleName_prm, SqlDbType.VarChar, 50).Value = rolename;
                sqlCmd.Parameters.Add(DGPMeth.MethName_prm, SqlDbType.VarChar, 100).Value = methname;

                string dupcmd = "SELECT * FROM DGPRoleMeth WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (RoleName = @RoleName AND MethName = @MethName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert DGPRoleMeth (rec_gid, row_ms, rec_state, rec_acct, src_id, src_ms, rec_dbname, RoleName, MethName) Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @src_id, @src_ms, @rec_dbname, @RoleName, @MethName);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, DGPSecTables.DGPRoleMeth, dupcmd, insertcmd, connstr);
            }

            return strresult;
        }
    }
}
