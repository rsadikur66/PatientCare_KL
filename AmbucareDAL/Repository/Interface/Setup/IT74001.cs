using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74001 : IDisposable
    {
        //For Item Dropdown Data Start
        IQueryable<T74002> GetItemBrandData { get; }

        IEnumerable GetItemUMData(int T_TYPE_ID);

        IQueryable<T74008> GetIType { get; }

        //For Item Dropdown Data End

        IQueryable<T74001> All { get; }

        //For GridData Method
        IEnumerable GetLabelData(int PageIndex, int PageSize);
        int GetGridData_Search_Count(string searchValue);
        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);

        bool AddItem(T74001 T74001);

        //bool AddItem73(T74073 _T74073);

        void Save();

        bool Delete(int id);


    }
}