using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Queries;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Web;
using GenCode128;
using Newtonsoft.Json;

namespace Ambucare.Controllers.Queries
{
    public class Q74001Controller : Controller
    {
        private IQ74001 repository;
        public Q74001Controller(IQ74001 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: Q74001
        public ActionResult Q74001()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult GetUserIDByAMBRegID()
        {
            try
            {
                var data = repository.GetUserIDByAMBRegID();
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
        public ActionResult GetActiveAmbulance()
        {
            try
            {
                var data = repository.GetActiveAmbulance();
              //  string JSONString = string.Empty;
               // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetDischargeAmbulance()
        {
            try
            {
                var data = repository.GetDischargeAmbulance();
                //  string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetallPatients()
        {
            try
            {
                var data = repository.GetallPatients();
                //  string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult WetwaitingAmbulance()
        {
            try
            {
                var data = repository.WetwaitingAmbulance();
                //  string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetMaleAmbulance()
        {
            try
            {
                var data = repository.GetMaleAmbulance();
                //  string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetfemalAmbulance()
        {
            try
            {
                var data = repository.GetfemalAmbulance();
                //  string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult R74001Report(String T_FIRST_LANG2_NAME)
        {
            var dtT74001 = repository.GetPatientDetails(T_FIRST_LANG2_NAME);

            // dtT74001.TableName = "dtT74001";
            // dtT74001.WriteXmlSchema(Server.MapPath("~/Report/xml/T74001.xml"));

            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/R74001.frx"));
                // System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74001, "dtT74001");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    //var excelExport = new Excel2007Export();
                    // webReport.Report.Export(excelExport, strm);
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    // Response.ContentType = "Application/vnd.ms-excel";
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }


            return View();
        }

        public ActionResult R74001ReportWaittingAmbulance()
        {
            var dtT74001WaittingAmbu = repository.GetWaittingAmbulanceDetails();

            // dtT74001WaittingAmbu.TableName = "dtT74001WaittingAmbu";
            // dtT74001WaittingAmbu.WriteXmlSchema(Server.MapPath("~/Report/xml/T74001WaittingAmbu.xml"));

            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/R74001WaittingAmbu.frx"));
                // System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74001WaittingAmbu, "dtT74001WaittingAmbu");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    //var excelExport = new Excel2007Export();
                    // webReport.Report.Export(excelExport, strm);
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    // Response.ContentType = "Application/vnd.ms-excel";
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }


            return View();
        }

        public ActionResult R74001ReportPatientDetails(int T_AMBU_REG_ID)
        {
            var dtT74001PatientDetails = repository.ReportPatientDetails(T_AMBU_REG_ID);

           // dtT74001PatientDetails.TableName = "dtT74001PatientDetails";
           // dtT74001PatientDetails.WriteXmlSchema(Server.MapPath("~/Report/xml/T74001PatientDetails.xml"));

            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/R74001PatientDetails.frx"));
                // System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74001PatientDetails, "dtT74001PatientDetails");
                webReport.Report.Prepare();
                using (var strm = new MemoryStream())
                {
                    //var excelExport = new Excel2007Export();
                    // webReport.Report.Export(excelExport, strm);
                    var pdfExport = new PDFExport();
                    webReport.Report.Export(pdfExport, strm);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    // Response.ContentType = "Application/vnd.ms-excel";
                    Response.ContentType = "Application/PDF";
                    Response.BinaryWrite(strm.ToArray());
                    Response.End();
                }
                ViewBag.WebReport = webReport;
            }


            return View();
        }
        //GetMaleAmbulance
    }
}