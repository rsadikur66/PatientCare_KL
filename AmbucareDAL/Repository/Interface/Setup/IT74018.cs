using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74018:IDisposable
    {

        IQueryable<T74072> GetCostTypeData { get; }
        bool Insert(T74018 _T74018);
        //For GridData Method
        IEnumerable GetLabelData(int PageIndex, int PageSize);
        int GetGridData_Search_Count(string searchValue);
        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);

        bool Delete(int id);
    }
}
