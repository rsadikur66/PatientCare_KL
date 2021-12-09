using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ambucare.Models;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Transaction
{
    public class T74136Controller : Controller
    {
        private IT74136 repository;
        private IError err;
        public T74136Controller(IT74136 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: T74135
        public ActionResult Index()
        {
            return View();
        }
        public async Task<object> GetZone()
        {
            try
            {
                object obj = null;
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    pairs.Add("sp", "GET_ZONE_T02064");
                    pairs.Add("db", "KSMC");
                    FormUrlEncodedContent formContent = new FormUrlEncodedContent(pairs);

                    // HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.PostAsync(APILink.GetDataNoParam + "?json=", formContent);
                    if (response.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        obj = response.Content.ReadAsStringAsync().Result;

                        // Deserializing the response recieved from web api and storing into the Employee list
                        //obj = JsonConvert.DeserializeObject<>(EmpResponse);

                    }
                    // returning the employee list to view
                    return obj;
                }
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<object> GetSite(string P_REGION_CODE)
        {
            try
            {
                object obj = null;
                TRequest tr = new TRequest();
                List<TParam> param = new List<TParam>();
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    //Param List---------------------------------start
                    tr.db = "KSMC";
                    tr.sp = "GET_SITE_T02065";
                    param.Add(new TParam() { Key = "P_REGION_CODE", Value = P_REGION_CODE });
                    //Param List---------------------------------end
                    tr.dict = param;
                    var myContent = JsonConvert.SerializeObject(tr);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync(APILink.GetDataParam + "?json=", byteContent);
                    if (response.IsSuccessStatusCode)
                    { obj = response.Content.ReadAsStringAsync().Result; }
                    return obj;
                }
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
        public JsonResult maxUserId()
        {
            try
            {
                var data = repository.maxUserId();
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
        public JsonResult getEmpList()
        {
            try
            {
                var rolcode = Session["T_ROLE_CODE"].ToString();
                var user = Session["T_USER_ID"].ToString();
                var data = repository.getEmpList(Session["T_LANG"].ToString(), rolcode, user);
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
        public JsonResult getRole()
        {
            try
            {
                var rolcode = Session["T_ROLE_CODE"].ToString();
                var lang = Session["T_LANG"].ToString();
                var data = repository.getRole(lang, rolcode);
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
        public JsonResult getZone()
        {
            try
            {
                var data = repository.getZone(Session["T_LANG"].ToString());
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
        public JsonResult CheckUserId(string userId)
        {
            try
            {
                var data = repository.CheckUserId(userId.ToUpper());
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


        //get save
        [HttpPost]
        public ActionResult saveUser(T74057 _T74057)
        {

            try
            {
                string msg = "";
                var entyuser = Session["T_USER_ID"].ToString();
                msg = repository.saveUser(_T74057, entyuser);
                return Json(msg, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetGridData()
        {
            try
            {
                var zone = Session["T_ZONE_CODE"].ToString();
                var user = Session["T_USER_ID"].ToString();
                var data = repository.GetGridData(Session["T_LANG"].ToString(), user, zone);
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
        public JsonResult getMaxValue(string type)
        {
            try
            {
                var zone = Session["T_ZONE_CODE"].ToString();
                var user = Session["T_USER_ID"].ToString();
                var data = repository.getMaxValue(user, type);
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
    }
}