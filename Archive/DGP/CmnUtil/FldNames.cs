
namespace CmnUtil
{
    
    public static class CmnFields
    {
        public const string row_id = "row_id";
        public const string row_ms = "row_ms";
        public const string rec_gid = "rec_gid";
        public const string rec_dbname = "rec_dbname";
        public const string rec_state = "rec_state";
        public const string rec_acct = "rec_acct";
        public const string src_id = "src_id";
        public const string src_ms = "src_ms";

        public const string PageNum = "PageNum";
        public const string PageSize = "PageSize";
        public const string SortOrder = "SortOrder";
        public const string SchemaFlag = "SchemaFlag";

        public const string row_id_prm = "@row_id";
        public const string row_ms_prm = "@row_ms";
        public const string rec_gid_prm = "@rec_gid";
        public const string rec_dbname_prm = "@rec_dbname";
        public const string rec_state_prm = "@rec_state";
        public const string rec_acct_prm = "@rec_acct";
        public const string src_id_prm = "@src_id";
        public const string src_ms_prm = "@src_ms";

        public const string PageNum_prm = "@PageNum";
        public const string PageSize_prm = "@PageSize";
        public const string SortOrder_prm = "@SortOrder";
        public const string SchemaFlag_prm = "@SchemaFlag";
    }

    // ************************************************************************
    // ************************************************************************
    // DGPSec Entities
    // ************************************************************************
    // ************************************************************************

    public static class DGPAcct
    {
        public const string AcctName = "AcctName";
        public const string SecToken = "SecToken";
        public const string AcctType = "AcctType";
        public const string AcctState = "AcctState";
        public const string FirstName = "FirstName";
        public const string MiddleName = "MiddleName";
        public const string LastName = "LastName";
        public const string AcctEmail = "AcctEmail";
        public const string ExpDate = "ExpDate";
        public const string MethList = "MethList";
        public const string ReadList = "ReadList";
        public const string WriteList = "WriteList";
        public const string MethLimit = "MethLimit";

        public const string AcctName_prm = "@AcctName";
        public const string SecToken_prm = "@SecToken";
        public const string AcctType_prm = "@AcctType";
        public const string AcctState_prm = "@AcctState";
        public const string FirstName_prm = "@FirstName";
        public const string MiddleName_prm = "@MiddleName";
        public const string LastName_prm = "@LastName";
        public const string AcctEmail_prm = "@AcctEmail";
        public const string ExpDate_prm = "@ExpDate";
        public const string MethList_prm = "@MethList";
        public const string ReadList_prm = "@ReadList";
        public const string WriteList_prm = "@WriteList";
        public const string MethLimit_prm = "@MethLimit";
    }

    public static class DGPMeth
    {
        public const string FullName = "FullName";
        public const string APIName = "APIName";
        public const string MethName = "MethName";
        public const string MethDescrip = "MethDescrip";
        public const string MethType = "MethType";

        public const string FullName_prm = "@FullName";
        public const string APIName_prm = "@APIName";
        public const string MethName_prm = "@MethName";
        public const string MethDescrip_prm = "@MethDescrip";
        public const string MethType_prm = "@MethType";
    }

    public static class DGPRole
    {
        public const string RoleName = "RoleName";
        public const string RoleDescrip = "RoleDescrip";

        public const string RoleName_prm = "@RoleName";
        public const string RoleDescrip_prm = "@RoleDescrip";
    }

    public static class DGPGroup
    {
        public const string GroupName = "GroupName";
        public const string GroupDescrip = "GroupDescrip";

        public const string GroupName_prm = "@GroupName";
        public const string GroupDescrip_prm = "@GroupDescrip";
    }

    //public static class AcctRole
    //{ 
    //    // common fields + acct and role natural keys
    //}

    //public static class RoleMeth
    //{
    //    // common fields + role and meth natural keys
    //}

    public static class DGPAcctGroup
    {
        // common fields + acct and group natural keys
        public const string AccLev = "AccLev";

        public const string AccLev_prm = "@AccLev";
    }

    // ************************************************************************
    // ************************************************************************
    // DGPData Entities
    // ************************************************************************
    // ************************************************************************

