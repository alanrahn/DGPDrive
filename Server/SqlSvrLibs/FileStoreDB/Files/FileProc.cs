using System.Data;
using System.Configuration;
using System;
using System.IO;

using ApiUtil;
using ApiUtil.DataClasses;
using SysMetricsDB;

namespace FileStoreDB.Files
{
    public class FileProc
    {
        string _connstr;

        public FileProc(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// recovering a deleted record inserts a new version, while recovering an edited record uses update logic
        /// </summary>
        public string RecoverFile(string action,
                                string rec_gid,
                                string row_id,
                                string grouplist)
        {
            string result = "";

            // get existing user record
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

                MsgUtil msgUtil = new MsgUtil();
                string new_row_ms = msgUtil.UnixTimeString();

                Files_write_dml files_write_Dml = new Files_write_dml(_connstr);
                result = files_write_Dml.Write(action,
                                    filerow["rec_gid"].ToString(),
                                    filerow["rec_user"].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    filerow["GroupGID"].ToString(),
                                    filerow["FolderGID"].ToString(),
                                    filerow["FolderPath"].ToString(),
                                    filerow["FileName"].ToString(),
                                    filerow["FileDescrip"].ToString(),
                                    filerow["FileExt"].ToString(),
                                    filerow["FileSize"].ToString(),
                                    filerow["FileDate"].ToString(),
                                    filerow["FileHash"].ToString(),
                                    filerow["FileVersion"].ToString(),
                                    filerow["EncFileSize"].ToString(),
                                    filerow["StoragePath"].ToString(),
                                    filerow["SvcKeyVersion"].ToString(),
                                    BoolVal.FALSE,
                                    grouplist);
            }
            else
            {
                result = APIResult.Error + ": File " + row_id + " not found";
            }

            return result;
        }

        /// <summary>
        /// process is called repeatedly by remote client app to read a data segment from local temp file;
        /// first segment called retrieves file from storage and decrypts it,
        /// last segment deletes the temp file used for downloading file segments
        /// </summary>
        public string APIFileDownload(string filegid,
                                        string totseg,
                                        string currseg,
                                        string userGID,
                                        string groupList)
        {
            // returning empty string indicates an error
            string procresult = "";

            // compare segment size to web service max segment size
            int maxsegsize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxSegSize"]);

            // use FileGID to get file info
            Files_read_dml files_read_Dml = new Files_read_dml(_connstr);
            DataTable filedata = files_read_Dml.GetByID(filegid, groupList);

            if (filedata.Rows.Count > 0)
            {
                DataRow dr = filedata.Rows[0];

                string groupgid = dr[DataGroupFields.GroupGID].ToString();

                if (groupList.IndexOf(groupgid, 0) != -1)
                {
                    string storagepath = dr["StoragePath"].ToString();

                    int currentseg = Convert.ToInt32(currseg);
                    long segoffset = maxsegsize * (currentseg - 1);
                    int totalseg = Convert.ToInt32(totseg);

                    if (File.Exists(storagepath))
                    {
                        FileUtil fileUtil = new FileUtil();

                        // read data segment from the encrypted file in storage 
                        byte[] readseg = fileUtil.ReadFileSegment(storagepath, segoffset);

                        procresult = Convert.ToBase64String(readseg);
                    }
                    else
                    {
                        // file segment error
                        ServerErrLog.LogError(userGID, "FileProc", "APIFileDownload", "Segment read error.", "Error reading a segment from the encrypted storage file");
                    }
                }
                else
                {
                    // not authorized to read from the DataGroup
                    ServerErrLog.LogError(userGID, "FileProc", "APIFileDownload", "Authorization error.", "User account is not authorized to read from the DataGroup");
                }
            }
            else
            {
                // file data record query error
                ServerErrLog.LogError(userGID, "FileProc", "APIFileDownload", "File metadata error.", "Error querying the database for the file data record");
            }

            return procresult;
        }

