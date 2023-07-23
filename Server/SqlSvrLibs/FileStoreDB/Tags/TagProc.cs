
using System.Data;

using ApiUtil;

namespace FileStoreDB.Tags
{
    public class TagProc
    {
        string _connstr;

        public TagProc(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// recovering a deleted record inserts a new version, while recovering an edited record uses update logic
        /// </summary>
        public string RecoverTag(string action,
                                string rec_gid,
                                string row_id,
                                string new_row_ms)
        {
            string result = "";

            // get existing tag record
            Tags_dml tags_Dml = new Tags_dml(_connstr);
            DataTable tagstbl = tags_Dml.RecoverByID(rec_gid, row_id);

            if (tagstbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow tagrow = tagstbl.Rows[0];
                string edit_ms = tagrow["row_ms"].ToString();

                if (action == ReplicaAction.Update)
                {
                    DataTable tmptbl = tags_Dml.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];

                        edit_ms = tmprow["row_ms"].ToString();
                    }
                }

                result = tags_Dml.Write(action,
                                    tagrow["rec_gid"].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    tagrow["rec_user"].ToString(),
                                    tagrow["TagName"].ToString(),
                                    tagrow["TagDescrip"].ToString());
            }
            else
            {
                result = APIResult.Error + ": Tag " + row_id + " not found";
            }

            return result;
        }

    }
}
