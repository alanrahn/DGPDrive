
using System;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class AcctGroupRead
    {
        string _connstr;

        public AcctGroupRead(string connStr)
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
                sqlCmd.CommandText = "select dg.GroupName, dg.GroupDescrip, dag.AccLev, dg.rec_gid as GroupGID, dag.rec_gid as AcctGroupGID" +
                                    " from DGPGroup dg" +
                                    " inner join DGPAcctGroup dag on dg.GroupName = dag.GroupName" +
                                    " where dag.AcctName = @AcctName" +
                                    " and dg.rec_state = @ActiveRecState and dag.rec_state = @ActiveRecState" +
                                    " order by dg.GroupName ASC;";

                sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctName;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

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
                sqlCmd.CommandText = "select GroupName, GroupDescrip" +
                                    " from DGPGroup" +
                                    " where rec_state = @ActiveRecState" +
                                    " and rec_gid not in" +
                                    " (select rec_gid" +
                                    " from DGPAcctGroup" +
                                    " where AcctName = @AcctName" +
                                    " and rec_state = @ActiveRecState)" +
                                    " order by GroupName ASC;";

                sqlCmd.Parameters.Add(DGPAcct.AcctName_prm, SqlDbType.VarChar, 50).Value = acctName;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the GroupUsers table
        /// </summary>
        public bool GetDupeCount(string acctName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID(DGPSecTables.DGPAcctGroup, _connstr);

                if (idcheck.Rows.Count > 0)
                {
                    dupcheck = true;

                    string delim = "";
                    string duplist = "";
                    foreach (DataRow dupid in idcheck.Rows)
                    {
                        duplist += delim + dupid[CmnFields.row_id].ToString();
                        delim = ",";
                    }

                    SvrErrLog.LogError(acctName, webSvcName, "AcctGroupRead.GetDupeCount", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select AcctName, GroupName, Count(*)" +
                                    " From DGPAcctGroup" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By AcctName, GroupName" +
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
                        duplist += delim + dupval[DGPAcct.AcctName].ToString() + "|" + dupval[DGPGroup.GroupName].ToString();
                        delim = ",";
                    }

                    SvrErrLog.LogError(acctName, webSvcName, "AcctGroupRead.GetDupeCount", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, DGPSecTables.DGPAcctGroup, _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, DGPSecTables.DGPAcctGroup, _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPAcctGroup, "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, DGPSecTables.DGPAcctGroup, maxdestid, _connstr);

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
            return dBUtil.GetSrcRecs(srcdbname, DGPSecTables.DGPAcctGroup, placeholderid, srcbatch, _connstr);
        }

    }
}
