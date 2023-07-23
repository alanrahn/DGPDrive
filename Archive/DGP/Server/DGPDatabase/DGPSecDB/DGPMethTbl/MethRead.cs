﻿
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class MethRead
    {
        string _connstr;

        public MethRead(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByName(string fullName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from DGPMeth" +
                                    " where FullName = @FullName" +
                                    " and rec_state = @rec_state;";

                sqlCmd.Parameters.Add(DGPMeth.FullName_prm, SqlDbType.VarChar, 100).Value = fullName;
                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string methodGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from DGPMeth" +
                                    " where rec_gid = @rec_gid" +
                                    " and rec_state = @rec_state;";

                sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = methodGID;
                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAPIList()
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select distinct APIName" +
                                    " from DGPMeth" +
                                    " where rec_state = @rec_state" +
                                    " order by APIName ASC;";

                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetHistory(string methodGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from DGPMeth" +
                                    " where rec_gid = @rec_gid" +
                                    " order by row_id ASC;";

                sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = methodGID;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RecoverByID(string methodGID, string rowID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from DGPMeth" +
                                    " where rec_gid = @rec_gid" +
                                    " and row_id = @row_id;";

                sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = methodGID;
                sqlCmd.Parameters.Add(CmnFields.row_id_prm, SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string apiName, string methodName, string recState)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From DGPMeth" +
                                    " where rec_state = @rec_state";

                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (apiName != null && apiName != "")
                {
                    sqlCmd.CommandText += " And APIName Like @APIName";
                    sqlCmd.Parameters.Add(DGPMeth.APIName_prm, SqlDbType.VarChar, 50).Value = apiName + "%";
                }

                if (methodName != null && methodName != "")
                {
                    sqlCmd.CommandText += " And MethodName Like @MethodName";
                    sqlCmd.Parameters.Add(DGPMeth.MethName_prm, SqlDbType.VarChar, 50).Value = methodName + "%";
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
        public DataTable GetSearch(string pageNum, string pageSize, string recState, string sortorder, string apiName, string methodName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                if (sortorder != "ASC" && sortorder != "DESC") sortorder = "ASC";

                sqlCmd.CommandText = "Select FullName, APIName, MethodName, MethodDescrip, rec_gid, row_id" +
                                    " From DGPMeth" +
                                    " Where rec_state = @rec_state";

                sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (apiName != null && apiName != "")
                {
                    sqlCmd.CommandText += " And APIName Like @APIName";
                    sqlCmd.Parameters.Add(DGPMeth.APIName_prm, SqlDbType.VarChar, 50).Value = apiName + "%";
                }

                if (methodName != null && methodName != "")
                {
                    sqlCmd.CommandText += " And MethodName Like @MethodName";
                    sqlCmd.Parameters.Add(DGPMeth.MethName_prm, SqlDbType.VarChar, 50).Value = methodName + "%";
                }

                sqlCmd.CommandText += " Order By APIName " + sortorder + ", MethodName " + sortorder;

                sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                    " Fetch Next " + pageSize + " Rows Only;";


                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the APIMethods table
        /// </summary>
        public bool GetDupeCount(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID(DGPSecTables.DGPMeth, _connstr);

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

                    SvrErrLog.LogError(userName, webSvcName, "MethRead.GetDupeCount", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select FullName, Count(*)" +
                                    " From DGPMeth" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By FullName" +
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
                        duplist += delim + dupval[CmnFields.row_id].ToString();
                        delim = ",";
                    }

                    SvrErrLog.LogError(userName, webSvcName, "MethRead.GetDupeCount", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, DGPSecTables.DGPMeth, _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, DGPSecTables.DGPMeth, _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPMeth, "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPMeth, maxdestid, _connstr);

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
            return dBUtil.GetSrcRecs(srcdbname, DGPSecTables.DGPMeth, placeholderid, srcbatch, _connstr);
        }
    }
}
