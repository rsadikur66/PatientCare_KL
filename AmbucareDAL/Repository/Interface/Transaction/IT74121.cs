using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74121 : IDisposable
    {
        IEnumerable GetProductList(string GEN_CODE);
        IEnumerable GetUomtList(int TypeId, string GEN_CODE, string TRADE_CODE);
        IEnumerable GenericData();
        IEnumerable GetTypeData();
        IEnumerable GetProTypeData(int type_id);
        bool SaveData(List<T74089> t74089);
    }
}
