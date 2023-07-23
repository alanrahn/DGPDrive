
using System;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;

namespace DGPDatabase.DGPSec
{
    public class MethSchema : ITbl
    {
        public MethSchema()
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
                                    " IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + DGPSecTables.DGPMeth + "]') AND type in (N'U'))\n" +
                                    " BEGIN\n" +
                                    " CREATE TABLE[dbo].[" + DGPSecTables.DGPMeth + "](\n" +

                                    " [" + CmnFields.row_id + "] [bigint] IDENTITY(1000000, 1) NOT NULL,\n" +
                                    " [" + CmnFields.row_ms + "] [bigint] NOT NULL,\n" +
                                    " [" + CmnFields.rec_gid + "] [varchar] (50) NOT NULL,\n" +
                                    " [" + CmnFields.rec_dbname + "] [varchar] (50) NOT NULL,\n" +
                                    " [" + CmnFields.rec_state + "] [varchar] (10) NOT NULL,\n" +
                                    " [" + CmnFields.rec_acct + "] [varchar] (50) NOT NULL,\n" +
                                    " [" + CmnFields.src_id + "] [bigint] NOT NULL,\n" +
                                    " [" + CmnFields.src_ms + "] [bigint] NOT NULL,\n" +

                                    " [" + DGPMeth.FullName + "] [varchar] (100) NOT NULL,\n" +
                                    " [" + DGPMeth.APIName + "] [varchar] (30) NOT NULL,\n" +
                                    " [" + DGPMeth.MethName + "] [varchar] (70) NOT NULL,\n" +
                                    " [" + DGPMeth.MethDescrip + "] [varchar] (100) NOT NULL,\n" +
                                    " [" + DGPMeth.MethType + "] [varchar] (10) NOT NULL,\n" +

                                    // PK index
                                    " CONSTRAINT[PK_DGPMeth] PRIMARY KEY CLUSTERED\n" + 
                                    " ([row_id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    " SET ANSI_PADDING ON;\n" +

                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMeth]') AND name = N'recgid_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recgid_idx] ON[dbo].[DGPMeth]([rec_gid] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +
                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMeth]') AND name = N'recstate_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recstate_idx] ON[dbo].[DGPMeth]([rec_state] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMeth]') AND name = N'recdbname_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recdbname_idx] ON[dbo].[DGPMeth]([rec_dbname] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMeth]') AND name = N'fullname_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[fullname_idx] ON[dbo].[DGPMeth]([FullName] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +
                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMeth]') AND name = N'apiname_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[apiname_idx] ON[dbo].[DGPMeth]([APIName] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +
                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMeth]') AND name = N'methodname_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[methodname_idx] ON[dbo].[DGPMeth]([MethodName] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_DGPMeth_recstate]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[DGPMeth] ADD CONSTRAINT[DF_DGPMeth_recstate]  DEFAULT('ACTIVE') FOR[rec_state]\n" +
                                    " END;\n" +
                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_DGPMeth_srcid]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[DGPMeth] ADD CONSTRAINT[DF_DGPMeth_srcid]  DEFAULT((0)) FOR[src_id]\n" +
                                    " END;\n" +
                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_DGPMeth_recdbname]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[DGPMeth] ADD CONSTRAINT[DF_DGPMeth_recdbname]  DEFAULT('" + DBName + "') FOR[rec_dbname]\n" +
                                    " END;\n";

                // add new columns, indexes and defaults below (table schema treated as immutable, append-only):

                DBUtil dBUtil = new DBUtil();
                int rowsaff = dBUtil.Execute(sqlCmd, SvrConnStr);

                if (rowsaff == -1)
                {
                    scanResult += "<p class=\"success\">DGPMeth Table Schema : OK</p>";

                    MethData methData = new MethData(SvrConnStr);
                    scanResult += methData.AddSecData();
                }
                else
                {
                    // error
                    scanResult = "<p class=\"error\">DGPMeth Table Schema : ERROR : An unknown problem occurred creating or updating the table.</p>";
                }
            }
            catch (Exception ex)
            {
                scanResult = "<p class=\"error\">DGPMeth Table Schema : EXCEPTION : " + ex.Message + "</p>";
            }

            return scanResult;
        }
    }
}
