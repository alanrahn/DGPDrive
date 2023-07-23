

namespace SysInfoDB.APIMethods
{
    /// <summary>
    /// global ID values for the API methods documented in the SysInfo database (used for authorization)
    /// </summary>
    public static class APIMethodID
    {
        // ***************************************************************** //
        // DGPAdminAPI Methods - 1,000,000
        // ***************************************************************** //

        // DataGroup API methods
        public const int GroupGetCountBaseID = 1010000;
        public const int GroupGetSearchBaseID = 1010010;
        public const int GroupGetByIDBaseID = 1010020;
        public const int GroupGetByNameBaseID = 1010030;
        public const int GroupGetHistoryBaseID = 1010040;
        public const int GroupGetSourceBaseID = 1010050;
        public const int GroupNewBaseID = 1010060;
        public const int GroupSaveBaseID = 1010070;
        public const int GroupDeleteBaseID = 1010080;
        public const int GroupReplicateBaseID = 1010090;
        public const int GroupRecoverBaseID = 1010100;
        public const int GroupDuplicateBaseID = 1010110;
        public const int GroupGetSrcCountsBaseID = 1010120;
        public const int GroupGetDestCountsBaseID = 1010130;
        public const int GroupGetPageSizeBaseID = 1010140;

        // GroupUser API methods
        public const int GroupUserGetAssignedBaseID = 1020000;
        public const int GroupUserGetAvailableBaseID = 1020010;
        public const int GroupUserGetSourceBaseID = 1020020;
        public const int GroupUserNewBaseID = 1020030;
        public const int GroupUserDeleteBaseID = 1020040;
        public const int GroupUserReplicateBaseID = 1020050;
        public const int GroupUserDuplicateBaseID = 1020060;
        public const int GroupUserGetSrcCountsBaseID = 1020070;
        public const int GroupUserGetDestCountsBaseID = 1020080;

        // Method API methods
        public const int MethodGetCountBaseID = 1030000;
        public const int MethodGetSearchBaseID = 1030010;
        public const int MethodGetByIDBaseID = 1030020;
        public const int MethodGetByNameBaseID = 1030030;
        public const int MethodGetHistoryBaseID = 1030040;
        public const int MethodGetSourceBaseID = 1030050;
        public const int MethodGetAPIListBaseID = 1030060;
        public const int MethodNewBaseID = 1030070;
        public const int MethodSaveBaseID = 1030080;
        public const int MethodDeleteBaseID = 1030090;
        public const int MethodReplicateBaseID = 1030100;
        public const int MethodRecoverBaseID = 1030110;
        public const int MethodDuplicateBaseID = 1030120;
        public const int MethodGetSrcCountsBaseID = 1030130;
        public const int MethodGetDestCountsBaseID = 1030140;
        public const int MethodGetPageSizeBaseID = 1030150;

        // RoleMethod API methods
        public const int RoleMethodGetAssignedBaseID = 1040000;
        public const int RoleMethodGetAvailableBaseID = 1040010;
        public const int RoleMethodGetSourceBaseID = 1040020;
        public const int RoleMethodGetUserMethodsBaseID = 1040030;
        public const int RoleMethodGetMethodRolesBaseID = 1040040;
        public const int RoleMethodNewBaseID = 1040050;
        public const int RoleMethodDeleteBaseID = 1040060;
        public const int RoleMethodReplicateBaseID = 1040070;
        public const int RoleMethodDuplicateBaseID = 1040080;
        public const int RoleMethodGetSrcCountsBaseID = 1040090;
        public const int RoleMethodGetDestCountsBaseID = 1040100;

