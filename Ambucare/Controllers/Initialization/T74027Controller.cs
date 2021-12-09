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
    public class T74027Controller : Controller
    {
        private readonly IT74027 repository;

        public T74027Controller(IT74027 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }

        // GET: T74027
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCompanysData()
        {
            try
            {
                var data = repository.GetCompanysData.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProductItem(int T_COST_TYPE_ID)
        {
            try
            {
                var data = repository.GetProductItem(T_COST_TYPE_ID);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetStoresData()
        {
            try
            {
                string zone = Session["T_ZONE_CODE"].ToString();
                var data = repository.GetStoresData(zone);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSupplierData()
        {
            try
            {
                var data = repository.GetSupplierData();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProductTypeData()
        {
            try
            {
                var data = repository.GetProductTypeData.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUom(int T_TYPE_ID)
        {
            try
            {
                var data = repository.GetUom(T_TYPE_ID);
                return Json(data, JsonRequestBehavior.AllowGet); ;
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        //get Grid All details  
        [HttpPost]
        public ActionResult GetGridAllData(int T_STORE_ID)
        {
            var list = repository.GetGridAllData(T_STORE_ID);
            //string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //get save
        [HttpPost]
        public void insert_T74027(List<T74027> _t74027)
        {
            repository.insert_T74027(_t74027);
        }

        public void Del_t74027(List<T74027> _t74027Del)
        {
            repository.Del_t74027(_t74027Del);

        }

        public ActionResult GetItemTypeList()
        {
            var ItemType = repository.GetItemTypeList();
            return Json(ItemType, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetStockQuantity(int ITEM_ID)
        {
            var ItemType = repository.GetStockQuantity(ITEM_ID);
            return Json(ItemType, JsonRequestBehavior.AllowGet);
        }
    }
}
