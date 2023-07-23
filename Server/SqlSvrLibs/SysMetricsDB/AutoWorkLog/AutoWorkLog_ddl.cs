using System;
using System.Data.SqlClient;

using SqlSvrUtil;

namespace SysMetricsDB
{
    public class AutoWorkLog_ddl : ITbl
    {
        public AutoWorkLog_ddl()
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
                                    " IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AutoWorkLog]') AND type in (N'U'))\n" +
                                    " BEGIN\n" +
                                    " CREATE TABLE[dbo].[AutoWorkLog](\n" +

                                    " [row_id] [int] IDENTITY(1000,1) NOT NULL,\n" +
                                    " [row_gid] [varchar](50) NOT NULL,\n" +
                                    " [LogTime] [datetime2](7) NOT NULL,\n" +
                                    " [WorkType] [varchar](10) NOT NULL,\n" +
                                    " [CompName] [varchar](50) NOT NULL,\n" +
                                    " [ClaimedBy] [varchar](50) NOT NULL,\n" +
                                    " [DurationMS] [int] NOT NULL,\n" +
                                    " [MaxDurMS] [int] NOT NULL,\n" +
                                    " [RunState] [varchar](10) NOT NULL,\n" +
                                    " [StateMsg] [varchar](250) NULL,\n" +
                                    " [ProcState] [varchar](10) NULL,\n" +
                                    " [ProcSteps] [varchar](5000) NULL,\n" +

                                    // PK index
                                    " CONSTRAINT[PK_AutoWorkLog] PRIMARY KEY CLUSTERED\n([row_id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    " SET ANSI_PADDING ON;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AutoWorkLog]') AND name = N'LogTime_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[LogTime_idx] ON[dbo].[AutoWorkLog]([LogTime] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AutoWorkLog]') AND name = N'WorkType_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[WorkType_idx] ON[dbo].[AutoWorkLog]([WorkType] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_AutoWorkLog_LogTime]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[AutoWorkLog] ADD CONSTRAINT[DF_AutoWorkLog_LogTime]  DEFAULT(getutcdate()) FOR[LogTime]\n" +
                                    " END;\n";

                // add new columns, indexes and defaults below (table schema treated as immutable, append-only):

                DBUtil dBUtil = new DBUtil();
                int rowsaff = dBUtil.Execute(sqlCmd, SvrConnStr);

                if (rowsaff == -1)
                {
                    // success
                    scanResult = "<p class=\"success\">AutoWorkLog Table Schema : OK</p>";
                }
                else
                {
                    // error
                    scanResult = "<p class=\"error\">AutoWorkLog Table Schema : ERROR : An unknown problem occurred creating or updating the table.</p>";
                }
            }
            catch (Exception ex)
            {
                scanResult = "<p class=\"error\">AutoWorkLog Table Schema : EXCEPTION : " + ex.Message + "</p>";
            }

            return scanResult;
        }
    }
}
