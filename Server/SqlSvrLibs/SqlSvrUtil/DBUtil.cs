using System;
using System.Data;
using System.Data.SqlClient;

using ApiUtil;

namespace SqlSvrUtil
{
    public class DBUtil
    {
        public DBUtil()
        {

        }

        /// <summary>
        /// parse the database name from an ADO.NET connection string
        /// </summary>
        public string GetDBName(string connStr)
        {
            string dbname = "";

            // break up connection string using semicolons
            string[] sections = connStr.Split(';');

            // get the database substring
            foreach (string section in sections)
            {
                if (section.Contains("Database="))
                {
                    string[] nameparts = section.Split('=');
                    dbname = nameparts[1];
                }
            }

            return dbname;
        }

        /// <summary>
        /// parse the database name from an ADO.NET connection string
        /// </summary>
        public string GetWritableShard(string shardListStr)
        {
            string shardName = "";

            // break up connection string using semicolons
            string[] shardList = shardListStr.Split(',');

            if (shardList.Length == 1)
            {
                shardName = shardList[0];
            }
            else if (shardList.Length > 1)
            {
                // randomly select one of the connection strings
                Random rand = new Random();
                int index = rand.Next(0, shardList.Length);
                shardName = shardList[index];
            }
            else
            {
                // error: no connection strings in shardList input - return empty string
            }

            return shardName;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable Read(SqlCommand sqlCmd, string connStr)
        {
            DataTable qresult = new DataTable();

            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;

                using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                {
                    qresult.Load(sqlReader);
                }
            }

            return qresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReadValue(SqlCommand sqlCmd, string connStr)
        {
            string queryval = "";

            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;

                object value = sqlCmd.ExecuteScalar();
                if (value != null) queryval = value.ToString();
            }

            return queryval;
        }


        /***************************************************************************/
        /***************************************************************************/
        /***************************************************************************/

        /// <summary>
        /// 
        /// </summary>
        public string ReplicaWrite(SqlCommand sqlCmd, string connStr, string action)
        {
            int rowsaff = 0;
            string result = APIResult.Error;

            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();

                using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
                {

                    sqlCmd.Connection = sqlConn;
                    sqlCmd.Transaction = sqlTran;

                    rowsaff = sqlCmd.ExecuteNonQuery();

                    // insert of new record only affects one row, update affects two rows
                    if ((action == ReplicaAction.Insert && rowsaff == 1) || (action != ReplicaAction.Insert && rowsaff == 2))
                    {
                        sqlTran.Commit();
                        result = APIResult.OK;
                    }
                    else
                    {
                        sqlTran.Rollback();
                        result += " : " + action + " : rows affected = " + rowsaff.ToString();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Write(SqlCommand sqlCmd, string connStr)
        {
            int rowsaff = 0;
            string result = APIResult.Error;

            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();

                using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
                {

                    sqlCmd.Connection = sqlConn;
                    sqlCmd.Transaction = sqlTran;

                    rowsaff = sqlCmd.ExecuteNonQuery();

                    if (rowsaff > 0)
                    {
                        sqlTran.Commit();
                        result = APIResult.OK;
                    }
                    else
                    {
                        sqlTran.Rollback();
                    }
                }
            }

            return result;
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// used for DDL and other logic that does not return the number of rows affected (-1)
        /// </summary>
        public int Execute(SqlCommand sqlCmd, string connStr)
        {
            int rowsaff = 0;

            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();

                using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
                {
                    sqlCmd.Connection = sqlConn;
                    sqlCmd.Transaction = sqlTran;

                    rowsaff = sqlCmd.ExecuteNonQuery();
                    sqlTran.Commit();
                }
            }

            return rowsaff;
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// query for a batch of source records from a source table
        /// origin table uses row_id as placeholder, all others use src_id 
        /// </summary>
        public DataTable GetSrcRecs(string srcDbName, string srcTblName, string placeholderID, string maxBatch, string connStr)
        {
            DataTable dtresult = new DataTable();

            // assign max batch size with a default of 10 if empty
            string srcbatch = "10";
            if (maxBatch != null && maxBatch != "")
            {
                srcbatch = maxBatch;
            }

            // check for origin vs. chain replication
            string connDBName = GetDBName(connStr);

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                // source records must be sorted by row_id value or src_id value
                sqlCmd.CommandText = "Select Top(" + maxBatch + ") *" +
                                " From " + srcTblName +
                                " Where rec_dbname = @rec_dbname";

                if (connDBName == srcDbName)
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

                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = srcDbName;
                sqlCmd.Parameters.Add("@placeholder_id", SqlDbType.BigInt).Value = Convert.ToInt64(placeholderID);

                dtresult = Read(sqlCmd, connStr);
            }

            return dtresult;
        }

        /// <summary>
        /// query that checks a table for duplicate records based on the global ID value and active state
        /// </summary>
        public DataTable DupeCheckByID(string tblName, string connStr)
        {
            DataTable idCheck = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select rec_gid, Count(*)" +
                                    " From " + tblName +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By rec_gid" +
                                    " Having Count(*) > 1;";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                idCheck = Read(sqlCmd, connStr);
            }

            return idCheck;
        }

        /// <summary>
        /// query for the maximum row_id from a source replica table for a given database name
        /// </summary>
        public string GetMaxRowID(string srcDbName, string tblName, string connStr)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select max(row_id)" +
                                    " From " + tblName +
                                    " Where rec_dbname = @rec_dbname;";

                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = srcDbName;

                strresult = ReadValue(sqlCmd, connStr);
            }

