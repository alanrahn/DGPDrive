
using SqlSvrUtil;

using FileStoreDB.Files;
using FileStoreDB.Folders;
using FileStoreDB.Favorites;
using FileStoreDB.FileTags;
using FileStoreDB.Tags;

namespace FileStoreDB
{
    public static class DGPDriveMaster
    {
        public const string SourceDB = "dgp_master_dgpdrive";
    }

    public class FileStoreSchema : IDB
    { 
        public FileStoreSchema()
        {

        }

        public string ScanDB(string dbName, string connStr)
        {
            string scanSummary = "";

            Folders_ddl folders_Ddl = new Folders_ddl();
            scanSummary += folders_Ddl.ScanTable(dbName, connStr);

            Files_ddl files_Ddl = new Files_ddl();
            scanSummary += files_Ddl.ScanTable(dbName, connStr);

            Favorites_ddl favorites_Ddl = new Favorites_ddl();
            scanSummary += favorites_Ddl.ScanTable(dbName, connStr);

            FileTags_ddl fileTags_Ddl = new FileTags_ddl();
            scanSummary += fileTags_Ddl.ScanTable(dbName, connStr);

            Tags_ddl tags_Ddl = new Tags_ddl();
            scanSummary += tags_Ddl.ScanTable(dbName, connStr);

            return scanSummary;
        }
    }
}
