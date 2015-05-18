using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagnoMedia.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            //Insert into SessionDetails
            SessionDetail session = new SessionDetail();
            session.SessionId = Guid.NewGuid().ToString();
            session.CompleteRequestUri = Request.Url.ToString();
            session.Referer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : null;
            session.RequestDate = DateTime.Now;
            session.UserAgent = Request.UserAgent != null ? Request.UserAgent.ToString() : null;
            session.IPAddress = Request.UserHostAddress;

            //Save into db here
            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
            //using (IDbConnection db = dbFactory.Open())
            //{
            //    db.Insert<SessionDetail>(session);
            //}
            InsertInDB<SessionDetail>(dbFactory, session);


            //Insert into tracking
            UserTrack userTrack = new UserTrack();
            userTrack.UpdatedDate = DateTime.Now;
            userTrack.SessionId = session.SessionId;
            userTrack.State = UserTrackState.LandingPage;
            InsertInDB<UserTrack>(dbFactory, userTrack);

            //Embedd this SessionId in download/install link   on index page
            return View();
        }

        private static void InsertInDB<T>(IDbConnectionFactory dbFactory, T data)
        {
            using (IDbConnection db = dbFactory.Open())
            {
                db.Insert<T>(data);
            }
        }

        //This should be hit when url is http://<rootaddress>/download/<sessionid>
        public ActionResult Download()
        {
            string sessionId = "";// Store sessionid from input url

            //Redirect to Landing Page because we assume that user has not been come directly via LP
            if (string.IsNullOrEmpty(sessionId))
            {
                //Redirect to Index(LP)
            }

            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                //Check here if session already exist in database table.
                //Also Check only in last 5 minutes. Because we assume the download request should come from user in 5 minutes after user hit the index page
                //Also as sessionid can be repeated so this will help us to track unique session in last 5 minutes

                SessionDetail lastSession = db.Select<SessionDetail>().SingleOrDefault(s => s.SessionId == sessionId && s.RequestDate > DateTime.Now.AddMinutes(-5));

                if (lastSession == null)
                {
                    //Redirect to Index.
                }
                else
                {
                    //Create a folder here.

                    string downloadFolderPath = Server.MapPath(string.Format("~/Temp//{0}", Session.SessionID));
                    System.IO.Directory.CreateDirectory(downloadFolderPath);

                    //Transfer all files from a static folder(//AppData/Application) to above created folder
                    //1. the parent source code
                    //2. the zipped files of child exes+dlls

                    //Copying the source code files. Do remember to have source code files in bin directory of hosting server
                    DirectoryCopy(Server.MapPath("~/App_Data//Code//Parent"), downloadFolderPath, true);

                    //Edit the form.cs file having SessionID as  private const string SESSION_ID = "#SESSIONID#";
                    string text = System.IO.File.ReadAllText(Path.Combine(downloadFolderPath, "Form1.cs"));
                    text = text.Replace("#SESSIONID#", Session.SessionID);
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

                    //redirect download link of above generated exe

                    //insert new tracking
                    UserTrack userTrack = null;// here find row on basis of sessionid
                    userTrack.UpdatedDate = DateTime.Now;
                    userTrack.SessionId = sessionId;
                    userTrack.State = UserTrackState.DownloadRequest;

                    InsertInDB<UserTrack>(dbFactory, userTrack);
                }
            }

            return View();
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
