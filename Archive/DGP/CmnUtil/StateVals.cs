
namespace CmnUtil
{

    // ************************************************************************
    // ************************************************************************

    public static class AccLev
    {
        public const string ReadOnly = "READONLY";
        public const string ReadWrite = "READWRITE";
    }

    public static class AcctState
    {
        public const string Enabled = "ENABLED";
        public const string Disabled = "DISABLED";
    }

    public static class AcctType
    {
        public const string Admin = "ADMIN";
        public const string Standard = "STANDARD";
        public const string System = "SYSTEM";
    }

    public static class AuthState
    {
        public const string Authorized = "AUTHORIZED";
        public const string Expired = "EXPIRED";
        public const string Nomatch = "NOMATCH";
        public const string Disabled = "DISABLED";
        public const string Duplicate = "DUPLICATE";
        public const string Exceeded = "EXCEEDED";
        public const string Timeout = "TIMEOUT";
        public const string Missing = "MISSING";
        public const string Error = "ERROR";
        public const string Exception = "EXCEPTION";
        public const string MimeType = "MIMETYPE";
        public const string OffLine = "OFFLINE";
    }

    public static class BoolVal
    {
        public const string TRUE = "TRUE";
        public const string FALSE = "FALSE";
    }

    public static class DBAction
    {
        public const string Insert = "INSERT";
        public const string Update = "UPDATE";
        public const string Delete = "DELETE";
        public const string Recover = "RECOVER";
        public const string Remove = "REMOVE";
    }

    public static class LocState
    {
        public const string Online = "ONLINE";
        public const string Offline = "OFFLINE";
        public const string LocType = "LOCTYPE";
        public const string Source = "SOURCE";
        public const string Destination = "DESTINATION";
        public const string Client = "CLIENT";
        public const string Server = "SERVER";
    }

    public static class MethResult
    {
        public const string OK = "OK";
        public const string Empty = "EMPTY";
        public const string Error = "ERROR";
        public const string Exception = "EXCEPTION";
        public const string Done = "DONE";
    }

    public static class MethReturn
    {
        public const string Default = "DEFAULT";
        public const string None = "NONE";
        public const string MethodError = "METHODERROR";
    }

    public static class MsgData
    {
        public const string Int = "INT";
        public const string Num = "NUM";
        public const string Text = "TEXT";
        public const string Date = "DATE";
        public const string XML = "XML";
        public const string JSON = "JSON";
        public const string DataTable = "DATATABLE";
    }

    public static class Network
    {
        public const string localhost = "LOCALHOST";
        public const string Internal = "INTERNAL";
        public const string DMZ = "DMZ";
        public const string External = "EXTERNAL";
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

    public static class RunState
    {
        public const string Ready = "READY";
        public const string Claimed = "CLAIMED";
        public const string Stopped = "STOPPED";
        public const string Disabled = "DISABLED";
    }

    public static class SchemaType
    {
        public const string Replica = "REPLICA";
        public const string FileShard = "FILESHARD";
    }

    public static class Settings
    {
        public const string ON = "ON";
        public const string OFF = "OFF";
        public const string TRUE = "TRUE";
        public const string FALSE = "FALSE";
    }

    public static class SortOrder
    {
        public const string ASC = "ASC";
        public const string DESC = "DESC";
    }

    public static class SrchMode
    {
        public const string ByFolder = "BYFOLDER";
        public const string ByMetadata = "BYMETADATA";
        public const string ByFavorite = "BYFAVORITE";
        public const string ByTags = "BYTAGS";
    }

    public static class WorkType
    {
        public const string TableRep = "TABLEREP";
        public const string ShardRep = "SHARDREP";
        public const string CountCheck = "COUNTCHECK";
        public const string DupeCheck = "DUPECHECK";
    }

}
