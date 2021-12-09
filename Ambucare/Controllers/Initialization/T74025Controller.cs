using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74025Controller : Controller
    {
        // GET: T74025
        private readonly IT74025 repository;

        public T74025Controller(IT74025 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74025()
        {
            return View();
        }

        //get grid data
        [HttpPost]
        public ActionResult GetItemBrandData()
        {
            try
            {
                //Datatable parameter
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                //paging parameter
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //sorting parameter
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                //filter parameter
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                Int32 PageIndex = 0;
                Int32 PageSize = 0;
                if (start == "0")
                {
                    PageIndex = 0;
                }
                else
                {
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length); //+ 1;
                }
                PageSize = Convert.ToInt32(length);

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                IEnumerable GD;
                int cnt;
                int recordsTotal = 0;

                if (searchValue == "")
                {
                    GD = repository.GetGridData(PageIndex, PageSize);
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

        //get save
        [HttpPost]
        public void Insert_T74025(T74025 _t74025)
        {
            repository.Get_T74025(_t74025);
        }
        //delete
        [HttpPost]
        public ActionResult Delete_T74025(int T_ITEM_BRA_ID)
        {
            var del = repository.Delete_T74025(T_ITEM_BRA_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}