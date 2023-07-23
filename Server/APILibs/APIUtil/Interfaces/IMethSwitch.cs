
using System.Collections.Generic;

using ApiUtil.DataClasses;

namespace ApiUtil
{
    public interface IMethSwitch
    {
        string CallMethod(UserInfo userInfo, string apiName, Dictionary<string, string> methParams);
    }
}
