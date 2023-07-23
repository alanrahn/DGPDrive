using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using FileStoreDB.Folders;
using SysMetricsDB;

namespace DGPFileStoreAPI.Folders
{
    public class Folders_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public Folders_mapper(string connStr)
        {
            _connstr = connStr;
            _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetPageSize(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, _pagesize);
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
        public string GetUserSubFolders(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string FolderGID = msgUtil.GetParamValue(FolderFields.FolderGID, methparams);

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
                if (FolderGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderGID; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    Folders_dml folders_Dml = new Folders_dml(_connstr);
                    results = folders_Dml.GetUserSubFolders(FolderGID, userinfo.ReadList);

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
        public string GetByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string FolderGID = msgUtil.GetParamValue(FolderFields.FolderGID, methparams);

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
                if (FolderGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderGID; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    Folders_dml folders_Dml = new Folders_dml(_connstr);
                    results = folders_Dml.GetByID(FolderGID, userinfo.ReadList);

                    if (results.Rows.Count > 0)
                    {
                        DataRow dr = results.Rows[0];
                        string row_id = dr[CommonFields.row_id].ToString();
                        CmnUtil cmnUtil = new CmnUtil();
                        string tblxml = cmnUtil.TableToXml(results, schemaflag);
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.DataTable, tblxml);
                        resultxml += msgUtil.CreateXMLResult(methodname, "RowID", APIResult.OK, APIData.Text, row_id);
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
                Folders_dml folders_Dml = new Folders_dml(_connstr);
                bool duplicates = folders_Dml.DupeCheck(userinfo.UserName, _appname);

                if (!duplicates)
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, "No duplicates were found.");
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Empty, APIData.Text, "Duplicates were found in the Folders table: refer to the error logs for details.");
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
                string srcdbname = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);
                string endid = msgUtil.GetParamValue(WorkFields.EndID, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

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

                    Folders_dml folders_Dml = new Folders_dml(_connstr);
                    string srccounts = folders_Dml.GetSrcCounts(srcdbname, endid);

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

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (srcdbname == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing SrcDBName; ";
                }

                if (reqFields)
                {

                    Folders_dml folders_Dml = new Folders_dml(_connstr);
                    string destcounts = folders_Dml.GetDestCounts(srcdbname);

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
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string srcdbname = msgUtil.GetParamValue(WorkFields.SrcDBName, methparams);
                string placeholder = msgUtil.GetParamValue(WorkFields.StartID, methparams);
                string batchsize = msgUtil.GetParamValue(WorkFields.BatchSize, methparams);

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
                    Folders_dml folders_Dml = new Folders_dml(_connstr);
                    results = folders_Dml.GetSrcRecs(srcdbname, placeholder, batchsize);

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

                string foldergid = cmnUtil.GetNewGID();
                string new_row_ms = msgUtil.UnixTimeString();

                string groupgid = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
                string parentgid = msgUtil.GetParamValue(FolderFields.ParentGID, methparams);
                string foldername = msgUtil.GetParamValue(FolderFields.FolderName, methparams);
                string displayorder = msgUtil.GetParamValue(FolderFields.DisplayOrder, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (groupgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing GroupGID; ";
                }
                if (parentgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ParentGID; ";
                }
                if (foldername == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderName; ";
                }
                if (displayorder == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DisplayOrder; ";
                }

                if (reqFields)
                {
                    Folders_dml folders_Dml = new Folders_dml(_connstr);
                    string rescode = folders_Dml.Write(ReplicaAction.Insert, foldergid, userinfo.UserGID, new_row_ms, "", groupgid, parentgid, foldername, displayorder, userinfo.WriteList);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, foldergid);
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
        /// 
        /// </summary>
        public string Save(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                CmnUtil cmnUtil = new CmnUtil();
                string new_row_ms = msgUtil.UnixTimeString();

                string foldergid = msgUtil.GetParamValue(FolderFields.FolderGID, methparams);
                string groupgid = msgUtil.GetParamValue(FolderFields.GroupGID, methparams);
                string parentgid = msgUtil.GetParamValue(FolderFields.ParentGID, methparams);
                string foldername = msgUtil.GetParamValue(FolderFields.FolderName, methparams);
                string displayorder = msgUtil.GetParamValue(FolderFields.DisplayOrder, methparams);
                string edit_ms = msgUtil.GetParamValue(CommonFields.row_ms, methparams);
                
                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (foldergid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderGID; ";
                }
                if (groupgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing GroupGID; ";
                }
                if (parentgid == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing ParentGID; ";
                }
                if (foldername == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing FolderName; ";
                }
                if (displayorder == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DisplayOrder; ";
                }
                if (edit_ms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_ms; ";
                }

                if (reqFields)
                {
                    Folders_dml folders_Dml = new Folders_dml(_connstr);
                    string rescode = folders_Dml.Write(action, foldergid, userinfo.UserGID, new_row_ms, edit_ms, groupgid, parentgid, foldername, displayorder, userinfo.WriteList);

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
                        Folders_dml folders_Dml = new Folders_dml(_connstr);

                        string procstate = "";
                        foreach (DataRow dr in srcRecs.Rows)
                        {
                            string row_id = dr[CommonFields.row_id].ToString();
                            string rec_dbname = dr[CommonFields.rec_dbname].ToString();
                            string src_ms = dr[CommonFields.src_ms].ToString();
                            string rec_gid = dr[CommonFields.rec_gid].ToString();
                            string rec_state = dr[CommonFields.rec_state].ToString();

                            string GroupGID = dr[FolderFields.GroupGID].ToString();
                            string ParentGID = dr[FolderFields.ParentGID].ToString();
                            string FolderName = dr[FolderFields.FolderName].ToString();
                            string DisplayOrder = dr[FolderFields.DisplayOrder].ToString();

                            procstate = folders_Dml.Replicate(row_id, 
                                                            src_ms, 
                                                            rec_dbname, 
                                                            rec_gid, 
                                                            rec_state, 
                                                            userinfo.UserGID, 
                                                            GroupGID, 
                                                            ParentGID, 
                                                            FolderName, 
                                                            DisplayOrder, 
                                                            _connstr);

                            if (procstate == APIResult.OK || procstate == APIResult.Done)
                            {
                                newplaceholder = row_id;
                            }
                            else
                            {
                                string errmsg = "Folders replication error for row " + row_id + " : " + procstate;
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
