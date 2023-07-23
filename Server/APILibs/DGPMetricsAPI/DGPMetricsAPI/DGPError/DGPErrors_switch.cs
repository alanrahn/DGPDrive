using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPMetricsAPI.DGPErrors
{
    public class DGPErrors_switch : IMethSwitch
    {
        string _connstr;

        public DGPErrors_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysMetrics"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            DGPErrors_mapper dgpErrors_Mapper = new DGPErrors_mapper(_connstr);

            switch (methodname)
            {
                case "DGPError.GetPageSize.base":
                    methXml = dgpErrors_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "DGPError.GetByID.base":
                    methXml = dgpErrors_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "DGPError.GetCount.base":
                    methXml = dgpErrors_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "DGPError.GetSearch.base":
                    methXml = dgpErrors_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "DGPError.GetErrData.base":
                    methXml = dgpErrors_Mapper.GetErrData(userinfo, methodname, methparams);
                    break;

                case "DGPError.GetAll.base":
                    methXml = dgpErrors_Mapper.GetAll(userinfo, methodname, methparams);
                    break;

                case "DGPError.New.base":
                    methXml = dgpErrors_Mapper.AddDGPError(userinfo, methodname, methparams);
                    break;

                case "DGPError.Delete.base":
                    methXml = dgpErrors_Mapper.Delete(userinfo, methodname, methparams);
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the DGPError API.");
            }

            return methXml;
        }
    }
}
