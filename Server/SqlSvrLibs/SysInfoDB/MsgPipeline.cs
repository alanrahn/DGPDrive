using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

using ApiUtil;
using ApiUtil.DataClasses;
using SysInfoDB.APIUser;
using SysMetricsDB;

namespace SysInfoDB
{
    public class MsgPipeline
    {
        string _connstr;

        public MsgPipeline(string connStr)
        {
            _connstr = connStr;
        }

        public string DecryptReqMsg(string encReqMsg)
        {
            string reqMsg = "";



            return reqMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReadReqMsg(ReqInfo reqInfo,
                                ref UserInfo userInfo,
                                HttpContext httpctx,
                                IApiSwitch apiSwitch,
                                string ttlCheckFlag,
                                int ttlSec,
                                string userCacheFlag,
                                string rateLimitFlag,
                                int maxMethBatch,
                                int maxFailedLogin,
                                string svcKey)
        {
            MsgUtil msgutil = new MsgUtil();
            string resultMsg = string.Empty;

            // outer try/catch for errors with the API request message or message pipeline functionality
            try
            {
                // check size of method batch
                if (reqInfo.MethodList.Count > 0 && reqInfo.MethodList.Count <= maxMethBatch)
                {
                    // (optional) check TTL
                    bool ttlOK = true;
                    if (ttlCheckFlag == APISetting.ON)
                    {
                        long interval = Convert.ToInt64(ttlSec);
                        long reqTime = Convert.ToInt64(reqInfo.Time);
                        ttlOK = msgutil.CheckMsgTTL(reqTime, interval);
                    }

                    if (ttlOK)
                    {
                        // (optional) get userinfo from cache
                        if (userCacheFlag == APISetting.ON)
                        {
                            if (httpctx.Cache[reqInfo.UserName] != null)
                            {
                                // get user info from cache
                                userInfo = (UserInfo)httpctx.Cache[reqInfo.UserName];
                            }
                            else
                            {
                                // get user info from database (first time or cache expired)
                                UserProc userproc = new UserProc(_connstr);
                                userInfo = userproc.GetUser(reqInfo.UserName, svcKey);

                                // store userinfo in cache if no problems with query, account is enabled and not expired
                                if (userInfo.AcctState == AcctState.Enabled && userInfo.AuthState == AuthState.OK)
                                {
                                    // insert userinfo into cache if account is authenticated and enabled
                                    int cacheseconds = Convert.ToInt32(ConfigurationManager.AppSettings["UserCacheSec"].ToString());
                                    httpctx.Cache.Insert(reqInfo.UserName, userInfo, null, DateTime.UtcNow.AddSeconds(cacheseconds), System.Web.Caching.Cache.NoSlidingExpiration);
                                }
                            }
                        }
                        else
                        {
                            // get user info from database
                            UserProc userproc = new UserProc(_connstr);
                            userInfo = userproc.GetUser(reqInfo.UserName, svcKey);
                        }

                        if (userInfo.AcctState == AcctState.Enabled)
                        {
                            // authentication state check allows limited API method access if expired
                            if (userInfo.AuthState == AuthState.OK || userInfo.AuthState == AuthState.Expired)
                            {
                                bool rateLimitOK = true;

                                // check for method call rate limit enforcement (MethodLimit of 0 disables rate check per account)
                                if (rateLimitFlag == APISetting.ON && userInfo.MethodLimit != 0)
                                {
                                    int methCount = 0;
                                    // get normal method count for user (tracked per minute)
                                    string userMethCount = reqInfo.UserName + "MethCount";
                                    if (httpctx.Cache[userMethCount] != null)
                                    {
                                        methCount = Convert.ToInt32(httpctx.Cache[userMethCount]);
                                    }
                                    else
                                    {
                                        httpctx.Cache.Insert(userMethCount, methCount, null, DateTime.UtcNow.AddSeconds(60), System.Web.Caching.Cache.NoSlidingExpiration);
                                    }

                                    methCount++;
                                    httpctx.Cache[userMethCount] = methCount;

                                    if (methCount > userInfo.MethodLimit) rateLimitOK = false;
                                }

                                if (rateLimitOK)
                                {
                                    // authenticate request message using HMAC hash
                                    EncryptUtil encryptUtil = new EncryptUtil();
                                    bool reqmsgauth = encryptUtil.ValidateHMACHash(userInfo.Password, reqInfo.Time, reqInfo.HMACHash);

                                    if (reqmsgauth)
                                    {
                                        // method batch loop
                                        for (int i = 0; i < reqInfo.MethodList.Count; i++)
                                        {
                                            string resultlistxml = string.Empty;

                                            MethInfo methInfo = new MethInfo();
                                            Dictionary<string, string> paramList = msgutil.ReadMethodParams(methInfo, reqInfo.MethodList[i]);

                                            // expired account restricted to only call login and password reset methods
                                            msgutil.CheckAuthorizaiton(userInfo, methInfo);

                                            if (methInfo.Authorized == true)
                                            {
                                                // inner try/catch for each API method call in the batch loop
                                                try
                                                {
                                                    resultlistxml += apiSwitch.CallApi(userInfo, methInfo, paramList);
                                                }
                                                catch (Exception exc)
                                                {
                                                    ServerErrLog.LogException(userInfo.UserName, "DGPWebSvc", "MsgPipeline.ReadReqMsg.inner", exc);
                                                    resultlistxml += msgutil.CreateXMLResult(methInfo.FullName, MethReturn.Default, APIResult.Exception, APIData.Text, "INNER: " + exc.Message);
                                                }
                                            }
                                            else
                                            {
                                                resultlistxml += msgutil.CreateXMLResult(methInfo.FullName, MethReturn.Default, APIResult.Error, APIData.Text, "The account is not authorized to call method [" + methInfo.FullName + "]");
                                            }

                                            resultMsg += resultlistxml;
                                        }
                                    }
                                    else
                                    {
                                        // request message HMAC hash failed
                                        userInfo.AuthState = AuthState.Error;
                                        FailedAuth(reqInfo, ref userInfo, httpctx, maxFailedLogin);
                                        resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "HMAC hash decryption of the request message failed.");
                                    }
                                }
                                else
                                {
                                    // method count per minute for account exceeded
                                    userInfo.AuthState = AuthState.Exceeded;
                                    resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The limit for maximum methods called per minute has been exceeded.");
                                }
                            }
                            else
                            {
                                // problem with UserInfo state
                                FailedAuth(reqInfo, ref userInfo, httpctx, maxFailedLogin);
                                resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "Account State: " + userInfo.AcctState + " + Authorization: " + userInfo.AuthState);
                            }
                        }
                        else
                        {
                            // no account found
                            resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The user account " + reqInfo.UserName + " was not found.");
                        }
                    }
                    else
                    {
                        // request message TTL expired
                        userInfo.AuthState = AuthState.Timeout;
                        resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The message TTL has expired.");
                    }
                }
                else
                {
                    // problem with request message method calls
                    if (reqInfo.MethodList.Count <= 0)
                    {
                        userInfo.AuthState = AuthState.Missing;
                        resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "No methods were sent in the request message.");
                    }
                    else
                    {
                        userInfo.AuthState = AuthState.Exceeded;
                        resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The number of methods sent in the request exceeded the max batch size allowed.");
                    }
                }
            }
            catch (Exception ex)
            {
                ServerErrLog.LogException(userInfo.UserName, "DGPWebSvc", "MsgPipeline.ReadReqMsg.outer", ex);
                userInfo.AuthState = AuthState.Exception;
                resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Exception, APIData.Text, "OUTER: " + ex.Message);
            }

            return resultMsg;
        }


        private void FailedAuth(ReqInfo reqInfo, ref UserInfo userInfo, HttpContext httpctx, int maxlogin)
        {
            // request message HMAC hash failed
            if (userInfo != null && userInfo.UserName != "")
            {
                // clear userinfo cache
                httpctx.Cache.Remove(reqInfo.UserName);

                string userFailCount = reqInfo.UserName + "FailCount";
                int failCount = 0;
                if (httpctx.Application[userFailCount] != null)
                {
                    failCount = Convert.ToInt32(httpctx.Application[userFailCount]);
                }
                failCount++;
                httpctx.Application[userFailCount] = failCount;

                // check fail count vs fail limit
                if (failCount > maxlogin)
                {
                    // disable account
                    UserProc userProc = new UserProc(_connstr);
                    string tmp = userProc.DisableAccount(userInfo.UserGID);
                    ServerErrLog.LogError(userInfo.UserName, "DGPWebSvc", "MsgPipeline.FailedAuth", "Exceeded max failed authentication count.", "User account " + userInfo.UserName + " was disabled for failed authentication count: " + tmp);
                }
            }
        }

    }
}
