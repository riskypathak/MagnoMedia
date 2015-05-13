using MagnoMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagnoMedia.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            //Insert into SessionDetails
            SessionDetail session = new SessionDetail();
            session.SessionId = Session.SessionID;
            session.CompleteRequestUri = Request.Url.ToString();
            session.Referer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : null;
            session.RequestDate = DateTime.Now;
            session.UserAgent = Request.UserAgent != null ? Request.UserAgent.ToString() : null;
            session.IPAddress = Request.UserHostAddress;

            //Save into db here

            //Insert into tracking
            UserTrack userTrack = new UserTrack();
            userTrack.UpdatedDate = DateTime.Now;
            userTrack.SessionId = session.SessionId;
            userTrack.State = TrackingState.LandingPage;

            return View();
        }

        public ActionResult Download()
        {
            //Check here if session already exist in database table.
            //Also Check only in last 5 minutes. Because a session expire in 20 minutes by default so lets take a 1/4th value
            //Also as sessionid can be repeated so this will help us to track unique session in last 5 min utes

            //if(db.Sessions.Where(s=>s.SessionId == Session.SessionID && s.RequestDate > DateTime.Now.AddMinutes(-5)))
            //{
            //Create a folder here.

            string downloadFolderPath = Server.MapPath(string.Format("Temp//{0}", Session.SessionID));
            System.IO.Directory.CreateDirectory(downloadFolderPath);

            //Transfer all files from a static folder(//AppData/Application) to above created folder
            //1. the parent source code
            //2. the zipped files of child exes+dlls

            //Copying the source code files. Do remember to have source code files in bin directory of hosting server
            DirectoryCopy(Server.MapPath("App_Data//Code//Parent"), downloadFolderPath, true);

            //Edit the form.cs file having SessionID as  private const string SESSION_ID = "#SESSIONID#";
            string text = System.IO.File.ReadAllText(Path.Combine(downloadFolderPath, "Form1.cs"));
            text = text.Replace("#SESSIONID#", Session.SessionID);
            System.IO.File.WriteAllText(Path.Combine(downloadFolderPath, "Form1.cs"), text);

            //Generate parent exe from code by calling MSBuild
            //An exe will be generated at this path
            Process.Start("C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe \"{0}\"", Path.Combine(downloadFolderPath, "MagnoMedia.Windows.Installer.csproj"));

            //redirect download link of above generated exe



            //insert new tracking
            UserTrack userTrack = null;// here find row on basis of sessionid
            userTrack.UpdatedDate = DateTime.Now;
            userTrack.SessionId = Session.SessionID;
            userTrack.State = TrackingState.LandingPage;




            //}

            //else redirect to Landing Page because we assume that user has not been come directly via LP


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
