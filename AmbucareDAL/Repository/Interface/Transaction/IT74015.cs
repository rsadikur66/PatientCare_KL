using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74015 : IDisposable
    {
        IEnumerable GetEmployeeTypeIdAndEmployeeIdByAmbulanceId(int T_AMBU_REG_ID);
        IEnumerable GetEmployeeData(string T_EMP_TYP_ID, string entryuser);
        IEnumerable getGridEmployeeTypeData(int T_AMBU_REG_ID);
        string Add_T74015(List<T74015> _T74015,string entrityUser);
        string Add_t74073(T74073 _t74073);
        void Del_T74015(List<T74015> _T74015Del);

        IEnumerable GetMohTeam(string lang, string zone);
        IEnumerable GetAmbulanceDropdownList(string user);
    }
}
