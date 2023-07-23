using System;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPConfigApi.db_host
{
    public class Host_switch : IMethSwitch
    {
        public Host_switch()
        {

        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Host_mapper host = new Host_mapper();

            switch (methodname)
            {
                case "Host.GetCount.base":
                    methXml = host.GetCount(userinfo, methodname, methparams);
                    break;

                case "Host.HostSearch.base":
                    methXml = host.HostSearch(userinfo, methodname, methparams);
                    break;

                case "Host.GetHostByID.base":
                    methXml = host.GetHostByID(userinfo, methodname, methparams);
                    break;

                case "Host.GetHostByName.base":
                    methXml = host.GetHostByName(userinfo, methodname, methparams);
                    break;

                case "Host.GetHosts.base":
                    methXml = host.GetHosts(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "Host.NewHost.base":
                    methXml = host.NewHost(userinfo, methodname, methparams);
                    break;

                case "Host.SaveHost.base":
                    methXml = host.SaveHost(userinfo, methodname, methparams, "update");
                    break;

                case "Host.DeleteHost.base":
                    methXml = host.SaveHost(userinfo, methodname, methparams, "delete");
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Host.GetSrcRecs.base":
                    //methXml = ;
                    break;

                case "Host.WriteSrcRecs.base":
                    //methXml = ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the Host API.");
            }

            return methXml;
        }
    }
}
