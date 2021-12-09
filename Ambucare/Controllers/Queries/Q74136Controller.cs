using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Queries;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Queries
{
    public class Q74136Controller : Controller
    {
        private IQ74136 repository;
        private IError err;
        public Q74136Controller(IQ74136 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: Q74136
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetNotificationData()
        {
            try
            {
                var zonCode = Session["T_ZONE_CODE"].ToString();
                var lang = Session["T_LANG"].ToString();
                var data = repository.GetNotificationData(lang, zonCode);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
public JsonResult GetCriterias()
        {
            try
            {
                var data = repository.GetCriterias(Session["T_ROLE_CODE"].ToString(),Session["T_LANG"].ToString());
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}