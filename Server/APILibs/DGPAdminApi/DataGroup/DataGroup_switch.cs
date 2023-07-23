using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.DataGroup
{
    public class DataGroup_switch : IMethSwitch
    {
        string _connstr;

        public DataGroup_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            DataGroup_read_mapper group_read_Mapper;
            DataGroup_write_mapper group_write_Mapper;

            switch (methodname)
            {
                case "DataGroup.GetPageSize.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetCount.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetSearch.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetByID.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetByName.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetByName(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetHistory.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetHistory(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "DataGroup.New.base":
                    group_write_Mapper = new DataGroup_write_mapper(_connstr);
                    methXml = group_write_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "DataGroup.Save.base":
                    group_write_Mapper = new DataGroup_write_mapper(_connstr);
                    methXml = group_write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "DataGroup.Delete.base":
                    group_write_Mapper = new DataGroup_write_mapper(_connstr);
                    methXml = group_write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                case "DataGroup.Recover.base":
                    group_write_Mapper = new DataGroup_write_mapper(_connstr);
                    methXml = group_write_Mapper.Recover(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//
                
                case "DataGroup.Duplicate.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetSrcCount.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetDestCount.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "DataGroup.GetSource.base":
                    group_read_Mapper = new DataGroup_read_mapper(_connstr);
                    methXml = group_read_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "DataGroup.Replicate.base":
                    group_write_Mapper = new DataGroup_write_mapper(_connstr);
                    methXml = group_write_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the DataGroup API.");
            }

            return methXml;
        }

    }
}
