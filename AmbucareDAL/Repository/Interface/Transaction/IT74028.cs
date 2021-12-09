using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74028: IDisposable

    {
    void Add_T74028(T74028 Z);
    bool Delete_T74028(int T_PUR_REQ_ID);
    IEnumerable GetGridData(int PageIndex, int PageSize);

    int GetGridData_Search_Count(string searchValue);

    //For Grid Data Search Method
    IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);

    T74028 Find(Int32 id);
    void Save();
    void Dispose();
    }
}
