using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPFileStoreAPI.Folders
{
    public class Folders_switch
    {
        string _connstr;

        public Folders_switch()
        {
            _connstr = ConfigurationManager.AppSettings["FileStore"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Folders_mapper folders_Mapper = new Folders_mapper(_connstr);

            switch (methodname)
            {
                case "Folder.GetUserSubFolders.base":
                    methXml = folders_Mapper.GetUserSubFolders(userinfo, methodname, methparams);
                    break;

                case "Folder.GetByID.base":
                    methXml = folders_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "Folder.AddSubFolder.base":
                    methXml = folders_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "Folder.Save.base":
                    methXml = folders_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "Folder.Delete.base":
                    methXml = folders_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Folder.Duplicate.base":
                    methXml = folders_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "Folder.GetSrcCount.base":
                    methXml = folders_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "Folder.GetDestCount.base":
                    methXml = folders_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "Folder.GetSource.base":
                    methXml = folders_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "Folder.Replicate.base":
                    methXml = folders_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the Folders API.");
            }

            return methXml;
        }
    }
}
