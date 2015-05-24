using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Dynamic;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Globalization;

namespace MagnoMedia.Web.Controllers
{
    public static class impFunctions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }
    }
    public class TestController : Controller
    {
        //
        // GET: /Test/

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
            userTrack.SessionDetailId = session.Id;
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

                SessionDetail lastSession = db.Select<SessionDetail>().SingleOrDefault(s => s.SessionCode == sessionId && s.RequestDate > DateTime.Now.AddMinutes(-5));

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
                    userTrack.SessionDetailId = lastSession.Id;
                    userTrack.State = UserTrackState.DownloadRequest;

                    InsertInDB<UserTrack>(dbFactory, userTrack);
                }
            }

            return View();
        }

        //There will be two methods. One is for get which will show the dropdowns and other fields for report.
        //There will be one post method which will generate report. The below logic is for post only.
         [HttpGet]
        public ActionResult ReportApps(int? AppId, int? CountryId, string _startDate, string _endDate)
        {
             
            //These values we will get from post parameters
       
            int appId = 0;
            int countryCode = 0;
            UserAppTrack appState;
            dynamic ResultCount;
            List<SearchResult> SearchResultList = new List<SearchResult>();
            DateTime startDate = DateTime.Now;
            if (_startDate != null)
            {
                startDate = DateTime.Parse(_startDate);
            }

            DateTime endDate = DateTime.Now;
            if (_endDate != null)
            {
                endDate = DateTime.Parse(_endDate);
            }
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    if (CountryId != null)
                    {
                        if (AppId != null)
                            appId = AppId.Value;
                        countryCode = CountryId.Value;
                        var validUsersTrack = db.Select<UserTrack>().Where(u => (int)u.State > 4 && u.UpdatedDate > startDate && u.UpdatedDate < endDate); //considering only those users whoose installer installs.

                        //Join with above users find in valid track
                        var validUsers = db.Select<User>().Where(u => u.CountryId == countryCode);

                        //Join below with users found above.
                        var validApps = db.Select<UserAppTrack>().Where(a => a.Id == appId);

                        //On basis of app state, generate a report having count for state(if no state then give all states as column)
                        //At header provide ApplicationName, Country, StarteDate, EndDate, State(if present)
                        ResultCount = (from ut in validUsersTrack
                                       join vu in validUsers on ut.SessionDetailId equals vu.SessionDetailId
                                       join vp in validApps on vu.Id equals vp.UserId
                                       group ut.State by new { vu.Country.Country_name, ut.State } into x
                                       select new SearchResult { Country = x.Key.Country_name, _UserTrackState = x.Key.State, DownLoadCount = x.Count() }).ToList();
                        SearchResultList.AddRange(ResultCount);   
                    }
                    ViewBag.country = new SelectList(db.Select<Country>(), "Id", "Country_name");

                    var statuses = from UserTrackState s in Enum.GetValues(typeof(UserTrackState))
                                   select new { ID = (int)s, Name = s.ToString() };
                    ViewBag.status = new SelectList(statuses, "ID", "Name");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(SearchResultList);
        }
   
        //There will be two methods. One is for get which will show the dropdowns and other fields for report.
        //There will be one post method which will generate report. The below logic is for post only.
        
 
        [HttpGet]
        public ActionResult ReportInstall(int? StatusId, int? CountryId, string _startDate, string _endDate)
        {
            //These values we will get from querystring
            DateTime startDate = DateTime.Now;
            if (_startDate != null)
            {
                startDate = DateTime.Parse(_startDate);
            }          
            
            DateTime endDate = DateTime.Now;
            if (_endDate != null)
            {
                endDate = DateTime.Parse(_endDate);
            }
            int countryCode=0;
            if(CountryId!=null)
             countryCode = CountryId.Value;
            UserTrack userTrack;
            dynamic ResultCount;
            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
            List<SearchResult> SearchResultList = new List<SearchResult>();
            using (IDbConnection db = dbFactory.Open())
            {
                if (CountryId != null)
                {
                    var validUsersTrack = db.Select<UserTrack>().Where(u => u.UpdatedDate > startDate && u.UpdatedDate < endDate); //considering only those users whoose installer installs.

                    if (StatusId != null)
                    {
                        validUsersTrack = db.Select<UserTrack>().Where(u => u.UpdatedDate > startDate && u.UpdatedDate < endDate && u.State == (UserTrackState)StatusId); //considering only those users whoose installer installs.

                    }
                    //Join with above users find in valid track
                    var validUsers = db.Select<User>().Where(u => u.CountryId == countryCode);
                    ResultCount = (from ut in validUsersTrack
                                   join vu in validUsers on ut.SessionDetailId equals vu.SessionDetailId
                                   group ut.State by new { vu.Country.Country_name, ut.State } into x
                                   select new SearchResult { Country = x.Key.Country_name, _UserTrackState = x.Key.State, DownLoadCount = x.Count()}).ToList();
                    SearchResultList.AddRange(ResultCount);                   
                }
                //On basis of user state, generate a report having count for state(if no state then give all states as column)
                //At header provide Country, StarteDate, EndDate, State(if Present)

                ViewBag.country = new SelectList(db.Select<Country>(), "Id", "Country_name");
                ViewBag.browser = new SelectList(db.Select<Browser>(), "Id", "BrowserName");
                var statuses = from UserTrackState s in Enum.GetValues(typeof(UserTrackState))
                               select new { ID = (int)s, Name = s.ToString() };
                ViewBag.status = new SelectList(statuses, "ID", "Name");
            }


            return View(SearchResultList);
        }

        public ActionResult Efficiency()
        {
            //The efficiency will be for each app for a specific date & country.

            //Get these values from post parameters
            int appId = 0;
            DateTime date = DateTime.Now;
            string countryCode = "";

            //We have revenue for each app, each country and each day.
            double appRevenue = 0.0;

            //We can also calculate for total revenue(from all apps) each country and each day.
            double installerRevenue = 0.0;

            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                //First calculate total successful app installs in input country and for input day.
                int totalAppInstalls = 0;

                double appCPM = appRevenue * 1000 / totalAppInstalls;

                //Second get Total Installs (not application but installer) for that day and that country
                int totalInstallerInstalls = 0;
                double installerCPM = installerRevenue * 1000 / totalInstallerInstalls;

                //Now install efficiency for this app in input country and for input day is
                double appInstallEfficiency = appCPM * 100 / installerCPM;

                //Provide this install efficiency in a nice tabular way.
            }

            return View();
        }

        public ActionResult Metrics()
        {
            //We want here few metrics

            //1. Download per LP visits Metric
            
            //2. Install Start per Download Metric

            //3. Install Complete per Download Metric.

            //4. Install Complete & Partial Success per Install Start Metric. This will tell us success rate of our installer

            return View();
        }

        
        //CRUD operation for Revenue model
        public ActionResult Revenue()
        {
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
