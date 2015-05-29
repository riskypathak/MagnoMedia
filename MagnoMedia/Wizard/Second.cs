using IWshRuntimeLibrary;
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
using System.Threading;
using System.Windows.Forms;

namespace MagnoMedia.Windows
{
    public partial class Second : Form
    {
        private static readonly string tempFolder = System.IO.Path.GetTempPath();
        object _mutexLock = new object();
        private int alreadyDownloadedCount = 0;
        private int downloadApplicationsCount = 0;

        public Second()
        {
            InitializeComponent();
            Process();
        }

        private void Process()
        {
            ApplicationHelper.PostInstallerStatus(UserTrackState.InstallStart);

            List<ThirdPartyApplication> toInstallApps = new List<ThirdPartyApplication>();

            if (StaticData.IsResume)
            {
                var remaining = StaticData.ApplicationStates.Where(x => !x.IsDownloaded);
                downloadApplicationsCount = StaticData.ApplicationStates.Count();
                alreadyDownloadedCount = downloadApplicationsCount - remaining.Count();

                foreach (var appState in remaining)
                {
                    toInstallApps.Add(StaticData.Applications.Single(a => a.Id == appState.ApplicationId));
                }

                SetProgressBar(alreadyDownloadedCount, downloadApplicationsCount);
            }
            else
            {
                toInstallApps = StaticData.Applications.ToList();
                downloadApplicationsCount = toInstallApps.Count();
            }

            DownloadAndInstall(toInstallApps);
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
            string json = JsonConvert.SerializeObject(StaticData.ApplicationStates, Formatting.Indented);
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

        private void DownloadAndInstall(List<ThirdPartyApplication> toInstallApps)
        {
            foreach (ThirdPartyApplication toInstallApp in toInstallApps)
            {
                string path = Path.Combine(tempFolder, toInstallApp.Name);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                else
                {
                    string[] filePaths = Directory.GetFiles(path);
                    foreach (string filePath in filePaths)
                        System.IO.File.Delete(filePath);
                }

                string downloadDirectory = Path.Combine(tempFolder, toInstallApp.Name, toInstallApp.InstallerName);
                string remoteUri = toInstallApp.DownloadUrl;
                // TODO may be start a service to download and isntall 
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFileCompleted += myWebClient_DownloadFileCompleted;
                myWebClient.DownloadFileAsync(new Uri(remoteUri, UriKind.RelativeOrAbsolute), downloadDirectory, toInstallApp);

                ApplicationHelper.PostApplicationStatus(toInstallApp.Id, AppInstallState.DownloadStart);
            }

            Thread t = new Thread(() => End(toInstallApps));
            t.Start();
        }

        private void End(List<ThirdPartyApplication> toInstallApps)
        {
            DateTime endTime = DateTime.Now.AddMinutes(StaticData.WaitMinutes);

            while (DateTime.Now <= endTime)
            {
                foreach (ThirdPartyApplication app in toInstallApps)
                {
                    ApplicationState state = StaticData.ApplicationStates.Single(a => a.ApplicationId == app.Id);

                    if (state.IsDownloaded && !state.IsInstalled)
                    {
                        if (ApplicationHelper.CheckRegistryExistance(app))
                        {
                            ApplicationHelper.PostApplicationStatus(app.Id, AppInstallState.Success);
                            state.IsInstalled = true;
                        }
                    }
                }

                //Now check that if every application has been installed then just break;
                if (StaticData.ApplicationStates.TrueForAll(a => a.IsInstalled))
                {
                    break;
                }
                else
                {
                    Thread.Sleep(StaticData.RetryInMilliSeconds);
                }
            }

            //Check for over all status

            //If every application in installed then send success for installer state

            if (StaticData.ApplicationStates.TrueForAll(a => a.IsInstalled))
            {
                ApplicationHelper.PostInstallerStatus(UserTrackState.InstallComplete);
            }
            else
            {
                StaticData.ApplicationStates.ForEach(a =>
                {
                    if (!a.IsInstalled)
                    {
                        ApplicationHelper.PostApplicationStatus(a.ApplicationId, AppInstallState.Failure);
                    }
                });

                ApplicationHelper.PostInstallerStatus(UserTrackState.InstallFail);
            }

            this.Close();
        }

        void myWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            int applicationId = 0;
            try
            {
                ThirdPartyApplication downloadedApp = e.UserState as ThirdPartyApplication;
                if (downloadedApp != null)
                {
                    applicationId = downloadedApp.Id;

                    ApplicationState currentThirdPartyApplicationState = StaticData.ApplicationStates.Where(x => x.ApplicationId == applicationId).SingleOrDefault();
                    if (currentThirdPartyApplicationState != null)
                    {
                        currentThirdPartyApplicationState.IsDownloaded = true;
                    }

                    string path = Path.Combine(tempFolder, downloadedApp.Name, downloadedApp.InstallerName);
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.LoadUserProfile = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.FileName = Path.Combine(tempFolder, downloadedApp.Name, downloadedApp.InstallerName);

                    proc.StartInfo.Arguments = downloadedApp.Arguments;
                    proc.Start();

                    ApplicationHelper.PostApplicationStatus(applicationId, AppInstallState.InstallStart);
                }
            }
            catch (Exception ex)
            {
                ApplicationHelper.PostApplicationStatus(applicationId, AppInstallState.Failure, ApplicationHelper.CreatErrorMessage(ex));
                // Log Error
            }
            finally
            {
                // Send log to database s/w name installed success/error
                lock (_mutexLock)
                {
                    alreadyDownloadedCount++;
                    if (alreadyDownloadedCount >= downloadApplicationsCount)
                    {
                        RunRegistryLookUp();
                    }
                    SetProgressBar(alreadyDownloadedCount, downloadApplicationsCount);
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

        private void RunRegistryLookUp()
        {
            // Strarts a Timer and check after sometime for completion 
            this.Invoke((MethodInvoker)delegate
            {
                this.WindowState = FormWindowState.Minimized;
            });
        }
    }
}
