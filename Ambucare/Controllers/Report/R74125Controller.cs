using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Report;
using FastReport.Export.Pdf;
using FastReport.Web;

namespace Ambucare.Controllers.Report
{
    public class R74125Controller : Controller
    {
        private readonly IR74125Report _r74125Report;

        public R74125Controller(IR74125Report iR74125Report)
        {
            _r74125Report = iR74125Report;
        }

        // GET: R74125
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllReportData(string T_REQUEST_ID)
        {
            DataTable dtPat = _r74125Report.GetPatInformation(T_REQUEST_ID);
            DataTable dtMed = _r74125Report.GetAdministeredMedicine(T_REQUEST_ID);
            DataTable dtVital = _r74125Report.GetVitalSignData(T_REQUEST_ID);
            DataTable dtService = _r74125Report.GetServiceData(T_REQUEST_ID);
            DataTable dtLabels = _r74125Report.GetLabelsData(Session["T_LANG"].ToString());
            DataTable dtGlasgowDataE = _r74125Report.GetGlasgowDataE(T_REQUEST_ID);
            DataTable dtGlasgowDataM = _r74125Report.GetGlasgowDataM(T_REQUEST_ID);
            DataTable dtGlasgowDataV = _r74125Report.GetGlasgowDataV(T_REQUEST_ID);
            DataTable dtTriageLevel = _r74125Report.GetTriageLevel(T_REQUEST_ID);




            dtPat.TableName = "R74125_pat";
            dtMed.TableName = "R74125_med";
            dtVital.TableName = "R74125_vital";
            dtService.TableName = "R74125_service";
            dtLabels.TableName = "R74125_labels";
            dtGlasgowDataE.TableName = "R74125_glasgoE";
            dtGlasgowDataM.TableName = "R74125_glasgowM";
            dtGlasgowDataV.TableName = "R74125_glasgowV";
            dtTriageLevel.TableName = "R74125_triagelevel";
            DataSet ds = new DataSet();
            ds.Tables.Add(dtPat);
            ds.Tables.Add(dtMed);
            ds.Tables.Add(dtVital);
            ds.Tables.Add(dtService);
            ds.Tables.Add(dtLabels);
            ds.Tables.Add(dtGlasgowDataE);
            ds.Tables.Add(dtGlasgowDataM);
            ds.Tables.Add(dtGlasgowDataV);
            ds.Tables.Add(dtTriageLevel);


            //ds.WriteXmlSchema(Server.MapPath("~/Report/xml/R74125.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/R74125.frx"));
                webReport.Report.RegisterData(dtPat, "R74125_pat");
                webReport.Report.RegisterData(dtMed, "R74125_med");
                webReport.Report.RegisterData(dtVital, "R74125_vital");
                webReport.Report.RegisterData(dtService, "R74125_service");
                webReport.Report.RegisterData(dtLabels, "R74125_labels");

                webReport.Report.RegisterData(dtGlasgowDataE, "R74125_glasgoE");
                webReport.Report.RegisterData(dtGlasgowDataM, "R74125_glasgowM");
                webReport.Report.RegisterData(dtGlasgowDataV, "R74125_glasgowV");

                webReport.Report.RegisterData(dtTriageLevel, "R74125_triagelevel");




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