        // Role API methods
        public const int RoleGetCountBaseID = 1050000;
        public const int RoleGetSearchBaseID = 1050010;
        public const int RoleGetByIDBaseID = 1050020;
        public const int RoleGetByNameBaseID = 1050030;
        public const int RoleGetHistoryBaseID = 1050040;
        public const int RoleGetSourceBaseID = 1050050;
        public const int RoleNewBaseID = 1050060;
        public const int RoleSaveBaseID = 1050070;
        public const int RoleDeleteBaseID = 1050080;
        public const int RoleReplicateBaseID = 1050090;
        public const int RoleRecoverBaseID = 1050100;
        public const int RoleDuplicateBaseID = 1050110;
        public const int RoleGetSrcCountsBaseID = 1050120;
        public const int RoleGetDestCountsBaseID = 1050130;
        public const int RoleGetPageSizeBaseID = 1050140;

        // RoleUser API methods
        public const int RoleUserGetAssignedBaseID = 1060000;
        public const int RoleUserGetAvailableBaseID = 1060010;
        public const int RoleUserGetSourceBaseID = 1060020;
        public const int RoleUserNewBaseID = 1060030;
        public const int RoleUserDeleteBaseID = 1060040;
        public const int RoleUserReplicateBaseID = 1060050;
        public const int RoleUserDuplicateBaseID = 1060060;
        public const int RoleUserGetSrcCountsBaseID = 1060070;
        public const int RoleUserGetDestCountsBaseID = 1060080;

        // User API methods
        public const int UserCheckNameBaseID = 1070000;
        public const int UserGetCountBaseID = 1070020;
        public const int UserGetSearchBaseID = 1070030;
        public const int UserGetByIDBaseID = 1070040;
        public const int UserGetByNameBaseID = 1070050;
        public const int UserGetHistoryBaseID = 1070060;
        public const int UserGetSourceBaseID = 1070070;
        public const int UserNewBaseID = 1070080;
        public const int UserSaveBaseID = 1070090;
        public const int UserDeleteBaseID = 1070100;
        public const int UserReplicateBaseID = 1070110;
        public const int UserRecoverBaseID = 1070120;
        public const int UserChangePasswordBaseID = 1070130;
        public const int UserDuplicateBaseID = 1070140;
        public const int UserGetSrcCountsBaseID = 1070150;
        public const int UserGetDestCountsBaseID = 1070160;
        public const int UserGetPageSizeBaseID = 1070170;

        // Self API methods
        public const int UserSelfLoginBaseID = 1080000;
        public const int UserSelfGetRolesID = 1080010;
        public const int UserSelfGetGroupsID = 1080020;
        public const int UserSelfGetInfoID = 1080030;
        public const int UserSelfGetUserGroupListBaseID = 1080050;
        public const int UserSelfSaveBaseID = 1080060;
        public const int UserSelfChangePasswordBaseID = 1080070;

        // ***************************************************************** //
        // DGPDrive Methods
        // ***************************************************************** //

        // Folder API methods
        public const int FolderGetUserSubFoldersBaseID = 1090000;
        public const int FolderGetByIDBaseID = 1090010;
        public const int FolderGetSourceBaseID = 1090020;
        public const int FolderAddSubFolderBaseID = 1090030;
        public const int FolderSaveBaseID = 1090040;
        public const int FolderDeleteBaseID = 1090050;
        public const int FolderReplicateBaseID = 1090060;
        public const int FolderDuplicateBaseID = 1090070;
        public const int FolderGetSrcCountsBaseID = 1090080;
        public const int FolderGetDestCountsBaseID = 1090090;

        // File API methods
        public const int FileGetCountByFolderBaseID = 1110000;
        public const int FileGetFilesByFolderBaseID = 1110010;
        public const int FileGetCountByMetadataBaseID = 1110020;
        public const int FileGetFilesByMetadataBaseID = 1110030;
        public const int FileGetCountByFavoriteBaseID = 1110040;
        public const int FileGetFilesByFavoriteBaseID = 1110050;
        public const int FileGetByIDBaseID = 1110060;
        public const int FileGetByNameBaseID = 1110070;
        public const int FileGetExtListBaseID = 1110080;
        public const int FileGetHistoryBaseID = 1110090;
        public const int FileDeleteBaseID = 1110100;
        public const int FileRecoverBaseID = 1110110;
        public const int FileNewBaseID = 1110120;
        public const int FileGetSourceBaseID = 1110140;
        public const int FileReplicateBaseID = 1110160;
        public const int FileSaveBaseID = 1110170;
        public const int FileGetCountByTagBaseID = 1110190;
        public const int FileGetFilesByTagBaseID = 1110200;
        public const int FileDuplicateBaseID = 1110210;
        public const int FileGetSrcCountsBaseID = 1110230;
        public const int FileGetDestCountsBaseID = 1110240;
        public const int FileSaveFileRecBaseID = 1110250;
        public const int FileRemoveBaseID = 1110260;
        public const int FileGetPageSizeBaseID = 1110270;

