
using SqlSvrUtil;

using FileStoreDB.FileShard;

namespace FileStoreDB
{
    public class FileShardSchema : IDB
    { 
        public FileShardSchema()
        {

        }

        public string ScanDB(string dbName, string connStr)
        {
            string scanSummary = "";

            FileShard_ddl fileshard_Ddl = new FileShard_ddl();
            scanSummary += fileshard_Ddl.ScanTable(dbName, connStr);

            return scanSummary;
        }
    }
}
