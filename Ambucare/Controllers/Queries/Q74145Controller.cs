using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Queries;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Queries
{
    public class Q74145Controller : Controller
    {
        // GET: Q74145
        private IQ74145 repository;
        private IError err;
        public Q74145Controller(IQ74145 ObjectIRepository,IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: Q74145
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetAllAcceptRequest(string from_date, string to_date)
        {
            try
            {
                var userid = Session["T_USER_ID"].ToString();
                var data = repository.GetAllAcceptRequest(userid,from_date,to_date);
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