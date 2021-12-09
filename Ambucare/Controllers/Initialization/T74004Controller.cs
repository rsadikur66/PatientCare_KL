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
    public class T74004Controller : Controller
    {
        // GET: T74004

        private readonly IT74004 repository;

        public T74004Controller(IT74004 objectRepository)
        {
            repository = objectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMaritalData()
        {
            var MaritaldData = repository.MaritalData.ToList();

            return Json(MaritaldData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBloodGroupData()
        {
            var bloodGroupdData = repository.BloodGroupData.ToList();

            return Json(bloodGroupdData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGenderData()
        {
            var genderdData = repository.GenderData.ToList();

            return Json(genderdData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReligionData()
        {
            var religionData = repository.ReligionData.ToList();
            return Json(religionData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeTypeData()
        {
            var religionData = repository.EmployeeTypeData.ToList();

            return Json(religionData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNationality()
        {
            var religionData = repository.NationalityData.ToList();
            return Json(religionData, JsonRequestBehavior.AllowGet);
        }
        //GetNationality
        public ActionResult SaveEmployeeInfo(T74004 _t74004)
        {
            string msg = "";
            string entryuser = HttpContext.Session["T_USER_ID"].ToString();
            if (_t74004.T_EMP_ID == 0)
            {
                //msg = "1";
                msg = repository.saveEmployeeInfo(_t74004, entryuser);
            }

            else
            {
                msg = repository.saveEmployeeInfo(_t74004, entryuser);
            }
            //repository.saveEmployeeInfo(_t74004);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //get grid data
        [HttpPost]
        public ActionResult GetEmployeeData()
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
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length);
                    // PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length) + 1;
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
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CheckPassport(T74004 _t74004)
        {
            var passportdata = repository.CheckPassportData(_t74004);
            return Json(passportdata, JsonRequestBehavior.AllowGet);
        }
    }
}