using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace SysInfoDB.APIUser
{
    public class APIUser_read_dml
    {
        string _connstr;

        public APIUser_read_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CheckName(string userName)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From APIUsers" +
                                    " Where rec_state = @RecState" +
                                    " And UserName = @UserName;";

                sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName.ToLower();
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string userGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from APIUsers" +
                                    " where rec_gid = @UserGID" +
                                    " and rec_state = @RecState;";

                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);

                if (dtresult.Rows.Count > 0)
                {
                    EncryptUtil encryptUtil = new EncryptUtil();

                    // decrypt password value
                    foreach (DataRow dr in dtresult.Rows)
                    {
                        string encpword = dr["Password"].ToString();
                        string svckeyversion = dr["SvcKeyVersion"].ToString();
                        string svckey = ConfigurationManager.AppSettings[svckeyversion].ToString();

                        string password = encryptUtil.DecryptString(encpword, svckey);
                        dr["Password"] = password;
                    }
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RecoverByID(string userGID, string rowID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select *" +
                                    " from APIUsers" +
                                    " where rec_gid = @UserGID" +
                                    " and row_id = @RowID;";

                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;
                sqlCmd.Parameters.Add("@RowID", SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);

                if (dtresult.Rows.Count > 0)
                {
                    EncryptUtil encryptUtil = new EncryptUtil();

                    // decrypt password value
                    foreach (DataRow dr in dtresult.Rows)
                    {
                        string encpword = dr["Password"].ToString();
                        string svckeyversion = dr["SvcKeyVersion"].ToString();
                        string svckey = ConfigurationManager.AppSettings[svckeyversion].ToString();

                        string password = encryptUtil.DecryptString(encpword, svckey);
                        dr["Password"] = password;
                    }
                }
            }

            return dtresult;
        }

        /// <summary>
        /// This query should be case-insensitive
        /// </summary>
        public DataTable GetByName(string userName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from APIUsers" +
                                    " where rec_state = @RecState" +
                                    " and UserName = @UserName;";

                sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);

                if (dtresult.Rows.Count > 0)
                {
                    EncryptUtil encryptUtil = new EncryptUtil();
                    DataRow user = dtresult.Rows[0];

                    // decrypt password value
                    string encpword = user["Password"].ToString();
                    string svckeyversion = user["SvcKeyVersion"].ToString();
                    string svckey = ConfigurationManager.AppSettings[svckeyversion].ToString();
                    string password = encryptUtil.DecryptString(encpword, svckey);
                    user["Password"] = password;
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetHistory(string userGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "select *" +
                                    " from APIUsers" +
                                    " where rec_gid = @UserGID" +
                                    " order by row_id ASC;";

                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);

                if (dtresult.Rows.Count > 0)
                {
                    EncryptUtil encryptUtil = new EncryptUtil();

                    // decrypt password value
                    foreach (DataRow dr in dtresult.Rows)
                    {
                        string encpword = dr["Password"].ToString();
                        string svckeyversion = dr["SvcKeyVersion"].ToString();
                        string svckey = ConfigurationManager.AppSettings[svckeyversion].ToString();

                        string password = encryptUtil.DecryptString(encpword, svckey);
                        dr["Password"] = password;
                    }
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCount(string userName, string lastName, string firstName, string recState)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select count(*) AS Count" +
                                    " From APIUsers" +
                                    " Where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (userName != null && userName != "")
                {
                    sqlCmd.CommandText += " And UserName Like @UserName";
                    sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName + "%";
                }

                if (lastName != null && lastName != "")
                {
                    sqlCmd.CommandText += " And LastName Like @LastName";
                    sqlCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName + "%";
                }

                if (firstName != null && firstName != "")
                {
                    sqlCmd.CommandText += " And FirstName Like @FirstName";
                    sqlCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName + "%";
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
        public DataTable GetSearch(string pageNum, string pageSize, string recState, string sortorder, string userName, string lastName, string firstName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                int _pagesize = Convert.ToInt32(pageSize);
                int _pagenum = Convert.ToInt32(pageNum);
                if (_pagenum < 0) _pagenum = 0;
                int _offset = _pagenum * _pagesize;

                if (sortorder != "ASC" && sortorder != "DESC") sortorder = "ASC";

                sqlCmd.CommandText = "Select LastName, FirstName, MiddleName, UserName, Email, AccountType, AccountState, ExpireDate, MethodLimit, SvcKeyVersion, rec_gid, row_id" +
                                    " From APIUsers" +
                                    " Where rec_state = @RecState";

                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recState;

                // search filter values are optional
                if (userName != null && userName != "")
                {
                    sqlCmd.CommandText += " And UserName Like @UserName";
                    sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName + "%";
                }

                if (lastName != null && lastName != "")
                {
                    sqlCmd.CommandText += " And LastName Like @LastName";
                    sqlCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName + "%";
                }

                if (firstName != null && firstName != "")
                {
                    sqlCmd.CommandText += " And FirstName Like @FirstName";
                    sqlCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName + "%";
                }

                sqlCmd.CommandText += " Order By LastName " + sortorder + ", FirstName " + sortorder + ", UserName " + sortorder;

                sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                    " Fetch Next " + pageSize + " Rows Only;";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the APIUsers table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("APIUsers", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "APIUsers_read_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select UserName, Count(*)" +
                                    " From APIUsers" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By UserName" +
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

                    ServerErrLog.LogError(userName, webSvcName, "APIUsers_read_dml.DupCheck", "Duplicate active values", duplist);
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

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "APIUsers", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "APIUsers", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, "APIUsers", "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, "APIUsers", maxdestid, _connstr);

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
            return dBUtil.GetSrcRecs(srcdbname, "APIUsers", placeholderid, srcbatch, _connstr);
        }

    }
}
