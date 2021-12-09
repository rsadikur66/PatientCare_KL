using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Data;
using System.Collections;

namespace Ambucare.Controllers.Initialization
{
    public class T74035Controller : Controller
    {
        private readonly IT74035 repository;

        // GET: T74035
        public ActionResult Index()
        {
            return View();
        }

        public T74035Controller(IT74035 ObjectRepository)
        {
            repository = ObjectRepository;
        }

        public ActionResult GetGridDataT74035()
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
                    GD = repository.GetGridDataT74035(PageIndex, PageSize);
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

        public ActionResult InsertUpdate(T74035 _T74035)
        {
            string mesg = "";

            if (_T74035.T_DEPET_ID == 0)
            {
                mesg = "Data Save Successfully";
            }
            else
            {
                mesg = "Data Update Successfully";
            }
            repository.InsertUpdate(_T74035);
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteData(int T_DEPET_ID)
        {
            var del = repository.deleteData(T_DEPET_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}