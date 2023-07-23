using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

using ApiUtil.DataClasses;

namespace ApiUtil
{
    /// <summary>
    /// Run on the CLIENT
    /// </summary>
    public class QueueScanProc
    {
        public QueueScanProc()
        {

        }

        /// <summary>
        /// RUNS ON THE CLIENT
        /// the process will call either 2 or 3 API methods, which is why it must be run in the DMZ and not from the internal network;
        /// workURL and workMethod are used to update/release the claimed ReplicaWork record
        /// </summary>
        public static void ScanQueue(string scanType, 
                                    string workAcct, 
                                    string WorkPword, 
                                    string scanURL,
                                    string logFlag)
        {
            Stopwatch process = new Stopwatch();
            process.Start();
            DateTime startTime = DateTime.UtcNow;
            MsgUtil msgUtil = new MsgUtil();
            long starttime = msgUtil.UnixTimeLong();

            string _procsteps = "";

            if (logFlag == "ON")
            {
                _procsteps += "QueueScan StartTime: " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";\n";
                _procsteps += "ThreadID: " + Thread.CurrentThread.ManagedThreadId.ToString() + ";\n";
            }

            try
            {
                // call API method to poll source table for a batch of records greater than the placeholder value
                string scanMethod = "";
                string releaseMethod = "";
                if (scanType == "ReplicaWork")
                {
                    scanMethod = "ReplicaWork.QueueScan.base";
                    releaseMethod = "ReplicaWork.ReleaseRecord.base";
                }
                else
                {
                    scanMethod = "GeneralWork.QueueScan.base";
                    releaseMethod = "GeneralWork.ReleaseRecord.base";
                }

                Dictionary<string, string> scanparams = new Dictionary<string, string>();
                scanparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);

                string srcmethlist = msgUtil.CreateXMLMethod(scanMethod, scanparams);
                string srcreqID = msgUtil.GetNewGID();
                string srcrespMsg = msgUtil.ApiMethHelper(srcmethlist,
                                                            srcreqID,
                                                            workAcct,
                                                            WorkPword,
                                                            scanURL);

                RespInfo srcrespinfo = new RespInfo();
                Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(srcrespinfo, srcrespMsg);

                ResInfo scanresult = msgUtil.GetResult(scanMethod + "_" + MethReturn.Default, methresults);

                if (scanresult.RCode == APIResult.OK || scanresult.RCode == APIResult.Empty)
                {
                    if (scanresult.RCode == APIResult.OK && scanresult.DType == APIData.DataTable)
                    {
                        CmnUtil cmnUtil = new CmnUtil();
                        DataTable scantable = cmnUtil.XmlToTable(scanresult.RVal);

                        if (scantable.Rows.Count > 0)
                        {
                            if (logFlag == "ON")
                            {
                                _procsteps += "SrcRowCount: " + scantable.Rows.Count.ToString() + ";\n";
                            }

                            foreach (DataRow dr in scantable.Rows)
                            {
                                int resetcount = Convert.ToInt32(dr[WorkFields.ResetCount].ToString());
                                resetcount++;
                                string runstate = RunState.Ready;
                                int maxresets = Convert.ToInt32(ConfigurationManager.AppSettings["MaxResets"].ToString());
                                if (resetcount > maxresets)
                                {
                                    runstate = RunState.Disabled;
                                }

                                // log the problem
                                RemoteErrLog.LogError(workAcct, WorkPword, scanURL, "AutoWork Service", "QueuScanProc.QueueScan", "QueueScan Error: " + scanType, "Record " + dr["rec_gid"].ToString() + " reset count: " + resetcount.ToString());

                                // release the record with updated resetcount value
                                Dictionary<string, string> workparams = new Dictionary<string, string>();
                                workparams.Add(WorkFields.RunState, runstate);
                                workparams.Add(WorkFields.StateMsg, "");
                                workparams.Add(WorkFields.ClaimedBy, "");
                                workparams.Add(WorkFields.ClaimID, "");
                                workparams.Add(CommonFields.rec_gid, dr["rec_gid"].ToString());
                                workparams.Add(WorkFields.NextRun, dr["NextRun"].ToString());
                                workparams.Add(WorkFields.StartID, dr["StartID"].ToString());
                                workparams.Add(WorkFields.ProcState, "Reset");
                                workparams.Add(WorkFields.ResetCount, resetcount.ToString());

                                string relmethlist = msgUtil.CreateXMLMethod(releaseMethod, workparams);
                                string relreqID = msgUtil.GetNewGID();
                                string relrespMsg = msgUtil.ApiMethHelper(relmethlist,
                                                                            relreqID,
                                                                            workAcct,
                                                                            WorkPword,
                                                                            scanURL);

                                RespInfo relrespinfo = new RespInfo();
                                Dictionary<string, ResInfo> relmethresults = msgUtil.ReadResponseString(relrespinfo, relrespMsg);

                                ResInfo releaseresult = msgUtil.GetResult(releaseMethod + "_" + MethReturn.Default, relmethresults);

                                if (releaseresult.RCode == null || releaseresult.RCode != APIResult.OK)
                                {
                                    // log error releasing queue record
                                    RemoteErrLog.LogError(workAcct, WorkPword, scanURL, "AutoWork Service", "QueuScanProc.QueueScan", "Error releasing ReplicaWork record.", releaseresult.RVal);
                                }
                            }


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // log exception info
                RemoteErrLog.LogException(workAcct, WorkPword, scanURL, "AutoWork Service", "QueuScanProc.QueueScan", ex);
            }
            
        }


    }
}
