using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74069Controller : Controller
    {
        // GET: T74069
        private readonly IT74069 repository;

        public T74069Controller(IT74069 objectRepository)
        {
            repository = objectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveBloodGroup(T74069 _T74069)
        {
            string msg = "";
            if (_T74069.T_BLD_GROUP_ID == 0)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
            repository.SaveBloodGroup(_T74069);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGridData()
        {
            var GridData = repository.getGridData.ToList();

            return Json(GridData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Int32 blood)
        {
            var del = repository.Delete(blood);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}