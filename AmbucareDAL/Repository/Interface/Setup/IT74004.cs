using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74004 : IDisposable
    {
        IQueryable<T74051> MaritalData { get; }
        IQueryable<T74069> BloodGroupData { get; }
        IQueryable<T74050> GenderData { get; }
        IQueryable<T74059> ReligionData { get; }
        IQueryable<T74004> GetEmployeeData { get; }
        IQueryable<T74005> EmployeeTypeData { get; }
        //IQueryable<T02003> NationalityData { get; }
        //IQueryable<T74205> NationalityData { get; }
        IQueryable<T02003> NationalityData { get; }

        IEnumerable CheckPassportData(T74004 _t74004);
        string saveEmployeeInfo(T74004 t74004, string entryuser);
        IEnumerable GetGridData(int PageIndex, int PageSize);
        int GetGridData_Search_Count(string searchValue);
        IEnumerable GetGrid_Data_Search(string searchValue, int pageIndex, int pageSize);
    }
}
