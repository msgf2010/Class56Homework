using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Class56.Data;
using Class56.Models;

namespace Class56.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        public ActionResult Index()
        {
            var mgr = new ImageManager(Properties.Settings.Default.ConStr);
            mgr.IncrementViewCount((int)Session["currentUser"]); //got lazy:-)
            var images = mgr.Get((int)Session["currentUser"]);
            return View(images);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image, string description)
        {
            string ext = Path.GetExtension(image.FileName);
            string fileName = $"{Guid.NewGuid()}{ext}";
            string fullPath = $"{Server.MapPath("/UploadedImages")}\\{fileName}";
            image.SaveAs(fullPath);
            var mgr = new ImageManager(Properties.Settings.Default.ConStr);
            mgr.SaveImage(new Image
            {
                FileName = fileName,
                Description = description,
                UserId = (int)Session["currentUser"]
            });
            return Redirect("/");
        }
    }
}