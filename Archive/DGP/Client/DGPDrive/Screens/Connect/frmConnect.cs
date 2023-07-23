using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Xml;

using CliUtil;
using DGPDrive.Util;

namespace DGPDrive.Screens.Connect
{
    public partial class frmConnect : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        CommonOpenFileDialog locdialog;

        List<DGPSys> _sysList;
        DGPSys _selSys;
        DGPLoc _selLoc;
        DGPEP _selEP;
        string _selController;
        string _selKeyUrl;

        public frmConnect(HttpClient httpClient, MainForm mainForm)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _main.SetMetrics(0, 0, 0, "", "");

                locdialog = new CommonOpenFileDialog();
                locdialog.Title = "Select System List File";
                locdialog.InitialDirectory = _main.SysListDir;
                locdialog.Filters.Add(new CommonFileDialogFilter("System List Files", "*.xml"));
                locdialog.EnsurePathExists = true;
                locdialog.EnsureValidNames = true;
                locdialog.IsFolderPicker = false;
                locdialog.Multiselect = false;

                ClearResults();
            }
            catch (Exception ex)
            {
                RemErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmConnect", ex);
                MessageBox.Show(ex.Message, "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // browse for location list file
            try
            {
                if (locdialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    // if file selected, clear connection form plus main form properties and controls
                    ClearSysList();
                    _main.ClearControls();

                    tbxFilePath.Text = locdialog.FileName;
                    _main.SysListDir = Path.GetDirectoryName(locdialog.FileName);

                    if (File.Exists(tbxFilePath.Text))
                    {
                        SysFileUtil sysFileUtil = new SysFileUtil();
                        string readStatus;
                        _sysList = sysFileUtil.ParseSysList(tbxFilePath.Text, out readStatus);

                        if (readStatus == "OK")
                        {
                            dgvSysList.DataSource = _sysList;
                            lblSelectSystem.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmConnect.btnBrowse_Click", ex);
                MessageBox.Show(ex.Message, "btnBrowse_Click", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ClearSysList()
        {
            tbxFilePath.Text = "";
            dgvSysList.DataSource = null;
            lblSelectSystem.Enabled = false;

            _sysList = null;
            _selSys = null;

            ClearLocations();
        }

        private void ClearLocations()
        {
            tbxEPURL.Text = "";
            dgvLocList.DataSource = null;
            lblSelectLocation.Enabled = false;


            _main.SvcUrl = "";
            _main.WebSvcVer = new DateTime(2000, 1, 1);
            _selLoc = null;

            ClearEndpoints();
        }

        private void ClearEndpoints()
        {
            tbxEPURL.Text = "";
            dgvEPList.DataSource = null;
            lblSelectEndpoint.Enabled = false;
            lblSelectedURL.Enabled = false;

            _main.SvcUrl = "";
            _main.WebSvcVer = new DateTime(2000, 1, 1);
            _selEP = null;
            _selController = "";

            ClearAccountInfo();
        }

        private void ClearAccountInfo()
        {
            tbxUserName.Text = "";
            tbxPassword.Text = "";
            lblConnect.Enabled = false;
            lblResults.Enabled = false;

            _main.ClearAllValues();
            _main.SetMetrics(0, 0, 0, "", "");

            ClearResults();
        }

        private void ClearResults()
        {
            // clear results
            brwsResults.DocumentText = "0";
            brwsResults.Document.OpenNew(true);
            ResultHTML resultHTML = new ResultHTML();
            string resHTML = resultHTML.HTMLStart() + resultHTML.HTMLEnd();
            brwsResults.Document.Write(resHTML);
            brwsResults.Refresh();
        }

        private void tbxPassword_TextChanged(object sender, EventArgs e)
        {
            CheckForm();
        }

        private void tbxLocURL_TextChanged(object sender, EventArgs e)
        {
            CheckForm();
        }

        private void CheckForm()
        {
            bool checkOK = true;

            if (dgvSysList.DataSource == null) checkOK = false;
            if (dgvLocList.DataSource == null) checkOK = false;
            if (tbxUserName.Text == null || tbxUserName.Text == "") checkOK = false;
            if (tbxPassword.Text == null || tbxPassword.Text == "") checkOK = false;

            if (checkOK)
            {
                if (tbxEPURL.Text != null && tbxEPURL.Text != "") btnConnect.Enabled = true;
            }
            else
            {
                btnConnect.Enabled = false;
            }
        }

        private void dgvSysList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearLocations();
            lblSelectLocation.Enabled = true;

            _selSys = (DGPSys)dgvSysList.CurrentRow.DataBoundItem;
            dgvLocList.DataSource = _selSys.LocList;
        }

        private void dgvLocList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearEndpoints();
            lblSelectEndpoint.Enabled = true;

            _selLoc = (DGPLoc)dgvLocList.CurrentRow.DataBoundItem;
            dgvEPList.DataSource = _selLoc.EPList;

            CheckForm();
        }

        private void dgvNetList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _selEP = (DGPEP)dgvEPList.CurrentRow.DataBoundItem;
            _selController = _selEP.URL;

            lblSelectedURL.Enabled = true;
            lblConnect.Enabled = true;
            lblResults.Enabled = true;
            tbxEPURL.Text = _selController;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _main.UserName = "";
            _main.Password = "";
            _main.WebSvcName = "";
            _main.WebSvcVerStr = "";
            _main.WebSvcVer = DateTime.MinValue;
            _main.MaxFileSize = "";
            _main.MaxSegSize = "";
            _main.MinPwordLen = "";
            _main.UserGID = "";
            _main.RoleList = "";
            _main.ReadList = "";
            _main.WriteList = "";

            ClearSysList();
            _main.SetMenu();

            CheckForm();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // call login method of selected web service to establish connection
            Cursor std = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            EncryptUtil encryptUtil = new EncryptUtil();
            ClearResults();
            _main.Connected = false;
            string connResult = "";

            try
            {
                MsgUtil msgUtil = new MsgUtil();

                _main.SvcUrl = tbxEPURL.Text;
                _main.UserName = tbxUserName.Text.ToLower();
                _main.Password = _main.UserName + tbxPassword.Text;
                string reqID = msgUtil.GetNewGID();

                Dictionary<string, string> loginparams = new Dictionary<string, string>();
                loginparams.Add(CommonFields.SchemaFlag, "true");
                string methList = msgUtil.CreateXMLMethod("UserSelf.Login.base", loginparams);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                string respmsg = msgUtil.ApiMethHelper(methList,
                                                        reqID,
                                                        _main.UserName,
                                                        _main.Password,
                                                        _httpClient,
                                                        _main.SvcUrl);
                sw.Stop();

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);
                    _main.SetMetrics(1, sw.Elapsed.TotalMilliseconds, Convert.ToDouble(respinfo.SvrMS), "Lattice", "frmConnect");

                    // set value of main form authorization property
                    _main.AcctAuth = respinfo.Auth;

                    // first check overall authentication
                    if (_main.AcctAuth == AuthState.OK || _main.AcctAuth == AuthState.Expired)
                    {
                        // login method returns multiple results and the values are assigned to main form properties
                        ResInfo apiUser = msgUtil.GetResult("UserSelf.Login.base_DEFAULT", methresults);
                        if (apiUser != null)
                        {
                            if (apiUser.RCode.ToUpper() == APIResult.OK)
                            {
                                XmlDocument doc = new XmlDocument();
                                XmlDocumentFragment docfrag = doc.CreateDocumentFragment();
                                docfrag.InnerXml = apiUser.RVal;

                                if (docfrag.SelectNodes("LoginResults") != null && docfrag.SelectNodes("LoginResults").ToString() != "")
                                {
                                    // read XML values and assign to their respective main form properties
                                    _main.WebSvcName = docfrag.SelectSingleNode("//WebSvcName").InnerText;
                                    _main.WebSvcVerStr = docfrag.SelectSingleNode("//WebSvcVer").InnerText;
                                    _main.WebSvcVer = Convert.ToDateTime(_main.WebSvcVerStr);
                                    _main.MaxFileSize = docfrag.SelectSingleNode("//MaxFileSize").InnerText;
                                    _main.MaxSegSize = docfrag.SelectSingleNode("//MaxSegSize").InnerText;
                                    _main.MinPwordLen = docfrag.SelectSingleNode("//MinPwordLen").InnerText;
                                    _main.UserGID = docfrag.SelectSingleNode("//UserGID").InnerText;
                                    _main.RoleList = docfrag.SelectSingleNode("//UserRoles").InnerText;
                                    _main.ReadList = docfrag.SelectSingleNode("//ReadGroups").InnerText;
                                    _main.WriteList = docfrag.SelectSingleNode("//WriteGroups").InnerText;
                                    string resptime = docfrag.SelectSingleNode("//RespTime").InnerText;
                                    string resptoken = docfrag.SelectSingleNode("//RespToken").InnerText;

                                    connResult += "<p class=\"success\">All LoginResult values read correctly.</p>";

                                    if (resptime != null && resptime != "" && resptoken != null && resptoken != "")
                                    {
                                        bool serverAuth = encryptUtil.ValidateHMACHash(_main.Password, resptime, resptoken);
                                        if (serverAuth)
                                        {
                                            connResult += "<p class=\"success\">Server Authentication: " + serverAuth.ToString() + "</p>";
                                            _main.Connected = true;
                                            lblResults.Enabled = true;
                                            if (ckbxAutoClose.Checked) this.Hide();
                                        }
                                        else
                                        {
                                            connResult += "<p class=\"error\">Server Authentication: " + serverAuth.ToString() + "</p>";
                                        }
                                    }
                                    else
                                    {
                                        connResult += "<p class=\"error\">RespTime and/or RespToken missing - Server authentication failed.</p>";
                                    }
                                }
                                else
                                {
                                    connResult += "<p class=\"error\">LoginResult XML Error: LoginResult XML was not returned by the Login method.";
                                }
                            }
                            else
                            {
                                connResult += "<p class=\"error\">ApiUser Error: " + apiUser.RCode + " : " + apiUser.RVal + "</p>";
                            }
                        }
                        else
                        {
                            connResult += "<p class=\"error\">No result found for UserSelf.Login.base_DEFAULT</p>";
                        }
                    }
                    else
                    {
                        connResult += "<p class=\"error\">API Request Error: " + respinfo.Auth + " - " + respinfo.Info + "</p>";
                    }
                }
                else
                {
                    connResult += "<p class=\"error\">Respons Message Error: No response message was returned.</p>";
                }
        }
            catch (Exception ex)
            {
                connResult += "<p class=\"error\">API Request Exception: " + ex.Message + "</p>";
            }

            _main.SetMenu();

            ResultHTML resultHTML = new ResultHTML();
            string htmlResult = resultHTML.HTMLStart() + "<div class=\"titlediv\">Connection:</div><div class=\"innerdiv\">";
            htmlResult += connResult;
            htmlResult += "</div>" + resultHTML.HTMLEnd();

            brwsResults.DocumentText = "0";
            brwsResults.Document.OpenNew(true);
            brwsResults.Document.Write(htmlResult);
            brwsResults.Refresh();

            Cursor.Current = std;
        }

    }
}
