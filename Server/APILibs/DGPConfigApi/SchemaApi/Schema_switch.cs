using System;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPConfigApi.db_schema
{
    public class Schema_switch : IMethSwitch
    {
        public Schema_switch()
        {

        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Schema_mapper schema = new Schema_mapper();

            switch (methodname)
            {
                case "Schema.GetCount.base":
                    methXml = schema.GetCount(userinfo, methodname, methparams);
                    break;

                case "Schema.SchemaSearch.base":
                    methXml = schema.SchemaSearch(userinfo, methodname, methparams);
                    break;

                case "Schema.GetSchemaByID.base":
                    methXml = schema.GetSchemaByID(userinfo, methodname, methparams);
                    break;

                case "Schema.GetSchemaByName.base":
                    methXml = schema.GetSchemaByName(userinfo, methodname, methparams);
                    break;

                case "Schema.GetSchemas.base":
                    methXml = schema.GetSchemas(userinfo, methodname, methparams);
                    break;

                //**********************************************************//
                //**********************************************************//

                case "Schema.NewSchema.base":
                    methXml = schema.NewSchema(userinfo, methodname, methparams);
                    break;

                case "Schema.SaveSchema.base":
                    methXml = schema.SaveSchema(userinfo, methodname, methparams, "update");
                    break;

                case "Schema.DeleteSchema.base":
                    methXml = schema.SaveSchema(userinfo, methodname, methparams, "delete");
                    break;

                //*******************************************************//
                //*******************************************************//

                case "Schema.GetSrcRecs.base":
                    //methXml = ;
                    break;

                case "Schema.WriteSrcRecs.base":
                    //methXml = ;
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the Schema API.");
            }

            return methXml;
        }
    }
}
