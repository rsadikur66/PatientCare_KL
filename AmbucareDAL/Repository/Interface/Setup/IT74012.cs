using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
   public interface IT74012 : IDisposable
    {
        //For Find Method
        T74012 Find(string id);

        //For Insert and Update Method
        void Insert_T74012(T74012 _T74012);


        //For Save change Method
        void Save();


        //For Deleted Method
        bool Deleted_T74012(int T_ITEM_UM_ID);


        //For GridData Method
        IEnumerable GetGridData(int PageIndex, int PageSize);

        int GetGridData_Search_Count(string searchValue);

        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);



        //For Dispose Method
        void Dispose();

       
    }
}
