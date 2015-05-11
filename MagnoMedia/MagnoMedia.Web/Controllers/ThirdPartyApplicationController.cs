using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagnoMedia.Web.DataModel;
using System.Data.Entity;
namespace MagnoMedia.Web.Controllers
{
    public class ThirdPartyApplicationController : Controller
    {
        //
        // GET: /ThirdPartyApplication/
       [Authorize]
        public ActionResult Index()
        {
            try
            {
                List<thirdpartyapplication> tpa=new List<thirdpartyapplication>();
                using (var _dbEntites = new magnomediaEntities())
                {
                    tpa = _dbEntites.thirdpartyapplications.ToList();
                   
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
        [Authorize]
        [HttpPost]
        public ActionResult Add(thirdpartyapplication tpa)
        {
            try
            {
                using (var _dbEntites = new magnomediaEntities())
                {
                    _dbEntites.thirdpartyapplications.Add(tpa);
                    _dbEntites.SaveChanges();
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
                thirdpartyapplication tpa=new thirdpartyapplication();
                using (var _dbEntites = new magnomediaEntities())
                {
                    tpa = _dbEntites.thirdpartyapplications.Where(e => e.Id == id).FirstOrDefault();
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
        public ActionResult Edit(thirdpartyapplication tpa)
        {
            try
            {
                thirdpartyapplication record;
                using (var _dbEntites = new magnomediaEntities())
                {
                   // record = _dbEntites.thirdpartyapplications.Where(s => s.Id == tpa.Id).FirstOrDefault();

                    _dbEntites.Entry(tpa).State = System.Data.Entity.EntityState.Modified;

                    _dbEntites.SaveChanges();

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
                using (var _dbEntites = new magnomediaEntities())
                {
                    var data = _dbEntites.thirdpartyapplications.Where(e => e.Id == id).FirstOrDefault();
                    _dbEntites.thirdpartyapplications.Remove(data);
                    _dbEntites.SaveChanges();
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
