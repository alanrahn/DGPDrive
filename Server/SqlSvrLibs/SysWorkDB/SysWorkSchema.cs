
using SqlSvrUtil;
using SysWorkDB;

namespace SysWorkDB
{
    public static class SysWorkMaster
    {
        public const string SourceDB = "dgp_master_syswork";
    }

    public class SysWorkSchema : IDB
    {

        public SysWorkSchema()
        {

        }

        public string ScanDB(string dbName, string connStr)
        {
            string scanSummary = "";

            ReplicaWork_ddl replicaWork_Ddl = new ReplicaWork_ddl();
            scanSummary += replicaWork_Ddl.ScanTable(dbName, connStr);

            GeneralWork_ddl generalWork_Ddl = new GeneralWork_ddl();
            scanSummary += generalWork_Ddl.ScanTable(dbName, connStr);

            return scanSummary;
        }

    }
}
