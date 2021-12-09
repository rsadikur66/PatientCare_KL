using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Initialization
{
    public class T74028Controller : Controller
    {
        // GET: T74028
        private readonly IT74028 repository;

        public T74028Controller(IT74028 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74028()
        {
            return View();
        }

        //get grid data
        [HttpPost]
        public ActionResult GetRequestData()
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
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length); //1;
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
        public void Add_T74028(T74028 _t74028)
        {
            repository.Add_T74028(_t74028);
        }
        //delete
        [HttpPost]
        public ActionResult Delete_T74028(int T_ITEM_BRA_ID)
        {
            var del = repository.Delete_T74028(T_ITEM_BRA_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}