using System;
using System.ComponentModel;
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
        private const string SESSION_ID = "#SESSIONID#";

        public Form1()
        {
            try
            {
                InitializeComponent();
                GetApplicationDetails();
            } 
            catch{
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

            //OtherSoftwareHelper.GetAllApplicableSoftWare(MachineUID: machineUniqueIdentifier, OSName: osName, DefaultBrowser: defaultBrowser, CountryName: countryName);
            var http = (HttpWebRequest)WebRequest.Create(new Uri("http://localhost:44227/api/software/applicationpath"));
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
                 path = Path.Combine(TempFolder, SESSION_ID,Guid.NewGuid().ToString());
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


                proc.StartInfo.Arguments = "sessionid:1234567";
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

        //public static void Decompress(FileInfo fi)
        //{
        //    // Get the stream of the source file. 
        //    using (FileStream inFile = fi.OpenRead())
        //    {
        //        // Get original file extension, for example "doc" from report.doc.gz.
        //        string curFile = fi.FullName;
        //        string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

        //        //Create the decompressed file. 
        //        using (FileStream outFile = File.Create(origName))
        //        {
        //            using (GZipStream Decompress = new GZipStream(inFile,
        //                    CompressionMode.Decompress))
        //            {
        //                //Copy the decompression stream into the output file. 
        //                byte[] buffer = new byte[4096];
        //                int numRead;
        //                while ((numRead = Decompress.Read(buffer, 0, buffer.Length)) != 0)
        //                {
        //                    outFile.Write(buffer, 0, numRead);
        //                }
        //                Console.WriteLine("Decompressed: {0}", fi.Name);

        //            }
        //        }
        //    }
        //}

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
