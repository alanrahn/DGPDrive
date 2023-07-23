using System;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;

using CliUtil;

namespace DGPDrive.Screens.FileStore
{
    public partial class frmTagHist : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        ucTags _parent;
        string _taggid;

        public frmTagHist(HttpClient httpClient, MainForm mainForm, ucTags parentForm, string tagGID, string tagName)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _parent = parentForm;
                _taggid = tagGID;

                lblTagName.Text = tagName;
                // lookup tag data using global id value
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);
                methparams.Add(TagFields.TagGID, _taggid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("Tag.GetHistory.base", methparams);
                string respmsg = msgUtil.ApiMethHelper(methList,
                                                        reqID,
                                                        _main.UserName,
                                                        _main.Password,
                                                        httpClient,
                                                        _main.SvcUrl);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("Tag.GetHistory.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                        if (methtable.Rows.Count > 0)
                        {
                            dgvTagHistory.DataSource = methtable;
                            if (_parent.SearchState == RecState.Active) btnRecoverSelected.Enabled = true;
                        }
                        else
                        {
                            dgvTagHistory.DataSource = null;
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmTagHist", ex);
                MessageBox.Show(ex.Message, "frmTagHist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecoverSelected_Click(object sender, EventArgs e)
        {
            // read values from selected row in grid
            string recgid = dgvTagHistory.SelectedRows[0].Cells[CommonFields.rec_gid].Value.ToString();
            string rowid = dgvTagHistory.SelectedRows[0].Cells[CommonFields.row_id].Value.ToString();

            // call process method to recover edited record
            Dictionary<string, string> methparams = new Dictionary<string, string>();
            methparams.Add("Action", ReplicaAction.Update);
            methparams.Add(CommonFields.rec_gid, recgid);
            methparams.Add(CommonFields.row_id, rowid);

            MsgUtil msgUtil = new MsgUtil();
            string reqID = msgUtil.GetNewGID();
            string methList = msgUtil.CreateXMLMethod("Tag.Recover.base", methparams);
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

                ResInfo methresult = msgUtil.GetResult("Tag.Recover.base_DEFAULT", methresults);

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
