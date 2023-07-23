using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace FileStoreDB.Folders
{
    public class Folders_dml
    {
        string _connstr;

        public Folders_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetUserSubFolders(string folderGID, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select *" +
                                        " from Folders" +
                                        " where rec_state = @ActiveRecState" +
                                        " and ParentGID = @ParentGID" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " order by DisplayOrder, FolderName;";

                    sqlCmd.Parameters.Add("@ParentGID", SqlDbType.VarChar, 50).Value = folderGID;
                    sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }


        public int GetSubFolderCount(string folderGID, string groupList)
        {
            int result = 0;

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select count(*)" +
                                        " from Folders" +
                                        " where rec_state = @ActiveRecState" +
                                        " and ParentGID = @ParentGID" +
                                        " and GroupGID IN (" + groupList + ");";

                    sqlCmd.Parameters.Add("@ParentGID", SqlDbType.VarChar, 50).Value = folderGID;
                    sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    DBUtil dBUtil = new DBUtil();
                    string tmp = dBUtil.ReadValue(sqlCmd, _connstr);

                    int.TryParse(tmp, out result);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string folderGID, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select *" +
                                        " from Folders" +
                                        " where rec_gid = @FolderGID" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " and rec_state = @ActiveRecState;";

                    sqlCmd.Parameters.Add("@FolderGID", SqlDbType.VarChar, 50).Value = folderGID;
                    sqlCmd.Parameters.Add("@GroupList", SqlDbType.VarChar, 500).Value = groupList;
                    sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByName(string folderName, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select *" +
                                        " from Folders" +
                                        " where FolderName = @FolderName" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " and rec_state = @ActiveRecState;";

                    sqlCmd.Parameters.Add("@FolderName", SqlDbType.VarChar, 50).Value = folderName;
                    sqlCmd.Parameters.Add("@GroupList", SqlDbType.VarChar, 500).Value = groupList;
                    sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetFolderGroupGID(string folderGID)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select GroupGID from Folders Where rec_gid = @FolderGID and rec_state = @RecState;";

                sqlCmd.Parameters.Add("@FolderGID", SqlDbType.VarChar, 50).Value = folderGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
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

                DataTable idcheck = dBUtil.DupeCheckByID("Folders", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "Folders_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select ParentGID, FolderName, Count(*)" +
                                    " From Folders" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By ParentGID, FolderName" +
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

                    ServerErrLog.LogError(userName, webSvcName, "Folders_dml.DupeCheck", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "Folders", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "Folders", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            return dbUtil.GetSrcRecCount(srcdbname, "Folders", maxdestid, _connstr);
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
            return dBUtil.GetSrcRecs(srcdbname, "Folders", placeholderid, srcbatch, _connstr);
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
                                string parentgid,
                                string foldername,
                                string displayorder,
                                string grouplist)
        {
            string strresult = "";

            if (grouplist != null && grouplist != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    string rstate = RecState.Active;
                    if (grouplist.IndexOf(groupgid, 0) != -1)
                    {
                        string folderGroupGID = GetFolderGroupGID(parentgid);
                        int subFolderCount = GetSubFolderCount(parentgid, grouplist);
                        int maxfolders = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFolders"].ToString());

                        if ((actiontype == ReplicaAction.Insert && subFolderCount < maxfolders) || actiontype != ReplicaAction.Insert)
                        {
                            if (grouplist.IndexOf(folderGroupGID, 0) != -1)
                            {
                                switch (actiontype)
                                {
                                    case ReplicaAction.Insert:

                                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM Folders WHERE (ParentGID = @ParentGID AND FolderName = @FolderName AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                                            " IF(@dup = 0) BEGIN Insert Folders (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupGID, ParentGID, FolderName, DisplayOrder) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupGID, @ParentGID, @FolderName, @DisplayOrder) END;";
                                        break;

                                    case ReplicaAction.Update:
                                    case ReplicaAction.Delete:

                                        if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;

                                        sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;

                                        sqlCmd.CommandText = "UPDATE Folders SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @RecStateActive AND row_ms = @edit_ms;" +
                                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM Folders WHERE ParentGID = @ParentGID AND FolderName = @FolderName AND rec_state = @RecStateActive);" +
                                                            " IF(@dup = 0) BEGIN Insert Folders (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupGID, ParentGID, FolderName, DisplayOrder) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupGID, @ParentGID, @FolderName, @DisplayOrder) END; END";
                                        break;
                                }

                                MsgUtil msgUtil = new MsgUtil();

                                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                                sqlCmd.Parameters.Add("@GroupGID", SqlDbType.VarChar, 50).Value = groupgid;
                                sqlCmd.Parameters.Add("@ParentGID", SqlDbType.VarChar, 50).Value = parentgid;
                                sqlCmd.Parameters.Add("@FolderName", SqlDbType.VarChar, 50).Value = foldername;
                                sqlCmd.Parameters.Add("@DisplayOrder", SqlDbType.Int).Value = Convert.ToInt32(displayorder);

                                DBUtil dBUtil = new DBUtil();
                                strresult = dBUtil.ReplicaWrite(sqlCmd, _connstr, actiontype);
                            }
                            else
                            {
                                // not authorized to write to the datagroup
                                strresult = APIResult.Error + ": user account is not authorized to create/save a subfolder to the parent folder DataGroup";
                            }
                        }
                        else
                        {
                            // too many subfolders
                            strresult = APIResult.Error + ": maximum number of subfoders for parent directory exceeded";
                        }
                    }
                    else
                    {
                        // not authorized to write to the datagroup
                        strresult = APIResult.Error + ": user account is not authorized to create/save a folder to the DataGroup";
                    }
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
                                string parentgid,
                                string foldername,
                                string displayorder,
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
                sqlCmd.Parameters.Add("@ParentGID", SqlDbType.VarChar, 50).Value = parentgid;
                sqlCmd.Parameters.Add("@FolderName", SqlDbType.VarChar, 50).Value = foldername;
                sqlCmd.Parameters.Add("@DisplayOrder", SqlDbType.Int).Value = Convert.ToInt32(displayorder);

                string dupcmd = "SELECT * FROM Folders WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (ParentGID = @ParentGID AND FolderName = @FolderName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert Folders (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, GroupGID, ParentGID, FolderName, DisplayOrder) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @GroupGID, @ParentGID, @FolderName, @DisplayOrder);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "Folders", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}
