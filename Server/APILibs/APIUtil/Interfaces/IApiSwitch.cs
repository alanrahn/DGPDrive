
using System.Collections.Generic;

using ApiUtil.DataClasses;

namespace ApiUtil
{
    public interface IApiSwitch
    {
        string CallApi(UserInfo userInfo, MethInfo methInfo, Dictionary<string, string> methParams);
    }
}
