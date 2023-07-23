using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace FileStoreDB.FileTags
{
    public class FileTags_dml
    {
        string _connstr;

        public FileTags_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAssigned(string fileGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select t.TagName, t.TagDescrip, t.rec_gid as TagGID, ft.rec_gid as FileTagGID" +
                                " from Tags t" +
                                " inner join FileTags ft on t.rec_gid = ft.TagGID" +
                                " where ft.FileGID = @FileGID" +
                                " and t.rec_state = @ActiveRecState and ft.rec_state = @ActiveRecState" +
                                " order by t.TagName";

                sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = fileGID;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// optional filter by tagname
        /// </summary>
        public DataTable GetAvailable(string fileGID, string tagName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select top (100) TagName, TagDescrip, rec_gid as TagGID" +
                                " from Tags" +
                                " where rec_state = @RecState";

                // search filter values are optional
                if (tagName != null && tagName != "")
                {
                    sqlCmd.CommandText += " And TagName Like @TagName";
                    sqlCmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = tagName + "%";
                }

                sqlCmd.CommandText += " and rec_gid not in" +
                                    " (select TagGID" +
                                    " from FileTags" +
                                    " where FileGID = @FileGID" +
                                    " and rec_state = @RecState)" +
                                    " order by TagName;";

                sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = fileGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the FileTags table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("FileTags", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "FileTags_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select FileGID, TagGID, Count(*)" +
                                    " From FileTags" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By FileGID, TagGID" +
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

                    ServerErrLog.LogError(userName, webSvcName, "FileTags_dml.DupeCheck", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "FileTags", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "FileTags", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            return dbUtil.GetSrcRecCount(srcdbname, "FileTags", maxdestid, _connstr);
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
            return dBUtil.GetSrcRecs(srcdbname, "FileTags", placeholderid, srcbatch, _connstr);
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
                                string filegid,
                                string taggid)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string rstate = RecState.Active;
                switch (actiontype)
                {
                    case ReplicaAction.Insert:
                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM FileTags WHERE (FileGID = @FileGID AND TagGID = @TagGID AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert FileTags (rec_gid, row_ms, rec_state, rec_user, src_ms, FileGID, TagGID) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @FileGID, @TagGID) END;";
                        break;

                    case ReplicaAction.Delete:
                        if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;

                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;

                        sqlCmd.CommandText = "UPDATE FileTags SET rec_state = @RecStateEdited WHERE FileGID = @FileGID AND TagGID = @TagGID AND rec_state = @RecStateActive;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM FileTags WHERE FileGID = @FileGID AND TagGID = @TagGID AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert FileTags (rec_gid, row_ms, rec_state, rec_user, src_ms, FileGID, TagGID) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @FileGID, @TagGID) END; END";
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = filegid;
                sqlCmd.Parameters.Add("@TagGID", SqlDbType.VarChar, 50).Value = taggid;

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
                                string filegid,
                                string taggid,
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

                sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = filegid;
                sqlCmd.Parameters.Add("@TagGID", SqlDbType.VarChar, 50).Value = taggid;

                string dupcmd = "SELECT * FROM FileTags WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (FileGID = @FileGID AND TagGID = @TagGID AND rec_state = @RecStateActive)";

                string insertcmd = "Insert FileTags (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, FileGID, TagGID) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @FileGID, @TagGID);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "FileTags", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }
    }
}
