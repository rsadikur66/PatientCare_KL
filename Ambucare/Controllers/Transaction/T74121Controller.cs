using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74121Controller : Controller
    {
        private readonly IT74121 repository;

        public T74121Controller(IT74121 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74121
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetProductList(string GEN_CODE)
        {
            var ProductData = repository.GetProductList(GEN_CODE);

            return Json(ProductData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetUomtList(int TypeId, string GEN_CODE, string TRADE_CODE)
        {
            var uom = repository.GetUomtList(TypeId, GEN_CODE,TRADE_CODE);

            return Json(uom, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GenericData()
        {
            var gen = repository.GenericData();

            return Json(gen, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTypeData()
        {
            var type = repository.GetTypeData();

            return Json(type, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProTypeData( int Type_id)
        {
            var ProType = repository.GetProTypeData(Type_id);

            return Json(ProType, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveData(List<T74089> t74089)
        {
            var save = repository.SaveData(t74089);

            return Json(save, JsonRequestBehavior.AllowGet);
        }
        //SaveData
    }
}