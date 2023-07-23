using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace SysInfoDB.APIRoles
{
    public class APIRoles_read_dml
    {
        string _connstr;

        public APIRoles_read_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string roleGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from APIRoles" +
                                    " where rec_gid = @RoleGID" +
                                    " and rec_state = @RecState;";

                sqlCmd.Parameters.Add("@RoleGID", SqlDbType.VarChar, 50).Value = roleGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByName(string roleName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from APIRoles" +
                                    " where rec_state = @RecState" +
                                    " and RoleName = @RoleName;";

                sqlCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = roleName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetHistory(string roleGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from APIRoles" +
                                    " where rec_gid = @RoleGID" +
                                    " order by row_id ASC;";

                sqlCmd.Parameters.Add("@RoleGID", SqlDbType.VarChar, 50).Value = roleGID;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RecoverByID(string roleGID, string rowID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from APIRoles" +
                                    " where rec_gid = @rec_gid" +
                                    " and row_id = @RowID;";

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = roleGID;
                sqlCmd.Parameters.Add("@RowID", SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string rolename,string recState)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From APIRoles" +
                                    " where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (rolename != null && rolename != "")
                {
                    sqlCmd.CommandText += " And RoleName Like @RoleName";
                    sqlCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = rolename + "%";
                }

                sqlCmd.CommandText += ";";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetSearch(string pageNum, string pageSize, string recState, string sortorder, string rolename)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                if (sortorder != "ASC" && sortorder != "DESC") sortorder = "ASC";

                sqlCmd.CommandText = "Select RoleName, RoleDescrip, rec_gid, row_id" +
                                    " From APIRoles" +
                                    " where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (rolename != null && rolename != "")
                {
                    sqlCmd.CommandText += " And RoleName Like @RoleName";
                    sqlCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = rolename + "%";
                }

                sqlCmd.CommandText += " Order By RoleName " + sortorder;

                sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                    " Fetch Next " + pageSize + " Rows Only";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the APIRoles table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("APIRoles", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "APIRoles_read_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select RoleName, Count(*)" +
                                    " From APIRoles" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By RoleName" +
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
                        duplist += delim + dupval["row_id"].ToString();
                        delim = ",";
                    }

                    ServerErrLog.LogError(userName, webSvcName, "APIRoles_read_dml.DupeCheck", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "APIRoles", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "APIRoles", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, "APIRoles", "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, "APIRoles", maxdestid, _connstr);

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
            return dBUtil.GetSrcRecs(srcdbname, "APIRoles", placeholderid, srcbatch, _connstr);
        }

    }
}
