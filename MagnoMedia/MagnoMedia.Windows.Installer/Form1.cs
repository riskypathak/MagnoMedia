﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MagnoMedia.Windows.Installer
{
    public partial class Form1 : Form
    {
        //private const string SESSION_ID = "#SESSIONID#";
        //private const string HOST_ADDRESS = "";

        //risky
        private const string SESSION_ID = "99b0618d-d635-4071-8e42-8cebb453602f";
        private const string HOST_ADDRESS = "http://localhost:4387/api";

        public Form1()
        {
            try
            {
                InitializeComponent();
                GetApplicationDetails();
            }
            catch
            {
                // Silently Kill Application
                this.Close();
            }
            finally
            {
            }
        }

        private void GetApplicationDetails()
        {
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
            string countryName = MachineHelper.GetCountryName();

            var http = (HttpWebRequest)WebRequest.Create(new Uri(HOST_ADDRESS + "/software/applicationpath"));
            http.Accept = "text/plain";
            http.ContentType = "application/json";
            http.Method = "POST";
            //SessionID

            string parsedContent = "{\"MachineUID\":\"" + machineUniqueIdentifier + "\" , \"OSName\":\"" + osName + "\", \"DefaultBrowser\":\"" + defaultBrowser + "\" ,\"countryName\":\"" + countryName + "\",\"SessionID\":\"" + SESSION_ID + "\" }";
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream, Encoding.UTF8);
            var content = sr.ReadToEnd();


            string childExePath = content;
            DownLoadRunnableApplication(childExePath);


        }

        private void DownLoadRunnableApplication(string childExePath)
        {
            string TempFolder = System.IO.Path.GetTempPath();

            string path = Path.Combine(TempFolder, SESSION_ID);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            {
                //string[] filePaths = Directory.GetFiles(path);
                //foreach (string filePath in filePaths)
                //    File.Delete(filePath);
                //DirectoryInfo dir = new DirectoryInfo(path);
                //foreach (DirectoryInfo subdir in dir.GetDirectories())
                //{
                //    subdir.Delete(true);
                //}
                path = Path.Combine(TempFolder, SESSION_ID, Guid.NewGuid().ToString());
                Directory.CreateDirectory(path);

            }
            string FilePath = Path.Combine(path, "Vidsoom.zip");
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFileCompleted += myWebClient_DownloadFileCompleted;
            //TODO Check for \" in response
            childExePath = childExePath.Replace("\\", "").Replace("\"", "");

            

            myWebClient.DownloadFileAsync(new Uri(childExePath, UriKind.RelativeOrAbsolute), FilePath, path);
        }

        private void myWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string ZipDirectoryPath = e.UserState as string;
            if (ZipDirectoryPath != null && e.Error == null)
            {
                string FilePath = Path.Combine(ZipDirectoryPath, "Vidsoom.zip");
                //Unzip and run exe
                ZipFile.ExtractToDirectory(FilePath, ZipDirectoryPath);// for 4.5 and above
                //Code for 3.5
                //https://msdn.microsoft.com/en-us/library/ms404280(v=vs.90).aspx

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = Path.Combine(ZipDirectoryPath, "Debug", "MagnoMedia.Windows.exe");

                //risky


                proc.StartInfo.Arguments = SESSION_ID;
                proc.Start();
                CloseSelf();
            }
        }

        private void CloseSelf()
        {
            if (this.InvokeRequired)
            {

                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });

            }
            else
            {
                this.Close();

            }
        }

        private void SetCurrentText(string currentText)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    labelInitialStep.Text = currentText;
                });
            }
            else
            {
                labelInitialStep.Text = currentText;
            }
        }
    }
}
