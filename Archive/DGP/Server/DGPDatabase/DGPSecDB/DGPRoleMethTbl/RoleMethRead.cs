
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class RoleMethRead
    {
        string _connstr;

        public RoleMethRead(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAcctMethods(string acctName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select distinct dm.FullName" +
                                    " from DGPMeth dm" +
                                    " inner join DGPRoleMeth drm on dm.FullName = drm.FullName" +
                                    " inner join DGPAcctRole dar on drm.RoleName = dar.RoleName" +
                                    " where dar.AcctName = @AcctName" +
                                    " and am.rec_state = @RecState and rm.rec_state = @RecState and ru.rec_state = @RecState" +
                                    " order by am.APIName, am.MethodName, am.VersionName ASC;";

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
        public DataTable GetMethodRoles(string fullName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select dr.RoleName, dr.RoleDescrip" +
                                    " from DGPRole dr" +
                                    " inner join DGPRoleMeth drm on dr.RoleName = drm.RoleName" +
                                    " where drm.FullName = @FullName" +
                                    " and dr.rec_state = @RecState and drm.rec_state = @RecState" +
                                    " order by dr.RoleName ASC;";

                sqlCmd.Parameters.Add("@FullName", SqlDbType.VarChar, 50).Value = fullName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAssigned(string roleName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select dm.FullName, dm.MethDescrip, dm.rec_gid as MethGID, drm.rec_gid as RoleMethGID" +
                                    " from DGPMeth dm" +
                                    " inner join DGPRoleMeth drm on dm.FullName = drm.FullName" +
                                    " where drm.RoleName = @RoleName" +
                                    " and dm.rec_state = @RecState and drm.rec_state = @RecState" +
                                    " order by dm.FullName ASC;";

                sqlCmd.Parameters.Add("@roleName", SqlDbType.VarChar, 50).Value = roleName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAvailable(string roleName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select FullName, MethDescrip, rec_gid as MethGID" +
                                    " from DGPMeth" +
                                    " where rec_state = @RecState" +
                                    " and FullName not in" +
                                    " (select FullName" +
                                    " from DGPRoleMeth" +
                                    " where RoleName = @RoleName" +
                                    " and rec_state = @RecState)" +
                                    " order by FullName ASC;";

                sqlCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = roleName;
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

                    SvrErrLog.LogError(acctName, webSvcName, "RoleMethRead.GetDupeCount", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select RoleName, FullName, Count(*)" +
                                    " From DGPRoleMeth" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By RoleName, FullName" +
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
                        duplist += delim + dupval[DGPRole.RoleName].ToString() + "|" + dupval[DGPMeth.MethName].ToString();
                        delim = ",";
                    }

                    SvrErrLog.LogError(acctName, webSvcName, "RoleMethRead.GetDupeCount", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, DGPSecTables.DGPRoleMeth, _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, DGPSecTables.DGPRoleMeth, _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPRoleMeth, "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPRoleMeth, maxdestid, _connstr);

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
            return dBUtil.GetSrcRecs(srcdbname, DGPSecTables.DGPRoleMeth, placeholderid, srcbatch, _connstr);
        }


    }
}
