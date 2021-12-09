using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74120Controller : Controller
    {
        // GET: T74120
        public ActionResult T74120()
        {
            return View();
        }

        private readonly IT74120 repository;

        public T74120Controller(IT74120 _repository)
        {
            this.repository = _repository;
        }

        //For Supplier List DropDown Data
        [HttpPost]
        public ActionResult GetSupplierList()
        {
            var supplier = repository.GetSupplierList();
            return Json(supplier, JsonRequestBehavior.AllowGet);
        }

        //For Store From List DropDown Data
        [HttpPost]
        public ActionResult GetStoreListFrom()
        {
            var store = repository.GetStoreListFrom();
            return Json(store, JsonRequestBehavior.AllowGet);
        }

        //For Store To List DropDown Data
        [HttpPost]
        public ActionResult GetStoreListTo()
        {
            var store = repository.GetStoreListTo();
            return Json(store, JsonRequestBehavior.AllowGet);
        }

        //For Item Type list Popup Data 
        [HttpPost]
        public ActionResult GetItemTypeList()
        {
            var ItemType = repository.GetItemTypeList();
            return Json(ItemType, JsonRequestBehavior.AllowGet);
        }

        //For Item Name list Popup Data 
        [HttpPost]
        public ActionResult GetItemNameList(int id)
        {
            var ItemName = repository.GetItemNameList(id);
            return Json(ItemName, JsonRequestBehavior.AllowGet);
        }

        //For Currency List DropDown Data
        [HttpPost]
        public ActionResult GetCurrencyList()
        {
            var currency = repository.GetCurrencyList();
            return Json(currency, JsonRequestBehavior.AllowGet);
        }

        //For Umo List DropDown Data
        [HttpPost]
        public ActionResult GetUom(int T_COST_TYPE_ID)
        {
            var data = repository.GetUom(T_COST_TYPE_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //For  Save Method
        [HttpPost]
        public ActionResult Insert_T74120(T74080 t74080, List<T74081> t74081List)
        {
            var data = repository.Insert_T74120(t74080, t74081List);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Person Type Popup Method Start
        public ActionResult GetPersonType()
        {
            var PerType = repository.GetPersonType();
            return Json(PerType, JsonRequestBehavior.AllowGet);
        }

        //For Umo List DropDown Data
        [HttpPost]
        public ActionResult GetPersonName(int T_EMP_TYP_ID)
        {
            var data = repository.GetPersonName(T_EMP_TYP_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Get stock Method Start
        [HttpPost]
        public ActionResult GetStock(int? ITEM_ID, int? UM_ID, int? STORE_ID)
        {
            var data = repository.GetStock(ITEM_ID, UM_ID, STORE_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        //Get Purchase Price Method Start
        [HttpPost]
        public ActionResult GetPurPrice(int ITEM_ID, int UM_ID)
        {
            var data = repository.GetPurPrice(ITEM_ID, UM_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}