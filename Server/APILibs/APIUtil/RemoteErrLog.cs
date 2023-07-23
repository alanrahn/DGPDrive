using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

using ApiUtil.DataClasses;

namespace ApiUtil
{
    public class RemoteErrLog
    {
        public static void LogInfo(string acctName,
                                    string passWord,
                                    string svcURL,
                                    string appName,
                                    string className,
                                    string msgName,
                                    string msgData)
        {
            string _compName = Environment.MachineName;
            string _errLevel = "INFO";

            // write the error to the Event Viewer
            WriteErrToEV(acctName, _compName, appName, className, "CLIENT", _errLevel, msgName, msgData);

            // write error to the database by calling a web service
            CallErrorAPI(acctName,
                            passWord,
                            svcURL,
                            _compName,
                            appName,
                            className,
                            _errLevel,
                            msgName,
                            msgData);
        }

        /// <summary>
        /// Store error information to a database table by calling remote web service API method
        /// </summary>
        public static void LogError(string acctName,
                                    string passWord,
                                    string svcURL,
                                    string appName,
                                    string className,
                                    string errMessage,
                                    string errData)
        {
            string _errLevel = "ERROR";
            string _compName = Environment.MachineName;

            // write the error to the Event Viewer
            WriteErrToEV(acctName, _compName, appName,  className, "CLIENT", _errLevel, errMessage, errData);

            // write error to the database by calling a web service
            CallErrorAPI(acctName,
                            passWord,
                            svcURL,
                            _compName,
                            appName,
                            className,
                            _errLevel,
                            errMessage,
                            errData);
        }

        /// <summary>
        /// Store exception information to a database table by calling remote web service API method
        /// </summary>
        public static void LogException(string acctName,
                                        string passWord,
                                        string svcURL,
                                        string appName,
                                        string className,
                                        Exception ex)
        {
            if (ex != null)
            {
                string _errLevel = "EXCEPTION";
                string _errMessage = ex.Message;
                string _errData = ex.StackTrace;
                string _compName = Environment.MachineName;

                // write the exception to the Event Viewer
                WriteErrToEV(acctName, _compName, appName, className, "CLIENT", _errLevel, _errMessage, _errData);

                // write exception to the database by calling a web service
                CallErrorAPI(acctName,
                                passWord,
                                svcURL,
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
        private static void CallErrorAPI(string acctName,
                                        string passWord,
                                        string svcURL,
                                        string compName,
                                        string appName,
                                        string className,
                                        string errLevel,
                                        string errMessage,
                                        string errData)
        {
            try
            {
                // trim errData value to fit the max size of the table column
                string trimData;
                if (errData.Length > 5000)
                {
                    trimData = errData.Substring(0, 5000);
                }
                else
                {
                    trimData = errData;
                }

                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(APIUserFields.UserName, acctName);
                methparams.Add(LogFields.CompName, compName);
                methparams.Add(LogFields.AppName, appName);
                methparams.Add(LogFields.ClassName, className);
                methparams.Add(LogFields.MsgLoc, "CLIENT");
                methparams.Add(LogFields.ErrLevel, errLevel);
                methparams.Add(LogFields.ErrMessage, errMessage);
                methparams.Add(LogFields.ErrData, trimData);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methlist = msgUtil.CreateXMLMethod("DGPError.New.base", methparams);

                string respmsg = msgUtil.ApiMethHelper(methlist,
                                                        reqID,
                                                        acctName,
                                                        passWord,
                                                        svcURL);

                RespInfo respinfo = new RespInfo();
                Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                ResInfo methresult = msgUtil.GetResult("DGPError.New.base_DEFAULT", methresults);

                if (methresult.RCode == null || methresult.RCode != APIResult.OK)
                {
                    WriteErrToEV("SYSTEM ACCOUNT", compName, "RemoteErrLog", "CallErrorAPI", "CLIENT", "ERROR", methresult.RCode, methresult.RVal);
                }
            }
            catch (Exception ex)
            {
                WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "RemoteErrLog", "CallErrorAPI", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        public static void WriteErrToEV(string acctName,
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
                if (ConfigurationManager.AppSettings["EventID"] != null)
                {
                    _eventID = Convert.ToInt32(ConfigurationManager.AppSettings["EventID"].ToString());
                }

                string msg = "DGPDrive Error" +
                            "\n-----------------" +
                            "\nAcctName   : " + acctName +
                            "\nCompName   : " + compName +
                            "\nAppName    : " + appName +
                            "\nClassName  : " + className +
                            "\nErrLoc     : " + errLoc +
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
