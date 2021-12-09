using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Ninject.Activation;
using Ambucare.Models;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ambucare.Controllers.Initialization
{
    public class T74014Controller : Controller
    {
        // GET: T74014
        public ActionResult T74014()
        {
            return View();
        }

        private readonly IT74014 repository;

        public T74014Controller(IT74014 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        //For Save and Update Start
        [HttpPost]

        public ActionResult Insert_T74014(T74014 _T74014, HttpPostedFileBase attachment)
        {
            string base64String = "";
            string mesg = "";
            string fname = "";
            var path = "";
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var inputStream = fileContent.InputStream;
                    var fileName = Path.GetFileName(fileContent.FileName);
                    _T74014.T_REG_IMAGE = fileName;
                    path = Path.Combine(Server.MapPath("~/Images/RegistrationImage/"), fileName);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        inputStream.CopyTo(fileStream);
                    }
                }
            }
            if (_T74014.T_AMBU_REG_ID == 0)
            {
                mesg = "Data Save Successfully";
            }
            else
            {
                mesg = "Data Update Successfully";
            }
            repository.Insert_T74014(_T74014);
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }
        //For Save and Update End

        //For Deleted Methord Start
        [HttpPost]
        public ActionResult Deleted_T74014(int T_AMBU_REG_ID)
        {
            var del = repository.Deleted_T74014(T_AMBU_REG_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
        //For Deleted Methord End

        public async Task<object> ImagetoBase64(string T_BASE64_DATA)
        {
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
                tr.Base64 = T_BASE64_DATA;
                param.Add(new TParam() { Key = "@SenderId", Value = "TEAM5" });
                param.Add(new TParam() { Key = "@ReceiverId", Value = "242" });
                param.Add(new TParam() { Key = "@fileLocation", Value = "http://202.40.189.18:82/Image/Doctor/" });
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
        //For Gridview Method Start 
        [HttpPost]
        public async Task<ActionResult> GetGridData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                Int32 PageIndex = 0;
                Int32 PageSize = 0;
                if (start == "0")
                {
                    PageIndex = 0;
                }
                else
                {
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length); //1;
                }
                PageSize = Convert.ToInt32(length);
                //IQueryable allCustomer;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                IEnumerable GD;
                int cnt;
                if (searchValue == "")
                {
                    GD = repository.GetGridData(PageIndex, PageSize, HttpContext.Session["T_USER_ID"].ToString());
                    cnt = repository.GetGridData_Search_Count(searchValue, HttpContext.Session["T_USER_ID"].ToString());
                }
                else
                {
                    GD = repository.GetGrid_Data_Search(searchValue, PageIndex, PageSize, HttpContext.Session["T_USER_ID"].ToString());
                    cnt = repository.GetGridData_Search_Count(searchValue, HttpContext.Session["T_USER_ID"].ToString());
                }
                recordsTotal = cnt;
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = await Task.FromResult(GD) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //For Gridview Method End

        //Dropdown Color Id Start
        [HttpPost]
        public JsonResult GetColorId()
        {
            try
            {
                var getcolor = repository.GetColorId;
                return Json(getcolor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //Dropdown Color Id End
        //Dropdown Ambulance type Id Start
        [HttpPost]
        public JsonResult GetAmbulanceId()
        {
            try
            {
                var getAmbulance = repository.GetAmbulanceId;
                return Json(getAmbulance, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //Dropdown Ambulance type Id End

        //Dropdown Brand type Id Start
        [HttpPost]
        public JsonResult GetBrandId()
        {
            try
            {
                var getbrand = repository.GetBrandId;
                return Json(getbrand, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //Dropdown Brand type Id End

        //Employee Data Method Start
        [HttpPost]
        public JsonResult GetEmployeeData()
        {
            try
            {
                var getemp = repository.GetEmployeeData;
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(getemp);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //Employee Data Method End
        [HttpPost]
        public JsonResult GetimageData()
        {
            try
            {
                var imageData = Session["IQbyte"].ToString();
                return Json(imageData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetRegNo(string regNo)
        {
            try
            {
                var getemp = repository.GetRegNo(regNo);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(getemp);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetChesisNo(string chesisNo)
        {
            try
            {
                var getemp = repository.GetChesisNo(chesisNo);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(getemp);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEngineNo(string engineNo)
        {
            try
            {
                var getemp = repository.GetEngineNo(engineNo);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(getemp);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getAmbulanceOwnerData()
        {
            try
            {
            var user=  Session["T_USER_ID"].ToString();
                var getemp = repository.getAmbulanceOwnerData(user);
                //string JSONString = string.Empty;
                //JSONString = JsonConvert.SerializeObject(getemp);
                return Json(getemp, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}