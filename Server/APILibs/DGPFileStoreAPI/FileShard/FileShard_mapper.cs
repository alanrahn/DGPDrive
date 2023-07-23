using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using FileStoreDB.FileShard;
using SysMetricsDB;

namespace DGPFileStoreAPI.FileShard
{
    public class FileShard_mapper
    {
        string _appname;

        public FileShard_mapper()
        {
            _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetShardName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                FileShard_dml fileshard_Dml = new FileShard_dml();
                string shardname = fileshard_Dml.GetShardName();

                if (shardname != null && shardname != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, shardname);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
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
        public string GetByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string FileShardGID = msgUtil.GetParamValue(FileFields.FileShardGID, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }
                if (ShardName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (FileShardGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileShardGID; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    results = fileshard_Dml.GetByID(ShardName, FileShardGID, userinfo.ReadList);

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
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
        public string GetSegCount(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            string results = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string FileGID = msgUtil.GetParamValue(FileFields.FileGID, methparams);
                string FileVersion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (ShardName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (FileGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileGID; ";
                }
                if (FileVersion == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileVersion; ";
                }

                if (reqFields)
                {
                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    results = fileshard_Dml.GetSegCount(ShardName, FileGID, FileVersion, userinfo.ReadList);

                    if (results != null && results != "")
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, results);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, "Unable to retrieve the SegData from the FileShard record.");
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
        public string GetDataBySegNum(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            string results = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string FileGID = msgUtil.GetParamValue(FileFields.FileGID, methparams);
                string FileVersion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);
                string SegNum = msgUtil.GetParamValue(FileFields.SegNum, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (ShardName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (FileGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileGID; ";
                }
                if (FileVersion == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileVersion; ";
                }
                if (SegNum == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegNum; ";
                }

                if (reqFields)
                {
                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    results = fileshard_Dml.GetDataBySegNum(ShardName, FileGID, FileVersion, SegNum, userinfo.ReadList);

                    if (results != null && results != "")
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, results);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, "Unable to retrieve the SegData from the FileShard record.");
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
        /// used by replication process
        /// </summary>
        public string GetByRowID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string row_id = msgUtil.GetParamValue(CommonFields.row_id, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }
                if (ShardName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (row_id == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_id; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    results = fileshard_Dml.GetByRowID(ShardName, row_id, userinfo.ReadList);

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
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
        public string DupeCheck(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);

                FileShard_dml fileshard_Dml = new FileShard_dml();
                bool duplicates = fileshard_Dml.DupeCheck(ShardName, userinfo.UserName, _appname);

                if (!duplicates)
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, "No duplicates were found.");
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "Duplicates were found in the Tags table: refer to the error logs for details.");
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
        public string GetSrcCounts(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string shardname = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string srcdbname = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);
                string endid = msgUtil.GetParamValue(WorkFields.EndID, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (shardname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (srcdbname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcDBName; ";
                }
                if (endid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing EndID; ";
                }

                if (reqFields)
                {

                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    string srccounts = fileshard_Dml.GetSrcCounts(shardname, srcdbname, endid);

                    if (srccounts != null && srccounts != "")
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, srccounts);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
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
        public string GetDestCounts(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string srcdbname = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);
                string shardname = msgUtil.GetParamValue(FileFields.ShardName, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (srcdbname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcDBName; ";
                }
                if (shardname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }

                if (reqFields)
                {

                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    string destcounts = fileshard_Dml.GetDestCounts(shardname, srcdbname);

                    if (destcounts != null && destcounts != "")
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, destcounts);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
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
        public string GetSource(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string srcdbname = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);
                string placeholder = msgUtil.GetParamValue(WorkFields.StartID, methparams);
                string batchsize = msgUtil.GetParamValue(WorkFields.BatchSize , methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (ShardName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (SchemaFlag == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SchemaFlag; ";
                }
                else
                {
                    var isBoolean = bool.TryParse(SchemaFlag, out bool n);
                    if (!isBoolean)
                    {
                        reqFields = false;
                        reqFieldNames += "SchemaFlag must be a bool; ";
                    }
                }
                if (srcdbname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcDBName; ";
                }
                if (placeholder == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing PlaceHolder; ";
                }
                else
                {
                    var isNumeric = long.TryParse(placeholder, out long n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "Placeholder must be a long; ";
                    }
                }
                if (batchsize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing BatchSize; ";
                }
                else
                {
                    var isNumeric = int.TryParse(batchsize, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "BatchSize must be an int; ";
                    }
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    results = fileshard_Dml.GetSrcRecs(ShardName, srcdbname, placeholder, batchsize);

                    if (results.Rows.Count > 0)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                    }
                    else
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "");
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


        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */

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

                string FileShardGID = cmnUtil.GetNewGID();
                string new_row_ms = msgUtil.UnixTimeString();

                string GroupGID = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string FileGID = msgUtil.GetParamValue(FileFields.FileGID, methparams);
                string FileVersion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);
                string TotalSeg = msgUtil.GetParamValue(FileFields.TotalSeg, methparams);
                string SegNum = msgUtil.GetParamValue(FileFields.SegNum, methparams);
                string SegSize = msgUtil.GetParamValue(FileFields.SegSize, methparams);
                string SegData = msgUtil.GetParamValue(FileFields.SegData, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (GroupGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing GroupGID; ";
                }
                if (ShardName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (FileGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileGID; ";
                }
                if (FileVersion == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileVersion; ";
                }
                if (TotalSeg == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TotalSeg; ";
                }
                if (SegNum == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegNum; ";
                }
                if (SegSize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegSize; ";
                }
                if (SegData == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegData; ";
                }

                if (reqFields)
                {
                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    string rescode = fileshard_Dml.Write(ReplicaAction.Insert, FileShardGID, userinfo.UserGID, new_row_ms, "", GroupGID, ShardName, FileGID, FileVersion, TotalSeg, SegNum, SegSize, SegData, userinfo.WriteList);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, FileShardGID);
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
        /// Only used to delete existing shards - not to update fileshard record values (immutable)
        /// </summary>
        public string Save(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string new_row_ms = msgUtil.UnixTimeString();

                string FileShardGID = msgUtil.GetParamValue(FileFields.FileShardGID, methparams);
                string GroupGID = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
                string ShardName = msgUtil.GetParamValue(FileFields.ShardName, methparams);
                string FileGID = msgUtil.GetParamValue(FileFields.FileGID, methparams);
                string FileVersion = msgUtil.GetParamValue(FileFields.FileVersion, methparams);
                string TotalSeg = msgUtil.GetParamValue(FileFields.TotalSeg, methparams);
                string SegNum = msgUtil.GetParamValue(FileFields.SegNum, methparams);
                string SegSize = msgUtil.GetParamValue(FileFields.SegSize, methparams);
                string SegData = msgUtil.GetParamValue(FileFields.SegData, methparams);
                string edit_ms = msgUtil.GetParamValue(CommonFields.row_ms, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (action == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing action; ";
                }
                if (edit_ms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_ms; ";
                }
                if (FileShardGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileShardGID; ";
                }
                if (GroupGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing GroupGID; ";
                }
                if (ShardName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ShardName; ";
                }
                if (FileGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileGID; ";
                }
                if (FileVersion == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FileVersion; ";
                }
                if (TotalSeg == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TotalSeg; ";
                }
                if (SegNum == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegNum; ";
                }
                if (SegSize == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegSize; ";
                }
                if (SegData == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SegData; ";
                }

                if (reqFields)
                {
                    FileShard_dml fileshard_Dml = new FileShard_dml();
                    string rescode = fileshard_Dml.Write(action, FileShardGID, userinfo.UserGID, new_row_ms, edit_ms, GroupGID, ShardName, FileGID, FileVersion, TotalSeg, SegNum, SegSize, SegData, userinfo.WriteList);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, methodname + ": successful.");
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
        /// the process is the same as other tables, but each FileShard record is processed individually due to the 64K size limitation of API messages
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
                        FileShard_dml fileshard_Dml = new FileShard_dml();

                        string procstate = "";
                        foreach (DataRow dr in srcRecs.Rows)
                        {
                            string row_id = dr[CommonFields.row_id].ToString();
                            string rec_dbname = dr[CommonFields.rec_dbname].ToString();
                            string src_ms = dr[CommonFields.src_ms].ToString();
                            string rec_gid = dr[CommonFields.rec_gid].ToString();
                            string rec_state = dr[CommonFields.rec_state].ToString();

                            string ShardName = dr[FileFields.ShardName].ToString();
                            string GroupGID = dr[FolderFields.GroupGID].ToString();
                            string FileGID = dr[FileFields.FileGID].ToString();
                            string FileVersion = dr[FileFields.FileVersion].ToString();
                            string TotalSeg = dr[FileFields.TotalSeg].ToString();
                            string SegNum = dr[FileFields.SegNum].ToString();
                            string SegSize = dr[FileFields.SegSize].ToString();
                            string SegData = dr[FileFields.SegData].ToString();

                            procstate = fileshard_Dml.Replicate(row_id, src_ms, rec_dbname, rec_gid, rec_state, userinfo.UserGID, GroupGID, ShardName, FileGID, FileVersion, TotalSeg, SegNum, SegSize, SegData);

                            if (procstate == APIResult.OK || procstate == APIResult.Done)
                            {
                                newplaceholder = row_id;
                            }
                            else
                            {
                                string errmsg = "Tags replication error for row " + row_id + " : " + procstate;
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

    }
}
