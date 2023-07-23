using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using ApiUtil;
using SqlSvrUtil;

namespace SysInfoDB.DataGroups
{
    public class DataGroups_write_dml
    {
        string _connstr;

        public DataGroups_write_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string rec_user,
                                string new_row_ms,
                                string edit_ms,
                                string groupname,
                                string groupdescrip)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                string rstate = RecState.Active;
                string tstate = RecState.Active;
                switch (actiontype)
                {
                    case ReplicaAction.Insert:

                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM DataGroups WHERE (GroupName = @GroupName AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert DataGroups (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupName, GroupDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupName, @GroupDescrip) END;";
                        break;

                    case ReplicaAction.Update:
                    case ReplicaAction.Delete:
                    case ReplicaAction.Recover:

                        if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;
                        if (actiontype == ReplicaAction.Recover) tstate = RecState.Deleted;

                        sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;
                        sqlCmd.Parameters.Add("@TargetState", SqlDbType.VarChar, 10).Value = tstate;

                        sqlCmd.CommandText = "UPDATE DataGroups SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @TargetState AND row_ms = @edit_ms;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM DataGroups WHERE GroupName = @GroupName AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert DataGroups (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupName, GroupDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupName, @GroupDescrip) END; END";
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = groupname;
                sqlCmd.Parameters.Add("@GroupDescrip", SqlDbType.VarChar, 100).Value = groupdescrip;

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
                                string rec_user,
                                string groupname,
                                string groupdescrip,
                                string connstr)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@src_id", SqlDbType.BigInt).Value = Convert.ToInt64(src_id);
                sqlCmd.Parameters.Add("@src_ms", SqlDbType.BigInt).Value = Convert.ToInt64(src_ms);
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = rec_dbname;
                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rec_state;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = groupname;
                sqlCmd.Parameters.Add("@GroupDescrip", SqlDbType.VarChar, 100).Value = groupdescrip;

                string dupcmd = "SELECT * FROM DataGroups WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (GroupName = @GroupName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert DataGroups (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, GroupName, GroupDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @GroupName, @GroupDescrip);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "DataGroups", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}
