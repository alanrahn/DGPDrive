using SqlSvrUtil;

using SysTestDB.TestReplica;
using SysTestDB.TestFile;

namespace SysTestDB
{
    public static class TestDBMaster
    {
        public const string SourceDB = "dgp_master_testdb";
    }

    public class TestDBSchema : IDB
    {
        public TestDBSchema()
        {

        }

        public string ScanDB(string dbName, string connStr)
        {
            string scanSummary = "";

            TestReplica_ddl testReplica_Ddl = new TestReplica_ddl();
            scanSummary += testReplica_Ddl.ScanTable(dbName, connStr);

            TestFile_ddl testFile_Ddl = new TestFile_ddl();
            scanSummary += testFile_Ddl.ScanTable(dbName, connStr);

            return scanSummary;
        }
    }
}
