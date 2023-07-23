using System;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Net.Http;
using System.IO;
using System.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.Diagnostics;

using ApiUtil;
using ApiUtil.DataClasses;

namespace DGPDrive.Screens.FileStore
{
    public partial class frmFileDownload : Form
    {
        HttpClient _httpClient;
        MainForm _main;
        ucFileStore _parent;
        CommonOpenFileDialog locdialog;
        BackgroundWorker bw;
        ResultHTML _resultHTML;

        DateTime _fileDate;
        string _shardName;
        string _fileGID;
        string _fileVersion;
        string _fileName;
        string _fileExt;
        string _fileSize;
        string _fileHash;

        string _htmlResultBase;
        string _htmlTempResult;

        int _totalSeg;
        int _segSize;
        bool _segmatch;

        public frmFileDownload(HttpClient httpClient, MainForm mainForm, ucFileStore parentForm, string ShardName, string fileGID, string fileVersion, string fileName, string fileExt, DateTime fileDate, string fileSize, string fileHash, string segSize, string totalSeg)
        {
            InitializeComponent();

            try
            {
                _httpClient = httpClient;
                _main = mainForm;
                _parent = parentForm;

                _shardName = ShardName;
                _fileGID = fileGID;
                tbxGlobalID.Text = _fileGID;
                _fileName = fileName;
                tbxFileName.Text = _fileName;
                _fileExt = fileExt;
                _fileSize = fileSize;
                tbxFileSize.Text = _fileSize;
                _fileDate = fileDate;
                _fileHash = fileHash;
                _fileVersion = fileVersion;
                _segSize = Convert.ToInt32(segSize);
                _totalSeg = Convert.ToInt32(totalSeg);

                locdialog = new CommonOpenFileDialog();
                locdialog.Title = "Select File to Download";
                //locdialog.Filters.Add(new CommonFileDialogFilter("Content Files", "*.*"));
                locdialog.EnsurePathExists = true;
                locdialog.EnsureValidNames = true;
                locdialog.IsFolderPicker = true;
                locdialog.Multiselect = false;

                bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork2);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.WorkerReportsProgress = true;

                ClearResults();

                // compare segcount to file totalseg values
                int filesegcount = GetSegCount(ShardName, _fileGID, _fileVersion);

                if (_totalSeg != filesegcount)
                {
                    // missing file shard segments
                    MessageBox.Show("The number of file shard segments does not match the file record totalseg value", "frmFileDownload", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                // initialize HTML progress message
                _resultHTML = new ResultHTML();
                _htmlResultBase = _resultHTML.HTMLStart() + "<div class=\"titlediv\">Download Results</div><div class=\"innerdiv\">";
                _htmlResultBase += "<p class=\"success\">File Size: " + _fileSize + " Bytes</p>";
                _htmlTempResult += "</div>" + _resultHTML.HTMLEnd();

                RefreshResults(_htmlResultBase + _htmlTempResult);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmFileDownload", ex);
                MessageBox.Show(ex.Message, "frmFileDownload", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetSegCount(string fileShard, string fileGID, string fileVersion)
        {
            int segcount = 0;

            try
            {
                Dictionary<string, string> methparams = new Dictionary<string, string>();
                methparams.Add(FileFields.ShardName, fileShard);
                methparams.Add(FileFields.FileGID, fileGID);
                methparams.Add(FileFields.FileVersion, fileVersion);

                MsgUtil msgUtil = new MsgUtil();
                string reqID = msgUtil.GetNewGID();
                string methList = msgUtil.CreateXMLMethod("FileShard.GetSegCount.base", methparams);
                string respmsg = msgUtil.ApiMethHelper(methList,
                                                            reqID,
                                                            _main.UserName,
                                                            _main.Password,
                                                            _httpClient,
                                                            _main.SvcUrl);

                if (respmsg != "")
                {
                    RespInfo respinfo = new RespInfo();
                    Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                    ResInfo methresult = msgUtil.GetResult("FileShard.GetSegCount.base_DEFAULT", methresults);

                    if (methresult != null && methresult.RCode == APIResult.OK)
                    {
                        int.TryParse(methresult.RVal, out segcount);
                    }
                    else
                    {
                        RemoteErrLog.LogError(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.GetSegCount", "", "");
                        MessageBox.Show("Error querying for the count of file segment records", "ucFileStore.GetSegCount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No response message was returned", "Respons Message Error");
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "ucFileStore.GetSegCount", ex);
                MessageBox.Show(ex.Message, "ucFileStore.GetSegCount", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return segcount;
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                btnDownload.Enabled = false;

                if (_parent.DownloadDir != null && _parent.DownloadDir != "")
                {
                    locdialog.InitialDirectory = _parent.DownloadDir;
                }
                else
                {
                    locdialog.InitialDirectory = Environment.CurrentDirectory;
                }

                if (locdialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    tbxFilePath.Text = locdialog.FileName;
                    btnDownload.Enabled = true;
                    _parent.DownloadDir = Path.GetDirectoryName(locdialog.FileName);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmFileDownload.btnBrowse_Click", ex);
                MessageBox.Show(ex.Message, "frmFileDownload.btnBrowse_Click", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
                btnDownload.Enabled = false;
                btnBrowse.Enabled = false;
            }
        }

        /// <summary>
        /// downloads file segments and appends to local file as a background worker thread
        /// </summary>
        private void bw_DoWork2(object sender, DoWorkEventArgs e)
        {
            string procmsg = "";
            string procresult = "";
            string targetpath = "";
            BackgroundWorker worker = (BackgroundWorker)sender;
            long _totalMS = 0;
            bool _complete = false;

            try
            {
                targetpath = tbxFilePath.Text + "\\" + _fileName + _fileExt;

                // check if other copies of file already exist in the local target folder
                if (File.Exists(targetpath))
                {
                    string[] files = Directory.GetFiles(tbxFilePath.Text, _fileName + "*");

                    if (files.Length > 0)
                    {
                        int copynum = files.Length + 1;
                        targetpath = tbxFilePath.Text + "\\" + _fileName + "(" + copynum.ToString() + ")" + _fileExt;
                    }
                }

                
                FileUtil fileUtil = new FileUtil();
                Stopwatch sw = new Stopwatch();
                sw.Start();

                for (int i = 1; i <= _totalSeg; i++)
                {
                    // call API method to retrive the FileShard record using the FileGID and SegNum
                    Dictionary<string, string> dataparams = new Dictionary<string, string>();
                    dataparams.Add(FileFields.ShardName, _shardName);
                    dataparams.Add(FileFields.FileGID, _fileGID);
                    dataparams.Add(FileFields.FileVersion, _fileVersion);
                    dataparams.Add(FileFields.SegNum, i.ToString());

                    MsgUtil msgUtil = new MsgUtil();
                    string reqID = msgUtil.GetNewGID();
                    string methList = msgUtil.CreateXMLMethod("FileShard.GetDataBySegNum.base", dataparams);
                    string respmsg = msgUtil.ApiMethHelper(methList,
                                                                reqID,
                                                                _main.UserName,
                                                                _main.Password,
                                                                _httpClient,
                                                                _main.SvcUrl);

                    if (respmsg != "")
                    {
                        RespInfo respinfo = new RespInfo();
                        Dictionary<string, ResInfo> methresults = msgUtil.ReadResponseString(respinfo, respmsg);

                        ResInfo segrec = msgUtil.GetResult("FileShard.GetDataBySegNum.base_DEFAULT", methresults);

                        if (segrec.RCode == APIResult.OK)
                        {
                            // decode from Base64 and append the segment to the file
                            byte[] fileseg = Convert.FromBase64String(segrec.RVal);

                            // append download result to the local temp encrypted file
                            bool appended = fileUtil.AppendFileSegment(targetpath, fileseg);

                            if (appended)
                            {
                                decimal percent = ((decimal)i / (decimal)_totalSeg) * 100;
                                int progress = Convert.ToInt32(percent);
                                worker.ReportProgress(progress, "");

                                if (i == _totalSeg) _complete = true;
                            }
                            else
                            {
                                // error appending segment to the file
                                procresult = APIResult.Error;
                                procmsg = "<p class=\"error\">File Append Error: The " + i.ToString() + " file segment was not appended - process stopped.</p>";
                                File.Delete(targetpath);
                                break;
                            }
                        }
                        else
                        {
                            procresult = APIResult.Error;
                            procmsg = "<p class=\"error\">File Append Error: error with file segment API method: " + segrec.RCode + " : " + segrec.RVal + "</p>";
                            break;
                        }
                    }
                    else
                    {
                        procresult = APIResult.Error;
                        procmsg = "<p class=\"error\">Response Message Error: No response message was received from the API method call.</p>";
                        break;
                    }
                }

                sw.Stop();
                _totalMS += sw.ElapsedMilliseconds;

                if (_complete)
                {
                    procmsg = "<p class=\"success\">" + _totalSeg.ToString() + " segments downloaded: " + sw.ElapsedMilliseconds.ToString() + " MS</p>";
                    sw.Restart();

                    // calculate the hash value of the downloaded file and compare to the stored value
                    bool filematch = fileUtil.CheckFile(targetpath, _fileHash);
                    sw.Stop();

                    if (filematch)
                    {
                        // set local file date to match the file date of the original file
                        File.SetLastAccessTime(targetpath, _fileDate);
                        procresult = APIResult.OK;

                        _totalMS += sw.ElapsedMilliseconds;
                        procmsg += "<p class=\"success\">File hash verified: " + sw.ElapsedMilliseconds.ToString() + " MS</p>";
                        worker.ReportProgress(100, procmsg);
                    }
                    else
                    {
                        procresult = APIResult.Error;
                        procmsg += "<p class=\"error\">File Download Process Error: The file hash value of the download did not match the original file hash value.</p>";
                        File.Delete(targetpath);
                        worker.ReportProgress(100, procmsg);
                    }
                }
                else
                {
                    procresult = APIResult.Error;
                    procmsg += "<p class=\"error\">File Download Process Error: The file download was incomplete - partial local file deleted.</p>";
                    File.Delete(targetpath);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.LogException(_main.UserName, _main.Password, _main.SvcUrl, "DGP DGP", "frmFileDownload.btnDownload_Click", ex);
                MessageBox.Show(ex.Message, "frmFileDownload.btnDownload_Click", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                File.Delete(targetpath);
            }

            e.Result = procresult + "|" + targetpath + "|" + procmsg;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBar1.Update();

            string progmsg = e.UserState.ToString();
            _htmlResultBase += progmsg;

            _htmlTempResult += "</div>" + _resultHTML.HTMLEnd();

            RefreshResults(_htmlResultBase + _htmlTempResult);
            Application.DoEvents();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            progressBar1.Update();

            string[] results = e.Result.ToString().Split('|');

            btnDownload.Enabled = true;
            if (results[0] == APIResult.OK)
            {
                if (ckbOpen.Checked)
                {
                    // open downloaded file
                    try
                    {
                        Process.Start(results[1]);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show(ex.Message + ": Do you want to select an application?", "File Open Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var processInfo = new ProcessStartInfo(results[1]);
                            processInfo.Verb = "openas";
                            Process.Start(processInfo);
                        }
                    }
                }
            }
        }

        private void RefreshResults(string htmlResult)
        {
            brwsResults.DocumentText = "0";
            brwsResults.Document.OpenNew(true);
            brwsResults.Document.Write(htmlResult);
            brwsResults.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearResults()
        {
            progressBar1.Value = 0;
            btnDownload.Enabled = false;

            // clear results
            brwsResults.DocumentText = "0";
            brwsResults.Document.OpenNew(true);
            ResultHTML resultHTML = new ResultHTML();
            string resHTML = resultHTML.HTMLStart() + resultHTML.HTMLEnd();
            brwsResults.Document.Write(resHTML);
            brwsResults.Refresh();
        }


        private void tbxFileName_TextChanged(object sender, EventArgs e)
        {
            _fileName = tbxFileName.Text;
        }

    }
}
