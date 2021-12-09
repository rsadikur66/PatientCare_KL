using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74120 : IDisposable
    {  //For Supplier List
        IEnumerable GetSupplierList();
        //Item Type List 
        IEnumerable GetItemTypeList();
        //Item Name List 
        IEnumerable GetItemNameList(int id);
        //UOM list
        IEnumerable GetUom(int T_TYPE_ID);
        //For Insert Method Start
        string Insert_T74120(T74080 t74080, List<T74081> t74081List);

        //Store From DropDownlist Method Start
        IEnumerable GetStoreListFrom();

        //Store To DropDownlist Method Start
        IEnumerable GetStoreListTo();

        //Currency DropdownList Method Start
        IEnumerable GetCurrencyList();

        //Person Type Popup Method Start
        IEnumerable GetPersonType();

        //Person Name Popup Method Start
        IEnumerable GetPersonName(int id);
        //Get stock Method Start
        decimal GetStock(int? ITEM_ID, int? UM_ID, int? STORE_ID);

        //Get Purchage Value Start
        decimal GetPurPrice(int ITEM_ID, int UM_ID);
    }
}
