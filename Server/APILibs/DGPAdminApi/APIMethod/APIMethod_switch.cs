using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.APIMethod
{
    public class APIMethod_switch : IMethSwitch
    {
        string _connstr;

        public APIMethod_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }


        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            APIMethod_read_mapper method_Read_Mapper;
            APIMethod_write_mapper method_Write_Mapper;

            switch (methodname)
            {
                case "APIMethod.GetPageSize.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetAPIList.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetAPIList(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetCount.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetSearch.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetByID.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetByName.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetByName(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetHistory.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetHistory(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "APIMethod.New.base":
                    method_Write_Mapper = new APIMethod_write_mapper(_connstr);
                    methXml = method_Write_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "APIMethod.Save.base":
                    method_Write_Mapper = new APIMethod_write_mapper(_connstr);
                    methXml = method_Write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "APIMethod.Delete.base":
                    method_Write_Mapper = new APIMethod_write_mapper(_connstr);
                    methXml = method_Write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                case "APIMethod.Recover.base":
                    method_Write_Mapper = new APIMethod_write_mapper(_connstr);
                    methXml = method_Write_Mapper.Recover(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "APIMethod.Duplicate.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetSrcCount.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetDestCount.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "APIMethod.GetSource.base":
                    method_Read_Mapper = new APIMethod_read_mapper(_connstr);
                    methXml = method_Read_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "APIMethod.Replicate.base":
                    method_Write_Mapper = new APIMethod_write_mapper(_connstr);
                    methXml = method_Write_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the APIMethod API.");
            }

            return methXml;
        }
    }
}