        // FileShard API methods
        public const int FileShardGetByIDBaseID = 1230000;
        public const int FileShardGetDataBySegNumBaseID = 1230010;
        public const int FileShardGetByRowIDBaseID = 1230030;
        public const int FileShardNewBaseID = 1230040;
        public const int FileShardDeleteBaseID = 1230050;
        public const int FileShardGetSourceBaseID = 1230060;
        public const int FileShardDuplicateBaseID = 1230070;
        public const int FileShardGetSrcCountsBaseID = 1230080;
        public const int FileShardGetDestCountsBaseID = 1230090;
        public const int FileShardReplicateBaseID = 1230100;
        public const int FileShardGetShardNameBaseID = 1230110;
        public const int FileShardGetPageSizeBaseID = 1230120;
        public const int FileShardGetSegCountBaseID = 1230130;

        // Favorites API methods
        public const int FavoriteGetSourceBaseID = 1120020;
        public const int FavoriteAssignBaseID = 1120030;
        public const int FavoriteRemoveBaseID = 1120050;
        public const int FavoriteReplicateBaseID = 1120060;
        public const int FavoriteDuplicateBaseID = 1120070;
        public const int FavoriteGetSrcCountsBaseID = 1120080;
        public const int FavoriteGetDestCountsBaseID = 1120090;
        public const int FavoriteGetPageSizeBaseID = 1120100;

        // FileTags API methods
        public const int FileTagGetAssignedBaseID = 1130020;
        public const int FileTagGetAvailableBaseID = 1130030;
        public const int FileTagAssignBaseID = 1130040;
        public const int FileTagRemoveBaseID = 1130050;
        public const int FileTagGetSourceBaseID = 1130060;
        public const int FileTagReplicateBaseID = 1130070;
        public const int FileTagDuplicateBaseID = 1130080;
        public const int FileTagGetSrcCountsBaseID = 1130090;
        public const int FileTagGetDestCountsBaseID = 1130100;

        // Tags API methods
        public const int TagGetByIDBaseID = 1140020;
        public const int TagGetByNameBaseID = 1140030;
        public const int TagFilterByNameBaseID = 1140040;
        public const int TagGetSourceBaseID = 1140050;
        public const int TagNewBaseID = 1140060;
        public const int TagSaveBaseID = 1140070;
        public const int TagDeleteBaseID = 1140080;
        public const int TagReplicateBaseID = 1140090;
        public const int TagDuplicateBaseID = 1140100;
        public const int TagGetSrcCountsBaseID = 1140110;
        public const int TagGetDestCountsBaseID = 1140120;
        public const int TagGetPageSizeBaseID = 1140130;
        public const int TagGetCountBaseID = 1140140;
        public const int TagGetSearchBaseID = 1140150;
        public const int TagGetHistoryBaseID = 1140160;
        public const int TagRecoverBaseID = 1140170;



        // ***************************************************************** //
        // DGPTestAPI Methods
        // ***************************************************************** //

        // Test methods
        public const int TestEchoTestBaseID = 1170020;
        public const int TestLoggingTestBaseID = 1170030;
        public const int TestExceptionTestBaseID = 1170040;
        public const int TestGetDBNameBaseID = 1170050;


        // ***************************************************************** //
        // SysMetrics API Methods
        // ***************************************************************** //

