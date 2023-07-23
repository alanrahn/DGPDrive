
using System;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    /// <summary>
    /// Idempotent functionality to add and maintain core security records in each environment of a system
    /// as if they had been replicated from a (virtual) master database
    /// </summary>
    public class MethData
    {
        string _dbconnstr;
        int _reccount = 0;
        int _addcount = 0;
        int _skipcount = 0;

        public MethData(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        private void AddMethod(string src_id,
                                string src_ms,
                                string fullname,
                                string methoddescrip)
        {
            try
            {
                _reccount++;

                // parse fullname value to split apiname from rest of method name
                string[] seg = fullname.Split(new char[] { '.' }, 2);

                // src_id is reused as rec_gid value for easy identification of core security records
                MethWrite methWrite = new MethWrite(_dbconnstr);
                string tmpresult = methWrite.Replicate(src_id, src_ms, MasterDB.SourceDB, src_id, RecState.Active, AcctNames.DGPAdmin, fullname, seg[0], seg[1], methoddescrip, _dbconnstr);

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
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DBSetup", "MethData.AddMethod", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ID range 101000 to 119000
        /// </summary>
        public string AddSecData()
        {
            // source record timestamp is set to the past to avoid replacing any current edits
            string src_ms = GenUtil.UnixTimeString(-3000);

            // ***************************************************************** //
            // ***************************************************************** //
            // DGPSec Methods
            // ***************************************************************** //
            // ***************************************************************** //

            // GroupAPI methods
            AddMethod("10100", src_ms, GroupAPI.GetHistory_table, "Returns all versions of a DGPGroup record.");
            AddMethod("10101", src_ms, GroupAPI.GetByID_table, "Returns DGPGroup with matching ID.");
            AddMethod("10102", src_ms, GroupAPI.GetByName_table, "Returns DGPGroup with matching Name.");
            AddMethod("10103", src_ms, GroupAPI.GetCount_base, "Returns count of DGPGroup that match search criteria.");
            AddMethod("10104", src_ms, GroupAPI.GetSearch_table, "Returns DGPGroup records that match the search criteria.");
            AddMethod("10105", src_ms, GroupAPI.GetSource_table, "Returns DGPGroup source records that are greater than the placeholder value.");
            AddMethod("10106", src_ms, GroupAPI.New_base, "Creates a new DGPGroup record.");
            AddMethod("10107", src_ms, GroupAPI.Save_base, "Updates an existing DGPGroup record.");
            AddMethod("10108", src_ms, GroupAPI.Delete_base, "Deletes (updates) an existing DGPGroup record.");
            AddMethod("10109", src_ms, GroupAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10110", src_ms, GroupAPI.Recover_base, "Recovers an edited or deleted DGPGroup record.");
            AddMethod("10111", src_ms, GroupAPI.GetDupeCount_base, "Checks for duplicates in the DGPGroup table.");
            AddMethod("10112", src_ms, GroupAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10113", src_ms, GroupAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");
            AddMethod("10114", src_ms, GroupAPI.GetPageSize_base, "Returns the maximum number of records in a page of results.");

            // MethAPI methods
            AddMethod("10200", src_ms, MethAPI.GetHistory_table, "Returns all versions of an DGPMeth record.");
            AddMethod("10201", src_ms, MethAPI.GetByID_table, "Returns DGPMeth with matching ID.");
            AddMethod("10202", src_ms, MethAPI.GetByName_table, "Returns DGPMeth with matching Name.");
            AddMethod("10203", src_ms, MethAPI.GetCount_base, "Returns count of DGPMeth that match search criteria.");
            AddMethod("10204", src_ms, MethAPI.GetSearch_table, "Returns DGPMeth records that match the search criteria.");
            AddMethod("10205", src_ms, MethAPI.GetSource_table, "Returns DGPMeth source records that are greater than the placeholder value.");
            AddMethod("10206", src_ms, MethAPI.GetAPIList_table, "Returns a list of all current API names.");
            AddMethod("10207", src_ms, MethAPI.New_base, "Creates a new DGPMeth record.");
            AddMethod("10208", src_ms, MethAPI.Save_base, "Updates an existing DGPMeth record.");
            AddMethod("10209", src_ms, MethAPI.Delete_base, "Deletes (updates) an existing DGPMeth record.");
            AddMethod("10210", src_ms, MethAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10211", src_ms, MethAPI.Recover_base, "Recovers an edited or deleted DGPMeth record.");
            AddMethod("10212", src_ms, MethAPI.GetDupeCount_base, "Checks for duplicates in the DGPMeth table.");
            AddMethod("10213", src_ms, MethAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10214", src_ms, MethAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");
            AddMethod("10215", src_ms, MethAPI.GetPageSize_base, "Returns the maximum number or records in a page of results.");

            // RoleAPI methods
            AddMethod("10300", src_ms, RoleAPI.GetHistory_table, "Returns all versions of an DGPRole record.");
            AddMethod("10301", src_ms, RoleAPI.GetByID_table, "Returns DGPRole with matching ID.");
            AddMethod("10302", src_ms, RoleAPI.GetByName_table, "Returns DGPRole with matching Name.");
            AddMethod("10303", src_ms, RoleAPI.GetCount_base, "Returns count of DGPRole that match search criteria.");
            AddMethod("10304", src_ms, RoleAPI.GetSearch_table, "Returns DGPRole records that match the search criteria.");
            AddMethod("10305", src_ms, RoleAPI.GetSource_table, "Returns DGPRole source records that are greater than the placeholder value.");
            AddMethod("10306", src_ms, RoleAPI.New_base, "Creates a new DGPRole record.");
            AddMethod("10307", src_ms, RoleAPI.Save_base, "Updates an existing DGPRole record.");
            AddMethod("10308", src_ms, RoleAPI.Delete_base, "Deletes (updates) an existing DGPRole record.");
            AddMethod("10309", src_ms, RoleAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10310", src_ms, RoleAPI.Recover_base, "Recovers an edited or deleted DGPRole record.");
            AddMethod("10311", src_ms, RoleAPI.GetDupeCount_base, "Checks for duplicates in the DGPRole table.");
            AddMethod("10312", src_ms, RoleAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10313", src_ms, RoleAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");
            AddMethod("10314", src_ms, RoleAPI.GetPageSize_base, "Returns the maximum number of records in a page of results.");

            // AcctAPI methods
            AddMethod("10400", src_ms, AcctAPI.GetHistory_table, "Returns all versions of an DGPAcct record.");
            AddMethod("10401", src_ms, AcctAPI.GetByID_table, "Returns DGPAcct with matching ID.");
            AddMethod("10402", src_ms, AcctAPI.GetByName_table, "Returns DGPAcct with matching username.");
            AddMethod("10403", src_ms, AcctAPI.GetCount_base, "Returns count of DGPAcct that match search criteria.");
            AddMethod("10404", src_ms, AcctAPI.CheckName_base, "Checks if a acctname has already been used.");
            AddMethod("10405", src_ms, AcctAPI.GetSearch_table, "Returns DGPAcct records that match the search criteria.");
            AddMethod("10406", src_ms, AcctAPI.GetSource_table, "Returns DGPAcct source records that are greater than the placeholder value.");
            AddMethod("10407", src_ms, AcctAPI.New_base, "Creates a new DGPAcct record.");
            AddMethod("10408", src_ms, AcctAPI.Save_base, "Updates an existing DGPAcct record.");
            AddMethod("10409", src_ms, AcctAPI.Delete_base, "Deletes (updates) an existing DGPAcct record.");
            AddMethod("10410", src_ms, AcctAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10411", src_ms, AcctAPI.Recover_base, "Recovers an edited or deleted DGPAcct record.");
            AddMethod("10412", src_ms, AcctAPI.ChangePassword_base, "Admin method to update a DGPAcct record with a new password value.");
            AddMethod("10413", src_ms, AcctAPI.GetDupeCount_base, "Checks for duplicates in the DGPAcct table.");
            AddMethod("10414", src_ms, AcctAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10415", src_ms, AcctAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");
            AddMethod("10416", src_ms, AcctAPI.GetPageSize_base, "Returns the maximum number of records in a page of results.");

            // UserSelfAPI methods (AcctAPI functionality restricted to own user account)
            AddMethod("10500", src_ms, UserSelfAPI.Login_base, "Runs login process to perform lazy update of authorization data.");
            AddMethod("10501", src_ms, UserSelfAPI.GetRoles_table, "Returns a list of role membership names.");
            AddMethod("10502", src_ms, UserSelfAPI.GetGroups_table, "Returns two lists of group membership ID values.");
            AddMethod("10503", src_ms, UserSelfAPI.GetInfo_table, "Returns the data of the user's own account record.");
            AddMethod("10504", src_ms, UserSelfAPI.GetUserGroupList_base, "Returns a table of user datagroup membership records.");
            AddMethod("10505", src_ms, UserSelfAPI.Save_base, "User can only update their own account record.");
            AddMethod("10506", src_ms, UserSelfAPI.ChangePassword_base, "User can only change their own password value.");

            // ***************************************************************** //
            // ***************************************************************** //

            // RoleMethAPI methods
            AddMethod("10600", src_ms, RoleMethAPI.GetAssigned_table, "Returns methods that have been assigned to a role.");
            AddMethod("10601", src_ms, RoleMethAPI.GetAvailable_table, "Returns methods that have not been assigned to a role.");
            AddMethod("10602", src_ms, RoleMethAPI.GetSource_table, "Returns DGPRoleMeth source records that are greater than the placeholder value.");
            AddMethod("10603", src_ms, RoleMethAPI.GetUserMethods_table, "Returns the list of all methods a user account can access.");
            AddMethod("10604", src_ms, RoleMethAPI.GetMethRoles_table, "Returns the list of roles a method has been assigned to.");
            AddMethod("10605", src_ms, RoleMethAPI.Assign_base, "Creates a new DGPRoleMeth record.");
            AddMethod("10606", src_ms, RoleMethAPI.Remove_base, "Deletes (updates) an existing DGPRoleMeth record.");
            AddMethod("10607", src_ms, RoleMethAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10608", src_ms, RoleMethAPI.GetDupeCount_base, "Checks for duplicates in the DGPRoleMeth table.");
            AddMethod("10609", src_ms, RoleMethAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10610", src_ms, RoleMethAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");

            // AcctRoleAPI methods
            AddMethod("10700", src_ms, AcctRoleAPI.GetAssigned_table, "Returns roles that have been assigned to the account.");
            AddMethod("10701", src_ms, AcctRoleAPI.GetAvailable_table, "Returns roles that have not been assigned to the account.");
            AddMethod("10702", src_ms, AcctRoleAPI.GetSource_table, "Returns DGPAcctRole source records that are greater than the placeholder value.");
            AddMethod("10703", src_ms, AcctRoleAPI.Assign_base, "Creates a new DGPAcctRole record.");
            AddMethod("10704", src_ms, AcctRoleAPI.Remove_base, "Deletes (updates) an existing DGPAcctRole record.");
            AddMethod("10705", src_ms, AcctRoleAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10706", src_ms, AcctRoleAPI.GetDupeCount_base, "Checks for duplicates in the DGPAcctRole table.");
            AddMethod("10707", src_ms, AcctRoleAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10708", src_ms, AcctRoleAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");

            // AcctGroupAPI methods
            AddMethod("10800", src_ms, AcctGroupAPI.GetAssigned_table, "Returns all groups assigned to the account.");
            AddMethod("10801", src_ms, AcctGroupAPI.GetAvailable_table, "Returns groups that have not been assigned to the account.");
            AddMethod("10802", src_ms, AcctGroupAPI.GetSource_table, "Returns DGPAcctGroup source records that are greater than the placeholder value.");
            AddMethod("10803", src_ms, AcctGroupAPI.Assign_base, "Creates a new DGPAcctGroup record.");
            AddMethod("10804", src_ms, AcctGroupAPI.Remove_base, "Deletes (updates) an existing DGPAcctGroup record.");
            AddMethod("10805", src_ms, AcctGroupAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10806", src_ms, AcctGroupAPI.GetDupeCount_base, "Checks for duplicates in the DGPAcctGroup table.");
            AddMethod("10807", src_ms, AcctGroupAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10808", src_ms, AcctGroupAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");


            // ***************************************************************** //
            // ***************************************************************** //
            // DGPDrive Methods
            // ***************************************************************** //
            // ***************************************************************** //

            // FolderAPI methods 
            AddMethod("10900", src_ms, FolderAPI.GetUserSubfolders_table, "Returns all subfolders of a given folder that are visible to a user.");
            AddMethod("10901", src_ms, FolderAPI.GetByID_table, "Returns folder record with matching ID.");
            AddMethod("10902", src_ms, FolderAPI.GetSource_table, "Returns folder source records that are greater than the placeholder value.");
            AddMethod("10903", src_ms, FolderAPI.AddSubfolder_base, "Creates a new subfolder record.");
            AddMethod("10904", src_ms, FolderAPI.Save_base, "Updates an existing folder record.");
            AddMethod("10905", src_ms, FolderAPI.Delete_base, "Deletes (updates) an existing folder record.");
            AddMethod("10906", src_ms, FolderAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("10907", src_ms, FolderAPI.GetDupeCount_base, "Checks for duplicates in the Folder table.");
            AddMethod("10908", src_ms, FolderAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("10909", src_ms, FolderAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");

            // FileAPI methods
            AddMethod("11000", src_ms, FileAPI.Delete_base, "Soft delete (update) of a file record.");
            AddMethod("11001", src_ms, FileAPI.New_base, "Insert a new File metadata record.");
            AddMethod("11002", src_ms, FileAPI.Save_base, "Update of the file name in a file record.");
            AddMethod("11003", src_ms, FileAPI.GetByID_table, "Returns the matching file record using the global ID value.");
            AddMethod("11004", src_ms, FileAPI.GetByName_table, "Returns the matching file record using the file name value.");
            AddMethod("11005", src_ms, FileAPI.GetCountByFavorite_base, "Returns the count of favorite records for a user.");
            AddMethod("11006", src_ms, FileAPI.GetCountByFolder_base, "Returns count of file records that are linked to the specified folder.");
            AddMethod("11007", src_ms, FileAPI.GetCountByMetadata_base, "Returns a page of file records that match the metadata search criteria.");
            AddMethod("11008", src_ms, FileAPI.GetExtList_table, "Returns a list of all distinct file extensions.");
            AddMethod("11009", src_ms, FileAPI.GetFilesByFavorite_table, "Returns a page of favorite file records.");
            AddMethod("11010", src_ms, FileAPI.GetFilesByFolder_table, "Returns a page of file records linked to a specified folder.");
            AddMethod("11011", src_ms, FileAPI.GetFilesByMetadata_table, "Returns a page of file records that match the metadata search criteria.");
            AddMethod("11012", src_ms, FileAPI.GetHistory_table, "Returns all versions of a file regardless of record state.");
            AddMethod("11013", src_ms, FileAPI.GetSource_table, "Queries for a set of file records greater than the placeholder value");
            AddMethod("11014", src_ms, FileAPI.Recover_base, "Restores an edited or deleted record as the active record version.");
            AddMethod("11015", src_ms, FileAPI.Replicate_base, "Merges one or more file records into a destination database table.");
            AddMethod("11016", src_ms, FileAPI.GetCountByTag_base, "Returns count of file records that are linked to the specified tag.");
            AddMethod("11017", src_ms, FileAPI.GetFilesByTag_table, "Returns a page of file records linked to a specified tag.");
            AddMethod("11018", src_ms, FileAPI.GetDupeCount_base, "Checks the Files table for duplicates.");
            AddMethod("11019", src_ms, FileAPI.GetSrcCount_base, "Gets a count of source records in the source table.");
            AddMethod("11020", src_ms, FileAPI.GetDestCount_base, "Gets a count of source records in a destination table.");
            AddMethod("11021", src_ms, FileAPI.SaveFileRec_base, "Edits a few select fields in the File record, or deletes the File record.");
            AddMethod("11022", src_ms, FileAPI.Remove_base, "Removes a record from the system (ignored, not hard delete)");
            AddMethod("11023", src_ms, FileAPI.GetPageSize_base, "Returns the maximum number of records in a page of results");

            // TagAPI methods 
            AddMethod("11100", src_ms, TagAPI.GetByID_table, "Returns tag record with matching ID.");
            AddMethod("11101", src_ms, TagAPI.GetByName_table, "Returns tag record with matching Name.");
            AddMethod("11102", src_ms, TagAPI.FilterByName_table, "Returns tag records that match the search criteria.");
            AddMethod("11103", src_ms, TagAPI.GetSource_table, "Returns tag source records that are greater than the placeholder value.");
            AddMethod("11104", src_ms, TagAPI.New_base, "Creates a new tag record.");
            AddMethod("11105", src_ms, TagAPI.Save_base, "Updates an existing tag record.");
            AddMethod("11106", src_ms, TagAPI.Delete_base, "Deletes (updates) an existing tag record.");
            AddMethod("11107", src_ms, TagAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("11108", src_ms, TagAPI.GetDupeCount_base, "Checks the Tags table for duplicates.");
            AddMethod("11109", src_ms, TagAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("11110", src_ms, TagAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");
            AddMethod("11111", src_ms, TagAPI.GetPageSize_base, "Returns the maximum number of records in a page of results.");
            AddMethod("11112", src_ms, TagAPI.GetCount_base, "Returns the count of records that match the search criteria");
            AddMethod("11113", src_ms, TagAPI.GetSearch_table, "Returns a page of results that match the search criteria.");
            AddMethod("11114", src_ms, TagAPI.GetHistory_table, "Returns the maximum number of records in a page of results.");
            AddMethod("11115", src_ms, TagAPI.Recover_base, "Restores an edited or deleted record as the active record version.");

            // FileTagAPI methods
            AddMethod("11200", src_ms, FileTagAPI.GetAssigned_table, "Returns all tags linked to a file.");
            AddMethod("11201", src_ms, FileTagAPI.GetAvailable_table, "Returns tags that have not been assigned to a file .");
            AddMethod("11202", src_ms, FileTagAPI.GetSource_table, "Returns filetag source records that are greater than the placeholder value.");
            AddMethod("11203", src_ms, FileTagAPI.Assign_base, "Creates a new filetag record.");
            AddMethod("11204", src_ms, FileTagAPI.Remove_base, "Deletes (updates) an existing filetag record.");
            AddMethod("11205", src_ms, FileTagAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("11206", src_ms, FileTagAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("11207", src_ms, FileTagAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");
            AddMethod("11208", src_ms, FileTagAPI.GetDupeCount_base, "Checks for duplicates in the FileTag table.");

            // FavoriteAPI methods
            AddMethod("11300", src_ms, FavoriteAPI.Assign_base,"Assigns a file to a user favorite list.");
            AddMethod("11301", src_ms, FavoriteAPI.Remove_base, "Removes a file from a user favorite list.");
            AddMethod("11302", src_ms, FavoriteAPI.GetSource_table, "Returns Favorite source records that are greater than the placeholder value.");
            AddMethod("11303", src_ms, FavoriteAPI.Replicate_base, "Merges a replicated record into the destination table.");
            AddMethod("11304", src_ms, FavoriteAPI.GetDupeCount_base, "Checks the Favorites table for duplicates.");
            AddMethod("11305", src_ms, FavoriteAPI.GetDestCount_base, "Returns the count of the source records in the source table.");
            AddMethod("11306", src_ms, FavoriteAPI.GetSrcCount_base, "Returns the count of source records in a destination table.");
            AddMethod("11307", src_ms, FavoriteAPI.GetPageSize_base, "Returns the maximum number of records in a page of results");


            // ***************************************************************** //
            // ***************************************************************** //
            // DGPShard Methods
            // ***************************************************************** //
            // ***************************************************************** //

            // FileSegAPI methods
            AddMethod("11400", src_ms, FileSegAPI.Delete_base, "Soft delete of the FileShard record.");
            AddMethod("11401", src_ms, FileSegAPI.GetDupeCount_base.ToString(), "Checks the FileShard table for duplicates.");
            AddMethod("11402", src_ms, FileSegAPI.GetByID_table.ToString(), "Returns the FileShard record with the matching ID.");
            AddMethod("11403", src_ms, FileSegAPI.GetByRowID_table.ToString(), "Returns the FileShard record with the matching row_id.");
            AddMethod("11404", src_ms, FileSegAPI.GetDestCount_base.ToString(), "Returns the count of the source records in the source table.");
            AddMethod("11405", src_ms, FileSegAPI.GetDataBySegNum_base.ToString(), "Returns segment data from a specific FileShard record matching the FileGID and SegNum values.");
            AddMethod("11406", src_ms, FileSegAPI.GetSource_table.ToString(), "Returns FileShard source records that are greater than the placeholder value.");
            AddMethod("11407", src_ms, FileSegAPI.GetSrcCount_base.ToString(), "Returns the count of source records in a destination table.");
            AddMethod("11408", src_ms, FileSegAPI.New_base.ToString(), "Creates a new FileShard record.");
            AddMethod("11409", src_ms, FileSegAPI.Replicate_base.ToString(), "Merges a replicated record into the destination table.");
            AddMethod("11410", src_ms, FileSegAPI.GetShardName_base.ToString(), "Returns the name of a writable file shard.");
            AddMethod("11411", src_ms, FileSegAPI.GetSegCount_base.ToString(), "Returns the count of fileshard records for a file version in a shard.");


            // ***************************************************************** //
            // ***************************************************************** //
            // DGPData Methods
            // ***************************************************************** //
            // ***************************************************************** //

            // ErrorAPI methods
            AddMethod("11500", src_ms, ErrorAPI.New_base, "Creates new DGPError records.");
            AddMethod("11501", src_ms, ErrorAPI.GetByID_table, "Query for a DGPError record by ID value.");
            AddMethod("11503", src_ms, ErrorAPI.Delete_base, "Soft delete of a DGPError record.");
            AddMethod("11504", src_ms, ErrorAPI.GetCount_base, "Returns the count of records that match the search criteria.");
            AddMethod("11505", src_ms, ErrorAPI.GetSearch_table, "Returns a page of results that match the search criteria.");
            AddMethod("11506", src_ms, ErrorAPI.GetPageSize_base, "Returns the size of a page of results for the table.");
            AddMethod("11507", src_ms, ErrorAPI.GetErrData_table, "Returns the error data for a selected DGPError record.");

            // MetricAPI methods
            AddMethod("11600", src_ms, MetricAPI.New_base, "Creates new DGPMetric records.");
            AddMethod("11601", src_ms, MetricAPI.GetByID_table, "Query for a DGPMetric record by ID value.");
            AddMethod("11602", src_ms, MetricAPI.Delete_base, "Soft delete of a DGPMetric record.");
            AddMethod("11603", src_ms, MetricAPI.GetPageSize_base, "Returns the number of rows in a page of results.");
            AddMethod("11604", src_ms, MetricAPI.GetCount_base, "Returns the count of records that match the search criteria.");
            AddMethod("11605", src_ms, MetricAPI.GetSearch_table, "Returns the size of a page of results for the table.");

            // TestResultAPI methods
            AddMethod("11700", src_ms, TestResultAPI.New_base, "Creates new TestResult records.");
            AddMethod("11701", src_ms, TestResultAPI.GetByID_table, "Query for a TestResult record by ID value.");
            AddMethod("11702", src_ms, TestResultAPI.Delete_base, "Soft delete of a TestResult record.");
            AddMethod("11703", src_ms, TestResultAPI.GetCount_base, "Returns the count of records that match the search criteria.");
            AddMethod("11704", src_ms, TestResultAPI.GetSearch_table, "Returns a page of results that match the search criteria.");
            AddMethod("11705", src_ms, TestResultAPI.GetPageSize_base, "Returns the size of a page of results for the table.");
            AddMethod("11706", src_ms, TestResultAPI.GetEvalInfo_table, "Returns the evaluation info for a selected TestResult record.");

            // ProcLogAPI methods
            AddMethod("11800", src_ms, ProcLogAPI.New_base, "Creates new ProcLog records.");
            AddMethod("11801", src_ms, ProcLogAPI.GetByID_table, "Query for an ProcLog record by ID value.");
            AddMethod("11802", src_ms, ProcLogAPI.Delete_base, "Soft delete of an ProcLog record.");
            AddMethod("11803", src_ms, ProcLogAPI.GetCount_base, "Returns the count of records that match the search criteria.");
            AddMethod("11804", src_ms, ProcLogAPI.GetSearch_table, "Returns a page of results that match the search criteria.");
            AddMethod("11805", src_ms, ProcLogAPI.GetPageSize_base, "Returns the size of a page of results for the table.");
            AddMethod("11806", src_ms, ProcLogAPI.GetProcSteps_table, "Returns the process steps for a selected ProcLog record.");

            // TestAPI methods
            AddMethod("11900", src_ms, TestAPI.EchoTest_base, "Echos the input param data back to the calling application.");
            AddMethod("11901", src_ms, TestAPI.LoggingTest_base, "Tests the global logging functionality.");
            AddMethod("11902", src_ms, TestAPI.ExceptionTest_base, "Tests the exception handling functionality.");
            AddMethod("11903", src_ms, TestAPI.GetDBName_base, "Returns the database name from the connstr associated with a DB label input.");

            // ***************************************************************** //
            // ***************************************************************** //
            // DGPProc Methods
            // ***************************************************************** //
            // ***************************************************************** //

            // DGPProcAPI methods
            AddMethod("12000", src_ms, ProcAPI.GetCount_base, "Returns the count of DGPProc records that match the search criteria.");
            AddMethod("12001", src_ms, ProcAPI.GetSearch_table, "Returns a page of DGPProc records that match the search criteria.");
            AddMethod("12002", src_ms, ProcAPI.GetByID_table, "Returns the DGPProc record by global ID.");
            AddMethod("12003", src_ms, ProcAPI.New_base, "Creates a new DGPProc record.");
            AddMethod("12004", src_ms, ProcAPI.Save_base, "Saves (updates) an existing DGPProc record.");
            AddMethod("12005", src_ms, ProcAPI.Delete_base, "Deletes (updates) an existing DGPProc record.");
            AddMethod("12006", src_ms, ProcAPI.ClaimRecords_base, "Claims a batch of DGPProc records.");
            AddMethod("12007", src_ms, ProcAPI.ReleaseRecords_base, "Releases a claimed DGPProc record.");
            AddMethod("12008", src_ms, ProcAPI.Clone_base, "Creates a copy of an existing DGPProc record.");
            AddMethod("12009", src_ms, ProcAPI.GetPageSize_base, "Returns the maximum number of records in a page of results.");
            AddMethod("12010", src_ms, ProcAPI.QueueScan_base, "Scans for problems with the records in the DGPProc table.");

            // ***************************************************************** //

            return "<p>Method Core Data: " + _reccount.ToString() + " RECORDS, " + _addcount.ToString() + " Created, " + _skipcount.ToString() + " Skipped</p>";
        }
    }
}
