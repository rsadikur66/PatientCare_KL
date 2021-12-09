using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Transaction;
using Ninject.Activation;

namespace Ambucare.Controllers.Transaction
{
    public class T74115Controller : Controller
    {
        private readonly IT74115 repository;

        public T74115Controller(IT74115 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74115
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GridData(string TypeID)
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
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    //int recordsTotal = 0;
                    //DataTable GridData = new DataTable();
                    //DataTable CountValue = new DataTable();
                    IEnumerable GD = searchValue == ""? repository.GridData(PageIndex, PageSize, TypeID): repository.GridData(searchValue, PageIndex, PageSize, TypeID);
                    int recordsTotal = repository.GridData(searchValue, TypeID);

                //if (searchValue == "")
                //    {
                //        GD = repository.GridData(PageIndex, PageSize, TypeID);
                //        cnt = repository.GridData(searchValue, TypeID);
                //    }
                //    else
                //    {
                //        GD = repository.GridData(searchValue, PageIndex, PageSize, TypeID);
                //        cnt = repository.GridData(searchValue, TypeID);
                //    }
                //    recordsTotal = cnt;

                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

                

    }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}