        // TestResultAPI
        public const int TestResultNewBaseID = 1180000;
        public const int TestResultGetByIDBaseID = 1180010;
        public const int TestResultGetAllBaseID = 1180020;
        public const int TestResultDeleteBaseID = 1180030;
        public const int TestResultGetCountBaseID = 1180040;
        public const int TestResultGetSearchBaseID = 1180050;
        public const int TestResultGetPageSizeBaseID = 1180060;
        public const int TestResultGetEvalInfoBaseID = 1180070;

        // DGPMetricsAPI
        public const int DGPMetricsNewBaseID = 1180100;
        public const int DGPMetricsGetByIDBaseID = 1180110;
        public const int DGPMetricsGetAllBaseID = 1180120;
        public const int DGPMetricsDeleteBaseID = 1180130;
        public const int DGPMetricsGetCountBaseID = 1180140;
        public const int DGPMetricsGetSearchBaseID = 1180150;
        public const int DGPMetricsGetPageSizeBaseID = 1180160;
        public const int DGPMetricsServerBaseID = 1180170;

        // DGPErrorAPI
        public const int DGPErrorNewBaseID = 1180200;
        public const int DGPErrorGetByIDBaseID = 1180210;
        public const int DGPErrorGetAllBaseID = 1180220;
        public const int DGPErrorDeleteBaseID = 1180230;
        public const int DGPErrorGetCountBaseID = 1180240;
        public const int DGPErrorGetSearchBaseID = 1180250;
        public const int DGPErrorGetPageSizeBaseID = 1180260;
        public const int DGPErrorGetErrDataBaseID = 1180270;

        // AutoWorkLogAPI
        public const int AutoWorkLogNewBaseID = 1180300;
        public const int AutoWorkLogGetByIDBaseID = 1180310;
        public const int AutoWorkLogGetAllBaseID = 1180320;
        public const int AutoWorkLogDeleteBaseID = 1180330;
        public const int AutoWorkLogGetCountBaseID = 1180340;
        public const int AutoWorkLogGetSearchBaseID = 1180350;
        public const int AutoWorkLogGetPageSizeBaseID = 1180360;
        public const int AutoWorkLogGetProcStepsBaseID = 1180370;


        // ***************************************************************** //
        // SysWork API Methods
        // ***************************************************************** //

        // ReplicaWork API 
        public const int ReplicaWorkGetCountBaseID = 1210000;
        public const int ReplicaWorkGetSearchBaseID = 1210010;
        public const int ReplicaWorkGetByIDBaseID = 1210020;
        public const int ReplicaWorkNewBaseID = 1210040;
        public const int ReplicaWorkSaveBaseID = 1210050;
        public const int ReplicaWorkDeleteBaseID = 1210060;
        public const int ReplicaWorkGetAllBaseID = 1210070;
        public const int ReplicaWorkClaimRecordsBaseID = 1210080;
        public const int ReplicaWorkReleaseRecordBaseID = 1210100;
        public const int ReplicaWorkCloneBaseID = 1210110;
        public const int ReplicaWorkGetPageSizeBaseID = 1210120;
        public const int ReplicaWorkQueueScanBaseID = 1210130;

        // GeneralWork API
        public const int GeneralWorkGetCountBaseID = 1220000;
        public const int GeneralWorkGetSearchBaseID = 1220010;
        public const int GeneralWorkGetByIDBaseID = 1220020;
        public const int GeneralWorkNewBaseID = 1220040;
        public const int GeneralWorkSaveBaseID = 1220050;
        public const int GeneralWorkDeleteBaseID = 1220060;
        public const int GeneralWorkGetAllBaseID = 1220070;
        public const int GeneralWorkClaimRecordsBaseID = 1220080;
        public const int GeneralWorkReleaseRecordBaseID = 1220100;
        public const int GeneralWorkCloneBaseID = 1220110;
        public const int GeneralWorkGetPageSizeBaseID = 1220120;
        public const int GeneralWorkQueueScanBaseID = 1220130;

    }
}
