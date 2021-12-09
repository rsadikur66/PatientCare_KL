using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Ambucare.Hubs;
using Ambucare.Models;
using AmbucareDAL.Repository.Interface.Transaction;
using FastReport.Data;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Export.Text;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Transaction
{
    public class T74131Controller : Controller
    {
        // GET: T74131
        private readonly IT74131 repository;

        public T74131Controller(IT74131 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74131()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPatInfo(string P_USER_ID)
        {
            try
            {
                var data = repository.GetPatInfo(P_USER_ID);
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
        public JsonResult GetPreviousMedicine(int requId)
        {
            try
            {
                var data = repository.GetPreviousMedicine(requId);
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
        public JsonResult GetChatHistory(int T_REQUEST_ID,string T_U_ID, string TYPE)
        {
            try
            {
                string T_SENDER_ID = "";
                string T_RECIEVER_ID = "";
                if (TYPE=="Pat")
                {
                    T_SENDER_ID = Session["T_EMP_ID"].ToString();
                    T_RECIEVER_ID = T_U_ID;
                }
                else if (TYPE == "Doc")
                {
                    T_SENDER_ID = Session["T_USER_ID"].ToString();
                    T_RECIEVER_ID = T_U_ID;
                }
                var data = repository.GetChatHistory(T_REQUEST_ID,T_SENDER_ID, T_RECIEVER_ID);
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
        public JsonResult chatHistory(T74098 t74098)
        {
            try
            {
                t74098.T_SENDER_ID = t74098.T_SENDER_TYPE== "Pat"?Session["T_USER_ID"].ToString(): Session["T_EMP_ID"].ToString();
                t74098.T_TIME=DateTime.Now;
                repository.chatHistory(t74098);
                string JSONString = string.Empty;
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult setDoc()
        {
            try
            {
                string uid = Session["T_USER_ID"].ToString();
                repository.setDoc(uid);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<object> GetSite()
        {
            string Code = repository.GetSite();
            object obj = null;
            TRequest tr = new TRequest();
            List<TParam> param = new List<TParam>();

            using (var client = new HttpClient())
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>();
                //Param List---------------------------------start
                tr.db = "KSMC";
                tr.sp = "GET_DOC_LIST_T02039";
                param.Add(new TParam() { Key = "P_SITE_CODES", Value = Code });
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
        [HttpPost]
        public JsonResult SaveSuggestedMedicine(int requestID, List<T74040> save40List)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var data= repository.SaveSuggestedMedicine(requestID, save40List, lang);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult saveRequestHospital(int requestID, string hosID)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var data = repository.saveRequestHospital(requestID, hosID, lang);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //saveRequestHospital
        //public string GetDecryptData(string Data)
        //{
        //    string decData = "";
        //    string hash = "foxle@rn";
        //    string dec = Data.Replace('_', '/');
        //    byte[] data = Convert.FromBase64String(dec);
        //    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        //    {
        //        byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        //        using (TripleDESCryptoServiceProvider tripDEs = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
        //        {
        //            ICryptoTransform transform = tripDEs.CreateDecryptor();
        //            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
        //            decData = UTF8Encoding.UTF8.GetString(results);
        //        }

        //    }

        //    return decData;
        //}
        //public async Task<object> GetDocInfo(string chatData)
        //{
        //    object obj = null;
        //    TRequest tr = new TRequest();
        //    List<TParam> param = new List<TParam>();

        //    using (var client = new HttpClient())
        //    {
        //        Dictionary<string, string> pairs = new Dictionary<string, string>();
        //        //Param List---------------------------------start
        //        tr.db = "KSMC";
        //        tr.sp = "GET_DOC_Info_T02039";
        //        string decData =GetDecryptData(chatData);
        //        string[] codes = decData.Split(',').Select(a => a.Trim()).ToArray();
        //        string EMP_CODE = codes[0];
        //        string ROLE_CODE = codes[1];
        //        string T_SITE_CODE = codes[2];
        //        param.Add(new TParam() { Key = "P_EMP_CODE", Value = EMP_CODE });
        //        param.Add(new TParam() { Key = "P_ROLE_CODE", Value = ROLE_CODE });
        //        param.Add(new TParam() { Key = "P_SITE_CODE", Value = T_SITE_CODE });
        //        //Param List---------------------------------end
        //        tr.dict = param;
        //        var myContent = JsonConvert.SerializeObject(tr);
        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        var byteContent = new ByteArrayContent(buffer);
        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //        HttpResponseMessage response = await client.PostAsync(APILink.GetDataParam + "?json=", byteContent);
        //        if (response.IsSuccessStatusCode)
        //        { obj = response.Content.ReadAsStringAsync().Result; }
        //        return obj;
        //    }
        //}
        //public ActionResult chatFileUpload(T74098 t74098, string b64)
        //{
        //    string base64String = "";
        //    string mesg = "";
        //    string fname = "";
        //    var path = "";
        //    foreach (string file in Request.Files)
        //    {
        //        var fileContent = Request.Files[file];
        //        if (fileContent != null && fileContent.ContentLength > 0)
        //        {
        //            var inputStream = fileContent.InputStream;
        //            var fileName = Path.GetFileName(fileContent.FileName);
        //            //_T74014.T_REG_IMAGE = fileName;
        //            path = Path.Combine(Server.MapPath("~/Images/RegistrationImage/"), fileName);
        //            using (var fileStream = System.IO.File.Create(path))
        //            {
        //                inputStream.CopyTo(fileStream);
        //            }
        //        }
        //    }
        //    //if (_T74014.T_AMBU_REG_ID == 0)
        //    //{
        //    //    mesg = "Data Save Successfully";
        //    //}
        //    //else
        //    //{
        //    //    mesg = "Data Update Successfully";
        //    //}
        //    //repository.Insert_T74014(_T74014);
        //    return Json(mesg, JsonRequestBehavior.AllowGet);
        //}
        public async Task<object> chatFileUpload(T74098 t74098, string b64,string ext)
        {
            t74098.T_SENDER_ID = t74098.T_SENDER_TYPE == "Pat" ? Session["T_USER_ID"].ToString() : Session["T_EMP_ID"].ToString();
            t74098.T_TIME = DateTime.Now;
            object obj = null;
            string base64String = "";
            var path = "";

            TRequest tr = new TRequest();
            List<TParam> param = new List<TParam>();
            //Param List---------------------------------start
            using (var client = new HttpClient())
            {
                tr.db = "PatientCare";
                tr.sp = "T74098_M_INSERT";
                tr.Base64 = b64;
                tr.extnsn = ext;
                param.Add(new TParam() { Key = "@SenderId", Value = t74098.T_SENDER_ID });
                param.Add(new TParam() { Key = "@ReceiverId", Value = t74098.T_RECIEVER_ID });
                param.Add(new TParam() { Key = "@SenderType", Value = t74098.T_SENDER_TYPE });
                param.Add(new TParam() { Key = "@RequestId", Value = t74098.T_REQUEST_ID.ToString() });
                //param.Add(new TParam() { Key = "@fileLocation", Value = "http://202.40.189.18:82/Image/UploadedFile/" });
                //Param List---------------------------------end
                tr.dict = param;

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
        [HttpPost]
        public JsonResult getConsoleApp()
        {
            try
            {
                string sStr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "console.txt");
                //string console = System.IO.File.ReadAllText(@"http://202.40.189.19:82/Image/Console/console.txt");
                string console = System.IO.File.ReadAllText(sStr);
                return Json(console, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //--------------------------Connectivity-----------------------------------
        public JsonResult reqListForTeam()
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                string zone = Session["T_ZONE_CODE"].ToString();
                var data = repository.reqListForTeam(user, zone);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult reqListForDoc()
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                string zone = Session["T_ZONE_CODE"].ToString();
                var data = repository.reqListForDoc(user, zone);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult acptReqofDoc()
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                string zone = Session["T_ZONE_CODE"].ToString();
                var data = repository.acptReqofDoc(user, zone);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult onRcvPatReq(int req)
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                string lang = Session["T_LANG"].ToString();
                var data = repository.onRcvPatReq(user, req,lang);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult closeChat(int req)
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                string lang = Session["T_LANG"].ToString();
                var data = repository.closeChat(user, req,lang);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult conWthDoc()
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                string zone = Session["T_ZONE_CODE"].ToString();
                var data = repository.conWthDoc(user, zone);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult chkConn()
        {
            try
            {
                string user = Session["T_USER_ID"].ToString();
                string zone = Session["T_ZONE_CODE"].ToString();
                var data = repository.chkConn(user, zone);
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
        public JsonResult getDocID()
        {
            string user = Session["T_USER_ID"].ToString();
            var data = repository.getDocID(user);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(data);
            return Json(JSONString, JsonRequestBehavior.AllowGet);
        }

        //----------------------------
        [HttpPost]
        public JsonResult chkReqHos(int requestId)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var data = repository.chkReqHos(requestId, lang);
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
        public JsonResult sendREq(string T_REQUEST_ID)
        {
            Session["T_REQUEST_ID"] = T_REQUEST_ID;
            var Data = "";
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(Data);
            return Json(JSONString, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult setDoc_new(int req, string user)
        {
            try
            {
                string zone = Session["T_ZONE_CODE"].ToString();
                string doc = Session["T_USER_ID"].ToString();
                repository.setDoc_new(zone,req, user, doc);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult setNewConv(int req)
        {
            try
            { 
                string doc = Session["T_USER_ID"].ToString();
                repository.setNewConv(req, doc);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult emergenceyReq(string text)
        {
            try
            {
                int req = Int32.Parse(Session["T_REQUEST_ID"].ToString());
                string user = Session["T_USER_ID"].ToString();
                var data= repository.emergenceyReq(req, text, user);
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
        public JsonResult getPatMsg(int req)
        {
            try
            {
                var data = repository.getPatMsg(req);
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
        public JsonResult chkPat(string req)
        {
            string JSONString = "0";
            try
            {
                
                //string req = Session["T_REQUEST_ID"].ToString();
                if (!string.IsNullOrWhiteSpace(req))
                {
                    var data = repository.chkPat(req);
                    JSONString = JsonConvert.SerializeObject(data);
                   
                }
                return Json(JSONString, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                //return Json(exc.Message, JsonRequestBehavior.AllowGet);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getPlaceHolder(string label)
        {
            try
            {
                var data = repository.getPlaceHolder(label);
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