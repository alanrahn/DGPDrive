using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.GroupUser
{
    public class GroupUser_switch : IMethSwitch
    {
        string _connstr;

        public GroupUser_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            GroupUser_mapper groupUser_Mapper = new GroupUser_mapper(_connstr);

            switch (methodname)
            {
                case "GroupUser.GetAssigned.base":
                    methXml = groupUser_Mapper.GetAssigned(userinfo, methodname, methparams);
                    break;

                case "GroupUser.GetAvailable.base":
                    methXml = groupUser_Mapper.GetAvailable(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "GroupUser.Assign.base":
                    methXml = groupUser_Mapper.Assign(userinfo, methodname, methparams);
                    break;

                case "GroupUser.Remove.base":
                    methXml = groupUser_Mapper.Remove(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "GroupUser.Duplicate.base":
                    methXml = groupUser_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "GroupUser.GetSrcCount.base":
                    methXml = groupUser_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "GroupUser.GetDestCount.base":
                    methXml = groupUser_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "GroupUser.GetSource.base":
                    methXml = groupUser_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "GroupUser.Replicate.base":
                    methXml = groupUser_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the GroupUser API.");
            }

            return methXml;
        }

    }
}
