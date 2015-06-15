using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
namespace MagnoMedia.Web.Controllers
{
    public class ThirdPartyApplicationController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
           
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                List<ThirdPartyApplication> tpa = new List<ThirdPartyApplication>() { new ThirdPartyApplication() { Arguments = "asd", DownloadUrl = "" } };
                using (IDbConnection db = dbFactory.Open())
                {
                    tpa = db.Select<ThirdPartyApplication>();
                }

                return View(tpa);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        public ActionResult Add()
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
                using (IDbConnection db = dbFactory.Open())
                {
                    ViewBag.country = new MultiSelectList(db.Select<Country>(), "Id", "Country_name");
                    ViewBag.browser = new MultiSelectList(db.Select<Browser>(), "Id", "BrowserName");
                    ViewBag.operatingSystem = new MultiSelectList(db.Select<MagnoMedia.Data.Models.OperatingSystem>(), "Id", "OSName");
                    ViewBag.Refer = new MultiSelectList(db.Select<Referer>(), "Id", "Name");
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(ThirdPartyApplication tpa, int[] CountryId, int[] BrowserId, int[] RefererId, int[] OSId)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Insert<ThirdPartyApplication>(tpa);
                    var appId = (int)db.LastInsertId();
                    AddApplicationCountryValidity(db, CountryId, appId);
                    AddBrowserCountryValidity(db, BrowserId, appId);
                    AddReferValidity(db, RefererId, appId);
                    AddOSValidity(db, OSId, appId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            try
            {
                ThirdPartyApplication tpa = null;

                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    List<int> countryIds = new List<int>();
                    List<int> browserIds = new List<int>();
                    List<int> referIds = new List<int>();
                    List<int> osIds = new List<int>();
                    tpa = db.SingleById<ThirdPartyApplication>(id.Value);

                    var country = db.Where<AppCountryValidity>("ApplicationId", id);
                    country.ForEach(x =>
                    {
                        countryIds.Add(x.CountryId);
                    });
                    var browser = db.Where<AppBrowserValidity>("ApplicationId", id);
                    browser.ForEach(x =>
                    {
                        browserIds.Add(x.BrowserId);
                    });


                    var referer = db.Where<AppReferValidity>("ApplicationId", id);
                    referer.ForEach(x =>
                    {
                        referIds.Add(x.ReferId);
                    });


                    var os = db.Where<AppOSValidity>("ApplicationId", id);
                    os.ForEach(x =>
                    {
                        osIds.Add(x.OSId);
                    });

                    ViewBag.country = new MultiSelectList(db.Select<Country>(), "Id", "Country_name", countryIds);
                    ViewBag.browser = new MultiSelectList(db.Select<Browser>(), "Id", "BrowserName", browserIds);
                    ViewBag.operatingSystem = new MultiSelectList(db.Select<MagnoMedia.Data.Models.OperatingSystem>(), "Id", "OSName", osIds);
                    ViewBag.Refer = new MultiSelectList(db.Select<Referer>(), "Id", "Name", referIds);
                }

                return View(tpa);
            }
            catch (Exception)
            {

                throw;
            }


        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ThirdPartyApplication tpa, int[] CountryId, int[] BrowserId, int[] RefererId, int[] OSId)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Update<ThirdPartyApplication>(tpa);
                    AddApplicationCountryValidity(db, CountryId, tpa.Id);
                    AddBrowserCountryValidity(db, BrowserId, tpa.Id);
                    AddReferValidity(db, RefererId, tpa.Id);
                    AddOSValidity(db, OSId, tpa.Id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void AddApplicationCountryValidity(IDbConnection db, int[] CountryId, int ThirdPartyApplicationId)
        {
            try
            {
                int order = 1;

                var countryrecords = db.Where<AppCountryValidity>("ApplicationId", ThirdPartyApplicationId);
                countryrecords.ForEach(x =>
                {
                    db.DeleteById<AppCountryValidity>(x.Id);

                });
                foreach (int item in CountryId)
                {
                    AppCountryValidity acv = new AppCountryValidity();
                    acv.CountryId = item;
                    acv.Order = order;
                    acv.ApplicationId = ThirdPartyApplicationId;
                    db.Insert<AppCountryValidity>(acv);
                    order++;
                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        void AddBrowserCountryValidity(IDbConnection db, int[] BrowserId, int ThirdPartyApplicationId)
        {
            try
            {
                var browserrecords = db.Where<AppBrowserValidity>("ApplicationId", ThirdPartyApplicationId);
                browserrecords.ForEach(x =>
                {
                    db.DeleteById<AppBrowserValidity>(x.ApplicationId);

                });

                int order = 1;
                foreach (int item in BrowserId)
                {
                    AppBrowserValidity abv = new AppBrowserValidity();
                    abv.BrowserId = item;
                    abv.Order = order;
                    abv.ApplicationId = ThirdPartyApplicationId;
                    db.Insert<AppBrowserValidity>(abv);
                    order++;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        void AddReferValidity(IDbConnection db, int[] RefererId, int ThirdPartyApplicationId)
        {
            try
            {
                var referRecords = db.Where<AppReferValidity>("ApplicationId", ThirdPartyApplicationId);
                referRecords.ForEach(x =>
                {
                    db.DeleteById<AppReferValidity>(x.ApplicationId);

                });

                int order = 1;
                foreach (int item in RefererId)
                {
                    AppReferValidity arv = new AppReferValidity();
                    arv.ReferId = item;
                    arv.Order = order;
                    arv.ApplicationId = ThirdPartyApplicationId;
                    db.Insert<AppReferValidity>(arv);
                    order++;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        void AddOSValidity(IDbConnection db, int[] OSId, int ThirdPartyApplicationId)
        {
            try
            {
                var OsRecords = db.Where<AppOSValidity>("ApplicationId", ThirdPartyApplicationId);
                OsRecords.ForEach(x =>
                {
                    db.DeleteById<AppOSValidity>(x.ApplicationId);

                });
                int order = 1;
                foreach (int item in OSId)
                {
                    AppOSValidity aov = new AppOSValidity();
                    aov.OSId = item;
                    aov.Order = order;
                    aov.ApplicationId = ThirdPartyApplicationId;
                    db.Insert<AppOSValidity>(aov);
                    order++;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }


        [Authorize]
        public ActionResult Delete(int? id)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.DeleteById<ThirdPartyApplication>(id.Value);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
