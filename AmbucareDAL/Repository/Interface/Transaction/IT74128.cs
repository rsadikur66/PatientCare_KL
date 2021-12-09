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
    public interface IT74128 : IDisposable
    {
        IEnumerable GetTrade();
        IEnumerable GetPackSize(int cosTyDel,int storeid);
        decimal GetStock(int item,int store,int umId);
        IEnumerable GetPrice(int costype, int umId);
        IEnumerable GetVat();
        IEnumerable GetPatienRequest();
        bool SaveData(T74036 t36, List<T74037> t37);
    }
}
