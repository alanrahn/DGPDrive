
using ApiUtil;

namespace ApiUtil.DataClasses
{
    public class EncReqMsg
    {
        public EncReqMsg()
        {
            
        }

        public string HttpMsg { get;  set; }
        public string KeyPair { get; set; }
        public string SymmKey { get; set; }
        public string ReqMsg { get; set; }
        public string SegData { get; set; }

        public void ParseEncReqMsg()
        {
            SymmKey = "";
            ReqMsg = "";
            SegData = "";

            // parse the encrypted HTTP request
            string[] msgarray = HttpMsg.Split('|');

            //assign segdata if present
            if (msgarray.Length >= 2)
            {
                if (msgarray.Length == 3)
                {
                    SegData = msgarray[2];
                }

                // use private key pair to decrypt the symmetric key
                EncryptUtil encryptUtil = new EncryptUtil();
                SymmKey = encryptUtil.RSADecryptKey(msgarray[0], KeyPair);

                // use symmetric key to decrypt API request message
                ReqMsg = encryptUtil.DecryptString(msgarray[1], SymmKey);
            }
        }
    }
}
