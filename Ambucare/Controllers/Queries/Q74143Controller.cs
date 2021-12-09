using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Queries;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Queries
{
    public class Q74143Controller : Controller
    {
        // GET: Q74143
        private IQ74143 repository;
        private IError err;
        public Q74143Controller(IQ74143 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: T74143
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getAllGridData()
        {
            try
            {
                var data = repository.getAllGridData(Session["T_LANG"].ToString());
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
        [HttpPost]
        public ActionResult update_discharged41(string T_REQUEST_ID)
        {
            try
            {
                repository.UpdateT74041(Convert.ToInt32(T_REQUEST_ID));
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult update_loggedOut57(string T_USER_ID)
        {
            try
            {
                repository.UpdateT74057(T_USER_ID);
                return Json("", JsonRequestBehavior.AllowGet);
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