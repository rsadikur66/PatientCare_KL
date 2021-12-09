using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74114Controller : Controller
    {
        // GET: T74114
        public ActionResult T74114()
        {
            return View();
        }

        private readonly IT74114 repository;

        public T74114Controller(IT74114 _repository)
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

        //For Store List DropDown Data
        [HttpPost]
        public ActionResult GetStoreList()
        {
            var store = repository.GetStoreList();
            return Json(store, JsonRequestBehavior.AllowGet);
        }

        //For Currency List DropDown Data
        [HttpPost]
        public ActionResult GetCurrencyList()
        {
            var currency = repository.GetCurrencyList();
            return Json(currency, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public ActionResult GetUom(int T_TYPE_ID)
        {
            var data = repository.GetUom(T_TYPE_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //get save
        [HttpPost]
        public ActionResult insert_T74114(T74030 t74030, List<T74031> t74031List)
        { 
            var data = repository.insert_T74114(t74030, t74031List);
            return Json(data, JsonRequestBehavior.AllowGet);
           
        }

        
    }
}