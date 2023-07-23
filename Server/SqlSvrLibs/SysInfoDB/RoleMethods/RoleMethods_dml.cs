using System;
using System.Data;
using System.Data.SqlClient;

using SqlSvrUtil;
using ApiUtil;
using SysMetricsDB;

namespace SysInfoDB.RoleMethods
{
    public class RoleMethods_dml
    {
        string _connstr;

        public RoleMethods_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetUserMethods(string userGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select distinct am.APIName, am.MethodName, am.VersionName" +
                                    " from APIMethods am" +
                                    " inner join RoleMethods rm on am.rec_gid = rm.MethodGID" +
                                    " inner join RoleUsers ru on rm.RoleGID = ru.RoleGID" +
                                    " where ru.UserGID = @UserGID" +
                                    " and am.rec_state = @RecState and rm.rec_state = @RecState and ru.rec_state = @RecState" +
                                    " order by am.APIName, am.MethodName, am.VersionName ASC;";

                sqlCmd.Parameters.Add("@UserGID", SqlDbType.VarChar, 50).Value = userGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetMethodRoles(string methodGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select r.RoleName, r.RoleDescrip" +
                                    " from APIRoles r" +
                                    " inner join RoleMethods rm on r.rec_gid = rm.RoleGID" +
                                    " where rm.MethodGID = @MethodGID" +
                                    " and r.rec_state = @RecState and rm.rec_state = @RecState" +
                                    " order by r.RoleName ASC;";

                sqlCmd.Parameters.Add("@MethodGID", SqlDbType.VarChar, 50).Value = methodGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAssigned(string roleGID)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select am.APIName, am.MethodName, am.VersionName, am.rec_gid as MethodGID, rm.rec_gid as RoleMethodGID" +
                                    " from APIMethods am" +
                                    " inner join RoleMethods rm on am.rec_gid = rm.MethodGID" +
                                    " where rm.RoleGID = @RoleGID" +
                                    " and am.rec_state = @RecState and rm.rec_state = @RecState" +
                                    " order by am.APIName, am.MethodName, am.VersionName ASC;";

                sqlCmd.Parameters.Add("@RoleGID", SqlDbType.VarChar, 50).Value = roleGID;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetAvailable(string roleGID, string apiName)
        {
            DataTable dtresult = new DataTable();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "select APIName, MethodName, VersionName, rec_gid as MethodGID" +
                                    " from APIMethods" +
                                    " where rec_state = @RecState" +
                                    " and APIName = @APIName" +
                                    " and rec_gid not in" +
                                    " (select MethodGID" +
                                    " from RoleMethods" +
                                    " where RoleGID = @RoleGID" +
                                    " and rec_state = @RecState)" +
                                    " order by APIName, MethodName, VersionName ASC;";

                sqlCmd.Parameters.Add("@RoleGID", SqlDbType.VarChar, 50).Value = roleGID;
                sqlCmd.Parameters.Add("@APIName", SqlDbType.VarChar, 50).Value = apiName;
                sqlCmd.Parameters.Add("@RecState", SqlDbType.VarChar, 10).Value = RecState.Active;

                DBUtil dBUtil = new DBUtil();
                dtresult = dBUtil.Read(sqlCmd, _connstr);
            }

            return dtresult;
        }

        /// <summary>
        /// query for the duplicate active records in the RoleMethods table
        /// </summary>
        public bool DupeCheck(string userName, string webSvcName)
        {
            bool dupcheck = false;

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                DBUtil dBUtil = new DBUtil();

                DataTable idcheck = dBUtil.DupeCheckByID("RoleMethods", _connstr);

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

                    ServerErrLog.LogError(userName, webSvcName, "RoleMethods_dml.DupeCheck", "Duplicate active rec_gid", duplist);
                }

                sqlCmd.CommandText = "Select RoleGID, MethodGID, Count(*)" +
                                    " From RoleMethods" +
                                    " Where rec_state = @ActiveRecState" +
                                    " Group By RoleGID, MethodGID" +
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

                    ServerErrLog.LogError(userName, webSvcName, "RoleMethods_dml.DupeCheck", "Duplicate active values", duplist);
                }
            }

