

using SvrUtil;
using DGPDatabase.DGPSec;

namespace DGPDatabase.DGPSecDB
{
    public static class LatticeMaster
    {
        public const string SourceDB = "dgp_master_dgpsec";
    }

    public class DGPSecSchema : IDB
    {
        string _dbname;
        string _connstr;

        public DGPSecSchema(string dbName, string connStr)
        {
            _dbname = dbName;
            _connstr = connStr;
        }


        public string ScanDB()
        {
            string scanSummary = "";

            // check to see if database exists


            // check each table in the database
            MethSchema methSchema = new MethSchema();
            scanSummary += methSchema.ScanTable(_dbname, _connstr);

            RoleSchema roleSchema = new RoleSchema();
            scanSummary += roleSchema.ScanTable(_dbname, _connstr);

            GroupSchema groupSchema = new GroupSchema();
            scanSummary += groupSchema.ScanTable(_dbname, _connstr);

            AcctSchema acctSchema = new AcctSchema();
            scanSummary += acctSchema.ScanTable(_dbname, _connstr);

            RoleMethSchema roleMethSchema = new RoleMethSchema();
            scanSummary += roleMethSchema.ScanTable(_dbname, _connstr);

            AcctRoleSchema acctRoleSchema = new AcctRoleSchema();
            scanSummary += acctRoleSchema.ScanTable(_dbname, _connstr);

            AcctGroupSchema acctGroupSchema = new AcctGroupSchema();
            scanSummary += acctGroupSchema.ScanTable(_dbname, _connstr);


            return scanSummary;
        }
    }
}
