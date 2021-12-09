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
    public class T74068Controller : Controller
    {
        // GET: T74068
        private readonly IT74068 repository;

        public T74068Controller(IT74068 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74068()
        {
            return View();
        }

        //For Save and Update Start
        [HttpPost]

        public ActionResult Insert_T74068(T74068 _T74068)
        {
            string mesg = "";

            if (_T74068.T_MSG_CODE == 0)
            {
                mesg = "Data Save Successfully";
            }
            else
            {
                mesg = "Data Update Successfully";
            }
            repository.Insert_T74068(_T74068);
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }
        //For Save and Update End

        //For Deleted Methord Start
        [HttpPost]
        public ActionResult Deleted_T74068(int T_MSG_CODE)
        {
            var del = repository.Deleted_T74068(T_MSG_CODE);
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