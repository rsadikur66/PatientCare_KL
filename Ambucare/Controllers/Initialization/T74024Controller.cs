using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74024Controller : Controller
    {
        // GET: T74024
        private readonly IT74024 repository;

        public T74024Controller(IT74024 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74024()
        {
            return View();
        }

        //get grid data
        [HttpPost]
        public ActionResult Get_t74024()
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
                }
                PageSize = Convert.ToInt32(length);
                List<T74024> allCustomer = new List<T74024>();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                // DataTable SearchValue = new DataTable();
                //if (searchValue == "")
                //{
                //    GridData = repository.GetItemBrandData();//PageIndex, PageSize);
                //    //CountValue = repo.GetPatientData_Search_Count(searchValue, PageIndex, PageSize);
                //}
                //else
                //{
                //    GridData = repository.GetItemBrandData();//PageIndex, PageSize);
                //    //CountValue = repo.GetPatientData_Search_Count(searchValue, PageIndex, PageSize);
                //}
                List<T74024> v = new List<T74024>();
                if (searchValue == "")
                {
                    v = repository.Get_T74024.ToList();
                }
                else
                {
                    v = repository.Get_T74024_Search(searchValue, PageIndex, PageSize).ToList();
                }

                //sort
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    //for make sort simpler we will add Syste.Linq.Dynamic reference
                    //v = v; //.OrderBy(sortColumn + " " + sortColumnDir);
                }

                recordsTotal = Convert.ToInt32(v.Count.ToString());//v.Count();
                allCustomer = v.Take(pageSize).ToList();                                                                     // allCustomer = v.Take(pageSize).ToList();//v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = allCustomer }, JsonRequestBehavior.AllowGet);
                // return Json(v,JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }

        //get save
        [HttpPost]
        public void Add_t74024(T74024 _t74024)
        {
            repository.Add_T74024(_t74024);
        }
        //delete
        [HttpPost]
        public ActionResult Delete_T74024(int T_RESULT_TYP_ID)
        {
            var del = repository.Delete_T74024(T_RESULT_TYP_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}