using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using ApiUtil;

namespace DGPSetup
{
    public class SOAPMsgUtil
    {
        /// <summary>
        /// (version which instantiates a new WebClient for each request)
        /// </summary>
        public string SOAPApiHelper(string methlist,
                                    string reqid,
                                    string username,
                                    string password,
                                    string url,
                                    string pubkey)
        {
            string encReqMsg = "";
            bool encryptedMsg = false;
            string symmKey = "";
            string encSymmKey = "";
            string respMsg;
            string reqMsg = CreateXMLRequest(username, password, reqid, methlist);

            EncryptUtil encutil = new EncryptUtil();
            DGPSOAPSvc.DGPSOAPSvc _soapsvc = new DGPSOAPSvc.DGPSOAPSvc();
            _soapsvc.Url = url;

            string msgencryption = ConfigurationManager.AppSettings["MsgEncryption"].ToString();

            // check for TLS encryption
            if (msgencryption == "ON")
            {
                encryptedMsg = true;
            }
            else if (msgencryption == "AUTO")
            {
                if (url.Substring(0, 5).ToLower() != "https")
                {
                    encryptedMsg = true;
                }
            }

            if (encryptedMsg)
            {
                // generate symmetric key value
                MsgUtil msgutil = new MsgUtil();
                symmKey = msgutil.GetNewGID();

                // encrypt reqMsg with symmkey
                encReqMsg = encutil.EncryptString(reqMsg, symmKey);

                // encrypt symmkey with web service public key
                encSymmKey = encutil.RSAEncryptKey(symmKey, pubkey);
            }
            else
            {
                encReqMsg = reqMsg;
            }

            // call SOAP controller
            string encrespmsg = _soapsvc.SvcCntrl(encSymmKey, encReqMsg);

            if (encryptedMsg)
            {
                // decrypt response
                respMsg = encutil.DecryptString(encrespmsg, symmKey);
            }
            else
            {
                respMsg = encrespmsg;
            }

            return respMsg;
        }

        /// <summary>
        /// (version which instantiates a new WebClient for each request)
        /// </summary>
        public string OneWayHelper(string methlist,
                                    string reqid,
                                    string username,
                                    string password,
                                    string url,
                                    string pubkey)
        {
            string encReqMsg = "";
            string symmKey = "";
            string encSymmKey = "";
            string reqMsg = CreateXMLRequest(username, password, reqid, methlist);

            EncryptUtil encutil = new EncryptUtil();
            DGPSOAPSvc.DGPSOAPSvc _soapsvc = new DGPSOAPSvc.DGPSOAPSvc();
            _soapsvc.Url = url;

            // check for TLS encryption
            if (url.Substring(0, 5).ToLower() != "https")
            {
                // generate symmetric key value
                MsgUtil msgutil = new MsgUtil();
                symmKey = msgutil.GetNewGID();

                // encrypt reqMsg with symmkey
                encReqMsg = encutil.EncryptString(reqMsg, symmKey);

                // encrypt symmkey with web service public key
                encSymmKey = encutil.RSAEncryptKey(symmKey, pubkey);
            }

            // call SOAP controller
            _soapsvc.OneWayCntrl(encSymmKey, encReqMsg);

            return "OK";
        }

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

        public string UnixTimeString()
        {
            DateTimeOffset dto = DateTimeOffset.UtcNow;
            return dto.ToUnixTimeMilliseconds().ToString();
        }

    }
}
