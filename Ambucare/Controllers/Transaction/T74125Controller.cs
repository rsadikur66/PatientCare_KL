using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ambucare.Models;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Transaction
{
    public class T74125Controller : Controller
    {

        private readonly IT74125 repository;
        private IError err;
        public T74125Controller(IT74125 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: T74125
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetPatientDetails(int patId)
        {
            try
            {
                var userId = Session["T_USER_ID"].ToString();
                var reId = Session["T_REQUEST_ID"].ToString();
                int rquestId = Convert.ToInt32(reId);
                var data = repository.GetPatientDetails(patId, userId, rquestId);
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
        public ActionResult GetBill(int patId)
        {
            try
            {
                var data = repository.GetBill(patId);
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
        public ActionResult GetBillDetails(int request)
        {
            try
            {
                var data = repository.GetBillDetails(request);
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
        public ActionResult GetIssueDetails(int request)
        {
            try
            {
                var data = repository.GetIssueDetails(request);
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
        public ActionResult GetIssueSumary(int request)
        {
            try
            {
                var data = repository.GetIssueSumary(request);
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
        //GetIssueSumary
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
        public ActionResult PatData(int T_PAT_ID)
        {
            try
            {
                var data = repository.PatData(T_PAT_ID);
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
         
        public async Task<object> SavePatInfo(T74046 t74046, T74041 t74041, T74043 t74043, T02003 t02003, string T_SITE_CODE)
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
                    tr.sp = "INSERT_T03001_T04220_T04215";
                    //param.Add(new TParam() { Key = "@P_BIRTH_DATE", Value = t74046.T_BIRTH_DATE.ToString("dd-MMM-yyyy") });
                    param.Add(new TParam() { Key = "@P_MRTL_STATUS", Value = t74046.T_MRTL_STATUS.ToString() });
                    param.Add(new TParam() { Key = "@P_FAMILY_LANG1_NAME", Value = t74046.T_FAMILY_LANG1_NAME });
                    param.Add(new TParam() { Key = "@P_FAMILY_LANG2_NAME", Value = t74046.T_FAMILY_LANG2_NAME });
                    param.Add(new TParam() { Key = "@P_FATHER_LANG1_NAME", Value = t74046.T_FATHER_LANG1_NAME });
                    param.Add(new TParam() { Key = "@P_FATHER_LANG2_NAME", Value = t74046.T_FATHER_LANG2_NAME });
                    param.Add(new TParam() { Key = "@P_GFATHER_LANG1_NAME", Value = t74046.T_GFATHER_LANG1_NAME });
                    param.Add(new TParam() { Key = "@P_GFATHER_LANG2_NAME", Value = t74046.T_GFATHER_LANG2_NAME });
                    param.Add(new TParam() { Key = "@P_FIRST_LANG1_NAME", Value = t74046.T_FIRST_LANG1_NAME });
                    param.Add(new TParam() { Key = "@P_FIRST_LANG2_NAME", Value = t74046.T_FIRST_LANG2_NAME });
                    param.Add(new TParam() { Key = "@P_MOTHER_LANG1_NAME", Value = t74046.T_MOTHER_LANG1_NAME });
                    param.Add(new TParam() { Key = "@P_MOTHER_LANG2_NAME", Value = t74046.T_MOTHER_LANG2_NAME });
                    param.Add(new TParam() { Key = "@P_NTNLTY_CODE", Value = t02003.T_NTNLTY_CODE });
                    param.Add(new TParam() { Key = "@P_NTNLTY_ID", Value = t74046.T_NTNLTY_ID.ToString() });
                    param.Add(new TParam() { Key = "@P_PASSPORT_NO", Value = t74046.T_PASSPORT_NO });
                    param.Add(new TParam() { Key = "@P_PHONE_HOME", Value = t74046.T_PHONE_HOME });
                    param.Add(new TParam() { Key = "@P_MOBILE_NO", Value = t74046.T_MOBILE_NO });
                    param.Add(new TParam() { Key = "@P_POSTAL_CODE", Value = t74046.T_POSTAL_CODE });
                    param.Add(new TParam() { Key = "@P_RLGN_CODE", Value = t74046.T_RLGN_CODE.ToString() });
                    param.Add(new TParam() { Key = "@P_GENDER", Value = t74046.T_SEX_CODE.ToString() });
                    param.Add(new TParam() { Key = "@P_PAT_ID", Value = t74046.T_PAT_ID.ToString() });

                    param.Add(new TParam() { Key = "@P_CHEIF_COMPLAINT", Value = t74041.T_CH_COMP.ToString() });
                    param.Add(new TParam() { Key = "@P_SITE_CODE", Value = T_SITE_CODE });
                    param.Add(new TParam() { Key = "@P_PROBELM_SPEC", Value = t74041.T_PROB_TYPE_ID.ToString() });

                    param.Add(new TParam() { Key = "@P_HIGHT", Value = t74043.T_HIGHT.ToString() });
                    param.Add(new TParam() { Key = "@P_WEIGHT", Value = t74043.T_WEIGHT.ToString() });
                    param.Add(new TParam() { Key = "@P_BP_FROM", Value = t74043.T_BP_SYS.ToString() });
                    param.Add(new TParam() { Key = "@P_BP_TO", Value = t74043.T_BP_DIA.ToString() });
                    param.Add(new TParam() { Key = "@P_TEMP", Value = t74043.T_TEMP.ToString() });
                    param.Add(new TParam() { Key = "@P_PULS", Value = t74043.T_PULS.ToString() });

                    tr.dict = param;
                    //Param List---------------------------------end
                    var myContent = JsonConvert.SerializeObject(tr);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync(APILink.SaveDataSmallData + "?json=", byteContent);
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
        public ActionResult UpdateT74046(int T_PAT_ID,string T_DISCH_DEST)
        {
            try
            {
                repository.UpdateT74046(T_PAT_ID, T_DISCH_DEST);
                return Json("", JsonRequestBehavior.AllowGet);
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
        public ActionResult UpdateT74041(T74041 t74041)
        {
            try
            {
                var data = repository.UpdateT74041(t74041);
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
        public ActionResult GetDischargeReason()
        {
            try
            {
                var data = repository.GetDischargeReason();
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
        public ActionResult verifySummeryReport(int requestId)
        {
            try
            {
                var data = repository.verifySummeryReport(requestId);
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