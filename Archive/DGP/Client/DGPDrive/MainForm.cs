using System;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

using CliUtil;
using CmnUtil;

namespace DGPDrive
{
    public partial class MainForm : Form
    {
        // from syslist file
        public string SvcUrl { get; set; }
        public bool Encrypted { get; set; }
        public bool Connected { get; set; }

        // stored from UI forms
        public string UserName { get; set; }
        public string Password { get; set; }

        // returned from web service login method
        public string WebSvcName { get; set; }
        public DateTime WebSvcVer { get; set; }
        public string WebSvcVerStr { get; set; }
        public string MaxFileSize { get; set; }
        public string MaxSegSize { get; set; }
        public string MinPwordLen { get; set; }
        public string AcctAuth { get; set; }
        public string UserGID { get; set; }
        public string RoleList { get; set; }
        public string ReadList { get; set; }
        public string WriteList { get; set; }
        public string DGPDriveRole { get; set; }

        // from local app.config file to find local resource files
        public string SysListDir { get; set; }

        public bool RemoteMonitor { get; set; }

        // reused objects
        HttpClient _httpClient;

        public frmConnect _connect;

        ucFileStore _ucFileStore;
        ucTags _ucTags;

        ucApiMethods _ucApiMethods;
        ucApiRoles _ucApiRoles;
        ucApiUsers _ucApiUsers;
        ucDataGroup _ucDataGroup;

        ucApiTestHarness _ucApiTestHarness;

        ucReplicaWork _ucReplicaWork;
        ucGeneralWork _ucGeneralWork;

        public MainForm()
        {
            InitializeComponent();

            _httpClient = new HttpClient();
            RemoteMonitor = false;
            SysListDir = Environment.CurrentDirectory;
        }

        public void ClearAllValues()
        {
            SvcUrl = "";
            WebSvcName = "";
            WebSvcVer = new DateTime(2000, 1, 1);
            WebSvcVerStr = "";
            MaxFileSize = "";
            MaxSegSize = "";
            MinPwordLen = "";
            AcctAuth = "";
            UserName = "";
            Password = "";
            UserGID = "";
            RoleList = "";
            ReadList = "";
            WriteList = "";
            DGPDriveRole = "";
            Connected = false;
            Encrypted = false;
        }

        public void SetMenu()
        {
            HideMenu();

            if (AcctAuth == AuthState.Expired)
            {
                // only enable change password UI
                userMenuItem.Visible = true;
                editProfileMenuItem.Visible = false;
            }
            else if (AcctAuth == AuthState.OK)
            {
                userMenuItem.Visible = true;

                if (RoleList != null && RoleList != "")
                {
                    userMenuItem.Visible = true;

                    string[] Roles = RoleList.Split(',');

                    foreach (string role in Roles)
                    {
                        switch (role)
                        {
                            case "WorkAdmin":
                                DateTime minconfig = new DateTime(2019, 1, 1);
                                if (WebSvcVer >= minconfig) configurationMenuItem.Visible = true;
                                break;

                            case "SecurityAdmin":
                                DateTime minsecurity = new DateTime(2019, 1, 1);
                                if (WebSvcVer >= minsecurity) securityMenuItem.Visible = true;
                                break;

                            case "Testing":
                                DateTime mintesting = new DateTime(2019, 1, 1);
                                if (WebSvcVer >= mintesting) testingMenuItem.Visible = true;
                                break;

                            case "FileStoreAdmin":
                            case "FileStoreUser":
                                LatticeRole = role;
                                DateTime minlattice = new DateTime(2019, 1, 1);
                                if (WebSvcVer >= minlattice) storageMenuItem.Visible = true;
                                break;

                            case "RemoteMetrics":
                                RemoteMonitor = true;
                                break;
                        }
                    }
                }
            }
        }

