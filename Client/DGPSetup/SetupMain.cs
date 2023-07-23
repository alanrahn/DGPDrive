using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using ApiUtil;
using SysInfoDB;
using SysWorkDB;
using SysMetricsDB;
using FileStoreDB;

namespace DGPSetup
{
    public partial class SetupMain : Form
    {
        string _dbname = "";

        public SetupMain()
        {
            InitializeComponent();
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

            DBSchemaClear();

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

        private void DBSchemaClear()
        {
            tbxDGPAdminUser.Text = "";
            tbxDGPAdminPword.Text = "";
            tbxSvcKey.Text = "";
            tbxKeyVersion.Text = "";

            grpbxSysInfo.Enabled = false;
        }

        private void BuildClear()
        {
            lblSelectedConnStr.Enabled = false;
            tbxDBConnStr.Text = "";
            btnBuildTables.Enabled = false;
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
                RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "SetupMain.btnHostConnect_Click", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
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

            // enable the Schema group box
            DBSchemaClear();
            lblSelectSchema.Enabled = true;
            cbxSchemas.SelectedIndex = -1;
            cbxSchemas.Enabled = true;
            grpbxSysInfo.Enabled = false;

            BuildClear();
        }

        private void cbxSchemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildClear();

            if (cbxSchemas.Text.ToLower() == "dgpsysinfo")
            {
                grpbxSysInfo.Enabled = true;
            }
            else
            {
                grpbxSysInfo.Enabled = false;
                lblSelectedConnStr.Enabled = true;
                btnBuildTables.Enabled = true;
            }

            tbxDBConnStr.Text = BuildDBConnStr(tbxHostName.Text, _dbname, tbxDBAdminUser.Text, tbxDBAdminPword.Text);
        }

        private void btnNewKey_Click(object sender, EventArgs e)
        {
            CmnUtil cmnUtil = new CmnUtil();
            tbxSvcKey.Text = cmnUtil.GetNewGID();
        }

        private void tbxKeyVersion_TextChanged(object sender, EventArgs e)
        {
            CheckSysInfo();
        }

        private void CheckSysInfo()
        {
            bool sysInfoOK = true;

            if (tbxDGPAdminUser.Text == null || tbxDGPAdminUser.Text == "") sysInfoOK = false;
            if (tbxDGPAdminPword.Text == null || tbxDGPAdminPword.Text == "") sysInfoOK = false;
            if (tbxSvcKey.Text == null || tbxSvcKey.Text == "") sysInfoOK = false;
            if (tbxKeyVersion.Text == null || tbxKeyVersion.Text == "") sysInfoOK = false;

            if (sysInfoOK)
            {
                lblSelectedConnStr.Enabled = true;
                btnBuildTables.Enabled = true;
            }
        }

        private void btnBuildTables_Click(object sender, EventArgs e)
        {
            string buildSummary = "";

            Cursor std = Cursor.Current;
            try
            {
                string schemaname = cbxSchemas.Text;

                switch (schemaname.ToLower())
                {
                    case "dgpsysinfo":
                        SysInfoSchema sysInfo = new SysInfoSchema();
                        sysInfo.AdminUser = tbxDGPAdminUser.Text;
                        sysInfo.AdminPassword = tbxDGPAdminPword.Text;
                        sysInfo.SvcKey = tbxSvcKey.Text;
                        sysInfo.SvcKeyVer = tbxKeyVersion.Text;
                        buildSummary += sysInfo.ScanDB(_dbname, tbxDBConnStr.Text);
                        break;

                    case "dgpsysmetrics":
                        SysMetricsSchema sysMetricsSchema = new SysMetricsSchema();
                        buildSummary += sysMetricsSchema.ScanDB(_dbname, tbxDBConnStr.Text);
                        break;

                    case "dgpsyswork":
                        SysWorkSchema syswork = new SysWorkSchema();
                        buildSummary += syswork.ScanDB(_dbname, tbxDBConnStr.Text);
                        break;

                    case "dgpfilestore":
                        FileStoreSchema filestore = new FileStoreSchema();
                        buildSummary += filestore.ScanDB(_dbname, tbxDBConnStr.Text);
                        break;

                    case "dgpfileshard":
                        FileShardSchema fileshard = new FileShardSchema();
                        buildSummary += fileshard.ScanDB(_dbname, tbxDBConnStr.Text);
                        break;
                }

                ResultHTML resultHTML = new ResultHTML();
                string htmlResult = resultHTML.HTMLStart() + "<div class=\"titlediv\">" + schemaname.ToUpper() + " Scan Results</div>";
                htmlResult += "<div class=\"innerdiv\">";
                htmlResult += buildSummary;
                htmlResult += "</div><p>&nbsp;</p>" + resultHTML.HTMLEnd();
                Results results = new Results(htmlResult);
                results.Show();
            }
            catch (Exception ex)
            {
                RemoteErrLog.WriteErrToEV("SYSTEM ACCOUNT", Environment.MachineName, "DGPSetup", "SetupMain.btnBuildTables_Click", "CLIENT", "EXCEPTION", ex.Message, ex.StackTrace);
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnHelpFile_Click(object sender, EventArgs e)
        {
            DBSetupHelp dbSetupHelp = new DBSetupHelp();
            dbSetupHelp.Show();
        }


    }
}
