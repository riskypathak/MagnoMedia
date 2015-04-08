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
using Wizard.Model;
using Wizard.Utilities;

namespace Wizard
{
    public partial class Form1 : Form
    {

        static IEnumerable<Othersoftware> SWList;
        static string TempFolder;
        public Form1()
        {
            InitializeComponent();
            LoadSoftwareList();
        }

        private void LoadSoftwareList()
        {
            progressBarInstall.Visible = false;
            // This will show all softwares that will installed in background
            flowLayoutPanelSoftwareList.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            SWList = OtherSoftwareHelper.GetAllApplicableSoftWare();
            foreach (Othersoftware sw in SWList)
            {
                // add linklabel to container
                LinkLabel otherSWlabel = new LinkLabel();
                otherSWlabel.Text = sw.Name;
                if (sw.HasUrl)
                {
                    otherSWlabel.Links.Add(0, sw.Name.Length, sw.Url);
                    otherSWlabel.LinkClicked += linkLabel_LinkClicked;
                }
                else
                {
                    otherSWlabel.Enabled = false;
                }
                //Todo arrangement of links in UI
                flowLayoutPanelSoftwareList.Controls.Add(otherSWlabel);
            }
        }

        void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            progressBarInstall.Visible = true;
            progressBarInstall.Value = 40;
            panelInstallCancel.Visible = false;
            DownLoadSoftWares();
        }

        private void DownLoadSoftWares()
        {
            TempFolder = System.IO.Path.GetTempPath();
            foreach (Othersoftware sw in SWList)
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
                Othersoftware downloadedSW = e.UserState as Othersoftware;
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
