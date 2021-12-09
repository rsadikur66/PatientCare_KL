using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
   public interface IT74148 : IDisposable
    {
        IQueryable<T02040> All { get; }
        bool InsertOrUpdate(T02040 _T02040, string userid);
        //For GridData Method
        IEnumerable getGridData(int PageIndex, int PageSize);
        int GetGridData_Search_Count(string searchValue);
        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);

        DataTable getGridAllData();
        bool Delete(int id);
    }
}
