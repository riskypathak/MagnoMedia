using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using MagnoMedia.Web.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

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

        [Authorize]
        [HttpGet]
        public ActionResult Efficiency()
        {
            MagnoMedia.Web.Models.Efficiency model = new Efficiency();

            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
                using (IDbConnection db = dbFactory.Open())
                {


                    List<SelectListItem> items = new SelectList(db.Select<Country>(), "Id", "Country_name").ToList();
                    items.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
                    model.CountryList = items;

                    List<SelectListItem> items1 = new SelectList(db.Select<Referer>(), "Id", "Name").ToList(); ;
                    items1.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
                    model.ReferList = items1;

                    List<SelectListItem> items2 = new SelectList(db.Select<ThirdPartyApplication>(), "Id", "Name").ToList(); ;
                    items2.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
                    model.ApplicationList = items2;


                    model.UpdatedDate = DateTime.Now.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Efficiency(string model1)
        {
            //The efficiency will be for each app for a specific date & country.
            JavaScriptSerializer jss = new JavaScriptSerializer();
            MagnoMedia.Web.Models.Efficiency model = jss.Deserialize<MagnoMedia.Web.Models.Efficiency>(model1);

            //Get these values from post parameters
            int appId = Convert.ToInt32(model.Application);
            DateTime date = Convert.ToDateTime(model.UpdatedDate);
            int countryId = Convert.ToInt32(model.Country);
            int referId = Convert.ToInt32(model.Referer); // as of not using this. But have to integrate it for finding all

            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);


            using (IDbConnection db = dbFactory.Open())
            {
                IEnumerable<Revenue> allRevenues = null;
                if (countryId > 0)
                {
                    allRevenues = db.Select<Revenue>().Where(r => r.CountryId == countryId && r.Date == date);
                }
                else //All Case
                {
                    allRevenues = db.Select<Revenue>().Where(r => r.Date == date);

                }

                if (allRevenues.Count() == 0)
                {
                    //Display Error that data is not present to find out Install Revenue Details. Please provide in Revenue Screen
                    model.InstallEfficiency.Add("0");
                    model.InstallerECPM.Add("0");
                    model.ApplicationECPM.Add("0");
                }
                else
                {
                    List<Revenue> revenue = new List<Revenue>();
                    if (countryId > 0 && appId > 0)
                    {
                        revenue.Add(db.Select<Revenue>().SingleOrDefault(r => r.ApplicationId == appId && r.CountryId == countryId && r.Date == date));
                    }
                    else if (countryId == 0 && appId > 0) // All Country
                    {
                        revenue.Add(db.Select<Revenue>().SingleOrDefault(r => r.ApplicationId == appId && r.Date == date));
                    }
                    else if (countryId > 0 && appId == 0) // All application
                    {
                        revenue.AddRange(db.Select<Revenue>().Where(r => r.CountryId == countryId && r.Date == date).ToList());
                    }
                    else //All application and country
                    {
                        revenue.AddRange(db.Select<Revenue>().Where(r => r.Date == date).ToList());
                    }

                    if (revenue == null)
                    {
                        //Display Error that Revenue Details for input day/input country/input app is not available. Please provide in Revenue Screen
                        model.InstallEfficiency.Add("0");
                        model.InstallerECPM.Add("0");
                        model.ApplicationECPM.Add("0");
                    }
                    else
                    {
                        //We have revenue for each app, each country and each day.
                        double appRevenue = revenue[0].Value;


                        //We can also calculate for total revenue(from all apps) each country and each day.
                        double installerRevenue = 0.0;
                        allRevenues.ToList().ForEach(r => installerRevenue += r.Value);

                        //First calculate total successful app installs in input country and for input day.
                        int totalAppInstalls = 0;
                        if (appId > 0 && referId > 0) 
                        {
                            totalAppInstalls = db.LoadSelect<UserAppTrack>().Where(a => a.SessionDetailId == referId && a.ApplicationId == appId && a.State == AppInstallState.Success && a.UpdatedDate.ToShortDateString() == date.ToShortDateString())
                                .GroupBy(a => a.UserId).Count();
                        }
                        else if (appId == 0 && referId > 0) //All app
                        {
                            totalAppInstalls = db.LoadSelect<UserAppTrack>().Where(a => a.SessionDetailId == referId && a.State == AppInstallState.Success && a.UpdatedDate.ToShortDateString() == date.ToShortDateString())
                               .GroupBy(a => a.UserId).Count();
                        }
                        else if (appId > 0 && referId == 0) //All referer
                        {
                            totalAppInstalls = db.LoadSelect<UserAppTrack>().Where(a => a.ApplicationId == appId && a.State == AppInstallState.Success && a.UpdatedDate.ToShortDateString() == date.ToShortDateString())
                               .GroupBy(a => a.UserId).Count();
                        }
                        else //All referer and app
                        {
                            totalAppInstalls = db.LoadSelect<UserAppTrack>().Where(a => a.State == AppInstallState.Success && a.UpdatedDate.ToShortDateString() == date.ToShortDateString())
                               .GroupBy(a => a.UserId).Count();
                        }

                        double appCPM = 0;
                        int totalInstallerInstalls = 0;
                        double installerCPM = 0;
                        if (totalAppInstalls > 0)
                        {
                            //Second get Total Installs (not application but installer) for that day and that country
                            appCPM = appRevenue * 1000 / totalAppInstalls;

                            //get installer count by referid
                            if (referId > 0) 
                                totalInstallerInstalls = db.LoadSelect<UserTrack>().Where(i => i.SessionDetailId == referId && i.State == UserTrackState.InstallComplete && i.UpdatedDate.ToShortDateString() == date.ToShortDateString()).Count();
                            else // all referer
                                totalInstallerInstalls = db.LoadSelect<UserTrack>().Where(i => i.State == UserTrackState.InstallComplete && i.UpdatedDate.ToShortDateString() == date.ToShortDateString()).Count();
                            
                            installerCPM = installerRevenue * 1000 / totalInstallerInstalls;
                        }


                        //Now install efficiency for this app in input country and for input day is
                        double appInstallEfficiency = 0;
                        if (appId > 0)
                        {
                            appInstallEfficiency = appCPM * 100 / installerCPM;
                            model.InstallEfficiency.Add(appInstallEfficiency.ToString());
                            model.InstallerECPM.Add(installerCPM.ToString());
                            model.ApplicationECPM.Add(appCPM.ToString());
                        }
                        else //All 
                        {
                            appInstallEfficiency = (appCPM / installerCPM) * 100;
                            model.InstallEfficiency.Add(appInstallEfficiency.ToString());
                            model.InstallerECPM.Add(installerCPM.ToString());
                            model.ApplicationECPM.Add(appCPM.ToString());
                        }

                        //Provide this install efficiency in a nice tabular way.


                    }
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(model);
            return Json(json); ;
        }
    }
}
