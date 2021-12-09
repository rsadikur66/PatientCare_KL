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
    public class T74045Controller : Controller
    {
        private readonly IT74045 repository;

        public T74045Controller(IT74045 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74045
        public ActionResult Index()
        {
            return View();
        }

        //For Save and Update Start
        [HttpPost]

        public ActionResult Insert_T74045(T74045 _T74045)
        {
            string mesg = "";

            if (_T74045.T_SUPPLIER_ID == 0)
            {
                mesg = "Data Save Successfully";
            }
            else
            {
                mesg = "Data Update Successfully";
            }
            repository.Insert_T74045(_T74045);
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }
        //For Save and Update End

        //For Deleted Methord Start
        [HttpPost]
        public ActionResult Deleted_T74045(int T_SUPPLIER_ID)
        {
            var del = repository.Deleted_T74045(T_SUPPLIER_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
        //For Deleted Methord End


        //For Gridview Method Start 
        [HttpPost]
        public ActionResult GetGridData()
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
        //For Gridview Method End
        
    }
}