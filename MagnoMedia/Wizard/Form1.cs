﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagnoMedia.Windows.Utilities;
using Magno.Data;
using System.Threading;

namespace MagnoMedia.Windows
{
    public partial class Form1 : Form
    {

       public static IEnumerable<ThirdPartyApplication> SWList;
        static string TempFolder;
        public Form1()
        {
            InitializeComponent();
            //LoadSoftwareList();
            Thread threadBackground = new Thread(() => LoadSoftwareList());
            threadBackground.Start();
        }

        private void LoadSoftwareList()
        {
            string currentText = "Checking System Requirements";
            SetCurrentText(currentText);
            string machineUniqueIdentifier = MachineHelper.UniqueIdentifierValue();
            string osName = MachineHelper.GetOSName();
            string defaultBrowser = MachineHelper.GetDefaultBrowserName();
            string ipAddress = MachineHelper.GetIpAddress();
            string countryName = MachineHelper.GetCountryName();
            SWList = OtherSoftwareHelper.GetAllApplicableSoftWare(MachineUID: machineUniqueIdentifier, OSName: osName, DefaultBrowser: defaultBrowser, IPAddress: ipAddress, CountryName: countryName);

            currentText  = "Analyzing Components...";
            SetCurrentText(currentText);
            foreach (ThirdPartyApplication sw in SWList)
            {
                
                //check Registory if already exist in m/c


            }

            currentText = "Initializing Setup Wizard";
            SetCurrentText(currentText);
            //Make agreement screent for each 3rdparty s/w
            GotoHomeScreen();
        }

        private void SetCurrentText(string currentText)
        {
            
            this.Invoke((MethodInvoker)delegate
            {
                labelInitialStep.Text = currentText;
            });
        }





        private void GotoHomeScreen()
        {
            
            this.Invoke((MethodInvoker)delegate
            {
                First HomeScreen = new First();
                HomeScreen.Show();
                this.Hide();
            });
        }

        void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {

            DownLoadSoftWares();
        }

        private void DownLoadSoftWares()
        {

            List<ThirdPartyApplication> toInstall = new List<ThirdPartyApplication>();
            toInstall = SWList.ToList();
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
                        File.Delete(filePath);
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


            try
            {
                ThirdPartyApplication downloadedSW = e.UserState as ThirdPartyApplication;
                if (downloadedSW != null)
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.LoadUserProfile = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.FileName = Path.Combine(TempFolder, downloadedSW.Name, downloadedSW.InstallerName);
                    proc.StartInfo.Arguments = downloadedSW.Arguments;
                    proc.Start();
                }
            }
            catch (Exception)
            {
                // Log Error
            }
            finally
            {
                // Send log to database s/w name installed success/error
            }
        }







    }
}
