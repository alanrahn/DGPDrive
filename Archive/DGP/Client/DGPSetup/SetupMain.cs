using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using CmnUtil;
using SvrUtil;
using DGPDatabase;

namespace DGPSetup
{
    public partial class SetupMain : Form
    {
        string _dbname = "";
        string _connstr = "";

        public SetupMain()
        {
            InitializeComponent();
        }

        private void ClearResults()
        {
            webbrResults.DocumentText = "0";
            webbrResults.Document.OpenNew(true);
            webbrResults.Document.Write(HTMLStart() + HTMLEnd());
            webbrResults.Refresh();
        }

        private void WriteResults(string resultHTML)
        {
            webbrResults.DocumentText = "0";
            webbrResults.Document.OpenNew(true);
            webbrResults.Document.Write(HTMLStart() + resultHTML + HTMLEnd());
            webbrResults.Refresh();
        }


        /// <summary>
        /// 
        /// </summary>
        public string BuildSvrConnStr(string hostName, string dbUsername, string dbUserpword)
        {
            string connstr = "Server=" + hostName + ";";
            connstr += "User ID=" + dbUsername + ";";
            connstr += "Password=" + dbUserpword + ";";

            return connstr;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BuildDBConnStr(string hostName, string dbName, string dbUsername, string dbUserpword)
        {
            string connstr = "Server=" + hostName + ";";
            connstr += "Database=" + dbName + ";";
            connstr += "User ID=" + dbUsername + ";";
            connstr += "Password=" + dbUserpword + ";";

            return connstr;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BuildEncDBConnStr(string hostName, string dbName, string dbUsername, string dbUserpword)
        {
            string connstr = "Server=" + hostName + ";";
            connstr += "Database=" + dbName + ";";
            connstr += "User ID=" + dbUsername + ";";
            connstr += "Password=" + dbUserpword + ";";
            connstr += "Column Encryption Setting=enabled;";

            return connstr;
        }

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //


        private void btnHostClear_Click(object sender, EventArgs e)
        {
            HostClear();

            DBListClear();

            BuildClear();
        }

        private void HostClear()
        {
            tbxHostName.Text = "";
            tbxDBAdminUser.Text = "";
            tbxDBAdminPword.Text = "";
        }

        private void DBListClear()
        {
            dgvHostDBList.DataSource = null;
        }

        private void BuildClear()
        {
            btnRunScan.Enabled = false;
        }

        private void btnHostConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string svrconnstr = BuildSvrConnStr(tbxHostName.Text, tbxDBAdminUser.Text, tbxDBAdminPword.Text);

                // populate listbox of databases on the new host
                DataTable databases = GetDatabases(svrconnstr);

                if (databases.Rows.Count > 0)
                {
                    // populate grid
                    dgvHostDBList.DataSource = databases.DefaultView;
                    dgvHostDBList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgvHostDBList.Columns[0].Width = 200;

                    lblSelectDatabase.Enabled = true;
                    dgvHostDBList.Enabled = true;
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "SetupMain.btnHostConnect_Click", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable GetDatabases(string serverConnStr)
        {
            DataTable qresult = new DataTable();
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "SELECT name, database_id, create_date FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb');";

            using (SqlConnection sqlConn = new SqlConnection(serverConnStr))
            {
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;

                using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                {
                    qresult.Load(sqlReader);
                    sqlConn.Close();
                }
            }

            return qresult;
        }

        private void dgvHostDBList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // get selected dbname
            _dbname = dgvHostDBList.SelectedRows[0].Cells["name"].Value.ToString();


            BuildClear();
        }

        private void btnRunScan_Click(object sender, EventArgs e)
        {
            string scanSummary = "";

            if (dgvHostDBList.SelectedRows.Count > 0)
            {
                this.UseWaitCursor = true;

                try
                {
                    
                    // scan selected dbname for DGP schema name


                    string schemaname = "";

                    switch (schemaname.ToLower())
                    {
                        case "dgpsec":

                            scanSummary += DGPS.ScanDB(_dbname, tbxDBConnStr.Text);
                            break;

                        case "dgpdata":

                            scanSummary += sysMetricsSchema.ScanDB(_dbname, tbxDBConnStr.Text);
                            break;

                        case "dgpproc":

                            scanSummary += syswork.ScanDB(_dbname, tbxDBConnStr.Text);
                            break;

                        case "dgpdrive":

                            scanSummary += filestore.ScanDB(_dbname, tbxDBConnStr.Text);
                            break;

                        case "dgpshard":

                            scanSummary += fileshard.ScanDB(_dbname, tbxDBConnStr.Text);
                            break;
                    }

                    string htmlResult = HTMLStart() + "<div class=\"titlediv\">" + schemaname.ToUpper() + " Scan Results</div>";
                    htmlResult += "<div class=\"innerdiv\">";
                    htmlResult += scanSummary;
                    htmlResult += "</div><p>&nbsp;</p>" + HTMLEnd();
                    Results results = new Results(htmlResult);
                    results.Show();

                }
                catch (Exception ex)
                {
                    SvrErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "SetupMain.btnBuildTables_Click", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
                    MessageBox.Show(ex.Message, "Exception");
                }

                this.UseWaitCursor = false;
            }
        }

        private void btnHelpFile_Click(object sender, EventArgs e)
        {
            DBSetupHelp dbSetupHelp = new DBSetupHelp();
            dbSetupHelp.Show();
        }

        public string HTMLStart()
        {
            return "<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">" +
                    "<head><meta charset=\"utf-8\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />" +
                    "<style type=\"text/css\">" +
                    "body {margin: 0; padding: 0; background-color: #404040; font-family:Verdana; font-size:x-small; color:lightgray;}" +
                    ".titlediv {color:cornflowerblue; margin: 30px 30px 10px 30px; font-size: 110%;}" +
                    ".innerdiv {margin: 10px 30px 10px 30px; font-size: 90%;}" +
                    ".innerdiv p {margin: 10px 10px 10px 10px; font-size: xsmall;}" +
                    ".success {color: cadetblue;}" +
                    ".error {color: sienna;}" +
                    "table, th, td {border: 1px solid black; padding: 5px 10px 5px 10px;}" +
                    "table {border-collapse: collapse; width: 100 %;}" +
                    "td {height: 20px;}" +
                    "tr {background-color: #424242; color: lightgray;}" +
                    "th {background-color: dimgray; color: black;}" +
                    "</style>" +
                    "<title>Results</title></head><body>";
        }

        public string HTMLEnd()
        {
            return "</body></html>";
        }
    }
}
