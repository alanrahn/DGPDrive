using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using MySqlLib.SysConfig.db_info;

namespace DGPConfigApi.db_info
{
    public class DBInfo_mapper
    {
        public DBInfo_mapper()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                // optional search parameter
                string LocName = msgUtil.GetParamValue("LocName", methparams);
                string hostname = msgUtil.GetParamValue("HostName", methparams);
                string schemaname = msgUtil.GetParamValue("SchemaName", methparams);
                string dbname = msgUtil.GetParamValue("DBName", methparams);

                db_info_dml db_info = new db_info_dml();

                string dbcount = db_info.DBInfoCount(LocName, hostname, schemaname, dbname);

                if (dbcount != null && dbcount != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), dbcount);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the DBInfo.GetCount method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DBSearch(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }

                // pagination values
                string pagenum = msgUtil.GetParamValue("PageNum", methparams);
                string pagesize = msgUtil.GetParamValue("PageSize", methparams);

                // search values (optional)
                string LocName = msgUtil.GetParamValue("LocName", methparams);
                string hostname = msgUtil.GetParamValue("HostName", methparams);
                string schemaname = msgUtil.GetParamValue("SchemaName", methparams);
                string dbname = msgUtil.GetParamValue("DBName", methparams);

                db_info_dml db_info = new db_info_dml();
                results = db_info.DBInfoSearch(pagenum, pagesize, LocName, hostname, schemaname, dbname);

                if (results.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string tblxml = svcUtil.TableToXml(results, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), tblxml);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the DBInfo.DBSearch method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetDBInfo(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }

                db_info_dml db_info = new db_info_dml();
                DataTable dbtbl = db_info.GetDBInfo();

                if (dbtbl != null && dbtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string hoststr = svcUtil.TableToXml(dbtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), hoststr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the DBInfo.GetDBInfo method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetDBInfoByLoc(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }

                db_info_dml db_info = new db_info_dml();
                DataTable dbtbl = db_info.GetDBInfoByLoc(ConfigurationManager.AppSettings["LocName"].ToString());

                if (dbtbl != null && dbtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string hoststr = svcUtil.TableToXml(dbtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), hoststr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the DBInfo.GetGetDBInfoByLoc.base method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetDBinfoByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }
                string dbinfogid = msgUtil.GetParamValue("DBInfoGID", methparams);

                db_info_dml db_info = new db_info_dml();
                DataTable dbtbl = db_info.GetDBInfoByID(dbinfogid);

                if (dbtbl != null && dbtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string dbstr = svcUtil.TableToXml(dbtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), dbstr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the DBInfo.GetDBByID method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetDBInfoByName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }
                string dbname = msgUtil.GetParamValue("DBName", methparams);

                db_info_dml db_info = new db_info_dml();
                DataTable dbtbl = db_info.GetDBInfoByName(dbname);

                if (dbtbl != null && dbtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string dbstr = svcUtil.TableToXml(dbtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), dbstr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Host.GetDBByName method, and has been logged.", ex);
            }

            return resultxml;
        }



        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */


        /// <summary>
        /// 
        /// </summary>
        public string NewDBInfo(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                SvcUtil svcUtil = new SvcUtil();
                string dbgid = svcUtil.GetNewGID();
                string LocName = msgUtil.GetParamValue("LocName", methparams);
                string hostname = msgUtil.GetParamValue("HostName", methparams);
                string schemaname = msgUtil.GetParamValue("SchemaName", methparams);
                string shardname = msgUtil.GetParamValue("ShardName", methparams);
                string shardaction = msgUtil.GetParamValue("ShardAction", methparams);
                string dbname = msgUtil.GetParamValue("DBName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);
                string dbconnst = msgUtil.GetParamValue("DBConnStr", methparams);
                string dbversion = msgUtil.GetParamValue("DBVersion", methparams);

                db_info_dml db_info = new db_info_dml();
                string dbcode = db_info.dbinfo_insert(dbgid,
                                                LocName,
                                                hostname,
                                                schemaname,
                                                shardname,
                                                shardaction,
                                                dbname,
                                                descrip,
                                                dbconnst,
                                                dbversion,
                                                userinfo.UserGID);

                if (dbcode != null && dbcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), dbgid);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), dbcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the DBInfo.NewDB method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SaveDBInfo(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                string dbgid = msgUtil.GetParamValue("DBInfoGID", methparams);
                string LocName = msgUtil.GetParamValue("LocName", methparams);
                string hostname = msgUtil.GetParamValue("HostName", methparams);
                string schemaname = msgUtil.GetParamValue("SchemaName", methparams);
                string shardname = msgUtil.GetParamValue("ShardName", methparams);
                string shardaction = msgUtil.GetParamValue("ShardAction", methparams);
                string dbname = msgUtil.GetParamValue("DBName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);
                string dbconnst = msgUtil.GetParamValue("DBConnStr", methparams);
                string dbversion = msgUtil.GetParamValue("DBVersion", methparams);

                db_info_dml db_info = new db_info_dml();
                string dbcode = db_info.dbinfo_update(action,
                                                dbgid,
                                                LocName,
                                                hostname,
                                                schemaname,
                                                shardname,
                                                shardaction,
                                                dbname,
                                                descrip,
                                                dbconnst,
                                                dbversion,
                                                userinfo.UserGID);

                if (dbcode != null && dbcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), dbcode);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), dbcode);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the DBInfo.SaveDB method, and has been logged.", ex);
            }

            return resultxml;
        }

    }
}
