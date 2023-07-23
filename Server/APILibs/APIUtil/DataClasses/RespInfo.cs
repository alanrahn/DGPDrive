

namespace ApiUtil.DataClasses
{
    public class RespInfo
    {
        public RespInfo()
        {

        }

        public string ServerIP { get; set; }
        public string UserName { get; set; }
        public string ID { get; set; }
        public string Time { get; set; }
        public string Auth { get; set; }
        public string Info { get; set; }
        public string SvrMS { get; set; }
        public string MethCount { get; set; }
        public string HMACHash { get; set; }
    }
}
