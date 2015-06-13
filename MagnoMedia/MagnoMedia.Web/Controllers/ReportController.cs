using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MagnoMedia.Web.Controllers
{
    public class ReportController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult App(int? AppId, int? CountryId, string _startDate, string _endDate)
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
                        var validUsers = db.LoadSelect<User>().Where(u => u.CountryId == countryCode);

                        //Join below with users found above.
                        var validApps = db.Select<UserAppTrack>().Where(a => a.ApplicationId == appId);

                        //On basis of app state, generate a report having count for state(if no state then give all states as column)
                        //At header provide ApplicationName, Country, StarteDate, EndDate, State(if present)
                        ResultCount = (from ut in validUsersTrack
                                       join vu in validUsers on ut.SessionDetailId equals vu.SessionDetailId
                                       join vp in validApps on vu.Id equals vp.UserId
                                       group ut.State by new { vu.Country.Country_name, ut.State } into x
                                       select new SearchResult { Country = x.Key.Country_name, _UserTrackState = x.Key.State, Count = x.Count() }).ToList();
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

        [Authorize]
        [HttpGet]
        public ActionResult Installer(int? StatusId, int? CountryId, string _startDate, string _endDate)
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
            int countryCode = 0;
            if (CountryId != null)
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
                    var validUsers = db.LoadSelect<User>().Where(u => u.CountryId == countryCode);
                    ResultCount = (from ut in validUsersTrack
                                   join vu in validUsers on ut.SessionDetailId equals vu.SessionDetailId
                                   group ut.State by new { vu.Country.Country_name, ut.State } into x
                                   select new SearchResult { Country = x.Key.Country_name, _UserTrackState = x.Key.State, Count = x.Count() }).ToList();
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
    }
}
