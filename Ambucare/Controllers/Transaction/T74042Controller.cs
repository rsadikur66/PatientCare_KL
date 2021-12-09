using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74042Controller : Controller
    {
        // GET: T74042

        private IT74042 repository;
        public T74042Controller(IT74042 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetAllHospitalLatlong()
        {
            var zonCode = Session["T_ZONE_CODE"].ToString();
            var data = repository.GetAllHospitalLatlong(zonCode);
            // string JSONString = string.Empty;
            // JSONString = JsonConvert.SerializeObject(data);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPatientInformation(int requestId)
        {
            var data = repository.GetPatientInformation(requestId);
            // string JSONString = string.Empty;
            // JSONString = JsonConvert.SerializeObject(data);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateByOperator(int requestId, string doc)
        {
            string lang = Session["T_LANG"].ToString();
            var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
            var data = repository.UpdateByOperator(requestId, doc, user, lang);
            // string JSONString = string.Empty;
            // JSONString = JsonConvert.SerializeObject(data);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}