    public static class DGPError
    {
        public const string LogTime = "LogTime";
        public const string AcctName = "AcctName";
        public const string CompName = "CompName";
        public const string CompLoc = "CompLoc";
        public const string AppName = "AppName";
        public const string ClsName = "ClsName";
        public const string ErrLev = "ErrLev";
        public const string ErrMsg = "ErrMsg";
        public const string ErrData = "ErrData";

        public const string LogTime_prm = "@LogTime";
        public const string AcctName_prm = "@AcctName";
        public const string CompName_prm = "@CompName";
        public const string CompLoc_prm = "@CompLoc";
        public const string AppName_prm = "@AppName";
        public const string ClsName_prm = "@ClsName";
        public const string ErrLev_prm = "@ErrLev";
        public const string ErrMsg_prm = "@ErrMsg";
        public const string ErrData_prm = "@ErrData";
    }

    public static class DGPMetric
    {
        public const string LogTime = "LogTime";
        public const string AcctName = "AcctName";
        public const string CompName = "CompName";
        public const string AppName = "AppName";
        public const string ClsName = "ClsName";
        public const string SvcName = "SvcName";
        public const string CliMS = "CliMS";
        public const string SvcMS = "SvcMS";
        public const string NetMS = "NetMS";

        public const string LogTime_prm = "@LogTime";
        public const string AcctName_prm = "@AcctName";
        public const string CompName_prm = "@CompName";
        public const string AppName_prm = "@AppName";
        public const string ClsName_prm = "@ClsName";
        public const string SvcName_prm = "@SvcName";
        public const string CliMS_prm = "@CliMS";
        public const string SvcMS_prm = "@SvcMS";
        public const string NetMS_prm = "@NetMS";
    }

    public static class DGPTestResults
    {
        public const string LogTime = "LogTime";
        public const string AcctName = "AcctName";
        public const string CompName = "CompName";
        public const string SvcURL = "SvcURL";
        public const string EvalCode = "EvalCode";
        public const string EvalMsg = "EvalMsg";
        public const string AuthCode = "AuthCode";
        public const string AuthMsg = "AuthMsg";
        public const string ExpAuth = "ExpAuth";
        public const string CliMS = "CliMS";
        public const string SvcMS = "SvcMS";
        public const string NetMS = "NetMS";
        public const string ReqByte = "ReqByte";
        public const string RespByte = "RespByte";

        public const string LogTime_prm = "@LogTime";
        public const string AcctName_prm = "@AcctName";
        public const string CompName_prm = "@CompName";
        public const string SvcURL_prm = "@SvcURL";
        public const string EvalCode_prm = "@EvalCode";
        public const string EvalMsg_prm = "@EvalMsg";
        public const string AuthCode_prm = "@AuthCode";
        public const string AuthMsg_prm = "@AuthMsg";
        public const string ExpAuth_prm = "@ExpAuth";
        public const string CliMS_prm = "@CliMS";
        public const string SvcMS_prm = "@SvcMS";
        public const string NetMS_prm = "@NetMS";
        public const string ReqByte_prm = "@ReqByte";
        public const string RespByte_prm = "@RespByte";
    }

    // ************************************************************************
    // ************************************************************************
    // DGPProcess Entities
    // ************************************************************************
    // ************************************************************************

    public static class DGPReplication
    {
        public const string NetSeg = "NetSeg";
        public const string WorkType = "WorkType";
        public const string SrcDBName = "SrcDBName";
        public const string ShardName = "ShardName";
        public const string SrcURL = "SrcURL";
        public const string SrcMeth = "SrcMeth";
        public const string SrcPlaceholder = "SrcPlaceholder";
        public const string DestURL = "DestURL";
        public const string DestMeth = "DestMeth";
        public const string EndPlaceholder = "EndPlaceholder";
        public const string MaxBatch = "MaxBatch";
        public const string IntervalMS = "IntervalMS";
        public const string NextRunTime = "NextRunTime";
        public const string MaxRunMS = "MaxRunMS";
        public const string ProcState = "ProcState";
        public const string ProcMsg = "ProcMsg";
        public const string LogFlag = "LogFlag";
        public const string Claimed = "Claimed";
        public const string ClaimID = "ClaimID";
        public const string ClaimTime = "ClaimTime";
        public const string ResetCount = "ResetCount";

