using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using FileStoreDB.Tags;
using SysMetricsDB;

namespace DGPFileStoreAPI.Tags
{
    public class Tags_mapper
    {
        string _connstr;
        string _appname;
        string _pagesize = "50";

        public Tags_mapper(string connStr)
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
        public string GetByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string TagGID = msgUtil.GetParamValue(TagFields.TagGID, methparams);

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
                if (TagGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TagGID; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    results = tags_Dml.GetByID(TagGID);

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
        public string GetByName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string TagName = msgUtil.GetParamValue(TagFields.TagName, methparams);

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
                if (TagName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TagName; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    results = tags_Dml.GetByName(TagName);

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
        public string FilterByName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string TagName = msgUtil.GetParamValue(TagFields.TagName, methparams);

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
                //if (TagName == "")
                //{
                //    reqFields = false;
                //    reqFieldNames += "Missing TagName; ";
                //}

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    results = tags_Dml.FilterByName(TagName);

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
        public string GetHistory(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string TagGID = msgUtil.GetParamValue(TagFields.TagGID, methparams);

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
                if (TagGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TagGID; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    results = tags_Dml.GetHistory(TagGID);

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
        public string GetCount(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string recstate = msgUtil.GetParamValue(CommonFields.rec_state, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (recstate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_state; ";
                }

                // optional search parameter
                string TagName = msgUtil.GetParamValue(TagFields.TagName, methparams);

                if (reqFields)
                {
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    string groupcount = tags_Dml.GetCount(TagName, recstate);

                    if (groupcount != null && groupcount != "")
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, groupcount);
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
        public string GetSearch(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                // pagination values
                string SchemaFlag = msgUtil.GetParamValue(CommonFields.SchemaFlag, methparams);
                string pagenum = msgUtil.GetParamValue(CommonFields.PageNum, methparams);
                string sortorder = msgUtil.GetParamValue(CommonFields.SortOrder, methparams);
                string recstate = msgUtil.GetParamValue(CommonFields.rec_state, methparams);

                // search values (optional)
                string TagName = msgUtil.GetParamValue(TagFields.TagName, methparams);

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
                if (pagenum == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing PageNum; ";
                }
                else
                {
                    var isNumeric = int.TryParse(pagenum, out int n);
                    if (!isNumeric)
                    {
                        reqFields = false;
                        reqFieldNames += "PageNum must be an int; ";
                    }
                }
                if (sortorder != "ASC" && sortorder != "DESC")
                {
                    reqFields = false;
                    reqFieldNames += "Incorrect SortOrder; ";
                }
                if (recstate == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing rec_state; ";
                }

                if (reqFields)
                {
                    bool schemaflag = Convert.ToBoolean(SchemaFlag);
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    results = tags_Dml.GetSearch(pagenum, _pagesize, recstate, sortorder, TagName);

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
                Tags_dml tags_Dml = new Tags_dml(_connstr);
                bool duplicates = tags_Dml.DupeCheck(userinfo.UserName, _appname);

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

                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    string srccounts = tags_Dml.GetSrcCounts(srcdbname, endid);

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

                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    string destcounts = tags_Dml.GetDestCounts(srcdbname);

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
                string batchsize = msgUtil.GetParamValue(WorkFields.BatchSize , methparams);

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
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    results = tags_Dml.GetSrcRecs(srcdbname, placeholder, batchsize);

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
                string TagGID = cmnUtil.GetNewGID();
                string new_row_ms = msgUtil.UnixTimeString();

                string TagName = msgUtil.GetParamValue(TagFields.TagName, methparams);
                string TagDescrip = msgUtil.GetParamValue(TagFields.TagDescrip, methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (TagName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TagName; ";
                }

                if (reqFields)
                {
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    string rescode = tags_Dml.Write(ReplicaAction.Insert, TagGID,  new_row_ms, "", userinfo.UserGID, TagName, TagDescrip);

                    if (rescode != null && rescode == APIResult.OK)
                    {
                        resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, TagGID);
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
                string TagGID = msgUtil.GetParamValue(TagFields.TagGID, methparams);
                string TagName = msgUtil.GetParamValue(TagFields.TagName, methparams);
                string TagDescrip = msgUtil.GetParamValue(TagFields.TagDescrip, methparams);
                string edit_ms = msgUtil.GetParamValue(CommonFields.row_ms, methparams);
                string new_row_ms = msgUtil.UnixTimeString();

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (TagGID == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TagGID; ";
                }
                if (TagName == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TagName; ";
                }
                if (TagDescrip == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing TagDescrip; ";
                }
                if (edit_ms == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing row_ms; ";
                }

                if (reqFields)
                {
                    Tags_dml tags_Dml = new Tags_dml(_connstr);
                    string rescode = tags_Dml.Write(action, TagGID, new_row_ms, edit_ms, userinfo.UserGID, TagName, TagDescrip);

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
                    TagProc tagProc = new TagProc(_connstr);
                    string rescode = tagProc.RecoverTag(action, recgid, rowid, new_row_ms);

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
                        Tags_dml tags_Dml = new Tags_dml(_connstr);

                        string procstate = "";
                        foreach (DataRow dr in srcRecs.Rows)
                        {
                            string row_id = dr[CommonFields.row_id].ToString();
                            string rec_dbname = dr[CommonFields.rec_dbname].ToString();
                            string src_ms = dr[CommonFields.src_ms].ToString();
                            string rec_gid = dr[CommonFields.rec_gid].ToString();
                            string rec_state = dr[CommonFields.rec_state].ToString();

                            string TagName = dr[TagFields.TagName].ToString();
                            string TagDescrip = dr[TagFields.TagDescrip].ToString();

                            procstate = tags_Dml.Replicate(row_id, src_ms, rec_dbname, rec_gid, rec_state, userinfo.UserGID, TagName, TagDescrip, _connstr);

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
