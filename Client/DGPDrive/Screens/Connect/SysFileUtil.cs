

using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DGPDrive.Util
{
    public class DGPSys
    {
        public string Name { get; set; }
        public string Descrip { get; set; }
        public List<DGPLoc> LocList { get; set; }

        public DGPSys()
        {

        }
    }

    public class DGPLoc
    {
        public string Name { get; set; }
        public string Descrip { get; set; }
        public List<DGPEP> EPList { get; set; }

        public DGPLoc()
        {

        }
    }

    public class DGPEP
    {
        public string Name { get; set; }
        public string Descrip { get; set; }
        public string URL { get; set; }
    }

    public class SysFileUtil
    {
        public SysFileUtil()
        {

        }

        public List<DGPSys> ParseSysList(string filePath, out string readStatus)
        {
            readStatus = "ERROR";
            List<DGPSys> SysList = new List<DGPSys>();

            if (File.Exists(filePath))
            {
                // read the file
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                if (xmlDoc.SelectNodes("SysList/Sys") != null && xmlDoc.SelectNodes("SysList/Sys").ToString() != "")
                {
                    XmlNodeList syslist = xmlDoc.SelectNodes("SysList/Sys");

                    foreach (XmlNode sys in syslist)
                    {
                        DGPSys dgpsys = new DGPSys();
                        dgpsys.Name = sys.SelectSingleNode("SysName").InnerText;
                        dgpsys.Descrip = sys.SelectSingleNode("SysDescrip").InnerText;

                        if (sys.SelectNodes("LocList/Loc") != null && sys.SelectNodes("LocList/Loc").ToString() != "")
                        {
                            XmlNodeList loclist = sys.SelectNodes("LocList/Loc");
                            dgpsys.LocList = new List<DGPLoc>();

                            foreach (XmlNode loc in loclist)
                            {
                                DGPLoc dgploc = new DGPLoc();
                                dgploc.Name = loc.SelectSingleNode("LocName").InnerText;
                                dgploc.Descrip = loc.SelectSingleNode("LocDescrip").InnerText;

                                if (loc.SelectNodes("EPList/EP") != null && loc.SelectNodes("EPList/EP").ToString() != "")
                                {
                                    XmlNodeList eplist = loc.SelectNodes("EPList/EP");
                                    dgploc.EPList = new List<DGPEP>();

                                    foreach (XmlNode ep in eplist)
                                    {
                                        DGPEP dgpep = new DGPEP();
                                        dgpep.Name = ep.SelectSingleNode("EPName").InnerText;
                                        dgpep.Descrip = ep.SelectSingleNode("EPDescrip").InnerText;
                                        dgpep.URL = ep.SelectSingleNode("EPURL").InnerText;

                                        dgploc.EPList.Add(dgpep);
                                    }
                                }

                                dgpsys.LocList.Add(dgploc);
                            }
                        }

                        SysList.Add(dgpsys);
                    }
                }

                readStatus = "OK";
            }

            return SysList;
        }

    }
}
