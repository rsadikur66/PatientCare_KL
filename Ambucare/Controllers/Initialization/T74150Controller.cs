using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Setup;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74150Controller : Controller
    {
        private IT74150 repository;
        public T74150Controller(IT74150 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74150
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetItemTypeList()
        {
            try
            {
                var data = repository.GetItemTypeList();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetGenList()
        {
            try
            {
                var data = repository.GetGenList();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetItemsList(string itemtype)
        {
            try
            {
                var data = repository.GetItemsList(itemtype);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetFormList()
        {
            try
            {
                var data = repository.GetFormList();
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
        public ActionResult allGridData(Int32 typeid)
        {
            try
            {
                string lang = HttpContext.Session["T_LANG"].ToString();
                //Datatable parameter
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                //paging parameter
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //sorting parameter
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                //  var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
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
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length) + 0;
                }
                PageSize = Convert.ToInt32(length);
                //List<t12068> allGridData = new List<t12068>();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                // DataTable SearchValue = new DataTable();
                if (searchValue == "")
                {
                    GridData = repository.GetGridData(typeid, PageIndex, PageSize,lang);
                    CountValue = repository.GetGridData_Count(typeid, searchValue, PageIndex, PageSize,lang);
                }
                else
                {
                    GridData = repository.GetGridData_Search(typeid, searchValue, PageIndex, PageSize,lang);
                    CountValue = repository.GetGridData_Search_Count(typeid, searchValue, PageIndex, PageSize,lang);
                }
                IEnumerable v = (from DataRow row in GridData.Rows
                    select new
                    {
                        T_COST_TYPE_DTL_ID = row["T_COST_TYPE_DTL_ID"],
                        ITEM_TYPE_CODE = row["ITEM_TYPE_CODE"],
                        ITEM_TYPE = row["ITEM_TYPE"],
                        T_GEN_CODE = row["T_GEN_CODE"].ToString(),
                        GenericName = row["GenericName"].ToString(),
                        T_FORM_CODE = row["T_FORM_CODE"].ToString(),
                        FORM_TYPE = row["FORM_TYPE"].ToString(),
                        T_LANG2_NAME = row["T_LANG2_NAME"].ToString(),
                        T_LANG1_NAME = row["T_LANG1_NAME"].ToString(),
                        T_ITEM_CODE = row["T_ITEM_CODE"].ToString()
                        //T_DISP_SEQ = row["T_DISP_SEQ"].ToString(),
                        //T_DIFFERAL_DAY = row["T_DIFFERAL_DAY"].ToString(),
                        //T_QUS_NO_COLOR = row["T_QUS_NO_COLOR"].ToString(),
                        //T_SEX = row["T_SEX"].ToString(),
                        //T_GENDER = row["T_GENDER"].ToString(),
                        //T_ACTION = row["T_ACTION"].ToString(),
                        //T_IF_FAIL = row["T_IF_FAIL"].ToString(),
                        //T_ACTIVE = row["T_ACTIVE"].ToString()
                    }).ToList();
                //sort
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                //{
                //    //for make sort simpler we will add Syste.Linq.Dynamic reference
                //    //v = v; //.OrderBy(sortColumn + " " + sortColumnDir);
                //}

                recordsTotal = Convert.ToInt32(CountValue.Rows[0]["cval"]);//v.Count();
                // allGridData = v;//v.Skip(skip).Take(pageSize).ToList();

                // return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = v }, JsonRequestBehavior.AllowGet);
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = v }, JsonRequestBehavior.AllowGet);
                // return Json(v,JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult insert(string itemid, string costdtlid,string itemtype, string gencode, string formcode, string itemnameeng, string itemnameloc)
        {
            string user = HttpContext.Session["T_USER_ID"].ToString();
            string status = repository.insertdata(itemid, costdtlid, itemtype, gencode, formcode, itemnameeng, itemnameloc,user);
            return Json(status, JsonRequestBehavior.AllowGet);

        }


    }
}