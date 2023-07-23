using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;
using FileStoreDB.Folders;

namespace FileStoreDB.Files
{
    public class Files_write_dml
    {
        string _connstr;

        public Files_write_dml(string connStr)
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
                                string groupgid,
                                string foldergid,
                                string folderpath,
                                string filename,
                                string filedescrip,
                                string fileext,
                                string filesize,
                                string filedate,
                                string filehash,
                                string fileversion,
                                string shardname,
                                string segsize,
                                string totalseg,
                                string grouplist)
        {
            string strresult = "";

            if (grouplist != null && grouplist != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    string rstate = RecState.Active;
                    string tstate = RecState.Active;

                    if (grouplist.IndexOf(groupgid, 0) != -1)
                    {
                        // get target folder groupGID
                        Folders_dml folders_Dml = new Folders_dml(_connstr);
                        string folderGroupGID = folders_Dml.GetFolderGroupGID(foldergid);

                        if (grouplist.IndexOf(folderGroupGID, 0) != -1)
                        {
                            switch (actiontype)
                            {
                                case ReplicaAction.Insert:

                                    sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM Files WHERE (FileName = @FileName AND FolderGID = @FolderGID AND FileExt = @FileExt AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                                        " IF(@dup = 0) BEGIN Insert Files (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupGID, FolderGID, FolderPath, FileName, FileDescrip, FileExt, FileSize, FileDate, FileHash, FileVersion, ShardName, SegSize, TotalSeg)" +
                                                        " Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupGID, @FolderGID, @FolderPath, @FileName, @FileDescrip, @FileExt, @FileSize, @FileDate, @FileHash, @FileVersion, @ShardName, @SegSize, @TotalSeg) END;";
                                    break;

                                case ReplicaAction.Update:
                                case ReplicaAction.Delete:
                                case ReplicaAction.Recover:
                                case ReplicaAction.Remove:

                                    if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;
                                    if (actiontype == ReplicaAction.Remove) rstate = RecState.X;
                                    if (actiontype == ReplicaAction.Recover) tstate = RecState.Deleted;

                                    sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                                    sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;
                                    sqlCmd.Parameters.Add("@TargetState", SqlDbType.VarChar, 10).Value = tstate;

                                    sqlCmd.CommandText = "UPDATE Files SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @TargetState AND row_ms = @edit_ms;" +
                                                        " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM Files WHERE FolderGID = @FolderGID AND FileName = @FileName AND FileExt = @FileExt AND rec_state = @RecStateActive);" +
                                                        " IF(@dup = 0) BEGIN Insert Files (rec_gid, row_ms, rec_state, rec_user, src_ms, GroupGID, FolderGID, FolderPath, FileName, FileDescrip, FileExt, FileSize, FileDate, FileHash, FileVersion, ShardName, SegSize, TotalSeg)" +
                                                        " Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @GroupGID, @FolderGID, @FolderPath, @FileName, @FileDescrip, @FileExt, @FileSize, @FileDate, @FileHash, @FileVersion, @ShardName, @SegSize, @TotalSeg) END; END";
                                    break;
                            }

                            MsgUtil msgUtil = new MsgUtil();

                            sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                            sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                            sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                            sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                            sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                            sqlCmd.Parameters.Add("@GroupGID", SqlDbType.VarChar, 50).Value = groupgid;
                            sqlCmd.Parameters.Add("@FolderGID", SqlDbType.VarChar, 50).Value = foldergid;
                            sqlCmd.Parameters.Add("@FolderPath", SqlDbType.VarChar, 250).Value = folderpath;
                            sqlCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 50).Value = filename;
                            sqlCmd.Parameters.Add("@FileDescrip", SqlDbType.VarChar, 250).Value = filedescrip;
                            sqlCmd.Parameters.Add("@FileExt", SqlDbType.VarChar, 5).Value = fileext;
                            sqlCmd.Parameters.Add("@FileSize", SqlDbType.BigInt).Value = Convert.ToInt64(filesize);
                            sqlCmd.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = Convert.ToDateTime(filedate);
                            sqlCmd.Parameters.Add("@FileHash", SqlDbType.VarChar, 250).Value = filehash;
                            sqlCmd.Parameters.Add("@FileVersion", SqlDbType.Int).Value = Convert.ToInt32(fileversion);
                            sqlCmd.Parameters.Add("@ShardName", SqlDbType.VarChar, 50).Value = shardname;
                            sqlCmd.Parameters.Add("@SegSize", SqlDbType.Int).Value = Convert.ToInt32(segsize);
                            sqlCmd.Parameters.Add("@TotalSeg", SqlDbType.Int).Value = Convert.ToInt32(totalseg);

                            DBUtil dBUtil = new DBUtil();
                            strresult = dBUtil.ReplicaWrite(sqlCmd, _connstr, actiontype);
                        }
                        else
                        {
                            // not authorized to write to the datagroup
                            strresult = APIResult.Error + ": user account is not authorized to create/save a file to the folder DataGroup";
                        }
                    }
                    else
                    {
                        // not authorized to write to the datagroup
                        strresult = APIResult.Error + ": user account is not authorized to create/save a file to the file DataGroup";
                    }
                }
            }

            return strresult;
        }

        /// <summary>
        /// only replicates the database record of a hybrid schema;
        /// the replication process run remotely handles file download and upload (before the record is replicated);
        /// </summary>
        public string Replicate(string src_id,
                                string src_ms,
                                string rec_dbname,
                                string rec_gid,
                                string rec_state,
                                string rec_user,
                                string groupgid,
                                string foldergid,
                                string folderpath,
                                string filename,
                                string filedescrip,
                                string fileext,
                                string filesize,
                                string filedate,
                                string filehash,
                                string fileversion,
                                string shardname,
                                string segsize,
                                string totalseg,
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
                sqlCmd.Parameters.Add("@FolderGID", SqlDbType.VarChar, 50).Value = foldergid;
                sqlCmd.Parameters.Add("@FolderPath", SqlDbType.VarChar, 250).Value = folderpath;
                sqlCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 50).Value = filename;
                sqlCmd.Parameters.Add("@FileDescrip", SqlDbType.VarChar, 250).Value = filedescrip;
                sqlCmd.Parameters.Add("@FileExt", SqlDbType.VarChar, 5).Value = fileext;
                sqlCmd.Parameters.Add("@FileSize", SqlDbType.BigInt).Value = Convert.ToInt64(filesize);
                sqlCmd.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = Convert.ToDateTime(filedate);
                sqlCmd.Parameters.Add("@FileHash", SqlDbType.VarChar, 250).Value = filehash;
                sqlCmd.Parameters.Add("@FileVersion", SqlDbType.Int).Value = Convert.ToInt32(fileversion);
                sqlCmd.Parameters.Add("@ShardName", SqlDbType.VarChar, 50).Value = shardname;
                sqlCmd.Parameters.Add("@SegSize", SqlDbType.Int).Value = Convert.ToInt32(segsize);
                sqlCmd.Parameters.Add("@TotalSeg", SqlDbType.Int).Value = Convert.ToInt32(totalseg);

                string dupcmd = "SELECT * FROM Files WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (FolderGID = @FolderGID AND FileName = @FileName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert Files (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, GroupGID, FolderGID, FolderPath, FileName, FileDescrip, FileExt, FileSize, FileDate, FileHash, FileVersion, ShardName, SegSize, TotalSeg)" +
                                    " Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @GroupGID, @FolderGID, @FolderPath, @FileName, @FileDescrip, @FileExt, @FileSize, @FileDate, @FileHash, @FileVersion, @ShardName, @SegSize, @TotalSeg);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "Files", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }

    }
}
