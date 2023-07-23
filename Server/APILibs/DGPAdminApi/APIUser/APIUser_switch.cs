using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.User
{
    public class APIUser_switch
    {
        string _connstr;

        public APIUser_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            APIUser_read_mapper user_read_Mapper;
            APIUser_write_mapper user_write_Mapper;

            switch (methodname)
            {
                case "APIUser.GetPageSize.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetCount.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetSearch.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetByID.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetByName.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetByName(userinfo, methodname, methparams);
                    break;

                case "APIUser.CheckName.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.CheckName(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetHistory.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetHistory(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "APIUser.New.base":
                    user_write_Mapper = new APIUser_write_mapper(_connstr);
                    methXml = user_write_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "APIUser.Save.base":
                    user_write_Mapper = new APIUser_write_mapper(_connstr);
                    methXml = user_write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "APIUser.Delete.base":
                    user_write_Mapper = new APIUser_write_mapper(_connstr);
                    methXml = user_write_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                case "APIUser.Recover.base":
                    user_write_Mapper = new APIUser_write_mapper(_connstr);
                    methXml = user_write_Mapper.Recover(userinfo, methodname, methparams);
                    break;

                case "APIUser.ChangePassword.base":
                    user_write_Mapper = new APIUser_write_mapper(_connstr);
                    methXml = user_write_Mapper.ChangePassword(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "APIUser.Duplicate.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetSrcCount.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetDestCount.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "APIUser.GetSource.base":
                    user_read_Mapper = new APIUser_read_mapper(_connstr);
                    methXml = user_read_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "APIUser.Replicate.base":
                    user_write_Mapper = new APIUser_write_mapper(_connstr);
                    methXml = user_write_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the APIUser API.");
            }

            return methXml;
        }
    }
}
