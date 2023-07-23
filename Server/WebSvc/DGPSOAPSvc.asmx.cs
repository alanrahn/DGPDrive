using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using System.Configuration;
using System.Diagnostics;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB;
using SysMetricsDB;
using System.IO;

namespace DGPWebSvc
{
    /// <summary>
    /// Summary description for DGPSOAPSvc
    /// </summary>
    [WebService(Namespace = "http://DGPDrive.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class DGPSOAPSvc : WebService
    {

        [WebMethod(Description = "Returns the RSA public key for the web service.")]
        public string GetPubKey()
        {
            string pubkey = "";

            try
            {
                // get the path to the rsa key XML files
                string publickeypath = Server.MapPath(@"App_Data\rsapubkey.xml");

                if (File.Exists(publickeypath))
                {
                    using (TextReader tr = new StreamReader(publickeypath))
                    {
                        pubkey = tr.ReadToEnd();
                    }
                }
                else
                {
                    throw new Exception("Unable to access the public key file.");
                }
            }
            catch (Exception ex)
            {
                string _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
                ServerErrLog.LogException("NA", _appname, "DGPSOAPSvc.GetPubKey", ex);
            }

            return pubkey;
        }

        [WebMethod (Description = "A SOAP web service synchronous front controller, with optional message encryption.")]
        public string SvcCntrl(string encSymmKey, string ReqMsg)
        {
            Stopwatch servertime = new Stopwatch();
            servertime.Start();

            bool encrypted = false;

            string _clrSymmKey = "";
            string _clrReqMsg = "";
            string _clrRespMsg = "";
            string _encRespMsg = "";

            MsgUtil msgutil = new MsgUtil();
            ReqInfo reqInfo = new ReqInfo();
            UserInfo userInfo = new UserInfo();

            string _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();
            long maxMsgSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxMsgSize"].ToString());

            try
            {
                if (ConfigurationManager.AppSettings["LocState"].ToString() == LocState.Primary || ConfigurationManager.AppSettings["LocState"].ToString() == LocState.Backup)
                {
                    // check to see if required inputs exist (encsymmkey, reqmsg)
                    if (ReqMsg != null && ReqMsg != "")
                    {
                        if (encSymmKey != null && encSymmKey != "")
                        {
                            encrypted = true;

                            // decrypt symKey with RSA private key
                            _clrSymmKey = DecryptSymKey(encSymmKey);

                            // decrypt the request message using the symmkey and save to var
                            _clrReqMsg = DecryptReqMsg(_clrSymmKey, ReqMsg);
                        }
                        else
                        {
                            // no encryption key, assume ReqMsg is cleartext
                            _clrReqMsg = ReqMsg;
                        }

                        // check length of request message input (default max size of SOAP request is 4 MB - DGP max is 80K)
                        if (_clrReqMsg.Length <= maxMsgSize)
                        {
                            // get configuration settings values
                            string ttlcheckflag = ConfigurationManager.AppSettings["TTLCheckFlag"].ToString();
                            int ttlsec = Convert.ToInt32(ConfigurationManager.AppSettings["TTLMS"]);
                            string usercacheflag = ConfigurationManager.AppSettings["UserCacheFlag"].ToString();
                            string ratelimitflag = ConfigurationManager.AppSettings["RateLimitFlag"].ToString();
                            int maxmethbatch = Convert.ToInt32(ConfigurationManager.AppSettings["MaxMethBatch"]);
                            int maxfailedlogin = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedLogin"]);
                            string sysinfoconnstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
                            string svckeyversion = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();
                            string svckey = ConfigurationManager.AppSettings[svckeyversion].ToString();

                            msgutil.ReadRequestString(reqInfo, _clrReqMsg);

                            MsgPipeline msgpipe = new MsgPipeline(sysinfoconnstr);

                            // process the request message
                            string resultMsg = msgpipe.ReadReqMsg(reqInfo, ref userInfo, HttpContext.Current, new DGPAPISwitch(), ttlcheckflag, ttlsec, usercacheflag, ratelimitflag, maxmethbatch, maxfailedlogin, svckey);
                            _clrRespMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, userInfo.AuthState, ConfigurationManager.AppSettings["LocState"].ToString(), servertime.Elapsed.TotalMilliseconds.ToString(), resultMsg);

                            // test size of response message compared to the max limit
                            if (_clrRespMsg.Length > maxMsgSize)
                            {
                                servertime.Stop();
                                string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The size of the API Response message exceeded the maximum " + maxMsgSize + " bytes");
                                _clrRespMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Exceeded, ConfigurationManager.AppSettings["LocState"].ToString(), servertime.Elapsed.TotalMilliseconds.ToString(), errMsg);
                            }
                        }
                        else
                        {
                            // reqmsg input parameter too large
                            servertime.Stop();
                            string errmsgtxt = "Missing API Request message";
                            if (HttpContext.Current.Request.ContentLength <= maxMsgSize) errmsgtxt = "The size of the API Request message exceeded the maximum " + maxMsgSize + " bytes";
                            string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, errmsgtxt);
                            _clrRespMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Content, errmsgtxt, "0", errMsg);
                        }
                    }
                    else
                    {
                        // empty request message - stop proessing
                        servertime.Stop();
                        string errmsgtxt = "Missing API Request message";
                        if (HttpContext.Current.Request.ContentLength <= maxMsgSize) errmsgtxt = "The size of the API Request message exceeded the maximum " + maxMsgSize + " bytes";
                        string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, errmsgtxt);
                        _clrRespMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Content, errmsgtxt, "0", errMsg);
                    }
                }
                else
                {
                    servertime.Stop();
                    string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The web service is currently offline.");
                    _clrRespMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, LocState.Offline, "The web service is currently offline.", "0", errMsg);
                }
            }
            catch (Exception ex)
            {
                servertime.Stop();
                ServerErrLog.LogException("DGPCntrl", _appname, "DGPCntrl.Page_Load", ex);
                string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Exception, APIData.Text, ex.Message);
                _clrRespMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Error, "DGPCtrl.Page_Load exception." + ex.Message, "0", errMsg);
            }

            // if encryption is used, encrypt the response message using the symmkey (otherwise do nothing)
            if (encrypted)
            {
                _encRespMsg = EncryptRespMsg(_clrSymmKey, _clrRespMsg);
            }
            else
            {
                _encRespMsg = _clrRespMsg;
            }

            // return the response message
            return (_encRespMsg);
        }

        [SoapDocumentMethod(OneWay = true)]
        [WebMethod (Description = "SOAP one-way front controller to start the execution of AutoWork fire-and-forget process methods, with optional encryption.")]
        public void OneWayCntrl(string encSymmKey, string ReqMsg)
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
                if (ConfigurationManager.AppSettings["LocState"].ToString() == LocState.Primary)
                {
                    // check to see if required inputs exist (encsymmkey, reqmsg)


                    // if encsymmkey not empty, decyrpt the symmkey using the private key and save to var


                        // decrypt the request message using the symmkey and save to var


                    // process the request message


                    // check request message content type (no file uploads allowed)
                    if ((HttpContext.Current.Request.ContentType == "text/xml" || HttpContext.Current.Request.ContentType == "application/soap+xml") && HttpContext.Current.Request.Files.Count == 0)
                    {
                        if (HttpContext.Current.Request.ContentLength > 0 && HttpContext.Current.Request.ContentLength <= maxMsgSize)
                        {
                            // get configuration settings values
                            string ttlcheckflag = ConfigurationManager.AppSettings["TTLCheckFlag"].ToString();
                            int ttlsec = Convert.ToInt32(ConfigurationManager.AppSettings["TTLMS"]);
                            string usercacheflag = ConfigurationManager.AppSettings["UserCacheFlag"].ToString();
                            string ratelimitflag = ConfigurationManager.AppSettings["RateLimitFlag"].ToString();
                            int maxmethbatch = Convert.ToInt32(ConfigurationManager.AppSettings["MaxMethBatch"]);
                            int maxfailedlogin = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedLogin"]);
                            string sysinfoconnstr = ConfigurationManager.AppSettings["SysInfo"].ToString();
                            string svckeyversion = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();
                            string svckey = ConfigurationManager.AppSettings[svckeyversion].ToString();

                            //msgutil.ReadRequestStream(reqInfo, Request.InputStream);

                            MsgPipeline msgpipe = new MsgPipeline(sysinfoconnstr);

                            string resultMsg = msgpipe.ReadReqMsg(reqInfo, ref userInfo, HttpContext.Current, new DGPAPISwitch(), ttlcheckflag, ttlsec, usercacheflag, ratelimitflag, maxmethbatch, maxfailedlogin, svckey);
                            respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, userInfo.AuthState, ConfigurationManager.AppSettings["LocState"].ToString(), servertime.Elapsed.TotalMilliseconds.ToString(), resultMsg);

                            // test size of response message compared to the max limit
                            if (respMsg.Length > maxMsgSize)
                            {
                                servertime.Stop();
                                string errMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The size of the API Response message exceeded the maximum " + maxMsgSize + " bytes");
                                respMsg = msgutil.CreateXMLResponse(reqInfo.UserName, reqInfo.ID, AuthState.Exceeded, ConfigurationManager.AppSettings["LocState"].ToString(), servertime.Elapsed.TotalMilliseconds.ToString(), errMsg);
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
                // update the work queue record with the latest state values and release it for the next iteration...

                // log details of the iteration to the appropriate log file

            }

        }

        /* ------------------------------------------------------------------------------------- */
        /* ------------------------------------------------------------------------------------- */
        /* ------------------------------------------------------------------------------------- */
        /* ------------------------------------------------------------------------------------- */
        /* ------------------------------------------------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        protected string DecryptSymKey(string encKeyText)
        {
            string symKey = "";
            string rsaKeyPair = "";

            // get the path to the rsa key XML files
            string privatekeypath = Server.MapPath(@"App_Data\rsakeypair.xml");

            // read the RSA xml key files
            if (File.Exists(privatekeypath))
            {
                using (TextReader tr = new StreamReader(privatekeypath))
                {
                    rsaKeyPair = tr.ReadToEnd();
                }
            }
            else
            {
                throw new Exception("Unable to access key pair.");
            }

            EncryptUtil encutil = new EncryptUtil();
            if (rsaKeyPair.Length > 0)
            {
                symKey = encutil.RSADecryptKey(encKeyText, rsaKeyPair);
            }
            else
            {
                throw new Exception("Unable to read key pair.");
            }
            return symKey;
        }

        /// <summary>
        /// 
        /// </summary>
        protected string DecryptReqMsg(string symKey, string encReqMsg)
        {
            EncryptUtil encutil = new EncryptUtil();
            return encutil.DecryptString(encReqMsg, symKey);
        }

        /// <summary>
        /// 
        /// </summary>
        protected string EncryptRespMsg(string symKey, string clrRespMsg)
        {
            EncryptUtil encutil = new EncryptUtil();
            return encutil.EncryptString(clrRespMsg, symKey);
        }
    }
}


