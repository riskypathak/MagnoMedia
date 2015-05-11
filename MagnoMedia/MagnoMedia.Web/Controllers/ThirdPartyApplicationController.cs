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
        //[Authorize]
        public ActionResult Index()
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                List<ThirdPartyApplication> tpa = null;
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
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Add(ThirdPartyApplication tpa)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Insert<ThirdPartyApplication>(tpa);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //[Authorize]
        public ActionResult Edit(int? id)
        {
            try
            {
                ThirdPartyApplication tpa = null;

                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    tpa = db.SingleById<ThirdPartyApplication>(id.Value);
                }

                return View(tpa);
            }
            catch (Exception)
            {

                throw;
            }


        }

        //[Authorize]
        [HttpPost]
        public ActionResult Edit(ThirdPartyApplication tpa)
        {
            try
            {
                IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

                using (IDbConnection db = dbFactory.Open())
                {
                    db.Update<ThirdPartyApplication>(tpa);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[Authorize]
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
