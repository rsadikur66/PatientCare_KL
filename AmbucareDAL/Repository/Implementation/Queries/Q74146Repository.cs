using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Repository.DAL.Query;
using AmbucareDAL.Repository.Interface.Queries;

namespace AmbucareDAL.Repository.Implementation.Queries
{
    public class Q74146Repository:IQ74146
    {
        Q74146DAL _q74146Dal = new Q74146DAL();
        public DataTable GetAmbulanceList(string zone,string role)
        {
            var data = _q74146Dal.GetAmbulanceList(zone,role);
            return data;
        }

        

        public DataTable GetStockItem(int ambuId)
          {
              DataTable data = new DataTable();
              var req = _q74146Dal.getReqId(ambuId);
              int reqId = Convert.ToInt32(req);
              if (reqId!=0)
              {
                 data = _q74146Dal.GetStockItem(ambuId, reqId);
                 
            }
              return data;

        }

       public DataTable GetUsedMedicineByTeam(string userid)
        {
            var data = _q74146Dal.GetUsedMedicineByTeam(userid);
            return data;
        }

       public DataTable GetUsedMedicineByRequest(int requestId)
        {
            var data = _q74146Dal.GetUsedMedicineByRequest(requestId);
            return data;
        }

       public DataTable GetStorId(int ambuId)
       {
           var data = _q74146Dal.GetStorId(ambuId);
           return data;
       }
    }
}