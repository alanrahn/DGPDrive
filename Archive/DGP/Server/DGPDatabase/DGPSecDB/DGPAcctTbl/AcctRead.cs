
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctRead
    {
        string _connstr;

        public AcctRead(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByToken(string secToken)
        {
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "select *" +
                                " from DGPAcct" +
                                " where rec_state = @rec_state" +
                                " and SecToken = @SecToken;";

            sqlCmd.Parameters.Add(DGPAcct.SecToken_prm, SqlDbType.VarChar, 50).Value = secToken;
            sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = RecState.Active;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlCmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public string CheckName(string acctName)
        {
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "Select count(*) AS Count" +
                                " From DGPAcct" +
                                " Where rec_state = @rec_state" +
                                " And AcctName = @AcctName;";

            sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctName.ToLower();
            sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = RecState.Active;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.ReadValue(sqlCmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string acctGID)
        {
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "select *" +
                                " from DGPAcct" +
                                " where rec_gid = @rec_gid" +
                                " and rec_state = @rec_state;";

            sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = acctGID;
            sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = RecState.Active;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlCmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RecoverByID(string acctGID, string rowID)
        {
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "select *" +
                                " from DGPAcct" +
                                " where rec_gid = @rec_gid" +
                                " and row_id = @row_id;";

            sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = acctGID;
            sqlCmd.Parameters.Add(CmnFields.row_id_prm, SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlCmd, _connstr);
        }

        /// <summary>
        /// This query should be case-insensitive
        /// </summary>
        public DataTable GetByName(string acctName)
        {
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "select *" +
                                " from DGPAcct" +
                                " where rec_state = @rec_state" +
                                " and AcctName = @AcctName;";

            sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctName;
            sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = RecState.Active;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlCmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetHistory(string acctGID)
        {
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "select *" +
                                " from DGPAcct" +
                                " where rec_gid = @rec_gid" +
                                " order by row_id ASC;";

            sqlCmd.Parameters.Add(CmnFields.rec_gid_prm, SqlDbType.VarChar, 50).Value = acctGID;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlCmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string acctName, string lastName, string firstName, string recState)
        {
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "Select count(*) AS Count" +
                                " From DGPAcct" +
                                " Where rec_state = @rec_state";

            sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = recState;

            // search filter values are optional
            if (acctName != null && acctName != "")
            {
                sqlCmd.CommandText += " And AcctName Like @AcctName";
                sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctName + "%";
            }

            if (lastName != null && lastName != "")
            {
                sqlCmd.CommandText += " And LastName Like @LastName";
                sqlCmd.Parameters.Add(DGPAcct.LastName_prm, SqlDbType.VarChar, 50).Value = lastName + "%";
            }

            if (firstName != null && firstName != "")
            {
                sqlCmd.CommandText += " And FirstName Like @FirstName";
                sqlCmd.Parameters.Add(DGPAcct.FirstName_prm, SqlDbType.VarChar, 50).Value = firstName + "%";
            }

            sqlCmd.CommandText += ";";

            DBUtil dBUtil = new DBUtil();
            return dBUtil.ReadValue(sqlCmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetSearch(string pageNum, string pageSize, string recState, string sortorder, string acctName, string lastName, string firstName)
        {
            SqlCommand sqlCmd = new SqlCommand();

            int _pagesize = Convert.ToInt32(pageSize);
            int _pagenum = Convert.ToInt32(pageNum);
            if (_pagenum < 0) _pagenum = 0;
            int _offset = _pagenum * _pagesize;

            if (sortorder != "ASC" && sortorder != "DESC") sortorder = "ASC";

            sqlCmd.CommandText = "Select LastName, FirstName, MiddleName, AcctName, AcctEmail, AcctType, AcctState, ExpDate, MethLimit, rec_gid, row_id" +
                                " From DGPAcct" +
                                " Where rec_state = @rec_state";

            sqlCmd.Parameters.Add(CmnFields.rec_state_prm, SqlDbType.VarChar, 10).Value = recState;

            // search filter values are optional
            if (acctName != null && acctName != "")
            {
                sqlCmd.CommandText += " And AcctName Like @AcctName";
                sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctName + "%";
            }

            if (lastName != null && lastName != "")
            {
                sqlCmd.CommandText += " And LastName Like @LastName";
                sqlCmd.Parameters.Add(DGPAcct.LastName_prm, SqlDbType.VarChar, 50).Value = lastName + "%";
            }

            if (firstName != null && firstName != "")
            {
                sqlCmd.CommandText += " And FirstName Like @FirstName";
                sqlCmd.Parameters.Add(DGPAcct.FirstName_prm, SqlDbType.VarChar, 50).Value = firstName + "%";
            }

            sqlCmd.CommandText += " Order By LastName " + sortorder + ", FirstName " + sortorder + ", AcctName " + sortorder;

            sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                " Fetch Next " + pageSize + " Rows Only;";

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlCmd, _connstr);
        }

        /// <summary>
        /// query for the duplicate active records in the APIUsers table
        /// </summary>
        public bool GetDupeCount(string acctName, string webSvcName)
        {
            bool dupcheck = false;

            SqlCommand sqlCmd = new SqlCommand();
            DBUtil dBUtil = new DBUtil();

            DataTable idcheck = dBUtil.DupeCheckByID(DGPSecTables.DGPAcct, _connstr);

            if (idcheck.Rows.Count > 0)
            {
                dupcheck = true;

                string delim = "";
                string duplist = "";
                foreach (DataRow dupid in idcheck.Rows)
                {
                    duplist += delim + dupid[CmnFields.rec_gid].ToString();
                    delim = ",";
                }

                SvrErrLog.LogError(acctName, webSvcName, "DGPAcct.GetDupeCount", "Duplicate active rec_gid", duplist);
            }

            sqlCmd.CommandText = "Select AcctName, Count(*)" +
                                " From DGPAcct" +
                                " Where rec_state = @ActiveRecState" +
                                " Group By AcctName" +
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
                    duplist += delim + dupval[DGPAcct.AcctName].ToString();
                    delim = ",";
                }

                SvrErrLog.LogError(acctName, webSvcName, "DGPAcct.GetDupeCount", "Duplicate active values", duplist);
            }

            return dupcheck;
        }

        /// <summary>
        /// query for the max src_id of records replicated in a destination table
        /// </summary>
        public string GetDestCounts(string srcdbname)
        {
            DBUtil dbUtil = new DBUtil();

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, DGPSecTables.DGPAcct, _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, DGPSecTables.DGPAcct, _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPAcct, "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPAcct, maxdestid, _connstr);

            return srccount + "," + srcdestcount;
        }

        /// <summary>
        /// query for a batch of source records from the specified table (password value is not decrypted)
        /// </summary>
        public DataTable GetSrcRecs(string srcdbname, string placeholderid, string maxbatch)
        {
            string srcbatch = "10";
            if (maxbatch != null && maxbatch != "")
            {
                srcbatch = maxbatch;
            }

            DBUtil dBUtil = new DBUtil();
            return dBUtil.GetSrcRecs(srcdbname, DGPSecTables.DGPAcct, placeholderid, srcbatch, _connstr);
        }

    }
}
