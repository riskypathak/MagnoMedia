using MagnoMedia.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagnoMedia.Web.Controllers
{
    public class VidSoomController : Controller
    {
        //
        // GET: /VidSoom/

        public ActionResult Index()
        {
            VidSoomHome home = new VidSoomHome
            {
                Title = "Welcome to Vidsoom"
            };
            return View(home);
        }

    }
}
