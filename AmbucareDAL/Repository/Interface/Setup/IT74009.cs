using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74009 : IDisposable
    {
        void AddMedicalType(T74009 Z);
        bool Delete_T74009(int T_MEDIC_TYPE_ID);
        IQueryable<T74009> GetMedicalData{ get; }
        T74009 Find(string id);
        void Save();
        void Dispose();
        IEnumerable GetGridData(int PageIndex, int PageSize);

        int GetGridData_Search_Count(string searchValue);

        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
    }
}
