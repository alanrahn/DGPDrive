

namespace SvrUtil
{
    public static class MasterDB
    {
        public const string SourceDB = "virtual_master_dgpsec";
    }

    public static class DBNames
    {
        public const string DGPSec = "DGPSec";
        public const string DGPData = "DGPData";
        public const string DGPProc = "DGPProc";
        public const string DGPDrive = "DGPDrive";
        public const string DGPShard = "DGPShard";
    }

    public static class DGPSecTables
    {
        public const string DGPAcct = "DGPAcct";
        public const string DGPAcctGroup = "DGPAcctGroup";
        public const string DGPAcctRole = "DGPAcctRole";
        public const string DGPGroup = "DGPGroup";
        public const string DGPMeth = "DGPMeth";
        public const string DGPRole = "DGPRole";
        public const string DGPRoleMeth = "DGPRoleMeth";
    }

    public static class DGPDataTables
    {
        public const string DGPError = "DGPError";
        public const string DGPMetric = "DGPMetric";
        public const string DGPTestResult = "DGPTestResult";
    }

    public static class DGPDriveTables
    {
        public const string DGPFavorite = "DGPFavorite";
        public const string DGPFile = "DGPFile";
        public const string DGPFileTag = "DGPFileTag";
        public const string DGPFolder = "DGPFolder";
        public const string DGPTag = "DGPTag";
    }

    public static class DGPShardTables
    {
        public const string DGPFileSeg = "DGPFileSeg";
    }

    public static class DGPProcTables
    {
        public const string DGPRepWork = "DGPRepWork";
        public const string DGPGenWork = "DGPGenWork";
    }
}
