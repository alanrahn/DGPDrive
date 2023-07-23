using System;
using System.Data;

using ApiUtil;

namespace SysMetricsDB.TestTbl1
{
    public class TestTbl1_proc
    {
        string _dbname;
        string _connstr;
        string _usergid;

        public TestTbl1_proc(string dbName, string connStr, string userGID)
        {
            _dbname = dbName;
            _connstr = connStr;
            _usergid = userGID;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CreateReplicaData(int batchSize)
        {
            string procresults = "<ProcResults>";
            TestTbl1_dml testFile_Dml = new TestTbl1_dml(_connstr);
            CmnUtil cmnUtil = new CmnUtil();
            MsgUtil msgUtil = new MsgUtil();

            for (int i = 1; i <= batchSize; i++)
            {
                // create new record
                string recGID = cmnUtil.GetNewGID();
                long row_ms = msgUtil.UnixTimeLong();
                string recName = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + i.ToString();
                string recVal = DateTime.Now.Ticks.ToString();

                string rescode = testFile_Dml.Write(ReplicaAction.Insert, recGID, _usergid, row_ms.ToString(), "", recName, recVal + ":INSERT");
                procresults += "<Result><Iteration>" + i.ToString() + "</Iteration><Action>INSERT</ACTION><RecName>" + recName + "</RecName><Code>" + rescode + "</Code></Result>";

                if (i % 3 == 0)
                {
                    // delete record
                    rescode = testFile_Dml.Write(ReplicaAction.Delete, recGID, _usergid, row_ms.ToString(), row_ms.ToString(), recName, recVal + ":DELETE");
                    procresults += "<Result><Iteration>" + i.ToString() + "</Iteration><Action>DELETE</ACTION><RecName>" + recName + "</RecName><Code>" + rescode + "</Code></Result>";
                }
            }

            procresults += "</ProcResults>";

            return procresults;
        }

    }
}
