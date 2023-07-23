

namespace SvrUtil
{
    public class AcctInfo
    {
        public AcctInfo()
        {

        }

        public string AcctGID { get; set; }
        public string AcctType { get; set; }
        public string AcctState { get; set; }
        public string AuthState { get; set; }
        public string AcctName { get; set; }
        public string SecToken { get; set; }
        public string MethList { get; set; }
        public string ReadList { get; set; }
        public string WriteList { get; set; }
        public int MethLimit { get; set; }
    }
}
