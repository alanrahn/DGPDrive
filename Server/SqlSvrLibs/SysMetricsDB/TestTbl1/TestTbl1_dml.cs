using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;

namespace SysMetricsDB.TestTbl1
{
    public class TestTbl1_dml
    {
        string _connstr;

        public TestTbl1_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string testGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from TestFile" +
                                    " where rec_gid = @TestGID" +
                                    " and rec_state = @RecState;";

                sqlCmd.Parameters.Add("@TestGID", SqlDbType.VarChar, 50).Value = testGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// returns a specific TestFile record, regardless of rec_state value;
        /// used by replication
        /// </summary>
        public DataTable GetByRowID(string shardName, string rowID)
        {
            DataTable dtresult = new DataTable();

            // get ADO.NET connection string from ShardName
            string connstr = ConfigurationManager.AppSettings[shardName].ToString();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from TestFile" +
                                    " where row_id = @RowID;";

                sqlCmd.Parameters.Add("@RowID", SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetMaxRowID(string SrcDbName)
        {
            string strresult = "0";

            DBUtil dBUtil = new DBUtil();
            string tmp = dBUtil.GetMaxRowID(SrcDbName, "TestFile", _connstr);

            if (tmp != null && tmp != "") strresult = tmp;

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetMaxSrcID(string SrcDbName)
        {
            string strresult = "0";

            DBUtil dBUtil = new DBUtil();
            string tmp = dBUtil.GetMaxSrcID(SrcDbName, "TestFile", _connstr);

            if (tmp != null && tmp != "") strresult = tmp;

            return strresult;
        }

        /// <summary>
        /// query for the duplicate active records in the APIMethods table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("TestFile", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "TestFile_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select Name, Count(*)" +
                                    " From TestFile" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By Name" +
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

                    ServerErrLog.LogError(userName, webSvcName, "TestFile_dml.DupeCheck", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "TestReplica", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "TestReplica", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, "TestReplica", "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, "TestReplica", maxdestid, _connstr);

            return srccount + "," + srcdestcount;
        }

        /// <summary>
        /// query for a batch of source records from the specified table
        /// </summary>
        public DataTable GetSrcRecs(string srcdbname, string placeholderid, string maxbatch)
        {
            DataTable dtresult = new DataTable();

            // assign max batch size with a default of 10 if empty
            string srcbatch = "10";
            if (maxbatch != null && maxbatch != "")
            {
                srcbatch = maxbatch;
            }

            // check for origin vs. chain replication
            DBUtil dbUtil = new DBUtil();
            string connDBName = dbUtil.GetDBName(_connstr);

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                // source records must be sorted by row_id value or src_id value
                sqlCmd.CommandText = "Select Top(" + maxbatch + ") row_id, src_id, rec_gid, ShardName" +
                                " From TestFile" +
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

                dtresult = dbUtil.Read(sqlCmd, _connstr);
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
                                string testname,
                                string testval)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string rstate = RecState.Active;

                switch (actiontype)
                {
                    case ReplicaAction.Insert:

                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM TestFile WHERE (Name = @TestName AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert TestFile (rec_gid, row_ms, rec_state, rec_user, src_ms, Name, Value, ShardName) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @TestName, @TestValue, 'FileShard0') END;";
                        break;

                    case ReplicaAction.Update:
                    case ReplicaAction.Delete:

                        if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;

                        sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;

                        sqlCmd.CommandText = "UPDATE TestFile SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @RecStateActive AND row_ms = @edit_ms;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM TestFile WHERE Name = @TestName AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert TestFile (rec_gid, row_ms, rec_state, rec_user, src_ms, Name, Value, ShardName) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @TestName, @TestValue, 'FileShard0') END; END";
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@TestName", SqlDbType.VarChar, 50).Value = testname;
                sqlCmd.Parameters.Add("@TestValue", SqlDbType.VarChar, 250).Value = testval;

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
                                string testname,
                                string testvalue,
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

                sqlCmd.Parameters.Add("@TestName", SqlDbType.VarChar, 50).Value = testname;
                sqlCmd.Parameters.Add("@TestValue", SqlDbType.VarChar, 250).Value = testvalue;

                string dupcmd = "SELECT * FROM TestFile WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (Name = @TestName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert TestFile (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, Name, Value) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @TestName, @TestValue);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "TestFile", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}
