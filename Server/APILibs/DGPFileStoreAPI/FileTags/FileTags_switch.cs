using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPFileStoreAPI.FileTags
{
    public class FileTags_switch : IMethSwitch
    {
        string _connstr;

        public FileTags_switch()
        {
            _connstr = ConfigurationManager.AppSettings["FileStore"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            FileTags_mapper fileTags_Mapper = new FileTags_mapper(_connstr);

            switch (methodname)
            {
                case "FileTag.GetAssigned.base":
                    methXml = fileTags_Mapper.GetAssigned(userinfo, methodname, methparams);
                    break;

                case "FileTag.GetAvailable.base":
                    methXml = fileTags_Mapper.GetAvailable(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "FileTag.Assign.base":
                    methXml = fileTags_Mapper.Assign(userinfo, methodname, methparams);
                    break;

                case "FileTag.Remove.base":
                    methXml = fileTags_Mapper.Remove(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "FileTag.Duplicate.base":
                    methXml = fileTags_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "FileTag.GetSrcCount.base":
                    methXml = fileTags_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "FileTag.GetDestCount.base":
                    methXml = fileTags_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "FileTag.GetSource.base":
                    methXml = fileTags_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "FileTag.Replicate.base":
                    methXml = fileTags_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the FileTags API.");
            }

            return methXml;
        }

    }
}
