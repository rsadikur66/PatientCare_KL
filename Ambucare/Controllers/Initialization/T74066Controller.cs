using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using System.Linq.Dynamic;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74066Controller : Controller
    {
        private readonly IT74066 repository;
        public T74066Controller(IT74066 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74066()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRoleList()
        {
            var data = repository.GetRoleList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetFormTypeList()
        {
            var data = repository.GetFormTypeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetPageTypeList()
        {
            var data = repository.GetPageTypeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetFormList(string P_FORM_CODE)
        {
            var data = repository.GetFormList(P_FORM_CODE);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMaxOrderNo(int P_ROLE_CODE, int P_FORM_TYPE_ID, int P_PAGE_TYPE_ID)
        {
            var data = repository.GetMaxOrderNo(P_ROLE_CODE,P_FORM_TYPE_ID,P_PAGE_TYPE_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Insert(T74066 _T74066)
        {
            bool status = repository.Insert(_T74066);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGridData()
        {
            var data = repository.GetGridData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}