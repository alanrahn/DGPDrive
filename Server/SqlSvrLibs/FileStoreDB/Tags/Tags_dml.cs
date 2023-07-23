using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace FileStoreDB.Tags
{
    public class Tags_dml
    {
        string _connstr;

        public Tags_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string tagGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from Tags" +
                                    " where rec_gid = @TagGID" +
                                    " and rec_state = @ActiveRecState;";

                sqlCmd.Parameters.Add("@TagGID", SqlDbType.VarChar, 50).Value = tagGID;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByName(string tagName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from Tags" +
                                    " where TagName = @TagName" +
                                    " and rec_state = @ActiveRecState;";

                sqlCmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = tagName;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable FilterByName(string tagName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select top(100) TagName, TagDescrip, rec_gid" +
                                    " from Tags" +
                                    " where rec_state = @ActiveRecState";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                // search filter values are optional
                if (tagName != null && tagName != "")
                {
                    sqlCmd.CommandText += " And TagName Like @TagName";
                    sqlCmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = tagName + "%";
                }

                sqlCmd.CommandText += " Order By TagName ASC;";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetHistory(string tagGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from Tags" +
                                    " where rec_gid = @TagGID" +
                                    " order by row_id ASC;";

                sqlCmd.Parameters.Add("@TagGID", SqlDbType.VarChar, 50).Value = tagGID;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RecoverByID(string tagGID, string rowID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from Tags" +
                                    " where rec_gid = @rec_gid" +
                                    " and row_id = @RowID;";

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = tagGID;
                sqlCmd.Parameters.Add("@RowID", SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string rolename, string recState)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From Tags" +
                                    " where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (rolename != null && rolename != "")
                {
                    sqlCmd.CommandText += " And TagName Like @TagName";
                    sqlCmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = rolename + "%";
                }

                sqlCmd.CommandText += ";";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetSearch(string pageNum, string pageSize, string recState, string sortorder, string tagname)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                if (sortorder != "ASC" && sortorder != "DESC") sortorder = "ASC";

                sqlCmd.CommandText = "Select TagName, TagDescrip, rec_gid, row_id" +
                                    " From Tags" +
                                    " where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (tagname != null && tagname != "")
                {
                    sqlCmd.CommandText += " And TagName Like @RoleName";
                    sqlCmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = tagname + "%";
                }

                sqlCmd.CommandText += " Order By TagName " + sortorder;

                sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                    " Fetch Next " + pageSize + " Rows Only";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the Folders table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("Tags", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "Tags_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select TagName, Count(*)" +
                                    " From Tags" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By TagName" +
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

                    ServerErrLog.LogError(userName, webSvcName, "Tags_dml.DupeCheck", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "Tags", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "Tags", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            return dbUtil.GetSrcRecCount(srcdbname, "Tags", maxdestid, _connstr);
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
            return dBUtil.GetSrcRecs(srcdbname, "Tags", placeholderid, srcbatch, _connstr);
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// 
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string new_row_ms,
                                string edit_ms,
                                string rec_user,
                                string tagname,
                                string tagdescrip)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string rstate = RecState.Active;
                string tstate = RecState.Active;
                switch (actiontype)
                {
                    case ReplicaAction.Insert:

                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM Tags WHERE (TagName = @TagName AND TagDescrip = @TagDescrip AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert Tags (rec_gid, row_ms, rec_state, rec_user, src_ms, TagName, TagDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @TagName, @TagDescrip) END;";
                        break;

                    case ReplicaAction.Update:
                    case ReplicaAction.Delete:
                    case ReplicaAction.Recover:

                        if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;
                        if (actiontype == ReplicaAction.Recover) tstate = RecState.Deleted;

                        sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;
                        sqlCmd.Parameters.Add("@TargetState", SqlDbType.VarChar, 10).Value = tstate;

                        sqlCmd.CommandText = "UPDATE Tags SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @TargetState AND row_ms = @edit_ms;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM Tags WHERE TagName = @TagName AND TagDescrip = @TagDescrip AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert Tags (rec_gid, row_ms, rec_state, rec_user, src_ms, TagName, TagDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @TagName, @TagDescrip) END; END";
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = tagname;
                sqlCmd.Parameters.Add("@TagDescrip", SqlDbType.VarChar, 250).Value = tagdescrip;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReplicaWrite(sqlCmd, _connstr, actiontype);

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
                                string tagname,
                                string tagdescrip,
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

                sqlCmd.Parameters.Add("@TagName", SqlDbType.VarChar, 50).Value = tagname;
                sqlCmd.Parameters.Add("@TagDescrip", SqlDbType.VarChar, 250).Value = tagdescrip;

                string dupcmd = "SELECT * FROM Tags WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (TagName = @TagName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert Tags (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, TagName, TagDescrip) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @TagName, @TagDescrip);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "Tags", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}
