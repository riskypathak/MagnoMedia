using MagnoMedia.Data.Models;
using MagnoMedia.Web.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MagnoMedia.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Insert into SessionDetails
            SessionDetail session = new SessionDetail();
            session.SessionCode = Guid.NewGuid().ToString();
            session.CompleteRequestUri = Request.Url.ToString();
            session.RefereralUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : null;
            session.RequestDate = DateTime.Now;
            session.UserAgent = Request.UserAgent != null ? Request.UserAgent.ToString() : null;
            session.IPAddress = Request.UserHostAddress;

            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            int refererId = 0;

            if (string.IsNullOrEmpty(session.RefereralUrl)) //We have to also add here condition to get referer id from url
            {
                using (IDbConnection db = dbFactory.Open())
                {
                    refererId = db.Select<Referer>().Single(r => r.RefererCode == "NOREFER").Id;
                }
            }
            else
            {

            }

            session.RefererId = refererId;
            long sessionId = InsertInDB<SessionDetail>(dbFactory, session);

            //Insert into tracking
            UserTrack userTrack = new UserTrack();
            userTrack.UpdatedDate = DateTime.Now;
            userTrack.SessionDetailId = Convert.ToInt32(sessionId);
            userTrack.State = UserTrackState.LandingPage;
            InsertInDB<UserTrack>(dbFactory, userTrack);

            DownloadData _downloaddata = new DownloadData { SessionId = session.SessionCode };
            _downloaddata.DownloadLink = String.Format("home/download/{0}‏", session.SessionCode);
            //Embedd this SessionId in download/install link   on index page
            return View(_downloaddata);
        }

        private static long InsertInDB<T>(IDbConnectionFactory dbFactory, T data)
        {
            using (IDbConnection db = dbFactory.Open())
            {
                return db.Insert<T>(data, selectIdentity: true);
            }
        }

        //This should be hit when url is http://<rootaddress>/download/<sessionid>
        public FileResult Download(string id)
        {
            string sessionCode = id;

            //Redirect to Landing Page because we assume that user has not been come directly via LP
            if (string.IsNullOrEmpty(sessionCode))
            {
                Redirect("/index");
                //Redirect to Index(LP)
            }

            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
            string downloadFile = string.Empty;
            using (IDbConnection db = dbFactory.Open())
            {
                //Check here if session already exist in database table.

                sessionCode = sessionCode.Substring(0, 36);// For bug appending ?(special character). Guid is of 36 length
                SessionDetail lastSession = db.Select<SessionDetail>(s => s.SessionCode == sessionCode).FirstOrDefault();


                if (lastSession == null)
                {
                    Redirect("/index");
                }
                else
                {
                    //Create a folder here.

                    string downloadFolderPath = Server.MapPath(string.Format("~/Temp//{0}", sessionCode));
                    System.IO.Directory.CreateDirectory(downloadFolderPath);

                    //Transfer all files from a static folder(//AppData/Application) to above created folder
                    //1. the parent source code
                    //2. the zipped files of child exes+dlls

                    //Copying the source code files. Do remember to have source code files in bin directory of hosting server
                    DirectoryCopy(Server.MapPath("~/App_Data//Code//Parent"), downloadFolderPath, true);

                    //Edit the form.cs file having SessionID as  private const string SESSION_ID = "#SESSIONID#";
                    string text = System.IO.File.ReadAllText(Path.Combine(downloadFolderPath, "Form1.cs"));
                    text = text.Replace("#SESSIONID#", sessionCode);
                    System.IO.File.WriteAllText(Path.Combine(downloadFolderPath, "Form1.cs"), text);

                    //Generate parent exe from code by calling MSBuild
                    //An exe will be generated at this path

                    string MsbuildPath = string.Format("C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe ");//, Path.Combine(downloadFolderPath, "MagnoMedia.Windows.Installer.csproj"));
                    //Process.Start("C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe \"{0}\"", Path.Combine(downloadFolderPath, "MagnoMedia.Windows.Installer.csproj"));
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.LoadUserProfile = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.FileName = MsbuildPath;
                    proc.StartInfo.Arguments = Path.Combine(downloadFolderPath, "MagnoMedia.Windows.Installer.csproj");
                    proc.Start();
                    proc.WaitForExit();
                    //redirect download link of above generated exe
                    downloadFile = Path.Combine(downloadFolderPath, "bin\\Debug", "MagnoMedia.Windows.Installer.exe");
                    //insert new tracking
                    UserTrack userTrack = new UserTrack();// here find row on basis of sessionid
                    userTrack.UpdatedDate = DateTime.Now;
                    userTrack.SessionDetailId = lastSession.Id;
                    userTrack.State = UserTrackState.DownloadRequest;

                    InsertInDB<UserTrack>(dbFactory, userTrack);
                }
            }
            return File(downloadFile, System.Net.Mime.MediaTypeNames.Application.Octet, "MagnoMedia.Windows.Installer.exe");

        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }


    }
}
