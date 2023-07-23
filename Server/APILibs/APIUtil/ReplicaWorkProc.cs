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
    public class ReplicaWorkProc
    {
        public ReplicaWorkProc()
        {

        }

        /// <summary>
        /// RUNS ON THE CLIENT
        /// the process will call either 2 or 3 API methods, which is why it must be run in the DMZ and not from the internal network;
        /// workURL and workMethod are used to update/release the claimed ReplicaWork record
        /// </summary>
        public static void Replicate(string releaseURL, 
                                    string releaseMethod, 
                                    string workAcct, 
                                    string WorkPword, 
                                    DataRow dataRow, 
                                    string maxSegSize)
        {
            Stopwatch process = new Stopwatch();
            process.Start();
            DateTime startTime = DateTime.UtcNow;
            MsgUtil msgUtil = new MsgUtil();
            long starttime = msgUtil.UnixTimeLong();

            // read work values from DataRow
            string _rec_gid = dataRow[CommonFields.rec_gid].ToString();
            string _schematype = dataRow[WorkFields.SchemaType].ToString();
            string _schematable = dataRow[WorkFields.SchemaTable].ToString();
            string _srcdbname = dataRow[WorkFields.SrcDBName].ToString();
            string _shardname = dataRow[FileFields.ShardName].ToString();
            string _worktype = dataRow[WorkFields.WorkType].ToString();
            string _srcurl = dataRow[WorkFields.SrcURL].ToString();
            string _srcmethod = dataRow[WorkFields.SrcMethod].ToString();
            string _desturl = dataRow[WorkFields.DestURL].ToString();
            string _destmethod = dataRow[WorkFields.DestMethod].ToString();
            string _claimedby = dataRow[WorkFields.ClaimedBy].ToString();
            string _claimid = dataRow[WorkFields.ClaimID].ToString();
            string _logging = dataRow[WorkFields.Logging].ToString();
            string _startid = dataRow[WorkFields.StartID].ToString();
            string _finalid = dataRow[WorkFields.FinalID].ToString();
            string _batchsize = dataRow[WorkFields.BatchSize].ToString();
            int _resetcount = Convert.ToInt32(dataRow[WorkFields.ResetCount].ToString());
            long _intervalMS = Convert.ToInt64(dataRow[WorkFields.IntervalMS]);
            long _maxdurms = Convert.ToInt64(dataRow[WorkFields.MaxDurMS]);
            long _nextrun = Convert.ToInt64(dataRow[WorkFields.NextRun]);

            long runlagms = starttime - _nextrun;

            string _runstate = RunState.Stopped;
            string _statemsg = "";
            string _procsteps = "";
            
            bool _maxbatch = false;
            string _endid = "0";

            if (_logging == "ON")
            {
                _procsteps += "ReplicaWork StartTime: " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";\n";
                _procsteps += "ThreadID: " + Thread.CurrentThread.ManagedThreadId.ToString() + ";\n";
                _procsteps += "SchemaTable: " + _schematable + ";\n";
                _procsteps += "RunLagMS: " + runlagms.ToString() + ";\n";
            }

            try
            {
                // call API method to poll source table for a batch of records greater than the placeholder value
                if (_startid != null && _startid != "")
                {
                    _endid = _startid;

                    Dictionary<string, string> srcparams = new Dictionary<string, string>();
                    srcparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);
                    srcparams.Add(WorkFields.SrcDBName, _srcdbname);
                    srcparams.Add(FileFields.ShardName, _shardname);
                    srcparams.Add(WorkFields.StartID, _startid);
                    srcparams.Add(WorkFields.BatchSize, _batchsize);

                    string srcmethlist = msgUtil.CreateXMLMethod(_srcmethod, srcparams);
                    string srcreqID = msgUtil.GetNewGID();
                    string srcrespMsg = msgUtil.ApiMethHelper(srcmethlist,
                                                                srcreqID,
                                                                workAcct,
                                                                WorkPword,
                                                                _srcurl);

                    RespInfo srcrespinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(srcrespinfo, srcrespMsg);

                    ResInfo srcresult = msgUtil.GetResult(_srcmethod + "_" + MethReturn.Default, methresults);

                    if (srcresult.RCode == APIResult.OK || srcresult.RCode == APIResult.Empty)
                    {
                        if (srcresult.RCode == APIResult.OK && srcresult.DType == APIData.DataTable)
                        {
                            CmnUtil cmnUtil = new CmnUtil();
                            DataTable srctable = cmnUtil.XmlToTable(srcresult.RVal);

                            if (srctable.Rows.Count > 0)
                            {
                                int maxbatch = Convert.ToInt32(_batchsize);
                                if (srctable.Rows.Count >= maxbatch) _maxbatch = true;

                                if (_logging == "ON")
                                {
                                    _procsteps += "SrcRowCount: " + srctable.Rows.Count.ToString() + ";\n";
                                    _procsteps += "SrcMaxBatch: " + _maxbatch.ToString() + ";\n";
                                    _procsteps += "SchemaType: " + _schematype + ";\n";
                                }

                                if (_schematype == SchemaType.Replica)
                                {
                                    // sends the batch of source records to be replicated to the destination
                                    string srctblxml = srcresult.RVal;

                                    // call API method to send the batch of source records to be merged into the destination table (returns new placeholder value)
                                    Dictionary<string, string> destparams = new Dictionary<string, string>();
                                    destparams.Add(CommonFields.SourceRecords, srctblxml);
                                    destparams.Add(WorkFields.StartID, _startid);
                                    destparams.Add(FileFields.ShardName, _shardname);

                                    string destmethlist = msgUtil.CreateXMLMethod(_destmethod, destparams);
                                    string destreqID = msgUtil.GetNewGID();
                                    string destrespMsg = msgUtil.ApiMethHelper(destmethlist,
                                                                                destreqID,
                                                                                workAcct,
                                                                                WorkPword,
                                                                                _desturl);

                                    RespInfo destrespinfo = new RespInfo();
                                    Dictionary<string, ResInfo> destmethresults = msgUtil.ReadResponseString(destrespinfo, destrespMsg);

                                    ResInfo mergeresult = msgUtil.GetResult(_destmethod + "_" + MethReturn.Default, methresults);

                                    if (mergeresult.RCode == APIResult.OK || mergeresult.RCode == APIResult.Done)
                                    {
                                        _runstate = RunState.Ready;
                                        _endid = mergeresult.RVal;

                                        if (_worktype == WorkType.Replicate)
                                        {
                                            _procsteps += srctable.Rows.Count.ToString() + " source records were replicated in the destination table.";
                                        }
                                        else
                                        {
                                            _procsteps += srctable.Rows.Count.ToString() + " source records were verified in the destination table.";
                                        }

                                        _statemsg = mergeresult.RCode;
                                    }
                                    else
                                    {
                                        // error replicating or verifying source records in destination database table
                                        _procsteps += "Replication Error - Replica: " + mergeresult.RCode + " : " + mergeresult.RVal + ";\n";

                                        // compare reset count to maxresets
                                        _resetcount++;
                                        int maxresets = Convert.ToInt32(ConfigurationManager.AppSettings["MaxResets"].ToString());
                                        if (_resetcount > maxresets)
                                        {
                                            _runstate = RunState.Disabled;
                                        }

                                        _statemsg = mergeresult.RCode;
                                        RemoteErrLog.LogError(workAcct, WorkPword, _srcurl, "ReplicaWorkProc.Replicate", _schematype + " : " + _worktype, "Error merging source records into destination table", mergeresult.RVal);
                                    }
                                }
                                else if (_schematype == SchemaType.FileShard)
                                {
                                    // call process to transfer a batch of file segment records from the source to the destination
                                    ProcRes transferRes = FileTransferProc.FileTransfer2(srctable, _startid, workAcct, WorkPword, _srcurl, "FileShard.GetByRowID.base", _desturl, "FileShard.Replicate.base");

                                    if (transferRes.ResCode == APIResult.OK || transferRes.ResCode == APIResult.Done)
                                    {
                                        _runstate = RunState.Ready;
                                        _endid = transferRes.ResVal;

                                        if (_worktype == WorkType.Replicate)
                                        {
                                            _statemsg = srctable.Rows.Count.ToString() + " fileseg source records merged into destination table.";
                                        }
                                        else
                                        {
                                            _statemsg = srctable.Rows.Count.ToString() + " fileseg source records were verified in the destination table.";
                                        }

                                        _statemsg = transferRes.ResCode;
                                    }
                                    else
                                    {
                                        // problem with file transfer replication or verification
                                        _procsteps += "Replication Error - FileShard: " + transferRes.ResMsg + ";\n";

                                        // compare reset count to maxresets
                                        _resetcount++;
                                        int maxresets = Convert.ToInt32(ConfigurationManager.AppSettings["MaxResets"].ToString());
                                        if (_resetcount > maxresets)
                                        {
                                            _runstate = RunState.Disabled;
                                        }

                                        _statemsg = transferRes.ResMsg;
                                        RemoteErrLog.LogError(workAcct, WorkPword, _srcurl, "ReplicaWorkProc.Replicate", _schematype + " : " + _worktype, "Error with replication file transfer and/or merge of source records into destination table", transferRes.ResMsg);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // empty, do nothing and release the work record
                            _statemsg = srcresult.RCode;
                            _runstate = RunState.Ready;
                        }
                    }
                    else
                    {
                        // error in query for source records: set error values for workstate and statemsg
                        _statemsg = srcresult.RCode;
                        RemoteErrLog.LogError(workAcct, WorkPword, _srcurl, "ReplicaWorkProc.Replicate", _schematype + " : " + _worktype, "Error with query for source records", srcresult.RVal);
                    }
                }
                else
                {
                    // previous placeholder value was not assigned
                    _statemsg = "Error: The previous placeholder value was not found.";
                    RemoteErrLog.LogError(workAcct, WorkPword, _srcurl, "ReplicaWorkProc.Replicate", _schematype + " : " + _worktype, "Missing input parameter", _statemsg);
                }
            }
            catch (Exception ex)
            {
                // log exception info
                _statemsg = ex.Message;
                RemoteErrLog.LogException(workAcct, WorkPword, _srcurl, "ReplicaWorkProc.Replicate", _schematype + " : " + _worktype, ex);
            }
            finally
            {
                string _procstate = "";
                long nextrun = msgUtil.GetNextRun(_intervalMS, _maxbatch);

                if (_maxbatch)
                {
                    _procstate = ProcState.Working;
                }
                else
                {
                    _procstate = ProcState.Current;
                }

                if (_runstate == RunState.Ready)
                {
                    if (_worktype == WorkType.Verify)
                    {
                        // check if not backlogged or placeholder >= finalID
                        if (_procstate == ProcState.Current || (_finalid != "0" && (Convert.ToInt64(_endid) >= Convert.ToInt64(_finalid))))
                        {
                            // if verify process has caught up to the current state, end it
                            _runstate = RunState.Stopped;
                            _procstate = ProcState.Complete;
                        }
                    }
                }
                else
                {
                    _procstate = ProcState.Error;
                }

                process.Stop();
                long duration = process.ElapsedMilliseconds;

                // check if duration exceeds max limit
                if (_maxdurms != 0 && duration > _maxdurms)
                {
                    // log error
                    RemoteErrLog.LogError(workAcct, WorkPword, _srcurl, "ReplicaWorkProc.Replicate", _schematype + " : " + _worktype, "Process duration error.", "Process duration = " + duration.ToString() + " MS : Max duration = " + _maxdurms.ToString() + " MS");
                }

                Dictionary<string, string> workparams = new Dictionary<string, string>();
                workparams.Add(WorkFields.RunState, _runstate);
                workparams.Add(WorkFields.StateMsg, _statemsg);
                workparams.Add(WorkFields.ClaimedBy, _claimedby);
                workparams.Add(WorkFields.ClaimID, _claimid);
                workparams.Add(CommonFields.rec_gid, _rec_gid);
                workparams.Add(WorkFields.NextRun, nextrun.ToString());
                workparams.Add(WorkFields.StartID, _endid);
                workparams.Add(WorkFields.ProcState, _procstate);
                workparams.Add(WorkFields.ResetCount, _resetcount.ToString());

                string relmethlist = msgUtil.CreateXMLMethod(releaseMethod, workparams);
                string relreqID = msgUtil.GetNewGID();
                string relrespMsg = msgUtil.ApiMethHelper(relmethlist,
                                                            relreqID,
                                                            workAcct,
                                                            WorkPword,
                                                            releaseURL);

                RespInfo relrespinfo = new RespInfo();
                Dictionary<string, ResInfo> relmethresults = msgUtil.ReadResponseString(relrespinfo, relrespMsg);

                ResInfo releaseresult = msgUtil.GetResult(releaseMethod + "_" + MethReturn.Default, relmethresults);

                if (releaseresult.RCode == null || releaseresult.RCode != APIResult.OK)
                {
                    // log error releasing queue record
                    RemoteErrLog.LogError(workAcct, WorkPword, _srcurl, "ReplicaWorkProc.Replicate", _schematype + " : " + _worktype, "Error releasing ReplicaWork record.", releaseresult.RVal);
                }

                if (_logging == "ON")
                {
                    _procsteps += "EndTime: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";\n";

                    // call AutoWorkLog API
                    Dictionary<string, string> autoparams = new Dictionary<string, string>();
                    autoparams.Add(WorkFields.WorkType, _worktype);
                    autoparams.Add(LogFields.CompName, Environment.MachineName);
                    autoparams.Add(WorkFields.ClaimedBy, _claimedby);
                    autoparams.Add(LogFields.DurationMS, duration.ToString());
                    autoparams.Add(WorkFields.MaxDurMS, _maxdurms.ToString());
                    autoparams.Add(WorkFields.RunState, _runstate);
                    autoparams.Add(WorkFields.StateMsg, _statemsg);
                    autoparams.Add(WorkFields.ProcState, _procstate);
                    autoparams.Add(WorkFields.ProcSteps, _procsteps);

                    string automethlist = msgUtil.CreateXMLMethod("AutoWorkLog.New.base", autoparams);
                    string autoreqID = msgUtil.GetNewGID();
                    string autorespMsg = msgUtil.ApiMethHelper(automethlist,
                                                                autoreqID,
                                                                workAcct,
                                                                WorkPword,
                                                                releaseURL);

                    RespInfo autorespinfo = new RespInfo();
                    Dictionary<string, ResInfo> automethresults = msgUtil.ReadResponseString(autorespinfo, autorespMsg);

                    ResInfo autologresult = msgUtil.GetResult("AutoWorkLog.New.base_DEFAULT", automethresults);

                    if (autologresult.RCode == null || autologresult.RCode != APIResult.OK)
                    {
                        // log error
                        RemoteErrLog.WriteErrToEV(workAcct, Environment.MachineName, "DGP AutoWork", "ReplicaWorkProc.Replicate", "SERVER", "ERROR", "Error creating AutoWorkLog record", autologresult.RVal);
                    }
                }
            }
        }


    }
}
