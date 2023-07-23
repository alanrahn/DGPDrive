using System.Data;

namespace SqlSvrUtil
{
    public interface ITbl
    {
        string ScanTable(string DBName, string SvrConnStr);
    }
}