        public void SetMetrics(int methCount, double clientMS, double serverMS, string appName, string formName)
        {
            tslblMethCount.Text = methCount.ToString();
            double networkMS = clientMS - serverMS;
            tslblLatency.Text = Math.Round(clientMS, 2).ToString();
            tslblServer.Text = Math.Round(serverMS, 2).ToString();

            Application.DoEvents();

            if (RemoteMonitor && methCount != 0)
            {
                // save metrics data to the sysmetrics database
                try
                {
                    // lookup method data using global id value
                    Dictionary<string, string> methparams = new Dictionary<string, string>();
                    methparams.Add("UserName", UserName);
                    methparams.Add("CompName", Environment.MachineName);
                    methparams.Add("AppName", appName);
                    methparams.Add("FormName", formName);
                    methparams.Add("WebSvcName", WebSvcName);
                    methparams.Add("WebSvcVer", WebSvcVerStr);
                    methparams.Add("ClientTime", DateTime.UtcNow.ToString());
                    methparams.Add("MethodCount", methCount.ToString());
                    methparams.Add("EndToEndMS", clientMS.ToString());
                    methparams.Add("NetworkMS", networkMS.ToString());
                    methparams.Add("ServerMS", serverMS.ToString());

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methlist = msgUtil.CreateXMLMethod("LatticeMetrics.New.base", methparams);
                    string respmsg = msgUtil.ApiMethHelper(methlist,
                                                                    reqID,
                                                                    UserName,
                                                                    Password,
                                                                    _httpClient,
                                                                    SvcUrl);

                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("LatticeMetrics.New.base_DEFAULT", methresults);

                    if (methresult == null || methresult.RCode != APIResult.OK)
                    {
                        RemoteErrLog.LogError(UserName, Password, SvcUrl, "DGP Lattice", "MainForm.SetMetrics", "Save Lattice metrics error.", methresult.RCode + " : " + methresult.RVal);
                    }
                }
                catch (Exception ex)
                {
                    RemoteErrLog.LogException(UserName, Password, SvcUrl, "DGP Lattice", "MainForm.SetMetrics", ex);
                }
            }
        }

        public void ClearControls()
        {
            // Home and Login controls should not be cleared
            _ucApiMethods = null;
            _ucApiRoles = null;
            _ucApiTestHarness = null;
            _ucApiUsers = null;
            _ucDataGroup = null;
            _ucFileStore = null;
        }

        // ************************************************************************************* //
        // ************************************************************************************* //
        // ************************************************************************************* //

        private void HideMenu()
        {
            userMenuItem.Visible = false;
            configurationMenuItem.Visible = false;
            securityMenuItem.Visible = false;
            testingMenuItem.Visible = false;
            storageMenuItem.Visible = false;
        }

        private void HideControls()
        {
            if (_ucFileStore != null) _ucFileStore.Hide();
            if (_ucTags != null) _ucTags.Hide();

            if (_ucApiMethods != null) _ucApiMethods.Hide();
            if (_ucApiRoles != null) _ucApiRoles.Hide();
            if (_ucApiUsers != null) _ucApiUsers.Hide();
            if (_ucDataGroup != null) _ucDataGroup.Hide();

            if (_ucApiTestHarness != null) _ucApiTestHarness.Hide();

        }

        // ************************************************************************************* //

        private void connectMenuItem_Click(object sender, EventArgs e)
        {
            if (_connect == null)
            {
                _connect = new frmConnect(_httpClient, this);
            }

            HideControls();
            _connect.ShowDialog();
            _connect.BringToFront();
        }

        // ************************************************************************************* //

        private void filestoreMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucFileStore == null)
            {
                _ucFileStore = new ucFileStore(this, _httpClient);
                this.Controls.Add(_ucFileStore);
                _ucFileStore.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucFileStore.Show();
            _ucFileStore.BringToFront();
        }

