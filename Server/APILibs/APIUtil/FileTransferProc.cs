
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;

using ApiUtil.DataClasses;

namespace ApiUtil
{
    public class FileTransferProc
    {
        public FileTransferProc()
        {

        }

        /// <summary>
        /// Used for replication of FileShard records;
        /// A batch of partial source records are processed in a loop one at a time, 
        /// downloading the full source record and then uploading (merging) it to the destination using the regular replication process
        /// </summary>
        public static ProcRes FileTransfer2(DataTable srctable,
                                            string startID,
                                            string userName,
                                            string passWord,
                                            string srcURL,
                                            string srcMethod,
                                            string destURL,
                                            string destMethod)
        {
            ProcRes procRes = new ProcRes();

            string _end_id = startID;

            if (srctable.Rows.Count > 0)
            {

                foreach (DataRow segsrc in srctable.Rows)
                {
                    // get row values
                    Dictionary<string, string> srcparams = new Dictionary<string, string>();
                    srcparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);
                    srcparams.Add(CommonFields.row_id, segsrc[CommonFields.row_id].ToString());
                    srcparams.Add(FileFields.ShardName, segsrc[FileFields.ShardName].ToString());

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methlist = msgUtil.CreateXMLMethod(srcMethod, srcparams);

                    // get complete source record with segdata for replication
                    string respmsg = msgUtil.ApiMethHelper(methlist,
                                                            reqID,
                                                            userName,
                                                            passWord,
                                                            srcURL);

                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult(srcMethod + "_" + MethReturn.Default, methresults);

                    if (methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        // get 1 record datatable returned
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable srcfileshard = cmnUtil.XmlToTable(methresult.RVal);

                        if (srcfileshard.Rows.Count == 1)
                        {
                            Dictionary<string, string> destparams = new Dictionary<string, string>();
                            destparams.Add(CommonFields.SourceRecords, methresult.RVal);
                            destparams.Add(WorkFields.StartID, _end_id);

                            string destmethlist = msgUtil.CreateXMLMethod(destMethod, destparams);

                            // pass 1 row datatable to the file shard replicate method
                            string destrespmsg = msgUtil.ApiMethHelper(destmethlist,
                                                                        reqID,
                                                                        userName,
                                                                        passWord,
                                                                        destURL);

                            RespInfo destrespinfo = new RespInfo();
                            Dictionary<string, ResInfo> destmethresults = msgUtil.ReadResponseString(destrespinfo, destrespmsg);

                            ResInfo destmethresult = msgUtil.GetResult(destMethod + "_" + MethReturn.Default, destmethresults);

                            if (destmethresult.RCode == APIResult.OK || destmethresult.RCode == APIResult.Done)
                            {
                                // return status and placeholder value
                                procRes.ResCode = APIResult.OK;
                                _end_id = destmethresult.RVal;
                            }
                            else
                            {
                                // upload error
                                procRes.ResCode = APIResult.Error;
                                procRes.ResMsg = "FileTransferProc.FileTransfer2: fileshard replication error: " + destmethresult.RVal;
                                break;
                            }
                        }
                        else
                        {
                            // incorrect number of rows in the fileshard source table
                            procRes.ResCode = APIResult.Error;
                            procRes.ResMsg = "FileTransferProc.FileTransfer2: fileshard source table rowcount error: " + srctable.Rows.Count.ToString();
                        }
                    }
                    else
                    {
                        // error in query for source records
                        procRes.ResCode = APIResult.Error;
                        procRes.ResMsg = "FileTransferProc.FileTransfer2: error in query for source records";
                    }
                }
            }
            else
            {
                // error in query for source records
                procRes.ResCode = APIResult.Error;
                procRes.ResMsg = "FileTransferProc.FileTransfer2: no records found in the datatable of source records.";
            }

            procRes.ResVal = _end_id;
            return procRes;
        }

        /// <summary>
        /// RUNS ON THE CLIENT
        /// Process that downloads each segment of an encrypted file from the source and then uploads the segment to the destination;
        /// if overall download/upload process is completed successfully, the uploaded temp file is copied into the destination storage
        /// and the file info record is replicated to the destination database;
        /// </summary>
        //public ProcRes FileTransfer(DataRow srcfileinfo,
        //                            string userName,
        //                            string passWord,
        //                            string srcURL,
        //                            string destURL,
        //                            string segsize)
        //{
        //    ProcRes procRes = new ProcRes();

        //    int _segSize = Convert.ToInt32(segsize);

        //    // calculate number of encrypted file segments from file length
        //    FileUtil fileUtil = new FileUtil();
        //    int _segNum = fileUtil.GetFileSegCount(Convert.ToInt64(srcfileinfo["EncFileSize"]), _segSize);
        //    string _row_id = srcfileinfo[CommonFields.row_id].ToString();

