using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
   public interface IT74117 : IDisposable
   {  //For Supplier List
       IEnumerable GetSupplierList();
       //Item Type List 
       IEnumerable GetItemTypeList();
        //Item Name List 
       IEnumerable GetItemNameList(int id);
        //UOM list
       IEnumerable GetUom(int T_TYPE_ID);
       //For Save
       string insert_T74117(T74048 t74048, List<T74049> t74049List);
       //Store From DropDownlist Method Start
       IEnumerable GetStoreListFrom();

       //Store To DropDownlist Method Start
       IEnumerable GetStoreListTo();

       //Currency DropdownList Method Start
       IEnumerable GetCurrencyList();

       //Person Type Popup Method Start
        IQueryable<T74005> GetPersonalType { get; }

       //Person Name Popup Method Start
       IEnumerable GetPersonName(int id);
   }
}
