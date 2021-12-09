using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Report;
using FastReport.Export.Pdf;
using FastReport.Web;

namespace Ambucare.Controllers.Report
{
    public class T74027ReportController : Controller
    {
        private readonly IR74027Report T74027_repository;
        public T74027ReportController(IR74027Report repository)
        {
            T74027_repository = repository;
        }
        // GET: T74027Report
        public ActionResult T74027Report()
        {
            var dtT74027 = T74027_repository.GetStoresData();
            dtT74027.TableName = "T74027";
            //dtT74027.WriteXmlSchema(Server.MapPath("~/Report/xml/T74027.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74027.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74027, "T74027");
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
        public ActionResult T74027SuppliersReport()
        {
            var dtT74027 = T74027_repository.GetSuppliersData();
            dtT74027.TableName = "T74027";
            //dtT74027.WriteXmlSchema(Server.MapPath("~/Report/xml/T74027suppliers.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74027suppliers.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74027, "T74027");
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

        public ActionResult GetGridAllDataReport( int storeid)
        {
            var dtT74027 = T74027_repository.GetGridAllData(storeid);
           
            dtT74027.TableName = "T74027";
            //dtT74027.WriteXmlSchema(Server.MapPath("~/Report/xml/T74027.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/R74027.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74027, "T74027");
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
    }
}