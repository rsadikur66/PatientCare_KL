using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74116Controller : Controller
    {
        private readonly IT74116 repository;

        public T74116Controller(IT74116 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74116
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult GetPursData(int Pur_ID)
        {
            var PursData = repository.getPursData(Pur_ID);

            return Json(PursData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetreceiveBy(int store_ID)
        {
            var receiveBy = repository.getreceiveBy(store_ID);

            return Json(receiveBy, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetpurProduct(int Pur_ID)
        {
            var purProduct = repository.getpurProduct(Pur_ID);

            return Json(purProduct, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUom(int T_ITEM_ID)
        {
            var receiveBy = repository.GetUom(T_ITEM_ID);

            return Json(receiveBy, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveData(T74060 t74060, List<T74061> t74061, List<T74031> t74031, T74030 t74030, List<T74027> t74027)
        {
            var receiveBy = repository.SaveData(t74060, t74061, t74031, t74030, t74027);

            return Json(receiveBy, JsonRequestBehavior.AllowGet);
        }
        //SaveData
    }
}