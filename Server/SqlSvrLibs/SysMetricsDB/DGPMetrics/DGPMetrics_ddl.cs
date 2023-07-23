using System;
using System.Data.SqlClient;

using SqlSvrUtil;

namespace SysMetricsDB
{
    public class DGPMetrics_ddl : ITbl
    {
        public DGPMetrics_ddl()
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
                                    " IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DGPMetrics]') AND type in (N'U'))\n" +
                                    " BEGIN\n" +
                                    " CREATE TABLE[dbo].[DGPMetrics](\n" +

                                    " [row_id] [int] IDENTITY(1000,1) NOT NULL,\n" +
                                    " [row_gid] [varchar](50) NOT NULL,\n" +

                                    " [UserName] [varchar](50) NOT NULL,\n" +
                                    " [CompName] [varchar](50) NOT NULL,\n" +
                                    " [AppName] [varchar](50) NOT NULL,\n" +
                                    " [FormName] [varchar](50) NOT NULL,\n" +
                                    " [WebSvcName] [varchar](50) NOT NULL,\n" +
                                    " [WebSvcVer] [varchar](50) NOT NULL,\n" +
                                    " [ServerTime] [datetime2](7) NOT NULL,\n" +
                                    " [ClientTime] [datetime2](7) NOT NULL,\n" +
                                    " [MethodCount] [int] NOT NULL,\n" +
                                    " [EndToEndMS] [float] NOT NULL,\n" +
                                    " [NetworkMS] [float] NOT NULL,\n" +
                                    " [ServerMS] [float] NOT NULL,\n" +

                                    // PK index
                                    " CONSTRAINT[PK_DGPMetrics] PRIMARY KEY CLUSTERED\n([row_id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]\n" +
                                    " END;\n" +
                                    " SET ANSI_PADDING ON;\n" +

                                    // DGP index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMetrics]') AND name = N'rowgid_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE UNIQUE NONCLUSTERED INDEX[rowgid_idx] ON[dbo].[DGPMetrics]([row_gid] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMetrics]') AND name = N'AppName_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[AppName_idx] ON[dbo].[DGPMetrics]([AppName] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMetrics]') AND name = N'FormName_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[FormName_idx] ON[dbo].[DGPMetrics]([FormName] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // schema index
                                    " IF NOT EXISTS(SELECT* FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DGPMetrics]') AND name = N'WebSvcName_idx')\n" +
                                    " BEGIN\n" +
                                    " CREATE NONCLUSTERED INDEX[WebSvcName_idx] ON[dbo].[DGPMetrics]([WebSvcName] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];\n" +
                                    " END;\n" +

                                    // default
                                    " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_DGPMetrics_ServerTime]') AND type = 'D')\n" +
                                    " BEGIN\n" +
                                    " ALTER TABLE[dbo].[DGPMetrics] ADD CONSTRAINT[DF_DGPMetrics_ServerTime]  DEFAULT(getutcdate()) FOR[ServerTime]\n" +
                                    " END;\n";

                // add new columns, indexes and defaults below (table schema treated as immutable, append-only):

                DBUtil dBUtil = new DBUtil();
                int rowsaff = dBUtil.Execute(sqlCmd, SvrConnStr);

                if (rowsaff == -1)
                {
                    // success
                    scanResult = "<p class=\"success\">DGPMetrics Table Schema : OK</p>";
                }
                else
                {
                    // error
                    scanResult = "<p class=\"error\">DGPMetrics Table Schema : ERROR : An unknown problem occurred creating or updating the table.</p>";
                }
            }
            catch (Exception ex)
            {
                scanResult = "<p class=\"error\">DGPMetrics Table Schema : EXCEPTION : " + ex.Message + "</p>";
            }

            return scanResult;
        }
    }
}
