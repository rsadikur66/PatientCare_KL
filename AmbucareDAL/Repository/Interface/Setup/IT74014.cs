using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74014 : IDisposable
    {
        //For Find Method
        T74014 Find(string id);

        //For Insert and Update Method
        void Insert_T74014(T74014 _T74014);

        //For Save change Method
        void Save();

        //For Deleted Method
        bool Deleted_T74014(int T_AMBU_REG_ID);

        //For GridData Method
        IEnumerable GetGridData(int PageIndex, int PageSize, string entryUser);

        int GetGridData_Search_Count(string searchValue, string entryuser);

        //For Grid Data Search Method
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, string entryUser);

        //Get Color Id Method
        IQueryable<T74011> GetColorId { get; }

        //Get Ambulance Id Method
        IQueryable<T74047> GetAmbulanceId { get; }

        //Get Brand Id Method
        IQueryable<T74002> GetBrandId { get; }
        //Get Brand Id Method
        IQueryable<T74004> GetEmployeeData { get; }

        int GetRegNo(string regNo);
        int GetChesisNo(string chesisNo);
        int GetEngineNo(string engineNo);

        //For Dispose Method
        void Dispose();

        IEnumerable getAmbulanceOwnerData(string user);
    }
}
