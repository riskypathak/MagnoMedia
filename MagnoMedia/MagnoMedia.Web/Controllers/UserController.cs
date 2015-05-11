using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MagnoMedia.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);

                    return RedirectToAction("Index", "ThirdPartyApplication");
                }
                else
                {
                    ModelState.AddModelError("", "Unable To Login");
                }
            }
            return View(user);
        }

        private bool IsValid(string userName, string password)
        {
           
            if (userName == "abc" && password == "abc")
                return true;
            return false;

        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "User");
        }

    }
}
