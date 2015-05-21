using MagnoMedia.Data;
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

                List<ThirdPartyApplication> tpa = new List<ThirdPartyApplication>() { new ThirdPartyApplication(){ Arguments="asd",DownloadUrl="" } };
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
        public ActionResult Add(ThirdPartyApplication tpa, int[] CountryId, int[] BrowserId)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Insert<ThirdPartyApplication>(tpa);
                    var id =(int) db.LastInsertId();
                    AddApplicationCountryValidity(db, CountryId, id);
                    AddBrowserCountryValidity(db, BrowserId, id);
                }
                return RedirectToAction("Index");
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

                    ViewBag.country = new MultiSelectList(db.Select<Country>(), "Id", "Country_name", countryIds);
                    ViewBag.browser = new MultiSelectList(db.Select<Browser>(), "Id", "BrowserName", browserIds);
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
        public ActionResult Edit(ThirdPartyApplication tpa, int[] CountryId, int[] BrowserId)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Update<ThirdPartyApplication>(tpa);
                    AddApplicationCountryValidity(db, CountryId, tpa.Id);
                    AddBrowserCountryValidity(db, BrowserId, tpa.Id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
