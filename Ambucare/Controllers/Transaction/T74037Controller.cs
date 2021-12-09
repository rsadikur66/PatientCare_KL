using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Setup;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74037Controller : Controller
    {
        // GET: T74037
        private readonly IT74037 repository;
        private IError err;
        public T74037Controller(IT74037 objectRepository, IError errRepo)
        {
            repository = objectRepository;
            err = errRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCompanyData()
        {
            try
            {
                var bloodGroupdData = repository.GetCompanyData.ToList();
                return Json(bloodGroupdData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                err.SetServerErrorLog(ControllerContext.RouteData.Values["controller"].ToString(),
                    ControllerContext.RouteData.Values["action"].ToString(),
                    Session["T_USER_ID"] == null ? "" : Session["T_USER_ID"].ToString(), exc.Message);
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmployeeData()
        {

            try
            {
                var bloodGroupdData = repository.GetEmployeeData.ToList();
                return Json(bloodGroupdData, JsonRequestBehavior.AllowGet);
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