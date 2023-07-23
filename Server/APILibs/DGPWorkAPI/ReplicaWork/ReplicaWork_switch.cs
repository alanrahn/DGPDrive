using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPWorkAPI
{
    public class ReplicaWork_switch : IMethSwitch
    {
        string _connstr;

        public ReplicaWork_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysWork"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            ReplicaWork_mapper repwork_Mapper = new ReplicaWork_mapper(_connstr);

            switch (methodname)
            {
                case "ReplicaWork.GetPageSize.base":
                    methXml = repwork_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.GetCount.base":
                    methXml = repwork_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.GetSearch.base":
                    methXml = repwork_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.GetByID.base":
                    methXml = repwork_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.GetAll.base":
                    methXml = repwork_Mapper.GetAll(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.QueueScan.base":
                    methXml = repwork_Mapper.QueueScan(userinfo, methodname, methparams);
                    break;

                /* --------------------------------------------------------------------------------------------- */

                case "ReplicaWork.ClaimRecords.base":
                    methXml = repwork_Mapper.ClaimWorkRecords(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.ReleaseRecord.base":
                    methXml = repwork_Mapper.ReleaseWorkRecord(userinfo, methodname, methparams);
                    break;

                /* --------------------------------------------------------------------------------------------- */

                case "ReplicaWork.New.base":
                    methXml = repwork_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.Clone.base":
                    methXml = repwork_Mapper.Clone(userinfo, methodname, methparams);
                    break;

                case "ReplicaWork.Save.base":
                    methXml = repwork_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "ReplicaWork.Delete.base":
                    methXml = repwork_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the ReplicaWork API.");
            }

            return methXml;
        }

    }
}
