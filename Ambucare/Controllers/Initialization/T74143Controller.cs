using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74143Controller : Controller
    {
        private IT74143 repository;
        public T74143Controller(IT74143 ObjectIRepository)
        {
            repository = ObjectIRepository;
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
                var zoneCode = Session["T_ZONE_CODE"].ToString();
                var lang = Session["T_LANG"].ToString();
                var data = repository.getAllGridData(lang, zoneCode);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult update_discharged41(string T_REQUEST_ID)
        {
            repository.UpdateT74041(Convert.ToInt32(T_REQUEST_ID));
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult update_loggedOut57(string T_USER_ID)
        {
            repository.UpdateT74057(T_USER_ID);
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}