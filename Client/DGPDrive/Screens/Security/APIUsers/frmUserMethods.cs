using System;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.Security
{
    public partial class frmUserMethods : Form
    {
        MainForm _main;
        ucApiUsers _parent;

        public frmUserMethods(MainForm mainForm, ucApiUsers parentForm, string userGID, string userName)
        {
            InitializeComponent();

            try
            {
                _main = mainForm;
                _parent = parentForm;

                lblUserName.Text = userName;
                // lookup method data using global id value
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add("SchemaFlag", "true");
                methparams.Add("UserGID", userGID);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("RoleMethod.GetUserMethods.base", methparams);
                SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("RoleMethod.GetUserMethods.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK && methresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable methtable = cmnUtil.XmlToTable(methresult.RVal);
                        if (methtable.Rows.Count > 0)
                        {
                            dgvUserMethods.DataSource = methtable;
                        }
                        else
                        {
                            dgvUserMethods.DataSource = null;
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
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmUserMethods", ex);
                MessageBox.Show(ex.Message, "frmUserMethods", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
