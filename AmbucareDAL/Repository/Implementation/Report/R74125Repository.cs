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
    public class R74125Repository : IR74125Report
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private readonly R74125DAL _r62104Dal = new R74125DAL();

        public R74125Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public DataTable GetPatInformation(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetPatInformation(T_REQUEST_ID);
            return Data;
        }

        public DataTable GetAdministeredMedicine(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetAdministeredMedicine(T_REQUEST_ID);
            return Data;
        }
        public DataTable GetVitalSignData(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetVitalSignData(T_REQUEST_ID);
            return Data;
        }
        public DataTable GetServiceData(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetServiceData(T_REQUEST_ID);
            return Data;
        }
        public DataTable GetLabelsData(string lang)
        {
            var Data = _r62104Dal.GetLabelsData(lang);
            return Data;
        }

        public DataTable GetGlasgowDataE(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetGlasgowDataE(T_REQUEST_ID);
            return Data;
        }

        public DataTable GetGlasgowDataM(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetGlasgowDataM(T_REQUEST_ID);
            return Data;
        }
        public DataTable GetGlasgowDataV(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetGlasgowDataV(T_REQUEST_ID);
            return Data;
        }
        public DataTable GetTriageLevel(string T_REQUEST_ID)
        {
            var Data = _r62104Dal.GetTriageLevel(T_REQUEST_ID);
            return Data;
        }

    }
}