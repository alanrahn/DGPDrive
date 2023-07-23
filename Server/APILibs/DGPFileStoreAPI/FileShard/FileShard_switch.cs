using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPFileStoreAPI.FileShard
{
    public class FileShard_switch
    {
        public FileShard_switch()
        {
            
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            FileShard_mapper fileshard_Mapper = new FileShard_mapper();

            switch (methodname)
            {
                case "FileShard.GetShardName.base":
                    methXml = fileshard_Mapper.GetShardName(userinfo, methodname, methparams);
                    break;

                case "FileShard.GetByID.base":
                    methXml = fileshard_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "FileShard.GetByRowID.base":
                    methXml = fileshard_Mapper.GetByRowID(userinfo, methodname, methparams);
                    break;

                case "FileShard.GetSegCount.base":
                    methXml = fileshard_Mapper.GetSegCount(userinfo, methodname, methparams);
                    break;

                case "FileShard.GetDataBySegNum.base":
                    methXml = fileshard_Mapper.GetDataBySegNum(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "FileShard.New.base":
                    methXml = fileshard_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "FileShard.Delete.base":
                    methXml = fileshard_Mapper.Save(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "FileShard.GetSource.base":
                    methXml = fileshard_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                case "FileShard.Duplicate.base":
                    methXml = fileshard_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "FileShard.GetSrcCount.base":
                    methXml = fileshard_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "FileShard.GetDestCount.base":
                    methXml = fileshard_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "FileShard.Replicate.base":
                    methXml = fileshard_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the DataGroup API.");
            }

            return methXml;
        }

    }
}
