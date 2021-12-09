using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Report;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Web;
using GenCode128;

namespace Ambucare.Controllers.Report
{
    public class R74123Controller : Controller
    {

        private readonly IR74123Report R74123report_repository;
        public R74123Controller(IR74123Report repository)
        {
            R74123report_repository = repository;
        }
        // GET: R74123
        public ActionResult Index()
        {
            return View();
        }


//public ActionResult GetPatientDetailsReport(int storeid)
//        {
//            var dtT74123 = T74123_repository.GetPatientDetails(storeid);

//            dtT74123.TableName = "T74123";
//            //dtT74123.WriteXmlSchema(Server.MapPath("~/Report/xml/T74027.xml"));
//            using (var webReport = new WebReport())
//            {
//                webReport.Report.Load(Server.MapPath("~/Report/Report/R74027.frx"));
//                System.Data.DataSet dataSet = new System.Data.DataSet();
//                webReport.Report.RegisterData(dtT74123, "T74123");
//                webReport.Report.Prepare();
//                using (var strm = new MemoryStream())
//                {
//                    var pdfExport = new PDFExport();
//                    webReport.Report.Export(pdfExport, strm);
//                    Response.ClearContent();
//                    Response.ClearHeaders();
//                    Response.Buffer = true;
//                    Response.ContentType = "Application/PDF";
//                    Response.BinaryWrite(strm.ToArray());
//                    Response.End();
//                }
//                ViewBag.WebReport = webReport;
//            }
//            return View();
//        }


        public ActionResult R74123Report(int T_REQUEST_ID)
        {
            var dtT74123 = R74123report_repository.GetPatientDetails(T_REQUEST_ID);
           
            //dtT74123.WriteXmlSchema(Server.MapPath("~/Report/xml/T74123.xml"));

            if (dtT74123.Rows.Count >0 )
            {
                dtT74123.TableName = "T74123";
                dtT74123.Columns.Add("Pat_BarCode", typeof(Bitmap));
                Image myimg = Code128Rendering.MakeBarcodeImage(dtT74123.Rows[0]["T_PAT_ID"].ToString(), int.Parse("2"), true);
                dtT74123.Rows[0]["Pat_BarCode"] = myimg;
                using (var webReport = new WebReport())
                {
                    webReport.Report.Load(Server.MapPath("~/Report/Report/R74123.frx"));
                    System.Data.DataSet dataSet = new System.Data.DataSet();
                    webReport.Report.RegisterData(dtT74123, "T74123");
                    webReport.Report.Prepare();
                    using (var strm = new MemoryStream())
                    {
                        var excelExport = new Excel2007Export();
                        webReport.Report.Export(excelExport, strm);
                        //var pdfExport = new PDFExport();
                        //webReport.Report.Export(pdfExport, strm);
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Buffer = true;
                        Response.ContentType = "Application/vnd.ms-excel";
                        // Response.ContentType = "Application/PDF";
                        Response.BinaryWrite(strm.ToArray());
                        Response.End();
                    }
                    ViewBag.WebReport = webReport;
                }
            }
            else
            {
                
                var dtT74123BodMeasure = R74123report_repository.GetPatientBodMeasure(T_REQUEST_ID);
                dtT74123BodMeasure.TableName = "T74123Measure";
                Image myimg = Code128Rendering.MakeBarcodeImage(dtT74123BodMeasure.Rows[0]["T_PAT_ID"].ToString(), int.Parse("2"), true);
                dtT74123BodMeasure.Columns.Add("Pat_BarCode", typeof(Bitmap));
                dtT74123BodMeasure.Rows[0]["Pat_BarCode"] = myimg;

              //  dtT74123BodMeasure.WriteXmlSchema(Server.MapPath("~/Report/xml/T74123BodMeasure.xml"));


                using (var webReport = new WebReport())
                {
                    webReport.Report.Load(Server.MapPath("~/Report/Report/T74123BodMeasure.frx"));
                    System.Data.DataSet dataSet = new System.Data.DataSet();
                    webReport.Report.RegisterData(dtT74123BodMeasure, "T74123Measure");
                    webReport.Report.Prepare();
                    using (var strm = new MemoryStream())
                    {
                        var excelExport = new Excel2007Export();
                        webReport.Report.Export(excelExport, strm);
                        //----------------For PDF-------------------
                        //var pdfExport = new PDFExport();
                        //webReport.Report.Export(pdfExport, strm);
                        //-----------------------------------
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Buffer = true;
                        Response.ContentType = "Application/vnd.ms-excel";
                        //Response.ContentType = "Application/PDF";
                        Response.BinaryWrite(strm.ToArray());
                        Response.End();
                    }
                    ViewBag.WebReport = webReport;
                }

            }




            return View();
        }
    }
}