using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPMetricsAPI
{
    public class AutoWorkLog_switch : IMethSwitch
    {
        string _connstr;

        public AutoWorkLog_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysMetrics"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            AutoWorkLog_mapper autoWorkLog_Mapper = new AutoWorkLog_mapper(_connstr);

            switch (methodname)
            {
                case "AutoWorkLog.GetPageSize.base":
                    methXml = autoWorkLog_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "AutoWorkLog.GetByID.base":
                    methXml = autoWorkLog_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "AutoWorkLog.GetAll.base":
                    methXml = autoWorkLog_Mapper.GetAll(userinfo, methodname, methparams);
                    break;

                case "AutoWorkLog.GetCount.base":
                    methXml = autoWorkLog_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "AutoWorkLog.GetSearch.base":
                    methXml = autoWorkLog_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "AutoWorkLog.GetProcSteps.base":
                    methXml = autoWorkLog_Mapper.GetProcSteps(userinfo, methodname, methparams);
                    break;

                case "AutoWorkLog.New.base":
                    methXml = autoWorkLog_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "AutoWorkLog.Delete.base":
                    methXml = autoWorkLog_Mapper.Delete(userinfo, methodname, methparams);
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the AutoWorkLog API.");
            }

            return methXml;
        }
    }
}
