using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;

using CliUtil;

namespace DGPDrive.Screens.Security
{
    public partial class frmEditGroup : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        ucDataGroup _parent;
        string _groupgid;
        string _rowdate;

        public frmEditGroup(HttpClient httpClient, MainForm mainForm, ucDataGroup parentForm, string groupGID)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _parent = parentForm;
                _groupgid = groupGID;

                DisableControls();

                if (_groupgid == null || _groupgid == "")
                {
                    // new group (empty form)
                    this.Text = "New Group";
                    tbxGroupName.Enabled = true;
                    btnDelete.Enabled = false;
                }
                else
                {
                    // update of existing group (description only)
                    this.Text = "Edit Group";
                    btnDelete.Enabled = true;
                    Populate();
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmEditGroup", ex);
                MessageBox.Show(ex.Message, "frmEditGroup", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                methparams.Add("SchemaFlag", "true");
                methparams.Add("GroupGID", _groupgid);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("DataGroup.GetByID.base", methparams);
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

                    ResInfo methresult = msgUtil.GetResult("DataGroup.GetByID.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                        if (methtable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in methtable.Rows)
                            {
                                tbxGlobalID.Text = dr["rec_gid"].ToString();
                                tbxGroupName.Text = dr["GroupName"].ToString();
                                tbxDescription.Text = dr["GroupDescrip"].ToString();
                                _rowdate = dr[CommonFields.row_ms].ToString();
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmEditGroup.Populate", ex);
                MessageBox.Show(ex.Message, "frmEditGroup.Populate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    apimethcall = "DataGroup.New.base";
                    isValid = CheckFields("NEW");
                }
                else
                {
                    apimethcall = "DataGroup.Save.base";
                    methparams.Add("GroupGID", tbxGlobalID.Text);
                    methparams.Add(CommonFields.row_ms, _rowdate);
                    isValid = CheckFields("UPDATE");
                }

                if (isValid)
                {
                    methparams.Add("GroupName", tbxGroupName.Text);
                    methparams.Add("GroupDescrip", tbxDescription.Text);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod(apimethcall, methparams);
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmEditGroup.btnSave_Click", ex);
                MessageBox.Show(ex.Message, "frmEditGroup.btnSave_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    methparams.Add("GroupGID", tbxGlobalID.Text);
                    methparams.Add("GroupName", tbxGroupName.Text);
                    methparams.Add("GroupDescrip", tbxDescription.Text);
                    methparams.Add(CommonFields.row_ms, _rowdate);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("DataGroup.Delete.base", methparams);
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

                        ResInfo methresult = msgUtil.GetResult("DataGroup.Delete.base_DEFAULT", methresults);

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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP Lattice", "frmEditGroup.btnDelete_Click", ex);
                MessageBox.Show(ex.Message, "frmEditGroup:btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            tbxGroupName.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool CheckFields(string action)
        {
            bool fieldsOK = true;

            if (action == "SAVE")
            {
                if (tbxGlobalID.Text == null || tbxGlobalID.Text == "") fieldsOK = false;
            }

            if (tbxGroupName.Text == null || tbxGroupName.Text == "") fieldsOK = false;
            if (tbxDescription.Text == null || tbxDescription.Text == "") fieldsOK = false;

            return fieldsOK;
        }
    }
}
