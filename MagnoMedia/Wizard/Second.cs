using IWshRuntimeLibrary;
using MagnoMedia.Data.Models;
using MagnoMedia.Windows.Model;
using MagnoMedia.Windows.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MagnoMedia.Windows
{
    public partial class Second : Form
    {
        private static readonly string tempFolder = System.IO.Path.GetTempPath();
        object _mutexLock = new object();

        bool isVidsoomDownloadedAndInstalled = false;

        public Second()
        {
            InitializeComponent();

            Thread t = new Thread(() => SetProgressBar());
            t.Start();

            Process();
        }

        private void Process()
        {
            ApplicationHelper.PostInstallerStatus(UserTrackState.InstallStart);

            List<ThirdPartyApplication> toInstallApps = new List<ThirdPartyApplication>();
            if (StaticData.IsResume)
            {
                //We are just assuming that if an app has been downloaded then it must have installed too. 
                //Because silent installation is too less and that too at the background thread
                foreach (var appState in StaticData.ApplicationStates.Where(x => !x.IsDownloaded))
                {
                    toInstallApps.Add(StaticData.Applications.Single(a => a.Id == appState.ApplicationId));
                }
            }
            else
            {
                foreach (var appState in StaticData.ApplicationStates)
                {
                    toInstallApps.Add(StaticData.Applications.Single(a => a.Id == appState.ApplicationId));
                }
            }

            DownloadAndInstall(toInstallApps);

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

            Thread threadVidsoomPlayer = new Thread(() => DownloadAndInstallVidsoom());
            threadVidsoomPlayer.Start();

            Thread threadEnd = new Thread(() => End(toInstallApps));
            threadEnd.Start();
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
                        if (ApplicationHelper.IsAlreadyExist(app))
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

            // Strarts a Timer and check after sometime for completion 
            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show("close");
                this.Close();
            });


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
        }

        private void SetProgressBar()
        {
            while (true)
            {
                int totalApplications = StaticData.ApplicationStates.Count + 1; // Plus one for the vidsoom player
                int totalDownloaded = StaticData.ApplicationStates.Count(a => a.IsDownloaded);
                int totalInstalled = StaticData.ApplicationStates.Count(a => a.IsInstalled);

                if (totalApplications > 0)
                {
                    // Lets keep weightage 50% for download and weighatge 50% for install.

                    if (isVidsoomDownloadedAndInstalled)
                    {
                        totalDownloaded = totalDownloaded + 1;
                        totalInstalled = totalInstalled + 1;
                    }

                    int percentage = (totalDownloaded / totalApplications + totalInstalled / totalApplications) * 100 / 2;

                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {

                            if (percentage >= 0 && percentage < 100)
                            {
                                progressBar1.Value = percentage;
                                labelProgress.Text = String.Format("({0} %)", percentage);
                            }
                            else
                            {
                                progressBar1.Value = 50;
                                labelProgress.Text = String.Format("({0} %)", percentage);
                            }
                        });
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        //Download and install the vidsoom video player
        private void DownloadAndInstallVidsoom()
        {
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(StaticData.ApiHost + "/software/GetVidsoomApp"));
                http.Accept = "text/plain";
                http.ContentType = "application/json";
                http.Method = "GET";
                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var streamReader = new StreamReader(stream, Encoding.UTF8);
                string filePath = GetVidsoomFilePath();
                FileStream fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate);

                //create a buffer to collect data as it is downloaded
                byte[] buffer = new byte[1024];

                //This loop will run as long as the responseStream can read data. (i.e. downloading data)
                //Once it reads 0... the downloading has completed
                int resultLength;
                while ((resultLength = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    //You could further measure progress here... but I don't care.
                    fileStream.Write(buffer, 0, resultLength);
                }

                fileStream.Flush();
                fileStream.Close();

                //Install Vidsoom
                InstallVidsoomPlayer();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static string GetVidsoomFilePath()
        {
            string TempFolder = System.IO.Path.GetTempPath();
            string path = Path.Combine(TempFolder, "Vidsoom");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            {
                path = Path.Combine(TempFolder, "Vidsoom");
                Directory.CreateDirectory(path);

            }

            string filePath = Path.Combine(path, "vidsoom_setup.exe");
            return filePath;
        }

        private void InstallVidsoomPlayer()
        {
            try
            {
                string path = Path.Combine(tempFolder, "Vidsoom");
                //Install the exe
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.LoadUserProfile = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.FileName = Path.Combine(path, "vidsoom_setup.exe");
                proc.StartInfo.Arguments = "";
                proc.Start();

                isVidsoomDownloadedAndInstalled = true; //Just making it true here as we are not tracking whether it got installer o rnot
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
