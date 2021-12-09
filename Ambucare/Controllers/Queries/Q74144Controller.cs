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
    public class Q74144Controller : Controller
    {
        private IQ74144 repository;
        private IError err;
        public Q74144Controller(IQ74144 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: Q74144
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllTeamData()
        {
            try
            {
                var zonCode = Session["T_ZONE_CODE"].ToString();
                var lang = Session["T_LANG"].ToString();
                var data = repository.GetAllTeamData(lang, zonCode);
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