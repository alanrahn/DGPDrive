using System;
using System.IO;
using System.Windows.Forms;

namespace DGPSetup
{
    public partial class DBSetupHelp : Form
    {
        public DBSetupHelp()
        {
            InitializeComponent();

            string curDir = Directory.GetCurrentDirectory();
            this.brwsHelp.Url = new Uri(String.Format("file:///{0}/DBSetupHelp.html", curDir));
        }
    }
}
