using System;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DGPDrive.Screens.Help
{
    public partial class frmRSAKey : Form
    {
        public frmRSAKey()
        {
            InitializeComponent();
        }

        /// <summary>
        /// NIST specifies that the minimum RSA key length of 2048 bits is acceptable until 2030
        /// </summary>
        private void btnCreateKeys_Click(object sender, EventArgs e)
        {
            tbxPublicKey.Text = "";
            tbxKeyPair.Text = "";

            int keySize = 2048;
            if (rbtn4096.Checked) keySize = 4096;
            
            //create RSA provider
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(keySize);

            if (ckbXmlEncode.Checked)
            {
                // set key values encoded for use in .config files
                tbxPublicKey.Text = SecurityElement.Escape(RSA.ToXmlString(false));
                tbxKeyPair.Text = SecurityElement.Escape(RSA.ToXmlString(true));
            }
            else
            {
                tbxPublicKey.Text = RSA.ToXmlString(false);
                tbxKeyPair.Text = RSA.ToXmlString(true);
            }

            RSA.Clear();
        }

    }
}
