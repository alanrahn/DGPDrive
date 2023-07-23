namespace ApiUtil
{
    public static class LocState
    {
        public const string Primary = "PRIMARY";
        public const string Backup = "BACKUP";
        public const string Maintenance = "MAINTENANCE";
        public const string Offline = "OFFLINE";
    }

    public static class Network
    {
        public const string localhost = "LOCALHOST";
        public const string Internal = "INTERNAL";
        public const string DMZ = "DMZ";
        public const string External = "EXTERNAL";
    }

    public static class SchemaType
    {
        public const string Replica = "REPLICA";
        public const string FileShard = "FILESHARD";
    }

    public static class WorkType
    {
        public const string Replicate = "REPLICATE";
        public const string Verify = "VERIFY";
        public const string DupeCheck = "DUPECHECK";
        public const string CountCheck = "COUNTCHECK";
    }

    public static class AcctType
    {
        public const string User = "USER";
        public const string System = "SYSTEM";
    }

    public static class AcctState
    {
        public const string Enabled = "ENABLED";
        public const string Disabled = "DISABLED";
    }

    public static class AccessLevel
    {
        public const string ReadOnly = "READONLY";
        public const string ReadWrite = "READWRITE";
    }

    public static class APIData
    {
        public const string Int = "INT";
        public const string Num = "NUM";
        public const string Text = "TEXT";
        public const string Date = "DATE";
        public const string XML = "XML";
        public const string JSON = "JSON";
        public const string DataTable = "DATATABLE";
    }

    public static class APIResult
    {
        public const string OK = "OK";
        public const string Empty = "EMPTY";
        public const string Error = "ERROR";
        public const string Exception = "EXCEPTION";
        public const string Done = "DONE";
    }

    public static class APISetting
    {
        public const string ON = "ON";
        public const string OFF = "OFF";
        public const string SINGLE = "SINGLE";
    }

    public static class BoolVal
    {
        public const string TRUE = "TRUE";
        public const string FALSE = "FALSE";
    }

    public static class AuthState
    {
        public const string OK = "OK";
        public const string Expired = "EXPIRED";
        public const string Error = "ERROR";
        public const string Nomatch = "NOMATCH";
        public const string Disabled = "DISABLED";
        public const string Duplicate = "DUPLICATE";
        public const string Exceeded = "EXCEEDED";
        public const string Content = "CONTENT";
        public const string Timeout = "TIMEOUT";
        public const string Missing = "MISSING";
        public const string Exception = "EXCEPTION";
    }

    public static class RunState
    {
        public const string Ready = "READY";
        public const string Claimed = "CLAIMED";
        public const string Stopped = "STOPPED";
        public const string Disabled = "DISABLED";
    }

    public static class ProcState
    {
        public const string Current = "CURRENT";
        public const string Working = "WORKING";
        public const string Complete = "COMPLETE";
        public const string Error = "ERROR";
    }

    public static class RecState
    {
        public const string Active = "ACTIVE";
        public const string Edited = "EDITED";
        public const string Deleted = "DELETED";
        public const string Duplicate = "DUPLICATE";
        public const string X = "X";
    }

    public static class ReplicaAction
    {
        public const string Insert = "INSERT";
        public const string Update = "UPDATE";
        public const string Delete = "DELETE";
        public const string Recover = "RECOVER";
        public const string Remove = "REMOVE";
    }

    public static class MethReturn
    {
        public const string Default = "DEFAULT";
        public const string None = "NONE";
        public const string MethodError = "METHODERROR";
    }

    public static class SearchModeVal
    {
        public const string ByFolder = "BYFOLDER";
        public const string ByMetadata = "BYMETADATA";
        public const string ByFavorite = "BYFAVORITE";
        public const string ByTags = "BYTAGS";
    }

    public static class SortOrder
    {
        public const string ASC = "ASC";
        public const string DESC = "DESC";
    }

    // ********************************************************************************************************* //
    // ********************************************************************************************************* //
    // ********************************************************************************************************* //

    public static class CommonFields
    {
        public const string row_id = "row_id";
        public const string src_id = "src_id";
        public const string row_ms = "row_ms";
        public const string src_ms = "src_ms";
        public const string rec_gid = "rec_gid";
        public const string rec_dbname = "rec_dbname";
        public const string rec_state = "rec_state";
        public const string rec_user = "rec_user";
        public const string start_rec_state = "start_rec_state";
        public const string RespInfo = "RespInfo";
        public const string SchemaFlag = "SchemaFlag";
        public const string PageNum = "PageNum";
        public const string PageSize = "PageSize";
        public const string Source = "Source";
        public const string SourceRecords = "SourceRecords";
        public const string LocType = "LocType";
        public const string Action = "Action";
        public const string SortOrder = "SortOrder";
        public const string row_gid = "row_gid";
    }

    public static class LogFields
    {
        public const string CompName = "CompName";
        public const string AppName = "AppName";
        public const string FormName = "FormName";
        public const string ClassName = "ClassName";
        public const string MsgLoc = "MsgLoc";
        public const string ErrLevel = "ErrLevel";
        public const string ErrMessage = "ErrMessage";
        public const string ErrData = "ErrData";
        public const string BatchCount = "BatchCount";
        public const string StartID = "StartID";
        public const string EndID = "EndID";
        public const string DurationMS = "DurationMS";
        public const string ProcState = "ProcState";
        public const string WebSvcName = "WebSvcName";
        public const string WebSvcVer = "WebSvcVer";
        public const string ClientTime = "ClientTime";
        public const string MethodCount = "MethodCount";
        public const string EndToEndMS = "EndToEndMS";
        public const string NetworkMS = "NetworkMS";
        public const string ServerMS = "ServerMS";
        public const string MsgName = "MsgName";
        public const string MsgData = "MsgData";
        public const string LogTime = "LogTime";
        public const string Eval = "Eval";
        public const string APIMethod = "APIMethod";
    }

    // ************************************************************************
    // ************************************************************************
    // DGPAdmin API
    // ************************************************************************
    // ************************************************************************

    public static class APIMethodFields
    {
        public const string MethodGID = "MethodGID";
        public const string APIName = "APIName";
        public const string MethodName = "MethodName";
        public const string VersionName = "VersionName";
        public const string MethodDescrip = "MethodDescrip";
    }

    public static class APIRoleFields
    {
        public const string RoleGID = "RoleGID";
        public const string RoleName = "RoleName";
        public const string RoleDescrip = "RoleDescrip";
    }

    public static class APIUserFields
    {
        public const string UserGID = "UserGID";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string FirstName = "FirstName";
        public const string MiddleName = "MiddleName";
        public const string LastName = "LastName";
        public const string Email = "Email";
        public const string AccountType = "AccountType";
        public const string AccountState = "AccountState";
        public const string ExpireDate = "ExpireDate";
        public const string MethodList = "MethodList";
        public const string ReadList = "ReadList";
        public const string WriteList = "WriteList";
        public const string MethodLimit = "MethodLimit";
        public const string SvcKeyVersion = "SvcKeyVersion";
    }

    public static class DataGroupFields
    {
        public const string GroupGID = "GroupGID";
        public const string GroupName = "GroupName";
        public const string GroupDescrip = "GroupDescrip";
    }

    public static class GroupUserFields
    {
        public const string GroupUserGID = "GroupUserGID";
        public const string AccessLevel = "AccessLevel";
    }

    public static class RoleMethodFields
    {
        public const string RoleMethodGID = "RoleMethodGID";
    }

    public static class RoleUserFields
    {
        public const string RoleUserGID = "RoleUserGID";
    }

    // ************************************************************************
    // ************************************************************************
    // FileStore API
    // ************************************************************************
    // ************************************************************************

    public static class FolderFields
    {
        public const string FolderGID = "FolderGID";
        public const string GroupGID = "GroupGID";
        public const string ParentGID = "ParentGID";
        public const string FolderName = "FolderName";
        public const string DisplayOrder = "DisplayOrder";
        public const string FolderPath = "FolderPath";
    }

    public static class FileFields
    {
        public const string FileGID = "FileGID";
        public const string FileName = "FileName";
        public const string FileDescrip = "FileDescrip";
        public const string FileExt = "FileExt";
        public const string FileSize = "FileSize";
        public const string FileDate = "FileDate";
        public const string FileHash = "FileHash";
        public const string FileVersion = "FileVersion";
        public const string ShardName = "ShardName";
        public const string SegSize = "SegSize";
        public const string TotalSeg = "TotalSeg";
        public const string SegNum = "SegNum";
        public const string SegData = "SegData";
        public const string SortBy = "SortBy";
        public const string SortOrder = "SortOrder";
        public const string Offset = "Offset";
        public const string ThreadID = "ThreadID";
        public const string FileShardGID = "FileShardGID";
    }

    public static class TagFields
    {
        public const string TagGID = "TagGID";
        public const string TagName = "TagName";
        public const string TagDescrip = "TagDescrip";
        public const string FileTagGID = "FileTagGID";
    }

    public static class FavoriteFields
    {
        public const string FavoriteGID = "FavoriteGID";
    }

    // ************************************************************************
    // ************************************************************************
    // Metrics API
    // ************************************************************************
    // ************************************************************************

    public static class TestResultsFields
    {
        public const string ResultRecords = "ResultRecords";
        public const string TestRun = "TestRun";
        public const string Eval = "Eval";
        public const string EvalInfo = "EvalInfo";
        public const string APIMethod = "APIMethod";
        public const string AuthCode = "AuthCode";
        public const string AuthInfo = "AuthInfo";
        public const string ExpAuthCode = "ExpAuthCode";
        public const string ClientMS = "ClientMS";
        public const string NewtorkMS = "NetworkMS";
        public const string ServerMS = "ServerMS";
        public const string UserName = "UserName";
        public const string CompName = "CompName";
        public const string TimeStamp = "TimeStamp";
        public const string ReqSize = "ReqSize";
        public const string RespSize = "RespSize";
        public const string SvcURL = "SvcURL";
        public const string FileName = "FileName";
    }

    // ************************************************************************
    // ************************************************************************
    // Test API
    // ************************************************************************
    // ************************************************************************

    public static class TestReplicaFields
    {
        public const string DBName = "DBName";
        public const string TestReplicaGID = "TestReplicaGID";
        public const string Name = "Name";
        public const string Value = "Value";
    }


    // ************************************************************************
    // ************************************************************************
    // DGPWork API
    // ************************************************************************
    // ************************************************************************

    public static class IntervalFields
    {
        public const string MS = "MS";
        public const string Sec = "SEC";
        public const string MIN = "MIN";
        public const string HOUR = "HOUR";
        public const string TIMEOFDAY = "TIMEOFDAY";
        public const string DAYOFWEEK = "DAYOFWEEK";
        public const string DAYOFMONTH = "DAYOFMONTH";
    }

    public static class WorkFields
    {
        public const string WorkGID = "WorkGID";
        public const string WorkType = "WorkType";
        public const string SchemaType = "SchemaType";
        public const string SchemaTable = "SchemaTable";
        public const string SrcDBName = "SrcDBName";
        public const string NextRun = "NextRun";
        public const string ClaimedBy = "ClaimedBy";
        public const string ClaimID = "ClaimID";
        public const string ClaimTime = "ClaimTime";
        public const string Placeholder = "Placeholder";
        public const string StartID = "StartID";
        public const string EndID = "EndID";
        public const string FinalID = "FinalID";
        public const string FinalAction = "FinalAction";
        public const string BatchSize = "BatchSize";
        public const string IntervalMS = "IntervalMS";
        public const string RunState = "RunState";
        public const string StateMsg = "StateMsg";
        public const string SrcURL = "SrcURL";
        public const string DestURL = "DestURL";
        public const string SrcMethod = "SrcMethod";
        public const string DestMethod = "DestMethod";
        public const string Logging = "Logging";
        public const string ProcState = "ProcState";
        public const string InputParams = "InputParams";
        public const string IntervalType = "IntervalType";
        public const string IntervalVal = "IntervalVal";
        public const string MaxDurMS = "MaxDurMS";
        public const string ProcSteps = "ProcSteps";
        public const string Network = "Network";
        public const string ServerTime = "ServerTime";
        public const string ResetCount = "ResetCount";
    }

    public static class ReplicaFields
    {
        public const string ReplicaWork = "ReplicaWork";
        public const string ReplicaNetwork = "ReplicaNetwork";
        public const string ReplicaWorkMS = "ReplicaWorkMS";
        public const string ReplicaMaxBatch = "ReplicaMaxBatch";
        public const string GeneralWork = "GeneralWork";
        public const string GeneralNetwork = "GeneralNetwork";
        public const string GeneralWorkMS = "GeneralWorkMS";
        public const string GeneralMaxBatch = "GeneralMaxBatch";
        public const string AcctName = "AcctName";
        public const string AcctPword = "AcctPword";
        public const string QueueCheck = "QueueCheck";
        public const string QueueCheckMS = "QueueCheckMS";
    }

    public static class CountFields
    {
        public const string SrcTblName = "SrcTblName";
        public const string MaxSrcID = "MaxSrcID";
        public const string MaxDestSrcID = "MaxDestSrcID";
        public const string SrcCount = "SrcCount";
        public const string SrcActiveCount = "SrcActiveCount";
        public const string DestSrcCount = "DestSrcCount";
        public const string DestSrcActiveCount = "DestSrcActiveCount";
    }


}

