using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

                }
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
