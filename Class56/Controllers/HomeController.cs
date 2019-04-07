using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Class56.Data;
using Class56.Models;

namespace Class56.Controllers
{
    //Using the existing image upload application, add the following
    //functionality:

    //1) Only allow logged in users to upload images (this will require
    //sign up/login pages).

    //2) There should be a My Account page that displays a list
    //of all images uploaded by the currently logged in user,
    //along with the amount of views

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new HomePageViewModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated
            };

            if (User.Identity.IsAuthenticated)
            {
                var db = new AuthDb(Properties.Settings.Default.ConStr);
                var user = db.GetByEmail(User.Identity.Name);
                vm.Name = user.Name;
            }

            return View(vm);
        }
    }
}