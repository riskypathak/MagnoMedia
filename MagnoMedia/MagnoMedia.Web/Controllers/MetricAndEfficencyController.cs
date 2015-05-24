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

            if (totalDownloads != 0 && totalLP != 0)
            {
                ViewBag.DownloadPerLP = Math.Round((totalDownloads / totalLP) * 100, 2, MidpointRounding.AwayFromZero).ToString() + " %";
            }
            else
            {
                ViewBag.DownloadPerLP = "NA";
            }
        }

    }
}
