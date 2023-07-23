using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Text;
using System.Xml;

using CmnUtil;

namespace CliUtil
{
    public class CliMsgUtil
    {
        public CliMsgUtil()
        {

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
        /// used to post request message to a web service;
        /// (version which reuses HttpClient)
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
        /// (instantiates a new WebClient for each request)
        /// </summary>
        public RespInfo ApiMethHelper(bool encflag,
                                    string acctname,
                                    string password,
                                    string methlist,
                                    string msgdata,
                                    string url)
        {
            RespInfo results = new RespInfo();
            string reqid = GenUtil.GetNewGUID();

            string _symmkey = "";
            if (encflag == true) _symmkey = EncUtil.GetNewEncKey();

            string reqMsg = CreateXMLRequest(acctname, password, methlist, msgdata, reqid, encflag.ToString(), _symmkey);
            string encRespMsg = HttpPost(url, reqMsg);

            ReadResponseString(results, encRespMsg, _symmkey);

            return results;
        }

        /// <summary>
        /// (reuses an existing httpclient object)
        /// </summary>
        public RespInfo ApiMethHelper(bool encflag,
                                    string acctname,
                                    string password,
                                    string methlist,
                                    string msgdata,
                                    HttpClient hclient,
                                    string url)
        {
            RespInfo results = new RespInfo();
            string reqid = GenUtil.GetNewGUID();

            string _symmkey = "";
            if (encflag == true) _symmkey = EncUtil.GetNewEncKey();

            string reqMsg = CreateXMLRequest(acctname, password, methlist, msgdata, reqid, encflag.ToString(), _symmkey);
            string encRespMsg = HttpPost(hclient, url, reqMsg);

            ReadResponseString(results, encRespMsg, _symmkey);

            return results;
        }

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //

        // Request Message
        /// ----------------------------------------------------------------------------------------
        // <ReqMsg>
        //   <ReqID></ReqID>                 -- a unique ID value created by the client, round-tripped for asynch method calls
        //   <EncFlag></EncFlag>             -- turns the built in PGP style encryption on and off
        //   <EncKey></EncKey>               -- the [symmetric message encryption key] encrypted with the web service RSA public key
        //   <EncMsg>                        -- the content of the request message encrypted with the symmetric key
        //      <![CDATA[                    -- CDATA blocks only used to encapsulate encrypted messages (cannot nest CDATA blocks)
        //         <ReqTime></ReqTime>       -- the Unix timestamp of the request message used for TTL check
        //         <SecToken></SecToken>     -- the sectoken is a SHA256 Hash of the acctname+password used to query for the account
        //         <MList>                     
        //            <Meth>
        //               <MName></MName>
        //               <PList>
        //                  <Prm>
        //                     <PName></PName>
        //                     <PVal>
        //                        <![CDATA[ ... ]]>
        //                     </PVal>
        //                  </Prm>
        //               </PList>
        //            </Meth>
        //         </MList>
        //      ]]>
        //   </EncMsg>
        //   <MsgData><![CDATA[ ... ]]></MsgData>
        // </ReqMsg>
        // ----------------------------------------------------------------------------------------

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //

        /// <summary>
        /// Builds an XML request message as a single assignment to a string
        /// </summary>
        public string CreateXMLRequest(string acctName, string passWord, string methList, string msgData, string reqID, string encFlag, string symmKey)
        {
            string _pubkey = "";
            string _enckey = "";
            string _encmsg = "";
            string _reqmsg = "";

            if (encFlag == Settings.TRUE)
            {
                _pubkey = ConfigurationManager.AppSettings["PubKey"].ToString();
                _enckey = EncUtil.RSAEncryptKey(symmKey, _pubkey);
            }

            string reqTime = GenUtil.UnixTimeString();
            string secToken = EncUtil.GetSHA256Hash(acctName + passWord);
            string clrMsg = "<SecToken>" + secToken + "</SecToken><ReqTime>" + reqTime + "</ReqTime><MList>" + methList + "</MList>";

            if (encFlag == Settings.TRUE)
            {
                _encmsg = EncUtil.EncryptString(clrMsg, symmKey);
                _reqmsg = "<ReqMsg><ReqID>" + reqID + "</ReqID><EncFlag>" + encFlag + "</EncFlag><EncKey><![CDATA[" + _enckey + "]]></EncKey><EncMsg><![CDATA[" + _encmsg + "]]></EncMsg><MsgData><![CDATA[" + msgData + "]]></MsgData></ReqMsg>";
            }
            else
            {
                _reqmsg = "<ReqMsg><ReqID>" + reqID + "</ReqID><EncFlag>" + encFlag + "</EncFlag><EncKey>" + _enckey + "</EncKey><EncMsg>" + clrMsg + "</EncMsg><MsgData><![CDATA[" + msgData + "]]></MsgData></ReqMsg>";
            }

            return _reqmsg;
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

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //

        // RespMsg XML Format
        // ----------------------------------------------------------------------------------------
        // <RespMsg>
        //    <ReqID></ReqID>                  -- a unique ID value created by the client, round-tripped for asynch method calls
        //    <EncFlag></EncFlag>              -- turns the built in PGP style encryption on and off
        //    <EncMsg>
        //       <![CDATA[                     -- CDATA blocks only used to encapsulate encrypted messages (cannot nest CDATA blocks)
        //          <AuthCode />               -- status of request message authentication (OK, NoMatch, Expired, Disabled, Error, Exception)
        //          <AuthMsg />                -- optional info explaining the authentication status code
        //          <SvrMS />                  -- time spent executing the API method(s) on the server, in MS
        //          <MethCount />              -- number of methods in the APIRequest message batch
        //          <RList>                    -- list of method results returned in the response message
        //             <Result>
        //                <RName />            -- apiname.methodname_resultname
        //                <RCode />            -- result code (OK, Empty, Error, Exception)
        //                <DType />            -- RVal data type(Int, Num, Text, DateTime, XML, JSON, DataTable)
        //                <RVal>
        //                   <![CDATA[ ...method results, error messages, etc... ]>>
        //                </RVal>
        //             </Result>
        //          </RList>
        //       ]]>
        //    </EncMsg>
        //    <MsgData><![CDATA[ ... ]]></MsgData>
        // </RespMsg>
        // ----------------------------------------------------------------------------------------

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //

        /// <summary>
        /// 
        /// </summary>
        public void ReadResponseString(RespInfo respInfo, string respMsgStr, string symmKey)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(respMsgStr);
            MemoryStream stream = new MemoryStream(byteArray);

            ReadResponseStream(respInfo, stream, symmKey);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadResponseStream(RespInfo respData, Stream respMsgStream, string symmKey)
        {
            long maxMsgSize = Convert.ToInt64(ConfigurationManager.AppSettings["MaxMsgSize"]);
            Dictionary<string, ResInfo> reslist = new Dictionary<string, ResInfo>();

            if (respMsgStream != null && respMsgStream.Length > 0)
            {
                // XmlReaderSettings and the use of an XML fragment lock down the reader against potential XML attacks
                XmlReaderSettings readersettings = new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                    MaxCharactersInDocument = maxMsgSize, // includes MaxCharactersFromEntities
                    IgnoreProcessingInstructions = true,
                    DtdProcessing = DtdProcessing.Prohibit,
                    ValidationType = ValidationType.None,
                    IgnoreComments = true,
                    IgnoreWhitespace = true,
                    XmlResolver = null
                };

                try
                {
                    string _encflag;
                    string _encmsg;
                    string _respmsg;

                    // rewind the stream back to the beginning of the content
                    respMsgStream.Seek(0L, SeekOrigin.Begin);

                    // the reader will break if these required elements are missing or out of order; any additional elements must be appended after the required elements
                    using (XmlReader reader = XmlReader.Create(respMsgStream, readersettings))
                    {
                        reader.Read();
                        reader.ReadToFollowing("ReqID");

                        if (reader.LocalName.Equals("ReqID")) respData.ReqID = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing ReqID element.");

                        if (reader.LocalName.Equals("EncFlag")) _encflag = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing EncFlag element.");

                        if (reader.LocalName.Equals("EncMsg")) _encmsg = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing EncMsg element.");

                        if (reader.LocalName.Equals("MsgData")) respData.MsgData = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing MsgData element.");
                    }

                    if (_encflag == Settings.TRUE)
                    {
                        _encmsg = _encmsg.Replace("<![CDATA[", "");
                        _encmsg = _encmsg.Replace("]]>", "");
                        _respmsg = EncUtil.DecryptString(_encmsg, symmKey);
                    }
                    else
                    {
                        _respmsg = _encmsg;
                    }

                    using (XmlReader reader = XmlReader.Create(new StringReader(_respmsg)))
                    {
                        if (reader.LocalName.Equals("AuthCode")) respData.AuthCode = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing AuthCode element.");

                        if (reader.LocalName.Equals("AuthMsg")) respData.AuthMsg = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing AuthMsg element.");

                        if (reader.LocalName.Equals("SvrMS")) respData.SvrMS = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing SvrMS element.");

                        if (reader.LocalName.Equals("MethCount")) respData.MethCount = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Response message missing response MethCount element.");

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
                }
                catch (Exception ex)
                {
                    RemErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "MsgUtil", "ReadResponseStream", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
                }
            }

            respData.ResList = reslist;
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
