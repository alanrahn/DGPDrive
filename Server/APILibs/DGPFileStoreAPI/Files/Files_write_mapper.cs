using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using FileStoreDB.Files;
using SysMetricsDB;

namespace DGPFileStoreAPI.Files
{
    public class Files_write_mapper
    {
        string _connstr;
        string _appname;

        public Files_write_mapper(string connStr)
        {
            _connstr = connStr;
            _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public string New(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();
                string FileGID = cmnUtil.GetNewGID();
                string new_row_ms = msgUtil.UnixTimeString();

                string foldergid = msgUtil.GetParamValue(FolderFields.FolderGID, methparams);
                string folderpath = msgUtil.GetParamValue(FolderFields.FolderPath, methparams);
                string groupgid = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
                string shardname = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string filename = msgUtil.GetParamValue(FileFields.FileName, methparams);
                string filedescrip = msgUtil.GetParamValue(FileFields.FileDescrip, methparams);
                string fileext = msgUtil.GetParamValue(FileFields.FileExt, methparams);
                string filesize = msgUtil.GetParamValue(FileFields.FileSize, methparams);
                string filedate = msgUtil.GetParamValue(FileFields.FileDate, methparams);
                string filehash = msgUtil.GetParamValue(FileFields.FileHash, methparams);
                string fileversion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);
                string segsize = msgUtil.GetParamValue(FileFields.SegSize, methparams);
                string totalseg = msgUtil.GetParamValue(FileFields.TotalSeg, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";
                
                if (foldergid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderGID; ";
                }
                if (folderpath == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderPath; ";
                }
                if (groupgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing GroupGID; ";
                }
                if (filename == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileName; ";
                }
                if (fileext == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileExt; ";
                }
                if (filesize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileSize; ";
                }
                else
                {
                    var isNumeric = int.TryParse(filesize, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "FileSize must be an int; ";
                    }
                }
                if (filedate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileDate; ";
                }
                else
                {
                    var isDate = DateTime.TryParse(filedate, out DateTime d);
                    if (!isDate)
                    {
                        reqFields = false;
                        reqFieldNames += "FileDate must be a valid DateTime; ";
                    }
                }
                if (filehash == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileHash; ";
                }
                if (fileversion == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileVersion; ";
                }
                if (segsize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegSize value; ";
                }
                else
                {
                    var isNumeric = int.TryParse(segsize, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "SegSize must be an int; ";
                    }
                }
                if (totalseg == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TotalSeg value; ";
                }
                else
                {
                    var isNumeric = int.TryParse(totalseg, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "TotalSeg must be an int; ";
                    }
                }

                if (reqFields)
                {
                    Files_write_dml files_write_Dml = new Files_write_dml(_connstr);
                    string rescode = files_write_Dml.Write(ReplicaAction.Insert,
                                                            FileGID,
                                                            userinfo.UserGID,
                                                            new_row_ms,
                                                            "",
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
                                                            shardname,
                                                            segsize,
                                                            totalseg,
                                                            userinfo.WriteList);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, FileGID);
                        resultxml += msgUtil.CreateXMLResult(methodname, "RowMS", APIResult.OK, APIData.Text, new_row_ms);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// used to create a new version of the same file (in the latest writable shard)
        /// </summary>
        public string Save(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string new_row_ms = msgUtil.UnixTimeString();

                string edit_ms = msgUtil.GetParamValue(CommonFields.row_ms, methparams);
                string rec_gid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
                string foldergid = msgUtil.GetParamValue(FolderFields.FolderGID, methparams);
                string folderpath = msgUtil.GetParamValue(FolderFields.FolderPath, methparams);
                string groupgid = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
                string shardname = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string filename = msgUtil.GetParamValue(FileFields.FileName, methparams);
                string filedescrip = msgUtil.GetParamValue(FileFields.FileDescrip, methparams);
                string fileext = msgUtil.GetParamValue(FileFields.FileExt, methparams);
                string filesize = msgUtil.GetParamValue(FileFields.FileSize, methparams);
                string filedate = msgUtil.GetParamValue(FileFields.FileDate, methparams);
                string filehash = msgUtil.GetParamValue(FileFields.FileHash, methparams);
                string fileversion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);
                string segsize = msgUtil.GetParamValue(FileFields.SegSize, methparams);
                string totalseg = msgUtil.GetParamValue(FileFields.TotalSeg, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (edit_ms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_ms; ";
                }
                if (rec_gid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }
                if (foldergid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderGID; ";
                }
                if (folderpath == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderPath; ";
                }
                if (groupgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing GroupGID; ";
                }
                if (filename == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileName; ";
                }
                if (fileext == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileExt; ";
                }
                if (filesize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileSize; ";
                }
                else
                {
                    var isNumeric = int.TryParse(filesize, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "FileSize must be an int; ";
                    }
                }
                if (filedate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileDate; ";
                }
                else
                {
                    var isDate = DateTime.TryParse(filedate, out DateTime d);
                    if (!isDate)
                    {
                        reqFields = false;
                        reqFieldNames += "FileDate must be a valid DateTime; ";
                    }
                }
                if (filehash == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileHash; ";
                }
                if (fileversion == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileVersion; ";
                }
                if (segsize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegSize value; ";
                }
                else
                {
                    var isNumeric = int.TryParse(segsize, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "SegSize must be an int; ";
                    }
                }
                if (totalseg == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TotalSeg value; ";
                }
                else
                {
                    var isNumeric = int.TryParse(totalseg, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "TotalSeg must be an int; ";
                    }
                }

                if (reqFields)
                {
                    Files_write_dml files_write_Dml = new Files_write_dml(_connstr);
                    string rescode = files_write_Dml.Write(ReplicaAction.Update,
                                                            rec_gid,
                                                            userinfo.UserGID,
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
                                                            fileversion,
                                                            shardname,
                                                            segsize,
                                                            totalseg,
                                                            userinfo.WriteList);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, rec_gid);
                        resultxml += msgUtil.CreateXMLResult(methodname, "RowMS", APIResult.OK, APIData.Text, new_row_ms);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// only affects the File metadata record (no file shard records) to edit some fields or delete the File record
        /// </summary>
        public string SaveFileRec(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string filegid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
                string filename = msgUtil.GetParamValue(FileFields.FileName, methparams);
                string filedescrip = msgUtil.GetParamValue(FileFields.FileDescrip, methparams);
                string recstate = msgUtil.GetParamValue(CommonFields.rec_state, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (action == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing action value; ";
                }
                if (filegid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing file GID; ";
                }
                if (filename == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileName; ";
                }

                if (reqFields)
                {
                    Files_read_dml files_read_Dml = new Files_read_dml(_connstr);

                    DataTable filerecs = files_read_Dml.GetByID(filegid, userinfo.ReadList);

                    if (filerecs.Rows.Count > 0)
                    {
                        DataRow filerec = filerecs.Rows[0];
                        string new_row_ms = msgUtil.UnixTimeString();

                        Files_write_dml files_write_Dml = new Files_write_dml(_connstr);
                        string rescode = files_write_Dml.Write(action,
                                                                filegid,
                                                                userinfo.UserGID,
                                                                new_row_ms,
                                                                filerec[CommonFields.row_ms].ToString(),
                                                                filerec[FolderFields.GroupGID].ToString(),
                                                                filerec[FolderFields.FolderGID].ToString(),
                                                                filerec[FolderFields.FolderPath].ToString(),
                                                                filename,
                                                                filedescrip,
                                                                filerec[FileFields.FileExt].ToString(),
                                                                filerec[FileFields.FileSize].ToString(),
                                                                filerec[FileFields.FileDate].ToString(),
                                                                filerec[FileFields.FileHash].ToString(),
                                                                filerec[FileFields.FileVersion].ToString(),
                                                                filerec[FileFields.ShardName].ToString(),
                                                                filerec[FileFields.SegSize].ToString(),
                                                                filerec[FileFields.TotalSeg].ToString(),
                                                                userinfo.WriteList);

                        if (rescode != null && rescode == APIResult.OK)
                        {
                            resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, new_row_ms);
                        }
                        else
                        {
                            resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
                        }
                    }
                    else
                    {
                        // error - no records returned for file global ID
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " Error: No record found for file global ID.");
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        public string Recover(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string action = msgUtil.GetParamValue(CommonFields.Action, methparams);
                string recgid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
                string rowid = msgUtil.GetParamValue(CommonFields.row_id, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (recgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_gid; ";
                }
                if (rowid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_id; ";
                }
                if (action == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing action value; ";
                }

                if (reqFields)
                {
                    string new_row_ms = msgUtil.UnixTimeString();
                    FileProc2 fileProc = new FileProc2(_connstr);
                    string rescode = fileProc.RecoverFile(action, recgid, rowid, new_row_ms, userinfo.WriteList);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, new_row_ms);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Replicate(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();
            string newplaceholder = "";

            try
            {
                string srcrecs = msgUtil.GetParamValue(CommonFields.SourceRecords, methparams);
                string placeholder = msgUtil.GetParamValue(WorkFields.StartID, methparams);
                newplaceholder = placeholder;

                bool reqFields = true;
                string reqFieldNames = "";

                if (srcrecs == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SourceRecords value; ";
                }
                if (placeholder == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing Placeholder value; ";
                }

                if (reqFields)
                {
                    CmnUtil cmnUtil = new CmnUtil();
                    DataTable srcRecs = cmnUtil.XmlToTable(srcrecs);

                    if (srcRecs.Rows.Count > 0)
                    {
                        Files_write_dml files_write_Dml = new Files_write_dml(_connstr);

                        string procstate = "";
                        foreach (DataRow dr in srcRecs.Rows)
                        {
                            string row_id = dr[CommonFields.row_id].ToString();
                            string rec_dbname = dr[CommonFields.rec_dbname].ToString();
                            string src_ms = dr[CommonFields.src_ms].ToString();
                            string rec_gid = dr[CommonFields.rec_gid].ToString();
                            string rec_state = dr[CommonFields.rec_state].ToString();

                            string GroupGID = dr[FolderFields.GroupGID].ToString();
                            string FolderGID = dr[FolderFields.FolderGID].ToString();
                            string FolderPath = dr[FolderFields.FolderPath].ToString();
                            string FileName = dr[FileFields.FileName].ToString();
                            string FileDescrip = dr[FileFields.FileDescrip].ToString();
                            string FileExt = dr[FileFields.FileExt].ToString();
                            string FileSize = dr[FileFields.FileSize].ToString();
                            string FileDate = dr[FileFields.FileDate].ToString();
                            string FileHash = dr[FileFields.FileHash].ToString();
                            string FileVersion = dr[FileFields.FileVersion].ToString();
                            string ShardName = dr[FileFields.ShardName].ToString();
                            string SegSize = dr[FileFields.SegSize].ToString();
                            string TotalSegs = dr[FileFields.TotalSeg].ToString();

                            procstate = files_write_Dml.Replicate(row_id,
                                                                src_ms,
                                                                rec_dbname,
                                                                rec_gid,
                                                                rec_state,
                                                                userinfo.UserGID,
                                                                GroupGID,
                                                                FolderGID,
                                                                FolderPath,
                                                                FileName,
                                                                FileDescrip,
                                                                FileExt,
                                                                FileSize,
                                                                FileDate,
                                                                FileHash,
                                                                FileVersion,
                                                                ShardName,
                                                                SegSize,
                                                                TotalSegs,
                                                                _connstr);

                            if (procstate == APIResult.OK || procstate == APIResult.Done)
                            {
                                newplaceholder = row_id;
                            }
                            else
                            {
                                string errmsg = "Files replication error for row " + row_id + " : " + procstate;
                                ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Replication error.", errmsg);
                                break;
                            }
                        }

                        // return new placeholder value (errors will prevent the new placeholder value from advancing until corrected)
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, procstate, APIData.Text, newplaceholder);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, newplaceholder);
                    }
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, newplaceholder);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, newplaceholder);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        //public string Upload(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        //{
        //    string resultxml = "";
        //    MsgUtil msgUtil = new MsgUtil();

        //    try
        //    {
        //        string action = msgUtil.GetParamValue(CommonFields.Action, methparams);
        //        string rec_gid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
        //        string edit_ms = msgUtil.GetParamValue(CommonFields.row_ms, methparams);
        //        string new_row_ms = msgUtil.UnixTimeString();

        //        string foldergid = msgUtil.GetParamValue(FolderFields.FolderGID, methparams);
        //        string folderpath = msgUtil.GetParamValue(FileFields.FolderPath, methparams);
        //        string groupgid = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
        //        string filename = msgUtil.GetParamValue(FileFields.FileName, methparams);
        //        string filedescrip = msgUtil.GetParamValue(FileFields.FileDescrip, methparams);
        //        string fileext = msgUtil.GetParamValue(FileFields.FileExt, methparams);
        //        string filesize = msgUtil.GetParamValue(FileFields.FileSize, methparams);
        //        string filedate = msgUtil.GetParamValue(FileFields.FileDate, methparams);
        //        string filehash = msgUtil.GetParamValue(FileFields.FileHash, methparams);
        //        string fileversion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);
        //        string encfilesize = msgUtil.GetParamValue(FileFields.EncFileSize, methparams);
        //        string segsize = msgUtil.GetParamValue(FileFields.SegSize, methparams);
        //        string totalseg = msgUtil.GetParamValue(FileFields.TotalSeg, methparams);
        //        string currseg = msgUtil.GetParamValue(FileFields.CurrentSeg, methparams);
        //        string segdata = msgUtil.GetParamValue(FileFields.SegData, methparams);

        //        // test for required input parameters
        //        bool reqFields = true;
        //        string reqFieldNames = "";

        //        if (action == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing action value; ";
        //        }

        //        if (action == ReplicaAction.Update && rec_gid == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing rec_gid; ";
        //        }

        //        if (action == ReplicaAction.Update && edit_ms == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing row_ms; ";
        //        }

        //        if (foldergid == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FolderGID; ";
        //        }

        //        if (folderpath == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FolderPath; ";
        //        }

        //        if (groupgid == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing GroupGID; ";
        //        }

        //        if (filename == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileName; ";
        //        }

        //        if (fileext == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileExt; ";
        //        }

        //        if (filesize == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileSize; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(filesize, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "FileSize must be an int; ";
        //            }
        //        }

        //        if (filedate == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileDate; ";
        //        }
        //        else
        //        {
        //            var isDate = DateTime.TryParse(filedate, out DateTime d);
        //            if (!isDate)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "FileDate must be a valid DateTime; ";
        //            }
        //        }

        //        if (filehash == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileHash; ";
        //        }

        //        if (fileversion == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileVersion; ";
        //        }

        //        if (segsize == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing SegSize value; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(segsize, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "SegSize must be an int; ";
        //            }
        //        }

        //        if (totalseg == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing TotalSeg value; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(totalseg, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "TotalSeg must be an int; ";
        //            }
        //        }

        //        if (currseg == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing CurrentSeg value; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(currseg, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "CurrentSeg must be an int; ";
        //            }
        //        }

        //        if (segdata == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing SegData; ";
        //        }

        //        if (reqFields)
        //        {
        //            if (action == ReplicaAction.Insert)
        //            {
        //                CmnUtil cmnUtil = new CmnUtil();
        //                rec_gid = cmnUtil.GetNewGID();
        //            }

        //            FileProc fileProc = new FileProc(_connstr);
        //            string rescode = fileProc.APIFileUpload(action,
        //                                                    rec_gid,
        //                                                    new_row_ms, 
        //                                                    edit_ms,
        //                                                    foldergid, 
        //                                                    groupgid,
        //                                                    folderpath,
        //                                                    filename, 
        //                                                    filedescrip,
        //                                                    fileext, 
        //                                                    filesize, 
        //                                                    filedate, 
        //                                                    filehash, 
        //                                                    fileversion,
        //                                                    encfilesize,
        //                                                    segsize, 
        //                                                    totalseg, 
        //                                                    currseg, 
        //                                                    segdata, 
        //                                                    userinfo.UserGID, 
        //                                                    userinfo.WriteList);

        //            if (rescode != null && rescode == APIResult.OK)
        //            {
        //                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, methodname + ": successful.");
        //            }
        //            else
        //            {
        //                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + rescode);
        //            }
        //        }
        //        else
        //        {
        //            resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
        //        ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
        //    }

        //    return resultxml;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public string ReplicateUpload(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        //{
        //    string resultxml = "";
        //    MsgUtil msgUtil = new MsgUtil();
        //    string _StoragePath;

        //    try
        //    {
        //        string row_id = msgUtil.GetParamValue(CommonFields.row_id, methparams);
        //        string rec_dbname = msgUtil.GetParamValue(CommonFields.rec_dbname, methparams);
        //        string src_ms = msgUtil.GetParamValue(CommonFields.src_ms, methparams);
        //        string rec_gid = msgUtil.GetParamValue(CommonFields.rec_gid, methparams);
        //        string rec_state = msgUtil.GetParamValue(CommonFields.rec_state, methparams);
        //        string rec_user = msgUtil.GetParamValue(CommonFields.rec_user, methparams);

        //        string GroupGID = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
        //        string FolderGID = msgUtil.GetParamValue(FolderFields.FolderGID, methparams);
        //        string FolderPath = msgUtil.GetParamValue(FileFields.FolderPath, methparams);
        //        string FileName = msgUtil.GetParamValue(FileFields.FileName, methparams);
        //        string FileDescrip = msgUtil.GetParamValue(FileFields.FileDescrip, methparams);
        //        string FileExt = msgUtil.GetParamValue(FileFields.FileExt, methparams);
        //        string FileSize = msgUtil.GetParamValue(FileFields.FileSize, methparams);
        //        string FileDate = msgUtil.GetParamValue(FileFields.FileDate, methparams);
        //        string FileHash = msgUtil.GetParamValue(FileFields.FileHash, methparams);
        //        string FileVersion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);
        //        string EncFileSize = msgUtil.GetParamValue(FileFields.EncFileSize, methparams);
        //        _StoragePath = msgUtil.GetParamValue(FileFields.StoragePath, methparams);
        //        string SvcKeyVersion = msgUtil.GetParamValue(FileFields.SvcKeyVersion, methparams);
        //        //string UploadFlag = msgUtil.GetParamValue(FileFields.UploadFlag, methparams);

        //        string segsize = msgUtil.GetParamValue(FileFields.SegSize, methparams);
        //        string totalseg = msgUtil.GetParamValue(FileFields.TotalSeg, methparams);
        //        string currseg = msgUtil.GetParamValue(FileFields.CurrentSeg, methparams);
        //        string segdata = msgUtil.GetParamValue(FileFields.SegData, methparams);

        //        // test for required input parameters
        //        bool reqFields = true;
        //        string reqFieldNames = "";

        //        if (row_id == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing row_id value; ";
        //        }

        //        if (rec_dbname == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing rec_dbname; ";
        //        }

        //        if (src_ms == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing src_ms; ";
        //        }    

        //        if (rec_gid == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing rec_gid; ";
        //        }

        //        if (rec_state == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing rec_state; ";
        //        }

        //        if (rec_user == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing rec_user; ";
        //        }

        //        if (GroupGID == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing GroupGID; ";
        //        }

        //        if (FolderGID == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FolderGID; ";
        //        }

        //        if (FolderPath == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FolderPath; ";
        //        }

        //        if (FileName == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileName; ";
        //        }

        //        if (FileExt == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileExt; ";
        //        }

        //        if (FileSize == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileSize; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(FileSize, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "FileSize must be an int; ";
        //            }
        //        }

        //        if (FileDate == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileDate; ";
        //        }
        //        else
        //        {
        //            var isDate = DateTime.TryParse(FileDate, out DateTime d);
        //            if (!isDate)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "FileDate must be a valid DateTime; ";
        //            }
        //        }

        //        if (FileHash == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileHash; ";
        //        }

        //        if (FileVersion == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing FileVersion; ";
        //        }

        //        if (_StoragePath == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing StoragePath; ";
        //        }

        //        if (SvcKeyVersion == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing SvcKeyVersion; ";
        //        }

        //        //if (UploadFlag == "")
        //        //{
        //        //    reqFields = false;
        //        //    reqFieldNames += "Missing UploadFlag; ";
        //        //}


        //        if (segsize == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing SegSize value; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(segsize, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "SegSize must be an int; ";
        //            }
        //        }

        //        if (totalseg == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing TotalSeg value; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(totalseg, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "TotalSeg must be an int; ";
        //            }
        //        }

        //        if (currseg == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing CurrentSeg value; ";
        //        }
        //        else
        //        {
        //            var isNumeric = int.TryParse(currseg, out int n);
        //            if (!isNumeric)
        //            {
        //                reqFields = false;
        //                reqFieldNames += "CurrentSeg must be an int; ";
        //            }
        //        }

        //        if (segdata == "")
        //        {
        //            reqFields = false;
        //            reqFieldNames += "Missing SegData; ";
        //        }

        //        if (reqFields)
        //        {
        //            FileProc fileProc = new FileProc(_connstr);
        //            ProcRes fileInfoRes = fileProc.ReplicateFileUpload(row_id,
        //                                                                src_ms,
        //                                                                rec_dbname,
        //                                                                rec_gid,
        //                                                                rec_state,
        //                                                                userinfo.UserGID,
        //                                                                GroupGID,
        //                                                                FolderGID,
        //                                                                FolderPath,
        //                                                                FileName,
        //                                                                FileDescrip,
        //                                                                FileExt,
        //                                                                FileSize,
        //                                                                FileDate,
        //                                                                FileHash,
        //                                                                FileVersion,
        //                                                                EncFileSize,
        //                                                                _StoragePath,
        //                                                                SvcKeyVersion,
        //                                                                //UploadFlag,
        //                                                                segsize,
        //                                                                totalseg,
        //                                                                currseg,
        //                                                                segdata,
        //                                                                userinfo.WriteList);

        //            if (fileInfoRes.ResCode == APIResult.OK)
        //            {
        //                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, methodname + ": successful.");
        //            }
        //            else
        //            {
        //                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + fileInfoRes.ResMsg);
        //                ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Replication error.", fileInfoRes.ResMsg);
        //            }
        //        }
        //        else
        //        {

        //            resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
        //            ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "Required fields error.", reqFieldNames);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
        //        ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
        //    }

        //    return resultxml;
        //}


    }
}
