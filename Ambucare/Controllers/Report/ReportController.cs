using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Report;
using AmbucareDAL.Repository.Interface.Transaction;
using FastReport.Export.Pdf;
using FastReport.Web;

namespace Ambucare.Controllers.Report
{
    public class ReportController : Controller
    {
        private readonly IT74015Report T74015report_repository;
        private readonly IT74015 T74015_repository;
        public ReportController(IT74015Report _repository)
        {
            this.T74015report_repository = _repository;
        }

        //Get Report popup data Bind with IT74015Report
        public ActionResult T74015Report()
        {
            var dtT74015 = T74015report_repository.GetEmployeeTypeIdAndEmployeeIdByAmbulanceIdReport();
            dtT74015.TableName = "T74015";
            //dtT74015.WriteXmlSchema(Server.MapPath("~/Report/xml/T74015.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74015.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74015, "T74015");
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

        public ActionResult R74120()
        {
            return View();
        }
        public ActionResult R74125()
        {
            return View();
        }
    }
}