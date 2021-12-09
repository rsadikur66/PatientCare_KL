using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74015Controller : Controller
    {
        private readonly IT74015 repository;

        public T74015Controller(IT74015 _repository)
        {
            this.repository = _repository;
        }
        // GET: T74015
        public ActionResult Index()
        {
            return View();
        }

        //get save
        [HttpPost]
        public ActionResult Add_T74015(List<T74015> _T74015)
        {
           var entrityUser = HttpContext.Session["T_USER_ID"].ToString();
            var msg = repository.Add_T74015(_T74015, entrityUser);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add_t74073(T74073 _t74073)
        {
            var msg = repository.Add_t74073(_t74073);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        //get employee Id and employee details Ambulance 
        [HttpPost]
        public ActionResult GetEmployeeTypeId(int T_AMBU_REG_ID)
        {
            var list = repository.GetEmployeeTypeIdAndEmployeeIdByAmbulanceId(T_AMBU_REG_ID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetEmployeeData(string T_EMP_TYP_ID)
        {
            var entryuser = HttpContext.Session["T_USER_ID"].ToString();
            var list = repository.GetEmployeeData(T_EMP_TYP_ID, entryuser);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(list);
            return Json(JSONString, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getGridEmployeeTypeData(int T_AMBU_REG_ID)
        {
            try
            {
                var getemp = repository.getGridEmployeeTypeData(T_AMBU_REG_ID);
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
        public void Del_T74015(List<T74015> _T74015Del)
        {
            repository.Del_T74015(_T74015Del);

        }
        [HttpPost]
        public ActionResult GetMohTeam()
        {
            string lang = Session["T_LANG"].ToString();
            string zone = Session["T_ZONE_CODE"].ToString();
            var list = repository.GetMohTeam(lang, zone);
            //string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //get Ambulance List
        [HttpPost]
        public ActionResult GetAmbulanceDropdownList()
        {
            var user = Session["T_USER_ID"].ToString();
            var list = repository.GetAmbulanceDropdownList( user);
            //string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //get grid data
    }
}