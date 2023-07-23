using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using DGPFileStoreAPI.FileShard;

namespace DGPFileStoreAPI.Files
{
    public class Files_switch
    {
        string _connstr;

        public Files_switch()
        {
            _connstr = ConfigurationManager.AppSettings["FileStore"].ToString();
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Files_read_mapper files_read_Mapper;
            Files_write_mapper files_write_Mapper;

            switch (methodname)
            {
                case "File.GetPageSize.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetPageSize(userinfo, methodname, methparams);
                    break;

                case "File.GetByID.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetByID(userinfo, methodname, methparams);
                    break;

                case "File.GetByName.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetByName(userinfo, methodname, methparams);
                    break;

                case "File.GetExtensionList.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetExtList(userinfo, methodname, methparams);
                    break;

                case "File.GetCountByFolder.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetCountByFolder(userinfo, methodname, methparams);
                    break;

                case "File.GetFilesByFolder.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetFilesByFolder(userinfo, methodname, methparams);
                    break;

                case "File.GetCountByMetadata.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetCountByMetadata(userinfo, methodname, methparams);
                    break;

                case "File.GetFilesByMetadata.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetFilesByMetadata(userinfo, methodname, methparams);
                    break;

                case "File.GetCountByTag.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetCountByTag(userinfo, methodname, methparams);
                    break;

                case "File.GetFilesByTag.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetFilesByTag(userinfo, methodname, methparams);
                    break;

                case "File.GetCountByFavorite.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetCountByFavorite(userinfo, methodname, methparams);
                    break;

                case "File.GetFilesByFavorite.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetFilesByFavorite(userinfo, methodname, methparams);
                    break;

                case "File.GetHistory.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetHistory(userinfo, methodname, methparams);
                    break;

                case "File.GetSource.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetSource(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "File.New.base":
                    files_write_Mapper = new Files_write_mapper(_connstr);
                    methXml = files_write_Mapper.New(userinfo, methodname, methparams);
                    break;

                case "File.Save.base":
                    files_write_Mapper = new Files_write_mapper(_connstr);
                    methXml = files_write_Mapper.Save(userinfo, methodname, methparams);
                    break;

                case "File.SaveFileRec.base":
                    files_write_Mapper = new Files_write_mapper(_connstr);
                    methXml = files_write_Mapper.SaveFileRec(userinfo, methodname, methparams, ReplicaAction.Update);
                    break;

                case "File.Delete.base":
                    files_write_Mapper = new Files_write_mapper(_connstr);
                    methXml = files_write_Mapper.SaveFileRec(userinfo, methodname, methparams, ReplicaAction.Delete);
                    break;

                case "File.Remove.base":
                    files_write_Mapper = new Files_write_mapper(_connstr);
                    methXml = files_write_Mapper.SaveFileRec(userinfo, methodname, methparams, ReplicaAction.Remove);
                    break;

                case "File.Recover.base":
                    files_write_Mapper = new Files_write_mapper(_connstr);
                    methXml = files_write_Mapper.Recover(userinfo, methodname, methparams);
                    break;

                //*******************************************************//
                //*******************************************************//

                case "File.Duplicate.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.DupeCheck(userinfo, methodname, methparams);
                    break;

                case "File.GetSrcCount.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetSrcCounts(userinfo, methodname, methparams);
                    break;

                case "File.GetDestCount.base":
                    files_read_Mapper = new Files_read_mapper(_connstr);
                    methXml = files_read_Mapper.GetDestCounts(userinfo, methodname, methparams);
                    break;

                case "File.Replicate.base":
                    files_write_Mapper = new Files_write_mapper(_connstr);
                    methXml = files_write_Mapper.Replicate(userinfo, methodname, methparams); ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the DataGroup API.");
            }

            return methXml;
        }
    }
}
