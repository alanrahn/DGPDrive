using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SqlSvrUtil;
using ApiUtil;

namespace SysInfoDB.APIUser
{
    public class APIUser_write_dml
    {
        string _connstr;

        public APIUser_write_dml(string connStr)
        {
            _connstr = connStr;
        }

        /// <summary>
        /// low-level method called by the UserProc methods - generally should not be called directly by API mapper methods
        /// </summary>
        public string Write(string actiontype,
                                string rec_gid,
                                string rec_user,
                                string new_row_ms,
                                string edit_ms,
                                string username,
                                string password,
                                string accttype,
                                string acctstate,
                                string expiration,
                                string firstname,
                                string middlename,
                                string lastname,
                                string email,
                                string methlist,
                                string grpreadlist,
                                string grpwritelist,
                                string methodlimit)
        {
            string strresult = "";

            using (SqlCommand sqlCmd = new SqlCommand())
            {

                string rstate = RecState.Active;
                string tstate = RecState.Active;
                switch (actiontype)
                {
                    case ReplicaAction.Insert:

                        sqlCmd.CommandText = "DECLARE @dup INT SET @dup = (SELECT count(*) FROM APIUsers WHERE(UserName = @UserName AND rec_state = @RecStateActive) OR (rec_gid = @rec_gid AND rec_state = @RecStateActive));" +
                                            " IF(@dup = 0) BEGIN Insert APIUsers (rec_gid, row_ms, rec_state, rec_user, src_ms, UserName, Password, AccountType, AccountState, ExpireDate, FirstName, MiddleName, LastName, Email, MethodList, ReadList, WriteList, MethodLimit, SvcKeyVersion)" +
                                            " Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @UserName, @Password, @AccountType, @AccountState, @ExpireDate, @FirstName, @MiddleName, @LastName, @Email, @MethodList, @ReadList, @WriteList, @MethodLimit, @SvcKeyVersion) END;";
                        break;

                    case ReplicaAction.Update:
                    case ReplicaAction.Delete:
                    case ReplicaAction.Recover:

                        if (actiontype == ReplicaAction.Delete) rstate = RecState.Deleted;
                        if (actiontype == ReplicaAction.Recover) tstate = RecState.Deleted;

                        sqlCmd.Parameters.Add("@edit_ms", SqlDbType.BigInt).Value = Convert.ToInt64(edit_ms);
                        sqlCmd.Parameters.Add("@RecStateEdited", SqlDbType.VarChar, 10).Value = RecState.Edited;
                        sqlCmd.Parameters.Add("@TargetState", SqlDbType.VarChar, 10).Value = tstate;

                        sqlCmd.CommandText = "UPDATE APIUsers SET rec_state = @RecStateEdited WHERE rec_gid = @rec_gid AND rec_state = @TargetState AND row_ms = @edit_ms;" +
                                            " IF (@@ROWCOUNT > 0) BEGIN DECLARE @dup INT SET @dup = (SELECT count(*) FROM APIUsers WHERE UserName = @UserName AND rec_state = @RecStateActive);" +
                                            " IF(@dup = 0) BEGIN Insert APIUsers (rec_gid, row_ms, rec_state, rec_user, src_ms, UserName, Password, AccountType, AccountState, ExpireDate, FirstName, MiddleName, LastName, Email, MethodList, ReadList, WriteList, MethodLimit, SvcKeyVersion)" +
                                            " Values (@rec_gid, @row_ms, @rec_state, @rec_user, @row_ms, @UserName, @Password, @AccountType, @AccountState, @ExpireDate, @FirstName, @MiddleName, @LastName, @Email, @MethodList, @ReadList, @WriteList, @MethodLimit, @SvcKeyVersion) END; END";
                        break;
                }

                // always use the latest version of svckey to write new records
                string svckeyversion = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();
                string svcKey = ConfigurationManager.AppSettings[svckeyversion].ToString();
                EncryptUtil encryptUtil = new EncryptUtil();
                string encpword = encryptUtil.EncryptString(password, svcKey);

                MsgUtil msgUtil = new MsgUtil();

                sqlCmd.Parameters.Add("@RecStateActive", SqlDbType.VarChar, 10).Value = RecState.Active;

                sqlCmd.Parameters.Add("@rec_gid", SqlDbType.VarChar, 50).Value = rec_gid;
                sqlCmd.Parameters.Add("@row_ms", SqlDbType.BigInt).Value = Convert.ToInt64(new_row_ms);
                sqlCmd.Parameters.Add("@rec_state", SqlDbType.VarChar, 10).Value = rstate;
                sqlCmd.Parameters.Add("@rec_user", SqlDbType.VarChar, 50).Value = rec_user;

                sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username.ToLower();
                sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = encpword;
                sqlCmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 10).Value = accttype;
                sqlCmd.Parameters.Add("@AccountState", SqlDbType.VarChar, 10).Value = acctstate;
                sqlCmd.Parameters.Add("@ExpireDate", SqlDbType.BigInt).Value = Convert.ToInt64(expiration);
                sqlCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstname;
                sqlCmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = middlename;
                sqlCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastname;
                sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = email;
                sqlCmd.Parameters.Add("@MethodList", SqlDbType.VarChar, -1).Value = methlist;
                sqlCmd.Parameters.Add("@ReadList", SqlDbType.VarChar, 500).Value = grpreadlist;
                sqlCmd.Parameters.Add("@WriteList", SqlDbType.VarChar, 500).Value = grpwritelist;
                sqlCmd.Parameters.Add("@MethodLimit", SqlDbType.Int).Value = Convert.ToInt32(methodlimit);
                sqlCmd.Parameters.Add("@SvcKeyVersion", SqlDbType.VarChar, 10).Value = svckeyversion;

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.ReplicaWrite(sqlCmd, _connstr, actiontype);
            }

