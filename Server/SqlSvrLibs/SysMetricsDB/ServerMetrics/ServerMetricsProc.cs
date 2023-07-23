using System;
using System.Diagnostics;
using ApiUtil;

namespace SysMetricsDB
{
    public class ServerMetricsProc
    {
        string _connstr;

        public ServerMetricsProc(string connStr)
        {
            _connstr = connStr;
        }

        public string StoreServerMetrics(string userName, string webSvcName, string webSvcVersion)
        {
            PerformanceCounter totalbytes = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter gen0bytes = new PerformanceCounter(".NET CLR Memory", "Gen 0 heap size", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter gen1bytes = new PerformanceCounter(".NET CLR Memory", "Gen 1 heap size", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter gen2bytes = new PerformanceCounter(".NET CLR Memory", "Gen 2 heap size", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter lohbytes = new PerformanceCounter(".NET CLR Memory", "Large Object Heap size", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter gen0gc = new PerformanceCounter(".NET CLR Memory", "# Gen 0 Collections", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter gen1gc = new PerformanceCounter(".NET CLR Memory", "# Gen 1 Collections", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter gen2gc = new PerformanceCounter(".NET CLR Memory", "# Gen 2 Collections", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter gcpercent = new PerformanceCounter(".NET CLR Memory", "% Time in GC", Process.GetCurrentProcess().ProcessName);

            string totalBytes = totalbytes.NextValue().ToString();
            string gen0Bytes = gen0bytes.NextValue().ToString();
            string gen1Bytes = gen1bytes.NextValue().ToString();
            string gen2Bytes = gen2bytes.NextValue().ToString();
            string lohBytes = lohbytes.NextValue().ToString();
            string gen0GC = gen0gc.NextValue().ToString();
            string gen1GC = gen1gc.NextValue().ToString();
            string gen2GC = gen2gc.NextValue().ToString();
            string gcPercent = gcpercent.NextValue().ToString();

            CmnUtil cmnUtil = new CmnUtil();
            string newGID = cmnUtil.GetNewGID();

            ServerMetrics_dml serverMetrics_Dml = new ServerMetrics_dml(_connstr);
            return serverMetrics_Dml.Write(newGID, userName, Environment.MachineName, webSvcName, webSvcVersion, totalBytes, gen0Bytes, gen1Bytes, gen2Bytes, lohBytes, gen0GC, gen1GC, gen2GC, gcPercent);
        }
    }
}
