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
    public class T74016Controller : Controller
    {
        private readonly IT74016 repository;

        public T74016Controller(IT74016 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74016
        public ActionResult T74016()
        {
            return View();
        }

        //For Save and Update Start
        [HttpPost]

        public ActionResult Insert_T74016(T74016 _T74016)
        {
            string mesg = "";

            if (_T74016.T_CURRENCY_ID == 0)
            {
                mesg = "Data Save Successfully";
            }
            else
            {
                mesg = "Data Update Successfully";
            }
            repository.Insert_T74016(_T74016);
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }
        //For Save and Update End

        //For Deleted Methord Start
        [HttpPost]
        public ActionResult Deleted_T74016(int T_CURRENCY_ID)
        {
            var del = repository.Deleted_T74016(T_CURRENCY_ID);
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
                int recordsTotal = 0;
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