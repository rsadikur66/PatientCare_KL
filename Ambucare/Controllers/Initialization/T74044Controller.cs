using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74044Controller : Controller
    {
        // GET: T74044
        private readonly IT74044 repository;

        public T74044Controller(IT74044 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74044()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAmbReg()
        {
            var list = repository.GetAmbReg();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMohTeam()
        {
            string lang = Session["T_LANG"].ToString();
            string zone = Session["T_ZONE_CODE"].ToString();
            var list = repository.GetMohTeam(lang, zone);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMohStation()
        {
            string lang = Session["T_LANG"].ToString();
            string zone = Session["T_ZONE_CODE"].ToString();
            var list = repository.GetMohStation(lang, zone);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetStoreData()
        {
            try
            {
                string lang = Session["T_LANG"].ToString();
                string zone = Session["T_ZONE_CODE"].ToString();
                string user = Session["T_USER_ID"].ToString();
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
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length); //+ 1;
                }
                PageSize = Convert.ToInt32(length);

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                IEnumerable GD;
                int cnt;
                int recordsTotal = 0;

                if (searchValue == "")
                {
                    GD = repository.GetGridData(PageIndex, PageSize, lang, zone, user);
                    cnt = repository.GetGridData_Search_Count(searchValue, lang, zone, user);
                }
                else
                {
                    GD = repository.GetGrid_Data_Search(searchValue, PageIndex, PageSize, lang, zone, user);
                    cnt = repository.GetGridData_Search_Count(searchValue, lang, zone, user);
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
        public ActionResult AddStore(T74044 _t74044)
        {
            string lang = Session["T_LANG"].ToString();
            string user = Session["T_USER_ID"].ToString();
            _t74044.T_ENTRY_USER = user;
            var msg = repository.AddStore(_t74044, lang);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}