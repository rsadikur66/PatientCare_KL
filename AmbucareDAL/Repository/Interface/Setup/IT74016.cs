using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
   public interface IT74016:IDisposable
   {
       //For Find Method Start
       T74016 Find(string id);

       //For Insert and Update Method
       void Insert_T74016(T74016 _T74016);

       //For Deleted Method
       bool Deleted_T74016(int T_CURRENCY_ID);

       //For GridData Method
       IEnumerable GetGridData(int PageIndex, int PageSize);

       //For Grid Data Search Count Method
       int GetGridData_Search_Count(string searchValue);

       //For Grid Data Search Method
       IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
    }
}
