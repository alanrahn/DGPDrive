using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;

using CliUtil;

namespace DGPDrive.Screens.Security
{
    public partial class frmChangePassword : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        string _username;
        bool isValid = false;

        public frmChangePassword(HttpClient httpClient, MainForm mainForm, string userName)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _username = userName;
                tbxUserName.Text = _username;
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmChangePassword", ex);
                MessageBox.Show(ex.Message, "frmChangePassword", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxNewPassword1.Text = "";
            tbxNewPassword2.Text = "";
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1.  newpassword1 must be the same as newpassword2
                if (tbxNewPassword1.Text != tbxNewPassword2.Text) MessageBox.Show("The new password and the verification must match.", "New Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 2.  newpassword2 must meet all of the password rules
                CmnUtil cmnUtil = new CmnUtil();
                string pwordcheck = cmnUtil.PasswordCheck(tbxNewPassword2.Text);

                if (pwordcheck == APIResult.OK)
                {
                    // 4.  if all rules passed, new password is valid
                    isValid = true;
                }
                else
                {
                    MessageBox.Show(pwordcheck, "New Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Dictionary<string, string> methparams = new Dictionary<string, string>();

                if (isValid)
                {
                    methparams.Add(APIUserFields.UserName, _username);
                    methparams.Add(APIUserFields.Password, tbxNewPassword2.Text);

                    // update the user's password
                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("APIUser.ChangePassword.base", methparams);
                    string respmsg = msgUtil.ApiMethHelper(methList,
                                                                reqID,
                                                                _main.UserName,
                                                                _main.Password,
                                                                _httpClient,
                                                                _main.SvcUrl);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult("APIUser.ChangePassword.base_DEFAULT", methresults);

                        if (methresult.RCode.ToUpper() == APIResult.OK)
                        {
                            // if successful, close the dialog
                            this.Close();
                        }
                        else
                        {
                            // error saving user info: display error message
                            MessageBox.Show(methresult.RVal, methresult.RCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("You must provide values for all required fields", "Required Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmChangePassword.btnSave_Click", ex);
                MessageBox.Show(ex.Message, "frmChangePassword.btnSave_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
