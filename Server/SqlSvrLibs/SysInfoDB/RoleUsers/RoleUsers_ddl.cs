﻿using System;
using System.Data.SqlClient;

using SqlSvrUtil;

namespace SysInfoDB.RoleUsers
{
    class RoleUsers_ddl : ITbl
    {


        /// <summary>
        /// 
        /// </summary>
        public string ScanTable(string DBName, string SvrConnStr)
        {
            string scanResult = "";
            SqlCommand sqlCmd = new SqlCommand();

            try
            {
                sqlCmd.CommandText = "USE [" + DBName + "];\n" +
                                    " SET ANSI_NULLS ON;\n" +
                                    " SET QUOTED_IDENTIFIER ON;\n" +
                                    // table
                                    " IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND type in (N'U'))\n" +
                                    " BEGIN\n" +
                                    " CREATE TABLE[dbo].[RoleUsers](\n" +

                                    " [row_id] [bigint] IDENTITY(1000000, 1) NOT NULL,\n" +
                                    " [row_ms] [bigint] NOT NULL,\n" +
                                    " [rec_gid] [varchar] (50) NOT NULL,\n" +
                                    " [rec_dbname] [varchar] (50) NOT NULL,\n" +
                                    " [rec_state] [varchar] (10) NOT NULL,\n" +
                                    " [rec_user] [varchar] (50) NOT NULL,\n" +
                                    " [src_id] [bigint] NOT NULL,\n" +
                                    " [src_ms] [bigint] NOT NULL,\n" +

                                    " [RoleGID] [varchar] (50) NOT NULL,\n" +
                                    " [UserGID] [varchar] (50) NOT NULL,\n" +

                                    // PK index
                                    " CONSTRAINT[PK_RoleUsers] PRIMARY KEY CLUSTERED\n([row_id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    " SET ANSI_PADDING ON;\n" +

                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND name = N'recgid_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recgid_idx] ON[dbo].[RoleUsers]([rec_gid] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +
                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND name = N'recstate_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recstate_idx] ON[dbo].[RoleUsers]([rec_state] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND name = N'recdbname_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recdbname_idx] ON[dbo].[RoleUsers]([rec_dbname] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND name = N'rolegid_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[rolegid_idx] ON[dbo].[RoleUsers]([RoleGID] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +
                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND name = N'usergid_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[usergid_idx] ON[dbo].[RoleUsers]([UserGID] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +
                                    
                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleUsers_recstate]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[RoleUsers] ADD CONSTRAINT[DF_RoleUsers_recstate]  DEFAULT('ACTIVE') FOR[rec_state]\n" +
                                    " END;\n" +
                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleUsers_srcid]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[RoleUsers] ADD CONSTRAINT[DF_RoleUsers_srcid]  DEFAULT((0)) FOR[src_id]\n" +
                                    " END;\n" +
                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleUsers_recdbname]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[RoleUsers] ADD CONSTRAINT[DF_RoleUsers_recdbname]  DEFAULT('" + DBName + "') FOR[rec_dbname]\n" +
                                    " END;\n";

                // add new columns, indexes and defaults below (table schema treated as immutable, append-only):

                DBUtil dBUtil = new DBUtil();
                int rowsaff = dBUtil.Execute(sqlCmd, SvrConnStr);

                if (rowsaff == -1)
                {
                    // success
                    scanResult = "<p class=\"success\">RoleUsers Table Schema : OK</p>";

                    RoleUsers_data roleUsers_Data = new RoleUsers_data(SvrConnStr);
                    scanResult += roleUsers_Data.AddCoreData();
                }
                else
                {
                    // error
                    scanResult = "<p class=\"error\">RoleUsers Table Schema : ERROR : An unknown problem occurred creating or updating the table.</p>";
                }
            }
            catch (Exception ex)
            {
                scanResult = "<p class=\"error\">RoleUsers Table Schema : EXCEPTION : " + ex.Message + "</p>";
            }

            return scanResult;
        }
    }
}
