using System;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;
using DGPFileStoreAPI.Folders;
using DGPFileStoreAPI.Files;
using DGPFileStoreAPI.Favorites;
using DGPFileStoreAPI.Tags;
using DGPFileStoreAPI.FileTags;
using DGPFileStoreAPI.FileShard;
using DGPAdminAPI.APIMethod;
using DGPAdminAPI.DataGroup;
using DGPAdminAPI.GroupUser;
using DGPAdminAPI.Role;
using DGPAdminAPI.RoleMethod;
using DGPAdminAPI.RoleUser;
using DGPAdminAPI.Self;
using DGPAdminAPI.User;
using DGPTestAPI.Test;
using DGPWorkAPI;
using DGPMetricsAPI.TestResults;
using DGPMetricsAPI.DGPMetrics;
using DGPMetricsAPI.DGPErrors;
using DGPMetricsAPI;

namespace DGPWebSvc
{
    public class DGPAPISwitch : IApiSwitch
    {
        public DGPAPISwitch()
        {

        }

        /// <summary>
        /// maps the API name to the appropriate API class.
        /// </summary>
        public string CallApi(UserInfo userinfo, MethInfo methinfo, Dictionary<string, string> methparams)
        {
            string respXml = "";
            switch (methinfo.ApiName)
            {
                // DGP FileStore API
                // **********************************************************************
                case "Folder":
                    Folders_switch folders = new Folders_switch();
                    respXml = folders.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "File":
                    Files_switch files = new Files_switch();
                    respXml = files.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "Favorite":
                    Favorites_switch favorites = new Favorites_switch();
                    respXml = favorites.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "FileTag":
                    FileTags_switch filetags = new FileTags_switch();
                    respXml = filetags.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "Tag":
                    Tags_switch tags = new Tags_switch();
                    respXml = tags.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "FileShard":
                    FileShard_switch shard = new FileShard_switch();
                    respXml = shard.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                // DGPAdmin API
                // **********************************************************************
                case "UserSelf":
                    UserSelf_switch self = new UserSelf_switch();
                    respXml = self.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "APIUser":
                    APIUser_switch user = new APIUser_switch();
                    respXml = user.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "APIMethod":
                    APIMethod_switch method = new APIMethod_switch();
                    respXml = method.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "APIRole":
                    APIRole_switch role = new APIRole_switch();
                    respXml = role.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "RoleMethod":
                    RoleMethod_switch rolemethod = new RoleMethod_switch();
                    respXml = rolemethod.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "RoleUser":
                    RoleUser_switch roleuser = new RoleUser_switch();
                    respXml = roleuser.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "DataGroup":
                    DataGroup_switch group = new DataGroup_switch();
                    respXml = group.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "GroupUser":
                    GroupUser_switch read = new GroupUser_switch();
                    respXml = read.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;


                // DGPTest API
                // **********************************************************************
                case "Test":
                    Test_switch test = new Test_switch();
                    respXml = test.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;


                // DGPWork API
                // **********************************************************************
                case "ReplicaWork":
                    ReplicaWork_switch replicaWork = new ReplicaWork_switch();
                    respXml = replicaWork.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "GeneralWork":
                    GeneralWork_switch generalWork = new GeneralWork_switch();
                    respXml = generalWork.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;


                // DGPSysMetrics API
                // **********************************************************************
                case "TestResult":
                    TestResults_switch testresult = new TestResults_switch();
                    respXml = testresult.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "DGPMetrics":
                    DGPMetrics_switch dgpmetrics = new DGPMetrics_switch();
                    respXml = dgpmetrics.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "DGPError":
                    DGPErrors_switch dgperrors = new DGPErrors_switch();
                    respXml = dgperrors.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;

                case "AutoWorkLog":
                    AutoWorkLog_switch autoworklog = new AutoWorkLog_switch();
                    respXml = autoworklog.CallMethod(userinfo, methinfo.FullName, methparams);
                    break;


                // **********************************************************************
                default:
                    throw new Exception("Error: API [ " + methinfo.ApiName + " ] was not found in the DGP API switch.");
            }

            return respXml;
        }

    }
}