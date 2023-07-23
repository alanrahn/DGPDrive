using System.Data;

namespace SqlSvrUtil
{
    public interface IDB
    {
        string ScanDB(string dbName, string serverConnStr);
    }
}
