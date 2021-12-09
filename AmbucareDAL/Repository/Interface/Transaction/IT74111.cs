using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74111 : IDisposable
    {
        //Insert Method
        void Insert_T74073(List<T74089> _T74089);

        //AmbulanceDropdownList Method 
        IEnumerable GetAmbulanceDropdownList();

        //For GridData Method
        IEnumerable GetGridData(int PageIndex, int PageSize, int Id);

        //For GridData Count Method
        int GetGridData_Search_Count(string searchValue, int Id);

        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, int Id);


    }
}
