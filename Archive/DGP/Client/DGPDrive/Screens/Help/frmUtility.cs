using System;
using System.Diagnostics;
using System.Windows.Forms;

using CliUtil;

namespace DGPDrive.Screens.Help
{
    public partial class frmUtility : Form
    {
        public frmUtility()
        {
            InitializeComponent();
        }

        private void btnConvertUnixtime_Click(object sender, EventArgs e)
        {
            long unixtimeMS = Convert.ToInt64(tbxSelUnixTime.Text);
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeMilliseconds(unixtimeMS);
            tbxDateString.Text = dto.ToString();
        }

        private void btnConvertDate_Click(object sender, EventArgs e)
        {
            DateTimeOffset dto = new DateTimeOffset(dtpSelectDate.Value);
            tbxUnixTime.Text = dto.ToUnixTimeMilliseconds().ToString();
        }

        private void btnCreateEventSource_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxEventSourceName.Text != null && tbxEventSourceName.Text != "")
                {
                    if (EventLog.SourceExists(tbxEventSourceName.Text))
                    {
                        MessageBox.Show("The Event Source " + tbxEventSourceName.Text + " already exists.");
                    }
                    else
                    {
                        // create new event log and source (using the same name)
                        EventLog.CreateEventSource(tbxEventSourceName.Text, tbxEventSourceName.Text);
                        MessageBox.Show("The Event Source " + tbxEventSourceName.Text + " was created.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Create Event Log Error");
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            tbxFinishText.Text = "";

            if (tbxStartText.Text != null && tbxStartText.Text != "")
            {
                if (tbxEncryptKey.Text != null && tbxEncryptKey.Text != "")
                {
                    EncryptUtil encryptUtil = new EncryptUtil();
                    tbxFinishText.Text = encryptUtil.EncryptString(tbxStartText.Text, tbxEncryptKey.Text);
                }
                else
                {
                    MessageBox.Show("You must provide an encryption key", "Encryption Error");
                }
            }
            else
            {
                MessageBox.Show("You must provide text to be encrypted or decrypted", "Encryption Error");
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            tbxFinishText.Text = "";

            if (tbxStartText.Text != null && tbxStartText.Text != "")
            {
                if (tbxEncryptKey.Text != null && tbxEncryptKey.Text != "")
                {
                    EncryptUtil encryptUtil = new EncryptUtil();
                    tbxFinishText.Text = encryptUtil.DecryptString(tbxStartText.Text, tbxEncryptKey.Text);
                }
                else
                {
                    MessageBox.Show("You must provide an encryption key", "Encryption Error");
                }
            }
            else
            {
                MessageBox.Show("You must provide text to be encrypted or decrypted", "Encryption Error");
            }
        }
    }
}