        private void tagsMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucTags == null)
            {
                _ucTags = new ucTags(this, _httpClient);
                this.Controls.Add(_ucTags);
                _ucTags.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucTags.Show();
            _ucTags.BringToFront();
        }

        // ************************************************************************************* //

        private void changePasswordMenuItem_Click(object sender, EventArgs e)
        {
            Screens.User.frmChangePassword userChangePassword = new Screens.User.frmChangePassword(_httpClient, this);
            userChangePassword.ShowDialog();
        }

        private void editProfileMenuItem_Click(object sender, EventArgs e)
        {
            frmEditSelf editSelf = new frmEditSelf(_httpClient, this);
            editSelf.ShowDialog();
        }

        // ************************************************************************************* //

        private void replicaScheduleMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucReplicaWork == null)
            {
                _ucReplicaWork = new ucReplicaWork(this, _httpClient);
                this.Controls.Add(_ucReplicaWork);
                _ucReplicaWork.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucReplicaWork.Show();
            _ucReplicaWork.BringToFront();
        }

        private void genWorkScheduleMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucGeneralWork == null)
            {
                _ucGeneralWork = new ucGeneralWork(this, _httpClient);
                this.Controls.Add(_ucGeneralWork);
                _ucGeneralWork.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucGeneralWork.Show();
            _ucGeneralWork.BringToFront();
        }

        // ************************************************************************************* //

        private void methodsMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucApiMethods == null)
            {
                _ucApiMethods = new ucApiMethods(this, _httpClient);
                this.Controls.Add(_ucApiMethods);
                _ucApiMethods.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucApiMethods.Show();
            _ucApiMethods.BringToFront();
        }

        private void rolesMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucApiRoles == null)
            {
                _ucApiRoles = new ucApiRoles(this, _httpClient);
                this.Controls.Add(_ucApiRoles);
                _ucApiRoles.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucApiRoles.Show();
            _ucApiRoles.BringToFront();
        }

        private void usersMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucApiUsers == null)
            {
                _ucApiUsers = new ucApiUsers(this, _httpClient);
                this.Controls.Add(_ucApiUsers);
                _ucApiUsers.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucApiUsers.Show();
            _ucApiUsers.BringToFront();
        }

        private void dataGroupsMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucDataGroup == null)
            {
                _ucDataGroup = new ucDataGroup(this, _httpClient);
                this.Controls.Add(_ucDataGroup);
                _ucDataGroup.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucDataGroup.Show();
            _ucDataGroup.BringToFront();
        }

        // ************************************************************************************* //

        private void testHarnessMenuItem_Click(object sender, EventArgs e)
        {
            if (_ucApiTestHarness == null)
            {
                _ucApiTestHarness = new ucApiTestHarness(this, _httpClient);
                this.Controls.Add(_ucApiTestHarness);
                _ucApiTestHarness.Dock = DockStyle.Fill;
            }

            HideControls();
            _ucApiTestHarness.Show();
            _ucApiTestHarness.BringToFront();
        }

        private void utilityMenuItem_Click(object sender, EventArgs e)
        {
            frmUtility frmUtility = new frmUtility();
            frmUtility.ShowDialog();
        }

        private void testLoggingMenuItem_Click(object sender, EventArgs e)
        {
            RemoteErrLog.LogError(UserName, Password, SvcUrl, "DGP Lattice", "MainForm", "Remote Error test", "Test of remote error logging.  Check the Event Viewer and the DGPErrors DB table to confirm the test results.");
            MessageBox.Show("Check the local Event Viewer and the DGPErrors DB table to confirm the test results.", "Remote Error Log Test", MessageBoxButtons.OK);
        }

        private void errorLogMenuItem_Click(object sender, EventArgs e)
        {
            frmDGPErrorLog dgpErrors = new frmDGPErrorLog(_httpClient, this);
            dgpErrors.Show();
        }

        private void autoWorkLogMenuItem_Click(object sender, EventArgs e)
        {
            frmAutoWorkLog autoworkLog = new frmAutoWorkLog(_httpClient, this);
            autoworkLog.Show();
        }

        private void latticeMetricsMenuItem_Click(object sender, EventArgs e)
        {
            frmLatticeMetrics latticeMetrics = new frmLatticeMetrics(_httpClient, this);
            latticeMetrics.Show();
        }

        private void testResultsMenuItem_Click(object sender, EventArgs e)
        {
            frmTestResultLog testResultLog = new frmTestResultLog(_httpClient, this);
            testResultLog.Show();
        }

        private void rsaKeysMenuItem_Click(object sender, EventArgs e)
        {
            frmRSAKey frmRSA = new frmRSAKey();
            frmRSA.ShowDialog();
        }
    }
}
