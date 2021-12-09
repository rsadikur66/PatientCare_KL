using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ambucare.Models;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using T74207 = AmbucareDAL.Models.T74207;

namespace Ambucare.Controllers.Transaction
{
    public class T74041Controller : Controller
    {
        private IT74041 repository;
        private IError err;
        public T74041Controller(IT74041 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        public ActionResult T74041()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Gender()
        {
            try
            {
            var data = repository.Gender();
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


        //this method needed to copy for main source
        [HttpPost]
        public JsonResult GetAllUserLatlong(string patLat, string patLon)
        {
            try
            {
            if (patLat == null && patLon == null)
            {
                var zonCode = Session["T_ZONE_CODE"].ToString();
                var data = repository.GetAllUserLatlong(zonCode);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var datas = getAllAmb(patLat, patLon);
                //string JSONString = string.Empty;
                //JSONString = JsonConvert.SerializeObject(data.Data);
                return Json(datas.Data, JsonRequestBehavior.AllowGet);
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
        //this method needed to copy for main source
        
    [HttpPost]
        public JsonResult GetLoginUserLatlong()
        {
            try
            {
            var data = repository.GetLoginUserLatlong(Session["T_USER_ID"].ToString());
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
        public JsonResult GetAllDataOnMap_T74041(int P_AMBU_REG_ID)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var data = repository.GetAllDataOnMap_T74041(P_AMBU_REG_ID,lang);
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
        public JsonResult GetUserIDByAMBRegID(int T_AMBU_REG_ID)
        {
            try
            {
                var data = repository.GetUserIDByAMBRegID(T_AMBU_REG_ID);
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
        public ActionResult GetPatInfo()
        {
            try
            {
            var data = repository.GetPatInfo();
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
        public JsonResult Insert_t74046(T74046 _t74046)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var data = repository.Insert_t74046(_t74046);
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
        public void Insert_t74041(T74041 _t74041)
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            repository.Insert_t74041(_t74041);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
              
            }
        }

        [HttpPost]
        public JsonResult Insert_T74207(T74207 t74207,T74041 t74041,T74046 t74046,string type)
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            var user = Session["T_USER_ID"].ToString();
            var data=repository.Insert_T74207(t74207, t74041, t74046,type,user,lang);
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
        public JsonResult GetPendingRequestData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            var user = Session["T_USER_ID"].ToString();
            var data = repository.GetPendingRequestData(user, lang);
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
        public JsonResult CancelReuest(int request, string canCode)
        {
            try
            {
            var data = repository.CancelReuest(request, canCode);
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
        public JsonResult GetAllHospitalLatlong()
        {
            try
            {
            var data = repository.GetAllHospitalLatlong();
           // string JSONString = string.Empty;
           // JSONString = JsonConvert.SerializeObject(data);
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
        public JsonResult GetPatientInformation(int requestId)
        {
            try
            {
            var data = repository.GetPatientInformation(requestId);
            // string JSONString = string.Empty;
            // JSONString = JsonConvert.SerializeObject(data);
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
        public JsonResult UpdateByOperator(int requestId,string doc)
        {
            try
            {
            var data = repository.UpdateByOperator(requestId, doc);
            // string JSONString = string.Empty;
            // JSONString = JsonConvert.SerializeObject(data);
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
        public JsonResult GetCancelReasonData()
        {
            try
            {
            var data = repository.GetCancelReasonData();
            // string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(data);
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
        public ActionResult Result()
        {
            try
            {
            int lang = Int32.Parse(Session["T_LANG"].ToString());
            // var data = repository.Result(lang);
            var data = repository.result_1(lang);
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
        //UpdateByOperator

        //public void GPS_Insert(decimal latitude, decimal longitude)
        //{
        //  //  var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
        //    var user = Session["T_USER_ID"].ToString();
        //    repository.GPS_Insert(latitude, longitude, user);

        //}

        //[HttpPost]
        //public JsonResult GetCriteriasData()
        //{
        //    var data = repository.GetCriteriasData();
        //    // string JSONString = string.Empty;
        //    //JSONString = JsonConvert.SerializeObject(data);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult GetActivePatientsData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            string code = Session["T_ZONE_CODE"].ToString();
            var data = repository.GetActivePatientsData(code, lang);
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
        public JsonResult SaveRemarks(int req, string rem)
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            var data = repository.SaveRemarks(req,rem);
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


        //-------------Search Crieteria---------
        [HttpPost]
        public JsonResult getAmbulanceList()
        {
            try
            {
            string zone = Session["T_ZONE_CODE"].ToString();
            var data = repository.getAmbulanceList(zone);
            //string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(data);
            //return Json(JSONString, JsonRequestBehavior.AllowGet);

            string JSONString = string.Empty;

            JSONString = JsonConvert.SerializeObject(data);
            JArray obj = JArray.Parse(JSONString);
            DataTable dt = new DataTable();
            if (obj.Count != 0)
            {

                dt.Columns.Add("latitude", typeof(string));
                dt.Columns.Add("longitude", typeof(string));
                dt.Columns.Add("markImg", typeof(string));
                dt.Columns.Add("AMB_CAPASITY", typeof(string));
                dt.Columns.Add("T_AMBU_REG_ID", typeof(string));
                //dt.Columns.Add("distance", typeof(string));
                dt.Columns.Add("text", typeof(string));
                dt.Columns.Add("acceptCount", typeof(string));
                dt.Columns.Add("HOS_LAT", typeof(string));
                dt.Columns.Add("HOS_LON", typeof(string));
                //dt.Columns.Add("rejectCount", typeof(string));
                for (int i = 0; i < obj.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    //string llat = obj[i]["latitude"].ToString();
                    dr["latitude"] = obj[i]["latitude"].ToString();
                    dr["longitude"] = obj[i]["longitude"].ToString();
                    dr["markImg"] = obj[i]["markImg"].ToString();
                    dr["AMB_CAPASITY"] = obj[i]["AMB_CAPASITY"].ToString();
                    dr["T_AMBU_REG_ID"] = obj[i]["T_AMBU_REG_ID"].ToString();
                    dr["HOS_LAT"] = obj[i]["HOS_LAT"].ToString();
                    dr["HOS_LON"] = obj[i]["HOS_LON"].ToString();
                    //string newdata = GetAmbDist(patLat, patLon, obj[i]["latitude"].ToString(), obj[i]["longitude"].ToString())[0]["distance"]["text"].ToString();
                    
                    if (!string.IsNullOrEmpty(obj[i]["HOS_LON"].ToString()) && !string.IsNullOrEmpty(obj[i]["HOS_LON"].ToString()))
                    {
                         dr["text"] = GetAmbDist(obj[i]["latitude"].ToString(), obj[i]["longitude"].ToString(), obj[i]["HOS_LAT"].ToString(), obj[i]["HOS_LON"].ToString(),"t");
                    //dr["distance"] = GetAmbDist(patLat, patLon, obj[i]["latitude"].ToString(), obj[i]["longitude"].ToString())[0]["distance"]["value"].ToString();
                    }
                    else
                    {
                        dr["text"] = string.Empty;
                        //dr["distance"] = GetAmbDist(patLat, patLon, obj[i]["latitude"].ToString(), obj[i]["longitude"].ToString())[0]["distance"]["value"].ToString();
                    }


                    dr["acceptCount"] = obj[i]["acceptCount"].ToString();

                    //dr["rejectCount"] = obj[i]["rejectCount"].ToString();
                    dt.Rows.Add(dr);

                }
            }

            string JSONStringdt = string.Empty;
            JSONStringdt = JsonConvert.SerializeObject(dt);
            return Json(JSONStringdt, JsonRequestBehavior.AllowGet);
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
        public JsonResult getLoggedOutAmbulanceList()
        {
            try
            {
            string zone = Session["T_ZONE_CODE"].ToString();
            var data = repository.getLoggedOutAmbulanceList(zone);
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
        public JsonResult getCleanNeedAmbulanceList()
        {
            try
            {
            string zone = Session["T_ZONE_CODE"].ToString();
            var data = repository.getCleanNeedAmbulanceList(zone);
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
        public JsonResult getMaxProtocol()
        {
            try
            {
            var data = repository.getMaxProtocol();
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
        

        private string GetAmbDist()
        {
            try
            {
            TRequest tr = new TRequest();
            tr.orLat = "23.7539543";
            tr.orLng = "90.3933735";
            tr.dsLat = "23.867427";
            tr.dsLng = "90.397315";
            string inputJson = (new JavaScriptSerializer()).Serialize(tr);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(APILink.GetDistance, inputJson);
            return json;
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return e.Message;
            }
        }
        [HttpPost]
        public JsonResult AsignPatientFromPendinglist(int reqId,string operation)
        {
            try
            {
            var user = Session["T_USER_ID"].ToString();
            var data = repository.AsignPatientFromPendinglist(reqId, user, operation);
           // string JSONString = string.Empty;
           // JSONString = JsonConvert.SerializeObject(data);
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
        public JsonResult GetCancelPatData()
        {

            try
            {
                string lang = Session["T_LANG"].ToString();
                var user = Session["T_USER_ID"].ToString();
                var data = repository.GetCancelPatData(lang, user);
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
        public JsonResult SaveCancelConfirmData(string cnfm, string reqId,string cnlRsn,int Tid)
        {
            try
            {
                var user = Session["T_USER_ID"].ToString();
                var data = repository.SaveCancelConfirmData(user, cnfm, reqId, cnlRsn, Tid);
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
        //SaveCancelConfirmData
        [HttpPost]
        //this method needed to copy for main source
        public JsonResult getAllAmb(string patLat, string patLon)
        {
            try
            {
                string zone = Session["T_ZONE_CODE"].ToString();
                string userid = Session["T_USER_ID"].ToString();
                var getAllAmblatlong = repository.GetAllUserLatlong(zone);
                string JSONString = string.Empty;

                JSONString = JsonConvert.SerializeObject(getAllAmblatlong);
                JArray obj = JArray.Parse(JSONString);
                DataTable dt = new DataTable();
                if (obj.Count != 0)
                {

                    dt.Columns.Add("latitude", typeof(string));
                    dt.Columns.Add("longitude", typeof(string));
                    dt.Columns.Add("markImg", typeof(string));
                    dt.Columns.Add("AMB_CAPASITY", typeof(string));
                    dt.Columns.Add("T_AMBU_REG_ID", typeof(string));
                    dt.Columns.Add("distance", typeof(string));
                    dt.Columns.Add("text", typeof(string));
                    dt.Columns.Add("acceptCount", typeof(string));
                    dt.Columns.Add("rejectCount", typeof(string));
                    for (int i = 0; i < obj.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        //string llat = obj[i]["latitude"].ToString();
                        dr["latitude"] = obj[i]["latitude"].ToString();
                        dr["longitude"] = obj[i]["longitude"].ToString();
                        dr["markImg"] = obj[i]["markImg"].ToString();
                        dr["AMB_CAPASITY"] = obj[i]["AMB_CAPASITY"].ToString();
                        dr["T_AMBU_REG_ID"] = obj[i]["T_AMBU_REG_ID"].ToString();
                        dr["text"] = GetAmbDist(patLat, patLon, obj[i]["latitude"].ToString(), obj[i]["longitude"].ToString(), "t");
                        dr["distance"] = GetAmbDist(patLat, patLon, obj[i]["latitude"].ToString(), obj[i]["longitude"].ToString(), "d");

                        dr["acceptCount"] = obj[i]["acceptCount"].ToString();

                        dr["rejectCount"] = obj[i]["rejectCount"].ToString();
                        dt.Rows.Add(dr);

                    }
                }

                string JSONStringdt = string.Empty;
                JSONStringdt = JsonConvert.SerializeObject(dt);
                return Json(JSONStringdt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetAmbDist(string lat, string lon, string amLat, string amLon, string type)
        {
           
            string ret = "";
            try
            {
                TRequest tr = new TRequest();
                tr.orLat = lat;
                tr.orLng = lon;
                tr.dsLat = amLat;
                tr.dsLng = amLon;
                string inputJson = (new JavaScriptSerializer()).Serialize(tr);
                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string json = client.UploadString(APILink.GetDistance, inputJson);
                JObject obj2 = JObject.Parse(json);
                string rows = obj2["result"].ToString();
                JObject ddArray = JObject.Parse(rows);
                JArray elements = (JArray)ddArray.SelectToken("rows");
                string distance = elements[0]["elements"].ToString();
                JArray valuesArray = JArray.Parse(distance);

                ret = type == "t"
                    ? valuesArray[0]["distance"]["text"].ToString()
                    : valuesArray[0]["distance"]["value"].ToString();
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                ret = type == "t"? "N/A": Int32.MaxValue.ToString();
            }
            return ret;
        }

        //this method needed to copy for main source
        [HttpPost]
        public ActionResult getCallReason()
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var data = repository.getCallReason(lang);
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
        public JsonResult saveReq(T74041 t41, T74046 t46,T74120 t20,T74026 t26)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                t41.T_ENTRY_USER = Session["T_USER_ID"].ToString();
                var data = repository.saveReq(t41, t46, t20, t26, lang);
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