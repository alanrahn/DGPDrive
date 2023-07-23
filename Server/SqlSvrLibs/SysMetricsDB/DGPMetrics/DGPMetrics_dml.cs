using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;

namespace SysMetricsDB
{
    public class DGPMetrics_dml
    {
        string _connstr;

        public DGPMetrics_dml(string connStr)
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
                                    " from DGPMetrics" +
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
                sqlCmd.CommandText = "select top 50 *" +
                                    " from DGPMetrics" +
                                    " order by ServerTime DESC;";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string appName, string formName, string webSvcName)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From DGPMetrics";

                string linkcmd = " Where";
                // search filter values are optional
                if (appName != null && appName != "")
                {
                    sqlCmd.CommandText += linkcmd + " AppName Like @AppName";
                    sqlCmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = appName + "%";
                    linkcmd = " And";
                }

                if (formName != null && formName != "")
                {
                    sqlCmd.CommandText += linkcmd + " FormName Like @FormName";
                    sqlCmd.Parameters.Add("@FormName", SqlDbType.VarChar, 50).Value = formName + "%";
                    linkcmd = " And";
                }

                if (webSvcName != null && webSvcName != "")
                {
                    sqlCmd.CommandText += linkcmd + " WebSvcName Like @WebSvcName";
                    sqlCmd.Parameters.Add("@WebSvcName", SqlDbType.VarChar, 50).Value = webSvcName + "%";
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
        public DataTable GetSearch(string pageNum, string pageSize, string appName, string formName, string webSvcName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                sqlCmd.CommandText = "Select AppName, FormName, WebSvcName, WebSvcVer, ServerTime, ClientTime, MethodCount, EndToEndMS, NetworkMS, ServerMS, UserName, CompName, row_gid, row_id" +
                                    " From DGPMetrics";

                // search filter values are optional
                string linkcmd = " Where";
                string orderby = " Order By ServerTime DESC";

                // search filter values are optional
                if (appName != null && appName != "")
                {
                    sqlCmd.CommandText += linkcmd + " AppName Like @AppName";
                    sqlCmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = appName + "%";
                    linkcmd = " And";
                    orderby += ", AppName ASC";
                }

                if (formName != null && formName != "")
                {
                    sqlCmd.CommandText += linkcmd + " FormName Like @FormName";
                    sqlCmd.Parameters.Add("@FormName", SqlDbType.VarChar, 50).Value = formName + "%";
                    linkcmd = " And";
                    orderby += ", FormName ASC";
                }

                if (webSvcName != null && webSvcName != "")
                {
                    sqlCmd.CommandText += linkcmd + " WebSvcName Like @WebSvcName";
                    sqlCmd.Parameters.Add("@WebSvcName", SqlDbType.VarChar, 50).Value = webSvcName + "%";
                    linkcmd = " And";
                    orderby += ", WebSvcName ASC";
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
        public string Write(string row_gid,
                            string username,
                            string compname,
                            string appname,
                            string formname,
                            string websvcname,
                            string websvcver,
                            string clienttime,
                            string methcount,
                            string clientms,
                            string networkms,
                            string serverms)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Insert DGPMetrics (row_gid, UserName, CompName, AppName, FormName, WebSvcName, WebSvcVer, ClientTime, MethodCount, EndToEndMS, NetworkMS, ServerMS)" +
                                    " Values (@row_gid, @UserName, @CompName, @AppName, @FormName, @WebSvcName, @WebSvcVer, @ClientTime, @MethodCount, @EndToEndMS, @NetworkMS, @ServerMS);";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;
                sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
                sqlCmd.Parameters.Add("@CompName", SqlDbType.VarChar, 50).Value = compname;
                sqlCmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = appname;
                sqlCmd.Parameters.Add("@FormName", SqlDbType.VarChar, 50).Value = formname;
                sqlCmd.Parameters.Add("@WebSvcName", SqlDbType.VarChar, 50).Value = websvcname;
                sqlCmd.Parameters.Add("@WebSvcVer", SqlDbType.VarChar, 50).Value = websvcver;
                sqlCmd.Parameters.Add("@ClientTime", SqlDbType.DateTime2).Value = Convert.ToDateTime(clienttime);
                sqlCmd.Parameters.Add("@MethodCount", SqlDbType.Int).Value = Convert.ToInt32(methcount);
                sqlCmd.Parameters.Add("@EndToEndMS", SqlDbType.Float).Value = Convert.ToDouble(clientms);
                sqlCmd.Parameters.Add("@NetworkMS", SqlDbType.Float).Value = Convert.ToDouble(networkms);
                sqlCmd.Parameters.Add("@ServerMS", SqlDbType.Float).Value = Convert.ToDouble(serverms);

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
                sqlCmd.CommandText = "Delete FROM DGPMetrics" +
                                    " WHERE  row_gid = @row_gid;";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }
    }
}