            return strresult;
        }

        /// <summary>
        /// query for the maximum src_id from a destination replica table for a given database name
        /// </summary>
        public string GetMaxSrcID(string srcDbName, string tblName, string connStr)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select max(src_id)" +
                                    " From " + tblName +
                                    " Where rec_dbname = @rec_dbname;";

                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = srcDbName;

                strresult = ReadValue(sqlCmd, connStr);
            }

            return strresult;
        }

        /// <summary>
        /// query for a count of source records from a source table with optional maxID
        /// </summary>
        public string GetSrcRecCount(string srcDbName, string tblName, string maxDestID, string connStr)
        {
            string srccount = "0";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string endDML = ";";
                long maxID = Convert.ToInt64(maxDestID);

                if (maxID > 0)
                {
                    endDML = " And row_id <= @MaxID;";
                    sqlCmd.Parameters.Add("@MaxID", SqlDbType.BigInt).Value = maxID;
                }

                sqlCmd.CommandText = "Select count(*)" +
                                    " From " + tblName +
                                    " Where rec_dbname = @rec_dbname" +
                                    endDML;

                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = srcDbName;

                srccount = ReadValue(sqlCmd, connStr);
            }

            return srccount;
        }

        /// <summary>
        /// query for a count of source records from a source table using row_id or src_id
        /// </summary>
        public string GetDestRecCount(string srcDbName, string tblName, string connStr)
        {
            string srccount = "0";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "Select count(*)" +
                                    " From " + tblName +
                                    " Where rec_dbname = @rec_dbname;";

                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = srcDbName;

                srccount = ReadValue(sqlCmd, connStr);
            }

            return srccount;
        }

        /// <summary>
        /// The mergedestrec method is responsible for merging all source table records into each destination table.
        /// It queries for existing records, and if any are found it determines the correct state for both the existing record and the merged record.
        /// </summary>
        public string MergeDestRec(SqlCommand sqlCmd, string destTblName, string dupSql, string insertSql, string connStr)
        {
            string action = "";

            string rec_dbname = sqlCmd.Parameters["@rec_dbname"].Value.ToString();
            string src_gid = sqlCmd.Parameters["@rec_gid"].Value.ToString();
            string src_state = sqlCmd.Parameters["@rec_state"].Value.ToString();
            string src_id = sqlCmd.Parameters["@src_id"].Value.ToString();
            long src_ms = Convert.ToInt64(sqlCmd.Parameters["@src_ms"].Value);

            // query for an existing replicated record or an existing active record
            sqlCmd.CommandText = dupSql;
            DataTable destRecs = Read(sqlCmd, connStr);

            // dest record(s) found
            if (destRecs != null && destRecs.Rows.Count > 0)
            {
                long dest_ms = 0;
                string destgid = "";

                // check dest record as 1) already replicated or 2) LWW conflict
                foreach (DataRow dr in destRecs.Rows)
                {
                    // should only be one record, but logic must be able to handle more
                    if (dr["rec_dbname"].ToString() == rec_dbname && dr["src_id"].ToString() == src_id)
                    {
                        // the record has already been replicated: end the merge process (idempotent)
                        return APIResult.Done;
                    }
                    else
                    {
                        // existing active record with same values (LWW conflict)
                        dest_ms = Convert.ToInt64(dr["row_ms"]);
                        destgid = dr["rec_gid"].ToString();
                    }
                }
                    
                // for LWW conflict, first check source record state
                if (src_state != RecState.Active && src_state != RecState.Deleted)
                {
                    // if incoming source record has archived or ignored ('X') state, merge the source record as is
                    sqlCmd.CommandText = insertSql;
                    action = ReplicaAction.Insert;
                }
                else
                {
                    // LWW rule determines which record is set as Active/Deleted
                    if (src_ms > dest_ms)
                    {
                        // if source record is newer, update the existing destination record as edited before merging the newer source record as Active/Deleted
                        sqlCmd.CommandText = "UPDATE " + destTblName + " SET rec_state = '" + RecState.Edited + "' WHERE rec_gid = " + destgid + " AND rec_state = @RecStateActive;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN " + insertSql + " END;";
                        action = ReplicaAction.Update;
                    }
                    else
                    {
                        // if source record is older, merge the source record as edited and leave the current destination record as Active/Deleted
                        sqlCmd.CommandText = insertSql;
                        sqlCmd.Parameters["@rec_state"].Value = RecState.Edited;
                        action = ReplicaAction.Insert;
                    }
                }
            }
            else
            {
                // no existing records found in dest table: merge the source record as is
                sqlCmd.CommandText = insertSql;
                action = ReplicaAction.Insert;
            }

            return ReplicaWrite(sqlCmd, connStr, action);
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        

    }
}

