using System;
using System.Collections.Generic;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using MySqlLib.SysConfig.db_hosts;

namespace DGPConfigApi.db_host
{
    public class Host_mapper
    {
        public Host_mapper()
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

                db_hosts_dml host = new db_hosts_dml();
                string hostcount = host.HostCount(LocName, hostname);

                if (hostcount != null && hostcount != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), hostcount);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Host.GetCount method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string HostSearch(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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

                db_hosts_dml host = new db_hosts_dml();
                results = host.HostSearch(pagenum, pagesize, LocName, hostname);

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
                throw new Exception("An error occurred in the Host.HostSearch method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetHosts(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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

                string LocName = msgUtil.GetParamValue("LocName", methparams);

                db_hosts_dml host = new db_hosts_dml();
                DataTable hosttbl = host.GetHosts(LocName);

                if (hosttbl != null && hosttbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string hoststr = svcUtil.TableToXml(hosttbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), hoststr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Host.GetHosts method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetHostByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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

                string hostgid = msgUtil.GetParamValue("HostGID", methparams);

                db_hosts_dml host = new db_hosts_dml();
                DataTable hosttbl = host.GetHostByID(hostgid);

                if (hosttbl != null && hosttbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string hoststr = svcUtil.TableToXml(hosttbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), hoststr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Host.GetHostByID method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetHostByName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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

                string LocName = msgUtil.GetParamValue("LocName", methparams);
                string hostname = msgUtil.GetParamValue("HostName", methparams);

                db_hosts_dml host = new db_hosts_dml();
                DataTable hosttbl = host.GetHostByName(LocName, hostname);

                if (hosttbl != null && hosttbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string hoststr = svcUtil.TableToXml(hosttbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), hoststr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Host.GetHostByName method, and has been logged.", ex);
            }

            return resultxml;
        }


        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public string NewHost(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();
                SvcUtil svcUtil = new SvcUtil();

                string hostgid = svcUtil.GetNewGID();
                string LocName = msgUtil.GetParamValue("LocName", methparams);
                string hostname = msgUtil.GetParamValue("HostName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);
                string hostip = msgUtil.GetParamValue("HostIP", methparams);
                string hostport = msgUtil.GetParamValue("HostPort", methparams);
                string hoststatus = msgUtil.GetParamValue("HostStatus", methparams);
                string hoststate = msgUtil.GetParamValue("HostState", methparams);

                db_hosts_dml host = new db_hosts_dml();
                string hostcode = host.host_insert(hostgid, LocName, hostname, descrip, hostip, hostport, hoststatus, hoststate, userinfo.UserGID);

                if (hostcode != null && hostcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), hostgid);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), hostcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Host.NewHost method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SaveHost(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                string hostgid = msgUtil.GetParamValue("HostGID", methparams);
                string row_src = msgUtil.GetParamValue("RowSrc", methparams);
                string LocName = msgUtil.GetParamValue("LocName", methparams);
                string hostname = msgUtil.GetParamValue("HostName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);
                string hostip = msgUtil.GetParamValue("HostIP", methparams);
                string hostport = msgUtil.GetParamValue("HostPort", methparams);
                string hoststatus = msgUtil.GetParamValue("HostStatus", methparams);
                string hoststate = msgUtil.GetParamValue("HostState", methparams);

                db_hosts_dml host = new db_hosts_dml();
                string hostcode = host.host_update(action, hostgid, row_src, LocName, hostname, descrip, hostip, hostport, hoststatus, hoststate, userinfo.UserGID);

                if (hostcode != null && hostcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), hostcode);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), hostcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Host.SaveHost method, and has been logged.", ex);
            }

            return resultxml;
        }
    }
}
