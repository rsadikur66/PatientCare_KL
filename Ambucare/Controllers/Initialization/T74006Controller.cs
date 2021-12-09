using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74006Controller : Controller
    {
        // GET: T74006

        private readonly IT74006 repository;

        public T74006Controller(IT74006 objectRepository)
        {
            repository = objectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveEmpDesignation(T74006 _T74006)
        {
            string msg = "";
            if (_T74006.T_EMP_DESI_ID == 0)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
            repository.saveEmpDes(_T74006);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetGridData()
        {
            var GridData = repository.getGridData.ToList();

            return Json(GridData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteEmp(int T_EMP_DESI_ID)

        {
            repository.DeleteEmpDes(T_EMP_DESI_ID);

            var msg = "Data Delete Successfully";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
       
    }
}