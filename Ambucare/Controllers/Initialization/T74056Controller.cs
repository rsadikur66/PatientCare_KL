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
    public class T74056Controller : Controller
    {
        private readonly IT74056 repository;
        public T74056Controller(IT74056 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74056()
        {
            return View();
        }
        [HttpPost]
        public void Insert(T74056 _T74056)
        {
           repository.Insert(_T74056);
        }
        [HttpPost]
        public ActionResult Delete(int T_TRIAGE_SEQ)
        {
            var del = repository.Delete(T_TRIAGE_SEQ);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetGridData()
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
    }
}