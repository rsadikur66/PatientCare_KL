using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74145Controller : Controller
    {
        // GET: T74145
        private IT74145 repository;
        public T74145Controller(IT74145 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: Q74136
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getGridData()
        {
            try
            {
                var data = repository.getGridData();
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
        public JsonResult Insert(string T_LANG1_NAME, string T_LANG2_NAME)
        {
            try
            {
                var data = repository.insertStatus(Session["T_USER_ID"].ToString(), T_LANG1_NAME, T_LANG2_NAME);
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
        public JsonResult update(string T_LANG1_NAME, string T_LANG2_NAME, string T_DISCH_ID)
        {
            try
            {
                var data = repository.updateStatus(Session["T_EMP_ID"].ToString(), T_LANG1_NAME, T_LANG2_NAME, T_DISCH_ID);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}