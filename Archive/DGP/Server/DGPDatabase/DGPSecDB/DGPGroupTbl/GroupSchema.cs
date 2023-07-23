
using System;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class GroupSchema : ITbl
    {
        public GroupSchema()
        {

        }

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
                                    " IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + DGPSecTables.DGPAcctGroup + "]') AND type in (N'U'))\n" +
                                    " BEGIN\n" +
                                    " CREATE TABLE[dbo].[" + DGPSecTables.DGPAcctGroup + "](\n" +

                                    " [" + CmnFields.row_id + "] [bigint] IDENTITY(1000000, 1) NOT NULL,\n" +
                                    " [" + CmnFields.row_ms + "] [bigint] NOT NULL,\n" +
                                    " [" + CmnFields.rec_gid + "] [varchar] (50) NOT NULL,\n" +
                                    " [" + CmnFields.rec_dbname + "] [varchar] (50) NOT NULL,\n" +
                                    " [" + CmnFields.rec_state + "] [varchar] (10) NOT NULL,\n" +
                                    " [" + CmnFields.rec_acct + "] [varchar] (50) NOT NULL,\n" +
                                    " [" + CmnFields.src_id + "] [bigint] NOT NULL,\n" +
                                    " [" + CmnFields.src_ms + "] [bigint] NOT NULL,\n" +

                                    " [" + DGPGroup.GroupName + "][varchar](50) NOT NULL,\n" +
                                    " [" + DGPGroup.GroupDescrip + "] [varchar] (100) NULL,\n" +

                                    // PK index
                                    " CONSTRAINT[PK_DGPGroup] PRIMARY KEY CLUSTERED\n" +
                                    " ([row_id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    " SET ANSI_PADDING ON;\n" +

                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPGroup]') AND name = N'recgid_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recgid_idx] ON[dbo].[DGPGroup]([rec_gid] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +
                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPGroup]') AND name = N'recstate_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recstate_idx] ON[dbo].[DGPGroup]([rec_state] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPGroup]') AND name = N'recdbname_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recdbname_idx] ON[dbo].[DGPGroup]([rec_dbname] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPGroup]') AND name = N'groupname_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[groupname_idx] ON[dbo].[DGPGroup]([GroupName] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;" +

                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_DGPGroup_recstate]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[DGPGroup] ADD CONSTRAINT[DF_DGPGroup_recstate]  DEFAULT('ACTIVE') FOR[rec_state]\n" +
                                    " END;\n" +
                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_DGPGroup_srcid]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[DGPGroup] ADD CONSTRAINT[DF_DGPGroup_srcid]  DEFAULT((0)) FOR[src_id]\n" +
                                    " END;\n" +
                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_DGPGroup_recdbname]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[DGPGroup] ADD CONSTRAINT[DF_DGPGroup_recdbname]  DEFAULT('" + DBName + "') FOR[rec_dbname]\n" +
                                    " END;\n";

                // add new columns, indexes and defaults below (table schema treated as immutable, append-only):

                DBUtil dBUtil = new DBUtil();
                int rowsaff = dBUtil.Execute(sqlCmd, SvrConnStr);

                if (rowsaff == -1)
                {
                    scanResult += "<p class=\"success\">DGPGroup Table Schema : OK</p>";

                    GroupData groupData = new GroupData(SvrConnStr);
                    //scanResult += groupData.AddCoreData();
                }
                else
                {
                    // error
                    scanResult = "<p class=\"error\">DGPGroup Table Schema : ERROR : An unknown problem occurred creating or updating the table.</p>";
                }
            }
            catch (Exception ex)
            {
                scanResult = "<p class=\"error\">DGPGroup Table Schema : EXCEPTION : " + ex.Message + "</p>";
            }

            return scanResult;
        }
    }
}
