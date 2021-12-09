using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;


namespace Ambucare.Controllers.Initialization
{
    public class T74052Controller : Controller
    {
        private readonly IT74052 repository;

        public T74052Controller(IT74052 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }

    public ActionResult T74052()
        {
            return View();
        }

        public ActionResult AddZone(T74052 Z)
        {
            string msg = "";
            if (Z.T_ZONE_CODE == null)
            {
                msg = "Data Save Successfully";
            }
            else
            {
                msg = "Data Update Successfully";
            }
             repository.AddZone(Z);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetZoneData()
        {
           
                var list = repository.GetZoneData.ToList();
          
            return Json(list, JsonRequestBehavior.AllowGet);
            //var data = repository.All.ToList();
           // return View();
        }
        [HttpPost]
        public ActionResult DeleteZone(string T_ZONE_CODE)
        {
            repository.DeleteZone(T_ZONE_CODE);

            var msg = "Data Delete Successfully";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}