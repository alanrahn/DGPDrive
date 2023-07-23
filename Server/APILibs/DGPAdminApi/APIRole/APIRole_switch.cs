using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.Role
{
    public class APIRole_switch : IMethSwitch
    {
        string _connstr;

        public APIRole_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            APIRole_read_mapper role_read_Mapper;
            APIRole_write_mapper role_write_Mapper;

            switch (methodname)
            {
                case "APIRole.GetPageSize.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetCount.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetSearch.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetByID.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetByName.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetByName(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetHistory.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetHistory(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "APIRole.New.base":
                    role_write_Mapper = new APIRole_write_mapper(_connstr);
                    methXml = role_write_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "APIRole.Save.base":
                    role_write_Mapper = new APIRole_write_mapper(_connstr);
                    methXml = role_write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "APIRole.Delete.base":
                    role_write_Mapper = new APIRole_write_mapper(_connstr);
                    methXml = role_write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                case "APIRole.Recover.base":
                    role_write_Mapper = new APIRole_write_mapper(_connstr);
                    methXml = role_write_Mapper.Recover(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "APIRole.Duplicate.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetSrcCount.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetDestCount.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "APIRole.GetSource.base":
                    role_read_Mapper = new APIRole_read_mapper(_connstr);
                    methXml = role_read_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "APIRole.Replicate.base":
                    role_write_Mapper = new APIRole_write_mapper(_connstr);
                    methXml = role_write_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the APIRole API.");
            }

            return methXml;
        }

    }
}
