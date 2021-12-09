using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74146Controller : Controller
    {
        // GET: T74145
        private IT74146 repository;
        public T74146Controller(IT74146 ObjectIRepository)
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
        public JsonResult getFormCode()
        {
            try
            {
                var data = repository.getFormCode();
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
        public JsonResult Insert(string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG, string T_FORM_CODE, string T_ACTV_STTS)
        {
            try
            {
                var data = repository.insertStatus(T_MSG_CODE, T_LANG1_MSG, T_LANG2_MSG, Session["T_USER_ID"].ToString(), T_FORM_CODE, T_ACTV_STTS);
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
        public JsonResult update(string T_MSG_ID, string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG, string T_FORM_CODE, string T_ACTV_STTS)
        {
            try
            {
                var data = repository.updateStatus(T_MSG_ID, T_MSG_CODE, T_LANG1_MSG, T_LANG2_MSG, Session["T_USER_ID"].ToString(), T_FORM_CODE, T_ACTV_STTS);
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