        /// <summary>
        /// process is called repeatedly by remote client app to append a data segment to local temp file;
        /// once the encrypted temp file is complete, the file is copied into storage;
        /// this version limits one file upload at a time for each account (temp file is named using the account global ID value)
        /// </summary>
        public string APIFileUpload(string action,
                                        string filegid,
                                        string new_row_ms,
                                        string edit_ms,
                                        string foldergid,
                                        string groupgid,
                                        string folderpath,
                                        string filename,
                                        string filedescrip,
                                        string fileext,
                                        string filesize,
                                        string filedate,
                                        string filehash,
                                        string fileversion,
                                        string encfilesize,
                                        string segsize,
                                        string totalseg,
                                        string currseg,
                                        string segdata,
                                        string userGID,
                                        string groupList)
        {
            string procresult = APIResult.Error;

            // compare segment size to web service max segment size
            int maxfilesize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFileSize"]);
            int maxsegsize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxSegSize"]);
            int currsegsize = Convert.ToInt32(segsize);
            int currsegnum = Convert.ToInt32(currseg);
            int totalsegnum = Convert.ToInt32(totalseg);
            int currfilesize = Convert.ToInt32(filesize);

            if (groupList.IndexOf(groupgid, 0) != -1)
            {
                bool fileSizeOK = false;

                if (currfilesize <= maxfilesize)
                {
                    // segment should be smaller than max only for last partial segment or small files with only one segment
                    if (currsegsize == maxsegsize || (currsegsize < maxsegsize && (currsegnum == totalsegnum || totalsegnum == 1)))
                    {
                        fileSizeOK = true;
                    }
                }

                if (fileSizeOK)
                {
                    // convert segdata from base64 to byte array
                    byte[] fileseg = Convert.FromBase64String(segdata);

                    string TempUploadPath = ConfigurationManager.AppSettings["TempUploadPath"].ToString();
                    string tempfullpath = TempUploadPath + "\\" + userGID + ".upload";

                    // if first segment, make sure tempfile does not already exist (each temp file is named as the user account global ID value)
                    if (currseg == "1")
                    {
                        // delete existing temp file
                        if (File.Exists(tempfullpath))
                        {
                            File.Delete(tempfullpath);
                        }
                    }

                    // append data segment to the local server temp file 
                    FileUtil fileUtil = new FileUtil();
                    bool appended = fileUtil.AppendFileSegment(tempfullpath, fileseg);

                    if (appended)
                    {
                        procresult = APIResult.OK;

                        // if temp encrypted file is complete
                        if (currseg == totalseg)
                        {
                            // check temp file length vs original encrypted file length
                            bool filematch = false;
                            FileInfo fi = new FileInfo(tempfullpath);
                            if (fi.Length == Convert.ToInt64(encfilesize)) filematch = true;

                            // if file lengths match:
                            if (filematch)
                            {
                                string fileGID = "";
                                int fileVer = 0;

                                MsgUtil msgUtil = new MsgUtil();

                                if (action == ReplicaAction.Insert)
                                {
                                    // new file
                                    CmnUtil cmnUtil = new CmnUtil();
                                    fileGID = cmnUtil.GetNewGID();
                                    edit_ms = new_row_ms;
                                    fileVer = 1;
                                }
                                else
                                {
                                    // file update
                                    fileGID = filegid;
                                    int priorversion = Convert.ToInt32(fileversion);
                                    priorversion++;
                                    fileVer = priorversion;
                                }

                                // calculate relative file path from file date, combine with storage path, and create full path on file server
                                string storageDir = ConfigurationManager.AppSettings["StorageRootPath"].ToString();
                                DateTime fileDate = Convert.ToDateTime(filedate);
                                string storagePath = fileUtil.CreateFilePath(storageDir, fileDate);
                                string storageFullPath = storagePath + "\\" + fileGID + "_" + fileVer.ToString() + ".dgpenc";

                                // check if file already exist in the storage folder
                                bool stored = false;
                                if (File.Exists(storageFullPath))
                                {
                                    FileInfo fileInfo = new FileInfo(storageFullPath);
                                    if (fileInfo.Length == Convert.ToInt64(encfilesize))
                                    {
                                        stored = true;
                                    }
                                    else
                                    {
                                        File.Delete(storageFullPath);
                                    }

                                    //string[] files = Directory.GetFiles(storagePath, fileGID + "_" + fileVer.ToString() + "*");

                                    //if (files.Length > 0)
                                    //{
                                    //    int copynum = files.Length + 1;
                                    //    storageFullPath = storagePath + "\\" + fileGID + "(" + copynum.ToString() + ")_" + fileVer.ToString() + ".dgpenc";
                                    //}
                                }
                                else
                                {
                                    // store encrypted temp file to: full storage path \ filegid_ver#.dgpenc
                                    stored = fileUtil.CopyFile(tempfullpath, storageFullPath);
                                }

                                // if storage successful, 
                                string svcKeyVersion = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();
                                if (stored)
                                {
                                    // create new/updated file record with all information
                                    Files_write_dml files_write_Dml = new Files_write_dml(_connstr);
                                    string result = files_write_Dml.Write(action,
                                                                        fileGID,
                                                                        userGID,
                                                                        new_row_ms,
                                                                        edit_ms,
                                                                        groupgid,
                                                                        foldergid,
                                                                        folderpath,
                                                                        filename,
                                                                        filedescrip,
                                                                        fileext,
                                                                        filesize,
                                                                        filedate,
                                                                        filehash,
                                                                        fileVer.ToString(),
                                                                        encfilesize,
                                                                        storageFullPath,
                                                                        svcKeyVersion,
                                                                        BoolVal.TRUE,
                                                                        groupList);

                                    if (result != null && result == APIResult.OK)
                                    {
                                        procresult = APIResult.OK;

                                        // cleanup:  delete temp file
                                        if (File.Exists(tempfullpath))
                                        {
                                            // delete temp file
                                            File.Delete(tempfullpath);
                                        }
                                    }
                                    else
                                    {
                                        // file record problem - rollback storage (delete storage and temp files)
                                        procresult = "Error creating the file record during file upload: " + result;
                                        ServerErrLog.LogError(userGID, "DGPWebSvc", "FileProc.APIFileUpload", "Error creating the database file record during file upload.", result);

                                        if (File.Exists(tempfullpath))
                                        {
                                            // delete temp file
                                            File.Delete(tempfullpath);
                                        }

                                        if (File.Exists(storageFullPath))
                                        {
                                            // delete storage file
                                            File.Delete(storageFullPath);
                                        }
                                    }
                                }
                                else
                                {
                                    // storage problem
                                    procresult = "Error storing the temp file into storage";
                                    ServerErrLog.LogError(userGID, "DGPWebSvc", "FileProc.APIFileUpload", "File storage error.", procresult);
                                }
                            }
                            else
                            {
                                // file match error
                                procresult = "The file length of the upload did not match the original file length";
                                ServerErrLog.LogError(userGID, "DGPWebSvc", "FileProc.APIFileUpload", "File storage error.", procresult);
                            }
                        }
                    }
                    else
                    {
                        // append error
                        procresult = "Error appending a file segment to the temp file during file upload";
                        ServerErrLog.LogError(userGID, "DGPWebSvc", "FileProc.APIFileUpload", "File storage error.", procresult);
                    }
                }
                else
                {
                    // segment size or file size error
                    if (currsegsize > maxsegsize)
                    {
                        procresult = "segment size is larger than the web service max segment size";
                    }
                    else if (currfilesize > maxfilesize)
                    {
                        procresult = "file size is larger than the web service max file size";
                    }
                    else
                    {
                        procresult = "a partial segment can only be a single small file or the last segment of a larger file";
                    }

                    ServerErrLog.LogError(userGID, "DGPWebSvc", "FileProc.APIFileUpload", "File storage error.", procresult);
                }
            }
            else
            {
                // not authorized to write to the datagroup
                procresult = "User account is not authorized to write to the DataGroup";
                ServerErrLog.LogError(userGID, "DGPWebSvc", "FileProc.APIFileUpload", "File authorization error.", procresult);
            }

            return procresult;
        }

