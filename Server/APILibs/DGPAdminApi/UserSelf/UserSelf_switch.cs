using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPAdminAPI.Self
{
    public class UserSelf_switch
    {
        string _connstr;

        public UserSelf_switch()
        {
            _connstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            UserSelf_mapper self_Mapper = new UserSelf_mapper(_connstr);

            switch (methodname)
            {
                case "UserSelf.Login.base":
                    methXml = self_Mapper.Login(userinfo, methodname, methparams);
                    break;

                case "UserSelf.GetRoles.base":
                    methXml = self_Mapper.GetRoles(userinfo, methodname, methparams);
                    break;

                case "UserSelf.GetGroups.base":
                    methXml = self_Mapper.GetGroups(userinfo, methodname, methparams);
                    break;

                case "UserSelf.GetUserGroupList.base":
                    methXml = self_Mapper.GetUserGroupList(userinfo, methodname, methparams);
                    break;

                case "UserSelf.GetInfo.base":
                    methXml = self_Mapper.GetInfo(userinfo, methodname, methparams);
                    break;

                case "UserSelf.Save.base":
                    methXml = self_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "UserSelf.ChangePassword.base":
                    methXml = self_Mapper.ChangePassword(userinfo, methodname, methparams);
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the UserSelf API.");
            }

            return methXml;
        }
    }
}
