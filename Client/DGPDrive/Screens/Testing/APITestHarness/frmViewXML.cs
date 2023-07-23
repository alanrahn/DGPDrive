
using System.Drawing;
using System.Windows.Forms;

namespace DGPDrive.Screens.Testing
{
    public partial class frmViewXML : Form
    {
        public frmViewXML(string xmlDoc, string viewType)
        {
            InitializeComponent();

            switch (viewType)
            {
                case "TESTFILE":
                    this.Text = "View XML : Test File";
                    break;

                case "EVALINFO":
                    this.Text = "View XML : Evaluation Info";
                    break;

                case "REQUEST":
                    this.Text = "View XML : Request";
                    break;

                case "RESPONSE":
                    this.Text = "View XML : Response";
                    break;
            }

            wbrXMLView.DocumentText = xmlDoc;
        }
    }
}
