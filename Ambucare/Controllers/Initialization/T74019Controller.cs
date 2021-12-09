using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Threading.Tasks;
using Ambucare.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace Ambucare.Controllers.Initialization
{
    public class T74019Controller : Controller
    {
        private readonly IT74019 repository;

        public T74019Controller(IT74019 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }

      

        // GET: T74019
        public ActionResult T74019()
        {
            return View();
        }
        [HttpPost]

        public ActionResult InsertOrUpdate(T74019 Degree)
        {
            bool status = repository.InsertOrUpdate(Degree);
            return Json(status, JsonRequestBehavior.AllowGet); ;

        }
         
        public async Task<ActionResult> Index()
        {
            TRequest obj = new TRequest();
            TParam OOO = new TParam();
            //TParam tParamList1 = new TParam();
            List<TParam> tParamList = new List<TParam>();
            using (var client = new HttpClient())
            {

                
                Dictionary<string, string> pairs = new Dictionary<string, string>();
                pairs.Add("sp", "Get_Data_T01004");
                pairs.Add("db", "OrclConHospital");
                


                tParamList.Add(new TParam() { Key = "sp", Value = "Get_Data_T01004" });



                obj.db = "OrclConHospital";
                obj.sp = "Get_Data_T01004";

                //foreach (var i in tParamList)
                //{
                //    pairs.Add(i.Key, i.Value);
                //}

                for (int i = 0; i < tParamList.Count; i++)
                {
                    obj.dict = tParamList;
                    //pairs.Add("db", "OrclConHospital");
                }
                var myContent = JsonConvert.SerializeObject(obj);
               // var data = new JavaScriptSerializer().Serialize(obj);

                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
               // FormUrlEncodedContent formContent = new FormUrlEncodedContent(pairs);

                // HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(APILink.GetDataNoParam + "?json=", byteContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                   
                   // Deserializing the response recieved from web api and storing into the Employee list
                   // obj = JsonConvert.DeserializeObject<obj.dictList.ToList>(EmpResponse);

                }
               
                // returning the employee list to view
                return View(response);
            }
        }
        //For Gridview Method Start 
        [HttpPost]
        public ActionResult GetLabelData()
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
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length);
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
                    GD = repository.GetLabelData(PageIndex, PageSize);
                    cnt = repository.GetGridData_Search_Count(searchValue);
                }
                else
                {
                    GD = repository.GetGrid_Data_Search(searchValue, PageIndex, PageSize);
                    cnt = repository.GetGridData_Search_Count(searchValue);
                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult deleteData(int T_JOB_TYPE_ID)
        {
            var del = repository.Delete(T_JOB_TYPE_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}