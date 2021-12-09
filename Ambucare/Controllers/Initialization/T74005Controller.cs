using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74005Controller : Controller
    {
        // GET: T74005

        private readonly IT74005 repository;

        public T74005Controller(IT74005 objectRepository)
        {
            repository = objectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveEmployee(T74005 _T74005)
        {
            string msg = "";
            if (_T74005.T_EMP_TYP_ID == 0)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
            repository.saveEmployee(_T74005);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGridData()
        {
            var GridData = repository.getGridData.ToList();

            return Json(GridData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteEmType(int T_EMP_TYP_ID)
        {
            repository.deleteEmType(T_EMP_TYP_ID);

            var msg = "Data Delete Successfully";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        //GetGridData
    }
}