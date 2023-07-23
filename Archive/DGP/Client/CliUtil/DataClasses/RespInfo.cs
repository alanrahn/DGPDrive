
using System.Collections.Generic;

namespace CliUtil
{
    public class RespInfo
    {
        public RespInfo()
        {

        }

        public string ReqID { get; set; }
        public string AuthCode { get; set; }
        public string AuthMsg { get; set; }
        public string SvrMS { get; set; }
        public string MethCount { get; set; }
        public string MsgData { get; set; }

        public Dictionary<string, ResInfo> ResList { get; set; }
    }
}
