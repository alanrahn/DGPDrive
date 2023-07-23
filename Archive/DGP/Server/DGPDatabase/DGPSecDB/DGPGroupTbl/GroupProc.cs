
using System.Data;

using CmnUtil;

namespace DGPDatabase.DGPSec
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

            // get existing role record
            GroupRead groupRead = new GroupRead(_connstr);
            DataTable grouptbl = groupRead.RecoverByID(rec_gid, row_id);

            if (grouptbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow grouprow = grouptbl.Rows[0];
                string edit_ms = grouprow["row_ms"].ToString();

                if (action == DBAction.Update)
                {
                    DataTable tmptbl = groupRead.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];

                        edit_ms = tmprow["row_ms"].ToString();
                    }
                }

                GroupWrite groupWrite = new GroupWrite(_connstr);
                result = groupWrite.Write(action,
                                    grouprow["rec_gid"].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    grouprow["rec_user"].ToString(),
                                    grouprow["GroupName"].ToString(),
                                    grouprow["GroupDescrip"].ToString());
            }
            else
            {
                result = MethResult.Error + ": Group " + row_id + " not found";
            }

            return result;
        }

    }
}
