using System;
using System.Collections.Generic;
using System.Data;

using ApiUtil;
using ApiUtil.DataClasses;
using MySqlLib.SysConfig.db_schemas;

namespace DGPConfigApi.db_schema
{
    public class Schema_mapper
    {
        public Schema_mapper()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                // optional search parameter
                string schematype = msgUtil.GetParamValue("SchemaType", methparams);
                string schemaname = msgUtil.GetParamValue("SchemaName", methparams);

                db_schemas_dml schema = new db_schemas_dml();
                string schemacount = schema.SchemaCount(schematype, schemaname);

                if (schemacount != null && schemacount != "")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), schemacount);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.GetCount method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SchemaSearch(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            DataTable results = new DataTable();

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }

                // pagination values
                string pagenum = msgUtil.GetParamValue("PageNum", methparams);
                string pagesize = msgUtil.GetParamValue("PageSize", methparams);

                // search values (optional)
                string schematype = msgUtil.GetParamValue("SchemaType", methparams);
                string schemaname = msgUtil.GetParamValue("SchemaName", methparams);

                db_schemas_dml schema = new db_schemas_dml();
                results = schema.SchemaSearch(pagenum, pagesize, schematype, schemaname);

                if (results.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string tblxml = svcUtil.TableToXml(results, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), tblxml);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.SchemaSearch method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSchemaByID(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }
                string schemagid = msgUtil.GetParamValue("SchemaGID", methparams);

                db_schemas_dml schema = new db_schemas_dml();
                DataTable schematbl = schema.GetSchemaByID(schemagid);

                if (schematbl != null && schematbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string schemastr = svcUtil.TableToXml(schematbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), schemastr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.GetSchemaByID method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSchemaByName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }
                string schemaname = msgUtil.GetParamValue("SchemaName", methparams);

                db_schemas_dml schema = new db_schemas_dml();
                DataTable schematbl = schema.GetSchemaByName(schemaname);

                if (schematbl != null && schematbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string schemastr = svcUtil.TableToXml(schematbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), schemastr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the Partition.GetSchemaByName method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSchemas(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                bool schemaFlag = true;
                string schemaflag = msgUtil.GetParamValue("SchemaFlag", methparams);
                if (schemaflag != null && schemaflag != "" && schemaflag.ToLower() == "false")
                {
                    schemaFlag = false;
                }

                db_schemas_dml schema = new db_schemas_dml();
                DataTable schematbl = schema.GetSchemas();

                if (schematbl != null && schematbl.Rows.Count > 0)
                {
                    SvcUtil svcUtil = new SvcUtil();
                    string schemastr = svcUtil.TableToXml(schematbl, schemaFlag);
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.DataTable.ToString(), schemastr);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Empty.ToString(), DTypes.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.GetSchemas method, and has been logged.", ex);
            }

            return resultxml;
        }



        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */



        /// <summary>
        /// 
        /// </summary>
        public string NewSchema(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                SvcUtil svcUtil = new SvcUtil();
                string schemagid = svcUtil.GetNewGID();
                string type = msgUtil.GetParamValue("SchemaType", methparams);
                string name = msgUtil.GetParamValue("SchemaName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);

                db_schemas_dml schema = new db_schemas_dml();
                string schemacode = schema.schema_insert(schemagid, type, name, descrip, userinfo.UserGID);

                if (schemacode != null && schemacode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), schemagid);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), schemacode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.NewSchema method, and has been logged.", ex);
            }

            return resultxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SaveSchema(UserInfo userinfo, string methodname, Dictionary<string, string> methparams, string action)
        {
            string resultxml = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                string schemagid = msgUtil.GetParamValue("SchemaGID", methparams);
                string row_src = msgUtil.GetParamValue("RowSrc", methparams);
                string type = msgUtil.GetParamValue("SchemaType", methparams);
                string name = msgUtil.GetParamValue("SchemaName", methparams);
                string descrip = msgUtil.GetParamValue("Description", methparams);

                db_schemas_dml schema = new db_schemas_dml();
                string schemacode = schema.schema_update(action, schemagid, row_src, type, name, descrip, userinfo.UserGID);

                if (schemacode != null && schemacode == "OK")
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.OK.ToString(), DTypes.Text.ToString(), schemacode);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, "Default", RCodes.Error.ToString(), DTypes.Text.ToString(), schemacode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the SchemaApi.SaveSchema method, and has been logged.", ex);
            }

            return resultxml;
        }

    }
}
