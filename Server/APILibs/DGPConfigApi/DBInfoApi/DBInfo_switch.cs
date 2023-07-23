using System;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPConfigApi.db_info
{
    public class DBInfo_switch : IMethSwitch
    {
        public DBInfo_switch()
        {

        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            DBInfo_mapper dbinfo = new DBInfo_mapper();

            switch (methodname)
            {
                case "DBInfo.GetCount.base":
                    methXml = dbinfo.GetCount(userinfo, methodname, methparams);
                    break;

                case "DBInfo.DBSearch.base":
                    methXml = dbinfo.DBSearch(userinfo, methodname, methparams);
                    break;

                case "DBInfo.GetDBInfoByID.base":
                    methXml = dbinfo.GetDBinfoByID(userinfo, methodname, methparams);
                    break;

                case "DBInfo.GetDBInfoByName.base":
                    methXml = dbinfo.GetDBInfoByName(userinfo, methodname, methparams);
                    break;

                case "DBInfo.GetDBInfo.base":
                    methXml = dbinfo.GetDBInfo(userinfo, methodname, methparams);
                    break;

                case "DBInfo.GetDBInfoByLoc.base":
                    methXml = dbinfo.GetDBInfoByLoc(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "DBInfo.NewDBInfo.base":
                    methXml = dbinfo.NewDBInfo(userinfo, methodname, methparams);
                    break;

                case "DBInfo.SaveDBInfo.base":
                    methXml = dbinfo.SaveDBInfo(userinfo, methodname, methparams, "update");
                    break;

                case "DBInfo.DeleteDBInfo.base":
                    methXml = dbinfo.SaveDBInfo(userinfo, methodname, methparams, "delete");
                    break;

                //*******************************************************//
                //*******************************************************//

                case "DBInfo.GetSrcRecs.base":
                    //methXml = ;
                    break;

                case "DBInfo.WriteSrcRecs.base":
                    //methXml = ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the DBInfo API.");
            }

            return methXml;
        }
    }
}
