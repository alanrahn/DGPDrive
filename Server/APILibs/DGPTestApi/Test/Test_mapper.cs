using System;
using System.Collections.Generic;
using System.Configuration;

using ApiUtil;
using ApiUtil.DataClasses;
using SqlSvrUtil;
using SysMetricsDB;


namespace DGPTestAPI.Test
{
    public class Test_mapper
    {
        string _appname;

        public Test_mapper()
        {
            _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
        }

        /// <summary>
        /// test of end-to-end connectivity, API security, request message parsing and response message generation 
        /// </summary>
        public string EchoTest(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string echoval = msgUtil.GetParamValue("EchoValue", methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (echoval == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing EchoValue; ";
                }

                if (reqFields)
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, echoval);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// direct test of the ServerErrLog.LogError and LogException methods
        /// </summary>
        public string LoggingTest(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            string logmsg = "Test of the Test.LoggingTest.base method to execute various levels of EventLogger logging.";

            try
            {
                ServerErrLog.LogError(userinfo.UserName, _appname, methodname, "LoggingTest", "Test of the ServerErrLog.LogError method.");
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, new Exception("Test of the ServerErrLog.LogException method."));

                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, logmsg);
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /// <summary>
        /// test of the web service exception handling and the call to the ServerErrLog.LogException method
        /// </summary>
        public void SvcExceptionTest(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            Exception exc = new Exception("This exception message is a test of the inner exception handling and logging.");
            throw new Exception("This exception message is a test of the web service exception handling and logging.", exc);
        }


        /// <summary>
        /// test of end-to-end connectivity, API security, request message parsing and response message generation 
        /// </summary>
        public string GetDBName(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string resultxml = "";
            MsgUtil msgUtil = new MsgUtil();

            try
            {
                string dblabel = msgUtil.GetParamValue("DBLabel", methparams);

                // test for required input parameters
                bool reqFields = true;
                string reqFieldNames = "";

                if (dblabel == "")
                {
                    reqFields = false;
                    reqFieldNames += "Missing DBLabel; ";
                }

                if (reqFields)
                {
                    string connstr = ConfigurationManager.AppSettings[dblabel].ToString();
                    DBUtil dBUtil = new DBUtil();
                    string dbName = dBUtil.GetDBName(connstr);

                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.OK, APIData.Text, dbName);
                }
                else
                {
                    resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + ": " + reqFieldNames);
                }
            }
            catch (Exception ex)
            {
                resultxml = msgUtil.CreateXMLResult(methodname, MethReturn.Default, APIResult.Error, APIData.Text, methodname + " : " + ex.Message);
                ServerErrLog.LogException(userinfo.UserName, _appname, methodname, ex);
            }

            return resultxml;
        }

        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */
        /* --------------------------------------------------------------------------------------------- */


    }
}
