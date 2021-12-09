using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74132Controller : Controller
    {
        private IT74132 repository;
        private IError err;
        public T74132Controller(IT74132 ObjectIRepository,IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRole(string P_USER_ID)
        {
            try
            {
                var data = repository.GetRole(P_USER_ID);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetUserType(string P_TYPE)
        {
            try
            {
                var data = repository.GetUserType(P_TYPE);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetUserList(string P_USER_TYPE, int P_EMP_TYP_ID, string P_USER_STATUS, int P_FORM_TYPE_ID,string siteCode)
        {
            try
            {
                var data = repository.GetUserList(P_USER_TYPE, P_EMP_TYP_ID, P_USER_STATUS, P_FORM_TYPE_ID, siteCode);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetFormType(int? P_ROLE_CODE, string P_USER_ID)
        {
            try
            {
                var data = repository.GetFormType(P_ROLE_CODE, P_USER_ID);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetFormList(int P_FORM_TYPE_ID,int P_ROLE_CODE, string P_USER_ID,string user)
        {
            try
            {
                var data = repository.GetFormList(P_FORM_TYPE_ID, P_ROLE_CODE, P_USER_ID, user);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public ActionResult Insert(List<T74066> t74066, string status)
        {
            try
            {
                var data = repository.Insert(t74066, status);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetFdataByUser(string fmCode, string rCode, string uData)
        {
            try
            {
                var data = repository.GetFdataByUser(fmCode,  rCode,  uData);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetSiteData()
        {
            try
            {
                var site = "";
                 
                var rol=  Session["T_ROLE_CODE"].ToString();
                if (rol == "23")
                {
                    site = "";
                }
                else
                {
                    site = Session["T_ZONE_CODE"].ToString();
                }

                var data = repository.GetSiteData(site);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //getSiteData
    }
}