        /// <summary>
        /// Runs on SERVER
        /// process is called repeatedly by remote client app to append a data segment to dest storage file;
        /// </summary>
        public ProcRes ReplicateFileUpload(string src_id,
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
                                        string encfilesize,
                                        string storepath,
                                        string svckeyversion,
                                        //string uploadflag,
                                        string segsize,
                                        string totalseg,
                                        string currseg,
                                        string segdata,
                                        string groupList)
        {
            ProcRes procRes = new ProcRes();
            procRes.ResCode = APIResult.OK;

            // compare segment size to web service max segment size
            int maxfilesize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFileSize"]);
            int maxsegsize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxSegSize"]);
            int currsegsize = Convert.ToInt32(segsize);
            int currsegnum = Convert.ToInt32(currseg);
            int totalsegnum = Convert.ToInt32(totalseg);
            int currfilesize = Convert.ToInt32(filesize);

            if (groupList.IndexOf(groupgid, 0) != -1)
            {
                string TempUploadPath = ConfigurationManager.AppSettings["TempUploadPath"].ToString();
                string tempfullpath = TempUploadPath + "\\" + rec_gid + ".upload";

                bool fileSizeOK = false;
                bool segSizeOK = false;
                bool stored = false;

                // check if file is already in storage
                if (File.Exists(storepath))
                {
                    FileInfo fi = new FileInfo(storepath);
                    if (fi.Length == Convert.ToInt64(encfilesize)) stored = true;
                }

                if (!stored)
                {
                    if (currfilesize <= maxfilesize)
                    {
                        fileSizeOK = true;

                        // segment should be smaller than max only for last partial segment or small files with only one segment
                        if (currsegsize == maxsegsize || (currsegsize < maxsegsize && (currsegnum == totalsegnum || totalsegnum == 1)))
                        {
                            segSizeOK = true;
                        }
                    }

                    if (fileSizeOK && segSizeOK)
                    {
                        // convert segdata from base64 to byte array
                        byte[] fileseg = Convert.FromBase64String(segdata);

                        // if first segment, make sure tempfile does not already exist (each temp file is named as the file global ID value)
                        if (currseg == "1")
                        {
                            // delete existing temp file
                            if (File.Exists(tempfullpath))
                            {
                                File.Delete(tempfullpath);
                            }
                        }

                        // append data segment to the local server temp file 
                        FileUtil fileUtil = new FileUtil();
                        bool appended = fileUtil.AppendFileSegment(tempfullpath, fileseg);

                        if (appended)
                        {
                            procRes.ResCode = APIResult.OK;

                            // if temp encrypted file is complete
                            if (currseg == totalseg)
                            {
                                // check temp file length vs original encrypted file length
                                FileInfo fi = new FileInfo(tempfullpath);
                                if (fi.Length == Convert.ToInt64(encfilesize))
                                {
                                    string dirpath = Path.GetDirectoryName(storepath);
                                    fileUtil.CheckDirPath(dirpath);
                                    stored = fileUtil.CopyFile(tempfullpath, storepath);
                                }
                                else
                                {
                                    // file match error
                                    procRes.ResCode = APIResult.Error;
                                    procRes.ResMsg = "The file length of the upload did not match the original file length";
                                    ServerErrLog.LogError("System Account", "DGPWebSvc", "FileProc.ReplicateFileUpload", "File storage error.", procRes.ResMsg);
                                }
                            }
                        }
                        else
                        {
                            // append error
                            procRes.ResCode = APIResult.Error;
                            procRes.ResMsg = "Error appending a file segment to the temp file during file upload";
                            ServerErrLog.LogError("System Account", "DGPWebSvc", "FileProc.ReplicateFileUpload", "File storage error.", procRes.ResMsg);
                        }
                    }
                    else
                    {
                        // segment size or file size error
                        procRes.ResCode = APIResult.Error;
                        if (currsegsize > maxsegsize)
                        {
                            procRes.ResMsg = "segment size is larger than the web service max segment size";
                        }
                        else if (currfilesize > maxfilesize)
                        {
                            procRes.ResMsg = "file size is larger than the web service max file size";
                        }
                        else
                        {
                            procRes.ResMsg = "a partial segment can only be a single small file or the last segment of a larger file";
                        }

                        ServerErrLog.LogError("System Account", "DGPWebSvc", "FileProc.ReplicateFileUpload", "File storage error.", procRes.ResMsg);
                    }
                }

                // if file storage successful
                if (stored)
                {
                    // replicate file record
                    Files_write_dml files_write_Dml = new Files_write_dml(_connstr);
                    string result = files_write_Dml.Replicate(src_id,
                                                            src_ms,
                                                            rec_dbname,
                                                            rec_gid,
                                                            rec_state,
                                                            rec_user,
                                                            groupgid,
                                                            foldergid,
                                                            folderpath,
                                                            filename,
                                                            filedescrip,
                                                            fileext,
                                                            filesize,
                                                            filedate,
                                                            filehash,
                                                            fileversion,
                                                            encfilesize,
                                                            storepath,
                                                            svckeyversion,
                                                            BoolVal.FALSE,
                                                            _connstr);

                    if (result != null && (result == APIResult.OK || result == APIResult.Done))
                    {
                        procRes.ResCode = APIResult.Done;
                    }
                    else
                    {
                        // file record problem - 
                        procRes.ResCode = APIResult.Error;
                        procRes.ResMsg = "Error replicating the database file record during replication file upload: " + result;
                        ServerErrLog.LogError("System Account", "DGPWebSvc", "FileProc.ReplicateFileUpload", "File metadata error.", procRes.ResMsg);

                        // rollback storage (delete storage file)?
                        //if (File.Exists(storepath))
                        //{
                        //    // delete storage file
                        //    File.Delete(storepath);
                        //}
                    }

                    // cleanup:  delete temp file
                    if (File.Exists(tempfullpath))
                    {
                        // delete temp file
                        File.Delete(tempfullpath);
                    }
                }
                else
                {
                    // storage problem
                    procRes.ResCode = APIResult.Error;
                    procRes.ResMsg = "Error copying the temp file into storage";
                    ServerErrLog.LogError("System Account", "DGPWebSvc", "FileProc.ReplicateFileUpload", "File storage error.", procRes.ResMsg);
                }
            }
            else
            {
                // not authorized to write to the datagroup
                procRes.ResCode = APIResult.Error;
                procRes.ResMsg = "User account is not authorized to write to the DataGroup";
                ServerErrLog.LogError("System Account", "DGPWebSvc", "FileProc.ReplicateFileUpload", "File authorization error.", procRes.ResMsg);
            }

            return procRes;
        }


    }
}
