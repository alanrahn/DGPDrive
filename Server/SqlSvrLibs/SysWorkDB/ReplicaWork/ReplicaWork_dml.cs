using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;

namespace SysWorkDB
{
    public class ReplicaWork_dml
    {

        string _connstr;

        public ReplicaWork_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string recgid)
        {
            SqlCommand sqlcmd = new SqlCommand();

            sqlcmd.CommandText = "select *" +
                                " from ReplicaWork" +
                                " where rec_gid = @rec_gid" +
                                " and rec_state = @ActiveRecState;";

            sqlcmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = recgid;
            sqlcmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlcmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string schematable)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From ReplicaWork" +
                                    " where rec_state = @ActiveRecState";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                // search filter values are optional
                if (schematable != null && schematable != "")
                {
                    sqlCmd.CommandText += " And SchemaTable Like @SchemaTable";
                    sqlCmd.Parameters.Add("@SchemaTable", SqlDbType.VarChar, 50).Value = schematable + "%";
                }

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetSearch(string pagenum, string pagesize, string schematable)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                int _pagesize = Convert.ToInt32(pagesize);
                if (_pagesize > 100) _pagesize = 100;
                int _offset = Convert.ToInt32(pagenum) * _pagesize;

                sqlCmd.CommandText = "Select SchemaType, SchemaTable, SrcDBName, ShardName, WorkType, SrcURL, SrcMethod, DestURL, DestMethod, StartID, FinalID, BatchSize, IntervalMS, NextRun, RunState, StateMsg, Logging, MaxDurMS, rec_gid, row_id" +
                                    " From ReplicaWork" +
                                    " where rec_state = @ActiveRecState";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                // search filter values are optional
                if (schematable != null && schematable != "")
                {
                    sqlCmd.CommandText += " And SchemaTable Like @SchemaTable";
                    sqlCmd.Parameters.Add("@SchemaTable", SqlDbType.VarChar, 50).Value = schematable + "%";
                }

                sqlCmd.CommandText += " Order By Network, SchemaTable, WorkType";

                sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                    " Fetch Next " + pagesize + " Rows Only";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAll()
        {
            SqlCommand sqlcmd = new SqlCommand();

            sqlcmd.CommandText = "select top(100) Network, Schematype, schematable, srcdbname, shardname, worktype, startid, finalid, runstate, statemsg, maxdurms, claimedby, claimtime, srcurl, srcmethod, desturl, destmethod" +
                                " from ReplicaWork" +
                                " where rec_state = @RowState" +
                                " order by ClaimTime DESC;";

            sqlcmd.Parameters.Add("@RowState", SqlDbType.VarChar, 10).Value = RecState.Active;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlcmd, _connstr);
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable QueueScan()
        {
            SqlCommand sqlcmd = new SqlCommand();

            sqlcmd.CommandText = "select top(10) *" +
                                " from ReplicaWork" +
                                " where rec_state = @RowState and RunState = @ClaimedRunState and (@CurrTime - ClaimTime > @MaxResetInterval)" +
                                " order by ClaimTime ASC;";

            MsgUtil msgUtil = new MsgUtil();
            long currTime = msgUtil.UnixTimeLong();
            long maxresetinterval = Convert.ToInt64(ConfigurationManager.AppSettings["ResetIntervalSec"].ToString()) * 1000;

            sqlcmd.Parameters.Add("@RowState", SqlDbType.VarChar, 10).Value = RecState.Active;
            sqlcmd.Parameters.Add("@ClaimedRunState", SqlDbType.VarChar, 10).Value = RunState.Claimed;
            sqlcmd.Parameters.Add("@ClaimTime", SqlDbType.BigInt).Value = currTime;
            sqlcmd.Parameters.Add("@MaxResetInterval", SqlDbType.BigInt).Value = maxresetinterval;

            DBUtil dBUtil = new DBUtil();
            return dBUtil.Read(sqlcmd, _connstr);
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// sets workstate, claimedby and claimtime values
        /// </summary>
        public DataTable ClaimReplicaWorkRecs(string netWork, string claimedBy)
        {
            CmnUtil cmnUtil = new CmnUtil();
            MsgUtil msgUtil = new MsgUtil();
            DataTable qresult = new DataTable();

            string claimBatch = ConfigurationManager.AppSettings["MaxClaimBatch"].ToString();
            string network = netWork;
            if (network == null || network == "") network = "LOCALHOST";
            string claimID = cmnUtil.GetNewGID();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Update ReplicaWork" +
                                    " Set ClaimedBy = @ClaimedBy, ClaimID = @ClaimID, ClaimTime = @ClaimTime, RunState = @ClaimedRunState" +
                                    " Where Network = @Network" +
                                    " AND row_id IN " +
                                    "(Select TOP (" + claimBatch + ") row_id From ReplicaWork With (ROWLOCK, UPDLOCK, READPAST)" +
                                    " Where RunState = @ReadyRunState and NextRun <= @CurrTime and rec_state = @ActiveRecState" +
                                    " Order By NextRun ASC);" + 
                                    " select * from ReplicaWork where ClaimedBy = @ClaimedBy and ClaimID = @ClaimID and RunState = @ClaimedRunState and rec_state = @ActiveRecState";

                long currTime = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@Network", SqlDbType.VarChar, 10).Value = network;
                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;
                sqlCmd.Parameters.Add("@ClaimedBy", SqlDbType.VarChar, 50).Value = claimedBy;
                sqlCmd.Parameters.Add("@ClaimID", SqlDbType.VarChar, 50).Value = claimID;
                sqlCmd.Parameters.Add("@ClaimTime", SqlDbType.BigInt).Value = currTime;
                sqlCmd.Parameters.Add("@ClaimedRunState", SqlDbType.VarChar, 10).Value = RunState.Claimed;
                sqlCmd.Parameters.Add("@ReadyRunState", SqlDbType.VarChar, 10).Value = RunState.Ready;
                sqlCmd.Parameters.Add("@CurrTime", SqlDbType.BigInt).Value = currTime;

                DBUtil dBUtil = new DBUtil();
                qresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return qresult;
        }

        /// <summary>
        /// sets nextrun, placeholder, workstate and statemsg values
        /// </summary>
        public string ReleaseReplicaWorkRec(string rec_gid,
                                            string nextrun,
                                            string runstate,
                                            string statemsg,
                                            string placeholder,
                                            string claimedby,
                                            string claimid,
                                            string procstate,
                                            string resetcount)
        {
            string strresult = "";
            CmnUtil cmnUtil = new CmnUtil();
            string claimtoken = cmnUtil.GetNewGID();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                if (runstate == RunState.Ready && (placeholder != null && placeholder != ""))
                {
                    sqlCmd.CommandText = "update ReplicaWork" +
                                        " set nextrun = @NextRun, runstate = @RunState, statemsg = @StateMsg, startid = @Placeholder, claimedby = '', ProcState = @ProcState, ResetCount = @ResetCount" +
                                        " where rec_gid = @rec_gid and claimedby = @ClaimedBy and ClaimID = @ClaimID and RunState = @ClaimedRunState;";

                    sqlCmd.Parameters.Add("@Placeholder", SqlDbType.BigInt).Value = Convert.ToInt64(placeholder);
                    sqlCmd.Parameters.Add("@NextRun", SqlDbType.BigInt).Value = Convert.ToInt64(nextrun);
                    sqlCmd.Parameters.Add("@ResetCount", SqlDbType.Int).Value = Convert.ToInt32(resetcount);
                }
                else
                {
                    // only update certain fields due to error state, leave other values as they are currently
                    sqlCmd.CommandText = "update ReplicaWork" +
                                        " set runstate = @RunState, statemsg = @StateMsg, ProcState = @ProcState" +
                                        " where rec_gid = @rec_gid and claimedby = @ClaimedBy and ClaimID = @ClaimID and RunState = @ClaimedRunState;";
                }

                sqlCmd.Parameters.Add("@ClaimedRunState", SqlDbType.VarChar, 10).Value = RunState.Claimed;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@RunState", SqlDbType.VarChar, 10).Value = runstate.ToUpper();
                sqlCmd.Parameters.Add("@StateMsg", SqlDbType.VarChar, 250).Value = statemsg;
                sqlCmd.Parameters.Add("@ClaimedBy", SqlDbType.VarChar, 50).Value = claimedby;
                sqlCmd.Parameters.Add("@ClaimID", SqlDbType.VarChar, 50).Value = claimid;
                sqlCmd.Parameters.Add("@ProcState", SqlDbType.VarChar, 10).Value = procstate;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// 
        /// </summary>
        public string New(string rec_gid,
                            string rec_user,
                            string network,
                            string schematype,
                            string schematable,
                            string srcdbname,
                            string shardname,
                            string worktype,
                            string srcurl,
                            string srcmethod,
                            string desturl,
                            string destmethod,
                            string startid,
                            string finalid,
                            string batchsize,
                            string intervalms,
                            string nextrun,
                            string runstate,
                            string logging,
                            string maxdurms)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Insert ReplicaWork (rec_gid, row_ms, rec_user, Network, SchemaType, SchemaTable, SrcDBName, ShardName, WorkType, SrcURL, SrcMethod, DestURL, DestMethod, StartID, FinalID, BatchSize, IntervalMS, NextRun, RunState, Logging, MaxDurMS, ResetCount)" +
                                    " Values (@rec_gid, @row_ms, @rec_user, @Network, @SchemaType, @SchemaTable, @SrcDBName, @ShardName, @WorkType, @SrcURL, @SrcMethod, @DestURL, @DestMethod, @StartID, @FinalID, @BatchSize, @IntervalMS, @NextRun, @RunState, @Logging, @MaxDurMS, 0);";

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@Network", SqlDbType.VarChar, 10).Value = network;
                sqlCmd.Parameters.Add("@SchemaType", SqlDbType.VarChar, 10).Value = schematype;
                sqlCmd.Parameters.Add("@SchemaTable", SqlDbType.VarChar, 50).Value = schematable;
                sqlCmd.Parameters.Add("@SrcDBName", SqlDbType.VarChar, 50).Value = srcdbname;
                sqlCmd.Parameters.Add("@ShardName", SqlDbType.VarChar, 50).Value = shardname;
                sqlCmd.Parameters.Add("@WorkType", SqlDbType.VarChar, 10).Value = worktype;
                sqlCmd.Parameters.Add("@SrcURL", SqlDbType.VarChar, 100).Value = srcurl;
                sqlCmd.Parameters.Add("@SrcMethod", SqlDbType.VarChar, 100).Value = srcmethod;
                sqlCmd.Parameters.Add("@DestURL", SqlDbType.VarChar, 100).Value = desturl;
                sqlCmd.Parameters.Add("@DestMethod", SqlDbType.VarChar, 100).Value = destmethod;
                sqlCmd.Parameters.Add("@StartID", SqlDbType.BigInt).Value = Convert.ToInt64(startid);
                sqlCmd.Parameters.Add("@FinalID", SqlDbType.BigInt).Value = Convert.ToInt64(finalid);
                sqlCmd.Parameters.Add("@BatchSize", SqlDbType.Int).Value = Convert.ToInt32(batchsize);
                sqlCmd.Parameters.Add("@IntervalMS", SqlDbType.Int).Value = Convert.ToInt32(intervalms);
                sqlCmd.Parameters.Add("@NextRun", SqlDbType.BigInt).Value = Convert.ToInt64(nextrun);
                sqlCmd.Parameters.Add("@RunState", SqlDbType.VarChar, 10).Value = runstate;
                sqlCmd.Parameters.Add("@Logging", SqlDbType.VarChar, 10).Value = logging;
                sqlCmd.Parameters.Add("@MaxDurMs", SqlDbType.Int).Value = Convert.ToInt32(maxdurms);

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }


        public string Clone(string rec_gid,
                            string rec_user,
                            string newgid)
        {
            string strresult = "";

            DataTable clonerec = GetByID(rec_gid);

            if (clonerec.Rows.Count > 0)
            {
                DataRow rec = clonerec.Rows[0];

                strresult = New(newgid,
                                rec_user,
                                rec[WorkFields.Network].ToString(),
                                rec[WorkFields.SchemaType].ToString(),
                                rec[WorkFields.SchemaTable].ToString(),
                                rec[WorkFields.SrcDBName].ToString(),
                                rec[FileFields.ShardName].ToString(),
                                rec[WorkFields.WorkType].ToString(),
                                rec[WorkFields.SrcURL].ToString(),
                                rec[WorkFields.SrcMethod].ToString(),
                                rec[WorkFields.DestURL].ToString(),
                                rec[WorkFields.DestMethod].ToString(),
                                rec[WorkFields.StartID].ToString(),
                                rec[WorkFields.FinalID].ToString(),
                                rec[WorkFields.BatchSize].ToString(),
                                rec[WorkFields.IntervalMS].ToString(),
                                rec[WorkFields.NextRun].ToString(),
                                RunState.Disabled,
                                rec[WorkFields.Logging].ToString(),
                                rec[WorkFields.MaxDurMS].ToString());
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Save(string actiontype,
                            string rec_gid,
                            string rec_user,
                            string network,
                            string schematype,
                            string schematable,
                            string srcdbname,
                            string shardname,
                            string worktype,
                            string srcurl,
                            string srcmethod,
                            string desturl,
                            string destmethod,
                            string startid,
                            string finalid,
                            string batchsize,
                            string intervalms,
                            string nextrun,
                            string runstate,
                            string logging,
                            string maxdurms,
                            string resetcount)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "UPDATE ReplicaWork" +
                                    " SET rec_user = @rec_user, row_ms = @row_ms, rec_state = @rec_state, Network = @Network, SchemaType = @SchemaType, SchemaTable = @SchemaTable, SrcDBName = @SrcDBName," + 
                                    " ShardName = @ShardName, WorkType = @WorkType, SrcURL = @SrcURL, SrcMethod = @SrcMethod, DestURL = @DestURL, DestMethod = @DestMethod, StartID = @StartID, FinalID = @FinalID," +
                                    " BatchSize = @BatchSize, IntervalMS = @IntervalMS, NextRun = @NextRun, RunState = @RunState, Logging = @Logging, MaxDurMS = @MaxDurMS, ResetCount = @ResetCount" +
                                    " WHERE rec_gid = @rec_gid AND rec_state = @RecStateActive;";

                string rstate = RecState.Active;
                if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;

                sqlCmd.Parameters.Add("@Network", SqlDbType.VarChar, 10).Value = network;
                sqlCmd.Parameters.Add("@SchemaType", SqlDbType.VarChar, 10).Value = schematype;
                sqlCmd.Parameters.Add("@SchemaTable", SqlDbType.VarChar, 50).Value = schematable;
                sqlCmd.Parameters.Add("@SrcDBName", SqlDbType.VarChar, 50).Value = srcdbname;
                sqlCmd.Parameters.Add("@ShardName", SqlDbType.VarChar, 50).Value = shardname;
                sqlCmd.Parameters.Add("@WorkType", SqlDbType.VarChar, 10).Value = worktype;
                sqlCmd.Parameters.Add("@SrcURL", SqlDbType.VarChar, 100).Value = srcurl;
                sqlCmd.Parameters.Add("@SrcMethod", SqlDbType.VarChar, 100).Value = srcmethod;
                sqlCmd.Parameters.Add("@DestURL", SqlDbType.VarChar, 100).Value = desturl;
                sqlCmd.Parameters.Add("@DestMethod", SqlDbType.VarChar, 100).Value = destmethod;
                sqlCmd.Parameters.Add("@StartID", SqlDbType.BigInt).Value = Convert.ToInt64(startid);
                sqlCmd.Parameters.Add("@FinalID", SqlDbType.BigInt).Value = Convert.ToInt64(finalid);
                sqlCmd.Parameters.Add("@BatchSize", SqlDbType.Int).Value = Convert.ToInt32(batchsize);
                sqlCmd.Parameters.Add("@IntervalMS", SqlDbType.Int).Value = Convert.ToInt32(intervalms);
                sqlCmd.Parameters.Add("@NextRun", SqlDbType.BigInt).Value = Convert.ToInt64(nextrun);
                sqlCmd.Parameters.Add("@RunState", SqlDbType.VarChar, 10).Value = runstate;
                sqlCmd.Parameters.Add("@Logging", SqlDbType.VarChar, 10).Value = logging;
                sqlCmd.Parameters.Add("@MaxDurMS", SqlDbType.Int).Value = Convert.ToInt32(maxdurms);
                sqlCmd.Parameters.Add("@ResetCount", SqlDbType.Int).Value = Convert.ToInt32(resetcount);

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// Hard delete
        /// </summary>
        public string Delete(string rec_gid)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Delete FROM ReplicaWork" +
                                    " WHERE  rec_gid = @rec_gid;";

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }
    }
}
