using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPFileStoreAPI.Favorites
{
    public class Favorites_switch : IMethSwitch
    {
        string _connstr;

        public Favorites_switch()
        {
            _connstr = ConfigurationManager.AppSettings["FileStore"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Favorites_mapper favorites_Mapper = new Favorites_mapper(_connstr);

            switch (methodname)
            {
                case "Favorite.GetPageSize.base":
                    methXml = favorites_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Favorite.Assign.base":
                    methXml = favorites_Mapper.Assign(userinfo, methodname, methparams);
                    break;

                case "Favorite.Remove.base":
                    methXml = favorites_Mapper.Remove(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Favorite.Duplicate.base":
                    methXml = favorites_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "Favorite.GetSrcCount.base":
                    methXml = favorites_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "Favorite.GetDestCount.base":
                    methXml = favorites_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "Favorite.GetSource.base":
                    methXml = favorites_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "Favorite.Replicate.base":
                    methXml = favorites_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the FileTags API.");
            }

            return methXml;
        }

    }
}
