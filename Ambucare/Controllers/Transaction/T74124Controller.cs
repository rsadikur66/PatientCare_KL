using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74124Controller : Controller
    {
        private readonly IT74124 repository;

        public T74124Controller(IT74124 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74124
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAmbulance()
        {
            var ambulanceId = repository.AmbulanceId(Session["T_USER_ID"].ToString());
            var employeeId = repository.EmployeeId(Session["T_USER_ID"].ToString());
            var data = repository.GetAmbulance(Convert.ToInt32(ambulanceId), Convert.ToInt32(employeeId));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetPatint(int ambId)
        {
            var data = repository.GetPatint(ambId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}