            return dupcheck;
        }

        /// <summary>
        /// query for the max src_id of records replicated in a destination table
        /// </summary>
        public string GetDestCounts(string srcdbname)
        {
            DBUtil dbUtil = new DBUtil();

            string maxdestsrcid = dbUtil.GetMaxSrcID(srcdbname, "RoleMethods", _connstr);

            string destsrccount = dbUtil.GetDestRecCount(srcdbname, "RoleMethods", _connstr);

            return maxdestsrcid + "," + destsrccount;
        }

        /// <summary>
        /// query for the max row_id of records in a source table
        /// </summary>
        public string GetSrcCounts(string srcdbname, string maxdestid)
        {
            DBUtil dbUtil = new DBUtil();

            string srccount = dbUtil.GetSrcRecCount(srcdbname, "RoleMethods", "0", _connstr);

            string srcdestcount = dbUtil.GetSrcRecCount(srcdbname, "RoleMethods", maxdestid, _connstr);

            return srccount + "," + srcdestcount;
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
            return dBUtil.GetSrcRecs(srcdbname, "RoleMethods", placeholderid, srcbatch, _connstr);
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// 
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string rec_user,
                                string rolegid,
                                string methodgid)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                string rstate = RecState.Active;
                switch (actiontype)
                {
                    case ReplicaAction.Insert:
                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM RoleMethods WHERE (RoleGID = @RoleGID AND MethodGID = @MethodGID AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert RoleMethods (rec_gid, row_ms, rec_state, rec_user, src_ms, RoleGID, MethodGID) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @RoleGID, @MethodGID) END;";
                        break;

                    case ReplicaAction.Delete:
                        rstate = RecState.Deleted;

                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;

                        sqlCmd.CommandText = "UPDATE RoleMethods SET rec_state = @RecStateEdited WHERE RoleGID = @RoleGID AND MethodGID = @MethodGID AND rec_state = @RecStateActive;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM RoleMethods WHERE RoleGID = @RoleGID AND MethodGID = @MethodGID AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert RoleMethods (rec_gid, row_ms, rec_state, rec_user, src_ms, RoleGID, MethodGID) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @RoleGID, @MethodGID) END; END";
                        break;
                }

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@RoleGID", SqlDbType.VarChar, 50).Value = rolegid;
                sqlCmd.Parameters.Add("@MethodGID", SqlDbType.VarChar, 100).Value = methodgid;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReplicaWrite(sqlCmd, _connstr, actiontype);
            }

            return strresult;
        }

        /// <summary>
        /// row_id of replicated record is passed in as src_id ... row_id becomes src_id when replicated
        /// </summary>
        public string Replicate(string src_id,
                                string src_ms,
                                string rec_dbname,
                                string rec_gid,
                                string rec_state,
                                string rec_user,
                                string rolegid,
                                string methodgid,
                                string connstr)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@src_id", SqlDbType.BigInt).Value = Convert.ToInt64(src_id);
                sqlCmd.Parameters.Add("@src_ms", SqlDbType.BigInt).Value = Convert.ToInt64(src_ms);
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = msgUtil.UnixTimeLong();
                sqlCmd.Parameters.Add("@rec_dbname", SqlDbType.VarChar, 50).Value = rec_dbname;
                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rec_state;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@RoleGID", SqlDbType.VarChar, 50).Value = rolegid;
                sqlCmd.Parameters.Add("@MethodGID", SqlDbType.VarChar, 100).Value = methodgid;

                string dupcmd = "SELECT * FROM RoleMethods WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (RoleGID = @RoleGID AND MethodGID = @MethodGID AND rec_state = @RecStateActive)";

                string insertcmd = "Insert RoleMethods (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, RoleGID, MethodGID) Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @RoleGID, @MethodGID);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "RoleMethods", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }
    }
}
