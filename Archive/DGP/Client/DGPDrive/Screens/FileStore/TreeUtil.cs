using System;
using System.Windows.Forms;

namespace DGPDrive.Screens.FileStore
{

    public class TreeUtil
    {
        public TreeUtil()
        {

        }

        
    }

    public class LatticeNode
    {
        public LatticeNode()
        {

        }

        public string ParentGID { get; set; }
        public string FolderGID { get; set; }
        public string FolderName { get; set; }
        public string DisplayOrder { get; set; }
        public string GroupGID { get; set; }
        public string AccessLevel { get; set; }
        public string row_ms { get; set; }
        public int FileCount { get; set; }
    }
}
