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
    public class RevenueController : Controller
    {
        //
        // GET: /Revenue/
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                List<Revenue> revenue = new List<Revenue>();
                using (IDbConnection db = dbFactory.Open())
                {
                    revenue = db.LoadSelect<Revenue>();
                }

                return View(revenue);

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
                    ViewBag.Applicationlist = new SelectList(db.Select<ThirdPartyApplication>(), "Id", "Name");
                    ViewBag.Countrylist = new SelectList(db.Select<Country>(), "Id", "Country_name");
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(Revenue model)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Insert<Revenue>(model);

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


                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
                Revenue model = null;
                using (IDbConnection db = dbFactory.Open())
                {
                    List<int> countryIds = new List<int>();
                    List<int> browserIds = new List<int>();
                    model = db.SingleById<Revenue>(id.Value);
                    ViewBag.Applicationlist = new SelectList(db.Select<ThirdPartyApplication>(), "Id", "Name");
                    ViewBag.Countrylist = new SelectList(db.Select<Country>(), "Id", "Country_name");
                }
                return View(model);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Revenue model)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Update<Revenue>(model);
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
                    db.DeleteById<Revenue>(id.Value);
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
