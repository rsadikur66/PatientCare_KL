using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;

namespace Ambucare.Controllers.Initialization
{
    public class T74151Controller : Controller
    {
        private readonly IT74151 _repository;
        public T74151Controller(IT74151 objectIRepository)
        {
            _repository = objectIRepository;
        }

        // GET: T74116 Table
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetRespData()
        {
            try
            {
                var data = _repository.GetRespData();
                var jsonString = JsonConvert.SerializeObject(data);
                return Json(jsonString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult InsertResp(T74116 t74116)
        {
            try
            {
                var user = Session["T_USER_ID"].ToString();
                var data = _repository.InsertResp(t74116,user);
                var jsonStr = JsonConvert.SerializeObject(data);
                return Json(jsonStr, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}