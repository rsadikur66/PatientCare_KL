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
using Ambucare.Models;
using FastReport.Data;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Export.Text;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Initialization
{
    public class T74096Controller : Controller
    {
        // GET: T74096
        private readonly IT74096 repository;
        public T74096Controller(IT74096 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74096()
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
        public ActionResult GetAmbPatList(string T_SITE_CODE)
        {
            try
            {
                var data = repository.GetAmbPatList(T_SITE_CODE);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public void Insert_T74096(List<T74096> T74096)
        {
            repository.Insert_T74096(T74096);
        }


    }
}