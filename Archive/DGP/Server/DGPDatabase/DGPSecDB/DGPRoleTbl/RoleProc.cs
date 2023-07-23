
using System.Data;

using CmnUtil;

namespace DGPDatabase.DGPSec
{
    public class RoleProc
    {
        string _connstr;

        public RoleProc(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// recovering a deleted record inserts a new version, while recovering an edited record uses update logic
        /// </summary>
        public string RecoverRole(string action,
                                string rec_gid,
                                string row_id,
                                string new_row_ms)
        {
            string result = "";

            // get existing role record
            RoleRead roleRead = new RoleRead(_connstr);
            DataTable roletbl = roleRead.RecoverByID(rec_gid, row_id);

            if (roletbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow rolerow = roletbl.Rows[0];
                string edit_ms = rolerow["row_ms"].ToString();

                if (action == DBAction.Update)
                {
                    DataTable tmptbl = roleRead.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];

                        edit_ms = tmprow["row_ms"].ToString();
                    }
                }

                RoleWrite roleWrite = new RoleWrite(_connstr);
                result = roleWrite.Write(action,
                                    rolerow["rec_gid"].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    rolerow["rec_user"].ToString(),
                                    rolerow["RoleName"].ToString(),
                                    rolerow["RoleDescrip"].ToString());
            }
            else
            {
                result = MethResult.Error + ": Role " + row_id + " not found";
            }

            return result;
        }
    }
}
