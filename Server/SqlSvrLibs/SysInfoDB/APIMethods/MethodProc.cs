using System.Data;

using ApiUtil;

namespace SysInfoDB.APIMethods
{
    public class MethodProc
    {
        string _connstr;

        public MethodProc(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// recovering a deleted record inserts a new version, while recovering an edited record uses update logic
        /// </summary>
        public string RecoverMethod(string action,
                                string rec_gid,
                                string row_id,
                                string new_row_ms)
        {
            string result = "";

            // get existing user record
            APIMethods_read_dml methods_read_Dml = new APIMethods_read_dml(_connstr);
            DataTable methtbl = methods_read_Dml.RecoverByID(rec_gid, row_id);

            if (methtbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow methrow = methtbl.Rows[0];
                string edit_ms = methrow["row_ms"].ToString();

                if (action == ReplicaAction.Update)
                {
                    DataTable tmptbl = methods_read_Dml.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];
                        edit_ms = tmprow["row_ms"].ToString();
                    }
                }

                APIMethods_write_dml methods_write_Dml = new APIMethods_write_dml(_connstr);
                result = methods_write_Dml.Write(action,
                                    methrow["rec_gid"].ToString(),
                                    methrow["rec_user"].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    methrow["APIName"].ToString(),
                                    methrow["MethodName"].ToString(),
                                    methrow["VersionName"].ToString(),
                                    methrow["MethodDescrip"].ToString());
            }
            else
            {
                result = APIResult.Error + ": Method " + row_id + " not found";
            }

            return result;
        }

    }
}
