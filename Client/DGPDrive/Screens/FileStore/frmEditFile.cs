using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.FileStore
{
    public partial class frmEditFile : Form
    {
        MainForm _main;
        ucFileStore _parent;

        string _filegid;
        string _filename;
        string _filedescrip;
        string _filems;

        public frmEditFile(MainForm mainForm, ucFileStore parentForm, string fileGID, string fileName, string fileDescrip, string fileMS)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
                _parent = parentForm;

                _filegid = fileGID;
                _filename = fileName;
                _filedescrip = fileDescrip;
                _filems = fileMS;

                EnableControls();

                // update data of selected folder
                tbxGlobalID.Text = _filegid;
                tbxFileName.Text = _filename;
                tbxFileDescrip.Text = _filedescrip;
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditFile", ex);
                MessageBox.Show(ex.Message, "frmEditFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isValid = false;

            try
            {
                isValid = CheckFields();

                if (isValid)
                {
                    Dictionary<string, string> methparams = new Dictionary<string, string>();
                    methparams.Add(CommonFields.rec_gid, _filegid);
                    methparams.Add(FileFields.FileName, tbxFileName.Text);
                    methparams.Add(FileFields.FileDescrip, tbxFileDescrip.Text);
                    methparams.Add(CommonFields.row_ms, _filems);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("File.SaveFileRec.base", methparams);

                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult("File.SaveFileRec.base_DEFAULT", methresults);

                        if (methresult.RCode.ToUpper() == APIResult.OK)
                        {
                            _parent.FileSearch();

                            // close the dialog
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditFile:btnSave_Click", ex);
                MessageBox.Show(ex.Message, "frmEditFile:btnSave_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    Dictionary<string, string> methparams = new Dictionary<string, string>();
                    methparams.Add(CommonFields.rec_gid, _filegid);
                    methparams.Add(FileFields.FileName, tbxFileName.Text);
                    methparams.Add(FileFields.FileDescrip, tbxFileDescrip.Text);
                    methparams.Add(CommonFields.row_ms, _filems);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("File.Delete.base", methparams);

                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult("File.Delete.base_DEFAULT", methresults);

                        if (methresult.RCode.ToUpper() == APIResult.OK)
                        {
                            _parent.FileSearch();

                            // close the dialog
                            this.Close();
                        }
                        else
                        {
                            // error saving method info: display error message
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditFile:btnDelete_Click", ex);
                MessageBox.Show(ex.Message, "frmEditFile:btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // *********************************************************** //
        // *********************************************************** //

        private void EnableControls()
        {
            tbxGlobalID.Enabled = true;
            tbxFileName.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
        }

        private bool CheckFields()
        {
            bool fieldsOK = true;

            if (tbxGlobalID.Text == null || tbxGlobalID.Text == "") fieldsOK = false;
            if (tbxFileName.Text == null || tbxFileName.Text == "") fieldsOK = false;

            return fieldsOK;
        }
    }
}
