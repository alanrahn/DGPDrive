
namespace ApiUtil.DataClasses
{
    public class UserInfo
    {
        public UserInfo()
        {

        }

        public string AuthState { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGID { get; set; }
        public string RowDate { get; set; }
        public string AcctType { get; set; }
        public string AcctState { get; set; }
        public string MethList { get; set; }
        public string ReadList { get; set; }
        public string WriteList { get; set; }
        public int MethodLimit { get; set; }
    }
}