        public const string NetSeg_prm = "@NetSeg";
        public const string WorkType_prm = "@WorkType";
        public const string SrcDBName_prm = "@SrcDBName";
        public const string ShardName_prm = "@ShardName";
        public const string SrcURL_prm = "@SrcURL";
        public const string SrcMeth_prm = "@SrcMeth";
        public const string SrcPlaceholder_prm = "@SrcPlaceholder";
        public const string DestURL_prm = "@DestURL";
        public const string DestMeth_prm = "@DestMeth";
        public const string EndPlaceholder_prm = "@EndPlaceholder";
        public const string MaxBatch_prm = "@MaxBatch";
        public const string IntervalMS_prm = "@IntervalMS";
        public const string NextRunTime_prm = "@NextRunTime";
        public const string MaxRunMS_prm = "@MaxRunMS";
        public const string ProcState_prm = "@ProcState";
        public const string ProcMsg_prm = "@ProcMsg";
        public const string LogFlag_prm = "@LogFlag";
        public const string Claimed_prm = "@Claimed";
        public const string ClaimID_prm = "@ClaimID";
        public const string ClaimTime_prm = "@ClaimTime";
        public const string ResetCount_prm = "@ResetCount";
    }

    // ************************************************************************
    // ************************************************************************
    // DGPDrive Entities
    // ************************************************************************
    // ************************************************************************

    public static class DGPFolder
    {
        public const string FoldGID = "FoldGID";
        public const string GrpName = "GrpName";
        public const string ParFoldGID = "ParFoldGID";
        public const string FoldName = "FoldName";
        public const string DispOrd = "DispOrd";

        public const string FoldGID_prm = "@FoldGID";
        public const string GrpName_prm = "@GrpName";
        public const string ParFoldGID_prm = "@ParFoldGID";
        public const string FoldName_prm = "@FoldName";
        public const string DispOrd_prm = "@DispOrd";
    }

    public static class DGPFile
    {
        public const string FoldGID = "FoldGID";
        public const string FoldPath = "FoldPath";
        public const string GrpName = "GrpName";
        public const string FileGID = "FileGID";
        public const string FileName = "FileName";
        public const string FileExt = "FileExt";
        public const string FileDescrip = "FileDescrip";
        public const string FileDate = "FileDate";
        public const string FileVer = "FileVer";
        public const string FileBytes = "FileBytes";
        public const string FileHash = "FileHash";
        public const string ShardName = "ShardName";
        public const string SegBytes = "SegBytes";
        public const string SegCount = "SegCount";

        public const string FoldGID_prm = "@FoldGID";
        public const string FoldPath_prm = "@FoldPath";
        public const string GrpName_prm = "@GrpName";
        public const string FileGID_prm = "@FileGID";
        public const string FileName_prm = "@FileName";
        public const string FileExt_prm = "@FileExt";
        public const string FileDescrip_prm = "@FileDescrip";
        public const string FileDate_prm = "@FileDate";
        public const string FileVer_prm = "@FileVer";
        public const string FileBytes_prm = "@FileBytes";
        public const string FileHash_prm = "@FileHash";
        public const string ShardName_prm = "@ShardName";
        public const string SegBytes_prm = "@SegBytes";
        public const string SegCount_prm = "@SegCount";
    }

    public static class DGPTag
    {
        public const string TagName = "TagName";
        public const string TagDescrip = "TagDescrip";

        public const string TagName_prm = "@TagName";
        public const string TagDescrip_prm = "@TagDescrip";
    }

    // ************************************************************************
    // ************************************************************************
    // DGPShard Entities
    // ************************************************************************
    // ************************************************************************

    public static class DGPFileSeg
    {
        public const string ShardName = "ShardName";
        public const string SegNum = "FileDescrip";
        public const string SegBytes = "SegBytes";
        public const string SegCount = "SegCount";
        public const string SegData = "SegData";

        public const string ShardName_prm = "@ShardName";
        public const string SegNum_prm = "@FileDescrip";
        public const string SegBytes_prm = "@SegBytes";
        public const string SegCount_prm = "@SegCount";
        public const string SegData_prm = "@SegData";
    }
    
}
