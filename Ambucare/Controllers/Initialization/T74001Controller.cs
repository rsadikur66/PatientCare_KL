using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74001Controller : Controller
    {
        private readonly IT74001 repository;
        public T74001Controller(IT74001 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74001
        [HttpPost]
        public ActionResult T74001()
        {
            return View();
        }

        //Dropdown Start
        [HttpPost]
        public ActionResult GetItemBrandCode()
        {
            try
            {
                var ItemBrand = repository.GetItemBrandData.ToList();
                return Json(ItemBrand, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetItemUMCode(int T_TYPE_ID)
        {
            try
            {
                var ItemUM = repository.GetItemUMData(T_TYPE_ID);
                return Json(ItemUM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetTypeCode()
        {
            try
            {
                var Type = repository.GetIType.ToList();
                return Json(Type, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //Dropdown End
        
        //get save
        [HttpPost]
        public ActionResult AddItemBrand(T74001 _t74001)
        {
            bool status = repository.AddItem(_t74001);
            return Json(status, JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public ActionResult AddItemBrand73(T74001 _T74001)
        //{
        //    bool status = repository.AddItem73(_T74001);
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}

        //delete
        [HttpPost]
        public ActionResult Delete_T74001(int T_ITEM_ID)
        {
            var del = repository.Delete(T_ITEM_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }

        //For Gridview Method Start 
        [HttpPost]
        public ActionResult GetLabelData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                Int32 PageIndex = 0;
                Int32 PageSize = 0;
                if (start == "0")
                {
                    PageIndex = 0;
                }
                else
                {
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length);
                }
                PageSize = Convert.ToInt32(length);
                //IQueryable allCustomer;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                IEnumerable GD;
                int cnt;
                if (searchValue == "")
                {
                    GD = repository.GetLabelData(PageIndex, PageSize);
                    cnt = repository.GetGridData_Search_Count(searchValue);
                }
                else
                {
                    GD = repository.GetGrid_Data_Search(searchValue, PageIndex, PageSize);
                    cnt = repository.GetGridData_Search_Count(searchValue);
                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }

    }
}