using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Report;
using FastReport.Export.Pdf;
using FastReport.Web;

namespace Ambucare.Controllers.Report
{
    public class T74111ReportController : Controller
    {
        private readonly IT74111Report T74111_repository;
        public T74111ReportController(IT74111Report repository)
        {
            T74111_repository = repository;
        }
        // GET: T74111Report

        public ActionResult GetData(int id)
        {
            var dtT74111 = T74111_repository.GetData(id);
            dtT74111.TableName = "T74111";
            //dtT74111.WriteXmlSchema(Server.MapPath("~/Report/xml/GridDataReport.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74111GetGridAllData.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74111, "T74111");
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