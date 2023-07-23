using System;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;
using System.Collections.Generic;
using ApiUtil.DataClasses;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using System.Xml;

using ApiUtil;

namespace DGPAutoWork
{
    public partial class DGPAutoWorkSvc : ServiceBase
    {
        private Timer ReplicaTimer = new Timer();
        private Timer GeneralTimer = new Timer();
        private Timer QueueTimer = new Timer();

        // populated from App.config
        string _logging = "";
        string _claimedby = "";
        string _claimid = "";
        string _acctname = "";
        string _combopword = "";
        string _svcurl = "";

        string _replicawork = "";
        string _replicanet = "";
        int _replicaMS = 0;
        int _replicaBatch = 0;

        string _generalwork = "";
        string _generalnet = "";
        int _generalMS = 0;
        int _generalBatch = 0;

        string _queuework = "";
        int _queueMS = 0;

        // populated from login method
        string _websvcname = "";
        string _websvcverstr = "";
        string _maxsegsize = "";
        string _usergid = "";

        public DGPAutoWorkSvc()
        {
            InitializeComponent();

            try
            {
                _logging = ConfigurationManager.AppSettings[WorkFields.Logging].ToString();
                _claimedby = ConfigurationManager.AppSettings[WorkFields.ClaimedBy].ToString();
                _acctname = ConfigurationManager.AppSettings[ReplicaFields.AcctName].ToString();
                _combopword = _acctname + ConfigurationManager.AppSettings[ReplicaFields.AcctPword].ToString();
                _svcurl = ConfigurationManager.AppSettings[TestResultsFields.SvcURL].ToString();
                
                _replicawork = ConfigurationManager.AppSettings[ReplicaFields.ReplicaWork].ToString();
                _replicanet = ConfigurationManager.AppSettings[ReplicaFields.ReplicaNetwork].ToString();
                _replicaMS = Convert.ToInt32(ConfigurationManager.AppSettings[ReplicaFields.ReplicaWorkMS].ToString());
                _replicaBatch = Convert.ToInt32(ConfigurationManager.AppSettings[ReplicaFields.ReplicaMaxBatch].ToString());

                _generalwork = ConfigurationManager.AppSettings[ReplicaFields.GeneralWork].ToString();
                _generalnet = ConfigurationManager.AppSettings[ReplicaFields.GeneralNetwork].ToString();
                _generalMS = Convert.ToInt32(ConfigurationManager.AppSettings[ReplicaFields.GeneralWorkMS].ToString());
                _generalBatch = Convert.ToInt32(ConfigurationManager.AppSettings[ReplicaFields.GeneralMaxBatch].ToString());

                _queuework = ConfigurationManager.AppSettings[ReplicaFields.QueueCheck].ToString();
                _queueMS = Convert.ToInt32(ConfigurationManager.AppSettings[ReplicaFields.QueueCheckMS].ToString());
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc", ex);
            }
        }

        internal void ConsoleStartAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
                    Console.WriteLine("Press the Enter key to stop the application...\n");
                }

                // initialize the app (call login method)
                Login();

                // intialize timers
                if (_replicawork == APISetting.ON || _replicawork == APISetting.SINGLE) SetReplicaTimer(_replicaMS);
                if (_generalwork == APISetting.ON || _generalwork == APISetting.SINGLE) SetGeneralTimer(_generalMS);
                if (_queuework == APISetting.ON || _queuework == APISetting.SINGLE) SetQueueTimer(_queueMS);

