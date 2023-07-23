
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;

namespace SysMetricsDB
{
    public class DGPErrors_dml
    {
        string _connstr;

        public DGPErrors_dml(string connStr)
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
                                    " from DGPErrors" +
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
        public string GetCount(string appName, string className, string errLevel)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From DGPErrors";

                string linkcmd = " Where";
                // search filter values are optional
                if (appName != null && appName != "")
                {
                    sqlCmd.CommandText += linkcmd + " AppName Like @AppName";
                    sqlCmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = appName + "%";
                    linkcmd = " And";
                }

                if (className != null && className != "")
                {
                    sqlCmd.CommandText += linkcmd + " ClassName Like @ClassName";
                    sqlCmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 50).Value = className + "%";
                    linkcmd = " And";
                }

                if (errLevel != null && errLevel != "")
                {
                    sqlCmd.CommandText += linkcmd + " ErrLevel Like @ErrLevel";
                    sqlCmd.Parameters.Add("@ErrLevel", SqlDbType.VarChar, 10).Value = errLevel + "%";
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
        public DataTable GetSearch(string pageNum, string pageSize, string appName, string className, string errLevel)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                sqlCmd.CommandText = "Select LogTime, ErrLoc, ErrLevel, ErrMessage, AppName, ClassName, CompName, UserName, row_gid, row_id" +
                                    " From DGPErrors";

                // search filter values are optional
                string linkcmd = " Where";
                string orderby = " Order By LogTime DESC";

                // search filter values are optional
                if (appName != null && appName != "")
                {
                    sqlCmd.CommandText += linkcmd + " AppName Like @AppName";
                    sqlCmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = appName + "%";
                    linkcmd = " And";
                    orderby += ", AppName ASC";
                }

                if (className != null && className != "")
                {
                    sqlCmd.CommandText += linkcmd + " ClassName Like @ClassName";
                    sqlCmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 50).Value = className + "%";
                    linkcmd = " And";
                    orderby += ", ClassName ASC";
                }

                if (errLevel != null && errLevel != "")
                {
                    sqlCmd.CommandText += linkcmd + " ErrLevel Like @ErrLevel";
                    sqlCmd.Parameters.Add("@ErrLevel", SqlDbType.VarChar, 10).Value = errLevel + "%";
                    linkcmd = " And";
                    orderby += ", ErrLevel ASC";
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
        public string GetErrData(string row_gid)
        {
            string dtresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select ErrData" +
                                    " from DGPErrors" +
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
        public DataTable GetAll()
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select top 10 row_id, LogTime, ErrLoc, ErrLevel, ErrMessage, AppName, ClassName, CompName, UserName, row_gid, ErrData" +
                                    " from DGPErrors" +
                                    " order by LogTime DESC;";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Write(string row_gid,
                            string username,
                            string compname,
                            string appname,
                            string classname,
                            string errloc,
                            string errlevel,
                            string errmessage,
                            string errdata)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Insert DGPErrors (row_gid, UserName, CompName, AppName, ClassName, ErrLoc, ErrLevel, ErrMessage, ErrData)" +
                                    " Values (@row_gid, @UserName, @CompName, @AppName, @ClassName, @ErrLoc, @ErrLevel, @ErrMessage, @ErrData);";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;
                sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
                sqlCmd.Parameters.Add("@CompName", SqlDbType.VarChar, 50).Value = compname;
                sqlCmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = appname;
                sqlCmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 50).Value = classname;
                sqlCmd.Parameters.Add("@ErrLoc", SqlDbType.VarChar, 10).Value = errloc;
                sqlCmd.Parameters.Add("@ErrLevel", SqlDbType.VarChar, 10).Value = errlevel;
                sqlCmd.Parameters.Add("@ErrMessage", SqlDbType.VarChar, 250).Value = errmessage;

                if (errdata == null || errdata.Length <= 0) errdata = "";
                sqlCmd.Parameters.Add("@ErrData", SqlDbType.VarChar, 5000).Value = errdata;

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
                sqlCmd.CommandText = "Delete FROM DGPErrors" +
                                    " WHERE  row_gid = @row_gid;";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }
    }
}