            return strresult;
        }

        /// <summary>
        /// row_id of replicated record is passed in as src_id ... row_id becomes src_id when replicated
        /// replication does not decrypt the password value, and replicates it in encrypted form
        /// </summary>
        public string Replicate(string src_id,
                                string src_ms,
                                string rec_dbname,
                                string rec_gid,
                                string rec_state,
                                string rec_user,
                                string username,
                                string password,
                                string accttype,
                                string acctstate,
                                string expiredate,
                                string firstname,
                                string middlename,
                                string lastname,
                                string email,
                                string methlist,
                                string grpreadlist,
                                string grpwritelist,
                                string methodlimit,
                                string svckeyversion,
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

                sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
                sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = password;
                sqlCmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 10).Value = accttype;
                sqlCmd.Parameters.Add("@AccountState", SqlDbType.VarChar, 10).Value = acctstate;
                sqlCmd.Parameters.Add("@ExpireDate", SqlDbType.BigInt).Value = Convert.ToInt64(expiredate);
                sqlCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstname;
                sqlCmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = middlename;
                sqlCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastname;
                sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = email;
                sqlCmd.Parameters.Add("@MethodList", SqlDbType.VarChar, -1).Value = methlist;
                sqlCmd.Parameters.Add("@ReadList", SqlDbType.VarChar, 500).Value = grpreadlist;
                sqlCmd.Parameters.Add("@WriteList", SqlDbType.VarChar, 500).Value = grpwritelist;
                sqlCmd.Parameters.Add("@MethodLimit", SqlDbType.Int).Value = Convert.ToInt32(methodlimit);
                sqlCmd.Parameters.Add("@SvcKeyVersion", SqlDbType.VarChar, 10).Value = svckeyversion;

                string dupcmd = "SELECT * FROM APIUsers WHERE (rec_dbname = @rec_dbname AND src_id = @src_id) OR (UserName = @UserName AND rec_state = @RecStateActive)";

                string insertcmd = "Insert APIUsers (rec_gid, row_ms, rec_state, rec_user, src_id, src_ms, rec_dbname, UserName, Password, AccountType, AccountState, ExpireDate, FirstName, MiddleName, LastName, Email, MethodList, ReadList, WriteList, MethodLimit, SvcKeyVersion)" +
                                    " Values (@rec_gid, @row_ms, @rec_state, @rec_user, @src_id, @src_ms, @rec_dbname, @UserName, @Password, @AccountType, @AccountState, @ExpireDate, @FirstName, @MiddleName, @LastName, @Email, @MethodList, @ReadList, @WriteList, @MethodLimit, @SvcKeyVersion);";

                DBUtil dBUtil = new DBUtil();
                strresult = dBUtil.MergeDestRec(sqlCmd, "APIUsers", dupcmd, insertcmd, connstr);
            }

            return strresult;
        }
    }
}
