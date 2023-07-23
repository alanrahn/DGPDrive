
using System.Collections.Generic;

namespace SvrUtil
{
    public interface IAPISwitch
    {
        string CallApi(AcctInfo acctInfo, MethInfo methInfo, Dictionary<string, string> methParams);
    }
}
