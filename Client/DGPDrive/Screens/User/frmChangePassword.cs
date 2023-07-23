using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.User
{
    public partial class frmChangePassword : Form
    {
        MainForm _main;
        bool isValid = false;

        public frmChangePassword(MainForm mainForm)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmChangePassword", ex);
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
                // 1.  New password cannot be the same as the old password
                if (tbxNewPassword2.Text == _main.Password) MessageBox.Show("The new password cannot be the same as the old password.", "New Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 2.  newpassword1 must be the same as newpassword2
                if (tbxNewPassword1.Text != tbxNewPassword2.Text) MessageBox.Show("The new password and the verification must match.", "New Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 3.  newpassword2 must meet all of the password rules
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
                    methparams.Add(APIUserFields.Password, tbxNewPassword2.Text);

                    // update the user's password
                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("UserSelf.ChangePassword.base", methparams);
                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult("UserSelf.ChangePassword.base_DEFAULT", methresults);

                        if (methresult.RCode.ToUpper() == APIResult.OK)
                        {
                            // if successful, also update the main form credentials
                            _main.Password = tbxNewPassword2.Text;
                            _main._connect.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            // error saving user info: display error message
                            MessageBox.Show(methresult.RVal, methresult.RCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No response message was returned", "Respons Message Error");
                    }
                }
                else
                {
                    MessageBox.Show("You must provide values for all required fields", "Required Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmChangePassword.btnSave_Click", ex);
                MessageBox.Show(ex.Message, "frmChangePassword-User.btnSave_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
