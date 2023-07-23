

using System.Collections.Generic;

namespace CliUtil
{
    public class TestFileInfo
    {
        public TestFileInfo()
        {

        }

        public string TestName { get; set; }
        public string Descrip { get; set; }
        public string TGID { get; set; }

        public List<string> MethodList { get; set; }
    }
}
