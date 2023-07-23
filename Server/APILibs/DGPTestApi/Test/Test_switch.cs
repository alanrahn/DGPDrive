using System;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPTestAPI.Test
{
    public class Test_switch : IMethSwitch
    {
        public Test_switch()
        {
            
        }

        public string CallMethod(UserInfo userinfo, string methodname, Dictionary<string, string> methparams)
        {
            string methXml = "";
            Test_mapper test_Mapper = new Test_mapper();

            switch (methodname)
            {
                case "Test.EchoTest.base":
                    methXml = test_Mapper.EchoTest(userinfo, methodname, methparams);
                    break;

                case "Test.LoggingTest.base":
                    methXml = test_Mapper.LoggingTest(userinfo, methodname, methparams);
                    break;

                case "Test.ExceptionTest.base":
                    test_Mapper.SvcExceptionTest(userinfo, methodname, methparams);
                    break;

                case "Test.GetDBName.base":
                    methXml = test_Mapper.GetDBName(userinfo, methodname, methparams);
                    break;

                default:
                    throw new Exception("Error: Method [ " + methodname + " ] was not found in the Test API.");
            }

            return methXml;
        }
    }
}
