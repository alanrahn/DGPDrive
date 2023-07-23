﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.Security
{
    public partial class frmMethodHist : Form
    {
        MainForm _main;
        ucApiMethods _parent;
        string _methgid;

        public frmMethodHist(MainForm mainForm, ucApiMethods parentForm, string methGID, string apiName, string methName)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
                _parent = parentForm;
                _methgid = methGID;

                lblMethName.Text = apiName + "." + methName;
                // lookup method data using global id value
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(CommonFields.SchemaFlag, "true");
                methparams.Add(APIMethodFields.MethodGID, _methgid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("APIMethod.GetHistory.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("APIMethod.GetHistory.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                        if (methtable.Rows.Count > 0)
                        {
                            dgvMethodHistory.DataSource = methtable;
                            if (_parent.SearchState == RecState.Active) btnRecoverSelected.Enabled = true;
                        }
                        else
                        {
                            dgvMethodHistory.DataSource = null;
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmMethodHist", ex);
                MessageBox.Show(ex.Message, "frmMethodHist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecoverSelected_Click(object sender, EventArgs e)
        {
            // read values from selected row in grid
            string recgid = dgvMethodHistory.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
            string rowid = dgvMethodHistory.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();

            // call process method to recover edited record
            Dictionary<string, string> methparams = new Dictionary<string, string>();
            methparams.Add("Action", ReplicaAction.Update);
            methparams.Add(CommonFields.rec_gid, recgid);
            methparams.Add(CommonFields.row_id, rowid);

            MsgUtil msgUtil = new MsgUtil();
            string reqID = msgUtil.GetNewGID();
            string methList = msgUtil.CreateXMLMethod("APIMethod.Recover.base", methparams);
            SOAPMsgUtil soapmsg = new SOAPMsgUtil();
            string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

            if (respmsg != "")
            {
                RespInfo respinfo = new RespInfo();
                Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                ResInfo methresult = msgUtil.GetResult("APIMethod.Recover.base_DEFAULT", methresults);

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
