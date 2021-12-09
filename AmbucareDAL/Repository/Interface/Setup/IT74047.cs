using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
   public interface IT74047:IDisposable
    {
        IEnumerable GetGridData(int PageIndex, int PageSize);
        int GetGridData_Search_Count(string searchValue);
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
        bool InsertOrUpdate(T74047 AmbuType);

        IEnumerable GetLabelData(int PageIndex, int PageSize);
        void Save();
        bool Delete(int id);
    }
}
