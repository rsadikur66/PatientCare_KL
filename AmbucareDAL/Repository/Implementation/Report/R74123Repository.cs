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
    public class R74123Repository : IR74123Report
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private readonly R74123DAL _r74123DAL = new R74123DAL();
        public R74123Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }
        public DataTable GetPatientDetails(int requestid)
        {
            var data = _r74123DAL.GetPatientDetails(requestid);
            return data;
        }
        public DataTable GetPatientBodMeasure(int requestid)
        {
            var data = _r74123DAL.GetPatientBodMeasure(requestid);
            return data;
        }
    }
}