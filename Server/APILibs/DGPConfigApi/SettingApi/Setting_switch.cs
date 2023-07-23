using System;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPConfigApi.sys_setting
{
    public class Setting_switch : IMethSwitch
    {
        public Setting_switch()
        {

        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Setting_mapper setting = new Setting_mapper();

            switch (methodname)
            {
                case "Setting.GetCount.base":
                    methXml = setting.GetCount(userinfo, methodname, methparams);
                    break;

                case "Setting.SettingSearch.base":
                    methXml = setting.SettingSearch(userinfo, methodname, methparams);
                    break;

                case "Setting.GetSettingByID.base":
                    methXml = setting.GetSettingByID(userinfo, methodname, methparams);
                    break;

                case "Setting.GetSchemaByName.base":
                    methXml = setting.GetSettingByName(userinfo, methodname, methparams);
                    break;

                case "Setting.GetSettings.base":
                    methXml = setting.GetSettings(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "Setting.NewSetting.base":
                    methXml = setting.NewSetting(userinfo, methodname, methparams);
                    break;

                case "Setting.SaveSetting.base":
                    methXml = setting.SaveSetting(userinfo, methodname, methparams, "update");
                    break;

                case "Setting.DeleteSetting.base":
                    methXml = setting.SaveSetting(userinfo, methodname, methparams, "delete");
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Setting.GetSrcRecs.base":
                    //methXml = ;
                    break;

                case "Setting.WriteSrcRecs.base":
                    //methXml = ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the Settings API.");
            }

            return methXml;
        }
    }
}
