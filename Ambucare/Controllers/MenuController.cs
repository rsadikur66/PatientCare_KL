using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ambucare.Models;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Menu;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;


namespace Ambucare.Controllers
{
    public class MenuController : Controller
    {
        private IMenu repository;
        private IError err;

        public MenuController(IMenu ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MenuData(int T_FORM_TYPE_ID)
        {
            try
            {
                int lang = Int32.Parse(Session["T_LANG"].ToString());
                var menuData = repository.MenuData(Session["T_USER_ID"].ToString(), Int32.Parse(Session["T_ROLE_CODE"].ToString()), T_FORM_TYPE_ID, lang);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(menuData);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetAllMenuData()
        {
            try
            {
                int lang = Int32.Parse(Session["T_LANG"].ToString());
                var menuData = repository.GetAllMenuData(Session["T_USER_ID"].ToString(),
                    Int32.Parse(Session["T_ROLE_CODE"].ToString()), lang);
                string jsonString = string.Empty;
                jsonString = JsonConvert.SerializeObject(menuData);
                return Json(jsonString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UserName()
        {
            try
            {
                string userName = "";
                if (!string.IsNullOrEmpty(Session["T_USER_NAME"] as string))
                {
                    userName = Session["T_USER_NAME"].ToString();
                }
                else
                {
                    userName = "Session Out";
                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(userName);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UserId()
        {
            try
            {
                string userId = "";
                if (!string.IsNullOrEmpty(Session["T_USER_ID"] as string))
                {
                    userId = Session["T_USER_ID"].ToString();
                }
                else
                {
                    userId = "NaN";
                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(userId);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult LabelData(string T_FORM_CODE, string T_LABEL_NAME)
        {

            try
            {
                var lblData = repository.LabelData(T_FORM_CODE, T_LABEL_NAME);
                return Json(lblData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public void GPS_Insert(decimal latitude, decimal longitude)
        {
            string msg = "";
            try
            {
                var user = Session["T_USER_ID"].ToString();
                repository.GPS_Insert(latitude, longitude, user);
            }
            catch (Exception e)
            {
                msg = e.Message;
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
            }



        }

        [HttpPost]
        public void Save_t74041(string userid, string lat, string lon, string address)
        {

            try
            {
                repository.Save_t74041(userid, lat, lon, address);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message); 
            }
        }

        public JsonResult GetLatLong(string T_USER_ID)
        {

            try
            {
                var LatLong = repository.GetLatLong(T_USER_ID);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(LatLong);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult SaveLatLong(T74026 t74026)
        {
            try
            {
                var data = t74026.T_USER_ID = Session["T_USER_ID"].ToString();
                repository.SaveLatLong(t74026);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult GetAllUserMsg(string T_FORM_CODE)
        {
            try
            {
                var msg = repository.GetAllUserMsg(Session["T_LANG"].ToString(), T_FORM_CODE);
                string JSONstring = JsonConvert.SerializeObject(msg);
                return Json(JSONstring, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getFormName(string code)
        {
            try
            {
                var msg = repository.getFormName(Session["T_LANG"].ToString(), code);
                string JSONstring = JsonConvert.SerializeObject(msg);
                return Json(JSONstring, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getAPILink()
        {
            try
            {
                var msg = APILink.Plink;
                string JSONstring = JsonConvert.SerializeObject(msg);
                return Json(JSONstring, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult updateForm(string form)
        {
            try
            {
                var msg = repository.updateForm(Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), form);
                string JSONstring = JsonConvert.SerializeObject(msg);
                return Json(JSONstring, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }



    }
}