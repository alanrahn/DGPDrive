
using System;
using System.Data.SqlClient;

using SqlSvrUtil;

namespace SysWorkDB
{
    public class ReplicaWork_ddl : ITbl
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
                                    " IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReplicaWork]') AND type in (N'U'))\n" +
                                    " BEGIN\n" +
                                    " CREATE TABLE[dbo].[ReplicaWork](\n" +

                                    " [row_id] [bigint] IDENTITY(1000000, 1) NOT NULL,\n" +
                                    " [row_ms] [bigint] NOT NULL,\n" +
                                    " [rec_gid] [varchar] (50) NOT NULL,\n" +
                                    " [rec_state] [varchar] (10) NOT NULL,\n" +
                                    " [rec_user] [varchar] (50) NOT NULL,\n" +

                                    " [SchemaType] [varchar](10) NULL,\n" +
                                    " [SchemaTable] [varchar](50) NOT NULL,\n" +
                                    " [SrcDBName] [varchar](50) NOT NULL,\n" +
                                    " [ShardName] [varchar](50) NOT NULL,\n" +
                                    " [WorkType] [varchar](10) NOT NULL,\n" +
                                    " [SrcURL] [varchar](100) NOT NULL,\n" +
                                    " [SrcMethod] [varchar](100) NOT NULL,\n" +
                                    " [DestURL] [varchar](100) NOT NULL,\n" +
                                    " [DestMethod] [varchar](100) NOT NULL,\n" +
                                    " [StartID] [bigint] NOT NULL,\n" +
                                    " [FinalID] [bigint] NOT NULL,\n" +
                                    " [BatchSize] [int] NOT NULL,\n" +
                                    " [IntervalMS] [int] NOT NULL,\n" +
                                    " [NextRun] [bigint] NOT NULL,\n" +
                                    " [RunState] [varchar](10) NOT NULL,\n" +
                                    " [StateMsg] [varchar](250) NULL,\n" +
                                    " [Logging] [varchar](10) NULL,\n" +
                                    " [ClaimedBy] [varchar](50) NULL,\n" +
                                    " [ClaimID] [varchar](50) NULL,\n" +
                                    " [ClaimTime] [bigint] NULL,\n" +
                                    " [ProcState] [varchar](10) NULL,\n" +
                                    " [MaxDurMS] [int] NULL,\n" +
                                    " [Network] [varchar](10) NULL,\n" +
                                    " [ResetCount] [int] NULL,\n" +

                                    // PK index
                                    " CONSTRAINT[PK_ReplicaWork] PRIMARY KEY CLUSTERED\n([row_id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    " SET ANSI_PADDING ON;\n" +

                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ReplicaWork]') AND name = N'recgid_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE UNIQUE NONCLUSTERED INDEX[recgid_idx] ON[dbo].[ReplicaWork]([rec_gid] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ReplicaWork]') AND name = N'recstate_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[recstate_idx] ON[dbo].[ReplicaWork]([rec_state] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ReplicaWork]') AND name = N'runstate_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[runstate_idx] ON[dbo].[ReplicaWork]([RunState] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ReplicaWork]') AND name = N'nextrun_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[nextrun_idx] ON[dbo].[ReplicaWork]([NextRun] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ReplicaWork]') AND name = N'claimedby_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[claimedby_idx] ON[dbo].[ReplicaWork]([ClaimedBy] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_ReplicaWork_recstate]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[ReplicaWork] ADD CONSTRAINT[DF_ReplicaWork_recstate]  DEFAULT('ACTIVE') FOR[rec_state]\n" +
                                    " END;\n";
                
                // add new columns, indexes and defaults below (table schema above treated as immutable, append-only):

                DBUtil dBUtil = new DBUtil();
                int rowsaff = dBUtil.Execute(sqlCmd, SvrConnStr);

                if (rowsaff == -1)
                {
                    // success
                    scanResult = "<p class=\"success\">ReplicaWork Table Schema : OK</p>";

                    //ReplicaWork_data ReplicaWork_Data = new ReplicaWork_data(SvrConnStr);
                    //scanResult += ReplicaWork_Data.AddCoreData();
                }
                else
                {
                    // error
                    scanResult = "<p class=\"error\">ReplicaWork Table Schema : ERROR : An unknown problem occurred creating or updating the table.</p>";
                }
            }
            catch (Exception ex)
            {
                scanResult = "<p class=\"error\">ReplicaWork Table Schema : EXCEPTION : " + ex.Message + "</p>";
            }

            return scanResult;
        }

    }
}
