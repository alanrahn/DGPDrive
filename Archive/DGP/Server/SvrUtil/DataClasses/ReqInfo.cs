

using System.Collections.Generic;

namespace SvrUtil
{
    public class ReqInfo
    {
        public ReqInfo()
        {

        }

        public string ReqID { get; set; }
        public string ReqTime { get; set; }
        public string SecToken { get; set; }
        public string MsgData { get; set; }
        public string SymmKey { get; set; }

        public List<string> MethodList { get; set; }
    }
}
