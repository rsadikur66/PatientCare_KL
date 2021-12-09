using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Transaction
{
    public class T74138Controller : Controller
    {
        private IT74138 repository;
        private IError err;
        public T74138Controller(IT74138 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        public ActionResult T74138()
        {
            return View();
        }
        // [HttpPost]
        //public ActionResult Gender()
        //{
        //    var data = repository.Gender();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult GetAllUserLatlong()
        {
            try
            {
                var zonCode = Session["T_ZONE_CODE"].ToString();
                var data = repository.GetAllUserLatlong(zonCode);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetLoginUserLatlong()
        {
            try
            {
                var reId = Session["T_REQUEST_ID"].ToString();
                int rquestId = Convert.ToInt32(reId);
                var data = repository.GetLoginUserLatlong(rquestId, Session["T_USER_ID"].ToString());
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpPost]
        //public JsonResult GetAllDataOnMap_T74041(int P_AMBU_REG_ID)
        //{
        //    try
        //    {
        //        var data = repository.GetAllDataOnMap_T74041(P_AMBU_REG_ID);
        //        string JSONString = string.Empty;
        //        JSONString = JsonConvert.SerializeObject(data);
        //        return Json(JSONString, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exc)
        //    {
        //        return Json(exc.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpPost]
        //public JsonResult GetUserIDByAMBRegID(int T_AMBU_REG_ID)
        //{
        //    try
        //    {
        //        var data = repository.GetUserIDByAMBRegID(T_AMBU_REG_ID);
        //        string JSONString = string.Empty;
        //        JSONString = JsonConvert.SerializeObject(data);
        //        return Json(JSONString, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exc)
        //    {
        //        return Json(exc.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpPost]
        //public ActionResult GetPatInfo()
        //{
        //    var data = repository.GetPatInfo();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult Insert_t74046(T74046 _t74046)
        //{
        //    try
        //    {
        //        var data = repository.Insert_t74046(_t74046);
        //        string JSONString = string.Empty;
        //        JSONString = JsonConvert.SerializeObject(data);
        //        return Json(JSONString, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exc)
        //    {
        //        return Json(exc.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public void Insert_t74041(T74041 _t74041)
        //{
        //    _t74041.T_ENTRY_DATE = DateTime.Now;
        //    repository.Insert_t74041(_t74041);
        //}

        //public void GPS_Insert(decimal latitude, decimal longitude)
        //{
        //  //  var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
        //    var user = Session["T_USER_ID"].ToString();
        //    repository.GPS_Insert(latitude, longitude, user);

        //}

        [HttpPost]
        public JsonResult getArrivedDuration(int req)
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                var data = repository.getArrivedDuration(user, req);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult getDestination(int e)
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                var data = repository.getDestination(Session["T_USER_ID"].ToString(), e);
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
        public JsonResult setHandoverHospital(string site)
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                var data = repository.setHandoverHospital(Session["T_USER_ID"].ToString(), site);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult getDocSite()
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                var data = repository.getDocSite(user);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult saveEvent(string evt, int req, decimal? lat, decimal? lng)
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                var data = repository.saveEvent(user, evt, req, lat, lng);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult saveRequestHospitalbyTeam(int requestID, string hosID)
        {
            try
            {

                var data = repository.saveRequestHospitalbyTeam(requestID, hosID);
                return Json(data, JsonRequestBehavior.AllowGet);
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