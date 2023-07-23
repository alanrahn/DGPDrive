
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctRoleRead
    {
        string _connstr;

        public AcctRoleRead(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAssigned(string acctName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select dr.RoleName, dr.RoleDescrip, dr.rec_gid as RoleGID, dar.rec_gid as AcctRoleGID" +
                                    " from DGPAcct dr" +
                                    " inner join DGPAcctRole dar on dr.AcctName = dar.AcctName" +
                                    " where dar.AcctName = @AcctName" +
                                    " and dr.rec_state = @RecState and dar.rec_state = @RecState" +
                                    " order by dr.RoleName ASC;";

                sqlCmd.Parameters.Add("@AcctName", SqlDbType.VarChar, 50).Value = acctName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAvailable(string acctName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select RoleName, RoleDescrip, rec_gid as RoleGID" +
                                    " from DGPRole" +
                                    " where rec_state = @RecState" +
                                    " and RoleName not in" +
                                    " (select RoleName" +
                                    " from AcctRole" +
                                    " where AcctName = @AcctName" +
                                    " and rec_state = @RecState)" +
                                    " order by RoleName ASC;";

                sqlCmd.Parameters.Add("@AcctName", SqlDbType.VarChar, 50).Value = acctName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the RoleUsers table
        /// </summary>
        public bool GetDupeCount(string acctName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID(DGPSecTables.DGPAcctRole, _connstr);

                if (idcheck.Rows.Count > 0)
                {
                    dupcheck = true;

                    string delim = "";
                    string duplist = "";
                    foreach (DataRow dupid in idcheck.Rows)
                    {
                        duplist += delim + dupid["row_id"].ToString();
                        delim = ",";
                    }

                    SvrErrLog.LogError(acctName, webSvcName, "DGPAcctRole.GetDupeCount", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select AcctName, RoleName, Count(*)" +
                                    " From DGPAcctRole" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By AcctName, RoleName" +
                                    " Having Count(*) > 1;";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DataTable valcheck = dBUtil.Read(sqlCmd, _connstr);

                if (valcheck.Rows.Count > 0)
                {
                    dupcheck = true;

                    string delim = "";
                    string duplist = "";
                    foreach (DataRow dupval in valcheck.Rows)
                    {
                        duplist += delim + dupval[DGPAcct.AcctName].ToString() + "|" + dupval[DGPRole.RoleName].ToString();
                        delim = ",";
                    }

                    SvrErrLog.LogError(acctName, webSvcName, "AcctRoleRead.GetDupeCount", "Duplicate active values", duplist);
                }
            }

            return dupcheck;
        }

        /// <summary>
        /// query for the max src_id of records replicated in a destination table
        /// </summary>
        public string GetDestCounts(string srcdbname)
        {
            DBUtil dbUtil = new DBUtil();

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, DGPSecTables.DGPAcctRole, _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, DGPSecTables.DGPAcctRole, _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPAcctRole, "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPAcctRole, maxdestid, _connstr);

            return srccount + "," + srcdestcount;
        }

        /// <summary>
        /// query for a batch of source records from the specified table
        /// </summary>
        public DataTable GetSrcRecs(string srcdbname, string placeholderid, string maxbatch)
        {
            string srcbatch = "10";
            if (maxbatch != null && maxbatch != "")
            {
                srcbatch = maxbatch;
            }

            DBUtil dBUtil = new DBUtil();
            return dBUtil.GetSrcRecs(srcdbname, DGPSecTables.DGPAcctRole, placeholderid, srcbatch, _connstr);
        }

    }
}
