
using SqlSvrUtil;
using SysInfoDB.DataGroups;
using SysInfoDB.GroupUsers;
using SysInfoDB.APIMethods;
using SysInfoDB.RoleMethods;
using SysInfoDB.APIRoles;
using SysInfoDB.RoleUsers;
using SysInfoDB.APIUser;

namespace SysInfoDB
{
    public static class SysInfoMaster
    {
        public const string SourceDB = "dgp_master_sysinfo";
    }

    public class SysInfoSchema : IDB
    {
        public string AdminUser { get; set; }
        public string AdminPassword { get; set; }
        public string SvcKey { get; set; }
        public string SvcKeyVer { get; set; }

        public SysInfoSchema()
        {

        }

        public string ScanDB(string dbName, string connStr)
        {
            string scanSummary = "";

            APIMethods_ddl apiMethods_Ddl = new APIMethods_ddl();
            scanSummary += apiMethods_Ddl.ScanTable(dbName, connStr);

            APIRoles_ddl roles_Ddl = new APIRoles_ddl();
            scanSummary += roles_Ddl.ScanTable(dbName, connStr);

            RoleMethods_ddl roleMethods_Ddl = new RoleMethods_ddl();
            scanSummary += roleMethods_Ddl.ScanTable(dbName, connStr);

            RoleUsers_ddl roleUsers_Ddl = new RoleUsers_ddl();
            scanSummary += roleUsers_Ddl.ScanTable(dbName, connStr);

            DataGroups_ddl dataGroups_Ddl = new DataGroups_ddl();
            scanSummary += dataGroups_Ddl.ScanTable(dbName, connStr);

            GroupUsers_ddl groupUser_Ddl = new GroupUsers_ddl();
            scanSummary += groupUser_Ddl.ScanTable(dbName, connStr);

            APIUser_ddl userAccounts_Ddl = new APIUser_ddl();
            scanSummary += userAccounts_Ddl.ScanTable(dbName, connStr, AdminUser, AdminPassword, SvcKey, SvcKeyVer);

            return scanSummary;
        }
    }
}
