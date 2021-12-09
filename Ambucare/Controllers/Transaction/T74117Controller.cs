using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74117Controller : Controller
    {
        // GET: T74117
        public ActionResult T74117()
        {
            return View();
        }

        private readonly IT74117 repository;

        public T74117Controller(IT74117 _repository)
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
        public ActionResult GetUom(int T_TYPE_ID)
        {
            var data = repository.GetUom(T_TYPE_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //For  Save Method
        [HttpPost]
        public ActionResult insert_T74117(T74048 t74048, List<T74049> t74049List)
        {
            var data = repository.insert_T74117(t74048, t74049List);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //Person Type Popup Method Start
        public ActionResult GetPersonalType()
        {
            var PerType = repository.GetPersonalType.ToList();
            return Json(PerType, JsonRequestBehavior.AllowGet);
        }

        //For Umo List DropDown Data
        [HttpPost]
        public ActionResult GetPersonName(int T_EMP_TYP_ID)
        {
            var data = repository.GetPersonName(T_EMP_TYP_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}