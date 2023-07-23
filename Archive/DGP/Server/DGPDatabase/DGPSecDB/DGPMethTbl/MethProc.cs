
using System.Data;

using CmnUtil;

namespace DGPDatabase.DGPSec
{
    public class MethProc
    {
        string _connstr;

        public MethProc(string connStr)
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
            MethRead methRead= new MethRead(_connstr);
            DataTable methtbl = methRead.RecoverByID(rec_gid, row_id);

            if (methtbl.Rows.Count > 0)
            {
                // create new record using values from selected record
                DataRow methrow = methtbl.Rows[0];
                string edit_ms = methrow[CmnFields.row_ms].ToString();

                if (action == DBAction.Update)
                {
                    DataTable tmptbl = methRead.GetByID(rec_gid);

                    if (tmptbl.Rows.Count > 0)
                    {
                        DataRow tmprow = tmptbl.Rows[0];
                        edit_ms = tmprow[CmnFields.row_ms].ToString();
                    }
                }

                MethWrite methWrite = new MethWrite(_connstr);
                result = methWrite.Write(action,
                                    methrow[CmnFields.rec_gid].ToString(),
                                    methrow[CmnFields.rec_acct].ToString(),
                                    new_row_ms,
                                    edit_ms,
                                    methrow[DGPMeth.FullName].ToString(),
                                    methrow[DGPMeth.APIName].ToString(),
                                    methrow[DGPMeth.MethName].ToString(),
                                    methrow[DGPMeth.MethDescrip].ToString());
            }
            else
            {
                result = MethResult.Error + ": Method " + row_id + " not found";
            }

            return result;
        }


    }
}
