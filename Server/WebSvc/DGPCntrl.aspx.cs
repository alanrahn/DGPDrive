using System;
using System.Text;
using System.Web;
using System.Configuration;
using System.Diagnostics;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB;
using SysMetricsDB;

namespace DGPWebSvc
{
    public partial class DGPCntrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Stopwatch servertime = new Stopwatch();
            servertime.Start();

            MsgUtil msgutil = new MsgUtil();
            ReqInfo reqInfo = new ReqInfo();
            UserInfo userInfo = new UserInfo();

            string respMsg = string.Empty;
            string _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
            long maxMsgSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxMsgSize"].ToString());

            try
            {
                if (ConfigurationManager.AppSettings["LocState"].ToString() == LocState.Primary || ConfigurationManager.AppSettings["LocState"].ToString() == LocState.Backup)
                {
                    // check request message content type to insure there are no file uploads (no multipart form MIME type, no files in the HTTP request)
                    if (HttpContext.Current.Request.ContentType == "text/xml" && HttpContext.Current.Request.Files.Count == 0)
                    {
                        if (HttpContext.Current.Request.ContentLength > 0 && HttpContext.Current.Request.ContentLength <= maxMsgSize)
                        {
                            // get configuration settings values
                            string locstate = ConfigurationManager.AppSettings["LocState"].ToString();
                            string ttlcheckflag = ConfigurationManager.AppSettings["TTLCheckFlag"].ToString();
                            int ttlsec = Convert.ToInt32(ConfigurationManager.AppSettings["TTLMS"]);
                            string usercacheflag = ConfigurationManager.AppSettings["UserCacheFlag"].ToString();
                            string ratelimitflag = ConfigurationManager.AppSettings["RateLimitFlag"].ToString();
                            int maxmethbatch = Convert.ToInt32(ConfigurationManager.AppSettings["MaxMethBatch"]);
                            int maxfailedlogin = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedLogin"]);
                            string sysinfoconnstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
                            string svckeyversion = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();
                            string svckey = ConfigurationManager.AppSettings[svckeyversion].ToString();

                            msgutil.ReadRequestStream(reqInfo, Request.InputStream);

                            MsgPipeline msgpipe = new MsgPipeline(sysinfoconnstr);

                            string resultMsg = msgpipe.ReadReqMsg(reqInfo, ref userInfo, HttpContext.Current, new DGPAPISwitch(), ttlcheckflag, ttlsec, usercacheflag, ratelimitflag, maxmethbatch, maxfailedlogin, svckey);
                            respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, userInfo.AuthState, locstate, servertime.Elapsed.TotalMilliseconds.ToString(), resultMsg);

                            // test size of response message compared to the max limit
                            if (respMsg.Length > maxMsgSize)
                            {
                                servertime.Stop();
                                string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The size of the API Response message exceeded the maximum " + maxMsgSize + " bytes");
                                respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Exceeded, locstate, servertime.Elapsed.TotalMilliseconds.ToString(), errMsg);
                            }
                        }
                        else
                        {
                            servertime.Stop();
                            string errmsgtxt = "Missing API Request message";
                            if (HttpContext.Current.Request.ContentLength <= maxMsgSize) errmsgtxt = "The size of the API Request message exceeded the maximum " + maxMsgSize + " bytes";
                            string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, errmsgtxt);
                            respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Content, errmsgtxt, "0", errMsg);
                        }
                    }
                    else
                    { 
                        servertime.Stop();
                        string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "Only text/xml mime type is allowed for API Request messages.");
                        respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Content, "Only text/xml mime type is allowed for API Request messages.", "0", errMsg);
                    }
                }
                else
                {
                    servertime.Stop();
                    string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The web service is currently offline.");
                    respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, LocState.Offline, "The web service is currently offline.", "0", errMsg);
                }
            }
            catch (Exception ex)
            {
                servertime.Stop();
                ServerErrLog.LogException("DGPCntrl", _appname, "DGPCntrl.Page_Load", ex);
                string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Exception, APIData.Text, ex.Message);
                respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Error, "DGPCtrl.Page_Load exception." + ex.Message, "0", errMsg);
            }
            finally
            {
                Response.ContentType = "text/xml";
                Response.AppendHeader("Content-Disposition", "filename=respmsg.xml");
                Response.AppendHeader("Content-Length", Encoding.UTF8.GetBytes(respMsg).Length.ToString());
                Response.Charset = "utf-8";
                Response.Write(respMsg);

                try
                {
                    Response.Flush();
                    Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // https://support.microsoft.com/en-us/kb/312629
                }
                catch { }
            }
        }
    }
}