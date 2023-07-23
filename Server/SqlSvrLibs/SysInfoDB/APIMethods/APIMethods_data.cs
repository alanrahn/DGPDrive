using System;

using ApiUtil;
using SysInfoDB.APIUser;

namespace SysInfoDB.APIMethods
{
    public class APIMethods_data
    {
        string _dbconnstr;
        int reccount = 0;
        int scancount = 0;
        int skipcount = 0;

        public APIMethods_data(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        public string AddCoreData()
        {
            MsgUtil msgUtil = new MsgUtil();
            string src_ms = msgUtil.UnixTimeString(-3000);

            // ***************************************************************** //
            // DGPAdminAPI Methods - 1,000,000
            // ***************************************************************** //

            // DataGroup API methods 
            AddMethod(APIMethodID.GroupGetHistoryBaseID, src_ms, APIMethodID.GroupGetHistoryBaseID.ToString(), "DataGroup", "GetHistory", "Base", "Returns all versions of a datagroup record.");
            AddMethod(APIMethodID.GroupGetByIDBaseID, src_ms, APIMethodID.GroupGetByIDBaseID.ToString(), "DataGroup", "GetByID", "Base", "Returns datagroup with matching ID.");
            AddMethod(APIMethodID.GroupGetByNameBaseID, src_ms, APIMethodID.GroupGetByNameBaseID.ToString(), "DataGroup", "GetByName", "Base", "Returns datagroup with matching Name.");
            AddMethod(APIMethodID.GroupGetCountBaseID, src_ms, APIMethodID.GroupGetCountBaseID.ToString(), "DataGroup", "GetCount", "Base", "Returns count of datagroup that match search criteria.");
            AddMethod(APIMethodID.GroupGetSearchBaseID, src_ms, APIMethodID.GroupGetSearchBaseID.ToString(), "DataGroup", "GetSearch", "Base", "Returns datagroup records that match the search criteria.");
            AddMethod(APIMethodID.GroupGetSourceBaseID, src_ms, APIMethodID.GroupGetSourceBaseID.ToString(), "DataGroup", "GetSource", "Base", "Returns datagroup source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.GroupNewBaseID, src_ms, APIMethodID.GroupNewBaseID.ToString(), "DataGroup", "New", "Base", "Creates a new datagroup record.");
            AddMethod(APIMethodID.GroupSaveBaseID, src_ms, APIMethodID.GroupSaveBaseID.ToString(), "DataGroup", "Save", "Base", "Updates an existing datagroup record.");
            AddMethod(APIMethodID.GroupDeleteBaseID, src_ms, APIMethodID.GroupDeleteBaseID.ToString(), "DataGroup", "Delete", "Base", "Deletes (updates) an existing datagroup record.");
            AddMethod(APIMethodID.GroupReplicateBaseID, src_ms, APIMethodID.GroupReplicateBaseID.ToString(), "DataGroup", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.GroupRecoverBaseID, src_ms, APIMethodID.GroupRecoverBaseID.ToString(), "DataGroup", "Recover", "Base", "Recovers an edited or deleted datagroup record.");
            AddMethod(APIMethodID.GroupDuplicateBaseID, src_ms, APIMethodID.GroupDuplicateBaseID.ToString(), "DataGroup", "Duplicate", "Base", "Checks for duplicates in the DataGroup table.");
            AddMethod(APIMethodID.GroupGetDestCountsBaseID, src_ms, APIMethodID.GroupGetDestCountsBaseID.ToString(), "DataGroup", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.GroupGetSrcCountsBaseID, src_ms, APIMethodID.GroupGetSrcCountsBaseID.ToString(), "DataGroup", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.GroupGetPageSizeBaseID, src_ms, APIMethodID.GroupGetPageSizeBaseID.ToString(), "DataGroup", "GetPageSize", "Base", "Returns the maximum number of records in a page of results.");

            // GroupUser API methods
            AddMethod(APIMethodID.GroupUserGetAssignedBaseID, src_ms, APIMethodID.GroupUserGetAssignedBaseID.ToString(), "GroupUser", "GetAssigned", "Base", "Returns all groupuser groups assigned to a user.");
            AddMethod(APIMethodID.GroupUserGetAvailableBaseID, src_ms, APIMethodID.GroupUserGetAvailableBaseID.ToString(), "GroupUser", "GetAvailable", "Base", "Returns groupuser groups that have not been assigned to a user .");
            AddMethod(APIMethodID.GroupUserGetSourceBaseID, src_ms, APIMethodID.GroupUserGetSourceBaseID.ToString(), "GroupUser", "GetSource", "Base", "Returns groupuser source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.GroupUserNewBaseID, src_ms, APIMethodID.GroupUserNewBaseID.ToString(), "GroupUser", "Assign", "Base", "Creates a new groupuser record.");
            AddMethod(APIMethodID.GroupUserDeleteBaseID, src_ms, APIMethodID.GroupUserDeleteBaseID.ToString(), "GroupUser", "Remove", "Base", "Deletes (updates) an existing groupuser record.");
            AddMethod(APIMethodID.GroupUserReplicateBaseID, src_ms, APIMethodID.GroupUserReplicateBaseID.ToString(), "GroupUser", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.GroupUserDuplicateBaseID, src_ms, APIMethodID.GroupUserDuplicateBaseID.ToString(), "GroupUser", "Duplicate", "Base", "Checks for duplicates in the GroupUser table.");
            AddMethod(APIMethodID.GroupUserGetDestCountsBaseID, src_ms, APIMethodID.GroupUserGetDestCountsBaseID.ToString(), "GroupUser", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.GroupUserGetSrcCountsBaseID, src_ms, APIMethodID.GroupUserGetSrcCountsBaseID.ToString(), "GroupUser", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");

            // APIMethod API methods 
            AddMethod(APIMethodID.MethodGetHistoryBaseID, src_ms, APIMethodID.MethodGetHistoryBaseID.ToString(), "APIMethod", "GetHistory", "Base", "Returns all versions of an apimethod record.");
            AddMethod(APIMethodID.MethodGetByIDBaseID, src_ms, APIMethodID.MethodGetByIDBaseID.ToString(), "APIMethod", "GetByID", "Base", "Returns apimethod with matching ID.");
            AddMethod(APIMethodID.MethodGetByNameBaseID, src_ms, APIMethodID.MethodGetByNameBaseID.ToString(), "APIMethod", "GetByName", "Base", "Returns apimethod with matching Name.");
            AddMethod(APIMethodID.MethodGetCountBaseID, src_ms, APIMethodID.MethodGetCountBaseID.ToString(), "APIMethod", "GetCount", "Base", "Returns count of apimethod that match search criteria.");
            AddMethod(APIMethodID.MethodGetSearchBaseID, src_ms, APIMethodID.MethodGetSearchBaseID.ToString(), "APIMethod", "GetSearch", "Base", "Returns apimethod records that match the search criteria.");
            AddMethod(APIMethodID.MethodGetSourceBaseID, src_ms, APIMethodID.MethodGetSourceBaseID.ToString(), "APIMethod", "GetSource", "Base", "Returns apimethod source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.MethodGetAPIListBaseID, src_ms, APIMethodID.MethodGetAPIListBaseID.ToString(), "APIMethod", "GetAPIList", "Base", "Returns a list of all current API names.");
            AddMethod(APIMethodID.MethodNewBaseID, src_ms, APIMethodID.MethodNewBaseID.ToString(), "APIMethod", "New", "Base", "Creates a new apimethod record.");
            AddMethod(APIMethodID.MethodSaveBaseID, src_ms, APIMethodID.MethodSaveBaseID.ToString(), "APIMethod", "Save", "Base", "Updates an existing apimethod record.");
            AddMethod(APIMethodID.MethodDeleteBaseID, src_ms, APIMethodID.MethodDeleteBaseID.ToString(), "APIMethod", "Delete", "Base", "Deletes (updates) an existing apimethod record.");
            AddMethod(APIMethodID.MethodReplicateBaseID, src_ms, APIMethodID.MethodReplicateBaseID.ToString(), "APIMethod", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.MethodRecoverBaseID, src_ms, APIMethodID.MethodRecoverBaseID.ToString(), "APIMethod", "Recover", "Base", "Recovers an edited or deleted apimethod record.");
            AddMethod(APIMethodID.MethodDuplicateBaseID, src_ms, APIMethodID.MethodDuplicateBaseID.ToString(), "APIMethod", "Duplicate", "Base", "Checks for duplicates in the APIMethod table.");
            AddMethod(APIMethodID.MethodGetDestCountsBaseID, src_ms, APIMethodID.MethodGetDestCountsBaseID.ToString(), "APIMethod", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.MethodGetSrcCountsBaseID, src_ms, APIMethodID.MethodGetSrcCountsBaseID.ToString(), "APIMethod", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.MethodGetPageSizeBaseID, src_ms, APIMethodID.MethodGetPageSizeBaseID.ToString(), "APIMethod", "GetPageSize", "Base", "Returns the maximum number or records in a page of results.");

            // RoleMethod API methods
            AddMethod(APIMethodID.RoleMethodGetAssignedBaseID, src_ms, APIMethodID.RoleMethodGetAssignedBaseID.ToString(), "RoleMethod", "GetAssigned", "Base", "Returns apimethods that have been assigned to an apirole.");
            AddMethod(APIMethodID.RoleMethodGetAvailableBaseID, src_ms, APIMethodID.RoleMethodGetAvailableBaseID.ToString(), "RoleMethod", "GetAvailable", "Base", "Returns apimethods that have not been assigned to an apirole.");
            AddMethod(APIMethodID.RoleMethodGetSourceBaseID, src_ms, APIMethodID.RoleMethodGetSourceBaseID.ToString(), "RoleMethod", "GetSource", "Base", "Returns rolemethod source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.RoleMethodGetUserMethodsBaseID, src_ms, APIMethodID.RoleMethodGetUserMethodsBaseID.ToString(), "RoleMethod", "GetUserMethods", "Base", "Returns the list of all apimethods a user account can access.");
            AddMethod(APIMethodID.RoleMethodGetMethodRolesBaseID, src_ms, APIMethodID.RoleMethodGetMethodRolesBaseID.ToString(), "RoleMethod", "GetMethodRoles", "Base", "Returns the list of apiroles an apimethod has been assigned to.");
            AddMethod(APIMethodID.RoleMethodNewBaseID, src_ms, APIMethodID.RoleMethodNewBaseID.ToString(), "RoleMethod", "Assign", "Base", "Creates a new rolemethod record.");
            AddMethod(APIMethodID.RoleMethodDeleteBaseID, src_ms, APIMethodID.RoleMethodDeleteBaseID.ToString(), "RoleMethod", "Remove", "Base", "Deletes (updates) an existing rolemethod record.");
            AddMethod(APIMethodID.RoleMethodReplicateBaseID, src_ms, APIMethodID.RoleMethodReplicateBaseID.ToString(), "RoleMethod", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.RoleMethodDuplicateBaseID, src_ms, APIMethodID.RoleMethodDuplicateBaseID.ToString(), "RoleMethod", "Duplicate", "Base", "Checks for duplicates in the RoleMethod table.");
            AddMethod(APIMethodID.RoleMethodGetDestCountsBaseID, src_ms, APIMethodID.RoleMethodGetDestCountsBaseID.ToString(), "RoleMethod", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.RoleMethodGetSrcCountsBaseID, src_ms, APIMethodID.RoleMethodGetSrcCountsBaseID.ToString(), "RoleMethod", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");

            // APIRole API method 
            AddMethod(APIMethodID.RoleGetHistoryBaseID, src_ms, APIMethodID.RoleGetHistoryBaseID.ToString(), "APIRole", "GetHistory", "Base", "Returns all versions of an apirole record.");
            AddMethod(APIMethodID.RoleGetByIDBaseID, src_ms, APIMethodID.RoleGetByIDBaseID.ToString(), "APIRole", "GetByID", "Base", "Returns apirole with matching ID.");
            AddMethod(APIMethodID.RoleGetByNameBaseID, src_ms, APIMethodID.RoleGetByNameBaseID.ToString(), "APIRole", "GetByName", "Base", "Returns apirole with matching Name.");
            AddMethod(APIMethodID.RoleGetCountBaseID, src_ms, APIMethodID.RoleGetCountBaseID.ToString(), "APIRole", "GetCount", "Base", "Returns count of apirole that match search criteria.");
            AddMethod(APIMethodID.RoleGetSearchBaseID, src_ms, APIMethodID.RoleGetSearchBaseID.ToString(), "APIRole", "GetSearch", "Base", "Returns apirole records that match the search criteria.");
            AddMethod(APIMethodID.RoleGetSourceBaseID, src_ms, APIMethodID.RoleGetSourceBaseID.ToString(), "APIRole", "GetSource", "Base", "Returns apirole source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.RoleNewBaseID, src_ms, APIMethodID.RoleNewBaseID.ToString(), "APIRole", "New", "Base", "Creates a new apirole record.");
            AddMethod(APIMethodID.RoleSaveBaseID, src_ms, APIMethodID.RoleSaveBaseID.ToString(), "APIRole", "Save", "Base", "Updates an existing apirole record.");
            AddMethod(APIMethodID.RoleDeleteBaseID, src_ms, APIMethodID.RoleDeleteBaseID.ToString(), "APIRole", "Delete", "Base", "Deletes (updates) an existing apirole record.");
            AddMethod(APIMethodID.RoleReplicateBaseID, src_ms, APIMethodID.RoleReplicateBaseID.ToString(), "APIRole", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.RoleRecoverBaseID, src_ms, APIMethodID.RoleRecoverBaseID.ToString(), "APIRole", "Recover", "Base", "Recovers an edited or deleted apirole record.");
            AddMethod(APIMethodID.RoleDuplicateBaseID, src_ms, APIMethodID.RoleDuplicateBaseID.ToString(), "APIRole", "Duplicate", "Base", "Checks for duplicates in the APIRole table.");
            AddMethod(APIMethodID.RoleGetDestCountsBaseID, src_ms, APIMethodID.RoleGetDestCountsBaseID.ToString(), "APIRole", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.RoleGetSrcCountsBaseID, src_ms, APIMethodID.RoleGetSrcCountsBaseID.ToString(), "APIRole", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.RoleGetPageSizeBaseID, src_ms, APIMethodID.RoleGetPageSizeBaseID.ToString(), "APIRole", "GetPageSize", "Base", "Returns the maximum number of records in a page of results.");

            // RoleUser API methods
            AddMethod(APIMethodID.RoleUserGetAssignedBaseID, src_ms, APIMethodID.RoleUserGetAssignedBaseID.ToString(), "RoleUser", "GetAssigned", "Base", "Returns apiroles that have been assigned to an apiuser.");
            AddMethod(APIMethodID.RoleUserGetAvailableBaseID, src_ms, APIMethodID.RoleUserGetAvailableBaseID.ToString(), "RoleUser", "GetAvailable", "Base", "Returns apiroles that have not been assigned to an apiuser.");
            AddMethod(APIMethodID.RoleUserGetSourceBaseID, src_ms, APIMethodID.RoleUserGetSourceBaseID.ToString(), "RoleUser", "GetSource", "Base", "Returns roleuser source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.RoleUserNewBaseID, src_ms, APIMethodID.RoleUserNewBaseID.ToString(), "RoleUser", "Assign", "Base", "Creates a new roleuser record.");
            AddMethod(APIMethodID.RoleUserDeleteBaseID, src_ms, APIMethodID.RoleUserDeleteBaseID.ToString(), "RoleUser", "Remove", "Base", "Deletes (updates) an existing roleuser record.");
            AddMethod(APIMethodID.RoleUserReplicateBaseID, src_ms, APIMethodID.RoleUserReplicateBaseID.ToString(), "RoleUser", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.RoleUserDuplicateBaseID, src_ms, APIMethodID.RoleUserDuplicateBaseID.ToString(), "RoleUser", "Duplicate", "Base", "Checks for duplicates in the RoleUser table.");
            AddMethod(APIMethodID.RoleUserGetDestCountsBaseID, src_ms, APIMethodID.RoleUserGetDestCountsBaseID.ToString(), "RoleUser", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.RoleUserGetSrcCountsBaseID, src_ms, APIMethodID.RoleUserGetSrcCountsBaseID.ToString(), "RoleUser", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");

            // APIUser API methods
            AddMethod(APIMethodID.UserGetHistoryBaseID, src_ms, APIMethodID.UserGetHistoryBaseID.ToString(), "APIUser", "GetHistory", "Base", "Returns all versions of an apiuser record.");
            AddMethod(APIMethodID.UserGetByIDBaseID, src_ms, APIMethodID.UserGetByIDBaseID.ToString(), "APIUser", "GetByID", "Base", "Returns apiuser with matching ID.");
            AddMethod(APIMethodID.UserGetByNameBaseID, src_ms, APIMethodID.UserGetByNameBaseID.ToString(), "APIUser", "GetByName", "Base", "Returns apiuser with matching username.");
            AddMethod(APIMethodID.UserGetCountBaseID, src_ms, APIMethodID.UserGetCountBaseID.ToString(), "APIUser", "GetCount", "Base", "Returns count of apiusers that match search criteria.");
            AddMethod(APIMethodID.UserCheckNameBaseID, src_ms, APIMethodID.UserCheckNameBaseID.ToString(), "APIUser", "CheckName", "Base", "Checks if a username has already been used.");
            AddMethod(APIMethodID.UserGetSearchBaseID, src_ms, APIMethodID.UserGetSearchBaseID.ToString(), "APIUser", "GetSearch", "Base", "Returns apiuser records that match the search criteria.");
            AddMethod(APIMethodID.UserGetSourceBaseID, src_ms, APIMethodID.UserGetSourceBaseID.ToString(), "APIUser", "GetSource", "Base", "Returns apiuser source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.UserNewBaseID, src_ms, APIMethodID.UserNewBaseID.ToString(), "APIUser", "New", "Base", "Creates a new apiuser record.");
            AddMethod(APIMethodID.UserSaveBaseID, src_ms, APIMethodID.UserSaveBaseID.ToString(), "APIUser", "Save", "Base", "Updates an existing apiuser record.");
            AddMethod(APIMethodID.UserDeleteBaseID, src_ms, APIMethodID.UserDeleteBaseID.ToString(), "APIUser", "Delete", "Base", "Deletes (updates) an existing apiuser record.");
            AddMethod(APIMethodID.UserReplicateBaseID, src_ms, APIMethodID.UserReplicateBaseID.ToString(), "APIUser", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.UserRecoverBaseID, src_ms, APIMethodID.UserRecoverBaseID.ToString(), "APIUser", "Recover", "Base", "Recovers an edited or deleted apiuser record.");
            AddMethod(APIMethodID.UserChangePasswordBaseID, src_ms, APIMethodID.UserChangePasswordBaseID.ToString(), "APIUser", "ChangePassword", "Base", "Admin method to update a user account record with a new password value.");
            AddMethod(APIMethodID.UserDuplicateBaseID, src_ms, APIMethodID.UserDuplicateBaseID.ToString(), "APIUser", "Duplicate", "Base", "Checks for duplicates in the APIUser table.");
            AddMethod(APIMethodID.UserGetDestCountsBaseID, src_ms, APIMethodID.UserGetDestCountsBaseID.ToString(), "APIUser", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.UserGetSrcCountsBaseID, src_ms, APIMethodID.UserGetSrcCountsBaseID.ToString(), "APIUser", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.UserGetPageSizeBaseID, src_ms, APIMethodID.UserGetPageSizeBaseID.ToString(), "APIUser", "GetPageSize", "Base", "Returns the maximum number of records in a page of results.");

            // UserSelf API method
            AddMethod(APIMethodID.UserSelfLoginBaseID, src_ms, APIMethodID.UserSelfLoginBaseID.ToString(), "UserSelf", "Login", "Base", "Runs login process to perform lazy update of authorization data.");
            AddMethod(APIMethodID.UserSelfGetRolesID, src_ms, APIMethodID.UserSelfGetRolesID.ToString(), "UserSelf", "GetRoles", "Base", "Returns a list of role membership names.");
            AddMethod(APIMethodID.UserSelfGetGroupsID, src_ms, APIMethodID.UserSelfGetGroupsID.ToString(), "UserSelf", "GetGroups", "Base", "Returns two lists of datagroup membership ID values.");
            AddMethod(APIMethodID.UserSelfGetInfoID, src_ms, APIMethodID.UserSelfGetInfoID.ToString(), "UserSelf", "GetInfo", "Base", "Returns the data of the user's own account record.");
            AddMethod(APIMethodID.UserSelfGetUserGroupListBaseID, src_ms, APIMethodID.UserSelfGetUserGroupListBaseID.ToString(), "UserSelf", "GetUserGroupList", "Base", "Returns a table of user datagroup membership records.");
            AddMethod(APIMethodID.UserSelfSaveBaseID, src_ms, APIMethodID.UserSelfSaveBaseID.ToString(), "UserSelf", "Save", "Base", "User can only update their own account record.");
            AddMethod(APIMethodID.UserSelfChangePasswordBaseID, src_ms, APIMethodID.UserSelfChangePasswordBaseID.ToString(), "UserSelf", "ChangePassword", "Base", "User can only change their own password value.");

            // ***************************************************************** //
            // DGPFileStoreAPI Methods
            // ***************************************************************** //

            // Folder API methods 
            AddMethod(APIMethodID.FolderGetUserSubFoldersBaseID, src_ms, APIMethodID.FolderGetUserSubFoldersBaseID.ToString(), "Folder", "GetUserSubFolders", "Base", "Returns all subfolders of a given folder that are visible to a user.");
            AddMethod(APIMethodID.FolderGetByIDBaseID, src_ms, APIMethodID.FolderGetByIDBaseID.ToString(), "Folder", "GetByID", "Base", "Returns folder record with matching ID.");
            AddMethod(APIMethodID.FolderGetSourceBaseID, src_ms, APIMethodID.FolderGetSourceBaseID.ToString(), "Folder", "GetSource", "Base", "Returns folder source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.FolderAddSubFolderBaseID, src_ms, APIMethodID.FolderAddSubFolderBaseID.ToString(), "Folder", "AddSubFolder", "Base", "Creates a new subfolder record.");
            AddMethod(APIMethodID.FolderSaveBaseID, src_ms, APIMethodID.FolderSaveBaseID.ToString(), "Folder", "Save", "Base", "Updates an existing folder record.");
            AddMethod(APIMethodID.FolderDeleteBaseID, src_ms, APIMethodID.FolderDeleteBaseID.ToString(), "Folder", "Delete", "Base", "Deletes (updates) an existing folder record.");
            AddMethod(APIMethodID.FolderReplicateBaseID, src_ms, APIMethodID.FolderReplicateBaseID.ToString(), "Folder", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.FolderDuplicateBaseID, src_ms, APIMethodID.FolderDuplicateBaseID.ToString(), "Folder", "Duplicate", "Base", "Checks for duplicates in the Folder table.");
            AddMethod(APIMethodID.FolderGetDestCountsBaseID, src_ms, APIMethodID.FolderGetDestCountsBaseID.ToString(), "Folder", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.FolderGetSrcCountsBaseID, src_ms, APIMethodID.FolderGetSrcCountsBaseID.ToString(), "Folder", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");

            // File API methods
            AddMethod(APIMethodID.FileDeleteBaseID, src_ms, APIMethodID.FileDeleteBaseID.ToString(), "File", "Delete", "Base", "Soft delete (update) of a file record.");
            AddMethod(APIMethodID.FileNewBaseID, src_ms, APIMethodID.FileNewBaseID.ToString(), "File", "New", "Base", "Insert a new File metadata record.");
            AddMethod(APIMethodID.FileSaveBaseID, src_ms, APIMethodID.FileSaveBaseID.ToString(), "File", "Save", "Base", "Update of the file name in a file record.");
            AddMethod(APIMethodID.FileGetByIDBaseID, src_ms, APIMethodID.FileGetByIDBaseID.ToString(), "File", "GetByID", "Base", "Returns the matching file record using the global ID value.");
            AddMethod(APIMethodID.FileGetByNameBaseID, src_ms, APIMethodID.FileGetByNameBaseID.ToString(), "File", "GetByName", "Base", "Returns the matching file record using the file name value.");
            AddMethod(APIMethodID.FileGetCountByFavoriteBaseID, src_ms, APIMethodID.FileGetCountByFavoriteBaseID.ToString(), "File", "GetCountByFavorite", "Base", "Returns the count of favorite records for a user.");
            AddMethod(APIMethodID.FileGetCountByFolderBaseID, src_ms, APIMethodID.FileGetCountByFolderBaseID.ToString(), "File", "GetCountByFolder", "Base", "Returns count of file records that are linked to the specified folder.");
            AddMethod(APIMethodID.FileGetCountByMetadataBaseID, src_ms, APIMethodID.FileGetCountByMetadataBaseID.ToString(), "File", "GetCountByMetadata", "Base", "Returns a page of file records that match the metadata search criteria.");
            AddMethod(APIMethodID.FileGetExtListBaseID, src_ms, APIMethodID.FileGetExtListBaseID.ToString(), "File", "GetExtensionList", "Base", "Returns a list of all distinct file extensions.");
            AddMethod(APIMethodID.FileGetFilesByFavoriteBaseID, src_ms, APIMethodID.FileGetFilesByFavoriteBaseID.ToString(), "File", "GetFilesByFavorite", "Base", "Returns a page of favorite file records.");
            AddMethod(APIMethodID.FileGetFilesByFolderBaseID, src_ms, APIMethodID.FileGetFilesByFolderBaseID.ToString(), "File", "GetFilesByFolder", "Base", "Returns a page of file records linked to a specified folder.");
            AddMethod(APIMethodID.FileGetFilesByMetadataBaseID, src_ms, APIMethodID.FileGetFilesByMetadataBaseID.ToString(), "File", "GetFilesByMetadata", "Base", "Returns a page of file records that match the metadata search criteria.");
            AddMethod(APIMethodID.FileGetHistoryBaseID, src_ms, APIMethodID.FileGetHistoryBaseID.ToString(), "File", "GetHistory", "Base", "Returns all versions of a file regardless of record state.");
            AddMethod(APIMethodID.FileGetSourceBaseID, src_ms, APIMethodID.FileGetSourceBaseID.ToString(), "File", "GetSource", "Base", "Queries for a set of file records greater than the placeholder value");
            AddMethod(APIMethodID.FileRecoverBaseID, src_ms, APIMethodID.FileRecoverBaseID.ToString(), "File", "Recover", "Base", "Restores an edited or deleted record as the active record version.");
            AddMethod(APIMethodID.FileReplicateBaseID, src_ms, APIMethodID.FileReplicateBaseID.ToString(), "File", "Replicate", "Base", "Merges one or more file records into a destination database table.");
            AddMethod(APIMethodID.FileGetCountByTagBaseID, src_ms, APIMethodID.FileGetCountByTagBaseID.ToString(), "File", "GetCountByTag", "Base", "Returns count of file records that are linked to the specified tag.");
            AddMethod(APIMethodID.FileGetFilesByTagBaseID, src_ms, APIMethodID.FileGetFilesByTagBaseID.ToString(), "File", "GetFilesByTag", "Base", "Returns a page of file records linked to a specified tag.");
            AddMethod(APIMethodID.FileDuplicateBaseID, src_ms, APIMethodID.FileDuplicateBaseID.ToString(), "File", "Duplicate", "Base", "Checks the Files table for duplicates.");
            AddMethod(APIMethodID.FileGetSrcCountsBaseID, src_ms, APIMethodID.FileGetSrcCountsBaseID.ToString(), "File", "GetSrcCount", "Base", "Gets a count of source records in the source table.");
            AddMethod(APIMethodID.FileGetDestCountsBaseID, src_ms, APIMethodID.FileGetDestCountsBaseID.ToString(), "File", "GetDestCount", "Base", "Gets a count of source records in a destination table.");
            AddMethod(APIMethodID.FileSaveFileRecBaseID, src_ms, APIMethodID.FileSaveFileRecBaseID.ToString(), "File", "SaveFileRec", "Base", "Edits a few select fields in the File record, or deletes the File record.");
            AddMethod(APIMethodID.FileRemoveBaseID, src_ms, APIMethodID.FileRemoveBaseID.ToString(), "File", "Remove", "Base", "Removes a record from the system (ignored, not hard delete)");
            AddMethod(APIMethodID.FileGetPageSizeBaseID, src_ms, APIMethodID.FileGetPageSizeBaseID.ToString(), "File", "GetPageSize", "Base", "Returns the maximum number of records in a page of results");

            // Favorite API methods
            AddMethod(APIMethodID.FavoriteAssignBaseID, src_ms, APIMethodID.FavoriteAssignBaseID.ToString(), "Favorite", "Assign", "Base", "Assigns a file to a user favorite list.");
            AddMethod(APIMethodID.FavoriteRemoveBaseID, src_ms, APIMethodID.FavoriteRemoveBaseID.ToString(), "Favorite", "Remove", "Base", "Removes a file from a user favorite list.");
            AddMethod(APIMethodID.FavoriteGetSourceBaseID, src_ms, APIMethodID.FavoriteGetSourceBaseID.ToString(), "Favorite", "GetSource", "Base", "Returns Favorite source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.FavoriteReplicateBaseID, src_ms, APIMethodID.FavoriteReplicateBaseID.ToString(), "Favorite", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.FavoriteDuplicateBaseID, src_ms, APIMethodID.FavoriteDuplicateBaseID.ToString(), "Favorite", "Duplicate", "Base", "Checks the Favorites table for duplicates.");
            AddMethod(APIMethodID.FavoriteGetDestCountsBaseID, src_ms, APIMethodID.FavoriteGetDestCountsBaseID.ToString(), "Favorite", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.FavoriteGetSrcCountsBaseID, src_ms, APIMethodID.FavoriteGetSrcCountsBaseID.ToString(), "Favorite", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.FavoriteGetPageSizeBaseID, src_ms, APIMethodID.FavoriteGetPageSizeBaseID.ToString(), "Favorite", "GetPageSize", "Base", "Returns the maximum number of records in a page of results");

            // FileTag API methods
            AddMethod(APIMethodID.FileTagGetAssignedBaseID, src_ms, APIMethodID.FileTagGetAssignedBaseID.ToString(), "FileTag", "GetAssigned", "Base", "Returns all tags linked to a file.");
            AddMethod(APIMethodID.FileTagGetAvailableBaseID, src_ms, APIMethodID.FileTagGetAvailableBaseID.ToString(), "FileTag", "GetAvailable", "Base", "Returns tags that have not been assigned to a file .");
            AddMethod(APIMethodID.FileTagGetSourceBaseID, src_ms, APIMethodID.FileTagGetSourceBaseID.ToString(), "FileTag", "GetSource", "Base", "Returns filetag source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.FileTagAssignBaseID, src_ms, APIMethodID.FileTagAssignBaseID.ToString(), "FileTag", "Assign", "Base", "Creates a new filetag record.");
            AddMethod(APIMethodID.FileTagRemoveBaseID, src_ms, APIMethodID.FileTagRemoveBaseID.ToString(), "FileTag", "Remove", "Base", "Deletes (updates) an existing filetag record.");
            AddMethod(APIMethodID.FileTagReplicateBaseID, src_ms, APIMethodID.FileTagReplicateBaseID.ToString(), "FileTag", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.FileTagGetDestCountsBaseID, src_ms, APIMethodID.FileTagGetDestCountsBaseID.ToString(), "FileTag", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.FileTagGetSrcCountsBaseID, src_ms, APIMethodID.FileTagGetSrcCountsBaseID.ToString(), "FileTag", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.FileTagDuplicateBaseID, src_ms, APIMethodID.FileTagDuplicateBaseID.ToString(), "FileTag", "Duplicate", "Base", "Checks for duplicates in the FileTag table.");

            // Tag API methods 
            AddMethod(APIMethodID.TagGetByIDBaseID, src_ms, APIMethodID.TagGetByIDBaseID.ToString(), "Tag", "GetByID", "Base", "Returns tag record with matching ID.");
            AddMethod(APIMethodID.TagGetByNameBaseID, src_ms, APIMethodID.TagGetByNameBaseID.ToString(), "Tag", "GetByName", "Base", "Returns tag record with matching Name.");
            AddMethod(APIMethodID.TagFilterByNameBaseID, src_ms, APIMethodID.TagFilterByNameBaseID.ToString(), "Tag", "FilterByName", "Base", "Returns tag records that match the search criteria.");
            AddMethod(APIMethodID.TagGetSourceBaseID, src_ms, APIMethodID.TagGetSourceBaseID.ToString(), "Tag", "GetSource", "Base", "Returns tag source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.TagNewBaseID, src_ms, APIMethodID.TagNewBaseID.ToString(), "Tag", "New", "Base", "Creates a new tag record.");
            AddMethod(APIMethodID.TagSaveBaseID, src_ms, APIMethodID.TagSaveBaseID.ToString(), "Tag", "Save", "Base", "Updates an existing tag record.");
            AddMethod(APIMethodID.TagDeleteBaseID, src_ms, APIMethodID.TagDeleteBaseID.ToString(), "Tag", "Delete", "Base", "Deletes (updates) an existing tag record.");
            AddMethod(APIMethodID.TagReplicateBaseID, src_ms, APIMethodID.TagReplicateBaseID.ToString(), "Tag", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.TagDuplicateBaseID, src_ms, APIMethodID.TagDuplicateBaseID.ToString(), "Tag", "Duplicate", "Base", "Checks the Tags table for duplicates.");
            AddMethod(APIMethodID.TagGetDestCountsBaseID, src_ms, APIMethodID.TagGetDestCountsBaseID.ToString(), "Tag", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.TagGetSrcCountsBaseID, src_ms, APIMethodID.TagGetSrcCountsBaseID.ToString(), "Tag", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.TagGetPageSizeBaseID, src_ms, APIMethodID.TagGetPageSizeBaseID.ToString(), "Tag", "GetPageSize", "Base", "Returns the maximum number of records in a page of results.");
            AddMethod(APIMethodID.TagGetCountBaseID, src_ms, APIMethodID.TagGetCountBaseID.ToString(), "Tag", "GetCount", "Base", "Returns the count of records that match the search criteria");
            AddMethod(APIMethodID.TagGetSearchBaseID, src_ms, APIMethodID.TagGetSearchBaseID.ToString(), "Tag", "GetSearch", "Base", "Returns a page of results that match the search criteria.");
            AddMethod(APIMethodID.TagGetHistoryBaseID, src_ms, APIMethodID.TagGetHistoryBaseID.ToString(), "Tag", "GetHistory", "Base", "Returns the maximum number of records in a page of results.");
            AddMethod(APIMethodID.TagRecoverBaseID, src_ms, APIMethodID.TagRecoverBaseID.ToString(), "Tag", "Recover", "Base", "Restores an edited or deleted record as the active record version.");

            // FileShard API methods
            AddMethod(APIMethodID.FileShardDeleteBaseID, src_ms, APIMethodID.FileShardDeleteBaseID.ToString(), "FileShard", "Delete", "Base", "Soft delete of the FileShard record.");
            AddMethod(APIMethodID.FileShardDuplicateBaseID, src_ms, APIMethodID.FileShardDuplicateBaseID.ToString(), "FileShard", "Duplicate", "Base", "Checks the FileShard table for duplicates.");
            AddMethod(APIMethodID.FileShardGetByIDBaseID, src_ms, APIMethodID.FileShardGetByIDBaseID.ToString(), "FileShard", "GetByID", "Base", "Returns the FileShard record with the matching ID.");
            AddMethod(APIMethodID.FileShardGetByRowIDBaseID, src_ms, APIMethodID.FileShardGetByRowIDBaseID.ToString(), "FileShard", "GetByRowID", "Base", "Returns the FileShard record with the matching row_id.");
            AddMethod(APIMethodID.FileShardGetDestCountsBaseID, src_ms, APIMethodID.FileShardGetDestCountsBaseID.ToString(), "FileShard", "GetDestCount", "Base", "Returns the count of the source records in the source table.");
            AddMethod(APIMethodID.FileShardGetDataBySegNumBaseID, src_ms, APIMethodID.FileShardGetDataBySegNumBaseID.ToString(), "FileShard", "GetDataBySegNum", "Base", "Returns segment data from a specific FileShard record matching the FileGID and SegNum values.");
            AddMethod(APIMethodID.FileShardGetSourceBaseID, src_ms, APIMethodID.FileShardGetSourceBaseID.ToString(), "FileShard", "GetSource", "Base", "Returns FileShard source records that are greater than the placeholder value.");
            AddMethod(APIMethodID.FileShardGetSrcCountsBaseID, src_ms, APIMethodID.FileShardGetSrcCountsBaseID.ToString(), "FileShard", "GetSrcCount", "Base", "Returns the count of source records in a destination table.");
            AddMethod(APIMethodID.FileShardNewBaseID, src_ms, APIMethodID.FileShardNewBaseID.ToString(), "FileShard", "New", "Base", "Creates a new FileShard record.");
            AddMethod(APIMethodID.FileShardReplicateBaseID, src_ms, APIMethodID.FileShardReplicateBaseID.ToString(), "FileShard", "Replicate", "Base", "Merges a replicated record into the destination table.");
            AddMethod(APIMethodID.FileShardGetShardNameBaseID, src_ms, APIMethodID.FileShardGetShardNameBaseID.ToString(), "FileShard", "GetShardName", "Base", "Returns the name of a writable file shard.");
            AddMethod(APIMethodID.FileShardGetSegCountBaseID, src_ms, APIMethodID.FileShardGetSegCountBaseID.ToString(), "FileShard", "GetSegCount", "Base", "Returns the count of fileshard records for a file version in a shard.");

            // ***************************************************************** //
            // DGPTestAPI Methods
            // ***************************************************************** //

            // Test API method
            AddMethod(APIMethodID.TestEchoTestBaseID, src_ms, APIMethodID.TestEchoTestBaseID.ToString(), "Test", "EchoTest", "Base", "Returns the input value as the method result.");
            AddMethod(APIMethodID.TestLoggingTestBaseID, src_ms, APIMethodID.TestLoggingTestBaseID.ToString(), "Test", "LoggingTest", "Base", "Tests several levels of logging to the Event Viewer.");
            AddMethod(APIMethodID.TestExceptionTestBaseID, src_ms, APIMethodID.TestExceptionTestBaseID.ToString(), "Test", "ExceptionTest", "Base", "Tests the web service exception handler.");
            AddMethod(APIMethodID.TestGetDBNameBaseID, src_ms, APIMethodID.TestGetDBNameBaseID.ToString(), "Test", "GetDBName", "Base", "Returns the unique DB name given the DB label input.");

            // ***************************************************************** //
            // DGPSysMetricsAPI Methods
            // ***************************************************************** //

            // DGPErrors
            AddMethod(APIMethodID.DGPErrorNewBaseID, src_ms, APIMethodID.DGPErrorNewBaseID.ToString(), "DGPError", "New", "Base", "Creates new DGPError records.");
            AddMethod(APIMethodID.DGPErrorGetByIDBaseID, src_ms, APIMethodID.DGPErrorGetByIDBaseID.ToString(), "DGPError", "GetByID", "Base", "Query for a DGPError record by ID value.");
            AddMethod(APIMethodID.DGPErrorGetAllBaseID, src_ms, APIMethodID.DGPErrorGetAllBaseID.ToString(), "DGPError", "GetAll", "Base", "Query for a set of DGPError records.");
            AddMethod(APIMethodID.DGPErrorDeleteBaseID, src_ms, APIMethodID.DGPErrorDeleteBaseID.ToString(), "DGPError", "Delete", "Base", "Soft delete of a DGPError record.");
            AddMethod(APIMethodID.DGPErrorGetCountBaseID, src_ms, APIMethodID.DGPErrorGetCountBaseID.ToString(), "DGPError", "GetCount", "Base", "Returns the count of records that match the search criteria.");
            AddMethod(APIMethodID.DGPErrorGetSearchBaseID, src_ms, APIMethodID.DGPErrorGetSearchBaseID.ToString(), "DGPError", "GetSearch", "Base", "Returns a page of results that match the search criteria.");
            AddMethod(APIMethodID.DGPErrorGetPageSizeBaseID, src_ms, APIMethodID.DGPErrorGetPageSizeBaseID.ToString(), "DGPError", "GetPageSize", "Base", "Returns the size of a page of results for the table.");
            AddMethod(APIMethodID.DGPErrorGetErrDataBaseID, src_ms, APIMethodID.DGPErrorGetErrDataBaseID.ToString(), "DGPError", "GetErrData", "Base", "Returns the error data for a selected DGPError record.");

            // DGPMetrics
            AddMethod(APIMethodID.DGPMetricsNewBaseID, src_ms, APIMethodID.DGPMetricsNewBaseID.ToString(), "DGPMetrics", "New", "Base", "Creates new DGPMetrics records.");
            AddMethod(APIMethodID.DGPMetricsGetByIDBaseID, src_ms, APIMethodID.DGPMetricsGetByIDBaseID.ToString(), "DGPMetrics", "GetByID", "Base", "Query for a DGPMetrics record by ID value.");
            AddMethod(APIMethodID.DGPMetricsGetAllBaseID, src_ms, APIMethodID.DGPMetricsGetAllBaseID.ToString(), "DGPMetrics", "GetAll", "Base", "Query for a set of DGPMetrics records.");
            AddMethod(APIMethodID.DGPMetricsDeleteBaseID, src_ms, APIMethodID.DGPMetricsDeleteBaseID.ToString(), "DGPMetrics", "Delete", "Base", "Soft delete of a DGPMetrics record.");
            AddMethod(APIMethodID.DGPMetricsGetPageSizeBaseID, src_ms, APIMethodID.DGPMetricsGetPageSizeBaseID.ToString(), "DGPMetrics", "GetPageSize", "Base", "Returns the number of rows in a page of results.");
            AddMethod(APIMethodID.DGPMetricsGetCountBaseID, src_ms, APIMethodID.DGPMetricsGetCountBaseID.ToString(), "DGPMetrics", "GetCount", "Base", "Returns the count of records that match the search criteria.");
            AddMethod(APIMethodID.DGPMetricsGetSearchBaseID, src_ms, APIMethodID.DGPMetricsGetSearchBaseID.ToString(), "DGPMetrics", "GetSearch", "Base", "Returns the size of a page of results for the table.");
            AddMethod(APIMethodID.DGPMetricsServerBaseID, src_ms, APIMethodID.DGPMetricsServerBaseID.ToString(), "DGPMetrics", "Server", "Base", "Calls API method to collect and store server metrics.");

            // TestResult API method
            AddMethod(APIMethodID.TestResultNewBaseID, src_ms, APIMethodID.TestResultNewBaseID.ToString(), "TestResult", "New", "Base", "Creates new TestResult records.");
            AddMethod(APIMethodID.TestResultGetByIDBaseID, src_ms, APIMethodID.TestResultGetByIDBaseID.ToString(), "TestResult", "GetByID", "Base", "Query for a TestResult record by ID value.");
            AddMethod(APIMethodID.TestResultGetAllBaseID, src_ms, APIMethodID.TestResultGetAllBaseID.ToString(), "TestResult", "GetAll", "Base", "Query for a set of TestResult records.");
            AddMethod(APIMethodID.TestResultDeleteBaseID, src_ms, APIMethodID.TestResultDeleteBaseID.ToString(), "TestResult", "Delete", "Base", "Soft delete of a TestResult record.");
            AddMethod(APIMethodID.TestResultGetCountBaseID, src_ms, APIMethodID.TestResultGetCountBaseID.ToString(), "TestResult", "GetCount", "Base", "Returns the count of records that match the search criteria.");
            AddMethod(APIMethodID.TestResultGetSearchBaseID, src_ms, APIMethodID.TestResultGetSearchBaseID.ToString(), "TestResult", "GetSearch", "Base", "Returns a page of results that match the search criteria.");
            AddMethod(APIMethodID.TestResultGetPageSizeBaseID, src_ms, APIMethodID.TestResultGetPageSizeBaseID.ToString(), "TestResult", "GetPageSize", "Base", "Returns the size of a page of results for the table.");
            AddMethod(APIMethodID.TestResultGetEvalInfoBaseID, src_ms, APIMethodID.TestResultGetEvalInfoBaseID.ToString(), "TestResult", "GetEvalInfo", "Base", "Returns the evaluation info for a selected TestResult record.");

            // AutoWorkLog
            AddMethod(APIMethodID.AutoWorkLogNewBaseID, src_ms, APIMethodID.AutoWorkLogNewBaseID.ToString(), "AutoWorkLog", "New", "Base", "Creates new AutoWorkLog records.");
            AddMethod(APIMethodID.AutoWorkLogGetByIDBaseID, src_ms, APIMethodID.AutoWorkLogGetByIDBaseID.ToString(), "AutoWorkLog", "GetByID", "Base", "Query for an AutoWorkLog record by ID value.");
            AddMethod(APIMethodID.AutoWorkLogGetAllBaseID, src_ms, APIMethodID.AutoWorkLogGetAllBaseID.ToString(), "AutoWorkLog", "GetAll", "Base", "Query for a set of AutoWorkLog records.");
            AddMethod(APIMethodID.AutoWorkLogDeleteBaseID, src_ms, APIMethodID.AutoWorkLogDeleteBaseID.ToString(), "AutoWorkLog", "Delete", "Base", "Soft delete of an AutoWorkLog record.");
            AddMethod(APIMethodID.AutoWorkLogGetCountBaseID, src_ms, APIMethodID.AutoWorkLogGetCountBaseID.ToString(), "AutoWorkLog", "GetCount", "Base", "Returns the count of records that match the search criteria.");
            AddMethod(APIMethodID.AutoWorkLogGetSearchBaseID, src_ms, APIMethodID.AutoWorkLogGetSearchBaseID.ToString(), "AutoWorkLog", "GetSearch", "Base", "Returns a page of results that match the search criteria.");
            AddMethod(APIMethodID.AutoWorkLogGetPageSizeBaseID, src_ms, APIMethodID.AutoWorkLogGetPageSizeBaseID.ToString(), "AutoWorkLog", "GetPageSize", "Base", "Returns the size of a page of results for the table.");
            AddMethod(APIMethodID.AutoWorkLogGetProcStepsBaseID, src_ms, APIMethodID.AutoWorkLogGetProcStepsBaseID.ToString(), "AutoWorkLog", "GetProcSteps", "Base", "Returns the process steps for a selected AutoWorkLog record.");


            // ***************************************************************** //
            // DGPSysWorkAPI Methods
            // ***************************************************************** //

            // ReplicaWork API methods
            AddMethod(APIMethodID.ReplicaWorkGetCountBaseID, src_ms, APIMethodID.ReplicaWorkGetCountBaseID.ToString(), "ReplicaWork", "GetCount", "Base", "Returns the count of ReplicaWork records that match the search criteria.");
            AddMethod(APIMethodID.ReplicaWorkGetSearchBaseID, src_ms, APIMethodID.ReplicaWorkGetSearchBaseID.ToString(), "ReplicaWork", "GetSearch", "Base", "Returns a page of ReplicaWork records that match the search criteria.");
            AddMethod(APIMethodID.ReplicaWorkGetByIDBaseID, src_ms, APIMethodID.ReplicaWorkGetByIDBaseID.ToString(), "ReplicaWork", "GetByID", "Base", "Returns the ReplicaWork record by global ID.");
            AddMethod(APIMethodID.ReplicaWorkNewBaseID, src_ms, APIMethodID.ReplicaWorkNewBaseID.ToString(), "ReplicaWork", "New", "Base", "creates a new ReplicaWork record.");
            AddMethod(APIMethodID.ReplicaWorkSaveBaseID, src_ms, APIMethodID.ReplicaWorkSaveBaseID.ToString(), "ReplicaWork", "Save", "Base", "Saves (updates) an existing ReplicaWork record.");
            AddMethod(APIMethodID.ReplicaWorkDeleteBaseID, src_ms, APIMethodID.ReplicaWorkDeleteBaseID.ToString(), "ReplicaWork", "Delete", "Base", "Deletes (updates) an existing ReplicaWork record.");
            AddMethod(APIMethodID.ReplicaWorkGetAllBaseID, src_ms, APIMethodID.ReplicaWorkGetAllBaseID.ToString(), "ReplicaWork", "GetAll", "Base", "returns all ReplicaWork records.");
            AddMethod(APIMethodID.ReplicaWorkClaimRecordsBaseID, src_ms, APIMethodID.ReplicaWorkClaimRecordsBaseID.ToString(), "ReplicaWork", "ClaimRecords", "Base", "claims a batch of ReplicaWork records.");
            AddMethod(APIMethodID.ReplicaWorkReleaseRecordBaseID, src_ms, APIMethodID.ReplicaWorkReleaseRecordBaseID.ToString(), "ReplicaWork", "ReleaseRecord", "Base", "releases a claimed ReplicaWork record.");
            AddMethod(APIMethodID.ReplicaWorkCloneBaseID, src_ms, APIMethodID.ReplicaWorkCloneBaseID.ToString(), "ReplicaWork", "Clone", "Base", "creates a copy of an existing ReplicaWork record.");
            AddMethod(APIMethodID.ReplicaWorkGetPageSizeBaseID, src_ms, APIMethodID.ReplicaWorkGetPageSizeBaseID.ToString(), "ReplicaWork", "GetPageSize", "Base", "returns the maximum number of records in a page of results.");
            AddMethod(APIMethodID.ReplicaWorkQueueScanBaseID, src_ms, APIMethodID.ReplicaWorkQueueScanBaseID.ToString(), "ReplicaWork", "QueueScan", "Base", "scans for problems with the records in the ReplicaWork table.");

            // GeneralWork API methods
            AddMethod(APIMethodID.GeneralWorkGetCountBaseID, src_ms, APIMethodID.GeneralWorkGetCountBaseID.ToString(), "GeneralWork", "GetCount", "Base", "Returns the count of GeneralWork records that match the search criteria.");
            AddMethod(APIMethodID.GeneralWorkGetSearchBaseID, src_ms, APIMethodID.GeneralWorkGetSearchBaseID.ToString(), "GeneralWork", "GetSearch", "Base", "Returns a page of GeneralWork records that match the search criteria.");
            AddMethod(APIMethodID.GeneralWorkGetByIDBaseID, src_ms, APIMethodID.GeneralWorkGetByIDBaseID.ToString(), "GeneralWork", "GetByID", "Base", "Returns the GeneralWork record by global ID.");
            AddMethod(APIMethodID.GeneralWorkNewBaseID, src_ms, APIMethodID.GeneralWorkNewBaseID.ToString(), "GeneralWork", "New", "Base", "creates a new GeneralWork record.");
            AddMethod(APIMethodID.GeneralWorkSaveBaseID, src_ms, APIMethodID.GeneralWorkSaveBaseID.ToString(), "GeneralWork", "Save", "Base", "Saves (updates) an existing GeneralWork record.");
            AddMethod(APIMethodID.GeneralWorkDeleteBaseID, src_ms, APIMethodID.GeneralWorkDeleteBaseID.ToString(), "GeneralWork", "Delete", "Base", "Deletes (updates) an existing GeneralWork record.");
            AddMethod(APIMethodID.GeneralWorkGetAllBaseID, src_ms, APIMethodID.GeneralWorkGetAllBaseID.ToString(), "GeneralWork", "GetAll", "Base", "returns all GeneralWork records.");
            AddMethod(APIMethodID.GeneralWorkClaimRecordsBaseID, src_ms, APIMethodID.GeneralWorkClaimRecordsBaseID.ToString(), "GeneralWork", "ClaimRecords", "Base", "claims a batch of GeneralWork records.");
            AddMethod(APIMethodID.GeneralWorkReleaseRecordBaseID, src_ms, APIMethodID.GeneralWorkReleaseRecordBaseID.ToString(), "GeneralWork", "ReleaseRecord", "Base", "releases a claimed GeneralWork record.");
            AddMethod(APIMethodID.GeneralWorkCloneBaseID, src_ms, APIMethodID.GeneralWorkCloneBaseID.ToString(), "GeneralWork", "Clone", "Base", "creates a copy of an existing GeneralWork record.");
            AddMethod(APIMethodID.GeneralWorkGetPageSizeBaseID, src_ms, APIMethodID.GeneralWorkGetPageSizeBaseID.ToString(), "GeneralWork", "GetPageSize", "Base", "returns the maximum number of records in a page of results.");
            AddMethod(APIMethodID.GeneralWorkQueueScanBaseID, src_ms, APIMethodID.GeneralWorkQueueScanBaseID.ToString(), "GeneralWork", "QueueScan", "Base", "scans for problems with the records in the GeneralWork table.");

            return "<p>APIMethod Core Data: " + reccount.ToString() + " RECORDS, " + scancount.ToString() + " Created, " + skipcount.ToString() + " Skipped</p>";
        }

        private void AddMethod(long rowid,
                                string row_ms,
                                string recID,
                                string apiname,
                                string methodname,
                                string versionname,
                                string methoddescrip)
        {
            try
            {
                reccount++;

                APIMethods_write_dml methods_write_Dml = new APIMethods_write_dml(_dbconnstr);
                string tmpresult = methods_write_Dml.Replicate(rowid.ToString(), row_ms, SysInfoMaster.SourceDB, recID, RecState.Active, APIUserID.SysAdminID.ToString(), apiname, methodname, versionname, methoddescrip, _dbconnstr);

                if (tmpresult == "OK")
                {
                    scancount++;
                }
                else
                {
                    skipcount++;
                    string msg = "row_id: " + rowid + ", rec_gid: " + recID + ", APIName: " + apiname + ", MethodName: " + methodname + ", VersionName: " + versionname;
                    RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "APIMethods_data.AddMethod", "CLIENT", "INFO", "Skipped APIMethod", msg);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "APIMethods_data.AddMethod", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }
    }
}
