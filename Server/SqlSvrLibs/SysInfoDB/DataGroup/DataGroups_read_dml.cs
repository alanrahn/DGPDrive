using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using ApiUtil;
using SqlSvrUtil;
using SysMetricsDB;

namespace SysInfoDB.DataGroups
{
    public class DataGroups_read_dml
    {
        string _connstr;

        public DataGroups_read_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string groupGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from DataGroups" +
                                    " where rec_gid = @GroupGID" +
                                    " and rec_state = @RecState;";

                sqlCmd.Parameters.Add("@GroupGID", SqlDbType.VarChar, 50).Value = groupGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByName(string groupName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from DataGroups" +
                                    " where rec_state = @RecState" +
                                    " and GroupName = @GroupName;";

                sqlCmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = groupName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetHistory(string groupGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from DataGroups" +
                                    " where rec_gid = @GroupGID" +
                                    " order by row_id ASC;";

                sqlCmd.Parameters.Add("@GroupGID", SqlDbType.VarChar, 50).Value = groupGID;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RecoverByID(string groupGID, string rowID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from DataGroups" +
                                    " where rec_gid = @rec_gid" +
                                    " and row_id = @RowID;";

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = groupGID;
                sqlCmd.Parameters.Add("@RowID", SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string groupname, string recState)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From DataGroups" +
                                    " where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (groupname != null && groupname != "")
                {
                    sqlCmd.CommandText += " And GroupName Like @GroupName";
                    sqlCmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = groupname + "%";
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
        public DataTable GetSearch(string pageNum, string pageSize, string recState, string sortorder, string groupname)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                if (sortorder != "ASC" && sortorder != "DESC") sortorder = "ASC";

                sqlCmd.CommandText = "Select GroupName, GroupDescrip, rec_gid, row_id" +
                                    " From DataGroups" +
                                    " where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (groupname != null && groupname != "")
                {
                    sqlCmd.CommandText += " And GroupName Like @GroupName";
                    sqlCmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = groupname + "%";
                }

                sqlCmd.CommandText += " Order By GroupName " + sortorder;

                sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                    " Fetch Next " + pageSize + " Rows Only;";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the DataGroups table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("DataGroups", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "DataGroups_read_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select GroupName, Count(*)" +
                                    " From DataGroups" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By GroupName" +
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

                    ServerErrLog.LogError(userName, webSvcName, "DataGroups_read_dml.DupeCheck", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "DataGroups", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "DataGroups", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, "DataGroups", "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, "DataGroups", maxdestid, _connstr);

            return srccount + "," + srcdestcount;
        }

        /// <summary>
        /// extracts records "as is" - no encryption or decryption of the groupkey fields
        /// </summary>
        public DataTable GetSrcRecs(string srcdbname, string placeholderid, string maxbatch)
        {
            string srcbatch = "10";
            if (maxbatch != null && maxbatch != "")
            {
                srcbatch = maxbatch;
            }

            DBUtil dBUtil = new DBUtil();
            return dBUtil.GetSrcRecs(srcdbname, "DataGroups", placeholderid, srcbatch, _connstr);
        }


    }
}
