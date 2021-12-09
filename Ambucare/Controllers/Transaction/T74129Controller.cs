using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74129Controller : Controller
    {

        private readonly IT74129 repository;

        public T74129Controller(IT74129 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74129
        public ActionResult Index()
        {
            return View();
        }

        //GetStock
        [HttpPost]
        public ActionResult GetGridData()
        {
            var data = repository.GetGridData();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveData(T74095 t95)
        {
            var data = repository.SaveData(t95);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSearchData( string stl)
        {
            var data = repository.GetSearchData(stl);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //GetSearchData
    }
}