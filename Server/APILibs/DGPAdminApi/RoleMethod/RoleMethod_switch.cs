using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.RoleMethod
{
    public class RoleMethod_switch : IMethSwitch
    {
        string _connstr;

        public RoleMethod_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            RoleMethod_mapper roleMethod_Mapper = new RoleMethod_mapper(_connstr);

            switch (methodname)
            {
                case "RoleMethod.GetUserMethods.base":
                    methXml = roleMethod_Mapper.GetUserMethods(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.GetMethodRoles.base":
                    methXml = roleMethod_Mapper.GetMethodRoles(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.GetAssigned.base":
                    methXml = roleMethod_Mapper.GetAssigned(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.GetAvailable.base":
                    methXml = roleMethod_Mapper.GetAvailable(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "RoleMethod.Assign.base":
                    methXml = roleMethod_Mapper.Assign(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.Remove.base":
                    methXml = roleMethod_Mapper.Remove(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "RoleMethod.Duplicate.base":
                    methXml = roleMethod_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.GetSrcCount.base":
                    methXml = roleMethod_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.GetDestCount.base":
                    methXml = roleMethod_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.GetSource.base":
                    methXml = roleMethod_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "RoleMethod.Replicate.base":
                    methXml = roleMethod_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the RoleMethod API.");
            }

            return methXml;
        }

    }
}
