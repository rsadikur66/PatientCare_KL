using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;
using System.Linq.Dynamic;

namespace Ambucare.Controllers.Transaction
{
    public class T74111Controller : Controller
    {
        // GET: PriceSetup
        public ActionResult PriceSetup()
        {
            return View();
        }

        private readonly IT74111 repository;

        public T74111Controller(IT74111 _repository)
        {
            this.repository = _repository;
        }
        //For Save and Update Start
        [HttpPost]

        public void Insert_T74073(List<T74089> _T74089)
        {
           repository.Insert_T74073(_T74089);
        }
        //For Save and Update End

        //Ambulance List Data Method Start
        [HttpPost]
        public ActionResult GetAmbulanceDropdownList()
        {
            var list = repository.GetAmbulanceDropdownList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //For Gridview Method Start 
        [HttpPost]
        public ActionResult GetGridData(Int32 CompId)
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
                    GD = repository.GetGridData(PageIndex, PageSize, CompId);                   
                }
                else
                {
                    GD = repository.GetGrid_Data_Search(searchValue, PageIndex, PageSize, CompId);
                }
                cnt = repository.GetGridData_Search_Count(searchValue, CompId);
                recordsTotal = cnt;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    //for make sort simpler we will add Syste.Linq.Dynamic reference
                    GD = GD.OrderBy(sortColumn + " " + sortColumnDir);
                }

               // recordsTotal = GD.Count();
                //GD = GD.Skip(skip).Take(pageSize);
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