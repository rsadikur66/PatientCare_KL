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
   public interface IT74149
   {
       void insertStatus(T06301 _t06301);
       //IEnumerable<T06301> GridAllData();
       DataTable getGridAllData();

       IEnumerable GetGridData(int PageIndex, int PageSize);
       int GetGridData_Search_Count(string searchValue);
       IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
   }
}
