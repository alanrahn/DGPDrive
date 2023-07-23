using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPFileStoreAPI.Tags
{
    public class Tags_switch : IMethSwitch
    {
        string _connstr;

        public Tags_switch()
        {
            _connstr = ConfigurationManager.AppSettings["FileStore"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Tags_mapper tags_Mapper = new Tags_mapper(_connstr);

            switch (methodname)
            {
                case "Tag.GetPageSize.base":
                    methXml = tags_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "Tag.GetByID.base":
                    methXml = tags_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "Tag.GetByName.base":
                    methXml = tags_Mapper.GetByName(userinfo, methodname, methparams);
                    break;

                case "Tag.FilterByName.base":
                    methXml = tags_Mapper.FilterByName(userinfo, methodname, methparams);
                    break;

                case "Tag.GetCount.base":
                    methXml = tags_Mapper.GetCount(userinfo, methodname, methparams);
                    break;

                case "Tag.GetSearch.base":
                    methXml = tags_Mapper.GetSearch(userinfo, methodname, methparams);
                    break;

                case "Tag.GetHistory.base":
                    methXml = tags_Mapper.GetHistory(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "Tag.New.base":
                    methXml = tags_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "Tag.Save.base":
                    methXml = tags_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "Tag.Delete.base":
                    methXml = tags_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                case "Tag.Recover.base":
                    methXml = tags_Mapper.Recover(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Tag.GetSource.base":
                    methXml = tags_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "Tag.Duplicate.base":
                    methXml = tags_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "Tag.GetSrcCount.base":
                    methXml = tags_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "Tag.GetDestCount.base":
                    methXml = tags_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "Tag.Replicate.base":
                    methXml = tags_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the DataGroup API.");
            }

            return methXml;
        }

    }
}
