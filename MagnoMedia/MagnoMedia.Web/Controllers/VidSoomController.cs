using MagnoMedia.Web.Models;
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
