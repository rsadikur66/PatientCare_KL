using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
   public interface IT74133
    {
        IEnumerable GetZone();

        IEnumerable GetStoreListTo(int ambulanceId, int EmployeeId);

        string AmbulanceId(string userId);

        string EmployeeId(string userId);

        IEnumerable GetGridAllData(string ambuId, string zone);

        string GetActive(int? T_AMBU_REG_ID, string T_SITE_CODE);

        void Insert_T74133(List<T74096> T74096);

        void Save();
    }
}
