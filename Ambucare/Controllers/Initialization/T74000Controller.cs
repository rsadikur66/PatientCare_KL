using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74000Controller : Controller
    {
        private readonly IT74000 repository;

        public T74000Controller(IT74000 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }

        public ActionResult T74000()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertOrUpdate(T74000 labelField)
        {
            bool status = repository.InsertOrUpdate(labelField);
            return Json(status, JsonRequestBehavior.AllowGet); ;
            
        }

        //Dropdown Start
        [HttpPost]
        public ActionResult GetFormData()
        {
            try
            {
                var data = repository.GetFormData.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //Get Label Data Start
        [HttpPost]
        public ActionResult GetLabelTextData(string T_FORM_CODE)
        {
            int lang = Int32.Parse(Session["T_LANG"].ToString());
            var GridData = repository.getLabelTextData(T_FORM_CODE, lang);
            return Json(GridData, JsonRequestBehavior.AllowGet);
         }
        [HttpPost]
        public ActionResult MenuLabel(string T_FORM_CODE,string e)
        {
            Session["T_LANG"] = e;
            int lang = Int32.Parse(e);
            var GridData = repository.getLabelTextData(T_FORM_CODE, lang);
            return Json(GridData, JsonRequestBehavior.AllowGet);
         }
        
        //Get Label Data End

        [HttpPost]
        public ActionResult deleteData(int T_LABEL_ID)
            {
            var del = repository.Delete(T_LABEL_ID);
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
        //For Gridview Method End

    }
}