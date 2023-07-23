
using System.Windows.Forms;

namespace DGPSetup
{
    public partial class Results : Form
    {
        public Results(string resultHTML)
        {
            InitializeComponent();

            brwsResults.DocumentText = "0";
            brwsResults.Document.OpenNew(true);
            brwsResults.Document.Write(resultHTML);
            brwsResults.Refresh();
        }
    }
}
