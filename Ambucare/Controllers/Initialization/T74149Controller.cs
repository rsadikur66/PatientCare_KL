using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ambucare.Models;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74149Controller : Controller
    {
        private IT74149 repository;
        public T74149Controller(IT74149 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }

        // GET: T74149
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Insert(T06301 _t06301)
        {
            repository.insertStatus(_t06301);
        }

        public JsonResult getGridData2()
        {
            try
            {
                var data = repository.getGridAllData();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                //var jsonResult = 
                   return Json(JSONString, JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
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