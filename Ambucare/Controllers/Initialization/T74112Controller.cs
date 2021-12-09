using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74112Controller : Controller
    {
        
        // GET: GMNR
        public ActionResult T74112()
        {
            return View();
        }

        // GET: T74050
        private readonly IT74112 repository;

        public T74112Controller(IT74112 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74050()
        {
            return View();
        }


        //GET DATA FOR TABLES

        [HttpPost]
        //For GET Data with Searching and Pagination start
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

        public ActionResult GetMaritalGrid()
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
                    GD = repository.GetGridDataMarital(PageIndex, PageSize);
                    cnt = repository.GetGridDataMarital_Search_Count(searchValue);
                }
                else
                {
                    GD = repository.GetGridMarital_Data_Search(searchValue, PageIndex, PageSize);
                    cnt = repository.GetGridDataMarital_Search_Count(searchValue);
                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetNationalityGrid()
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
                    GD = repository.GetGridDataNationality(PageIndex, PageSize);
                    cnt = repository.GetGridDataNationality_Search_Count(searchValue);
                }
                else
                {
                    GD = repository.GetGridNationality_Data_Search(searchValue, PageIndex, PageSize);
                    cnt = repository.GetGridDataNationality_Search_Count(searchValue);
                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetReligionGrid()
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
                    GD = repository.GetGridDataReligion(PageIndex, PageSize);
                    cnt = repository.GetGridDataReligion_Search_Count(searchValue);
                }
                else
                {
                    GD = repository.GetGridReligion_Data_Search(searchValue, PageIndex, PageSize);
                    cnt = repository.GetGridDataReligion_Search_Count(searchValue);
                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //GET DATA FOR TABLES End


        [HttpPost]
        //insert and update start
        public ActionResult InsertOrUpdateT74050(T74050 _t74050)
        {
            bool status = repository.InsertOrUpdateT74050(_t74050);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertOrUpdateT74051(T74051 t74051)
        {
            bool status = repository.InsertOrUpdateT74051(t74051);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertOrUpdateT02003(T02003 t02003)
        {
            bool status = repository.InsertOrUpdateT02003(t02003, Convert.ToString(Session["T_USER_ID"]));
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertOrUpdateT74059(T74059 t74059)
        {
            bool status = repository.InsertOrUpdateT74059(t74059);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        
        //Delete Start
        public ActionResult deleteData(int T_SEX_CODE)
        {
            var del = repository.Delete(T_SEX_CODE);
            return Json(del, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteDataMarital(int T_MRTL_STATUS_CODE)
        {
            var del = repository.DeleteMarital(T_MRTL_STATUS_CODE);
            return Json(del, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteDataNationality(int T_NTNLTY_ID)
        {
            var del = repository.DeleteNationality(T_NTNLTY_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteDataReligion(int T_RLGN_CODE)
        {
            var del = repository.DeleteReligion(T_RLGN_CODE);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
       
    }
}
