using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace FileStoreDB.Files
{
    public class Files_read_dml
    {
        string _connstr;

        public Files_read_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByID(string fileGID, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select *" +
                                        " from Files" +
                                        " where rec_gid = @FileGID" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " and rec_state = @RecState;";

                    sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = fileGID;
                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        //public string GetSegCount(string fileGID, string groupList)
        //{
        //    string segcount;

        //    using (SqlCommand sqlCmd = new SqlCommand())
        //    {
        //        sqlCmd.CommandText = "select TotalSeg" +
        //                            " from Files" +
        //                            " where rec_gid = @FileGID" +
        //                            " and GroupGID IN (" + groupList + ")" +
        //                            " and rec_state = @RecState;";

        //        sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = fileGID;
        //        sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

        //        DBUtil dBUtil = new DBUtil();
        //        segcount = dBUtil.ReadValue(sqlCmd, _connstr);
        //    }

        //    return segcount;
        //}

        /// <summary>
        /// 
        /// </summary>
        public string GetMaxID()
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select max(row_id) from Files;";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetExtList()
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select distinct FileExt" +
                                    " from Files;";

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// checks for duplicate file names in the same folder
        /// </summary>
        public string CheckFile(string folderGID, string fileName)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "Select count(*) from Files where FolderGID = @FolderGID and FileName = @FileName and rec_state = @RecState;";

                sqlCmd.Parameters.Add("@FolderGID", SqlDbType.VarChar, 50).Value = folderGID;
                sqlCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 50).Value = fileName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReadValue(sqlCmd, _connstr);
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetByName(string fileName, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select *" +
                                        " from Files" +
                                        " where FileName = @FileName" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " and rec_state = @RecState;";

                    sqlCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 50).Value = fileName;
                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCountByFolder(string foldergid, string groupList)
        {
            string strresult = "";

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "Select count(*) AS Count" +
                                        " From Files" +
                                        " where FolderGID = @FolderGID" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " and rec_state = @RecState;";

                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;
                    sqlCmd.Parameters.Add("@FolderGID", SqlDbType.VarChar, 50).Value = foldergid;

                    DBUtil dBUtil = new DBUtil();
                    strresult = dBUtil.ReadValue(sqlCmd, _connstr);
                }
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetFilesByFolder(string pagenum, string pagesize, string foldergid, string sortBy, string sortOrder, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    int _pagesize = Convert.ToInt32(pagesize);
                    if (_pagesize > 100) _pagesize = 100;
                    int _offset = Convert.ToInt32(pagenum) * _pagesize;

                    string _sortby = "FileName";
                    if (sortBy != null && sortBy != "") _sortby = sortBy;

                    if (sortOrder != "ASC" && sortOrder != "DESC") sortOrder = "ASC";

                    switch (sortBy.ToUpper())
                    {
                        case "NAME":
                            _sortby = "FileName";
                            break;

                        case "TYPE":
                            _sortby = "FileExt";
                            break;

                        case "DATE":
                            _sortby = "FileDate";
                            break;
                    }

                    sqlCmd.CommandText = "Select FileName, FileDescrip, FileExt, FileDate, FileSize, FileVersion, FolderPath, ShardName, FileHash, SegSize, TotalSeg, FolderGID, GroupGID, rec_gid, row_id, row_ms" +
                                        " From Files" +
                                        " where FolderGID = @FolderGID" + "" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " and rec_state = @RecState";

                    sqlCmd.CommandText += " Order By " + _sortby + " " + sortOrder;

                    sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                        " Fetch Next " + pagesize + " Rows Only;";

                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;
                    sqlCmd.Parameters.Add("@FolderGID", SqlDbType.VarChar, 50).Value = foldergid;

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCountByMetadata(string filename, string fileext, string filedate, string recstate, string groupList)
        {
            string strresult = "";

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "Select count(*) AS Count" +
                                        " From Files" +
                                        " where rec_state = @RecState" +
                                        " and GroupGID IN (" + groupList + ")";

                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recstate;

                    // search filter values are optional
                    if (filename != null && filename != "")
                    {
                        sqlCmd.CommandText += " And FileName Like @FileName";
                        sqlCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 50).Value = filename + "%";
                    }

                    if (fileext != null && fileext != "")
                    {
                        sqlCmd.CommandText += " And FileExt = @FileExt";
                        sqlCmd.Parameters.Add("@FileExt", SqlDbType.VarChar, 50).Value = fileext;
                    }

                    if (filedate != null && filedate != "")
                    {
                        sqlCmd.CommandText += " And FileDate >= @FileDate";
                        sqlCmd.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = Convert.ToDateTime(filedate);
                    }

                    sqlCmd.CommandText += ";";

                    DBUtil dBUtil = new DBUtil();
                    strresult = dBUtil.ReadValue(sqlCmd, _connstr);
                }
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetFilesByMetadata(string pagenum, string pagesize, string filename, string fileext, string filedate, string recstate, string sortBy, string sortOrder, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    int _pagesize = Convert.ToInt32(pagesize);
                    if (_pagesize > 100) _pagesize = 100;
                    int _offset = Convert.ToInt32(pagenum) * _pagesize;

                    string _sortby = "FileName";
                    if (sortBy != null && sortBy != "") _sortby = sortBy;

                    if (sortOrder != "ASC" && sortOrder != "DESC") sortOrder = "ASC";

                    switch (sortBy.ToUpper())
                    {
                        case "NAME":
                            _sortby = "FileName";
                            break;

                        case "TYPE":
                            _sortby = "FileExt";
                            break;

                        case "DATE":
                            _sortby = "FileDate";
                            break;
                    }

                    sqlCmd.CommandText = "Select FileName, FileDescrip, FileExt, FileDate, FileSize, FileVersion, FolderPath, FileHash, SegSize, TotalSeg, ShardName, FolderGID, GroupGID, rec_gid, row_id, row_ms" +
                                        " From Files" +
                                        " where rec_state = @RecState" +
                                        " and GroupGID IN (" + groupList + ")";

                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = recstate;

                    // search filter values are optional
                    if (filename != null && filename != "")
                    {
                        sqlCmd.CommandText += " And FileName Like @FileName";
                        sqlCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 50).Value = filename + "%";
                    }

                    if (fileext != null && fileext != "" && fileext != "*")
                    {
                        sqlCmd.CommandText += " And FileExt = @FileExt";
                        sqlCmd.Parameters.Add("@FileExt", SqlDbType.VarChar, 50).Value = fileext;
                    }

                    if (filedate != null && filedate != "")
                    {
                        sqlCmd.CommandText += " And FileDate >= @FileDate";
                        sqlCmd.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = Convert.ToDateTime(filedate);
                    }

                    sqlCmd.CommandText += " Order By " + _sortby + " " + sortOrder;

                    sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                        " Fetch Next " + pagesize + " Rows Only;";

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCountByFavorite(string userGID, string groupList)
        {
            string strresult = "";

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "Select count(*) AS Count" +
                                        " From Files fi" +
                                        " inner join Favorites fav on fi.rec_gid = fav.FileGID" +
                                        " where fav.UserGID = @UserGID" +
                                        " and fi.GroupGID IN (" + groupList + ")" +
                                        " and fi.rec_state = @RecState and fav.rec_state = @RecState;";

                    sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;
                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;


                    DBUtil dBUtil = new DBUtil();
                    strresult = dBUtil.ReadValue(sqlCmd, _connstr);
                }
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetFilesByFavorite(string pagenum, string pagesize, string userGID, string sortBy, string sortOrder, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    int _pagesize = Convert.ToInt32(pagesize);
                    if (_pagesize > 100) _pagesize = 100;
                    int _offset = Convert.ToInt32(pagenum) * _pagesize;

                    string _sortby = "FileName";
                    if (sortBy != null && sortBy != "") _sortby = sortBy;

                    if (sortOrder != "ASC" && sortOrder != "DESC") sortOrder = "ASC";

                    switch (sortBy.ToUpper())
                    {
                        case "NAME":
                            _sortby = "fi.FileName";
                            break;

                        case "TYPE":
                            _sortby = "fi.FileExt";
                            break;

                        case "DATE":
                            _sortby = "fi.FileDate";
                            break;
                    }

                    sqlCmd.CommandText = "Select fi.FileName, fi.FileDescrip, fi.FileExt, fi.FileDate, fi.FileSize, fi.FileVersion, fi.FolderPath, fi.FileHash, fi.ShardName, fi.SegSize, fi.TotalSeg, fi.FolderGID, fi.GroupGID, fi.rec_gid, fi.row_id, fi.row_ms, fav.rec_gid AS FavoriteGID" +
                                        " From Files fi" +
                                        " inner join Favorites fav on fi.rec_gid = fav.FileGID" +
                                        " where fav.UserGID = @UserGID" +
                                        " and fi.GroupGID IN (" + groupList + ")" +
                                        " and fav.rec_state = @RecState and fi.rec_state = @RecState";

                    sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;
                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    sqlCmd.CommandText += " Order By " + _sortby + " " + sortOrder;

                    sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                        " Fetch Next " + pagesize + " Rows Only;";

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCountByTag(string tagGID, string groupList)
        {
            string strresult = "";

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "Select count(*) AS Count" +
                                        " From Files fi" +
                                        " inner join FileTags ft on fi.rec_gid = ft.FileGID" +
                                        " where ft.TagGID = @TagGID" +
                                        " and fi.GroupGID IN (" + groupList + ")" +
                                        " and fi.rec_state = @RecState and ft.rec_state = @RecState;";

                    sqlCmd.Parameters.Add("@TagGID", SqlDbType.VarChar, 50).Value = tagGID;
                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    DBUtil dBUtil = new DBUtil();
                    strresult = dBUtil.ReadValue(sqlCmd, _connstr);
                }
            }

            return strresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetFilesByTag(string pagenum, string pagesize, string tagGID, string sortBy, string sortOrder, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    int _pagesize = Convert.ToInt32(pagesize);
                    if (_pagesize > 100) _pagesize = 100;
                    int _offset = Convert.ToInt32(pagenum) * _pagesize;

                    string _sortby = "FileName";
                    if (sortBy != null && sortBy != "") _sortby = sortBy;

                    if (sortOrder != "ASC" && sortOrder != "DESC") sortOrder = "ASC";

                    switch (sortBy.ToUpper())
                    {
                        case "NAME":
                            _sortby = "fi.FileName";
                            break;

                        case "TYPE":
                            _sortby = "fi.FileExt";
                            break;

                        case "DATE":
                            _sortby = "fi.FileDate";
                            break;
                    }

                    sqlCmd.CommandText = "Select fi.FileName, fi.FileDescrip, fi.FileExt, fi.FileDate, fi.FileSize, fi.FileVersion, fi.FolderPath, fi.FileHash, fi.ShardName, fi.SegSize, fi.TotalSeg, fi.FolderGID, fi.GroupGID, fi.rec_gid, fi.row_id, fi.row_ms, ft.rec_gid AS FileTagGID" +
                                        " From Files fi" +
                                        " inner join FileTags ft on fi.rec_gid = ft.FileGID" +
                                        " where ft.TagGID = @TagGID" +
                                        " and fi.GroupGID IN (" + groupList + ")" +
                                        " and fi.rec_state = @RecState and ft.rec_state = @RecState";

                    sqlCmd.Parameters.Add("@TagGID", SqlDbType.VarChar, 50).Value = tagGID;
                    sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                    sqlCmd.CommandText += " Order By " + _sortby + " " + sortOrder;

                    sqlCmd.CommandText += " Offset " + _offset.ToString() + " Rows" +
                                        " Fetch Next " + pagesize + " Rows Only;";

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetHistory(string fileGID, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select FileName, FileDescrip, FileExt, FileSize, FileDate, FileVersion, FolderPath, FileHash, SegSize, TotalSeg, ShardName, row_id, rec_gid, row_ms, GroupGID, FolderGID" +
                                        " from Files" +
                                        " where rec_gid = @FileGID" +
                                        " and GroupGID IN (" + groupList + ")" +
                                        " order by row_id ASC;";

                    sqlCmd.Parameters.Add("@FileGID", SqlDbType.VarChar, 50).Value = fileGID;

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable RecoverByID(string fileGID, string rowID, string groupList)
        {
            DataTable dtresult = new DataTable();

            if (groupList != null && groupList != "")
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = "select *" +
                                        " from Files" +
                                        " where rec_gid = @rec_gid" +
                                        " and row_id = @RowID" +
                                        " and GroupGID IN (" + groupList + ");";

                    sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = fileGID;
                    sqlCmd.Parameters.Add("@RowID", SqlDbType.BigInt).Value = Convert.ToInt64(rowID);

                    DBUtil dBUtil = new DBUtil();
                    dtresult = dBUtil.Read(sqlCmd, _connstr);
                }
            }

            return dtresult;
        }

        /// <summary>
        /// query for the max src_id of records replicated in a destination table
        /// </summary>
        public string GetDestCounts(string srcdbname)
        {
            DBUtil dbUtil = new DBUtil();

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "Files", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "Files", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            return dbUtil.GetSrcRecCount(srcdbname, "Files", maxdestid, _connstr);
        }

        /// <summary>
        /// query for a batch of source records from the specified table
        /// </summary>
        public DataTable GetSrcRecs(string srcdbname, string placeholderid, string maxbatch)
        {
            string srcbatch = "10";
            if (maxbatch != null && maxbatch != "")
            {
                srcbatch = maxbatch;
            }

            DBUtil dBUtil = new DBUtil();
            return dBUtil.GetSrcRecs(srcdbname, "Files", placeholderid, srcbatch, _connstr);
        }

        /// <summary>
        /// query for the duplicate active records in the Files table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("Files", _connstr);

                if (idcheck.Rows.Count > 0)
                {
                    dupcheck = true;

                    string delim = "";
                    string duplist = "";
                    foreach (DataRow dupid in idcheck.Rows)
                    {
                        duplist += delim + dupid["row_id"].ToString();
                        delim = ",";
                    }

                    ServerErrLog.LogError(userName, webSvcName, "Files_read_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select FolderGID, FileName, FileExt, Count(*)" +
                                    " From Files" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By FolderGID, FileName, FileExt" +
                                    " Having Count(*) > 1;";

                sqlCmd.Parameters.Add("@ActiveRecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DataTable valcheck = dBUtil.Read(sqlCmd, _connstr);

                if (valcheck.Rows.Count > 0)
                {
                    dupcheck = true;

                    string delim = "";
                    string duplist = "";
                    foreach (DataRow dupval in valcheck.Rows)
                    {
                        duplist += delim + dupval["row_id"].ToString();
                        delim = ",";
                    }

                    ServerErrLog.LogError(userName, webSvcName, "Files_read_dml.DupeCheck", "Duplicate active values", duplist);
                }
            }

            return dupcheck;
        }

    }
}
