using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Repository.Interface.Report;
using FastReport.Export.Pdf;
using FastReport.Web;

namespace Ambucare.Controllers.Report
{
    public class R74120Controller : Controller
    {
        private readonly IR74120Report _r74120Report;

        public R74120Controller(IR74120Report iR74120Report)
        {
            _r74120Report = iR74120Report;
        }

        // GET: R74120
        public ActionResult R74120()
        {
            return View();
        }

        public ActionResult GetAllData(int storeid)
        {
            var dtR74120 = _r74120Report.GetAllData(storeid);
            dtR74120.TableName = "R74120";
            //dtR74120.WriteXmlSchema(Server.MapPath("~/Report/xml/R74120.xml"));
            using (var webReport = new WebReport())
            {
                webReport.Report.Load(Server.MapPath("~/Report/Report/R74120.frx"));
                System.Data.DataSet dataSet = new System.Data.DataSet();
                webReport.Report.RegisterData(dtR74120, "R74120");
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

        //For Store To List DropDown Data
        [HttpPost]
        public ActionResult GetStoreListTo()
        {
            var ambulanceId = _r74120Report.AmbulanceId(Session["T_USER_ID"].ToString());
            var employeeId = _r74120Report.EmployeeId(Session["T_USER_ID"].ToString());
           var store = _r74120Report.GetStoreListTo(Convert.ToInt32(ambulanceId), Convert.ToInt32(employeeId));
            return Json(store, JsonRequestBehavior.AllowGet);
        }       

       
        //For Gridview Method Start 
        [HttpPost]
        public async Task<ActionResult> GetGridData(int flag, int ambuRegId)
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                Int32 PageIndex = 0;
                Int32 PageSize = 0;
                
                if (start == "0")
                {
                    PageIndex = 0;
                }
                else
                {
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length);
                }
                PageSize = Convert.ToInt32(length);
                //IQueryable allCustomer;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                IEnumerable GD = null;
                int cnt = 0;

                if (searchValue == "")
                {
                    if (flag == 3)
                    {
                        GD = _r74120Report.GetGridData(PageIndex, PageSize, ambuRegId);
                        cnt = _r74120Report.GetGridData_Search_Count(searchValue, ambuRegId);
                    }
                    else if (flag == 4)
                    {
                        GD = _r74120Report.GetGridDataBill(PageIndex, PageSize, ambuRegId);
                        cnt = _r74120Report.GetGridData_Search_CountBill(searchValue, ambuRegId);
                    }
                }
                else
                {
                    if (flag == 3)
                    {
                        GD = _r74120Report.GetGrid_Data_Search(searchValue, PageIndex, PageSize, ambuRegId);
                        cnt = _r74120Report.GetGridData_Search_Count(searchValue, ambuRegId);
                    }
                    else if (flag == 4)
                    {
                        GD = _r74120Report.GetGrid_Data_SearchBill(searchValue, PageIndex, PageSize, ambuRegId);
                        cnt = _r74120Report.GetGridData_Search_CountBill(searchValue, ambuRegId);
                    }
                }

                recordsTotal = cnt;
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = await Task.FromResult(GD) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //For Gridview Method End
    }
}