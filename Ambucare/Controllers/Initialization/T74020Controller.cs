using AmbucareDAL.Repository.Interface.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;

namespace Ambucare.Controllers.Initialization
{
    public class T74020Controller : Controller
    {
        // GET: T74020

        private readonly IT74020 repository;

        public T74020Controller(IT74020 objectRepository)
        {
            repository = objectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveEducation(T74020 _T74020)
        {
            string msg = "";

            if (_T74020.T_DOC_SPECI_ID == 0)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
            repository.saveEducation(_T74020);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGridData()
        {
            var GridData = repository.getGridData.ToList();

            return Json(GridData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteEducation(int T_DOC_SPECI_ID)
        {
            repository.deleteEducation(T_DOC_SPECI_ID);

            var msg = "Data Delete Successfully";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}