                if (_logging == APISetting.ON)
                {
                    RemoteErrLog.LogInfo(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnStart", "DGP Service Start", "The DGP Service started at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnStart", ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                ReplicaTimer.Stop();
                ReplicaTimer.Dispose();

                GeneralTimer.Stop();
                GeneralTimer.Dispose();

                QueueTimer.Stop();
                QueueTimer.Dispose();


                if (Environment.UserInteractive)
                {
                    Console.WriteLine("Terminating the application...");
                }

                if (_logging == APISetting.ON)
                {
                    RemoteErrLog.LogInfo(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnStop", "DGP Service Stop", "The DGP Service stopped at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnStop", ex);
            }
        }

        protected void Login()
        {
            try
            {
                Dictionary<string, string> loginparams = new Dictionary<string, string>();
                loginparams.Add(CommonFields.SchemaFlag, "true");

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methlist = msgUtil.CreateXMLMethod("UserSelf.Login.base", loginparams);

                string respmsg = msgUtil.ApiMethHelper(methlist,
                                                        reqID,
                                                        _acctname,
                                                        _combopword,
                                                        _svcurl);

                RespInfo respinfo = new RespInfo();
                Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                // first check overall authentication
                if (respinfo.Auth == AuthState.OK)
                {
                    // login method returns multiple results and the values are assigned to main form properties
                    ResInfo apiUser = msgUtil.GetResult("UserSelf.Login.base_DEFAULT", methresults);
                    if (apiUser != null)
                    {
                        if (apiUser.RCode.ToUpper() == APIResult.OK)
                        {
                            XmlDocument doc = new XmlDocument();
                            XmlDocumentFragment docfrag = doc.CreateDocumentFragment();
                            docfrag.InnerXml = apiUser.RVal;

                            if (docfrag.SelectNodes("LoginResults") != null && docfrag.SelectNodes("LoginResults").ToString() != "")
                            {
                                // read XML values and assign to their respective main form properties
                                _websvcname = docfrag.SelectSingleNode("//WebSvcName").InnerText;
                                _websvcverstr = docfrag.SelectSingleNode("//WebSvcVer").InnerText;
                                _maxsegsize = docfrag.SelectSingleNode("//MaxSegSize").InnerText;
                                _usergid = docfrag.SelectSingleNode("//UserGID").InnerText;
                                string resptime = docfrag.SelectSingleNode("//RespTime").InnerText;
                                string resptoken = docfrag.SelectSingleNode("//RespToken").InnerText;

                                if (resptime != null && resptime != "" && resptoken != null && resptoken != "")
                                {
                                    EncryptUtil encryptUtil = new EncryptUtil();
                                    bool serverAuth = encryptUtil.ValidateHMACHash(_combopword, resptime, resptoken);
                                    if (!serverAuth)
                                    {
                                        // Server Authentication failed
                                        RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.Login", "Login Authentication Error", "Login method authentication of the Server failed.");
                                    }
                                }
                                else
                                {
                                    // RespTime and/or RespToken missing
                                    RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.Login", "Login Authentication Error", "RespTime and/or RespToken missing");
                                }
                            }
                            else
                            {
                                // LoginResult XML was not returned by the Login method
                                RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.Login", "Login Authentication Error", "LoginResult XML was not returned by the Login method");
                            }
                        }
                        else
                        {
                            // ApiUser Error: " + apiUser.RCode + " : " + apiUser.RVal
                            RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.Login", "Error claiming ReplicaWork record.", "ApiUser Error: " + apiUser.RCode + " : " + apiUser.RVal);
                        }
                    }
                    else
                    {
                        // No result found for UserSelf.Login.base_DEFAULT
                        RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.Login", "Login Authentication Error", "No result found for UserSelf.Login.base_DEFAULT");
                    }
                }
                else
                {
                    // API Request Error: " + respinfo.Auth + " - " + respinfo.Info
                    RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.Login", "Login Authentication Error", "API Request Error: " + respinfo.Auth + " - " + respinfo.Info);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.Login", ex);
            }
        }

        //***************************************************************************************//
        //***************************************************************************************//
        //***************************************************************************************//

        protected void SetReplicaTimer(int replicaMS)
        {
            ReplicaTimer = new Timer(replicaMS);
            ReplicaTimer.Elapsed += OnReplicaEvent;
            ReplicaTimer.AutoReset = false;
            ReplicaTimer.Enabled = true;
        }

        private void OnReplicaEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (_replicawork == APISetting.ON || _replicawork == APISetting.SINGLE)
                {
                    Stopwatch claimed = new Stopwatch();
                    claimed.Start();
                    DateTime startTime = DateTime.UtcNow;

                    string _runstate = "";
                    string _statemsg = "";
                    string _procsteps = "";
                    int _claimcount = 0;

                    CmnUtil cmnUtil = new CmnUtil();
                    string claimid = cmnUtil.GetNewGID();

                    if (_logging == APISetting.ON)
                    {
                        _procsteps += "StartTime: " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";\n";
                        _procsteps += "ClaimedBy: " + _claimedby + ";\n";
                        _procsteps += "ClaimID: " + claimid + ";\n";
                        _procsteps += "RunMode: " + _replicawork + ";\n";
                    }

                    Dictionary<string, string> claimparams = new Dictionary<string, string>();
                    claimparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);
                    claimparams.Add(WorkFields.Network, _replicanet);
                    claimparams.Add(WorkFields.ClaimedBy, _claimedby);
                    claimparams.Add(WorkFields.ClaimID, claimid);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string claimList = msgUtil.CreateXMLMethod("ReplicaWork.ClaimRecords.base", claimparams);

                    string claimresp = msgUtil.ApiMethHelper(claimList,
                                                            reqID,
                                                            _acctname,
                                                            _combopword,
                                                            _svcurl);

                    if (claimresp != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, claimresp);

                        ResInfo claimresult = msgUtil.GetResult("ReplicaWork.ClaimRecords.base_DEFAULT", methresults);

                        if (claimresult != null && (claimresult.RCode == APIResult.OK || claimresult.RCode == APIResult.Empty))
                        {
                            if (claimresult.RCode == APIResult.OK && claimresult.DType == APIData.DataTable)
                            {
                                DataTable _claimedRecs = cmnUtil.XmlToTable(claimresult.RVal);

                                if (_claimedRecs.Rows.Count > 0)
                                {
                                    _claimcount = _claimedRecs.Rows.Count;

                                    // run each batch of claimed records as a standalone fire-and-forget process
                                    Task.Run(() =>
                                    Parallel.ForEach(_claimedRecs.AsEnumerable(), (drow) =>
                                    {
                                        ReplicaWorkProc.Replicate(_svcurl, "ReplicaWork.ReleaseRecord.base", _acctname, _combopword, drow, _maxsegsize);
                                    }));
                                }
                                else
                                {
                                    _claimcount = 0;
                                }
                            }
                            else
                            {
                                string errmsg = _claimid + " Error: " + claimresult.RCode + " - " + claimresult.RVal;
                                RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnReplicaEvent", "Error claiming ReplicaWork record.", errmsg);
                            }
                        }
                        else
                        {
                            string errmsg = _claimid + " Error: " + claimresult.RCode + " - " + claimresult.RVal;
                            RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnReplicaEvent", "Error claiming ReplicaWork record.", errmsg);
                        }
                    }

                    claimed.Stop();
                    long duration = claimed.ElapsedMilliseconds;
                    long _maxdurms = Convert.ToInt64(ConfigurationManager.AppSettings["AutoWorkMaxDurMS"]);

                    // check if duration exceeds max limit
                    if (_maxdurms != 0 && duration > _maxdurms)
                    {
                        // log error
                        RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnReplicaEvent", "Process duration error.", "Process duration = " + duration.ToString() + " MS : Max duration = " + _maxdurms.ToString() + " MS");
                    }

                    if (_logging == APISetting.ON)
                    {
                        _procsteps += "ClaimedRecs: " + _claimcount.ToString() + ";\n";
                        _procsteps += "EndTime: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";\n";

                        // call AutoWorkLog API
                        Dictionary<string, string> autoparams = new Dictionary<string, string>();
                        autoparams.Add(WorkFields.WorkType, ReplicaFields.ReplicaWork);
                        autoparams.Add(LogFields.CompName, Environment.MachineName);
                        autoparams.Add(WorkFields.ClaimedBy, _claimedby);
                        autoparams.Add(LogFields.DurationMS, duration.ToString());
                        autoparams.Add(WorkFields.MaxDurMS, _maxdurms.ToString());
                        autoparams.Add(WorkFields.RunState, _runstate);
                        autoparams.Add(WorkFields.StateMsg, _statemsg);
                        autoparams.Add(WorkFields.ProcState, ProcState.Working);
                        autoparams.Add(WorkFields.ProcSteps, _procsteps);

                        string logList = msgUtil.CreateXMLMethod("AutoWorkLog.New.base", autoparams);

                        string logresp = msgUtil.ApiMethHelper(logList,
                                                                reqID,
                                                                _acctname,
                                                                _combopword,
                                                                _svcurl);

                        if (logresp != "")
                        {
                            RespInfo respinfo = new RespInfo();
                            Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, logresp);

                            ResInfo autologresult = msgUtil.GetResult("AutoWorkLog.New.base_DEFAULT", methresults);

                            if (autologresult.RCode == null || autologresult.RCode != APIResult.OK)
                            {
                                // log error
                                RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnReplicaEvent", "Error creating AutoWorkLog record: " + autologresult.RVal, _procsteps);
                            }
                        }
                    }

