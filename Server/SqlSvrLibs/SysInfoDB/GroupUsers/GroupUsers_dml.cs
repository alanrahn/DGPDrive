using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace SysInfoDB.GroupUsers
{
    public class GroupUsers_dml
    {
        string _connstr;

        public GroupUsers_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAssigned(string userGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select dg.GroupName, dg.GroupDescrip, gu.AccessLevel, dg.rec_gid as GroupGID, gu.rec_gid as GroupUserGID" +
                                    " from DataGroups dg" +
                                    " inner join GroupUsers gu on dg.rec_gid = gu.GroupGID" +
                                    " where gu.UserGID = @UserGID" +
                                    " and dg.rec_state = @ActiveRecState and gu.rec_state = @ActiveRecState" +
                                    " order by dg.GroupName ASC;";

                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAvailable(string userGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select GroupName, GroupDescrip, rec_gid as GroupGID" +
                                    " from DataGroups" +
                                    " where rec_state = @RecState" +
                                    " and rec_gid not in" +
                                    " (select GroupGID" +
                                    " from GroupUsers" +
                                    " where UserGID = @UserGID" +
                                    " and rec_state = @RecState)" +
                                    " order by GroupName ASC;";

                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the GroupUsers table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("GroupUsers", _connstr);

                if (idcheck.Rows.Count > 0)
                {
                    dupcheck = true;

                    string delim = "";
                    string duplist = "";
                    foreach (DataRow dupid in idcheck.Rows)
                    {
                        duplist += delim + dupid["row_id"].ToString();
                        delim = ",";
                    }

                    ServerErrLog.LogError(userName, webSvcName, "GroupUsers_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select GroupGID, UserGID, Count(*)" +
                                    " From GroupUsers" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By GroupGID, UserGID" +
                                    " Having Count(*) > 1;";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DataTable valcheck = dBUtil.Read(sqlCmd, _connstr);

                if (valcheck.Rows.Count > 0)
                {
                    dupcheck = true;

                    string delim = "";
                    string duplist = "";
                    foreach (DataRow dupval in valcheck.Rows)
                    {
                        duplist += delim + dupval["row_id"].ToString();
                        delim = ",";
                    }

                    ServerErrLog.LogError(userName, webSvcName, "GroupUsers_dml.DupeCheck", "Duplicate active values", duplist);
                }
            }

            return dupcheck;
        }

        /// <summary>
        /// query for the max src_id of records replicated in a destination table
        /// </summary>
        public string GetDestCounts(string srcdbname)
        {
            DBUtil dbUtil = new DBUtil();

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "GroupUsers", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "GroupUsers", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, "GroupUsers", "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, "GroupUsers", maxdestid, _connstr);

            return srccount + "," + srcdestcount;
        }

        /// <summary>
        /// query for a batch of source records from the specified table
        /// </summary>
        public DataTable GetSrcRecs(string srcdbname, string placeholderid, string maxbatch)
        {
            string srcbatch = "10";
            if (maxbatch != null && maxbatch != "")
            {
                srcbatch = maxbatch;
            }

            DBUtil dBUtil = new DBUtil();
            return dBUtil.GetSrcRecs(srcdbname, "GroupUsers", placeholderid, srcbatch, _connstr);
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// 
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string rec_user,
                                string groupgid,
                                string usergid,
                                string accesslevel)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string rstate = RecState.Active;
                switch (actiontype)
                {
                    case ReplicaAction.Insert:
                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM GroupUsers WHERE (GroupGID = @GroupGID AND UserGID = @UserGID AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert GroupUsers (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupGID, UserGID, AccessLevel) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupGID, @UserGID, @AccessLevel) END;";
                        break;

                    case ReplicaAction.Delete:
                        if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;

                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;

                        sqlCmd.CommandText = "UPDATE GroupUsers SET rec_state = @RecStateEdited WHERE GroupGID = @GroupGID AND UserGID = @UserGID AND rec_state = @RecStateActive;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM GroupUsers WHERE GroupGID = @GroupGID AND UserGID = @UserGID AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert GroupUsers (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupGID, UserGID, AccessLevel) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupGID, @UserGID, @AccessLevel) END; END";
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@GroupGID", SqlDbType.VarChar, 50).Value = groupgid;
                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 100).Value = usergid;
                sqlCmd.Parameters.Add("@AccessLevel", SqlDbType.VarChar, 10).Value = accesslevel;

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
                                string groupgid,
                                string usergid,
                                string accesslevel,
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

                sqlCmd.Parameters.Add("@GroupGID", SqlDbType.VarChar, 50).Value = groupgid;
                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 100).Value = usergid;
                sqlCmd.Parameters.Add("@AccessLevel", SqlDbType.VarChar, 10).Value = accesslevel;

                string dupcmd = "SELECT * FROM GroupUsers WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (GroupGID = @GroupGID AND UserGID = @UserGID AND rec_state = @RecStateActive)";

                string insertcmd = "Insert GroupUsers (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, GroupGID, UserGID, AccessLevel) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @GroupGID, @UserGID, @AccessLevel);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "GroupUsers", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}

