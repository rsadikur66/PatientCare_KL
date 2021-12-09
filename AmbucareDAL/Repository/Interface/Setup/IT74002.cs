using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
using System.Collections;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74002 : IDisposable
    {
        void AddItemBrand(T74002 Z);
        bool Delete_T74002(int T_ITEM_BRA_ID);
        IEnumerable GetGridData(int PageIndex, int PageSize);

        int GetGridData_Search_Count(string searchValue);

        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
        T74002 Find(Int32 id);
        void Save();
        void Dispose();
    }
}
