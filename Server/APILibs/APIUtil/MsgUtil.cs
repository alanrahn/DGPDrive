
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;

using ApiUtil.DataClasses;
using System.Threading.Tasks;

namespace ApiUtil
{
    public class MsgUtil
    {
        

        public MsgUtil()
        {

        }

        public string UnixTimeString()
        {
            DateTimeOffset dto = DateTimeOffset.UtcNow;
            return dto.ToUnixTimeMilliseconds().ToString();
        }

        public string UnixTimeString(int days)
        {
            DateTimeOffset dto = DateTimeOffset.UtcNow.AddDays(days);
            return dto.ToUnixTimeMilliseconds().ToString();
        }

        public long UnixTimeLong()
        {
            DateTimeOffset dto = DateTimeOffset.UtcNow;
            return dto.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// calculates the next datetime to run a process given an offset
        /// </summary>
        public long NextUnixTime(string timePeriod, string intervalValue)
        {
            DateTime nextdatetime = DateTime.UtcNow;

            // convert various time periods to milliseconds
            switch (timePeriod.ToUpper())
            {
                case "MS":
                    double milliseconds = Convert.ToDouble(intervalValue);
                    nextdatetime = DateTime.UtcNow.AddMilliseconds(milliseconds);
                    break;

                case "SEC":
                    double seconds = Convert.ToDouble(intervalValue);
                    nextdatetime = DateTime.UtcNow.AddSeconds(seconds);
                    break;

                case "MIN":
                    double minutes = Convert.ToDouble(intervalValue);
                    nextdatetime = DateTime.UtcNow.AddMinutes(minutes);
                    break;

                case "HOUR":
                    double hours = Convert.ToDouble(intervalValue);
                    nextdatetime = DateTime.UtcNow.AddHours(hours);
                    break;

                case "TIMEOFDAY":
                    // intervalvalue = tod minutes of 24 hour clock
                    double dayminutes = Convert.ToDouble(intervalValue);
                    nextdatetime = DateTime.Today.AddDays(1).Date;
                    nextdatetime.AddMinutes(dayminutes);
                    break;

                case "DAYOFWEEK":
                    // intervalvalue = day of week, tod minutes of 24 hour clock
                    string[] weekvalues = intervalValue.Split(',');
                    int weekday = Convert.ToInt32(weekvalues[0]);
                    double weekminutes = Convert.ToDouble(weekvalues[1]);
                    var startofweek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    nextdatetime = startofweek.AddDays(7 + weekday).Date;
                    nextdatetime.AddMinutes(weekminutes);
                    break;

                //case "DAYOFMONTH":
                //    // intervalvalue = day of month, tod minutes of 24 hour clock
                //    string[] monthvalues = intervalValue.Split(',');
                //    int monthday = Convert.ToInt32(monthvalues[0]);
                //    if (monthday > 28) monthday = 28;
                //    double monthminutes = Convert.ToDouble(monthvalues[1]);
                //    var now = DateTime.Today;
                //    var startofmonth = new DateTime(now.Year, now.Month, 1);
                //    nextdatetime = startofmonth.AddMonths(1).Date;
                //    nextdatetime.AddDays(monthday);
                //    nextdatetime.AddMinutes(monthminutes);
                //    break;

            }

            return ((DateTimeOffset)nextdatetime).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// if the previous iteration was limited to the max batch size, schedule the next run immediately,
        /// otherwise schedule then next run using the interval MS
        /// </summary>
        public long GetNextRun(long intervalMS, bool immediate)
        {
            long nextRun = UnixTimeLong();

            if (!immediate)
            {
                nextRun += intervalMS;
            }

            return nextRun;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetNewGID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// used to call services methods with no input parameters;
        /// (instantiates new WebClient for each request)
        /// </summary>
        public string HttpGet(string url)
        {
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }

        /// <summary>
        /// used to call services methods with no input parameters;
        /// (version which reuses HttpClient)
        /// </summary>
        public string HttpGet(HttpClient hclient, string url)
        {
            using (HttpResponseMessage response = hclient.GetAsync(url).Result)
            {
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// used to post request message to a web service synchronously;
        /// (instantiates new WebClient for each request)
        /// </summary>
        public string HttpPost(string url, string reqmsg)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "text/xml";
                return wc.UploadString(url, reqmsg);
            }
        }

        /// <summary>
        /// used to post request message to a web service synchronously;
        /// (reuses existing HttpClient)
        /// </summary>
        public string HttpPost(HttpClient hclient, string url, string reqmsg)
        {
            using (var content = new StringContent(reqmsg))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

                HttpResponseMessage response = hclient.PostAsync(url, content).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// async version of the method used to post request message to a web service
        /// </summary>
        public async Task<string> HttpPostAsync(HttpClient hclient, string url, string reqmsg)
        {
            using (var content = new StringContent(reqmsg))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
                HttpResponseMessage response = await hclient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// (version which instantiates a new WebClient for each request)
        /// </summary>
        public string ApiMethHelper(string methlist,
                                    string reqid,
                                    string username,
                                    string password,
                                    string url)
        {
            string reqMsg = CreateXMLRequest(username, password, reqid, methlist);
            return HttpPost(url, reqMsg);
        }

        /// <summary>
        /// (reuses an existing httpclient object for each request)
        /// </summary>
        public string ApiMethHelper(string methlist,
                                    string reqid,
                                    string username,
                                    string password,
                                    HttpClient hclient,
                                    string url)
        {
            string reqMsg = CreateXMLRequest(username, password, reqid, methlist);
            return HttpPost(hclient, url, reqMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CheckMsgTTL(long reqTime, long interval)
        {
            bool isOK = false;

            // check request TTL (use of Duration returns absolute value of difference)
            long checkTime = UnixTimeLong();
            long ttl = checkTime - reqTime;

            if (ttl <= interval)
            {
                isOK = true;
            }

            return isOK;
        }

        /// <summary>
        /// Test access control whitelist for presence of method name;  allow expired accounts to only call password reset method
        /// </summary>
        public void CheckAuthorizaiton(UserInfo userinfo, MethInfo methinfo)
        {
            methinfo.Authorized = false;

            if (userinfo.AuthState == AuthState.OK)
            {
                string methlist = userinfo.MethList;
                string methname = methinfo.FullName;

                if (methlist.IndexOf(methname, 0, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    methinfo.Authorized = true;
                }
            }
            else
            {
                // expired accounts are only allowed to call the login and password reset method
                if (userinfo.AuthState == AuthState.Expired)
                {
                    if (methinfo.FullName.ToLower() == "userself.login.base" || methinfo.FullName.ToLower() == "userself.changepassword.base")
                    {
                        methinfo.Authorized = true;
                    }
                }
            }
        }

        /// <summary>
        /// assumes user readlist was used as query filter, so only checks groupgid within writelist
        /// </summary>
        public string CheckAccessLevel(string writeList, string groupGID)
        {
            string accessLevel = AccessLevel.ReadOnly;

            if (writeList.IndexOf(groupGID, 0) != -1)
            {
                accessLevel = AccessLevel.ReadWrite;
            }

            return accessLevel;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetClientIPAddress()
        {
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            else
                ip = ip.Split(',')[0];

            return ip;
        }

        //public string EncApiMethHelper(string methlist,
        //                                string reqid,
        //                                string username,
        //                                string password,
        //                                string url,
        //                                string pubkey)
        //{
        //    string respMsg = "";

        //    CmnUtil cmnUtil = new CmnUtil();
        //    string symmKey = cmnUtil.GetNewGID();

        //    string reqMsg = CreateXMLRequest(username, password, reqid, methlist);

        //    EncryptUtil encryptUtil = new EncryptUtil();
        //    string encreqmsg = encryptUtil.EncryptString(reqMsg, symmKey);
        //    string encsymmkey = encryptUtil.RSAEncryptKey(symmKey);
        //    string comboreqmsg = encsymmkey + "|" + encreqmsg;

        //    string encrespmsg = HttpPost(url, comboreqmsg);
        //    if (encrespmsg != "") respMsg = encryptUtil.DecryptString(encrespmsg, symmKey);

        //    return respMsg;
        //}

        //public string EncApiMethHelper(string methlist,
        //                                string reqid,
        //                                string username,
        //                                string password,
        //                                HttpClient hclient,
        //                                string url,
        //                                string pubkey)
        //{
        //    string respMsg = "";

        //    CmnUtil cmnUtil = new CmnUtil();
        //    string symmKey = cmnUtil.GetNewGID();

        //    string reqMsg = CreateXMLRequest(username, password, reqid, methlist);

        //    EncryptUtil encryptUtil = new EncryptUtil();
        //    string encreqmsg = encryptUtil.EncryptString(reqMsg, symmKey);
        //    string encsymmkey = encryptUtil.RSAEncryptKey(symmKey);
        //    string comboreqmsg = encsymmkey + "|" + encreqmsg;

        //    string encrespmsg = HttpPost(hclient, url, comboreqmsg);

        //    if (encrespmsg != "") respMsg = encryptUtil.DecryptString(encrespmsg, symmKey);

        //    return respMsg;
        //}


        //public string SegApiMethHelper(string methlist,
        //                                string reqid,
        //                                string username,
        //                                string password,
        //                                HttpClient hclient,
        //                                string url,
        //                                string pubkey,
        //                                string segdata)
        //{
        //    string respMsg = "";

        //    CmnUtil cmnUtil = new CmnUtil();
        //    string symmKey = cmnUtil.GetNewGID();

        //    string reqMsg = CreateXMLRequest(username, password, reqid, methlist);

        //    EncryptUtil encryptUtil = new EncryptUtil();
        //    string encreqmsg = encryptUtil.EncryptString(reqMsg, symmKey);
        //    string encsymmkey = encryptUtil.RSAEncryptKey(symmKey);
        //    string comboreqmsg = encsymmkey + "|" + encreqmsg + "|" + segdata;

        //    string encrespmsg = HttpPost(hclient, url, comboreqmsg);

        //    if (encrespmsg != "") respMsg = encryptUtil.DecryptString(encrespmsg, symmKey);

        //    return respMsg;
        //}

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //

        // ReqMsg XML Format
        // ----------------------------------------------------------------------------------------
        //  <ReqMsg>
        //      <UserName />                -- DGP account name
        //      <ReqID />                   -- optional unique identifier created at the client for each request message, used for asynch messages
        //      <ReqToken />                -- HMAC hash of Time value using the account password as the secret key
        //      <Time />                    -- UTC Unix-time of request at the client, used for TTL check and HMAC authentication check
        //      <MList>                     -- list of API method calls, each with their own method name and list of method parameters
        //          <Meth>
        //              <MName />
        //              <PList>
        //                  <Prm>
        //                      <Name />
        //                      <Val>
        //                          <![CDATA[ ... ]]>
        //                      </Val>
        //                  </Prm>
        //              </PList>
        //          </Meth>
        //      </MList>
        // </ReqMsg>
        // ----------------------------------------------------------------------------------------


        /// <summary>
        /// Builds an XML request message as a single assignment to a string
        /// </summary>
        public string CreateXMLRequest(string userName, string password, string reqid, string methlist)
        {
            string reqtime = UnixTimeString();
            EncryptUtil encryptUtil = new EncryptUtil();
            string reqtoken = encryptUtil.GetHMACHash(password, reqtime);
            string reqmsg = "<ReqMsg><UserName>" + userName + "</UserName>" +
                            "<ReqID>" + reqid + "</ReqID>" +
                            "<ReqToken>" + reqtoken + "</ReqToken>" +
                            "<Time>" + reqtime + "</Time>" +
                            "<MList>" + methlist + "</MList></ReqMsg>";
            return reqmsg;
        }

        /// <summary>
        /// builds an XML method node to be embedded within a request message (all parameter values enclosed in CDATA blocks)
        /// </summary>
        public string CreateXMLMethod(string methname, Dictionary<string, string> methparams)
        {
            string reqmsg = "<Meth><MName>" + methname + "</MName><PList>";
            foreach (KeyValuePair<string, string> param in methparams)
            {
                reqmsg += "<Prm><Name>" + param.Key + "</Name><Val><![CDATA[" + param.Value + "]]></Val></Prm>";
            }
            reqmsg += "</PList></Meth>";
            return reqmsg;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadRequestString(ReqInfo reqInfo, string reqMsgStr)
        {
            if (reqMsgStr != null && reqMsgStr.Length > 0)
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(reqMsgStr);
                MemoryStream stream = new MemoryStream(byteArray);

                ReadRequestStream(reqInfo, stream);
            }
        }

        /// <summary>
        /// Reads the incoming XML fragment request message as a memory stream passed in from the web service;
        /// populates the ReqInfo object, and also returns the sectoken value
        /// </summary>
        public void ReadRequestStream(ReqInfo reqInfo, Stream reqStream)
        {
            List<string> methlist = new List<string>();

            if (reqStream != null && reqStream.Length > 0)
            {
                // XmlReaderSettings and the use of an XML fragment lock down the reader against potential XML attacks
                XmlReaderSettings readersettings = new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                    //MaxCharactersInDocument = maxMsgSize, // includes MaxCharactersFromEntities
                    IgnoreProcessingInstructions = true,
                    DtdProcessing = DtdProcessing.Prohibit,
                    ValidationType = ValidationType.None,
                    IgnoreComments = true,
                    IgnoreWhitespace = true,
                    XmlResolver = null
                };

                // rewind the stream back to the beginning of the content
                reqStream.Seek(0L, SeekOrigin.Begin);

                // the reader will break if these required elements are missing or out of order; any additional elements must be appended after the required elements
                using (XmlReader reader = XmlReader.Create(reqStream, readersettings))
                {
                    try
                    {
                        reader.Read();
                        reader.ReadToFollowing("UserName");

                        if (reader.LocalName.Equals("UserName")) reqInfo.UserName = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing UserName element.");

                        if (reader.LocalName.Equals("ReqID")) reqInfo.ID = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing ReqID element.");

                        if (reader.LocalName.Equals("ReqToken")) reqInfo.HMACHash = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing SecToken element.");

                        if (reader.LocalName.Equals("Time")) reqInfo.Time = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing Time element.");

                        reader.ReadToFollowing("Meth");
                        while (reader.LocalName.Equals("Meth") && reader.IsStartElement())
                        {
                            methlist.Add(reader.ReadOuterXml());
                        }
                    }
                    catch (Exception ex)
                    {
                        RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "MsgUtil", "ReadRequestStream", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
                    }
                }
            }

            reqInfo.MethodList = methlist;
        }

        /// <summary>
        /// Reads the method xml fragment as a string
        /// - populates the properties of the methinfo object,
        /// - returns a generic Dictionary<> of method param names/value pairs.
        /// </summary>
        public Dictionary<string, string> ReadMethodParams(MethInfo methInfo, string methodStr)
        {
            Dictionary<string, string> methodparams = new Dictionary<string, string>();

            if (methodStr != null && methodStr.Length > 0)
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(methodStr)))
                {
                    try
                    {
                        reader.Read();
                        reader.ReadToFollowing("MName");

                        if (reader.LocalName.Equals("MName"))
                        {
                            methInfo.FullName = reader.ReadElementContentAsString();
                        }
                        else
                        {
                            throw new InvalidDataException("Method XML missing MName element.");
                        }

                        while (reader.Read())
                        {
                            switch (reader.LocalName)
                            {
                                case "Prm":
                                    if (reader.IsStartElement())
                                    {
                                        reader.ReadToFollowing("Name");
                                        string paramname = reader.ReadElementContentAsString();
                                        string paramvalue = "";
                                        if (reader.LocalName.Equals("Val"))
                                        {
                                            // remove CDATA enclosures around each parameter value
                                            paramvalue = reader.ReadElementContentAsString();
                                            paramvalue = paramvalue.Replace("<![CDATA[", "");
                                            paramvalue = paramvalue.Replace("]]>", "");

                                            // prevent duplicate keys
                                            if (!methodparams.ContainsKey(paramname)) methodparams.Add(paramname, paramvalue);
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "MsgUtil", "ReadMethParams", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
                    }
                }
            }

            return methodparams;
        }

        /// <summary>
        /// Returns a string value if the parameter name (key) is found, otherwise returns an empty string;
        /// exceptions are caught by the TryGetValue method itself
        /// </summary>
        public string GetParamValue(string name, Dictionary<string, string> pList)
        {
            string paramval = "";

            if (pList != null && pList.Count > 0)
            {
                string pval;
                if (pList.TryGetValue(name, out pval))
                {
                    paramval = pval;
                }
            }

            return paramval;
        }

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //

        // RespMsg XML Format
        // ----------------------------------------------------------------------------------------
        // <RespMsg>
        //   <UserName />               -- DGP account name, echoed back to the client by the server (for asynch processing)
        //   <ReqID />                  -- optional unique identifier created at the client for each request message, echoed back to the client by the server (for asynch processing)
        //   <Time />                   -- UTC Unix-time the response was sent on the server
        //   <Auth />                   -- status of request message authentication (OK, NoMatch, Expired, Disabled, Error, Exception)
        //   <Info />                   -- optional info explaining the authentication status code
        //   <SvrMS />                  -- time spent executing the API method(s) on the server, in MS
        //   <MethCount />              -- number of methods in the APIRequest message batch
        //   <RList>                    -- list of method results returned in the response message
        //       <Result>
        //           <RName />          -- apiname.methodname.versionname_resultname
        //           <RCode />          -- result code (OK, Empty, Error, Exception)
        //           <DType />          -- RVal data type(Int, Num, Text, DateTime, XML, JSON, DataTable)
        //           <RVal>
        //                <![CDATA[ ...method results, error messages, etc... ]>>
        //           </RVal>
        //       </Result>
        //   </RList>
        // </RespMsg>
        // ----------------------------------------------------------------------------------------

        /// <summary>
        /// builds an xml response message as a single assignment to a string
        /// </summary>
        public string CreateXMLResponse(string userName, string reqID, string authCode, string authInfo, string svrMS, string resultList)
        {
            string respxml = "<RespMsg><UserName>" + userName + "</UserName>" +
                            "<ReqID>" + reqID + "</ReqID>" +
                            "<Time>" + UnixTimeString() + "</Time>" +
                            "<Auth>" + authCode + "</Auth>" +
                            "<Info>" + authInfo + "</Info>" +
                            "<SvrMS>" + svrMS + "</SvrMS>" +
                            "<RList>" + resultList + "</RList></RespMsg>";

            // add notification list when implemented

            return respxml;
        }

        /// <summary>
        /// builds an XML result node to be embedded within a response message (all result values enclosed within CDATA blocks)
        /// </summary>
        public string CreateXMLResult(string fullApiName, string rName, string rCode, string dType, string rVal)
        {
            string resxml = "<Result><RName>" + fullApiName + "_" + rName + "</RName>" +
                            "<RCode>" + rCode + "</RCode>" +
                            "<DType>" + dType + "</DType>" +
                            "<RVal><![CDATA[" + rVal + "]]></RVal></Result>";
            return resxml;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, ResInfo> ReadResponseString(RespInfo respInfo, string respMsgStr)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(respMsgStr);
            MemoryStream stream = new MemoryStream(byteArray);

            return ReadResponseStream(respInfo, stream);
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, ResInfo> ReadResponseStream(RespInfo respInfo, Stream respMsgStream)
        {
            Dictionary<string, ResInfo> reslist = new Dictionary<string, ResInfo>();

            if (respMsgStream != null && respMsgStream.Length > 0)
            {
                // XmlReaderSettings and the use of an XML fragment lock down the reader against potential XML attacks
                XmlReaderSettings readersettings = new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                    //MaxCharactersInDocument = 65536, // includes MaxCharactersFromEntities
                    IgnoreProcessingInstructions = true,
                    DtdProcessing = DtdProcessing.Prohibit,
                    ValidationType = ValidationType.None,
                    IgnoreComments = true,
                    IgnoreWhitespace = true,
                    XmlResolver = null
                };

                // rewind the stream back to the beginning of the content
                respMsgStream.Seek(0L, SeekOrigin.Begin);

                // the reader will break if these required elements are missing or out of order; any additional elements must be appended after the required elements
                using (XmlReader reader = XmlReader.Create(respMsgStream, readersettings))
                {
                    try
                    {
                        reader.ReadToFollowing("UserName");

                        if (reader.LocalName.Equals("UserName")) respInfo.UserName = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing UserName element.");

                        if (reader.LocalName.Equals("ReqID")) respInfo.ID = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing ID element.");

                        if (reader.LocalName.Equals("Time")) respInfo.Time = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing response time element.");

                        if (reader.LocalName.Equals("Auth")) respInfo.Auth = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing auth code element.");

                        if (reader.LocalName.Equals("Info")) respInfo.Info = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing auth info element.");

                        if (reader.LocalName.Equals("SvrMS")) respInfo.SvrMS = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing svrms element.");

                        while (reader.Read())
                        {
                            switch (reader.LocalName)
                            {
                                case "Result":
                                    if (reader.IsStartElement())
                                    {
                                        ResInfo res = new ResInfo();
                                        reader.ReadToFollowing("RName");
                                        if (reader.LocalName.Equals("RName")) res.RName = reader.ReadElementContentAsString();
                                        if (reader.LocalName.Equals("RCode")) res.RCode = reader.ReadElementContentAsString();
                                        if (reader.LocalName.Equals("DType")) res.DType = reader.ReadElementContentAsString();
                                        if (reader.LocalName.Equals("RVal"))
                                        {
                                            string tmp = "";
                                            if (res.DType == "XML" || res.DType == "DataTable")
                                            {
                                                tmp = reader.ReadInnerXml();
                                            }
                                            else
                                            {
                                                tmp = reader.ReadElementContentAsString();
                                            }

                                            // remove CDATA enclosures around each result value
                                            tmp = tmp.Replace("<![CDATA[", "");
                                            tmp = tmp.Replace("]]>", "");
                                            res.RVal = tmp;
                                        }

                                        // avoid adding duplicate results to collection (result names must be unique in a response message)
                                        if (!reslist.ContainsKey(res.RName)) reslist.Add(res.RName, res);
                                    }
                                    break;

                                case "Notice":
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "MsgUtil", "ReadResponseStream", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
                    }
                }
            }

            return reslist;
        }

        /// <summary>
        /// Returns a string value if the result name (key) is found, otherwise returns an empty string;
        /// exceptions are caught by the TryGetValue method itself
        /// </summary>
        public ResInfo GetResult(string name, Dictionary<string, ResInfo> rlist)
        {
            ResInfo result = new ResInfo();

            if (rlist != null && rlist.Count > 0)
            {
                rlist.TryGetValue(name, out result);
            }

            if (result == null) rlist.TryGetValue("METHODERROR_DEFAULT", out result);

            return result;
        }
    }
}
