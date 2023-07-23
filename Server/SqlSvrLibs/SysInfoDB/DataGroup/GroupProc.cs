
using System.Data;

using ApiUtil;

namespace SysInfoDB.DataGroups
{
    public class GroupProc
    {
        string _connstr;

        public GroupProc(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// recovering a deleted record inserts a new version, while recovering an edited record uses update logic
        /// </summary>
        public string RecoverGroup(string action,
                                string rec_gid,
                                string row_id,
                                string new_row_ms)
        {
            string result = "";

            // get existing user record
            DataGroups_read_dml groups_read_Dml = new DataGroups_read_dml(_connstr);
            DataTable grouptbl = groups_read_Dml.RecoverByID(rec_gid, row_id);

            if (grouptbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow grouprow = grouptbl.Rows[0];
                string edit_ms = grouprow["row_ms"].ToString();

                if (action == ReplicaAction.Update)
                {
                    DataTable tmptbl = groups_read_Dml.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];

                        edit_ms = tmprow["row_ms"].ToString();
                    }
                }

                DataGroups_write_dml groups_write_Dml = new DataGroups_write_dml(_connstr);
                result = groups_write_Dml.Write(action,
                                    grouprow["rec_gid"].ToString(),
                                    grouprow["rec_user"].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    grouprow["GroupName"].ToString(),
                                    grouprow["GroupDescrip"].ToString());
            }
            else
            {
                result = APIResult.Error + ": DataGroup " + row_id + " not found";
            }

            return result;
        }

    }
}
