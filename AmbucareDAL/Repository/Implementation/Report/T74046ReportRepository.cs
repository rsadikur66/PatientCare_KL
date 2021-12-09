using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Report;
using AmbucareDAL.Repository.Interface.Report;
using AmbucareDAL.Repository.Interface.Setup;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Report
{
    public class T74046ReportRepository : IT74046Report
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private T74046DAL _t74046Dal = new T74046DAL();
        public T74046ReportRepository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public DataTable GetPrescription(int T_REQUEST_ID)
        {
            var dtPresctiption = _t74046Dal.GetPrescription(T_REQUEST_ID);
            return dtPresctiption;
        }
        public DataTable GetBill(int T_REQUEST_ID)
        {
            var dtPresctiption = _t74046Dal.GetBill(T_REQUEST_ID);
            return dtPresctiption;
        }

        //for Patient Report

        public DataTable GetPatReport(int T_REQUEST_ID, string language)
        {
            var dtPatReport = _t74046Dal.GetPatReport(T_REQUEST_ID, language);
            return dtPatReport;
        }
    }
}