using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74119 : IDisposable
    {
        IEnumerable getStoreName(int T_STORE_ID);
        decimal getStock(int? ITEM_ID, int? UM_ID, int? STORE_ID);
        //IEnumerable getRequisition(int T_SL_REQ_ID);
        decimal? avgSalePrice(int? T_COST_TYPE_DTL_ID, int? T_ITEM_UM_ID);
        IEnumerable getSalesData(int T_SL_REQ_ID);
        IEnumerable getEmpList(int T_STORE_ID);
        void Insert(T74027 t74027, T74036 t74036, List<T74037> t74037List, T74048 t74048, List<T74049> t74049List);
    }
}
