using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Transaction
{
    public class T74128Controller : Controller
    {
        private readonly IT74128 repository;

        public T74128Controller(IT74128 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74128
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetTrade()
        {
            var data = repository.GetTrade();
            //string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(data);
            //return Json(JSONString, JsonRequestBehavior.AllowGet);
              return Json(data, JsonRequestBehavior.AllowGet);
        }
        //GetStock
        [HttpPost]
        public ActionResult GetPackSize(int cosTyDel, int storeid)
        {
            var data = repository.GetPackSize(cosTyDel, storeid);
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetStock(int item,int store, int umId)
        {
            var data = repository.GetStock(item, store, umId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //GetVat
        [HttpPost]
        public ActionResult GetPrice(int costype, int umId)
        {
            var data = repository.GetPrice(costype, umId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetVat()
        {
            var data = repository.GetVat();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetPatienRequest()
        {
            var data = repository.GetPatienRequest();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveData(T74036 t36, List<T74037> t37)
        {
            var data = repository.SaveData(t36, t37);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //SaveData
    }
}