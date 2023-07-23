

using System;

namespace CliUtil
{
    public class DGPFileInfo
    {
        public DGPFileInfo()
        {

        }

        public string FileName { get; set; }
        public string FileExt { get; set; }
        public long FileBytes { get; set; }
        public DateTime FileDate { get; set; }
        public string FileHash { get; set; }
    }
}
