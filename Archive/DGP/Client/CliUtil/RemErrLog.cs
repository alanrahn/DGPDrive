
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

using CmnUtil;

namespace CliUtil
{
    public class RemErrLog
    {
        RemErrLog()
        {

        }

        public static void LogInfo(bool encFlag,
                                    string acctName,
                                    string passWord,
                                    string svcURL,
                                    string appName,
                                    string className,
                                    string infoName,
                                    string infoData)
        {
            string _compName = Environment.MachineName;
            string _errLevel = "INFO";

            // write the error to the Event Viewer
            WriteErrToEV(acctName, _compName, appName, className, "CLIENT", _errLevel, infoName, infoData);

            // write error to the database by calling a web service
            CallErrorAPI(encFlag,
                        acctName,
                        passWord,
                        svcURL,
                        _compName,
                        appName,
                        className,
                        _errLevel,
                        infoName,
                        infoData);
        }

        /// <summary>
        /// Store error information to a database table by calling remote web service API method
        /// </summary>
        public static void LogError(bool encFlag,
                                    string acctName,
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
            WriteErrToEV(acctName, _compName, appName, className, "CLIENT", _errLevel, errMessage, errData);

            // write error to the database by calling a web service
            CallErrorAPI(encFlag,
                        acctName,
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
        public static void LogException(bool encFlag,
                                        string acctName,
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
                CallErrorAPI(encFlag,
                            acctName,
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
        private static void CallErrorAPI(bool encFlag,
                                        string acctName,
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
                methparams.Add(DGPAcct.AcctName, acctName);
                methparams.Add(DGPError.CompName, compName);
                methparams.Add(DGPError.AppName, appName);
                methparams.Add(DGPError.ClsName, className);
                methparams.Add(DGPError.CompLoc, "CLIENT");
                methparams.Add(DGPError.ErrLev, errLevel);
                methparams.Add(DGPError.ErrMsg, errMessage);
                methparams.Add(DGPError.ErrData, trimData);

                CliMsgUtil msgUtil = new CliMsgUtil();
                string methlist = msgUtil.CreateXMLMethod("DGPError.New.base", methparams);

                RespInfo methresults = msgUtil.ApiMethHelper(encFlag,
                                                            acctName,
                                                            passWord,
                                                            methlist,
                                                            "",
                                                            svcURL);

                ResInfo methresult = msgUtil.GetResult("DGPError.New.base_DEFAULT", methresults.ResList);

                if (methresult.RCode == null || methresult.RCode != MethResult.OK)
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

                string msg = "Optima ONE Error" +
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
