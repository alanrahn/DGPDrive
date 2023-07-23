using System;
using System.Collections.Generic;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using MySqlLib.SysConfig.sys_locations;

namespace DGPConfigApi.sys_location
{
    public class Location_mapper
    {
        public Location_mapper()
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
                string SysName = msgUtil.GetParamValue("SysName", methparams);
                string EnvName = msgUtil.GetParamValue("EnvName", methparams);
                string LocName = msgUtil.GetParamValue("LocName", methparams);

                sys_locations_dml location = new sys_locations_dml();
                string locationcount = location.LocationCount(SysName, EnvName, LocName);

                if (locationcount != null && locationcount != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), locationcount);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the location.GetCount method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LocationSearch(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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
                string SysName = msgUtil.GetParamValue("SysName", methparams);
                string EnvName = msgUtil.GetParamValue("EnvName", methparams);
                string LocName = msgUtil.GetParamValue("LocName", methparams);


                sys_locations_dml location = new sys_locations_dml();
                results = location.LocationSearch(pagenum, pagesize, SysName, EnvName, LocName);

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
                throw new Exception("An error occurred in the location.locationsearch method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetLocations(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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

                sys_locations_dml location = new sys_locations_dml();
                DataTable locationtbl = location.GetLocations();

                if (locationtbl != null && locationtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string locationstr = svcUtil.TableToXml(locationtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), locationstr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the location.Getlocations method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetLocationByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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
                string locationgid = msgUtil.GetParamValue("LocationGID", methparams);

                sys_locations_dml location = new sys_locations_dml();
                DataTable locationtbl = location.GetLocationByID(locationgid);

                if (locationtbl != null && locationtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string locationstr = svcUtil.TableToXml(locationtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), locationstr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the location.GetlocationByID method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetLocationByName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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

                string locname = msgUtil.GetParamValue("LocName", methparams);

                sys_locations_dml location = new sys_locations_dml();
                DataTable locationtbl = location.GetLocationByName(locname);

                if (locationtbl != null && locationtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string locationstr = svcUtil.TableToXml(locationtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), locationstr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the location.GetlocationByName method, and has been logged.", ex);
            }

            return resultxml;
        }


        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */


        /// <summary>
        /// 
        /// </summary>
        public string NewLocation(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                SvcUtil svcUtil = new SvcUtil();
                string locationgid = svcUtil.GetNewGID();
                string sysname = msgUtil.GetParamValue("SysName", methparams);
                string envname = msgUtil.GetParamValue("EnvName", methparams);
                string locname = msgUtil.GetParamValue("LocName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);

                sys_locations_dml location = new sys_locations_dml();
                string locationcode = location.location_insert(locationgid, sysname, envname, locname, descrip, userinfo.UserGID);

                if (locationcode != null && locationcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), locationgid);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), locationcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the location.Newlocation method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SaveLocation(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                string locationgid = msgUtil.GetParamValue("LocationGID", methparams);
                string row_src = msgUtil.GetParamValue("RowSrc", methparams);
                string sysname = msgUtil.GetParamValue("SysName", methparams);
                string envname = msgUtil.GetParamValue("EnvName", methparams);
                string locname = msgUtil.GetParamValue("LocName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);

                sys_locations_dml location = new sys_locations_dml();
                string locationcode = location.location_update(action, locationgid, row_src, sysname, envname, locname, descrip, userinfo.UserGID);

                if (locationcode != null && locationcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), locationcode);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), locationcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the location.Savelocation method, and has been logged.", ex);
            }

            return resultxml;
        }
    }
}
