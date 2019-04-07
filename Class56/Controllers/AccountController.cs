using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Class56.Models;
using Class56.Data;
using System.Web.Security;

namespace Class56.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user, string password)
        {
            var db = new AuthDb(Properties.Settings.Default.ConStr);
            db.AddUser(user, password);
            return Redirect("/");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var db = new AuthDb(Properties.Settings.Default.ConStr);
            var user = db.Login(email, password);
            if (user == null)
            {
                TempData["message"] = "Invalid login attempt";
                return Redirect("/account/login");
            }

            Session["currentUser"] = user.Id;

            FormsAuthentication.SetAuthCookie(email, true);
            return Redirect("/");
        }
    }
}