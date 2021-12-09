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
    public class T74073Controller : Controller
    {
        private readonly IT74073 repository;
        public T74073Controller(IT74073 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74073
        [HttpPost]
        public ActionResult T74073()
        {
            return View();
        }
        //Dropdown Start
        [HttpPost]
        public ActionResult GetCostTypeData()
        {
            try
            {
                var Cost = repository.GetCostType.ToList();
                return Json(Cost, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //get save
        [HttpPost]
        public ActionResult insert_t74073(T74073 _T74073)
        {
            bool status = repository.InsertOrUpdate(_T74073);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult insert_t74072(T74072 _T74072)
        {
            bool status = repository.InsertOrUpdate72(_T74072);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        //delete
        [HttpPost]
        public ActionResult Delete_T74073(int T_COST_TYPE_DTL_ID)
        {
            var del = repository.Delete(T_COST_TYPE_DTL_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }

        //For Gridview Method Start 
        [HttpPost]
        public ActionResult GetLabelData()
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
                    GD = repository.GetLabelData(PageIndex, PageSize);
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
    }
}