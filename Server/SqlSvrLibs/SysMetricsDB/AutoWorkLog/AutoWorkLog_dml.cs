
using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;

namespace SysMetricsDB
{
    public class AutoWorkLog_dml
    {
        string _connstr;

        public AutoWorkLog_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string recGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from AutoWorkLog" +
                                    " where row_gid = @row_gid;";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = recGID;

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
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select top 100 *" +
                                    " from AutoWorkLog" +
                                    " order by LogTime DESC;";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string workType, string runState, string procState)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From AutoWorkLog";

                string linkcmd = " Where";
                // search filter values are optional
                if (workType != null && workType != "")
                {
                    sqlCmd.CommandText += linkcmd + " WorkType Like @WorkType";
                    sqlCmd.Parameters.Add("@Worktype", SqlDbType.VarChar, 10).Value = workType + "%";
                    linkcmd = " And";
                }

                if (runState != null && runState != "")
                {
                    sqlCmd.CommandText += linkcmd + " RunState Like @RunState";
                    sqlCmd.Parameters.Add("@RunState", SqlDbType.VarChar, 10).Value = runState + "%";
                    linkcmd = " And";
                }

                if (procState != null && procState != "")
                {
                    sqlCmd.CommandText += linkcmd + " ProcState Like @ProcState";
                    sqlCmd.Parameters.Add("@ErrLevel", SqlDbType.VarChar, 10).Value = procState + "%";
                    linkcmd = " And";
                }



                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetSearch(string pageNum, string pageSize, string workType, string runState, string procState)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                sqlCmd.CommandText = "Select LogTime, WorkType, CompName, ClaimedBy, DurationMS, MaxDurMS, RunState, StateMsg, ProcState, row_gid, row_id" +
                                    " From AutoWorkLog";

                // search filter values are optional
                string linkcmd = " Where";
                string orderby = " Order By LogTime DESC";

                // search filter values are optional
                if (workType != null && workType != "")
                {
                    sqlCmd.CommandText += linkcmd + " WorkType Like @WorkType";
                    sqlCmd.Parameters.Add("@Worktype", SqlDbType.VarChar, 10).Value = workType + "%";
                    linkcmd = " And";
                    orderby += ", WorkType ASC";
                }

                if (runState != null && runState != "")
                {
                    sqlCmd.CommandText += linkcmd + " RunState Like @RunState";
                    sqlCmd.Parameters.Add("@RunState", SqlDbType.VarChar, 10).Value = runState + "%";
                    linkcmd = " And";
                    orderby += ", RunState ASC";
                }

                if (procState != null && procState != "")
                {
                    sqlCmd.CommandText += linkcmd + " ProcState Like @ProcState";
                    sqlCmd.Parameters.Add("@ErrLevel", SqlDbType.VarChar, 10).Value = procState + "%";
                    linkcmd = " And";
                    orderby += ", ProcState ASC";
                }

                sqlCmd.CommandText += orderby;

                sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                    " Fetch Next " + pageSize + " Rows Only";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetProcSteps(string row_gid)
        {
            string dtresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select ProcSteps" +
                                    " from AutoWorkLog" +
                                    " where row_gid = @rec_gid;";

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = row_gid;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Write(string row_gid,
                            string worktype,
                            string compname,
                            string claimedby,
                            string durationms,
                            string maxdurms,
                            string runstate,
                            string statemsg,
                            string procstate,
                            string procsteps)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Insert AutoWorkLog (row_gid, WorkType, CompName, ClaimedBy, DurationMS, MaxDurMS, RunState, StateMsg, ProcState, ProcSteps)" +
                                    " Values (@row_gid, @WorkType, @CompName, @ClaimedBy, @DurationMS, @MaxDurMS, @RunState, @StateMsg, @ProcState, @ProcSteps);";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;
                sqlCmd.Parameters.Add("@Worktype", SqlDbType.VarChar, 10).Value = worktype;
                sqlCmd.Parameters.Add("@CompName", SqlDbType.VarChar, 50).Value = compname;
                sqlCmd.Parameters.Add("@ClaimedBy", SqlDbType.VarChar, 50).Value = claimedby;
                sqlCmd.Parameters.Add("@DurationMS", SqlDbType.Int).Value = Convert.ToInt32(durationms);
                sqlCmd.Parameters.Add("@MaxDurMS", SqlDbType.Int).Value = Convert.ToInt32(maxdurms);
                sqlCmd.Parameters.Add("@RunState", SqlDbType.VarChar, 10).Value = runstate;
                sqlCmd.Parameters.Add("@StateMsg", SqlDbType.VarChar, 250).Value = statemsg;
                sqlCmd.Parameters.Add("@ProcState", SqlDbType.VarChar, 10).Value = procstate;
                sqlCmd.Parameters.Add("@ProcSteps", SqlDbType.VarChar, 5000).Value = procsteps;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// Hard delete
        /// </summary>
        public string Delete(string row_gid)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Delete FROM AutoWorkLog" +
                                    " WHERE  row_gid = @row_gid;";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }
    }
}
