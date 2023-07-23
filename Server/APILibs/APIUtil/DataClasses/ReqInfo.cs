
using System.Collections.Generic;

namespace ApiUtil.DataClasses
{
    public class ReqInfo
    {
        public ReqInfo()
        {

        }

        public string ClientIP { get; set; }
        public string UserName { get; set; }
        public string ID { get; set; }
        public string Time { get; set; }
        public string HMACHash { get; set; }

        public List<string> MethodList { get; set; }
    }
}
