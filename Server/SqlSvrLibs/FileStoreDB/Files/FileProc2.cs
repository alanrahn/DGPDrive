using System.Data;
using System.Configuration;
using System;
using System.IO;

using ApiUtil;
using ApiUtil.DataClasses;
using SysMetricsDB;

namespace FileStoreDB.Files
{
    public class FileProc2
    {
        string _connstr;

        public FileProc2(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// recovering a deleted record inserts a new version, while recovering an edited record uses update logic
        /// </summary>
        public string RecoverFile(string action,
                                string rec_gid,
                                string row_id,
                                string new_row_ms,
                                string grouplist)
        {
            string result = "";

            // get existing file record
            Files_read_dml files_read_Dml = new Files_read_dml(_connstr);
            DataTable filetbl = files_read_Dml.RecoverByID(rec_gid, row_id, grouplist);

            if (filetbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow filerow = filetbl.Rows[0];

                string edit_ms = filerow["row_ms"].ToString();

                if (action == ReplicaAction.Update)
                {
                    DataTable tmptbl = files_read_Dml.GetByID(rec_gid, grouplist);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];

                        edit_ms = tmprow["row_ms"].ToString();
                    }
                }

                Files_write_dml files_write_Dml = new Files_write_dml(_connstr);
                result = files_write_Dml.Write(action,
                                    filerow[CommonFields.rec_gid].ToString(),
                                    filerow[CommonFields.rec_user].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    filerow[FolderFields.GroupGID].ToString(),
                                    filerow[FolderFields.FolderGID].ToString(),
                                    filerow[FolderFields.FolderPath].ToString(),
                                    filerow[FileFields.FileName].ToString(),
                                    filerow[FileFields.FileDescrip].ToString(),
                                    filerow[FileFields.FileExt].ToString(),
                                    filerow[FileFields.FileSize].ToString(),
                                    filerow[FileFields.FileDate].ToString(),
                                    filerow[FileFields.FileHash].ToString(),
                                    filerow[FileFields.FileVersion].ToString(),
                                    filerow[FileFields.ShardName].ToString(),
                                    filerow[FileFields.SegSize].ToString(),
                                    filerow[FileFields.TotalSeg].ToString(),
                                    grouplist);
            }
            else
            {
                result = APIResult.Error + ": File " + row_id + " not found";
            }

            return result;
        }

    }
}
