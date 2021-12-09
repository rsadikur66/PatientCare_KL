using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ambucare.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface;
using FastReport.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Ambucare.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ILogin repo;
        private IError err;
        CommonDAL _cDal = new CommonDAL();
        public AccountsController(ILogin ObjectIRepository, IError errRepo)
        {
            repo = ObjectIRepository;
            err = errRepo;
        }
        
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        //public static bool CheckForInternetConnection()
        //{
        //    try
        //    {
        //        using (var client = new WebClient())
        //        {//CommonDAL c=new CommonDAL();
        //         //int chk = c.Query($"SELECT * FROM T74057").Rows.Count;
        //         //return chk > 0;
        //         //using (client.OpenRead("http://100.43.0.111/"))
        //         //{
        //            return true;
        //            // }

        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        [HttpPost]
        public JsonResult UserLogin(string userId, string Password, string lt, string ln)

        {
            
            int req = 0;
            string message = "";
            //string emessage = "";
            string role = "";
            DataTable dtLog=new DataTable();
            //if (CheckForInternetConnection())
            //{
            //emessage += "1";
            try
            {
                //emessage += "2";
                IEnumerable loginData = repo.GetUserInfo(userId, Password);
                //emessage += loginData;
                //emessage += "3";
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(loginData);
                JArray obj = JArray.Parse(JSONString);
                FastReport.Utils.Config.WebMode = true;
                //emessage += "4";
                if (loginData.Count() != 0)
                {
                    //emessage += "5";
                    Session["T_USER_ID"] = obj[0]["T_USER_ID"].ToString();
                    Session["T_ROLE_CODE"] = obj[0]["T_ROLE_CODE"].ToString();
                    Session["T_USER_NAME"] = obj[0]["EMP_NAME"].ToString();
                    Session["T_COMPANY_ID"] = obj[0]["T_COMPANY_ID"].ToString();
                    Session["T_EMP_ID"] = obj[0]["T_EMP_ID"].ToString();
                    Session["T_SITE_CODE"] = obj[0]["T_SITE_CODE"].ToString();
                    Session["T_ZONE_CODE"] = obj[0]["T_ZONE_CODE"].ToString();
                    Session["T_LANG"] = "2";
                    role = obj[0]["T_ROLE_CODE"].ToString();
                    message = "ok";
                    //emessage += "6";
                    string checkloginlogout = _cDal.CheckLoginLogoutType(Session["T_ROLE_CODE"].ToString());
                    //emessage += "7";
                    DataTable data=  _cDal.Query($"SELECT T_LATITUDE, T_LONGITUDE FROM T74026 WHERE T_USER_ID='{ Session["T_USER_ID"].ToString()}' ORDER BY ID");
                    //emessage += "8";
                    if (data !=null)
                    {
                        var lat =data.Rows[0]["T_LATITUDE"].ToString();
                        var lon =data.Rows[0]["T_LONGITUDE"].ToString();
                        //emessage += "9";
                        try
                        {
                            DataTable dt =_cDal.Query($"SELECT  T_REQUEST_ID FROM T74041 WHERE T_USER_ID='{Session["T_USER_ID"].ToString()}' AND T_DISCH_STATUS IS NULL AND T_ACCEPT_STATUS IS NOT NULL");
                            //emessage += "10";
                            if (dt.Rows.Count>0)
                            {
                                req = Int32.Parse(dt.Rows[0]["T_REQUEST_ID"].ToString());
                            }
                        }
                        catch (Exception e)
                        {
                            message = "fls";
                            ///emessage += e.InnerException != null ? e.InnerException.Message : e.Message;
                        }
                        if (lat !="")
                        {
                            //emessage += "11";
                            _cDal.Command(
                                $"insert into t74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_EVENT_ID,T_REQUEST_ID) values ((SELECT MAX(ID)+1 FROM T74026),'{Session["T_USER_ID"].ToString()}',SYSDATE,'{lat}','{lon}','{checkloginlogout}',DECODE({req},0,null,{req}))");
                            UpdateLogin(userId);
                            //emessage += "12";
                        } else
                        {
                            //emessage += "13";
                            _cDal.Command(
                                $"insert into t74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_EVENT_ID,T_REQUEST_ID) values ((SELECT MAX(ID)+1 FROM T74026),'{Session["T_USER_ID"].ToString()}',SYSDATE,'{lt}','{ln}','{checkloginlogout}',DECODE({req},0,null,{req}))");
                            //emessage += "14";
                            UpdateLogin(userId);
                            //emessage += "15";
                        }

                      
                    }
                    else
                    {
                        //emessage += "16";
                        _cDal.Command(
                            $"insert into t74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_EVENT_ID,T_REQUEST_ID) values ((SELECT MAX(ID)+1 FROM T74026),'{Session["T_USER_ID"].ToString()}',SYSDATE,'{lt}','{ln}','{checkloginlogout}',DECODE({req},0,null,{req}))");
                        //emessage += "17";
                        UpdateLogin(userId);
                       // emessage += "18";
                    }
                }
                else
                {
                    message = "fls";
                   // emessage += "19";
                }

                //if (userId.Length > 4)
                //{

                //}
                //else
                //{
                //    DataTable dt = GetDocInfo(userId, Password);
                //    Session["T_USER_ID"] = dt.Rows[0]["T_LOGIN_NAME"].ToString();
                //    Session["T_ROLE_CODE"] = dt.Rows[0]["T_ROLE_CODE"].ToString();
                //    Session["T_USER_NAME"] = dt.Rows[0]["DOCTOR"].ToString();
                //    Session["T_COMPANY_ID"] = "";
                //    Session["T_EMP_ID"] = dt.Rows[0]["T_EMP_CODE"].ToString();
                //    Session["T_SITE_CODE"] = dt.Rows[0]["T_SITE_CODE"].ToString();
                //    Session["T_LANG"] = "2";
                //    message = dt.Rows[0]["T_EMP_CODE"] + "," + dt.Rows[0]["T_ROLE_CODE"] + "," + dt.Rows[0]["T_SITE_CODE"];

                //}


            }
            catch (Exception e)
            {

                message = "fls";
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(),e.Message);
                //emessage += e.InnerException != null? e.InnerException.Message: e.Message;
            }
            //}
            //else
            //{
            //    try
            //    {
            //        CommonDALSQL cSql = new CommonDALSQL();
            //        DataTable dt = cSql.Query($"SELECT T57.T_USER_ID,T57.T_PWD ,T57.T_EMP_ID ,T57.T_COMPANY_ID,T57.T_ROLE_CODE ,CONCAT(T04.T_FIRST_LANG2_NAME,' ',T04.T_FATHER_LANG2_NAME,' ',T04.T_GFATHER_LANG2_NAME,' ', T04.T_FAMILY_LANG2_NAME) AS EMP_NAME FROM T74057 AS T57 INNER JOIN T74004 AS T04 ON T57.T_EMP_ID = T04.T_EMP_ID WHERE T57.T_USER_ID = '{userId}' AND  T57.T_PWD = '{Password}'");
            //        if (dt.Rows.Count > 0)
            //        {
            //            Session["T_USER_ID"] = dt.Rows[0]["T_USER_ID"].ToString();
            //            Session["T_ROLE_CODE"] = dt.Rows[0]["T_ROLE_CODE"].ToString();
            //            Session["T_USER_NAME"] = dt.Rows[0]["EMP_NAME"].ToString();
            //            Session["T_COMPANY_ID"] = dt.Rows[0]["T_COMPANY_ID"].ToString();
            //            Session["T_LANG"] = "2";
            //            message = dt.Rows[0]["T_USER_ID"].ToString();
            //        }
            //        else
            //        {
            //            message = "Invalid Id or Password....";
            //        }

            //    }
            //    catch (Exception e)
            //    {

            //        message = "Invalid Id or Password....";
            //    }
            //}
            
            dtLog.Columns.Add("msg", typeof(string));
            dtLog.Columns.Add("role", typeof(string));
            //dtLog.Columns.Add("emsg", typeof(string));
            dtLog.Rows.Add(message,role);
            
            string jst = string.Empty;
            jst = JsonConvert.SerializeObject(dtLog);
            return Json(jst, JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public JsonResult UserLogin(string userId, string Password)
        //{
        //    string message = "";
        //    if (CheckForInternetConnection())
        //    {
        //        try
        //    {
        //        if (userId.Length>4)
        //        {
        //            IEnumerable loginData = repo.GetUserInfo(userId, Password);
        //            string JSONString = string.Empty;
        //            JSONString = JsonConvert.SerializeObject(loginData);
        //            JArray obj = JArray.Parse(JSONString);
        //            FastReport.Utils.Config.WebMode = true;

        //            if (loginData.Count() != 0)
        //            {
        //                Session["T_USER_ID"] = obj[0]["T_USER_ID"].ToString();
        //                Session["T_ROLE_CODE"] = obj[0]["T_ROLE_CODE"].ToString();
        //                Session["T_USER_NAME"] = obj[0]["EMP_NAME"].ToString();
        //                Session["T_COMPANY_ID"] = obj[0]["T_COMPANY_ID"].ToString();
        //                Session["T_EMP_ID"] = obj[0]["T_EMP_ID"].ToString();
        //                Session["T_SITE_CODE"] = obj[0]["T_SITE_CODE"].ToString();
        //                Session["T_LANG"] = "2";
        //                message = obj[0]["T_USER_ID"].ToString();


        //            }
        //            else
        //            {
        //                message = "Invalid Id or Password....";
        //            }
        //            }
        //        else
        //        {
        //            DataTable dt = GetDocInfo(userId, Password);
        //            Session["T_USER_ID"] = dt.Rows[0]["T_LOGIN_NAME"].ToString();
        //            Session["T_ROLE_CODE"] = dt.Rows[0]["T_ROLE_CODE"].ToString();
        //            Session["T_USER_NAME"] = dt.Rows[0]["DOCTOR"].ToString();
        //            Session["T_COMPANY_ID"] = "";
        //            Session["T_EMP_ID"] = dt.Rows[0]["T_EMP_CODE"].ToString();
        //            Session["T_SITE_CODE"] = dt.Rows[0]["T_SITE_CODE"].ToString();
        //            Session["T_LANG"] = "2";
        //            message = dt.Rows[0]["T_EMP_CODE"]+","+ dt.Rows[0]["T_ROLE_CODE"]+","+ dt.Rows[0]["T_SITE_CODE"];

        //            }


        //    }
        //    catch (Exception e)
        //    {

        //        message = "Invalid Id or Password....";
        //    }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            CommonDALSQL cSql = new CommonDALSQL();
        //            DataTable dt = cSql.Query($"SELECT T57.T_USER_ID,T57.T_PWD ,T57.T_EMP_ID ,T57.T_COMPANY_ID,T57.T_ROLE_CODE ,CONCAT(T04.T_FIRST_LANG2_NAME,' ',T04.T_FATHER_LANG2_NAME,' ',T04.T_GFATHER_LANG2_NAME,' ', T04.T_FAMILY_LANG2_NAME) AS EMP_NAME FROM T74057 AS T57 INNER JOIN T74004 AS T04 ON T57.T_EMP_ID = T04.T_EMP_ID WHERE T57.T_USER_ID = '{userId}' AND  T57.T_PWD = '{Password}'");
        //            if (dt.Rows.Count>0)
        //            {
        //                Session["T_USER_ID"] = dt.Rows[0]["T_USER_ID"].ToString();
        //                Session["T_ROLE_CODE"] = dt.Rows[0]["T_ROLE_CODE"].ToString();
        //                Session["T_USER_NAME"] = dt.Rows[0]["EMP_NAME"].ToString();
        //                Session["T_COMPANY_ID"] = dt.Rows[0]["T_COMPANY_ID"].ToString();
        //                Session["T_LANG"] = "2";
        //                message = dt.Rows[0]["T_USER_ID"].ToString();
        //            }
        //            else
        //            {
        //                message = "Invalid Id or Password....";
        //            }

        //        }
        //        catch (Exception e)
        //        {

        //            message = "Invalid Id or Password....";
        //        }
        //    }

        //    return Json(message, JsonRequestBehavior.AllowGet);

        //}
        private DataTable GetDocInfo(string userId, string Password)
        {
            try
            {
            TRequest tr = new TRequest();
            List<TParam> param = new List<TParam>();
            //Param List---------------------------------start
            tr.db = "KSMC";
            tr.sp = "GET_DOC_LOG_INFO";
            param.Add(new TParam() { Key = "@userId", Value = userId });
            param.Add(new TParam() { Key = "@Password", Value = Password });
            //Param List---------------------------------end
            tr.dict = param;
            string inputJson = (new JavaScriptSerializer()).Serialize(tr);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(APILink.GetDataParam, inputJson);
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(json);
            DataTable dt = ds.Tables[1];
            return dt;
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                DataTable d = new DataTable();
                return d;
            }
        }
        [HttpPost]
        public void UpdateLogin(string userId)
        {
            try
            {
                repo.UpdateLogin(userId);
            }
            catch (Exception e)
            {
              
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString(), Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
             //   Console.WriteLine(e);
               // throw;
            }
           
        }
        [HttpPost]
        public JsonResult checkAmbulance(string userId)
        {
            
            try
            {
              var  data = repo.checkAmbulance(userId);
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
        public ActionResult UserLogout(string lt,string ln)
        {
            try
            {
                
                int req = 0;
                string eventId = _cDal.CheckLogoutType(Session["T_ROLE_CODE"].ToString());

                DataTable data =
                    _cDal.Query(
                        $"SELECT T_LATITUDE, T_LONGITUDE FROM T74026 WHERE T_USER_ID='{Session["T_USER_ID"].ToString()}' ORDER BY ID");
                if (data != null)
                {
                    var lat = data.Rows[0]["T_LATITUDE"].ToString();
                    var lon = data.Rows[0]["T_LONGITUDE"].ToString();
                    try
                    {
                        DataTable dt =
                            _cDal.Query(
                                $"SELECT T_REQUEST_ID FROM T74041 WHERE T_USER_ID='{Session["T_USER_ID"].ToString()}' AND T_DISCH_STATUS IS NULL AND T_ACCEPT_STATUS IS NOT NULL");
                        if (dt.Rows.Count > 0)
                        {
                            req = Int32.Parse(dt.Rows[0]["T_REQUEST_ID"].ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        //message = "Invalid Id or Password....";
                    }
                    if (lat != "")
                    {
                        _cDal.Command(
                            $"INSERT INTO T74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_EVENT_ID,T_REQUEST_ID) VALUES ((SELECT MAX(ID)+1 FROM T74026),'{Session["T_USER_ID"].ToString()}',SYSDATE,'{lat}','{lon}','{eventId}',DECODE({req},0,null,{req}))");
                        repo.UpdateLogout();
                    }
                    else
                    {
                        _cDal.Command(
                            $"INSERT INTO T74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_EVENT_ID,T_REQUEST_ID) VALUES ((SELECT MAX(ID)+1 FROM T74026),'{Session["T_USER_ID"].ToString()}',SYSDATE,'{lt}','{ln}','{eventId}',DECODE({req},0,null,{req}))");
                        repo.UpdateLogout();
                    }


                }
                else
                {
                    _cDal.Command(
                        $"INSERT INTO T74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_EVENT_ID,T_REQUEST_ID) VALUES ((SELECT MAX(ID)+1 FROM T74026),'{Session["T_USER_ID"].ToString()}',SYSDATE,'{lt}','{ln}','{eventId}',DECODE({req},0,null,{req}))");
                    repo.UpdateLogout();
                }
                Session.Clear();
                
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
              
            }
            return Json(JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EntryUser()
        {
            try
            {
                var EntryUser = Session["T_USER_ID"].ToString();
                return Json(EntryUser, JsonRequestBehavior.AllowGet);
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
        public JsonResult CompanyId()
        {
            try
            {
                var CompanyId = Session["T_COMPANY_ID"].ToString();
                return Json(CompanyId, JsonRequestBehavior.AllowGet);
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
        public JsonResult setClientErrorLog(string controller, string action, string message, string source)
        {
            //var msg = _cDal.setClientErrorLog(controller, action, message,Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), source);
            Trace.WriteLine(
               string.Format("{0}-{1}-{2}-{3}-\"{4}\"",
                   DateTime.Now.ToString("yyyyMMddHHmmss"),
                   controller,
                   action,
                   source,
                   message));
            var msg = "";
            string JSONstring = JsonConvert.SerializeObject(msg);
            return Json(JSONstring, JsonRequestBehavior.AllowGet);
        }

















        //---------------------its a reffernce code and its working------------------------
        //private string GetSite()
        //{
        //    TRequest tr = new TRequest();
        //    tr.orLat = "23.7539543";
        //    tr.orLng = "90.3933735";
        //    tr.dsLat = "23.867427";
        //    tr.dsLng = "90.397315";
        //    string inputJson = (new JavaScriptSerializer()).Serialize(tr);
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;
        //    string json = client.UploadString(APILink.GetDistance, inputJson);
        //    var jArray = JObject.Parse(json);
        //    var firstName = jArray.Properties().Where(a => a.Name == "result").Select(p => p.Value).FirstOrDefault();
        //    var jArray2 = JObject.Parse(firstName.ToString());
        //    var objM = jArray2.Properties().Where(a => a.Name == "rows").Select(p => p.Value).FirstOrDefault();
        //    var x = objM[0]["elements"].ToString();
        //    var jArray3 = JArray.Parse(x);
        //    var x2 = jArray3[0]["distance"].ToString();
        //    var jArray4 = JObject.Parse(x2);
        //    var text2 = jArray4.Properties().Where(a => a.Name == "text").Select(p => p.Value).FirstOrDefault().ToString();
        //    return json;


        //}
    }

}