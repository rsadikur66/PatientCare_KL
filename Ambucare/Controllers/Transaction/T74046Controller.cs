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
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ambucare.Controllers.Transaction
{
    public class T74046Controller : Controller
    {
        // GET: T74046
        private readonly IT74046 repository;
        private IError err;
        public T74046Controller(IT74046 ObjectIRepository, IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMaritalData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            var MaritaldData = repository.MaritalData(lang);
            return Json(MaritaldData, JsonRequestBehavior.AllowGet);

                //string JSONString = string.Empty;
                //JSONString = JsonConvert.SerializeObject(data);
                //return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetReligionData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            var religionData = repository.ReligionData(lang);
            return Json(religionData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBloodGroupData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            //var bloodGroupdData = repository.BloodGroupData.ToList();
            var bloodGroupdData = repository.BloodGroupData(lang);
            return Json(bloodGroupdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetGenderData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            //var genderdData = repository.GenderData.ToList();
            var genderdData = repository.GenderData(lang);
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetNationality()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            //var genderdData = repository.NationalityData.ToList();
           // var genderdData = repository.NationalityData(lang);
            var genderdData = repository. NationalityData_1(lang);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(genderdData);
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
        public ActionResult GetDesignation()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            //var genderdData = repository.DesignationData.ToList();
            var genderdData = repository.DesignationData(lang);
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetChiefComplaintData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            //var genderdData = repository.ChiefComplaintData.ToList();
            var genderdData = repository.ChiefComplaintData(lang);
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProblemTypeData()
        {
            try
            {
            string lang = Session["T_LANG"].ToString();
            //var genderdData = repository.ProblemTypeData.ToList();
            var genderdData = repository.ProblemTypeData(lang);
            return Json(genderdData, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetAmCuRe(int T_REQUEST_ID)
        {
            try
            {
            Session["T_REQUEST_ID"] = T_REQUEST_ID;
            string lang = Session["T_LANG"].ToString();
            var data = repository.GetAmCuRe(lang, T_REQUEST_ID);
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
        public ActionResult getHealthScreenData(int request)
        {
            try
            {
            var data = repository.getHealthScreenData(request);
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
        public ActionResult healthScreenAllData(int reqID)
        {
            try
            {
            var data = repository.healthScreenAllData(reqID);
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

        //healthScreenAllData
        public ActionResult GetICD10DataInSearch(string icd)
        {
            try
            {

            var data = repository.GetICD10DataInSearch(icd);

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
        public ActionResult CancelData(T74041 can_t41)
        {
            try
            {
            var data = repository.CancelData(can_t41);

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
        //CancelData
       
        //Grid Employee start
        public ActionResult GetEmployeeData()
        {
            try
            {
                //Datatable parameter
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                //paging parameter
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //sorting parameter
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                //filter parameter
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
                    //PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length) + 1;
                }
                PageSize = Convert.ToInt32(length);

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                IEnumerable GD;
                int cnt;
                if (searchValue == "")
                {
                    GD = repository.GetGridDataPat(PageIndex, PageSize);
                    cnt = repository.GetPatData_Search_Count(searchValue);
                }
                else
                {
                    GD = repository.GetGrid_Data_SearchPat(searchValue, PageIndex, PageSize);
                    cnt = repository.GetPatData_Search_Count(searchValue);

                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //Grid Employee End

        //Grid Diagnosis start

        public ActionResult GetDiagnosisData(int PatId)
        {
            try
            {
                //Datatable parameter
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                //paging parameter
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //sorting parameter
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                //filter parameter
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
                    //PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length) + 1;
                }
                PageSize = Convert.ToInt32(length);

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                IEnumerable GD;
                int cnt;
                if (searchValue == "")
                {
                    GD = repository.GetGridDataDiagnosis(PageIndex, PageSize, PatId);
                    cnt = repository.GetDiagnosisData_Search_Count(searchValue, PatId);
                }
                else
                {
                    GD = repository.GetGrid_Data_SearchDiagnosis(searchValue, PageIndex, PageSize);
                    cnt = repository.GetDiagnosisData_Search_Count(searchValue, PatId);

                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
               
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //Grid Diagnosis End
        public ActionResult SaveEmployee(T74046 _T74046,T74041 t41)
        {
            try
            {
                var lang = Session["T_LANG"].ToString();
                var data = repository.SaveEmployee(lang, _T74046, t41);
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

        public ActionResult SaveDiagnosis(T74043 _T74043)
        {
            try
            {
            var lang = Session["T_LANG"].ToString();
            var reId = Session["T_REQUEST_ID"].ToString();
            int rquestId = Convert.ToInt32(reId);
            string msg = "";
            if (_T74043.T_PCHECKUP_ID == 0)
            {
                msg = "Save Successfully";
            }
            else
            {
                msg = "Update Successfully";
            }
          var data=  repository.SaveDiagnosis(rquestId,lang,_T74043);

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

        public ActionResult SaveDiagT74041(T74041 _T74041)
        {
            try
            {
            var lang = Session["T_LANG"].ToString();
            
          var data =  repository.SaveDiagT74041(lang,_T74041);

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
        public ActionResult GetRequestData()
        {
            try
            {
            var genderdData = repository.RequestData();
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAmbulanceData(int T_AMBU_REG_ID)
        {
            try
            {
            var genderdData = repository.AmbulanceData(T_AMBU_REG_ID);
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetServiceData(int T_AMBU_REG_ID)
        {
            try
            {

            
            var genderdData = repository.GetServiceData(T_AMBU_REG_ID);
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDiagnosis(int T_REQUEST_ID)
        {
            try
            {
            var genderdData = repository.GetDiagnosis(T_REQUEST_ID);
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Vet_Discount()
        {
            try
            {
            var genderdData = repository.GetVetDiscount.ToList();
            return Json(genderdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //Vet_Discount

        //Billing Part-------------Imran-------------Start
        [HttpPost]
        public ActionResult AmbulancePrice(int T_REQUEST_ID)
        {
            try
            {
            var data = repository.AmbulancePrice(T_REQUEST_ID);
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
        public ActionResult DoctorPrice(int T_Doc_ID)
        {
            try
            {
            var data = repository.DoctorPrice(T_Doc_ID);
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
        public ActionResult ServicePrice(int T_REQUEST_ID)
        {
            try
            {
            var data = repository.ServicePrice(T_REQUEST_ID);
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
        public ActionResult DiagonosisByReq(int T_REQUEST_ID)
        {
            try
            {
            var data = repository.DiagonosisByReq(T_REQUEST_ID);
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
        public ActionResult GetDocId(int T_REQUEST_ID)
        {
            try
            {
            var data = repository.GetDocId(T_REQUEST_ID);
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
        public JsonResult GetMedicineList(int T_request_Id)
        {
            try
            {
                var data = repository.GetMedicineList(T_request_Id);
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
        public JsonResult GetMedicineListByGen(int T_request_Id, string T_GEN_CODE, string T_REQUEST_STRENGTH,
            string T_DRUG_ROUTE_CODE, string T_FORM_ID)
        {
            try
            {
                var data = repository.GetMedicineListByGen(T_request_Id, T_GEN_CODE, T_REQUEST_STRENGTH,
                 T_DRUG_ROUTE_CODE, T_FORM_ID);
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

        public JsonResult GetStock(int T_STORE_ID, int T_ITEM_ID)
        {
            try
            {
                var data = repository.GetStock(T_STORE_ID, T_ITEM_ID);
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

        public JsonResult getId()
        {
            try
            {
                string uId = Session["T_USER_ID"].ToString();
                var data = repository.getId(uId);
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

        public JsonResult chkT36(int id)
        {
            try
            {
                var data = repository.chkT36(id);
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

        public JsonResult chkT39(int id)
        {
            try
            {
                var data = repository.chkT39(id);
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

        public ActionResult SaveBill(T74036 t74036, List<T74037> t74037List, T74074 t74074, List<T74079> t74079List)
        {
            try
            {
            repository.SaveBill(t74036, t74037List, t74074, t74079List);
            return Json(true, JsonRequestBehavior.AllowGet);
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

        public JsonResult chkBill(int rId)
        {
            try
            {
                var data = repository.chkBill(rId);
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
        public JsonResult prvServices(int rId)
        {
            try
            {
                var data = repository.prvServices(rId);
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

        public JsonResult GetConLevel()
        {
            try
            {
                var data = repository.GetConLevel();
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
        //Billing Part-------------Imran-------------End
        public void GPS_Insert(decimal latitude, decimal longitude)
        {
            string msg = "";

            try
            {
                var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
                repository.GPS_Insert(latitude, longitude, user);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
               // return Json(e.Message, JsonRequestBehavior.AllowGet);
               
            }
        }
        public string AcceptPatient(int requId)
        {
            try
            {

            var zone = System.Web.HttpContext.Current.Session["T_ZONE_CODE"].ToString();
            var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
            //repository.AcceptPatient(requId, user);
            return repository.AcceptPatient(requId, user, zone);

            }
            catch (Exception e)
            {
                string sms = "";
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
               
                 return  sms;
            }
        }
        public void SeenPatient(int requId)
        {
            try
            {
            var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
            repository.SeenPatient(requId, user);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
            }
        }
        public void caseRecieved(int requId)
        {
            try
            {
            var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
            repository.caseRecieved(requId, user);
        }
        catch (Exception e)
        {
            err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                ControllerContext.RouteData.Values["action"].ToString(),
                Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
            }
}
        public void reqAcceptofOper(int requId,string hosCode)
        {
            try
            {
            var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
            repository.reqAcceptofOper(requId, hosCode, user);
            }
            catch (Exception e)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), e.Message);
            }
        }

        /* Pervez */

        [HttpPost]
        public JsonResult CleanAmbulance(T74117 t74117)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
                var data = repository.CleanAmbulance(t74117,user,lang);
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
        public JsonResult CleanConfirmAmbulance(T74117 t74117)
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
                var data = repository.CleanConfirmAmbulance(t74117, user,lang);
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

        /* Pervez */

        [HttpPost]
        public JsonResult getSuggestedHospital(int requID)
        {
            try
            {
               
                var data = repository.getSuggestedHospital(requID);
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
        public JsonResult getSuggestedHospitalOper(int requID)
        {
            try
            {
               
                var data = repository.getSuggestedHospitalOper(requID);
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
        public JsonResult confirmDocHos(int T_REQUEST_ID, string T_SITE_CODE, string T_ROLE_CODE)
        {
            try
            {

                var data = repository.confirmDocHos(T_REQUEST_ID, T_SITE_CODE, T_ROLE_CODE);
                //string JSONString = string.Empty;
                //JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public JsonResult Cancel_Suggested_Hospital(int T_REQUEST_ID, string T_SITE_CODE)
        {
            try
            {

                var data = repository.Cancel_Suggested_Hospital(T_REQUEST_ID, T_SITE_CODE);
                //string JSONString = string.Empty;
                //JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelAndRerequest(int ambID, T74041 can_t41)
        {
            try
            {

            
            // var user = System.Web.HttpContext.Current.Session["T_USER_ID"].ToString();
            var company = Session["T_COMPANY_ID"].ToString();
         var data=   repository.CancelAndRerequest(ambID, can_t41, company);
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
        public ActionResult getGCS()
        {
            try
            {
            int lang = Int32.Parse(Session["T_LANG"].ToString());
            var data = repository.getGCS(lang);
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
        public ActionResult GetCancelReasonData()
        {
            try
            {

            int lang = Int32.Parse(Session["T_LANG"].ToString());
            var data = repository.GetCancelReasonData(lang);
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
      
        public ActionResult getECGImg(int T_REQUEST_ID)
        {
            try
            {
            var ECGImg = repository.getECGImg(T_REQUEST_ID);
            return Json(ECGImg, JsonRequestBehavior.AllowGet);
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
        public JsonResult getStationArrivalTime(int requID)
        {
            try
            {
                var data = repository.getStationArrivalTime(requID);
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
        public JsonResult getCancelReasonDataForType3()
        {
            try
            {

                var data = repository.getCancelReasonDataForType3();
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
        public JsonResult GetRequestID()
        {
            try
            {

                var data = repository.GetRequestID();
                // string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //GetRequestID
        [HttpPost]
        public JsonResult GetNewReqId()
        {
            try
            {
                var user = Session["T_USER_ID"].ToString();
                var data = repository.GetNewReqId(user);
                // string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
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
        public JsonResult getMewsData(int reId)
        {
            try
            {
                var user = Session["T_USER_ID"].ToString();
                var data = repository.getMewsData(user, reId);
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
    }
}