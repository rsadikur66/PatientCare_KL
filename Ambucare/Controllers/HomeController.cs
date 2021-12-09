using Ambucare.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ambucare.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IT74014 repository;

        public HomeController()
        {
            
        }

        //public HomeController(IT74014 objIrepository)
        //{
        //    repository = objIrepository;           
        //}

        public ActionResult Index()
        {
            //var data = repository.All.ToList();
            //repository.Test();
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SaveTutorial(TutorialModel tutorial, HttpPostedFileBase attachment)
        {
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var inputStream = fileContent.InputStream;
                    var fileName = Path.GetFileName(fileContent.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        inputStream.CopyTo(fileStream);
                    }
                }
            }
            return Json("Tutorial Saved", JsonRequestBehavior.AllowGet);
        }
    }
}