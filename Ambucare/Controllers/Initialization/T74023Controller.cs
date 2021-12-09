using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74023Controller : Controller
    {
        // GET: T74023

        private readonly IT74023 repository;

        public T74023Controller(IT74023 objectRepository)
        {
            repository = objectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveDepartment(T74023 _T74023)
        {
            string msg = "";

            if (_T74023.T_EDU_BOARD_ID == 0)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
            repository.saveEducation(_T74023);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGridData()
        {
            var GridData = repository.getGridData.ToList();

            return Json(GridData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteDepartment(int T_EDU_BOARD_ID)
        {
            repository.deleteDepartment(T_EDU_BOARD_ID);

            var msg = "Data Delete Successfully";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}