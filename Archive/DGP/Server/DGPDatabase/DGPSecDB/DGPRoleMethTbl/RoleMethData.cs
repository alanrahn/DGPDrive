
using System;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class RoleMethData
    {
        string _dbconnstr;
        int _reccount = 0;
        int _addcount = 0;
        int _skipcount = 0;

        public RoleMethData(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        private void AddRoleMeth(string src_id,
                                string src_ms,
                                string rolename,
                                string fullname)
        {
            try
            {
                _reccount++;

                RoleMethWrite roleMethWrite = new RoleMethWrite(_dbconnstr);
                string tmpresult = roleMethWrite.Replicate(src_id, src_ms, MasterDB.SourceDB, src_id, RecState.Active, AcctNames.DGPAdmin, rolename, fullname, _dbconnstr);

                if (tmpresult == MethResult.OK)
                {
                    _addcount++;
                }
                else
                {
                    _skipcount++;
                }
            }
            catch (Exception ex)
            {
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "RoleMethData.AddRoleMeth", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ID range 120000
        /// </summary>
        public string AddSecData()
        {
            string src_ms = GenUtil.UnixTimeString(-3000);


            /*****************************************************************/
            /* Default Role                                                  */
            /*****************************************************************/

            AddRoleMeth("10000", src_ms, RoleNames.DefaultRole, UserSelfAPI.Login_base);
            AddRoleMeth("10001", src_ms, RoleNames.DefaultRole, UserSelfAPI.GetInfo_table);
            AddRoleMeth("10002", src_ms, RoleNames.DefaultRole, UserSelfAPI.GetGroups_table);
            AddRoleMeth("10003", src_ms, RoleNames.DefaultRole, UserSelfAPI.GetRoles_table);
            AddRoleMeth("10004", src_ms, RoleNames.DefaultRole, UserSelfAPI.GetUserGroupList_base);
            AddRoleMeth("10005", src_ms, RoleNames.DefaultRole, UserSelfAPI.Save_base);
            AddRoleMeth("10006", src_ms, RoleNames.DefaultRole, UserSelfAPI.ChangePassword_base);


            /*****************************************************************/
            /* Admin Role                                                    */
            /*****************************************************************/

            AddRoleMeth("10100", src_ms, RoleNames.AdminRole, MethAPI.GetPageSize_base);
            AddRoleMeth("10101", src_ms, RoleNames.AdminRole, MethAPI.GetAPIList_table);
            AddRoleMeth("10102", src_ms, RoleNames.AdminRole, MethAPI.GetCount_base);
            AddRoleMeth("10103", src_ms, RoleNames.AdminRole, MethAPI.GetSearch_table);
            AddRoleMeth("10104", src_ms, RoleNames.AdminRole, MethAPI.GetByID_table);
            AddRoleMeth("10105", src_ms, RoleNames.AdminRole, MethAPI.GetByName_table);
            AddRoleMeth("10106", src_ms, RoleNames.AdminRole, MethAPI.GetHistory_table);
            AddRoleMeth("10107", src_ms, RoleNames.AdminRole, MethAPI.GetSource_table);
            AddRoleMeth("10108", src_ms, RoleNames.AdminRole, MethAPI.GetDestCount_base);
            AddRoleMeth("10109", src_ms, RoleNames.AdminRole, MethAPI.GetSrcCount_base);
            AddRoleMeth("10110", src_ms, RoleNames.AdminRole, MethAPI.GetDupeCount_base);
            AddRoleMeth("10111", src_ms, RoleNames.AdminRole, MethAPI.New_base);
            AddRoleMeth("10112", src_ms, RoleNames.AdminRole, MethAPI.Save_base);
            AddRoleMeth("10113", src_ms, RoleNames.AdminRole, MethAPI.Delete_base);
            AddRoleMeth("10114", src_ms, RoleNames.AdminRole, MethAPI.Recover_base);
            AddRoleMeth("10115", src_ms, RoleNames.AdminRole, MethAPI.Replicate_base);

            AddRoleMeth("10200", src_ms, RoleNames.AdminRole, RoleAPI.GetPageSize_base);
            AddRoleMeth("10201", src_ms, RoleNames.AdminRole, RoleAPI.GetCount_base);
            AddRoleMeth("10202", src_ms, RoleNames.AdminRole, RoleAPI.GetSearch_table);
            AddRoleMeth("10203", src_ms, RoleNames.AdminRole, RoleAPI.GetByID_table);
            AddRoleMeth("10204", src_ms, RoleNames.AdminRole, RoleAPI.GetByName_table);
            AddRoleMeth("10205", src_ms, RoleNames.AdminRole, RoleAPI.GetHistory_table);
            AddRoleMeth("10206", src_ms, RoleNames.AdminRole, RoleAPI.GetSource_table);
            AddRoleMeth("10207", src_ms, RoleNames.AdminRole, RoleAPI.GetDestCount_base);
            AddRoleMeth("10208", src_ms, RoleNames.AdminRole, RoleAPI.GetSrcCount_base);
            AddRoleMeth("10209", src_ms, RoleNames.AdminRole, RoleAPI.GetDupeCount_base);
            AddRoleMeth("10210", src_ms, RoleNames.AdminRole, RoleAPI.New_base);
            AddRoleMeth("10211", src_ms, RoleNames.AdminRole, RoleAPI.Save_base);
            AddRoleMeth("10212", src_ms, RoleNames.AdminRole, RoleAPI.Delete_base);
            AddRoleMeth("10213", src_ms, RoleNames.AdminRole, RoleAPI.Recover_base);
            AddRoleMeth("10214", src_ms, RoleNames.AdminRole, RoleAPI.Replicate_base);

            AddRoleMeth("10300", src_ms, RoleNames.AdminRole, AcctAPI.GetPageSize_base);
            AddRoleMeth("10301", src_ms, RoleNames.AdminRole, AcctAPI.GetCount_base);
            AddRoleMeth("10302", src_ms, RoleNames.AdminRole, AcctAPI.GetSearch_table);
            AddRoleMeth("10303", src_ms, RoleNames.AdminRole, AcctAPI.GetByID_table);
            AddRoleMeth("10304", src_ms, RoleNames.AdminRole, AcctAPI.GetByName_table);
            AddRoleMeth("10305", src_ms, RoleNames.AdminRole, AcctAPI.CheckName_base);
            AddRoleMeth("10306", src_ms, RoleNames.AdminRole, AcctAPI.GetHistory_table);
            AddRoleMeth("10307", src_ms, RoleNames.AdminRole, AcctAPI.GetSource_table);
            AddRoleMeth("10308", src_ms, RoleNames.AdminRole, AcctAPI.GetDestCount_base);
            AddRoleMeth("10309", src_ms, RoleNames.AdminRole, AcctAPI.GetSrcCount_base);
            AddRoleMeth("10310", src_ms, RoleNames.AdminRole, AcctAPI.GetDupeCount_base);
            AddRoleMeth("10311", src_ms, RoleNames.AdminRole, AcctAPI.New_base);
            AddRoleMeth("10312", src_ms, RoleNames.AdminRole, AcctAPI.Save_base);
            AddRoleMeth("10313", src_ms, RoleNames.AdminRole, AcctAPI.Delete_base);
            AddRoleMeth("10314", src_ms, RoleNames.AdminRole, AcctAPI.Recover_base);
            AddRoleMeth("10315", src_ms, RoleNames.AdminRole, AcctAPI.Replicate_base);
            AddRoleMeth("10316", src_ms, RoleNames.AdminRole, AcctAPI.ChangePassword_base);

            AddRoleMeth("10400", src_ms, RoleNames.AdminRole, GroupAPI.GetPageSize_base);
            AddRoleMeth("10401", src_ms, RoleNames.AdminRole, GroupAPI.GetCount_base);
            AddRoleMeth("10402", src_ms, RoleNames.AdminRole, GroupAPI.GetSearch_table);
            AddRoleMeth("10403", src_ms, RoleNames.AdminRole, GroupAPI.GetByID_table);
            AddRoleMeth("10404", src_ms, RoleNames.AdminRole, GroupAPI.GetByName_table);
            AddRoleMeth("10405", src_ms, RoleNames.AdminRole, GroupAPI.GetHistory_table);
            AddRoleMeth("10406", src_ms, RoleNames.AdminRole, GroupAPI.GetSource_table);
            AddRoleMeth("10407", src_ms, RoleNames.AdminRole, GroupAPI.GetDestCount_base);
            AddRoleMeth("10408", src_ms, RoleNames.AdminRole, GroupAPI.GetSrcCount_base);
            AddRoleMeth("10409", src_ms, RoleNames.AdminRole, GroupAPI.GetDupeCount_base);
            AddRoleMeth("10410", src_ms, RoleNames.AdminRole, GroupAPI.New_base);
            AddRoleMeth("10411", src_ms, RoleNames.AdminRole, GroupAPI.Save_base);
            AddRoleMeth("10412", src_ms, RoleNames.AdminRole, GroupAPI.Delete_base);
            AddRoleMeth("10413", src_ms, RoleNames.AdminRole, GroupAPI.Recover_base);
            AddRoleMeth("10414", src_ms, RoleNames.AdminRole, GroupAPI.Replicate_base);

            AddRoleMeth("10500", src_ms, RoleNames.AdminRole, AcctRoleAPI.GetAssigned_table);
            AddRoleMeth("10501", src_ms, RoleNames.AdminRole, AcctRoleAPI.GetAvailable_table);
            AddRoleMeth("10502", src_ms, RoleNames.AdminRole, AcctRoleAPI.GetSource_table);
            AddRoleMeth("10503", src_ms, RoleNames.AdminRole, AcctRoleAPI.GetDestCount_base);
            AddRoleMeth("10504", src_ms, RoleNames.AdminRole, AcctRoleAPI.GetSrcCount_base);
            AddRoleMeth("10505", src_ms, RoleNames.AdminRole, AcctRoleAPI.GetDupeCount_base);
            AddRoleMeth("10506", src_ms, RoleNames.AdminRole, AcctRoleAPI.Assign_base);
            AddRoleMeth("10507", src_ms, RoleNames.AdminRole, AcctRoleAPI.Remove_base);
            AddRoleMeth("10508", src_ms, RoleNames.AdminRole, AcctRoleAPI.Replicate_base);

            AddRoleMeth("10600", src_ms, RoleNames.AdminRole, AcctGroupAPI.GetAssigned_table);
            AddRoleMeth("10601", src_ms, RoleNames.AdminRole, AcctGroupAPI.GetAvailable_table);
            AddRoleMeth("10602", src_ms, RoleNames.AdminRole, AcctGroupAPI.GetSource_table);
            AddRoleMeth("10603", src_ms, RoleNames.AdminRole, AcctGroupAPI.GetDestCount_base);
            AddRoleMeth("10604", src_ms, RoleNames.AdminRole, AcctGroupAPI.GetSrcCount_base);
            AddRoleMeth("10605", src_ms, RoleNames.AdminRole, AcctGroupAPI.GetDupeCount_base);
            AddRoleMeth("10606", src_ms, RoleNames.AdminRole, AcctGroupAPI.Assign_base);
            AddRoleMeth("10607", src_ms, RoleNames.AdminRole, AcctGroupAPI.Remove_base);
            AddRoleMeth("10608", src_ms, RoleNames.AdminRole, AcctGroupAPI.Replicate_base);

            AddRoleMeth("10700", src_ms, RoleNames.AdminRole, RoleMethAPI.GetAssigned_table);
            AddRoleMeth("10701", src_ms, RoleNames.AdminRole, RoleMethAPI.GetAvailable_table);
            AddRoleMeth("10702", src_ms, RoleNames.AdminRole, RoleMethAPI.GetSource_table);
            AddRoleMeth("10703", src_ms, RoleNames.AdminRole, RoleMethAPI.GetDestCount_base);
            AddRoleMeth("10704", src_ms, RoleNames.AdminRole, RoleMethAPI.GetSrcCount_base);
            AddRoleMeth("10705", src_ms, RoleNames.AdminRole, RoleMethAPI.GetDupeCount_base);
            AddRoleMeth("10706", src_ms, RoleNames.AdminRole, RoleMethAPI.GetUserMethods_table);
            AddRoleMeth("10707", src_ms, RoleNames.AdminRole, RoleMethAPI.GetMethRoles_table);
            AddRoleMeth("10708", src_ms, RoleNames.AdminRole, RoleMethAPI.Assign_base);
            AddRoleMeth("10709", src_ms, RoleNames.AdminRole, RoleMethAPI.Remove_base);
            AddRoleMeth("10710", src_ms, RoleNames.AdminRole, RoleMethAPI.Replicate_base);

            AddRoleMeth("10800", src_ms, RoleNames.AdminRole, FolderAPI.GetUserSubfolders_table);
            AddRoleMeth("10801", src_ms, RoleNames.AdminRole, FolderAPI.GetByID_table);
            AddRoleMeth("10802", src_ms, RoleNames.AdminRole, FolderAPI.GetSource_table);
            AddRoleMeth("10803", src_ms, RoleNames.AdminRole, FolderAPI.GetDupeCount_base);
            AddRoleMeth("10804", src_ms, RoleNames.AdminRole, FolderAPI.GetDestCount_base);
            AddRoleMeth("10805", src_ms, RoleNames.AdminRole, FolderAPI.GetSrcCount_base);
            AddRoleMeth("10806", src_ms, RoleNames.AdminRole, FolderAPI.AddSubfolder_base);
            AddRoleMeth("10807", src_ms, RoleNames.AdminRole, FolderAPI.Save_base);
            AddRoleMeth("10808", src_ms, RoleNames.AdminRole, FolderAPI.Delete_base);
            AddRoleMeth("10809", src_ms, RoleNames.AdminRole, FolderAPI.Replicate_base);

            AddRoleMeth("10900", src_ms, RoleNames.AdminRole, FileAPI.GetPageSize_base);
            AddRoleMeth("10901", src_ms, RoleNames.AdminRole, FileAPI.GetByID_table);
            AddRoleMeth("10902", src_ms, RoleNames.AdminRole, FileAPI.GetByName_table);
            AddRoleMeth("10903", src_ms, RoleNames.AdminRole, FileAPI.GetExtList_table);
            AddRoleMeth("10904", src_ms, RoleNames.AdminRole, FileAPI.GetHistory_table);
            AddRoleMeth("10905", src_ms, RoleNames.AdminRole, FileAPI.GetSource_table);
            AddRoleMeth("10906", src_ms, RoleNames.AdminRole, FileAPI.GetCountByFavorite_base);
            AddRoleMeth("10907", src_ms, RoleNames.AdminRole, FileAPI.GetCountByFolder_base);
            AddRoleMeth("10908", src_ms, RoleNames.AdminRole, FileAPI.GetCountByMetadata_base);
            AddRoleMeth("10909", src_ms, RoleNames.AdminRole, FileAPI.GetCountByTag_base);
            AddRoleMeth("10910", src_ms, RoleNames.AdminRole, FileAPI.GetFilesByFavorite_table);
            AddRoleMeth("10911", src_ms, RoleNames.AdminRole, FileAPI.GetFilesByFolder_table);
            AddRoleMeth("10912", src_ms, RoleNames.AdminRole, FileAPI.GetFilesByMetadata_table);
            AddRoleMeth("10913", src_ms, RoleNames.AdminRole, FileAPI.GetFilesByTag_table);
            AddRoleMeth("10914", src_ms, RoleNames.AdminRole, FileAPI.GetDupeCount_base);
            AddRoleMeth("10915", src_ms, RoleNames.AdminRole, FileAPI.GetDestCount_base);
            AddRoleMeth("10916", src_ms, RoleNames.AdminRole, FileAPI.GetSrcCount_base);
            AddRoleMeth("1917", src_ms, RoleNames.AdminRole, FileAPI.New_base);
            AddRoleMeth("10918", src_ms, RoleNames.AdminRole, FileAPI.Save_base);
            AddRoleMeth("10919", src_ms, RoleNames.AdminRole, FileAPI.Delete_base);
            AddRoleMeth("10920", src_ms, RoleNames.AdminRole, FileAPI.Replicate_base);
            AddRoleMeth("10921", src_ms, RoleNames.AdminRole, FileAPI.Recover_base);
            AddRoleMeth("10922", src_ms, RoleNames.AdminRole, FileAPI.Remove_base);
            AddRoleMeth("10923", src_ms, RoleNames.AdminRole, FileAPI.SaveFileRec_base);

            AddRoleMeth("11000", src_ms, RoleNames.AdminRole, TagAPI.GetPageSize_base);
            AddRoleMeth("11001", src_ms, RoleNames.AdminRole, TagAPI.GetByID_table);
            AddRoleMeth("11002", src_ms, RoleNames.AdminRole, TagAPI.GetByName_table);
            AddRoleMeth("11003", src_ms, RoleNames.AdminRole, TagAPI.GetCount_base);
            AddRoleMeth("11004", src_ms, RoleNames.AdminRole, TagAPI.GetSearch_table);
            AddRoleMeth("11005", src_ms, RoleNames.AdminRole, TagAPI.FilterByName_table);
            AddRoleMeth("11006", src_ms, RoleNames.AdminRole, TagAPI.GetHistory_table);
            AddRoleMeth("11007", src_ms, RoleNames.AdminRole, TagAPI.GetSource_table);
            AddRoleMeth("11008", src_ms, RoleNames.AdminRole, TagAPI.GetDestCount_base);
            AddRoleMeth("11009", src_ms, RoleNames.AdminRole, TagAPI.GetSrcCount_base);
            AddRoleMeth("11010", src_ms, RoleNames.AdminRole, TagAPI.GetDupeCount_base);
            AddRoleMeth("11011", src_ms, RoleNames.AdminRole, TagAPI.New_base);
            AddRoleMeth("11012", src_ms, RoleNames.AdminRole, TagAPI.Save_base);
            AddRoleMeth("11013", src_ms, RoleNames.AdminRole, TagAPI.Delete_base);
            AddRoleMeth("11014", src_ms, RoleNames.AdminRole, TagAPI.Recover_base);
            AddRoleMeth("11015", src_ms, RoleNames.AdminRole, TagAPI.Replicate_base);

            AddRoleMeth("11100", src_ms, RoleNames.AdminRole, FavoriteAPI.GetPageSize_base);
            AddRoleMeth("11101", src_ms, RoleNames.AdminRole, FavoriteAPI.GetSrcCount_base);
            AddRoleMeth("11102", src_ms, RoleNames.AdminRole, FavoriteAPI.GetDestCount_base);
            AddRoleMeth("11103", src_ms, RoleNames.AdminRole, FavoriteAPI.GetDupeCount_base);
            AddRoleMeth("11104", src_ms, RoleNames.AdminRole, FavoriteAPI.GetSource_table);
            AddRoleMeth("11105", src_ms, RoleNames.AdminRole, FavoriteAPI.Assign_base);
            AddRoleMeth("11106", src_ms, RoleNames.AdminRole, FavoriteAPI.Remove_base);
            AddRoleMeth("11107", src_ms, RoleNames.AdminRole, FavoriteAPI.Replicate_base);

            AddRoleMeth("11200", src_ms, RoleNames.AdminRole, FileTagAPI.GetPageSize_base);
            AddRoleMeth("11201", src_ms, RoleNames.AdminRole, FileTagAPI.GetSrcCount_base);
            AddRoleMeth("11202", src_ms, RoleNames.AdminRole, FileTagAPI.GetDestCount_base);
            AddRoleMeth("11203", src_ms, RoleNames.AdminRole, FileTagAPI.GetDupeCount_base);
            AddRoleMeth("11204", src_ms, RoleNames.AdminRole, FileTagAPI.GetSource_table);
            AddRoleMeth("11205", src_ms, RoleNames.AdminRole, FileTagAPI.GetAssigned_table);
            AddRoleMeth("11206", src_ms, RoleNames.AdminRole, FileTagAPI.GetAvailable_table);
            AddRoleMeth("11207", src_ms, RoleNames.AdminRole, FileTagAPI.Assign_base);
            AddRoleMeth("11208", src_ms, RoleNames.AdminRole, FileTagAPI.Remove_base);
            AddRoleMeth("11209", src_ms, RoleNames.AdminRole, FileTagAPI.Replicate_base);

            AddRoleMeth("11300", src_ms, RoleNames.AdminRole, FileSegAPI.GetByID_table);
            AddRoleMeth("11301", src_ms, RoleNames.AdminRole, FileSegAPI.GetByRowID_table);
            AddRoleMeth("11302", src_ms, RoleNames.AdminRole, FileSegAPI.GetSource_table);
            AddRoleMeth("11303", src_ms, RoleNames.AdminRole, FileSegAPI.GetDestCount_base);
            AddRoleMeth("11304", src_ms, RoleNames.AdminRole, FileSegAPI.GetSrcCount_base);
            AddRoleMeth("11305", src_ms, RoleNames.AdminRole, FileSegAPI.GetDupeCount_base);
            AddRoleMeth("11306", src_ms, RoleNames.AdminRole, FileSegAPI.GetDataBySegNum_base);
            AddRoleMeth("11307", src_ms, RoleNames.AdminRole, FileSegAPI.GetSegCount_base);
            AddRoleMeth("11308", src_ms, RoleNames.AdminRole, FileSegAPI.GetShardName_base);
            AddRoleMeth("11309", src_ms, RoleNames.AdminRole, FileSegAPI.New_base);
            AddRoleMeth("11310", src_ms, RoleNames.AdminRole, FileSegAPI.Delete_base);
            AddRoleMeth("11311", src_ms, RoleNames.AdminRole, FileSegAPI.Replicate_base);

            AddRoleMeth("11400", src_ms, RoleNames.AdminRole, ErrorAPI.GetPageSize_base);
            AddRoleMeth("11401", src_ms, RoleNames.AdminRole, ErrorAPI.GetByID_table);
            AddRoleMeth("11402", src_ms, RoleNames.AdminRole, ErrorAPI.GetCount_base);
            AddRoleMeth("11403", src_ms, RoleNames.AdminRole, ErrorAPI.GetSearch_table);
            AddRoleMeth("11404", src_ms, RoleNames.AdminRole, ErrorAPI.GetErrData_table);
            AddRoleMeth("11405", src_ms, RoleNames.AdminRole, ErrorAPI.New_base);
            AddRoleMeth("11406", src_ms, RoleNames.AdminRole, ErrorAPI.Delete_base);

            AddRoleMeth("11500", src_ms, RoleNames.AdminRole, MetricAPI.GetPageSize_base);
            AddRoleMeth("11501", src_ms, RoleNames.AdminRole, MetricAPI.GetByID_table);
            AddRoleMeth("11502", src_ms, RoleNames.AdminRole, MetricAPI.GetCount_base);
            AddRoleMeth("11503", src_ms, RoleNames.AdminRole, MetricAPI.GetSearch_table);
            AddRoleMeth("11504", src_ms, RoleNames.AdminRole, MetricAPI.New_base);
            AddRoleMeth("11505", src_ms, RoleNames.AdminRole, MetricAPI.Delete_base);

            AddRoleMeth("11600", src_ms, RoleNames.AdminRole, TestResultAPI.GetPageSize_base);
            AddRoleMeth("11601", src_ms, RoleNames.AdminRole, TestResultAPI.GetByID_table);
            AddRoleMeth("11602", src_ms, RoleNames.AdminRole, TestResultAPI.GetCount_base);
            AddRoleMeth("11603", src_ms, RoleNames.AdminRole, TestResultAPI.GetSearch_table);
            AddRoleMeth("11604", src_ms, RoleNames.AdminRole, TestResultAPI.GetEvalInfo_table);
            AddRoleMeth("11605", src_ms, RoleNames.AdminRole, TestResultAPI.New_base);
            AddRoleMeth("11606", src_ms, RoleNames.AdminRole, TestResultAPI.Delete_base);

            AddRoleMeth("11700", src_ms, RoleNames.AdminRole, ProcLogAPI.GetPageSize_base);
            AddRoleMeth("11701", src_ms, RoleNames.AdminRole, ProcLogAPI.GetByID_table);
            AddRoleMeth("11702", src_ms, RoleNames.AdminRole, ProcLogAPI.GetCount_base);
            AddRoleMeth("11703", src_ms, RoleNames.AdminRole, ProcLogAPI.GetSearch_table);
            AddRoleMeth("11705", src_ms, RoleNames.AdminRole, ProcLogAPI.GetProcSteps_table);
            AddRoleMeth("11706", src_ms, RoleNames.AdminRole, ProcLogAPI.New_base);
            AddRoleMeth("11707", src_ms, RoleNames.AdminRole, ProcLogAPI.Delete_base);

            AddRoleMeth("11800", src_ms, RoleNames.AdminRole, TestAPI.EchoTest_base);
            AddRoleMeth("11801", src_ms, RoleNames.AdminRole, TestAPI.LoggingTest_base);
            AddRoleMeth("11802", src_ms, RoleNames.AdminRole, TestAPI.ExceptionTest_base);
            AddRoleMeth("11803", src_ms, RoleNames.AdminRole, TestAPI.GetDBName_base);

            AddRoleMeth("11900", src_ms, RoleNames.AdminRole, ProcAPI.GetByID_table);
            AddRoleMeth("11901", src_ms, RoleNames.AdminRole, ProcAPI.GetCount_base);
            AddRoleMeth("11902", src_ms, RoleNames.AdminRole, ProcAPI.GetSearch_table);
            AddRoleMeth("11903", src_ms, RoleNames.AdminRole, ProcAPI.GetPageSize_base);
            AddRoleMeth("11904", src_ms, RoleNames.AdminRole, ProcAPI.QueueScan_base);
            AddRoleMeth("11905", src_ms, RoleNames.AdminRole, ProcAPI.New_base);
            AddRoleMeth("11906", src_ms, RoleNames.AdminRole, ProcAPI.Save_base);
            AddRoleMeth("11907", src_ms, RoleNames.AdminRole, ProcAPI.Delete_base);
            AddRoleMeth("11908", src_ms, RoleNames.AdminRole, ProcAPI.ClaimRecords_base);
            AddRoleMeth("11909", src_ms, RoleNames.AdminRole, ProcAPI.ReleaseRecords_base);
            AddRoleMeth("11910", src_ms, RoleNames.AdminRole, ProcAPI.Clone_base);


            return "<p>RoleMeth Core Data: " + _reccount.ToString() + " RECORDS, " + _addcount.ToString() + " Created, " + _skipcount.ToString() + " Skipped</p>";
        }


    }
}
