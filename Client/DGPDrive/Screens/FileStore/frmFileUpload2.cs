using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Net.Http;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.Diagnostics;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.FileStore
{
    /// <summary>
    /// New version that does not use background worker thread, and uploads files in the main UI thread
    /// </summary>
    public partial class frmFileUpload2 : Form
    {
        MainForm _main;
        ucFileStore _parent;
        DGPNode _folder;
        FileData _filedata;
        CommonOpenFileDialog locdialog;
        ResultHTML _resultHTML;

        string _action;
        string _rec_gid;
        string _row_ms;
        string _version;
        string _folderpath;

        string _htmlResultBase;
        string _htmlTempResult;

        int _totalSeg;
        int _segSize;

        public frmFileUpload2(MainForm mainForm, ucFileStore parentForm, DGPNode selNode, string folderPath, string action, string rec_gid, string filename, string filedescrip, string row_ms, string version)
        { 
            InitializeComponent();

            try
            {
                _main = mainForm;
                _parent = parentForm;
                _folder = selNode;
                _rec_gid = rec_gid;
                
                _row_ms = row_ms;
                _version = version;
                _action = action;
                _folderpath = folderPath;

                tbxSelectedFolder.Text = folderPath;

                tbxFileName.Text = filename;
                tbxFileDescrip.Text = filedescrip;

                Environment.CurrentDirectory = _parent.UploadDir;

                locdialog = new CommonOpenFileDialog();
                locdialog.Title = "Select File to Upload";
                //locdialog.Filters.Add(new CommonFileDialogFilter("Content Files", "*.*"));
                locdialog.EnsurePathExists = true;
                locdialog.EnsureValidNames = true;
                locdialog.IsFolderPicker = false;
                locdialog.Multiselect = false;

                btnUpload.Enabled = false;

                _segSize = Convert.ToInt32(_main.MaxSegSize);

                if (_action == ReplicaAction.Insert)
                {
                    // new file with new global ID and version
                    this.Text = "Upload New File";
                    _rec_gid = "";
                    tbxGlobalID.Text = "";
                    _version = "1";
                }
                else
                {
                    // new version of an existing file
                    this.Text = "Upload New File Version";
                    tbxGlobalID.Text = _rec_gid;
                }

                ClearResults();

                // initialize HTML progress message
                _resultHTML = new ResultHTML();
                _htmlResultBase = _resultHTML.HTMLStart() + "<div class=\"titlediv\">Upload Process:</div><div class=\"innerdiv\">";
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmFileUpload2", ex);
                MessageBox.Show(ex.Message, "frmFileUpload2", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // browse for file to upload
            try
            {
                if (_parent.UploadDir != null && _parent.UploadDir != "")
                {
                    locdialog.InitialDirectory = _parent.UploadDir;
                }
                else
                {
                    locdialog.InitialDirectory = Environment.CurrentDirectory;
                }

                if (locdialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    tbxFilePath.Text = locdialog.FileName;

                    if (File.Exists(tbxFilePath.Text))
                    {
                        btnUpload.Enabled = true;
                        _parent.UploadDir = Path.GetDirectoryName(locdialog.FileName);

                        FileUtil fileUtil = new FileUtil();
                        _filedata = fileUtil.GetFileData(tbxFilePath.Text);

                        if (tbxFileName.Text == "") tbxFileName.Text = _filedata.FileName + _filedata.FileExt;

                        _htmlResultBase += "<p class=\"success\">File Size: " + _filedata.FileBytes.ToString() + " Bytes</p>";

                        _htmlTempResult += "</div>" + _resultHTML.HTMLEnd();

                        RefreshResults(_htmlResultBase + _htmlTempResult);
                        Application.DoEvents();
                    }

                    btnUpload.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmFileUpload2.btnBrowse_Click", ex);
                MessageBox.Show(ex.Message, "frmFileUpload2.btnBrowse_Click", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Process to upload the selected file as file segments linked to a parent metadata record
        /// </summary>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpload.Enabled = false;
            btnBrowse.Enabled = false;

            string procmsg = "";
            string procresult = "";
            long totalMS = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Cursor std = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (_filedata.FileBytes > 0 && _filedata.FileBytes <= Convert.ToInt64(_main.MaxFileSize))
                {
                    long offset = 0;
                    string shardname = "";
                    string _filems = "";

                    // query to get the shard name of a writable shard
                    Dictionary<string, string> shardparams = new Dictionary<string, string>();

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("FileShard.GetShardName.base", shardparams);
                    SOAPMsgUtil soapmsg = new SOAPMsgUtil();
                    string respmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo shardresult = msgUtil.GetResult("FileShard.GetShardName.base_DEFAULT", methresults);

                        if (shardresult.RCode.ToUpper() == APIResult.OK)
                        {
                            sw.Stop();
                            // get the name of the writable shard to be used
                            shardname = shardresult.RVal;

                            totalMS += sw.ElapsedMilliseconds;
                            procmsg = "<p class=\"success\">ShardName: " + shardname + " retrieved from server: " + sw.ElapsedMilliseconds.ToString() + " MS</p>";
                            sw.Restart();

                            if (_action == ReplicaAction.Update)
                            {
                                // file update increments version number
                                int priorversion = Convert.ToInt32(_version);
                                priorversion++;
                                _version = priorversion.ToString();
                            }

                            // calculate total segments for file
                            FileUtil fileUtil = new FileUtil();
                            _totalSeg = fileUtil.GetFileSegCount(_filedata.FileBytes, _segSize);

                            // create or update parent file metadata record; get new global ID in return
                            Dictionary<string, string> fileparams = new Dictionary<string, string>();
                            fileparams.Add(CommonFields.Action, _action);
                            fileparams.Add(CommonFields.rec_gid, _rec_gid);
                            fileparams.Add(CommonFields.row_ms, _row_ms);
                            fileparams.Add(FolderFields.FolderGID, _folder.FolderGID);
                            fileparams.Add(FolderFields.GroupGID, _folder.GroupGID);
                            fileparams.Add(FolderFields.FolderPath, _folderpath);
                            fileparams.Add(FileFields.FileName, _filedata.FileName);
                            fileparams.Add(FileFields.FileDescrip, tbxFileDescrip.Text);
                            fileparams.Add(FileFields.FileExt, _filedata.FileExt);
                            fileparams.Add(FileFields.FileSize, _filedata.FileBytes.ToString());
                            fileparams.Add(FileFields.FileDate, _filedata.FileDate.ToString());
                            fileparams.Add(FileFields.FileHash, _filedata.FileHash);
                            fileparams.Add(FileFields.FileVersion, _version);
                            fileparams.Add(FileFields.SegSize, _segSize.ToString());
                            fileparams.Add(FileFields.TotalSeg, _totalSeg.ToString());
                            fileparams.Add(FileFields.ShardName, shardname);

                            string filemethod = "File.Save.base";
                            if (_action == ReplicaAction.Insert) filemethod = "File.New.base";

                            string filemethList = msgUtil.CreateXMLMethod(filemethod, fileparams);

                            string filerespmsg = soapmsg.SOAPApiHelper(filemethList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                            if (filerespmsg != "")
                            {
                                RespInfo filerespinfo = new RespInfo();
                                Dictionary<string, ResInfo> filemethresults = msgUtil.ReadResponseString(filerespinfo, filerespmsg);

                                ResInfo defres = msgUtil.GetResult(filemethod + "_" + MethReturn.Default, filemethresults);
                                ResInfo msres = msgUtil.GetResult(filemethod + "_" + "RowMS", filemethresults);

                                sw.Stop();
                                totalMS += sw.ElapsedMilliseconds;

                                if (defres.RCode.ToUpper() == APIResult.OK)
                                {
                                    _rec_gid = defres.RVal;
                                    _filems = msres.RVal;
                                    procresult = APIResult.OK;
                                    procmsg = "<p class=\"success\">File Record created/updated: " + sw.ElapsedMilliseconds.ToString() + " MS</p>";

                                    sw.Restart();

                                    // parse file into segments and save to a file shard table
                                    bool segupload = true;
                                    for (int x = 1; x <= _totalSeg; x++)
                                    {
                                        byte[] tmparray = fileUtil.ReadFileSegment(tbxFilePath.Text, offset, _segSize);

                                        if (tmparray.Length > 0)
                                        {
                                            string segdata = Convert.ToBase64String(tmparray);

                                            Dictionary<string, string> segparams = new Dictionary<string, string>();
                                            segparams.Add(FolderFields.GroupGID, _folder.GroupGID);
                                            segparams.Add(FileFields.ShardName, shardname);
                                            segparams.Add(FileFields.FileGID, _rec_gid);
                                            segparams.Add(FileFields.FileVersion, _version);
                                            segparams.Add(FileFields.TotalSeg, _totalSeg.ToString());
                                            segparams.Add(FileFields.SegNum, x.ToString());
                                            segparams.Add(FileFields.SegSize, segdata.Length.ToString());
                                            segparams.Add(FileFields.SegData, segdata);

                                            string shardmethList = msgUtil.CreateXMLMethod("FileShard.New.base", segparams);
                                            int msgsize = shardmethList.Length;

                                            string shardrespmsg = soapmsg.SOAPApiHelper(shardmethList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                                            if (shardrespmsg != "")
                                            {
                                                RespInfo shardrespinfo = new RespInfo();
                                                Dictionary<string, ResInfo> shardmethresults = msgUtil.ReadResponseString(shardrespinfo, shardrespmsg);

                                                ResInfo shardres = msgUtil.GetResult("FileShard.New.base_DEFAULT", shardmethresults);

                                                if (shardres.RCode.ToUpper() == APIResult.OK)
                                                {
                                                    offset += _segSize;
                                                    decimal percent = ((decimal)x / (decimal)_totalSeg) * 100;
                                                    int progress = Convert.ToInt32(percent);
                                                    ProgressChanged(progress, "");
                                                }
                                                else
                                                {
                                                    // error message
                                                    sw.Stop();
                                                    procresult = APIResult.Error;
                                                    procmsg = "<p class=\"error\">File Segment Process Error: " + shardres.RCode + " : " + shardres.RVal + "</p>";
                                                    ProgressChanged(100, procmsg);
                                                    segupload = false;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                sw.Stop();
                                                procresult = APIResult.Error;
                                                procmsg = "<p class=\"error\">File Segment Process Error:  Creating a new FileShard record returned an empty response message.</p>";
                                                ProgressChanged(100, procmsg);
                                                segupload = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            // error message
                                            sw.Stop();
                                            procresult = APIResult.Error;
                                            procmsg = "<p class=\"error\">File Segment Process Error: the segment size was zero bytes</p>";
                                            ProgressChanged(100, procmsg);
                                            segupload = false;
                                            break;
                                        }
                                    }

                                    sw.Stop();
                                    totalMS += sw.ElapsedMilliseconds;

                                    // all file segments uploaded successfully
                                    if (segupload)
                                    {
                                        procmsg = "<p class=\"success\">" + _totalSeg.ToString() + " segments uploaded: " + sw.ElapsedMilliseconds.ToString() + " MS</p>";
                                        double KB = (_totalSeg * _segSize) / (double)1024;
                                        double Sec = sw.ElapsedMilliseconds / (double)1000;
                                        procmsg += "<p class=\"success\">" + Math.Round(KB / Sec, 2).ToString() + " KB/Sec</p>";
                                    }
                                    else
                                    {
                                        // rollback process by removing the parent file record
                                        Dictionary<string, string> removeparams = new Dictionary<string, string>();
                                        removeparams.Add(CommonFields.rec_gid, _rec_gid);
                                        removeparams.Add(FileFields.FileName, tbxFileName.Text);
                                        removeparams.Add(FileFields.FileDescrip, "File removed due to problems with the upload of file segments");
                                        removeparams.Add(CommonFields.row_ms, _filems);

                                        string removemethList = msgUtil.CreateXMLMethod("File.Remove.base", removeparams);
                                        string removerespmsg = soapmsg.SOAPApiHelper(methList, reqID, _main.UserName, _main.Password, _main.SvcUrl, _main.SvcPubKey);

                                        if (removerespmsg != "")
                                        {
                                            RespInfo removerespinfo = new RespInfo();
                                            Dictionary<string, ResInfo> removemethresults = msgUtil.ReadResponseString(removerespinfo, removerespmsg);

                                            ResInfo removeres = msgUtil.GetResult("File.Remove.base_DEFAULT", removemethresults);

                                            if (removeres.RCode.ToUpper() == APIResult.OK)
                                            {
                                                procresult = APIResult.Error;
                                                procmsg = "<p class=\"error\">File Record Error: File removed due to problems with the upload of file segments.</p>";
                                                ProgressChanged(100, procmsg);
                                            }
                                        }
                                        else
                                        {
                                            procresult = APIResult.Error;
                                            procmsg = "<p class=\"error\">File Record Error: Attempt to remove file due to problems with the upload of file segments failed.</p>";
                                            ProgressChanged(100, procmsg);
                                        }
                                    }

                                    ProgressChanged(100, procmsg);
                                    sw.Restart();
                                }
                                else
                                {
                                    procresult = APIResult.Error;
                                    procmsg = "<p class=\"error\">File Record Error: " + defres.RCode + " : " + defres.RVal + "</p>";
                                }
                            }
                            else
                            {
                                // filerespmsg
                                sw.Stop();
                                procresult = APIResult.Error;
                                procmsg = "<p class=\"error\">API Method Error: Method to create a file record returned an empty response message.</p>";
                                ProgressChanged(100, procmsg);
                            }

                            sw.Restart();
                        }
                        else
                        {
                            // error: no writable shardname returned
                            sw.Stop();
                            procresult = APIResult.Error;
                            procmsg = "<p class=\"error\">Writable ShardName Error: " + shardresult.RCode + " : " + shardresult.RVal + "</p>";
                            ProgressChanged(100, procmsg);
                        }
                    }
                    else
                    {
                        // empty respmsg
                        sw.Stop();
                        procresult = APIResult.Error;
                        procmsg = "<p class=\"error\">API Method Error: Query for ShardName returned an empty response message.</p>";
                        ProgressChanged(100, procmsg);
                    }
                }
                else
                {
                    // error message
                    sw.Stop();
                    procresult = APIResult.Error;
                    procmsg = "<p class=\"error\">File Upload Process Error: the file is larger than the maximum file size limit.</p>";
                    ProgressChanged(0, procmsg);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmFileUpload2.btnUpload_Click", ex);
                MessageBox.Show(ex.Message, "btnUpload_Click", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                sw.Stop();
                totalMS += sw.ElapsedMilliseconds;

                if (procresult == APIResult.OK)
                {
                    procmsg += "<p class=\"success\">Total Process Time: " + totalMS.ToString() + " MS</p>";
                }
                else
                {
                    procmsg = "<p class=\"error\">File Upload Process Error: " + procresult + "</p>";
                    RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmFileUpload2.btnUpload_Click", "File Upload Process Error", procmsg);
                }

                Cursor.Current = std;

                _main.SetMetrics(_totalSeg, sw.Elapsed.TotalMilliseconds, 0, "DGPDrive", "frmFileUpload2");

                ProcComplete(procresult, procmsg);
            }
        }

        private void ProgressChanged(int progressPercent, string progressState)
        {
            progressBar1.Value = progressPercent;
            progressBar1.Update();

            //_htmlResultBase += progressState;

            _htmlTempResult += "</div>" + _resultHTML.HTMLEnd();

            RefreshResults(_htmlResultBase + _htmlTempResult);
            Application.DoEvents();
        }

        private void ProcComplete(string procResult, string htmlResult)
        {
            progressBar1.Value = 100;
            progressBar1.Update();

            _htmlResultBase += htmlResult;

            _htmlTempResult += "</div>" + _resultHTML.HTMLEnd();

            RefreshResults(_htmlResultBase + _htmlTempResult);

            // update contents of file grid
            _parent.FileSearch();

            btnBrowse.Enabled = false;
            btnUpload.Enabled = false;
            btnCancel.Enabled = false;

            if (procResult == APIResult.OK && ckbxAutoClose.Checked) this.Close();
        }

        private void ClearForm()
        {
            tbxGlobalID.Text = "";
            tbxFileName.Text = "";
            tbxFileDescrip.Text = "";
            btnUpload.Enabled = false;

            _filedata = null;

            progressBar1.Value = 0;

            _htmlResultBase = _resultHTML.HTMLStart() + "<div class=\"titlediv\">Upload Process:</div><div class=\"innerdiv\">";
            ClearResults();
        }

        private void RefreshResults(string htmlResult)
        {
            brwsResults.DocumentText = "0";
            brwsResults.Document.OpenNew(true);
            brwsResults.Document.Write(htmlResult);
            brwsResults.Refresh();
        }

        private void ClearResults()
        {
            // clear results
            brwsResults.DocumentText = "0";
            brwsResults.Document.OpenNew(true);
            ResultHTML resultHTML = new ResultHTML();
            string resHTML = resultHTML.HTMLStart() + resultHTML.HTMLEnd();
            brwsResults.Document.Write(resHTML);
            brwsResults.Refresh();
        }

    }
}
