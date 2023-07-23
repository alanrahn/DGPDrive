using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPMetricsAPI.DGPMetrics
{
    public class DGPMetrics_switch : IMethSwitch
    {
        string _connstr;

        public DGPMetrics_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysMetrics"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            DGPMetrics_mapper DGPMetrics_Mapper = new DGPMetrics_mapper(_connstr);

            switch (methodname)
            {
                case "DGPMetrics.GetPageSize.base":
                    methXml = DGPMetrics_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "DGPMetrics.GetByID.base":
                    methXml = DGPMetrics_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "DGPMetrics.GetCount.base":
                    methXml = DGPMetrics_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "DGPMetrics.GetSearch.base":
                    methXml = DGPMetrics_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "DGPMetrics.GetAll.base":
                    methXml = DGPMetrics_Mapper.GetAll(userinfo, methodname, methparams);
                    break;

                case "DGPMetrics.New.base":
                    methXml = DGPMetrics_Mapper.AddDGPMetric(userinfo, methodname, methparams);
                    break;

                case "DGPMetrics.Delete.base":
                    methXml = DGPMetrics_Mapper.Delete(userinfo, methodname, methparams);
                    break;

                case "DGPMetrics.Server.base":
                    methXml = DGPMetrics_Mapper.AddServerMetric(userinfo, methodname, methparams);
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the DGPMetrics API.");
            }

            return methXml;
        }
    }
}
