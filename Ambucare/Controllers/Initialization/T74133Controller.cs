using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74133Controller : Controller
    {
        private readonly IT74133 repository;
        public T74133Controller(IT74133 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74133
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetZone()
        {
            try
            {
                var data = repository.GetZone();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetStoreListTo()
        {

            var ambulanceId = repository.AmbulanceId(Session["T_USER_ID"].ToString());
            var employeeId = repository.EmployeeId(Session["T_USER_ID"].ToString());
            var store = repository.GetStoreListTo(Convert.ToInt32(ambulanceId), Convert.ToInt32(employeeId));
            return Json(store, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetGridAllData(string ambuId, string zone)
        {
            try
            {
                var data = repository.GetGridAllData(ambuId,zone);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public void Insert_T74133(List<T74096> T74096)
        {
            repository.Insert_T74133(T74096);
        }
    }
}