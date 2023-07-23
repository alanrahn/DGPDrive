
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using CmnUtil;

namespace SvrUtil
{
    public class SvrErrLog
    {
        public SvrErrLog()
        {

        }

        public static void LogInfo(string userName,
                                    string appName,
                                    string className,
                                    string msgName,
                                    string msgData)
        {
            string _compName = Environment.MachineName;
            string _errLevel = "INFO";

            // write the error to the Event Viewer
            WriteErrToEV(userName, _compName, appName, className, "SERVER", _errLevel, msgName, msgData);

            // write error directly to the database
            WriteErrToDB(userName,
                         _compName,
                         appName,
                         className,
                         _errLevel,
                         msgName,
                         msgData);
        }

        /// <summary>
        /// Store error information to a database table directly
        /// </summary>
        public static void LogError(string userName,
                                        string appName,
                                        string className,
                                        string errMessage,
                                        string errData)
        {
            string _compName = Environment.MachineName;
            string _errLevel = "ERROR";

            // write the error to the Event Viewer
            WriteErrToEV(userName, _compName, appName, className, "SERVER", _errLevel, errMessage, errData);

            // write error directly to the database
            WriteErrToDB(userName,
                         _compName,
                         appName,
                         className,
                         _errLevel,
                         errMessage,
                         errData);
        }



        /// <summary>
        /// Store exception information to a database table directly
        /// </summary>
        public static void LogException(string userName,
                                            string appName,
                                            string className,
                                            Exception ex)
        {
            if (ex != null)
            {
                string _errData = "";
                string _compName = Environment.MachineName;
                string _errLevel = "EXCEPTION";
                string _errMessage = ex.Message;
                if (ex.StackTrace != null && ex.StackTrace != "")
                {
                    _errData = ex.StackTrace;
                }
                else
                {
                    _errData = Environment.StackTrace;
                }

                // write the exception to the Event Viewer
                WriteErrToEV(userName, _compName, appName, className, "SERVER", _errLevel, _errMessage, _errData);

                // write exception directly to the database
                WriteErrToDB(userName,
                             _compName,
                             appName,
                             className,
                             _errLevel,
                             _errMessage,
                             _errData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void WriteErrToDB(string userName,
                                            string compName,
                                            string appName,
                                            string className,
                                            string errLevel,
                                            string errMessage,
                                            string errData)
        {
            try
            {
                string _connstr = ConfigurationManager.AppSettings["SysMetrics"].ToString();

                // trim errData value to fit the max size of the table column
                string trimData;
                if (errData != null && errData.Length > 5000)
                {
                    trimData = errData.Substring(0, 5000);
                }
                else
                {
                    trimData = errData;
                }

                string newgid = GenUtil.GetNewGUID();
                string resultstr = "";

                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "Insert DGPErrors (row_gid, UserName, CompName, AppName, ClassName, ErrLoc, ErrLevel, ErrMessage, ErrData)" +
                                        " Values (@row_gid, @UserName, @CompName, @AppName, @ClassName, @ErrLoc, @ErrLevel, @ErrMessage, @ErrData);";

                    sqlCmd.Parameters.Add("@row_gid", SqlDbType.VarChar, 50).Value = newgid;
                    sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName;
                    sqlCmd.Parameters.Add("@CompName", SqlDbType.VarChar, 50).Value = compName;
                    sqlCmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = appName;
                    sqlCmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 50).Value = className;
                    sqlCmd.Parameters.Add("@ErrLoc", SqlDbType.VarChar, 10).Value = LocState.Server;
                    sqlCmd.Parameters.Add("@ErrLevel", SqlDbType.VarChar, 10).Value = errLevel;
                    sqlCmd.Parameters.Add("@ErrMessage", SqlDbType.VarChar, 250).Value = errMessage;

                    if (trimData == null || trimData.Length <= 0) trimData = "";
                    sqlCmd.Parameters.Add("@ErrData", SqlDbType.VarChar, 5000).Value = trimData;

                    int rowsaff = 0;
                    resultstr = MethResult.Error;

                    using (SqlConnection sqlConn = new SqlConnection(_connstr))
                    {
                        sqlConn.Open();

                        using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
                        {

                            sqlCmd.Connection = sqlConn;
                            sqlCmd.Transaction = sqlTran;

                            rowsaff = sqlCmd.ExecuteNonQuery();

                            if (rowsaff > 0)
                            {
                                sqlTran.Commit();
                                resultstr = MethResult.OK;
                            }
                            else
                            {
                                sqlTran.Rollback();
                            }
                        }
                    }
                }

                if (resultstr == null || resultstr != MethResult.OK)
                {
                    WriteErrToEV("SYSTEM ACCOUNT", compName, "ServerErrLog", "WriteErrToDB", "SERVER", "ERROR", "Error writing to the DGPErrors database.", resultstr);
                }
            }
            catch (Exception ex)
            {
                WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "ServerErrLog", "WriteErrToDB", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }


        public static void WriteErrToEV(string userName,
                                        string compName,
                                        string appName,
                                        string className,
                                        string errLoc,
                                        string errLevel,
                                        string errMessage,
                                        string errData)
        {
            try
            {
                string _eventSrc = ".NET Runtime";
                if (ConfigurationManager.AppSettings["EventSource"] != null)
                {
                    _eventSrc = ConfigurationManager.AppSettings["EventSource"].ToString();
                }

                int _eventID = 1000;
                if (ConfigurationManager.AppSettings["EventSource"] != null)
                {
                    _eventID = Convert.ToInt32(ConfigurationManager.AppSettings["EventID"].ToString());
                }

                string msg = "DGP Lattice Error" +
                            "\n-----------------" +
                            "\nUserName   : " + userName +
                            "\nCompName   : " + compName +
                            "\nAppName    : " + appName +
                            "\nClassName  : " + className +
                            "\nErrLoc     : " + errLoc +
                            "\nErrLevel   : " + errLevel +
                            "\nErrMessage : " + errMessage +
                            "\nErrData    : " + errData;

                if (errLevel == "EXCEPTION")
                {
                    EventLog.WriteEntry(_eventSrc, msg, EventLogEntryType.Error, _eventID);
                }
                else
                {
                    EventLog.WriteEntry(_eventSrc, msg, EventLogEntryType.Warning, _eventID);
                }
            }
            catch (Exception ex)
            {
                // this catch is just to avoid unhandled exceptions;
                // writing to the Event Viewer must be reliable enough so these exceptions do not occur (verified by testing).
            }
        }

    }
}
