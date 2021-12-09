using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Report;
using AmbucareDAL.Repository.Interface.Transaction;
using FastReport.Export.Pdf;
using FastReport.Web;
using GenCode128;

namespace Ambucare.Controllers.Report
{
    public class T74046ReportController : Controller
    {
        private readonly IT74046Report T74046report_repository;
        private readonly IT74046 T74046_repository;
        public T74046ReportController(IT74046Report _repository)
        {
            this.T74046report_repository = _repository;
        }

        //Get Report popup data Bind with IT74046Report
        public ActionResult T74046Report(int T_REQUEST_ID)
        {
            var dtT74046 = T74046report_repository.GetPrescription(T_REQUEST_ID);
            dtT74046.TableName = "T74046";
            dtT74046.Columns.Add("Pat_BarCode", typeof(Bitmap));
            Image myimg = Code128Rendering.MakeBarcodeImage(dtT74046.Rows[0]["T_PAT_ID"].ToString(), int.Parse("2"), true);
            dtT74046.Rows[0]["Pat_BarCode"] = myimg;
            //dtT74046.WriteXmlSchema(Server.MapPath("~/Report/xml/T74046.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/R74046.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74046, "T74046");
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
        public ActionResult T74046ReportBill(int T_REQUEST_ID)
        {
            var dtT74046 = T74046report_repository.GetBill(T_REQUEST_ID)
;
            dtT74046.TableName = "T74046";
            dtT74046.Columns.Add("Pat_BarCode", typeof(Bitmap));
            Image myimg = Code128Rendering.MakeBarcodeImage(dtT74046.Rows[0]["T_PAT_ID"].ToString(), int.Parse("2"), true);
            dtT74046.Rows[0]["Pat_BarCode"] = myimg;
            dtT74046.Columns.Add("Bill_BarCode", typeof(Bitmap));
            Image bill = Code128Rendering.MakeBarcodeImage(dtT74046.Rows[0]["T_BILL_ID"].ToString(), int.Parse("2"), true);
            dtT74046.Rows[0]["Bill_BarCode"] = bill;
            //dtT74046.WriteXmlSchema(Server.MapPath("~/Report/xml/T74046Bill.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74046Bill.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74046, "T74046");
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
        public ActionResult GetPatReport(int T_REQUEST_ID)
        {
            var dtT74046Patient = T74046report_repository.GetPatReport(T_REQUEST_ID, Session["T_LANG"].ToString());
            dtT74046Patient.TableName = "T74046";
            dtT74046Patient.Columns.Add("Pat_BarCode", typeof(Bitmap));
            Image myimg = Code128Rendering.MakeBarcodeImage(dtT74046Patient.Rows[0]["T_REQUEST_ID"].ToString(), int.Parse("2"), true);
            dtT74046Patient.Rows[0]["Pat_BarCode"] = myimg;
            //dtT74046Patient.Columns.Add("Req_BarCode", typeof(Bitmap));
            //Image bill = Code128Rendering.MakeBarcodeImage(dtT74046Patient.Rows[0]["T_BILL_ID"].ToString(), int.Parse("2"), true);
            //dtT74046Patient.Rows[0]["Req_BarCode"] = bill;
            //dtT74046Patient.WriteXmlSchema(Server.MapPath("~/Report/xml/T74046Patient.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/T74046Patient.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtT74046Patient, "T74046");
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