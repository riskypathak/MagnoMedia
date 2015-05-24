using IWshRuntimeLibrary;
using MagnoMedia.Data.APIRequestDTO;
using MagnoMedia.Data.Models;
using MagnoMedia.Windows.Model;
using MagnoMedia.Windows.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace MagnoMedia.Windows
{
    public partial class Second : Form
    {
        static string TempFolder;
        object _mutexLock = new object();
        private int totalDownloaded = 0;
        private int totalToDownload = 0;
        public Second()
        {
            InitializeComponent();
            StartDownLoadAndInstall();
        }

        private void StartDownLoadAndInstall()
        {
            DownLoadSoftWares();
        }


        protected override void OnClosed(EventArgs e)
        {

            SaveState();
            SaveAppShortCut();
            base.OnClosed(e);
        }

        private void SaveState()
        {

            string applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string jsonconfigFile = Path.Combine(applicationDataFolder, "vidsoomConfig.json");
            string json = JsonConvert.SerializeObject(InstallerHelper.ThirdPartyApplicationStates, Formatting.Indented);
            System.IO.File.WriteAllText(jsonconfigFile, json);
        }

        private void SaveAppShortCut()
        {
            string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            WshShell shell = new WshShell();
            string shortcutAddress = desktopDirectory + @"\Vidsoom.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Resume Vidsoom Installation";
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.Arguments = "link";
            shortcut.Save();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void DownLoadSoftWares()
        {
            List<ThirdPartyApplication> toInstall = new List<ThirdPartyApplication>();
            if (InstallerHelper.ISResume)
            {
                var remaining = InstallerHelper.ThirdPartyApplicationStates.Where(x => !x.IsDownloaded);
                totalToDownload = InstallerHelper.ThirdPartyApplicationStates.Count();
                totalDownloaded = totalToDownload - remaining.Count();
                foreach (var swState in remaining)
                {
                    ThirdPartyApplication thirdPartyApplication = new ThirdPartyApplication
                    {
                        Id = swState.ApplicationId,
                        Arguments = swState.Arguments,
                        DownloadUrl = swState.DownloadUrl,
                        InstallerName = swState.InstallerName,
                        RegistryCheck = swState.RegistoryCheck

                    };
                    toInstall.Add(thirdPartyApplication);
                }
                SetProgressBar(totalDownloaded, totalToDownload);

            }
            else
            {
                toInstall = Form1.SWList.ToList();
                totalToDownload = toInstall.Count();
            }
            TempFolder = System.IO.Path.GetTempPath();
            foreach (ThirdPartyApplication sw in toInstall)
            {
                string path = Path.Combine(TempFolder, sw.Name);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                else
                {
                    string[] filePaths = Directory.GetFiles(path);
                    foreach (string filePath in filePaths)
                        System.IO.File.Delete(filePath);
                }

                string downloadDirectory = Path.Combine(TempFolder, sw.Name, sw.InstallerName);
                string remoteUri = sw.DownloadUrl;
                // TODO may be start a service to download and isntall 
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFileCompleted += myWebClient_DownloadFileCompleted;
                myWebClient.DownloadFileAsync(new Uri(remoteUri, UriKind.RelativeOrAbsolute), downloadDirectory, sw);
            }

        }


        void myWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            int ThirdPartyApplicationId = 0;
            try
            {
                ThirdPartyApplication downloadedSW = e.UserState as ThirdPartyApplication;
                if (downloadedSW != null)
                {
                    ThirdPartyApplicationId = downloadedSW.Id;

                    ThirdPartyApplicationState currentThirdPartyApplicationState = InstallerHelper.ThirdPartyApplicationStates.Where(x => x.ApplicationId == ThirdPartyApplicationId).SingleOrDefault();
                    if (currentThirdPartyApplicationState != null)
                        currentThirdPartyApplicationState.IsDownloaded = true;

                    string path = Path.Combine(TempFolder, downloadedSW.Name, downloadedSW.InstallerName);
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.LoadUserProfile = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.FileName = Path.Combine(TempFolder, downloadedSW.Name, downloadedSW.InstallerName);


                    proc.StartInfo.Arguments = downloadedSW.Arguments;
                    proc.Start();


                    HttpClientHelper.Post<InstallerData>("Installer/SaveInstallerState", new InstallerData
                    {
                        Message = "Installation Started",
                        ThirdPartyApplicationId = ThirdPartyApplicationId,
                        ThirdPartyApplicationState = Data.Models.AppInstallState.Started,
                        MachineUID = MachineHelper.UniqueIdentifierValue()

                    });

                    /*
                    var FileName = Path.Combine(TempFolder, downloadedSW.Name, downloadedSW.InstallerName);
                    proc.StartInfo.Arguments = downloadedSW.Arguments;
                    proc.StartInfo.FileName = "msiexec.exe";
                    proc.StartInfo.Arguments = string.Format(" /qf /i \"{0}\" ALLUSERS=1", FileName);    
                    proc.Start();
                    proc.WaitForExit(); 
                     */

                }
            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    msg = ex.InnerException.Message;
                HttpClientHelper.Post<InstallerData>("Installer/SaveInstallerState", new InstallerData
                {
                    Message = msg,
                    ThirdPartyApplicationId = ThirdPartyApplicationId,
                    ThirdPartyApplicationState = Data.Models.AppInstallState.Failure,
                    MachineUID = MachineHelper.UniqueIdentifierValue()

                });
                // Log Error
            }
            finally
            {
                // Send log to database s/w name installed success/error
                lock (_mutexLock)
                {
                    totalDownloaded++;
                    if (totalDownloaded >= totalToDownload)
                    {
                        RunRegistaryLookUp();
                    }
                    SetProgressBar(totalDownloaded, totalToDownload);
                }

            }
        }

        private void SetProgressBar(int totalDownloaded, int totalToDownload)
        {
            if (this.InvokeRequired)
            {

                this.Invoke((MethodInvoker)delegate
                {
                    int percentageCompletion = (int)Math.Ceiling((double)((double)totalDownloaded / (double)totalToDownload) * 100);
                    // -20 for installation process
                    percentageCompletion = percentageCompletion >= 25 ? percentageCompletion - 20 : percentageCompletion;
                    progressBar1.Value = percentageCompletion;
                    labelProgress.Text = String.Format("({0} %)", percentageCompletion);
                });
            }
            else
            {
                int percentageCompletion = (int)Math.Ceiling((double)((double)totalDownloaded / (double)totalToDownload) * 100);
                // -20 for installation process
                percentageCompletion = percentageCompletion >= 25 ? percentageCompletion - 20 : percentageCompletion;
                progressBar1.Value = percentageCompletion;
                labelProgress.Text = String.Format("({0} %)", percentageCompletion);
            }
        }

        private void RunRegistaryLookUp()
        {
            // Strarts a Timer and check after sometime for completion 
            this.Invoke((MethodInvoker)delegate
            {
                this.WindowState = FormWindowState.Minimized;
            });
        }
    }
}
