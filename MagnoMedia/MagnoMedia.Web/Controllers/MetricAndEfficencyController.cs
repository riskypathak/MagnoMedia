using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace MagnoMedia.Web.Controllers
{
    public class MetricAndEfficencyController : Controller
    {
        //
        // GET: /MetricAndEfficency/
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
                using (IDbConnection db = dbFactory.Open())
                {

                    ViewBag.CountryList = new SelectList(db.Select<Country>(), "Id", "Country_name");
                    ViewBag.Refer = new SelectList(db.Select<Referer>(), "Id", "Name");



                    CalculateDownloadPerLP(db);

                }
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CalculateDownloadPerLP(IDbConnection db)
        {
            double totalDownloads = db.Select<UserTrack>().Where(t => t.State == UserTrackState.DownloadRequest).Count();
            double totalLP = db.Select<UserTrack>().Where(t => t.State == UserTrackState.LandingPage).Count();

            double totalInstalls = db.Select<UserTrack>().Where(t => t.State == UserTrackState.InstallInit).Count();
            double totalInstallsSuccess = db.Select<UserTrack>().Where(t => t.State == UserTrackState.InstallComplete).Count();
            double totalInstallsFail = db.Select<UserTrack>().Where(t => t.State == UserTrackState.InstallFail).Count();

            if (totalDownloads != 0 && totalLP != 0)
            {
                ViewBag.DownloadPerLP = Math.Round((totalDownloads / totalLP) * 100, 2, MidpointRounding.AwayFromZero).ToString() + " %";
            }
            else
            {
                ViewBag.DownloadPerLP = "NA";
            }

            if (totalInstalls != 0 && totalDownloads != 0)
            {
                ViewBag.InstallPerDownload = Math.Round((totalInstalls / totalDownloads) * 100, 2, MidpointRounding.AwayFromZero).ToString() + " %";
            }
            else
            {
                ViewBag.InstallPerDownload = "NA";
            }

            if (totalInstallsSuccess != 0 && totalInstalls != 0)
            {
                ViewBag.SuccessPerInstall = Math.Round((totalInstallsSuccess / totalInstalls) * 100, 2, MidpointRounding.AwayFromZero).ToString() + " %";
            }
            else
            {
                ViewBag.SuccessPerInstall = "NA";
            }

            if (totalInstallsSuccess != 0 && totalDownloads != 0)
            {
                ViewBag.SuccessPerDownload = Math.Round((totalInstallsSuccess / totalDownloads) * 100, 2, MidpointRounding.AwayFromZero).ToString() + " %";
            }
            else
            {
                ViewBag.SuccessPerDownload = "NA";
            }

            if (totalInstallsSuccess != 0 && totalLP != 0)
            {
                ViewBag.SuccessPerVisit = Math.Round((totalInstallsSuccess / totalLP) * 100, 2, MidpointRounding.AwayFromZero).ToString() + " %";
            }
            else
            {
                ViewBag.SuccessPerVisit = "NA";
            }

            if (totalInstallsFail != 0 && totalLP != 0)
            {
                ViewBag.FailurePerInstall = Math.Round((totalInstallsFail / totalInstalls) * 100, 2, MidpointRounding.AwayFromZero).ToString() + " %";
            }
            else
            {
                ViewBag.FailurePerInstall = "NA";
            }
        }

        public ActionResult Efficiency()
        {
            //The efficiency will be for each app for a specific date & country.

            //Get these values from post parameters
            int appId = 0;
            DateTime date = DateTime.Now;
            int countryId = 0;
            int referId = 0; // as of not using this. But have to integrate it for finding all

            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);


            using (IDbConnection db = dbFactory.Open())
            {
                IEnumerable<Revenue> allRevenues = db.Select<Revenue>().Where(r => r.CountryId == countryId && r.Date == date);

                if (allRevenues.Count() == 0)
                {
                    //Display Error that data is not present to find out Install Revenue Details. Please provide in Revenue Screen
                }
                else
                {
                    Revenue revenue = db.Select<Revenue>().SingleOrDefault(r => r.ApplicationId == appId && r.CountryId == countryId && r.Date == date);

                    if (revenue == null)
                    {
                        //Display Error that Revenue Details for input day/input country/input app is not available. Please provide in Revenue Screen
                    }
                    else
                    {
                        //We have revenue for each app, each country and each day.
                        double appRevenue = revenue.Value;


                        //We can also calculate for total revenue(from all apps) each country and each day.
                        double installerRevenue = 0.0;
                        allRevenues.ToList().ForEach(r => installerRevenue += r.Value);

                        //First calculate total successful app installs in input country and for input day.
                        int totalAppInstalls = db.LoadSelect<UserAppTrack>().Where(a => a.ApplicationId == appId && a.State == AppInstallState.Success && a.UpdatedDate == date)
                            .GroupBy(a => a.UserId).Count();

                        double appCPM = appRevenue * 1000 / totalAppInstalls;

                        //Second get Total Installs (not application but installer) for that day and that country
                        int totalInstallerInstalls = db.LoadSelect<UserTrack>().Where(i => i.State == UserTrackState.InstallComplete && i.UpdatedDate == date).Count();

                        double installerCPM = installerRevenue * 1000 / totalInstallerInstalls;

                        //Now install efficiency for this app in input country and for input day is
                        double appInstallEfficiency = appCPM * 100 / installerCPM;

                        //Provide this install efficiency in a nice tabular way.
                        ViewBag.InstallerEfficiency = appInstallEfficiency;
                    }
                }
            }

            return View();
        }
    }
}
