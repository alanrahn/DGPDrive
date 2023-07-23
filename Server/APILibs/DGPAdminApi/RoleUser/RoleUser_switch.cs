using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.RoleUser
{
    public class RoleUser_switch : IMethSwitch
    {
        string _connstr;

        public RoleUser_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            RoleUser_mapper roleUser_Mapper = new RoleUser_mapper(_connstr);

            switch (methodname)
            {
                case "RoleUser.GetAssigned.base":
                    methXml = roleUser_Mapper.GetAssigned(userinfo, methodname, methparams);
                    break;

                case "RoleUser.GetAvailable.base":
                    methXml = roleUser_Mapper.GetAvailable(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "RoleUser.Assign.base":
                    methXml = roleUser_Mapper.Assign(userinfo, methodname, methparams);
                    break;

                case "RoleUser.Remove.base":
                    methXml = roleUser_Mapper.Remove(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "RoleUser.Duplicate.base":
                    methXml = roleUser_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "RoleUser.GetSrcCount.base":
                    methXml = roleUser_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "RoleUser.GetDestCount.base":
                    methXml = roleUser_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "RoleUser.GetSource.base":
                    methXml = roleUser_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "RoleUser.Replicate.base":
                    methXml = roleUser_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the RoleUser API.");
            }

            return methXml;
        }
    }
}