        //    // create collection of method params that work for both download and upload API method calls
        //    Dictionary<string, string> methparams = new Dictionary<string, string>();
        //    methparams.Add(CommonFields.row_id, _row_id);
        //    methparams.Add(CommonFields.rec_gid, srcfileinfo[CommonFields.rec_gid].ToString());
        //    methparams.Add(CommonFields.src_ms, srcfileinfo[CommonFields.src_ms].ToString());
        //    methparams.Add(CommonFields.rec_dbname, srcfileinfo[CommonFields.rec_dbname].ToString());
        //    methparams.Add(CommonFields.rec_state, srcfileinfo[CommonFields.rec_state].ToString());
        //    methparams.Add(CommonFields.rec_user, srcfileinfo[CommonFields.rec_user].ToString());

        //    methparams.Add(FolderFields.GroupGID, srcfileinfo[FolderFields.GroupGID].ToString());
        //    methparams.Add(FolderFields.FolderGID, srcfileinfo[FolderFields.FolderGID].ToString());
        //    methparams.Add(FileFields.FolderPath, srcfileinfo[FileFields.FolderPath].ToString());
        //    methparams.Add(FileFields.FileName, srcfileinfo[FileFields.FileName].ToString());
        //    methparams.Add(FileFields.FileDescrip, srcfileinfo[FileFields.FileDescrip].ToString());
        //    methparams.Add(FileFields.FileExt, srcfileinfo[FileFields.FileExt].ToString());
        //    methparams.Add(FileFields.FileSize, srcfileinfo[FileFields.FileSize].ToString());
        //    methparams.Add(FileFields.FileDate, srcfileinfo[FileFields.FileDate].ToString());
        //    methparams.Add(FileFields.FileHash, srcfileinfo[FileFields.FileHash].ToString());
        //    methparams.Add(FileFields.FileVersion, srcfileinfo[FileFields.FileVersion].ToString());
        //    methparams.Add(FileFields.EncFileSize, srcfileinfo[FileFields.EncFileSize].ToString());
        //    methparams.Add(FileFields.SvcKeyVersion, srcfileinfo[FileFields.SvcKeyVersion].ToString());
        //    methparams.Add(FileFields.StoragePath, srcfileinfo[FileFields.StoragePath].ToString());
        //    //methparams.Add(FileFields.UploadFlag, srcfileinfo[FileFields.UploadFlag].ToString());

        //    methparams.Add(FileFields.SegSize, _segSize.ToString());
        //    methparams.Add(FileFields.TotalSeg, _segNum.ToString());
        //    methparams.Add(FileFields.CurrentSeg, "0");
        //    methparams.Add(FileFields.SegData, "");

        //    // test for existence of storage path and file
        //    if (File.Exists(srcfileinfo[FileFields.StoragePath].ToString()))
        //    {
        //        MsgUtil msgUtil = new MsgUtil();

        //        // loop for number of segments
        //        for (long x = 1; x <= _segNum; x++)
        //        {
        //            string dataseg = "";

        //            // update segment param value
        //            methparams[FileFields.CurrentSeg] = x.ToString();

        //            // download encrypted file segment from source storage
        //            ResInfo downloadSeg = msgUtil.ApiMethHelper("File.Download.base",
        //                                                       userName,
        //                                                       passWord,
        //                                                       methparams,
        //                                                       srcURL);

        //            if (downloadSeg.RCode == APIResult.OK)
        //            {
        //                dataseg = downloadSeg.RVal;

        //                // update segment data param value
        //                methparams[FileFields.SegData] = dataseg;

        //                // upload encrypted file segment to destination storage;
        //                ResInfo uploadSeg = msgUtil.ApiMethHelper("File.Replicate.upload",
        //                                                            userName,
        //                                                            passWord,
        //                                                            methparams,
        //                                                            destURL);

        //                if (uploadSeg.RCode == APIResult.Done)
        //                {
        //                    procRes.ResCode = APIResult.OK;
        //                    procRes.ResVal = _row_id;
        //                    break;
        //                }
        //                else if (uploadSeg.RCode == APIResult.OK)
        //                {
        //                    if (x == _segNum)
        //                    {
        //                        // return status and placeholder value
        //                        procRes.ResCode = APIResult.OK;
        //                        procRes.ResVal = _row_id;
        //                    }
        //                }
        //                else
        //                {
        //                    // upload error
        //                    procRes.ResCode = APIResult.Error;
        //                    procRes.ResMsg = "FileTransferProc.FileTransfer: file segment upload error: " + uploadSeg.RVal;
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                // download error
        //                procRes.ResCode = APIResult.Error;
        //                procRes.ResMsg = "FileTransferProc.FileTransfer: file segment download error: " + downloadSeg.RVal;
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // download error
        //        procRes.ResCode = APIResult.Error;
        //        procRes.ResMsg = "FileTransferProc.FileTransfer: file segment download error: source file to be replicated not found.";
        //    }

        //    return procRes;
        //}


    }
}
