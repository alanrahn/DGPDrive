

using System.Collections.Generic;

namespace ApiUtil.DataClasses
{
    public class TestMsgInfo
    {
        public TestMsgInfo()
        {

        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string MethName { get; set; }
        public string Descrip { get; set; }
        public string ExpAuthCode { get; set; }
        public string MsgType { get; set; }
        public string MethXML { get; set; }

        public List<TestResInfo> ResultList { get; set; }
    }
}
