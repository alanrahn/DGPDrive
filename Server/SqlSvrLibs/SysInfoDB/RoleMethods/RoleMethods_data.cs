using System;

using ApiUtil;
using SysInfoDB.APIRoles;
using SysInfoDB.APIMethods;
using SysInfoDB.APIUser;

namespace SysInfoDB.RoleMethods
{
    public class RoleMethods_data
    {
        string _dbconnstr;
        int reccount = 0;
        int scancount = 0;
        int skipcount = 0;

        public RoleMethods_data(string dbConnStr)
        {
            _dbconnstr = dbConnStr;
        }

        public string AddCoreData()
        {
            MsgUtil msgUtil = new MsgUtil();
            string src_ms = msgUtil.UnixTimeString(-3000);

            // RoleMethods - 6,000,000

            // ***************************************************************** //
            // DGPAdminAPI Methods
            // ***************************************************************** //

            // sysinfoadmin datagroups
            AddRoleMethod(6000000, src_ms, "6000000", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetHistoryBaseID.ToString());
            AddRoleMethod(6000010, src_ms, "6000010", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetByIDBaseID.ToString());
            AddRoleMethod(6000020, src_ms, "6000020", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetByNameBaseID.ToString());
            AddRoleMethod(6000030, src_ms, "6000030", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetCountBaseID.ToString());
            AddRoleMethod(6000040, src_ms, "6000040", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetSearchBaseID.ToString());
            AddRoleMethod(6000050, src_ms, "6000050", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetSourceBaseID.ToString());
            AddRoleMethod(6000060, src_ms, "6000060", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupNewBaseID.ToString());
            AddRoleMethod(6000070, src_ms, "6000070", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupSaveBaseID.ToString());
            AddRoleMethod(6000080, src_ms, "6000080", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupDeleteBaseID.ToString());
            AddRoleMethod(6000090, src_ms, "6000090", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupReplicateBaseID.ToString());
            AddRoleMethod(6000100, src_ms, "6000100", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupRecoverBaseID.ToString());
            AddRoleMethod(6000110, src_ms, "6000110", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupDuplicateBaseID.ToString());
            AddRoleMethod(6000120, src_ms, "6000120", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetSrcCountsBaseID.ToString());
            AddRoleMethod(6000130, src_ms, "6000130", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetDestCountsBaseID.ToString());
            AddRoleMethod(6000140, src_ms, "6000140", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupGetPageSizeBaseID.ToString());

            // sysinfoadmin groupuser
            AddRoleMethod(6010000, src_ms, "6010000", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserGetAssignedBaseID.ToString());
            AddRoleMethod(6010010, src_ms, "6010010", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserGetAvailableBaseID.ToString());
            AddRoleMethod(6010020, src_ms, "6010020", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserGetSourceBaseID.ToString());
            AddRoleMethod(6010030, src_ms, "6010030", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserNewBaseID.ToString());
            AddRoleMethod(6010040, src_ms, "6010040", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserDeleteBaseID.ToString());
            AddRoleMethod(6010050, src_ms, "6010050", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserReplicateBaseID.ToString());
            AddRoleMethod(6010060, src_ms, "6010060", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserDuplicateBaseID.ToString());
            AddRoleMethod(6010070, src_ms, "6010070", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserGetSrcCountsBaseID.ToString());
            AddRoleMethod(6010080, src_ms, "6010080", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.GroupUserGetDestCountsBaseID.ToString());

            // sysinfoadmin methods
            AddRoleMethod(6020000, src_ms, "6020000", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetHistoryBaseID.ToString());
            AddRoleMethod(6020010, src_ms, "6020010", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetByIDBaseID.ToString());
            AddRoleMethod(6020020, src_ms, "6020020", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetByNameBaseID.ToString());
            AddRoleMethod(6020030, src_ms, "6020030", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetCountBaseID.ToString());
            AddRoleMethod(6020040, src_ms, "6020040", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetSearchBaseID.ToString());
            AddRoleMethod(6020050, src_ms, "6020050", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetSourceBaseID.ToString());
            AddRoleMethod(6020060, src_ms, "6020060", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetAPIListBaseID.ToString());
            AddRoleMethod(6020070, src_ms, "6020070", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodNewBaseID.ToString());
            AddRoleMethod(6020080, src_ms, "6020080", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodSaveBaseID.ToString());
            AddRoleMethod(6020090, src_ms, "6020090", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodDeleteBaseID.ToString());
            AddRoleMethod(6020100, src_ms, "6020100", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodReplicateBaseID.ToString());
            AddRoleMethod(6020110, src_ms, "6020110", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodRecoverBaseID.ToString());
            AddRoleMethod(6020120, src_ms, "6020120", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodDuplicateBaseID.ToString());
            AddRoleMethod(6020130, src_ms, "6020130", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetSrcCountsBaseID.ToString());
            AddRoleMethod(6020140, src_ms, "6020140", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetDestCountsBaseID.ToString());
            AddRoleMethod(6020150, src_ms, "6020150", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.MethodGetPageSizeBaseID.ToString());

            // sysinfoadmin roles
            AddRoleMethod(6030000, src_ms, "6030000", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetHistoryBaseID.ToString());
            AddRoleMethod(6030010, src_ms, "6030010", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetByIDBaseID.ToString());
            AddRoleMethod(6030020, src_ms, "6030020", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetByNameBaseID.ToString());
            AddRoleMethod(6030030, src_ms, "6030030", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetCountBaseID.ToString());
            AddRoleMethod(6030040, src_ms, "6030040", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetSearchBaseID.ToString());
            AddRoleMethod(6030050, src_ms, "6030050", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetSourceBaseID.ToString());
            AddRoleMethod(6030060, src_ms, "6030060", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleNewBaseID.ToString());
            AddRoleMethod(6030070, src_ms, "6030070", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleSaveBaseID.ToString());
            AddRoleMethod(6030080, src_ms, "6030080", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleDeleteBaseID.ToString());
            AddRoleMethod(6030090, src_ms, "6030090", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleReplicateBaseID.ToString());
            AddRoleMethod(6030100, src_ms, "6030100", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleRecoverBaseID.ToString());
            AddRoleMethod(6030120, src_ms, "6030120", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleDuplicateBaseID.ToString());
            AddRoleMethod(6030130, src_ms, "6030130", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetSrcCountsBaseID.ToString());
            AddRoleMethod(6030140, src_ms, "6030140", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetDestCountsBaseID.ToString());
            AddRoleMethod(6030150, src_ms, "6030150", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleGetPageSizeBaseID.ToString());

            // sysinfoadmin rolemethods
            AddRoleMethod(6040000, src_ms, "6040000", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodGetAssignedBaseID.ToString());
            AddRoleMethod(6040010, src_ms, "6040010", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodGetAvailableBaseID.ToString());
            AddRoleMethod(6040020, src_ms, "6040020", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodGetSourceBaseID.ToString());
            AddRoleMethod(6040030, src_ms, "6040030", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodGetMethodRolesBaseID.ToString());
            AddRoleMethod(6040040, src_ms, "6040040", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodGetUserMethodsBaseID.ToString());
            AddRoleMethod(6040050, src_ms, "6040050", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodNewBaseID.ToString());
            AddRoleMethod(6040060, src_ms, "6040060", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodDeleteBaseID.ToString());
            AddRoleMethod(6040070, src_ms, "6040070", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodReplicateBaseID.ToString());
            AddRoleMethod(6040080, src_ms, "6040080", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodDuplicateBaseID.ToString());
            AddRoleMethod(6040090, src_ms, "6040090", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodGetSrcCountsBaseID.ToString());
            AddRoleMethod(6040100, src_ms, "6040100", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleMethodGetDestCountsBaseID.ToString());

            // sysinfoadmin roleusers
            AddRoleMethod(6050000, src_ms, "6050000", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserGetAssignedBaseID.ToString());
            AddRoleMethod(6050010, src_ms, "6050010", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserGetAvailableBaseID.ToString());
            AddRoleMethod(6050020, src_ms, "6050020", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserGetSourceBaseID.ToString());
            AddRoleMethod(6050030, src_ms, "6050030", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserNewBaseID.ToString());
            AddRoleMethod(6050040, src_ms, "6050040", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserDeleteBaseID.ToString());
            AddRoleMethod(6050050, src_ms, "6050050", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserReplicateBaseID.ToString());
            AddRoleMethod(6050060, src_ms, "6050060", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserDuplicateBaseID.ToString());
            AddRoleMethod(6050070, src_ms, "6050070", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserGetSrcCountsBaseID.ToString());
            AddRoleMethod(6050080, src_ms, "6050080", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.RoleUserGetDestCountsBaseID.ToString());

            // sysinfoadmin users
            AddRoleMethod(6060000, src_ms, "6060000", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetHistoryBaseID.ToString());
            AddRoleMethod(6060010, src_ms, "6060010", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetByIDBaseID.ToString());
            AddRoleMethod(6060020, src_ms, "6060020", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetByNameBaseID.ToString());
            AddRoleMethod(6060040, src_ms, "6060040", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetCountBaseID.ToString());
            AddRoleMethod(6060050, src_ms, "6060050", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetSearchBaseID.ToString());
            AddRoleMethod(6060060, src_ms, "6060060", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetSourceBaseID.ToString());
            AddRoleMethod(6060070, src_ms, "6060070", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserCheckNameBaseID.ToString());
            AddRoleMethod(6060080, src_ms, "6060080", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserNewBaseID.ToString());
            AddRoleMethod(6060090, src_ms, "6060090", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserSaveBaseID.ToString());
            AddRoleMethod(6060100, src_ms, "6060100", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserDeleteBaseID.ToString());
            AddRoleMethod(6060110, src_ms, "6060110", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserReplicateBaseID.ToString());
            AddRoleMethod(6060120, src_ms, "6060120", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserRecoverBaseID.ToString());
            AddRoleMethod(6060130, src_ms, "6060130", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserChangePasswordBaseID.ToString());
            AddRoleMethod(6060140, src_ms, "6060140", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserDuplicateBaseID.ToString());
            AddRoleMethod(6060150, src_ms, "6060150", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetSrcCountsBaseID.ToString());
            AddRoleMethod(6060160, src_ms, "6060160", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetDestCountsBaseID.ToString());
            AddRoleMethod(6060170, src_ms, "6060170", APIRolesID.SysInfoAdminID.ToString(), APIMethodID.UserGetPageSizeBaseID.ToString());

            // defaultuser userself
            AddRoleMethod(6070000, src_ms, "6070000", APIRolesID.DefaultUserID.ToString(), APIMethodID.UserSelfLoginBaseID.ToString());
            AddRoleMethod(6070010, src_ms, "6070010", APIRolesID.DefaultUserID.ToString(), APIMethodID.UserSelfGetRolesID.ToString());
            AddRoleMethod(6070020, src_ms, "6070020", APIRolesID.DefaultUserID.ToString(), APIMethodID.UserSelfGetGroupsID.ToString());
            AddRoleMethod(6070030, src_ms, "6070030", APIRolesID.DefaultUserID.ToString(), APIMethodID.UserSelfGetInfoID.ToString());
            AddRoleMethod(6070040, src_ms, "6070040", APIRolesID.DefaultUserID.ToString(), APIMethodID.UserSelfGetUserGroupListBaseID.ToString());
            AddRoleMethod(6070050, src_ms, "6070050", APIRolesID.DefaultUserID.ToString(), APIMethodID.UserSelfSaveBaseID.ToString());
            AddRoleMethod(6070060, src_ms, "6070060", APIRolesID.DefaultUserID.ToString(), APIMethodID.UserSelfChangePasswordBaseID.ToString());
            
            AddRoleMethod(6070070, src_ms, "6070070", APIRolesID.DefaultUserID.ToString(), APIMethodID.DGPMetricsNewBaseID.ToString());
            AddRoleMethod(6070080, src_ms, "6070080", APIRolesID.DefaultUserID.ToString(), APIMethodID.DGPErrorNewBaseID.ToString());


            // ***************************************************************** //
            // DGPFileStoreAPI Methods
            // ***************************************************************** //

            // filestoreuser folders
            AddRoleMethod(6080000, src_ms, "6080000", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FolderGetUserSubFoldersBaseID.ToString());
            AddRoleMethod(6080010, src_ms, "6080010", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FolderGetByIDBaseID.ToString());
            AddRoleMethod(6080030, src_ms, "6080030", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FolderAddSubFolderBaseID.ToString());
            AddRoleMethod(6080040, src_ms, "6080040", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FolderSaveBaseID.ToString());
            AddRoleMethod(6080050, src_ms, "6080050", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FolderDeleteBaseID.ToString());

            // filestoreadmin folders
            AddRoleMethod(6080200, src_ms, "6080200", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FolderReplicateBaseID.ToString());
            AddRoleMethod(6080210, src_ms, "6080210", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FolderGetSourceBaseID.ToString());
            AddRoleMethod(6080220, src_ms, "6080220", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FolderDuplicateBaseID.ToString());
            AddRoleMethod(6080230, src_ms, "6080230", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FolderGetSrcCountsBaseID.ToString());
            AddRoleMethod(6080240, src_ms, "6080240", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FolderGetDestCountsBaseID.ToString());

            // filestoreuser files
            AddRoleMethod(6090000, src_ms, "6090000", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileDeleteBaseID.ToString());
            AddRoleMethod(6090020, src_ms, "6090020", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetByIDBaseID.ToString());
            AddRoleMethod(6090030, src_ms, "6090030", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetByNameBaseID.ToString());
            AddRoleMethod(6090040, src_ms, "6090040", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetCountByFavoriteBaseID.ToString());
            AddRoleMethod(6090050, src_ms, "6090050", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetCountByFolderBaseID.ToString());
            AddRoleMethod(6090060, src_ms, "6090060", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetCountByMetadataBaseID.ToString());
            AddRoleMethod(6090070, src_ms, "6090070", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetCountByTagBaseID.ToString());
            AddRoleMethod(6090080, src_ms, "6090080", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileNewBaseID.ToString());
            AddRoleMethod(6090090, src_ms, "6090090", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetExtListBaseID.ToString());
            AddRoleMethod(6090100, src_ms, "6090100", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetFilesByFavoriteBaseID.ToString());
            AddRoleMethod(6090110, src_ms, "6090110", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetFilesByFolderBaseID.ToString());
            AddRoleMethod(6090120, src_ms, "6090120", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetFilesByMetadataBaseID.ToString());
            AddRoleMethod(6090130, src_ms, "6090130", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetFilesByTagBaseID.ToString());
            AddRoleMethod(6090140, src_ms, "6090140", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetHistoryBaseID.ToString());
            AddRoleMethod(6090150, src_ms, "6090150", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileSaveBaseID.ToString());
            AddRoleMethod(6090160, src_ms, "6090160", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileRecoverBaseID.ToString());
            AddRoleMethod(6090170, src_ms, "6090170", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileSaveFileRecBaseID.ToString());
            AddRoleMethod(6090180, src_ms, "6090180", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileRemoveBaseID.ToString());
            AddRoleMethod(6090190, src_ms, "6090190", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileGetPageSizeBaseID.ToString());

            // filestoreadmin files
            AddRoleMethod(6090200, src_ms, "6090200", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileGetDestCountsBaseID.ToString());
            AddRoleMethod(6090210, src_ms, "6090210", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileDuplicateBaseID.ToString());
            AddRoleMethod(6090220, src_ms, "6090220", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileReplicateBaseID.ToString());
            AddRoleMethod(6090230, src_ms, "6090230", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileGetSourceBaseID.ToString());
            AddRoleMethod(6090240, src_ms, "6090240", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileGetSrcCountsBaseID.ToString());

            // filestoreuser favorites
            AddRoleMethod(6100000, src_ms, "6100000", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FavoriteAssignBaseID.ToString());
            AddRoleMethod(6100010, src_ms, "6100010", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FavoriteRemoveBaseID.ToString());
            AddRoleMethod(6100020, src_ms, "6100020", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FavoriteGetPageSizeBaseID.ToString());

            // filestoreadmin favorites
            AddRoleMethod(6100200, src_ms, "6100200", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FavoriteGetSourceBaseID.ToString());
            AddRoleMethod(6100210, src_ms, "6100210", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FavoriteReplicateBaseID.ToString());
            AddRoleMethod(6100220, src_ms, "6100220", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FavoriteGetDestCountsBaseID.ToString());
            AddRoleMethod(6100230, src_ms, "6100230", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FavoriteGetSrcCountsBaseID.ToString());
            AddRoleMethod(6100240, src_ms, "6100240", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FavoriteDuplicateBaseID.ToString());

            // filestoreuser filetags
            AddRoleMethod(6110000, src_ms, "6110000", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileTagAssignBaseID.ToString());
            AddRoleMethod(6110010, src_ms, "6110010", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileTagGetAssignedBaseID.ToString());
            AddRoleMethod(6110020, src_ms, "6110020", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileTagGetAvailableBaseID.ToString());
            AddRoleMethod(6110030, src_ms, "6110030", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileTagRemoveBaseID.ToString());

            // filestoreadmin filetags
            AddRoleMethod(6110200, src_ms, "6110200", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileTagGetSourceBaseID.ToString());
            AddRoleMethod(6110210, src_ms, "6110210", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileTagReplicateBaseID.ToString());
            AddRoleMethod(6110220, src_ms, "6110220", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileTagDuplicateBaseID.ToString());
            AddRoleMethod(6110230, src_ms, "6110230", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileTagGetSrcCountsBaseID.ToString());
            AddRoleMethod(6110240, src_ms, "6110240", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileTagGetDestCountsBaseID.ToString());

            // filestoreuser tags
            AddRoleMethod(6120000, src_ms, "6120000", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagDeleteBaseID.ToString());
            AddRoleMethod(6120010, src_ms, "6120010", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagFilterByNameBaseID.ToString());
            AddRoleMethod(6120020, src_ms, "6120020", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagGetByIDBaseID.ToString());
            AddRoleMethod(6120030, src_ms, "6120030", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagGetByNameBaseID.ToString());
            AddRoleMethod(6120040, src_ms, "6120040", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagNewBaseID.ToString());
            AddRoleMethod(6120050, src_ms, "6120050", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagSaveBaseID.ToString());
            AddRoleMethod(6120060, src_ms, "6120060", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagGetPageSizeBaseID.ToString());
            AddRoleMethod(6120070, src_ms, "6120070", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagGetCountBaseID.ToString());
            AddRoleMethod(6120080, src_ms, "6120080", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagGetSearchBaseID.ToString());
            AddRoleMethod(6120090, src_ms, "6120090", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagGetHistoryBaseID.ToString());
            AddRoleMethod(6120100, src_ms, "6120100", APIRolesID.FileStoreUserID.ToString(), APIMethodID.TagRecoverBaseID.ToString());

            // filestoreadmin tags
            AddRoleMethod(6120200, src_ms, "6120200", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.TagGetSourceBaseID.ToString());
            AddRoleMethod(6120210, src_ms, "6120210", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.TagReplicateBaseID.ToString());
            AddRoleMethod(6120220, src_ms, "6120220", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.TagDuplicateBaseID.ToString());
            AddRoleMethod(6120230, src_ms, "6120230", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.TagGetSrcCountsBaseID.ToString());
            AddRoleMethod(6120240, src_ms, "6120240", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.TagGetDestCountsBaseID.ToString());

            // filestoreuser fileshards
            AddRoleMethod(6130000, src_ms, "6130000", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardDeleteBaseID.ToString());
            AddRoleMethod(6130010, src_ms, "6130010", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardGetByIDBaseID.ToString());
            AddRoleMethod(6130020, src_ms, "6130020", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardGetByRowIDBaseID.ToString());
            AddRoleMethod(6130030, src_ms, "6130030", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardGetDataBySegNumBaseID.ToString());
            AddRoleMethod(6130050, src_ms, "6130050", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardNewBaseID.ToString());
            AddRoleMethod(6130060, src_ms, "6130060", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardGetShardNameBaseID.ToString());
            AddRoleMethod(6130070, src_ms, "6130070", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardGetPageSizeBaseID.ToString());
            AddRoleMethod(6130080, src_ms, "6130080", APIRolesID.FileStoreUserID.ToString(), APIMethodID.FileShardGetSegCountBaseID.ToString());

            // filestoreadmin fileshards
            AddRoleMethod(6130200, src_ms, "6130200", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileShardDuplicateBaseID.ToString());
            AddRoleMethod(6130210, src_ms, "6130210", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileShardGetDestCountsBaseID.ToString());
            AddRoleMethod(6130220, src_ms, "6130220", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileShardGetSourceBaseID.ToString());
            AddRoleMethod(6130230, src_ms, "6130230", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileShardGetSrcCountsBaseID.ToString());
            AddRoleMethod(6130240, src_ms, "6130240", APIRolesID.FileStoreAdminID.ToString(), APIMethodID.FileShardReplicateBaseID.ToString());

            // ***************************************************************** //
            // DGPTestAPI Methods
            // ***************************************************************** //

            // testing
            AddRoleMethod(7200000, src_ms, "7200000", APIRolesID.TestingID.ToString(), APIMethodID.TestEchoTestBaseID.ToString());
            AddRoleMethod(7200010, src_ms, "7200010", APIRolesID.TestingID.ToString(), APIMethodID.TestLoggingTestBaseID.ToString());
            AddRoleMethod(7200020, src_ms, "7200020", APIRolesID.TestingID.ToString(), APIMethodID.TestExceptionTestBaseID.ToString());
            AddRoleMethod(7200030, src_ms, "7200030", APIRolesID.TestingID.ToString(), APIMethodID.TestGetDBNameBaseID.ToString());


            // ***************************************************************** //
            // Sysmetrics API Methods
            // ***************************************************************** //

            // DGPError
            AddRoleMethod(7500100, src_ms, "7500100", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorNewBaseID.ToString());
            AddRoleMethod(7500110, src_ms, "7500110", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorGetByIDBaseID.ToString());
            AddRoleMethod(7500120, src_ms, "7500120", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorGetAllBaseID.ToString());
            AddRoleMethod(7500130, src_ms, "7500130", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorDeleteBaseID.ToString());
            AddRoleMethod(7500140, src_ms, "7500140", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorGetCountBaseID.ToString());
            AddRoleMethod(7500150, src_ms, "7500150", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorGetSearchBaseID.ToString());
            AddRoleMethod(7500160, src_ms, "7500160", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorGetPageSizeBaseID.ToString());
            AddRoleMethod(7500170, src_ms, "7500170", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPErrorGetErrDataBaseID.ToString());

            // AutoWorkLog
            AddRoleMethod(7500400, src_ms, "7500400", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogNewBaseID.ToString());
            AddRoleMethod(7500410, src_ms, "7500410", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogGetByIDBaseID.ToString());
            AddRoleMethod(7500420, src_ms, "7500420", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogGetAllBaseID.ToString());
            AddRoleMethod(7500430, src_ms, "7500430", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogDeleteBaseID.ToString());
            AddRoleMethod(7500440, src_ms, "7500440", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogGetCountBaseID.ToString());
            AddRoleMethod(7500450, src_ms, "7500450", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogGetSearchBaseID.ToString());
            AddRoleMethod(7500460, src_ms, "7500460", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogGetPageSizeBaseID.ToString());
            AddRoleMethod(7500470, src_ms, "7500470", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.AutoWorkLogGetProcStepsBaseID.ToString());

            // SysMetrics
            AddRoleMethod(7500200, src_ms, "7500200", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsNewBaseID.ToString());
            AddRoleMethod(7500210, src_ms, "7500210", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsGetByIDBaseID.ToString());
            AddRoleMethod(7500220, src_ms, "7500220", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsGetAllBaseID.ToString());
            AddRoleMethod(7500230, src_ms, "7500230", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsDeleteBaseID.ToString());
            AddRoleMethod(7500240, src_ms, "7500240", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsGetPageSizeBaseID.ToString());
            AddRoleMethod(7500250, src_ms, "7500250", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsGetCountBaseID.ToString());
            AddRoleMethod(7500260, src_ms, "7500260", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsGetSearchBaseID.ToString());
            AddRoleMethod(7500270, src_ms, "7500270", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.DGPMetricsServerBaseID.ToString());

            // TestResult
            AddRoleMethod(7500300, src_ms, "7500300", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultNewBaseID.ToString());
            AddRoleMethod(7500310, src_ms, "7500310", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultGetByIDBaseID.ToString());
            AddRoleMethod(7500320, src_ms, "7500320", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultGetAllBaseID.ToString());
            AddRoleMethod(7500330, src_ms, "7500330", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultDeleteBaseID.ToString());
            AddRoleMethod(7500340, src_ms, "7500340", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultGetCountBaseID.ToString());
            AddRoleMethod(7500350, src_ms, "7500350", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultGetSearchBaseID.ToString());
            AddRoleMethod(7500360, src_ms, "7500360", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultGetPageSizeBaseID.ToString());
            AddRoleMethod(7500370, src_ms, "7500370", APIRolesID.SysMetricsUserID.ToString(), APIMethodID.TestResultGetEvalInfoBaseID.ToString());

            // ***************************************************************** //
            // Syswork API Methods
            // ***************************************************************** //

            // ReplicaWork
            AddRoleMethod(8200000, src_ms, "8200000", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkDeleteBaseID.ToString());
            AddRoleMethod(8200010, src_ms, "8200010", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkGetByIDBaseID.ToString());
            AddRoleMethod(8200020, src_ms, "8200020", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkGetCountBaseID.ToString());
            AddRoleMethod(8200030, src_ms, "8200030", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkGetSearchBaseID.ToString());
            AddRoleMethod(8200040, src_ms, "8200040", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkNewBaseID.ToString());
            AddRoleMethod(8200050, src_ms, "8200050", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkSaveBaseID.ToString());
            AddRoleMethod(8200060, src_ms, "8200060", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkGetAllBaseID.ToString());
            AddRoleMethod(8200070, src_ms, "8200070", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkClaimRecordsBaseID.ToString());
            AddRoleMethod(8200090, src_ms, "8200090", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkReleaseRecordBaseID.ToString());
            AddRoleMethod(8200100, src_ms, "8200100", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkCloneBaseID.ToString());
            AddRoleMethod(8200110, src_ms, "8200110", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkGetPageSizeBaseID.ToString());
            AddRoleMethod(8200120, src_ms, "8200120", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.ReplicaWorkQueueScanBaseID.ToString());

            // GeneralWork
            AddRoleMethod(8300000, src_ms, "8300000", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkDeleteBaseID.ToString());
            AddRoleMethod(8300010, src_ms, "8300010", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkGetByIDBaseID.ToString());
            AddRoleMethod(8300020, src_ms, "8300020", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkGetCountBaseID.ToString());
            AddRoleMethod(8300030, src_ms, "8300030", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkGetSearchBaseID.ToString());
            AddRoleMethod(8300040, src_ms, "8300040", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkNewBaseID.ToString());
            AddRoleMethod(8300050, src_ms, "8300050", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkSaveBaseID.ToString());
            AddRoleMethod(8300060, src_ms, "8300060", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkGetAllBaseID.ToString());
            AddRoleMethod(8300070, src_ms, "8300070", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkClaimRecordsBaseID.ToString());
            AddRoleMethod(8300090, src_ms, "8300090", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkReleaseRecordBaseID.ToString());
            AddRoleMethod(8300100, src_ms, "8300100", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkCloneBaseID.ToString());
            AddRoleMethod(8300110, src_ms, "8300110", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkGetPageSizeBaseID.ToString());
            AddRoleMethod(8300120, src_ms, "8300120", APIRolesID.SysWorkAdminID.ToString(), APIMethodID.GeneralWorkQueueScanBaseID.ToString());


            return "<p>RoleMethods Core Data: " + reccount.ToString() + " RECORDS, " + scancount.ToString() + " Created, " + skipcount.ToString() + " Skipped</p>";
        }

        private void AddRoleMethod(long rowid,
                                string row_ms,
                                string recID,
                                string roleID,
                                string methodID)
        {
            try
            {
                reccount++;

                RoleMethods_dml rolemethods_Dml = new RoleMethods_dml(_dbconnstr);
                string tmpresult = rolemethods_Dml.Replicate(rowid.ToString(), row_ms, SysInfoMaster.SourceDB, recID, RecState.Active, APIUserID.SysAdminID.ToString(), roleID, methodID, _dbconnstr);

                if (tmpresult == "OK")
                {
                    scancount++;
                }
                else
                {
                    skipcount++;
                    string msg = "row_id: " + rowid + ", rec_gid: " + recID + ", RoleID: " + roleID + ", MethodID: " + methodID;
                    RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "RoleMethods_data.AddMethod", "CLIENT", "INFO", "Skipped RoleMethod", msg);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "RoleMethods_data.AddMethod", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
            }
        }



    }
}
