using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;
using FastReport.Data;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Export.Text;

namespace Ambucare.Controllers.Initialization
{
    public class T74002Controller : Controller
    {
        // GET: T74002
        private readonly IT74002 repository;

        public T74002Controller(IT74002 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        public ActionResult T74002()
        {
            return View();
        }

        //get grid data
        [HttpPost]
        public ActionResult GetItemBrandData()
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
                    PageIndex = Convert.ToInt32(start) / Convert.ToInt32(length); //1;
                }
                PageSize = Convert.ToInt32(length);
                //IQueryable allCustomer;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                DataTable GridData = new DataTable();
                DataTable CountValue = new DataTable();
                IEnumerable GD;
                int cnt;
                if (searchValue == "")
                {
                    GD = repository.GetGridData(PageIndex, PageSize);
                    cnt = repository.GetGridData_Search_Count(searchValue);
                }
                else
                {
                    GD = repository.GetGrid_Data_Search(searchValue, PageIndex, PageSize);
                    cnt = repository.GetGridData_Search_Count(searchValue);
                }
                recordsTotal = cnt;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GD }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }

        }

        //get save
        [HttpPost]
        public void AddItemBrand(T74002 _t74002)
        {
            repository.AddItemBrand(_t74002);
        }
        //delete
        [HttpPost]
        public ActionResult Delete_T74002(int T_ITEM_BRA_ID)
        {
            var del = repository.Delete_T74002(T_ITEM_BRA_ID);
            return Json(del, JsonRequestBehavior.AllowGet);
        }


        private DataSet DataSetGetir()
        {
            var resultDataSet = new DataSet();
            using (var valueTable = new DataTable("message"))
            {
                valueTable.Columns.Add("to");
                valueTable.Columns.Add("from");
                valueTable.Columns.Add("heading");
                valueTable.Columns.Add("body");

                var ilkRow = valueTable.NewRow();
                ilkRow["to"] = "ali";
                ilkRow["from"] = "veli";
                ilkRow["heading"] = "baslik1";
                ilkRow["body"] = "ilk mesajım";

                var ikinciRow = valueTable.NewRow();
                ikinciRow["to"] = "temel";
                ikinciRow["from"] = "dursun";
                ikinciRow["heading"] = "baslik2";
                ikinciRow["body"] = "ikinci mesajım";

                valueTable.Rows.Add(ilkRow);
                valueTable.Rows.Add(ikinciRow);

                resultDataSet.Tables.Add(valueTable);

                return resultDataSet;
            }
        }

        [HttpPost]
        public void RaporGoster()
        {

        //    FastReport.Utils.Config.WebMode = true;

        //    #region dataseti manuel oluşturuyorum

        //    var resultDataSet = DataSetGetir();

        //    #endregion

        //    var rootPath = HttpContext.Server.MapPath("~");

        //    var frxFile = rootPath + @"testdata.frx";
        //    var pdfOutputFile = rootPath + @"testdata.pdf";
        //    PDFExport pdfExport;

        //    using (var fastReport = new FastReport.Report())
        //    {
        //        fastReport.Load(frxFile);
        //        fastReport.RegisterData(resultDataSet);

        //        if (fastReport.Prepare())
        //        {
        //            using (pdfExport = new FastReport.Export.Pdf.PDFExport())
        //            {
        //                pdfExport.Export(fastReport, pdfOutputFile);
        //            }
        //        }
        //    }

        //    return File(pdfOutputFile, "application/pdf");
        }
    }
}