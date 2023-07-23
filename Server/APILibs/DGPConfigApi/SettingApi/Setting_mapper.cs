using System;
using System.Collections.Generic;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using MySqlLib.SysConfig.sys_settings;

namespace DGPConfigApi.sys_setting
{
    public class Setting_mapper
    {
        public Setting_mapper()
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
                string settingname = msgUtil.GetParamValue("SettingName", methparams);

                sys_settings_dml settings = new sys_settings_dml();
                string settingcount = settings.SettingCount(settingname);

                if (settingcount != null && settingcount != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), settingcount);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.GetCount method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SettingSearch(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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
                string settingname = msgUtil.GetParamValue("SettingName", methparams);

                sys_settings_dml settings = new sys_settings_dml();
                results = settings.SettingSearch(pagenum, pagesize, settingname);

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
                throw new Exception("An error occurred in the SettingApi.SettingSearch method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSettingByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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
                string settinggid = msgUtil.GetParamValue("SettingGID", methparams);

                sys_settings_dml settings = new sys_settings_dml();
                DataTable settbl = settings.GetSettingByID(settinggid);

                if (settbl != null && settbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string dbstr = svcUtil.TableToXml(settbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), dbstr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SettingApi.GetSettingByID method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSettingByName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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
                string settingname = msgUtil.GetParamValue("SettingName", methparams);

                sys_settings_dml settings = new sys_settings_dml();
                DataTable settbl = settings.GetSettingByName(settingname);

                if (settbl != null && settbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string dbstr = svcUtil.TableToXml(settbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), dbstr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SettingApi.GetSettingByName method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSettings(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
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
                sys_settings_dml settings = new sys_settings_dml();
                DataTable settingtbl = settings.GetSettings();

                if (settingtbl != null && settingtbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string schemastr = svcUtil.TableToXml(settingtbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), schemastr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.GetSchemas method, and has been logged.", ex);
            }

            return resultxml;
        }


        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */


        /// <summary>
        /// 
        /// </summary>
        public string NewSetting(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                SvcUtil svcUtil = new SvcUtil();
                string settinggid = svcUtil.GetNewGID();
                string settingname = msgUtil.GetParamValue("SettingName", methparams);
                string settingvalue = msgUtil.GetParamValue("SettingValue", methparams);
                string settingdescrip = msgUtil.GetParamValue("SettingDescrip", methparams);

                sys_settings_dml settings = new sys_settings_dml();
                string settingcode = settings.setting_insert(settinggid,
                                                settingname,
                                                settingvalue,
                                                settingdescrip,
                                                userinfo.UserGID);

                if (settingcode != null && settingcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), settinggid);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), settingcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SettingApi.NewSetting method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SaveSetting(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                string settinggid = msgUtil.GetParamValue("SettingGID", methparams);
                string settingname = msgUtil.GetParamValue("SettingName", methparams);
                string settingvalue = msgUtil.GetParamValue("SettingValue", methparams);
                string settingdescrip = msgUtil.GetParamValue("SettingDescrip", methparams);
                string dbname = msgUtil.GetParamValue("RowSrc", methparams);

                sys_settings_dml settings = new sys_settings_dml();
                string settingcode = settings.setting_update(action,
                                                settinggid,
                                                dbname,
                                                settingname,
                                                settingvalue,
                                                settingdescrip,
                                                userinfo.UserGID);

                if (settingcode != null && settingcode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), settingcode);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), settingcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SettingApi.SaveSetting method, and has been logged.", ex);
            }

            return resultxml;
        }
    }
}
