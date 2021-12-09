using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
   public interface IT74068:IDisposable
    {
        //For Find Method
        T74068 Find(string id);

        //For Insert and Update Method
        void Insert_T74068(T74068 _T74068);


        //For Save change Method
        void Save();


        //For Deleted Method
        bool Deleted_T74068(int T_ITEM_UM_ID);


        //For GridData Method
        IEnumerable GetGridData(int PageIndex, int PageSize);

        int GetGridData_Search_Count(string searchValue);

        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);

        //For Dispose Method
        void Dispose();
    }
}
