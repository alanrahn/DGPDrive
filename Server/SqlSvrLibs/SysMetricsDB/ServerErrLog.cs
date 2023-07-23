using System;
using System.Configuration;
using System.Diagnostics;

using ApiUtil;

namespace SysMetricsDB
{
    public class ServerErrLog
    {
        public ServerErrLog()
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

                CmnUtil cmnUtil = new CmnUtil();
                string newgid = cmnUtil.GetNewGID();

                DGPErrors_dml dgpErrors_Dml = new DGPErrors_dml(_connstr);

                string resultstr = dgpErrors_Dml.Write(newgid,
                                                        userName,
                                                        compName,
                                                        appName,
                                                        className,
                                                        "SERVER",
                                                        errLevel,
                                                        errMessage,
                                                        trimData);

                if (resultstr == null || resultstr != APIResult.OK)
                {
                    WriteErrToEV("SYSTEM ACCOUNT", compName, "ServerErrLog", "WriteErrToDB", "SERVER", "ERROR", "Error writing to the DGPErrors database.", resultstr);
                }
            }
            catch (Exception ex)
            {
                WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "ServerErrLog", "WriteErrToDB", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }


        private static void WriteErrToEV(string userName,
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

                string msg = "DGPDrive Error" +
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
