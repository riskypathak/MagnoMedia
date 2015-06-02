using System;
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
        private const string SESSION_ID = "249077e1-ff45-4d00-93a2-f2dff7c944a0";
        //private const string HOST_ADDRESS = "";

        //risky
        //private const string SESSION_ID = "99b0618d-d635-4071-8e42-8cebb453602f";
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
            var streamReader = new StreamReader(stream, Encoding.UTF8);

            string filePath = GetFilePath();

            FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate);

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

            ExtractAndStart(filePath);
        }

        private static string GetFilePath()
        {
            string TempFolder = System.IO.Path.GetTempPath();
            string path = Path.Combine(TempFolder, SESSION_ID);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            {
                path = Path.Combine(TempFolder, SESSION_ID, Guid.NewGuid().ToString());
                Directory.CreateDirectory(path);

            }

            string filePath = Path.Combine(path, "Vidsoom.zip");
            return filePath;
        }

        private void ExtractAndStart(string filePath)
        {
            string zipDirectoryPath = Path.GetDirectoryName(filePath);

            //Unzip and run exe
            ZipFile.ExtractToDirectory(filePath, zipDirectoryPath);// for 4.5 and above
            //Code for 3.5
            //https://msdn.microsoft.com/en-us/library/ms404280(v=vs.90).aspx

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Path.Combine(zipDirectoryPath, "MagnoMedia.Windows.exe");

            proc.StartInfo.Arguments = SESSION_ID;
            proc.Start();

            CloseSelf();
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
