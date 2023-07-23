using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Text;
using System.Xml;

using CmnUtil;

namespace SvrUtil
{
    public class SvrMsgUtil
    {
        public SvrMsgUtil()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public bool CheckMsgTTL(long reqTime, long interval)
        {
            bool isOK = false;

            // check request TTL (use of Duration returns absolute value of difference)
            long checkTime = GenUtil.UnixTimeLong();
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
        public void CheckAuthorizaiton(AcctInfo acctInfo, MethInfo methinfo)
        {
            methinfo.Authorized = false;

            if (acctInfo.AuthState == AuthState.Authorized)
            {
                string methlist = acctInfo.MethList;
                string methname = methinfo.FullName;

                if (methlist.IndexOf(methname, 0, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    methinfo.Authorized = true;
                }
            }
            else
            {
                // expired accounts are only allowed to call the login and password reset method
                if (acctInfo.AuthState == AuthState.Expired)
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
            string accessLevel = AccLev.ReadOnly;

            if (writeList.IndexOf(groupGID, 0) != -1)
            {
                accessLevel = AccLev.ReadWrite;
            }

            return accessLevel;
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
        /// builds an xml response message as a single assignment to a string
        /// </summary>
        public string CreateXMLResponse(string authCode, string authInfo, string svrMS, string methCount, string resultList, string msgData, string reqID, string encFlag, string symmKey)
        {
            string _respmsg = "";

            string clrMsg = "<AuthCode>" + authCode + "</AuthCode>" +
                            "<AuthMsg>" + authInfo + "</AuthMsg>" +
                            "<SvrMS>" + svrMS + "</SvrMS>" +
                            "<MethCount>" + methCount + "</MethCount>" +
                            "<RList>" + resultList + "</RList></RespMsg>";

            if (encFlag == Settings.TRUE)
            {
                string _encmsg = EncUtil.EncryptString(clrMsg, symmKey);
                _respmsg = "<RespMsg><ReqID>" + reqID + "</ReqID><EncFlag>" + encFlag + "</EncFlag><EncMsg><![CDATA[" + _encmsg + "]]></EncMsg><MsgData><![CDATA[" + msgData + "]]></MsgData></RespMsg>";
            }
            else
            {
                _respmsg = "<RespMsg><ReqID>" + reqID + "</ReqID><EncFlag>" + encFlag + "</EncFlag><EncMsg>" + clrMsg + "</EncMsg><MsgData><![CDATA[" + msgData + "]]></MsgData></RespMsg>";
            }

            return _respmsg;
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
        /// 
        /// </summary>
        public void ReadRequestString(ReqInfo reqInfo, string reqMsgStr, string pubKey)
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
            if (reqStream != null && reqStream.Length > 0)
            {
                long maxMsgSize = Convert.ToInt64(ConfigurationManager.AppSettings["MaxMsgSize"]);

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
                    string _enckey;
                    string _encmsg;
                    string _reqmsg;

                    // rewind the stream back to the beginning of the content
                    reqStream.Seek(0L, SeekOrigin.Begin);

                    // read the encrypted and optional elements of the request message
                    using (XmlReader reader = XmlReader.Create(reqStream, readersettings))
                    {
                        // read to the first element of the request message
                        reader.Read();
                        reader.ReadToFollowing("ReqID");

                        if (reader.LocalName.Equals("ReqID")) reqInfo.ReqID = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing ReqID element.");

                        // each of the other elements must exist and be in the following order:
                        if (reader.LocalName.Equals("EncFlag")) _encflag = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing EncFlag element.");

                        if (reader.LocalName.Equals("EncKey")) _enckey = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing EncKey element.");

                        if (reader.LocalName.Equals("EncMsg")) _encmsg = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing EncMsg element.");

                        if (reader.LocalName.Equals("MsgData")) reqInfo.MsgData = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing MsgData element.");
                    }

                    if (_encflag == Settings.TRUE)
                    {
                        string _keypair = ConfigurationManager.AppSettings["KeyPair"].ToString();
                        _enckey = _enckey.Replace("<![CDATA[", "");
                        _enckey = _enckey.Replace("]]>", "");
                        reqInfo.SymmKey = EncUtil.RSADecryptKey(_enckey, _keypair);

                        _encmsg = _encmsg.Replace("<![CDATA[", "");
                        _encmsg = _encmsg.Replace("]]>", "");
                        _reqmsg = EncUtil.DecryptString(_encmsg, reqInfo.SymmKey);
                    }
                    else
                    {
                        _reqmsg = _encmsg;
                    }

                    List<string> methlist = new List<string>();

                    // read the decrypted request message
                    using (XmlReader reader = XmlReader.Create(new StringReader(_reqmsg)))
                    {
                        reader.ReadToFollowing("ReqTime");

                        if (reader.LocalName.Equals("ReqTime")) reqInfo.ReqTime = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing ReqTime element.");

                        if (reader.LocalName.Equals("SecToken")) reqInfo.SecToken = reader.ReadElementContentAsString();
                        else throw new InvalidDataException("Request message missing SecToken element.");

                        reader.ReadToFollowing("Meth");
                        while (reader.LocalName.Equals("Meth") && reader.IsStartElement())
                        {
                            methlist.Add(reader.ReadOuterXml());
                        }
                    }

                    reqInfo.MethodList = methlist;
                }
                catch (Exception ex)
                {
                    SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "MsgUtil", "ReadRequestStream", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Reads the method xml fragment as a string
        /// 1) populates the properties of the methinfo object,
        /// 2) returns a generic Dictionary of method param names/value pairs.
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
                        SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "MsgUtil", "ReadMethParams", "SERVER", "EXCEPTION", ex.Message, ex.StackTrace);
                    }
                }
            }

            return methodparams;
        }

        /// <summary>
        /// Returns a string value if the parameter name (key) is found and of the correct data type, otherwise returns an empty string;
        /// </summary>
        public string GetParamValue(string name, string datatype, Dictionary<string, string> pList)
        {
            string paramval = "";

            if (pList != null && pList.Count > 0)
            {
                string pval;
                if (pList.TryGetValue(name, out pval))
                {
                    paramval = pval;
                }

                if (datatype.ToLower() != "string")
                {
                    bool result;
                    // test datatype
                    if (paramval != null && paramval != "")
                    {
                        switch (datatype.ToLower())
                        {
                            case "int":
                                int tmpint;
                                result = int.TryParse(paramval, out tmpint);
                                if (!result) paramval = "";
                                break;

                            case "double":
                                double tmpdouble;
                                result = double.TryParse(paramval, out tmpdouble);
                                if (!result) paramval = "";
                                break;

                            case "datetime":
                                DateTime tmpdatetime;
                                result = DateTime.TryParse(paramval, out tmpdatetime);
                                if (!result) paramval = "";
                                break;
                        }
                    }
                }
            }

            return paramval;
        }
    }
}
