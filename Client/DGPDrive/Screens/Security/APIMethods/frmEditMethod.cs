using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.Security
{
    public partial class frmEditMethod : Form
    {
        MainForm _main;
        ucApiMethods _parent;
        string _methgid;
        string _row_ms;

        public frmEditMethod(MainForm mainForm, ucApiMethods parentForm, string methGID)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
                _parent = parentForm;
                _methgid = methGID;

                DisableControls();

                if (_methgid == null || _methgid == "")
                {
                    // new method (empty form)
                    this.Text = "New Method";
                    tbxAPIName.Enabled = true;
                    tbxAPIName.TabStop = true;
                    tbxMethodName.Enabled = true;
                    tbxMethodName.TabStop = true;
                    tbxVersionName.Enabled = true;
                    tbxVersionName.TabStop = true;
                    btnDelete.Enabled = false;
                    btnDelete.TabStop = false;

                }
                else
                {
                    // update of existing method (description only)
                    this.Text = "Edit Method";
                    btnDelete.Enabled = true;
                    btnDelete.TabStop = true;
                    Populate();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditMethod", ex);
                MessageBox.Show(ex.Message, "frmEditMethod", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Populate()
        {
            try
            {
                // lookup method data using global id value
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(CommonFields.SchemaFlag, "true");
                methparams.Add(APIMethodFields.MethodGID, _methgid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("APIMethod.GetByID.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("APIMethod.GetByID.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                        if (methtable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in methtable.Rows)
                            {
                                tbxGlobalID.Text = dr[CommonFields.rec_gid].ToString();
                                tbxAPIName.Text = dr[APIMethodFields.APIName].ToString();
                                tbxMethodName.Text = dr[APIMethodFields.MethodName].ToString();
                                tbxVersionName.Text = dr[APIMethodFields.VersionName].ToString();
                                tbxDescription.Text = dr[APIMethodFields.MethodDescrip].ToString();
                                _row_ms = dr[CommonFields.row_ms].ToString();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditMethod.Populate", ex);
                MessageBox.Show(ex.Message, "frmEditMethod.Populate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string apimethcall = "";
                Dictionary<string, string> methparams = new Dictionary<string, string>();

                if (tbxGlobalID.Text == null || tbxGlobalID.Text == "")
                {
                    // new method, required parameters
                    apimethcall = "APIMethod.New.base";
                    isValid = CheckFields("NEW");
                }
                else
                {
                    apimethcall = "APIMethod.Save.base";
                    methparams.Add(APIMethodFields.MethodGID, tbxGlobalID.Text);
                    methparams.Add(CommonFields.row_ms, _row_ms);
                    isValid = CheckFields("SAVE");
                }

                if (isValid)
                {
                    methparams.Add(APIMethodFields.APIName, tbxAPIName.Text);
                    methparams.Add(APIMethodFields.MethodName, tbxMethodName.Text);
                    methparams.Add(APIMethodFields.VersionName, tbxVersionName.Text);
                    methparams.Add(APIMethodFields.MethodDescrip, tbxDescription.Text);
                    
                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod(apimethcall, methparams);
                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult(apimethcall + "_" + MethReturn.Default, methresults);

                        if (methresult.RCode.ToUpper() == APIResult.OK)
                        {
                            // update the parent form and then close the dialog
                            _parent.Search();
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditMethod.btnSave_Click", ex);
                MessageBox.Show(ex.Message, "EditMethod:btnSave_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckFields("delete"))
                {
                    Dictionary<string, string> methparams = new Dictionary<string, string>();
                    methparams.Add(APIMethodFields.MethodGID, tbxGlobalID.Text);
                    methparams.Add(APIMethodFields.APIName, tbxAPIName.Text);
                    methparams.Add(APIMethodFields.MethodName, tbxMethodName.Text);
                    methparams.Add(APIMethodFields.VersionName, tbxVersionName.Text);
                    methparams.Add(APIMethodFields.MethodDescrip, tbxDescription.Text);
                    methparams.Add(CommonFields.row_ms, _row_ms);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("APIMethod.Delete.base", methparams);
                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo methresult = msgUtil.GetResult("APIMethod.Delete.base_DEFAULT", methresults);

                        if (methresult.RCode.ToUpper() == APIResult.OK)
                        {
                            // update the parent form and then close the dialog
                            _parent.Search();
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmEditMethod.btnDelete_Click", ex);
                MessageBox.Show(ex.Message, "frmEditMethod:btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void DisableControls()
        {
            tbxGlobalID.Enabled = false;
            tbxGlobalID.TabStop = false;
            tbxAPIName.Enabled = false;
            tbxAPIName.TabStop = false;
            tbxMethodName.Enabled = false;
            tbxMethodName.TabStop = false;
            tbxVersionName.Enabled = false;
            tbxVersionName.TabStop = false;
            btnDelete.Enabled = false;
        }

        private bool CheckFields(string action)
        {
            bool fieldsOK = true;

            if (action == "SAVE")
            {
                if (tbxGlobalID.Text == null || tbxGlobalID.Text == "") fieldsOK = false;
            }

            if (tbxAPIName.Text == null || tbxAPIName.Text == "") fieldsOK = false;
            if (tbxMethodName.Text == null || tbxMethodName.Text == "") fieldsOK = false;
            if (tbxVersionName.Text == null || tbxVersionName.Text == "") fieldsOK = false;
            if (tbxDescription.Text == null || tbxDescription.Text == "") fieldsOK = false;

            return fieldsOK;
        }
    }
}
