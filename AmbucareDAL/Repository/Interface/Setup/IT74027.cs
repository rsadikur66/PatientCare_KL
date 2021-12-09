using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74027 : IDisposable
    {
        IQueryable<T74054> GetCompanysData { get; }
        //IQueryable<T74001> GetItemsData { get; }

        IEnumerable GetProductItem(int id);
        IEnumerable GetStoresData(string zone);
        IEnumerable GetSupplierData();
        IEnumerable GetGridAllData(int T_STORE_ID);
        IQueryable<T74072> GetProductTypeData { get; }

        IEnumerable GetUom(int T_TYPE_ID);

        void insert_T74027(List<T74027> _t74027);
        void Del_t74027(List<T74027> _t74027Del);
        decimal GetStockQuantity(int? ITEM_ID);

        IEnumerable GetItemTypeList();
    }
}
