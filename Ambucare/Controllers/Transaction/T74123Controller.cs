using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74123Controller : Controller
    {
        private readonly IT74123 repository;

        public T74123Controller(IT74123 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }

        // GET: T74123
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPatients()
        {
            var data = repository.GetPatients();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //PriscriptionData
        [HttpPost]
        public ActionResult PatientDetalisData(int Id)
        {
            var data = repository.PatientDetalisData(Id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PriscriptionData(int Id)
        {
            var data = repository.PriscriptionData(Id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BodyMeasurementData(int Id)
        {
            var data = repository.BodyMeasurementData(Id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}