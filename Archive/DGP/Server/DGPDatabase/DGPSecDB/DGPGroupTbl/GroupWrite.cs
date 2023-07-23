﻿
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class GroupWrite
    {
        string _connstr;

        public GroupWrite(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string rec_acct,
                                string new_row_ms,
                                string edit_ms,
                                string groupname,
                                string groupdescrip)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                string rec_state = RecState.Active;
                string targ_state = RecState.Active;
                switch (actiontype)
                {
                    case DBAction.Insert:

                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM DGPGroup WHERE (GroupName = @GroupName AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert DGPGroup (rec_gid, row_ms, rec_state, rec_acct, src_ms, GroupName, GroupDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @row_ms, @GroupName, @GroupDescrip) END;";
                        break;

                    case DBAction.Update:
                    case DBAction.Delete:
                    case DBAction.Recover:

                        if (actiontype == DBAction.Delete) rec_state = RecState.Deleted;
                        if (actiontype == DBAction.Recover) targ_state = RecState.Deleted;

                        sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;
                        sqlCmd.Parameters.Add("@TargetState", SqlDbType.VarChar, 10).Value = targ_state;

                        sqlCmd.CommandText = "UPDATE DGPGroup SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @TargetState AND row_ms = @edit_ms;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM DGPGroup WHERE GroupName = @GroupName AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert DGPGroup (rec_gid, row_ms, rec_state, rec_acct, src_ms, GroupName, GroupDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @row_ms, @GroupName, @GroupDescrip) END; END";
                        break;
                }

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add(CmnFields.row_ms_prm, SqlDbType.BigInt).Value = GenUtil.UnixTimeLong();
                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = rec_state;
                sqlCmd.Parameters.Add(CmnFields.rec_acct_prm, SqlDbType.VarChar, 50).Value = rec_acct;

                sqlCmd.Parameters.Add(DGPGroup.GroupName_prm, SqlDbType.VarChar, 50).Value = groupname;
                sqlCmd.Parameters.Add(DGPGroup.GroupDescrip_prm, SqlDbType.VarChar, 100).Value = groupdescrip;

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
                                string groupname,
                                string groupdescrip,
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

                sqlCmd.Parameters.Add(DGPGroup.GroupName_prm, SqlDbType.VarChar, 50).Value = groupname;
                sqlCmd.Parameters.Add(DGPGroup.GroupDescrip_prm, SqlDbType.VarChar, 100).Value = groupdescrip;

                string dupcmd = "SELECT * FROM DGPGroup WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (GroupName = @GroupName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert DGPGroup (rec_gid, row_ms, rec_state, rec_acct, src_id, src_ms, rec_dbname, GroupName, GroupDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_acct, @src_id, @src_ms, @rec_dbname, @GroupName, @GroupDescrip);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, DGPSecTables.DGPGroup, dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}
