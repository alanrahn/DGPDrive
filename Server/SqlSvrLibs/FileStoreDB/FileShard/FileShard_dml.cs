using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace FileStoreDB.FileShard
{
    public class FileShard_dml
    {
        public FileShard_dml()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string shardName, string segGID, string groupList)
        {
            DataTable dtresult = new DataTable();

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from FileShard" +
                                    " where rec_gid = @SegmentGID" +
                                    " and GroupGID IN (" + groupList + ")" +
                                    " and rec_state = @ActiveRecState;";

                sqlCmd.Parameters.Add("@SegmentGID", SqlDbType.VarChar, 50).Value = segGID;
                sqlCmd.Parameters.Add("@GroupList", SqlDbType.VarChar, 500).Value = groupList;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// Used in the file download process
        /// </summary>
        public string GetSegCount(string shardName, string fileGID, string fileVerison, string groupList)
        {
            string dtresult = "";

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select count(*)" +
                                    " from FileShard" +
                                    " where FileGID = @FileGID" +
                                    " and FileVersion = @FileVersion" +
                                    " and GroupGID IN (" + groupList + ")" +
                                    " and rec_state = @ActiveRecState;";

                sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = fileGID;
                sqlCmd.Parameters.Add("@FileVersion", SqlDbType.Int).Value = Convert.ToInt32(fileVerison);
                sqlCmd.Parameters.Add("@GroupList", SqlDbType.VarChar, 500).Value = groupList;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.ReadValue(sqlCmd, connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// Used in the file download process
        /// </summary>
        public string GetDataBySegNum(string shardName, string fileGID, string fileVersion, string segNum, string groupList)
        {
            string dtresult = "";

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select SegData" +
                                    " from FileShard" +
                                    " where FileGID = @FileGID" +
                                    " and FileVersion = @FileVersion" +
                                    " and SegNum = @SegNum" +
                                    " and GroupGID IN (" + groupList + ")" +
                                    " and rec_state = @ActiveRecState;";

                sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = fileGID;
                sqlCmd.Parameters.Add("@FileVersion", SqlDbType.Int).Value = Convert.ToInt32(fileVersion);
                sqlCmd.Parameters.Add("@SegNum", SqlDbType.Int).Value = Convert.ToInt32(segNum);
                sqlCmd.Parameters.Add("@GroupList", SqlDbType.VarChar, 500).Value = groupList;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.ReadValue(sqlCmd, connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// returns a specific FileShard record, regardless of rec_state value;
        /// used by replication
        /// </summary>
        public DataTable GetByRowID(string shardName, string rowID, string groupList)
        {
            DataTable dtresult = new DataTable();

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from FileShard" +
                                    " where row_id = @RowID" +
                                    " and GroupGID IN (" + groupList + ");";

                sqlCmd.Parameters.Add("@RowID", SqlDbType.BigInt).Value = Convert.ToInt64(rowID);
                sqlCmd.Parameters.Add("@GroupList", SqlDbType.VarChar, 500).Value = groupList;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetShardName()
        {
            DBUtil dBUtil = new DBUtil();

            // get shard name from list of writable shards
            string shardListStr = ConfigurationManager.AppSettings["WritableShards"].ToString();
            return dBUtil.GetWritableShard(shardListStr);
        }

        /// <summary>
        /// query for the duplicate active records in the Folders table
        /// </summary>
        public bool DupeCheck(string shardName, string userName, string webSvcName)
        {
            bool dupcheck = false;

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("FileShard", connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "FileShard_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select GroupGID, FileGID, FileVersion, SegNum, Count(*)" +
                                    " From FileShard" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By GroupGID, FileGID, FileVersion, SegNum" +
                                    " Having Count(*) > 1;";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DataTable valcheck = dBUtil.Read(sqlCmd, connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "FileShard_dml.DupeCheck", "Duplicate active values", duplist);
                }
            }

            return dupcheck;
        }

        /// <summary>
        /// query for the max src_id of records replicated in a destination table
        /// </summary>
        public string GetDestCounts(string shardName, string srcdbname)
        {
            DBUtil dbUtil = new DBUtil();

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "FileShard", connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "FileShard", connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string shardName, string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            return dbUtil.GetSrcRecCount(srcdbname, "FileShard", maxdestid, connstr);
        }

        /// <summary>
        /// query for a batch of source records from the specified table (does not return segdata field in records)
        /// </summary>
        public DataTable GetSrcRecs(string shardName, string srcdbname, string placeholderid, string maxbatch)
        {
            DataTable dtresult = new DataTable();

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            // assign max batch size with a default of 10 if empty
            string srcbatch = "10";
            if (maxbatch != null && maxbatch != "")
            {
                srcbatch = maxbatch;
            }

            // check for origin vs. chain replication
            DBUtil dbUtil = new DBUtil();
            string connDBName = dbUtil.GetDBName(connstr);

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                // source records must be sorted by row_id value or src_id value
                sqlCmd.CommandText = "Select Top(" + maxbatch + ") row_id, src_id, rec_gid, ShardName, FileGID, FileVersion" +
                                " From FileShard" +
                                " Where rec_dbname = @rec_dbname";

                if (connDBName == srcdbname)
                {
                    // origin source table
                    sqlCmd.CommandText += " And row_id > @placeholder_id" +
                                            " Order By row_id ASC;";
                }
                else
                {
                    // chain replication
                    sqlCmd.CommandText += " And src_id > @placeholder_id" +
                                            " Order By src_id ASC;";
                }

                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = srcdbname;
                sqlCmd.Parameters.Add("@placeholder_id", SqlDbType.BigInt).Value = Convert.ToInt64(placeholderid);

                dtresult = dbUtil.Read(sqlCmd, connstr);
            }

            return dtresult;
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
                                string new_row_ms,
                                string edit_ms,
                                string groupgid,
                                string shardname,
                                string filegid,
                                string fileversion,
                                string totalseg,
                                string segnum,
                                string segsize,
                                string segdata,
                                string grouplist)
        {
            string strresult = "";

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardname].ToString();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string rstate = RecState.Active;
                if (grouplist.IndexOf(groupgid, 0) != -1)
                {
                    switch (actiontype)
                    {
                        case ReplicaAction.Insert:

                            sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM FileShard WHERE (GroupGID = @GroupGID AND FileGID = @FileGID AND FileVersion = @FileVersion AND SegNum = @SegNum AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                                " IF(@dup = 0) BEGIN Insert FileShard (rec_gid, row_ms, rec_state, rec_user, src_ms, ShardName, GroupGID, FileGID, FileVersion, TotalSeg, SegNum, SegSize, SegData) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @ShardName, @GroupGID, @FileGID, @FileVersion, @TotalSeg, @SegNum, @SegSize, @SegData) END;";
                            break;

                        case ReplicaAction.Delete:

                            if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;

                            sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                            sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;

                            sqlCmd.CommandText = "UPDATE FileShard SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @RecStateActive AND row_ms = @edit_ms;" +
                                                " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM FileShard WHERE (GroupGID = @GroupGID AND FileGID = @FileGID AND FileVersion = @FileVersion AND SegNum = @SegNum AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                                " IF(@dup = 0) BEGIN Insert FileShard (rec_gid, row_ms, rec_state, rec_user, src_ms, ShardName, GroupGID, FileGID, FileVersion, TotalSeg, SegNum, SegSize, SegData) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @ShardName, @GroupGID, @FileGID, @FileVersion, @TotalSeg, @SegNum, @SegSize, @SegData) END; END;";
                            break;
                    }

                    MsgUtil msgUtil = new MsgUtil();

                    sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                    sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                    sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                    sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                    sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                    sqlCmd.Parameters.Add("@GroupGID", SqlDbType.VarChar, 50).Value = groupgid;
                    sqlCmd.Parameters.Add("@ShardName", SqlDbType.VarChar, 50).Value = shardname;
                    sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = filegid;
                    sqlCmd.Parameters.Add("@FileVersion", SqlDbType.VarChar, 50).Value = fileversion;
                    sqlCmd.Parameters.Add("@TotalSeg", SqlDbType.Int).Value = Convert.ToInt32(totalseg);
                    sqlCmd.Parameters.Add("@SegNum", SqlDbType.Int).Value = Convert.ToInt32(segnum);
                    sqlCmd.Parameters.Add("@SegSize", SqlDbType.Int).Value = Convert.ToInt32(segsize);
                    sqlCmd.Parameters.Add("@SegData", SqlDbType.VarChar, -1).Value = segdata;

                    DBUtil dBUtil = new DBUtil();
                    strresult = dBUtil.ReplicaWrite(sqlCmd, connstr, actiontype);
                }
                else
                {
                    // not authorized to write to the datagroup
                    strresult = APIResult.Error + ": user account is not authorized to insert/delete a fileshard file segment record in the DataGroup";
                }
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Replicate(string src_id,
                                string src_ms,
                                string rec_dbname,
                                string rec_gid,
                                string rec_state,
                                string rec_user,
                                string groupgid,
                                string shardname,
                                string filegid,
                                string fileversion,
                                string totalseg,
                                string segnum,
                                string segsize,
                                string segdata)
        {
            string strresult = "";

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardname].ToString();

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
                sqlCmd.Parameters.Add("@ShardName", SqlDbType.VarChar, 50).Value = shardname;
                sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = filegid;
                sqlCmd.Parameters.Add("@FileVersion", SqlDbType.VarChar, 50).Value = fileversion;
                sqlCmd.Parameters.Add("@TotalSeg", SqlDbType.Int).Value = Convert.ToInt32(totalseg);
                sqlCmd.Parameters.Add("@SegNum", SqlDbType.Int).Value = Convert.ToInt32(segnum);
                sqlCmd.Parameters.Add("@SegSize", SqlDbType.Int).Value = Convert.ToInt32(segsize);
                sqlCmd.Parameters.Add("@SegData", SqlDbType.VarChar, -1).Value = segdata;

                string dupcmd = "SELECT * FROM FileShard WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (GroupGID = @GroupGID AND FileGID = @FileGID AND FileVersion = @FileVersion AND SegNum = @SegNum AND rec_state = @RecStateActive)";

                string insertcmd = "Insert FileShard (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, GroupGID, ShardName, FileGID, FileVersion, TotalSeg, SegNum, SegSize, SegData) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @GroupGID, @ShardName, @FileGID, @FileVersion, @TotalSeg, @SegNum, @SegSize, @SegData);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "FileShard", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}
