using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74112 : IDisposable
    {
        //For Get Data
        IQueryable<T74050> GetItem { get; }
        IQueryable<T74051> GetMaritalItem { get; }
        IQueryable<T02003> GetNationalityItem { get; }
        IQueryable<T74059> GetReligionItem { get; }

        //for label
        IQueryable<T74050> GetLabelDataT74050 { get; }

        //For Insert and Update
        bool InsertOrUpdateT74050(T74050 _T74050);
        bool InsertOrUpdateT74051(T74051 _T74051);
        bool InsertOrUpdateT02003(T02003 t02003, string userid);
        bool InsertOrUpdateT74059(T74059 _T74059);

        //FOR DELETE 
        bool Delete(int id);
        bool DeleteMarital(int id);
        bool DeleteNationality(int id);
        bool DeleteReligion(int id);

        //FOR SEARCHING
        IEnumerable GetGridData(int PageIndex, int PageSize);
        int GetGridData_Search_Count(string searchValue);
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
        //End Gender Searching

        IEnumerable GetGridDataMarital(int PageIndex, int PageSize);
        int GetGridDataMarital_Search_Count(string searchValue);
        IEnumerable GetGridMarital_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
        //End Marital Searching

        IEnumerable GetGridDataNationality(int PageIndex, int PageSize);
        int GetGridDataNationality_Search_Count(string searchValue);
        IEnumerable GetGridNationality_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
        //End Nationality Searching

        IEnumerable GetGridDataReligion(int PageIndex, int PageSize);
        int GetGridDataReligion_Search_Count(string searchValue);
        IEnumerable GetGridReligion_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);
        //End Religion Searching



    }
}