using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74025 : IDisposable
    {
        void Get_T74025(T74025 Z);
        bool Delete_T74025(int T_ITEM_BRA_ID);
        T74025 Find(Int32 id);
        void Save();
        void Dispose();
        IEnumerable GetGridData(int PageIndex, int PageSize);

        int GetGridData_Search_Count(string searchValue);

        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
    }
}
