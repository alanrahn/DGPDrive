using System;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.Security
{
    public partial class frmMethodRoles : Form
    {
        MainForm _main;
        ucApiMethods _parent;
        string _methgid;

        public frmMethodRoles(MainForm mainForm, ucApiMethods parentForm, string methGID, string apiName, string methName)
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
                methparams.Add(APIMethodFields.MethodGID, methGID);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("RoleMethod.GetMethodRoles.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("RoleMethod.GetMethodRoles.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                        if (methtable.Rows.Count > 0)
                        {
                            dgvMethodRoles.DataSource = methtable;
                        }
                        else
                        {
                            dgvMethodRoles.DataSource = null;
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmMethodRoles", ex);
                MessageBox.Show(ex.Message, "frmMethodRoles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
