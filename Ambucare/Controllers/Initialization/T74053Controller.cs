using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Setup;
using AmbucareDAL.Models;


namespace Ambucare.Controllers.Initialization
{
    public class T74053Controller : Controller
    {
        private readonly IT74053 repository;

        public T74053Controller(IT74053 objectRepository)
        {
            repository = objectRepository;
        }
        // GET: T74002
        public ActionResult T74053()
        {
            return View();
        }

        public ActionResult GetZoneCode()
        {
            var ZoneCode = repository.getZoneCode.ToList();

            return Json(ZoneCode, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHosptalData()
        {
              var HospitaData = repository.getHospital();

            return Json(HospitaData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddHospital(T74053 H)
        {
            string msg = "";
            if (H.T_SITE_NO == null)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
            repository.AddHospital(H);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteHosPital(int T_SITE_NO)
        {
            repository.DeleteHosPital(T_SITE_NO);

            var msg = "Data Delete Successfully";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}