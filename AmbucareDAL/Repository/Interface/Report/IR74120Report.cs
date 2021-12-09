using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Report
{
   public interface IR74120Report
    {
        DataTable GetAllData(int storeid);

        IEnumerable GetStoreListTo(int ambulanceId , int EmployeeId);

        IEnumerable GetGridData(int PageIndex, int PageSize, int ambuRegId);

        int GetGridData_Search_Count(string searchValue, int ambuRegId);

        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, int ambuRegId);

        IEnumerable GetGridDataBill(int PageIndex, int PageSize, int storId);

        int GetGridData_Search_CountBill(string searchValue, int storId);

        IEnumerable GetGrid_Data_SearchBill(string searchValue, Int32 PageIndex, Int32 PageSize, int storId);

        string AmbulanceId(string userId);

        string EmployeeId(string userId);
    }
}
