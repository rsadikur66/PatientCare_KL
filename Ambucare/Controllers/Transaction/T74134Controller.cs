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
    public class T74134Controller : Controller
    {

        private readonly IT74134 repository;
        private IError err;
        public T74134Controller(IT74134 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: T74134
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetPatInfo()
        {
            try
            {
                var reId = Session["T_REQUEST_ID"].ToString();
                int dd = Convert.ToInt32(reId);
                var Data = repository.GetPatInfo(dd);
                return Json(Data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetVitalSign(int reqId)
        {
            try
            {
                var Data = repository.GetVitalSign(reqId);
                return Json(Data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetItem(int ambu)
        {
            try
            {
                var reId = Session["T_REQUEST_ID"].ToString();
                int rquestId = Convert.ToInt32(reId);
                var Data = repository.GetItem(ambu, rquestId);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(Data);
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
        public ActionResult GetStockData(int ambu,int item)
        {
            try
            {
                var Data = repository.GetStockData(ambu, item);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(Data);
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
        public ActionResult SaveData(T74036 t74036, List<T74037> t74037)
        {
            try
            {
                var lang = Session["T_LANG"].ToString();
                var reId = Session["T_REQUEST_ID"].ToString();
                int rquestId = Convert.ToInt32(reId);
                var Data = repository.SaveData(lang, rquestId, t74036, t74037);
                return Json(Data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetPreviousMedicine(int requID)
        {
            try
            {
                var Data = repository.GetPreviousMedicine(requID);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(Data);
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
        public ActionResult GetServiceData(int requID)
        {
            try
            {
                var Data = repository.GetServiceData(requID);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(Data);
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
        public ActionResult SaveServiceData(T74074 t74074, List<T74079> t74079)
        {
            try
            {
                var lang = Session["T_LANG"].ToString();
                var reId = Session["T_REQUEST_ID"].ToString();
                int rquestId = Convert.ToInt32(reId);
                var Data = repository.SaveServiceData(lang, rquestId, t74074, t74079);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //GetServiceData 
    }
}