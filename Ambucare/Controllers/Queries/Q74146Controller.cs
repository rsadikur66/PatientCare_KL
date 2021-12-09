using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.DAL.Query;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Queries;
using FastReport.Export.Pdf;
using FastReport.Web;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Queries
{
    public class Q74146Controller : Controller
    {
        private IQ74146 repository;
        Q74146DAL _q74146Dal = new Q74146DAL();
        private IError err;
        public Q74146Controller(IQ74146 ObjectIRepository , IError errRepo)
        {
            repository = ObjectIRepository;
            err = errRepo;
        }
        // GET: Q74146
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAmbulanceList()
        {
            try
            {
                var zonCode = Session["T_ZONE_CODE"].ToString();
                var role = Session["T_ROLE_CODE"].ToString();
                var data = repository.GetAmbulanceList(zonCode, role);
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
        public JsonResult GetStorId(int ambuId)
        {
            try
            {
                //var zonCode = Session["T_ZONE_CODE"].ToString();
               // var role = Session["T_ROLE_CODE"].ToString();
                var data = repository.GetStorId(ambuId);
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
        public ActionResult GetStockItem(int ambuId)
        {
            
            var dt = repository.GetStockItem(ambuId);
            dt.TableName = "T74146";
            //  dt.WriteXmlSchema(Server.MapPath("~/Report/xml/Q74146MedicinStockByTeam.xml"));
            //  return View();
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/Q74146MedicinStockByTeam.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dt, "T74146");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }
            return View();
            
        }
       

        public ActionResult GetAcceptedData(string userid, string fromDate, string toDate)
        {


            var zoneCode = Session["T_ZONE_CODE"].ToString();
            var dt74146 = _q74146Dal.GetAcceptedData(zoneCode, userid, fromDate, toDate);
            dt74146.TableName = "T74146";
            //dt74146.WriteXmlSchema(Server.MapPath("~/Report/xml/T74146_ACCEPTED.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74146_ACCEPTED.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dt74146, "T74146");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }
            return View();
        }
        public ActionResult GetRejectedData(string userid, string fromDate, string toDate)
        {
            var zoneCode = Session["T_ZONE_CODE"].ToString();
            var dt74146 = _q74146Dal.GetRejectedData(zoneCode, userid, fromDate, toDate);
            dt74146.TableName = "T74146";
            //dt74146.WriteXmlSchema(Server.MapPath("~/Report/xml/T74146_REJECTED.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74146_REJECTED.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dt74146, "T74146");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }
            return View();
        }
        public ActionResult GetAllRequest(string userid, string fromDate, string toDate)
        {
            var zoneCode = Session["T_ZONE_CODE"].ToString();
            var dt74146 = _q74146Dal.GetAllRequest(zoneCode, userid, fromDate, toDate);
            dt74146.TableName = "T74146";
            //dt74146.WriteXmlSchema(Server.MapPath("~/Report/xml/T74146_ALLREQUEST.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74146_ALLREQUEST.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dt74146, "T74146");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }
            return View();
        }
        public ActionResult GetUsedMedicineByTeam(string userid)
        {
            var zoneCode = Session["T_ZONE_CODE"].ToString();
            var dt74146 = repository.GetUsedMedicineByTeam(userid);
            dt74146.TableName = "T74146";
            //dt74146.WriteXmlSchema(Server.MapPath("~/Report/xml/T74146_UsedMedicineByTeam.xml"));
           // return View();
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74146_UsedMedicineByTeam.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dt74146, "T74146");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }
            return View();
        }
        public ActionResult GetUsedMedicineByRequest(int requestId)
        {
            var zoneCode = Session["T_ZONE_CODE"].ToString();
            var dt74146 = repository.GetUsedMedicineByRequest(requestId);
            dt74146.TableName = "T74146";
            //dt74146.WriteXmlSchema(Server.MapPath("~/Report/xml/T74146_UsedMedicineByRequest.xml"));
           // return View();
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74146_ALLREQUEST.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dt74146, "T74146");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }
            return View();
        }

       
        //GetUsedMedicineByRequest
    }
}