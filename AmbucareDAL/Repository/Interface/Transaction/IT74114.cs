using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
   public interface IT74114:IDisposable
   {  //For Supplier List
       IEnumerable GetSupplierList();

        //For Store List
       IEnumerable GetStoreList();

       //Currency DropdownList Method Start
       IEnumerable GetCurrencyList();

        //Item Type List 
       IEnumerable GetItemTypeList();

        //Item Name List 

        //for max value
        IEnumerable GetMaxValue();
        IEnumerable GetItemNameList(int id);

       IEnumerable GetUom(int T_TYPE_ID);
        //For Save
       string insert_T74114(T74030 t74030, List<T74031> t74031List);
       
    }
}