                    if (Environment.UserInteractive)
                    {
                        Console.WriteLine(_procsteps);
                    }
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnReplicaEvent", ex);
            }

            // only restart timer if set to ON
            if (_replicawork == APISetting.ON) ReplicaTimer.AutoReset = true;
        }

        //***************************************************************************************//
        //***************************************************************************************//
        //***************************************************************************************//

        protected void SetGeneralTimer(int generalMS)
        {
            GeneralTimer = new Timer(generalMS);
            GeneralTimer.Elapsed += OnGeneralEvent;
            GeneralTimer.AutoReset = false;
            GeneralTimer.Enabled = true;
        }

        private void OnGeneralEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (_generalwork == APISetting.ON || _generalwork == APISetting.SINGLE)
                {
                    GeneralTimer.Enabled = false;

                    Stopwatch claimed = new Stopwatch();
                    claimed.Start();
                    DateTime startTime = DateTime.UtcNow;

                    string _runstate = "";
                    string _statemsg = "";
                    string _procsteps = "";
                    int _claimcount = 0;

                    CmnUtil cmnUtil = new CmnUtil();
                    string claimid = cmnUtil.GetNewGID();

                    if (_logging == APISetting.ON)
                    {
                        _procsteps += "StartTime: " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";\n";
                        _procsteps += "ClaimedBy: " + _claimedby + ";\n";
                        _procsteps += "ClaimID: " + claimid + ";\n";
                        _procsteps += "RunMode: " + _generalwork + ";\n";
                    }

                    Dictionary<string, string> claimparams = new Dictionary<string, string>();
                    claimparams.Add(CommonFields.SchemaFlag, BoolVal.TRUE);
                    claimparams.Add(WorkFields.Network, _generalnet);
                    claimparams.Add(WorkFields.ClaimedBy, _claimid);

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string claimList = msgUtil.CreateXMLMethod("GeneralWork.ClaimRecords.base", claimparams);

                    string claimresp = msgUtil.ApiMethHelper(claimList,
                                                                reqID,
                                                                _acctname,
                                                                _combopword,
                                                                _svcurl);

                    if (claimresp != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, claimresp);

                        ResInfo claimresult = msgUtil.GetResult("GeneralWork.ClaimRecords.base_DEFAULT", methresults);

                        if (claimresult != null && (claimresult.RCode == APIResult.OK || claimresult.RCode == APIResult.Empty))
                        {
                            if (claimresult.RCode == APIResult.OK)
                            {
                                if (claimresult.DType == APIData.DataTable)
                                {
                                    DataTable _claimedRecs = cmnUtil.XmlToTable(claimresult.RVal);

                                    if (_claimedRecs.Rows.Count > 0)
                                    {
                                        _claimcount = _claimedRecs.Rows.Count;

                                        // run each batch of claimed records as a standalone fire-and-forget process
                                        Task.Run(() =>
                                        Parallel.ForEach(_claimedRecs.AsEnumerable(), (drow) =>
                                        {
                                            GeneralWorkProc.DoWork(_svcurl, "GeneralWork.ReleaseRecord.base", _acctname, _combopword, drow);
                                        }));
                                    }
                                    else
                                    {
                                        _claimcount = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            string errmsg = _claimid + " Error: " + claimresult.RCode + " - " + claimresult.RVal;
                            RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnGeneralEvent", "Error claiming GeneralWork record.", errmsg);
                        }
                    }

                    claimed.Stop();
                    long duration = claimed.ElapsedMilliseconds;
                    long _maxdurms = Convert.ToInt64(ConfigurationManager.AppSettings["AutoWorkMaxDurMS"]);

                    // check if duration exceeds max limit
                    if (_maxdurms != 0 && duration > _maxdurms)
                    {
                        // log error
                        RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnGeneralEvent", "Process duration error.", "Process duration = " + duration.ToString() + " MS : Max duration = " + _maxdurms.ToString() + " MS");
                    }

                    if (_logging == APISetting.ON)
                    {
                        _procsteps += "ClaimedRecs: " + _claimcount.ToString() + ";\n";
                        _procsteps += "EndTime: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";\n";

                        // call AutoWorkLog API
                        Dictionary<string, string> autoparams = new Dictionary<string, string>();
                        autoparams.Add(WorkFields.WorkType, ReplicaFields.GeneralWork);
                        autoparams.Add(LogFields.CompName, Environment.MachineName);
                        autoparams.Add(WorkFields.ClaimedBy, _claimid);
                        autoparams.Add(LogFields.DurationMS, duration.ToString());
                        autoparams.Add(WorkFields.MaxDurMS, _maxdurms.ToString());
                        autoparams.Add(WorkFields.RunState, _runstate);
                        autoparams.Add(WorkFields.StateMsg, _statemsg);
                        autoparams.Add(WorkFields.ProcState, ProcState.Working);
                        autoparams.Add(WorkFields.ProcSteps, _procsteps);

                        string logList = msgUtil.CreateXMLMethod("AutoWorkLog.New.base", autoparams);

                        string logresp = msgUtil.ApiMethHelper(logList,
                                                                reqID,
                                                                _acctname,
                                                                _combopword,
                                                                _svcurl);

                        if (logresp != "")
                        {
                            RespInfo respinfo = new RespInfo();
                            Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, logresp);

                            ResInfo autologresult = msgUtil.GetResult("AutoWorkLog.New.base_DEFAULT", methresults);

                            if (autologresult.RCode == null || autologresult.RCode != APIResult.OK)
                            {
                                // log error
                                RemoteErrLog.LogError(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnReplicaEvent", "Error creating AutoWorkLog record: " + autologresult.RVal, _procsteps);
                            }
                        }
                    }

                    if (Environment.UserInteractive)
                    {
                        Console.WriteLine(_procsteps);
                    }
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnGeneralEvent", ex);
            }

            // only restart timer if set to ON
            if (_generalwork == APISetting.ON) GeneralTimer.Enabled = true;
        }

        //***************************************************************************************//
        //***************************************************************************************//
        //***************************************************************************************//

        protected void SetQueueTimer(int queueMS)
        {
            QueueTimer = new Timer(queueMS);
            QueueTimer.Elapsed += OnQueueEvent;
            QueueTimer.AutoReset = false;
            QueueTimer.Enabled = true;
        }

        private void OnQueueEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (_queuework == APISetting.ON || _queuework == APISetting.SINGLE)
                {
                    QueueTimer.Enabled = false;

                    if (Environment.UserInteractive)
                    {
                        Console.WriteLine("The Queue timer event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
                    }

                    Task.Run(() => QueueScanProc.ScanQueue("ReplicaWork", _acctname, _combopword, _svcurl, "ON"));

                    Task.Run(() => QueueScanProc.ScanQueue("GeneralWork", _acctname, _combopword, _svcurl, "ON"));

                    if (_logging == APISetting.ON)
                    {
                        RemoteErrLog.LogInfo(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnQueueEvent", "OnQueueEvent Timer", "The OnQueueEvent timer was raised at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    }
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_acctname, _combopword, _svcurl, "DGPAutoWorkSvc", "DGPAutoWorkSvc.OnQueueEvent", ex);
            }

            // only restart timer if set to ON
            if (_queuework == APISetting.ON) QueueTimer.Enabled = true;
        }
    }
}
