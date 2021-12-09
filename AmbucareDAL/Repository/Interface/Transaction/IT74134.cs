using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
   public interface IT74134: IDisposable
    {
        IEnumerable GetPatInfo(int reId);
        IEnumerable GetVitalSign(int reqId);
        DataTable GetItem(int ambu,int rquestId);
        DataTable GetStockData(int ambu, int item);
        string SaveData(string lang,int rquestId, T74036 t74036, List<T74037> t74037);
        DataTable GetPreviousMedicine(int requId);
        DataTable GetServiceData(int requId);
        string SaveServiceData(string lang,int rquestId, T74074 t74074, List<T74079> t74079);
    }
}
