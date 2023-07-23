using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;

namespace SysMetricsDB
{
    public class ServerMetrics_dml
    {
        string _connstr;

        public ServerMetrics_dml(string connStr)
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
                                    " from ServerMetrics" +
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
                sqlCmd.CommandText = "select top 10 *" +
                                    " from ServerMetrics" +
                                    " order by ServerTime DESC;";

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
                            string websvcname,
                            string websvcver,
                            string totalbytes,
                            string gen0bytes,
                            string gen1bytes,
                            string gen2bytes,
                            string lohbytes,
                            string gen0gc,
                            string gen1gc,
                            string gen2gc,
                            string gcpercent)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Insert ServerMetrics (row_gid, UserName, CompName, WebSvcName, WebSvcVer, TotalBytes, Gen0Size, Gen1Size, Gen2Size, LOHSize, Gen0GC, Gen1GC, Gen2GC, GCPercent)" +
                                    " Values (@row_gid, @UserName, @CompName, @WebSvcName, @WebSvcVer, @TotalBytes, @Gen0Size, @Gen1Size, @Gen2Size, @LOHSize, @Gen0GC, @Gen1GC, @Gen2GC, @GCPercent);";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;
                sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
                sqlCmd.Parameters.Add("@CompName", SqlDbType.VarChar, 50).Value = compname;
                sqlCmd.Parameters.Add("@WebSvcName", SqlDbType.VarChar, 50).Value = websvcname;
                sqlCmd.Parameters.Add("@WebSvcVer", SqlDbType.VarChar, 50).Value = websvcver;
                sqlCmd.Parameters.Add("@TotalBytes", SqlDbType.VarChar, 50).Value = totalbytes;
                sqlCmd.Parameters.Add("@Gen0Size", SqlDbType.VarChar, 50).Value = gen0bytes;
                sqlCmd.Parameters.Add("@Gen1Size", SqlDbType.VarChar, 50).Value = gen1bytes;
                sqlCmd.Parameters.Add("@Gen2Size", SqlDbType.VarChar, 50).Value = gen2bytes;
                sqlCmd.Parameters.Add("@LOHSize", SqlDbType.VarChar, 50).Value = lohbytes;
                sqlCmd.Parameters.Add("@Gen0GC", SqlDbType.VarChar, 50).Value = gen0gc;
                sqlCmd.Parameters.Add("@Gen1GC", SqlDbType.VarChar, 50).Value = gen1gc;
                sqlCmd.Parameters.Add("@Gen2GC", SqlDbType.VarChar, 50).Value = gen2gc;
                sqlCmd.Parameters.Add("@GCPercent", SqlDbType.VarChar, 50).Value = gcpercent;

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
                sqlCmd.CommandText = "Delete FROM [SysMetrics].[dbo].[ServerMetrics]" +
                                    " WHERE  row_gid = @row_gid;";

                sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = row_gid;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.Write(sqlCmd, _connstr);
            }

            return strresult;
        }
    }
}
