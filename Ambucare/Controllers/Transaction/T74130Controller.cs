using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Ambucare.Models;
using AmbucareDAL.Repository.Interface.Transaction;
using FastReport.Data;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Export.Text;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Transaction
{
    public class T74130Controller : Controller
    {
        // GET: T74130
        private readonly IT74130 repository;

        public T74130Controller(IT74130 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74130()
        {
            return View();
        }
        
         [HttpPost]
        public ActionResult GetZone()
        {
            try
            {
                var data = repository.GetZone();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSite(string zone)
        {
            try
            {
                var data = repository.GetSite(zone);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetAmbPatList()
        {
            try
            {
                var data = repository.GetAmbPatList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getAmbPatList_t74130()
        {
            try
            {
                var data = repository.GetAmbPatList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public void Insert_T74130(List<T74096> T74096)
        {
          // repository.Insert_T74130(T74096);
        }
    }
}