using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Report;
using AmbucareDAL.Repository.Interface.Report;

namespace AmbucareDAL.Repository.Implementation.Report
{
    public class R74046Repository: IR74046Report
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private R74046DAL _r74046Dal = new R74046DAL();
        public R74046Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public DataTable GetPrescription(int T_REQUEST_ID)
        {
            var dtPresctiption = _r74046Dal.GetPrescription(T_REQUEST_ID);
            return dtPresctiption;
        }
        public DataTable GetBill(int T_REQUEST_ID)
        {
            var dtPresctiption = _r74046Dal.GetBill(T_REQUEST_ID);
            return dtPresctiption;
        }
    }
}