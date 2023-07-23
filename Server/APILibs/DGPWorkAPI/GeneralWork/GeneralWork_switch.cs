using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPWorkAPI
{
    public class GeneralWork_switch : IMethSwitch
    {
        string _connstr;

        public GeneralWork_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysWork"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            GeneralWork_mapper genwork_Mapper = new GeneralWork_mapper(_connstr);

            switch (methodname)
            {
                case "GeneralWork.GetPageSize.base":
                    methXml = genwork_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.GetCount.base":
                    methXml = genwork_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.GetSearch.base":
                    methXml = genwork_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.GetByID.base":
                    methXml = genwork_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.GetAll.base":
                    methXml = genwork_Mapper.GetAll(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.QueueScan.base":
                    methXml = genwork_Mapper.QueueScan(userinfo, methodname, methparams);
                    break;

                /* --------------------------------------------------------------------------------------------- */

                case "GeneralWork.ClaimRecords.base":
                    methXml = genwork_Mapper.ClaimWorkRecords(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.ReleaseRecord.base":
                    methXml = genwork_Mapper.ReleaseWorkRecord(userinfo, methodname, methparams);
                    break;

                /* --------------------------------------------------------------------------------------------- */

                case "GeneralWork.New.base":
                    methXml = genwork_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.Clone.base":
                    methXml = genwork_Mapper.Clone(userinfo, methodname, methparams);
                    break;

                case "GeneralWork.Save.base":
                    methXml = genwork_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "GeneralWork.Delete.base":
                    methXml = genwork_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the GeneralWork API.");
            }

            return methXml;
        }

    }
}
