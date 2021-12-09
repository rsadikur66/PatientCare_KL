using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74044:IDisposable
    {
        IEnumerable GetAmbReg();
        IEnumerable GetMohTeam(string lang, string zone);
        IEnumerable GetMohStation(string lang, string zone);
        IEnumerable GetGridData(int PageIndex, int PageSize, string lang, string zone, string user);
        int GetGridData_Search_Count(string searchValue, string lang, string zone, string user);
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, string lang, string zone, string user);
        string AddStore(T74044 _t74044, string lang);
    }
}
