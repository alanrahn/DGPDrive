using System;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPConfigApi.sys_location
{
    public class Location_switch : IMethSwitch
    {
        public Location_switch()
        {

        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Location_mapper location = new Location_mapper();

            switch (methodname)
            {
                case "Location.GetCount.base":
                    methXml = location.GetCount(userinfo, methodname, methparams);
                    break;

                case "Location.LocationSearch.base":
                    methXml = location.LocationSearch(userinfo, methodname, methparams);
                    break;

                case "Location.GetLocations.base":
                    methXml = location.GetLocations(userinfo, methodname, methparams);
                    break;

                case "Location.GetLocationByID.base":
                    methXml = location.GetLocationByID(userinfo, methodname, methparams);
                    break;

                case "Location.GetLocationByName.base":
                    methXml = location.GetLocationByName(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "Location.NewLocation.base":
                    methXml = location.NewLocation(userinfo, methodname, methparams);
                    break;

                case "Location.SaveLocation.base":
                    methXml = location.SaveLocation(userinfo, methodname, methparams, "update");
                    break;

                case "Location.DeleteLocation.base":
                    methXml = location.SaveLocation(userinfo, methodname, methparams, "delete");
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Location.GetSrcRecs.base":
                    //methXml = ;
                    break;

                case "Location.WriteSrcRecs.base":
                    //methXml = ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the Location API.");
            }

            return methXml;
        }
    }
}
