
using System.Collections.Generic;

namespace SvrUtil
{
    public interface ISvcSwitch
    {
        string CallMethod(AcctInfo acctInfo, string apiName, Dictionary<string, string> methParams);
    }
}
