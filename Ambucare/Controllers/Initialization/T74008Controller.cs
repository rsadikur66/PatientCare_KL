using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74008Controller : Controller
    {
        // GET: T74008

        private readonly IT74008 repository;

        public T74008Controller(IT74008 objectRepository)
        {
            repository = objectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveProType(T74008 _T74008)
        {
            string msg = "";
            if (_T74008.T_TYPE_ID == 0)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
            repository.saveProType(_T74008);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetGridData()
        {
            var GridData = repository.getGridData.ToList();

            return Json(GridData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProd(int T_TYPE_ID)
        {
            repository.deleteProd(T_TYPE_ID);
            var msg = "Data Delete Successfully";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}