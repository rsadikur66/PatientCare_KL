using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74011 : IDisposable   
    {

        IQueryable<T74011> All { get; }
        bool InsertOrUpdate(T74011 ItemColor);

        //For GridData Method
        IEnumerable GetLabelData(int PageIndex, int PageSize);
        int GetGridData_Search_Count(string searchValue);
        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);

        bool Delete(int id);
    }
}