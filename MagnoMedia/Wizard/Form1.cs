using Microsoft.Win32;
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

namespace MagnoMedia.Windows
{
    public partial class Form1 : Form
    {

        static IEnumerable<ThirdPartyApplication> SWList;
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
            flowLayoutPanelSoftwareList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanelSoftwareList.AutoScroll = true;
            flowLayoutPanelSoftwareList.WrapContents = false;
            string machineUniqueIdentifier = MachineHelper.UniqueIdentifierValue();
            string osName = MachineHelper.GetOSName();
            string defaultBrowser = MachineHelper.GetDefaultBrowserName();


            SWList = OtherSoftwareHelper.GetAllApplicableSoftWare(MachineUID: machineUniqueIdentifier,OSName:osName,DefaultBrowser:defaultBrowser);


            ((ListBox)checkedListBoxSW).DataSource = SWList;
            ((ListBox)checkedListBoxSW).DisplayMember = "Name";
            ((ListBox)checkedListBoxSW).ValueMember = "Id";
            for (int i = 0; i < checkedListBoxSW.Items.Count; i++)
            {
                
                checkedListBoxSW.SetItemChecked(i, true);
            }


            foreach (ThirdPartyApplication sw in SWList)
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
           
            List<ThirdPartyApplication> toInstall = new List<ThirdPartyApplication>();
            if (checkedListBoxSW.Visible) { 
            //get checked itmes //TODO will allow that or not ?
                foreach (object itemChecked in checkedListBoxSW.CheckedItems)
                {
                    ThirdPartyApplication thirdPartyApp = itemChecked as ThirdPartyApplication;
                    if (thirdPartyApp != null)
                        toInstall.Add(thirdPartyApp);
                
                }


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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCustomize_Click(object sender, EventArgs e)
        {
            checkedListBoxSW.Visible = true;
            buttonCustomize.Visible = false;

        }

       

    }
}
