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
    public class T74009Controller : Controller
    {
        // GET: T74009
        private readonly IT74009 repository;

        public T74009Controller(IT74009 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74009()
        {
            return View();
        }

        //get grid data
        [HttpPost]
        public ActionResult GetMedicalData()
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

        //save
        [HttpPost]
        public ActionResult AddMedicalData(T74009 _T74009)
        {
            string mesg = "";
            if (_T74009.T_MEDIC_TYPE_ID == 0)
            {
                mesg = "Data Save Successfully";
            }
            else
            {
                mesg = "Data Update Successfully";
            }
            repository.AddMedicalType(_T74009);
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }

        //delete
        [HttpPost]
        public ActionResult Delete_T74009(int T_MEDIC_TYPE_ID)
        {
            var del = repository.Delete_T74009(T_MEDIC_TYPE_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}