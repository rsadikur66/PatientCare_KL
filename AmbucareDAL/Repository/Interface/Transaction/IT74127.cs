using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74127 : IDisposable
    {
        IEnumerable GetEmployee();
        IEnumerable GetEmployeeDetails(int empId);
        IEnumerable GetItem(int empId);
        bool SaveData(T74094 t74094, List<T74095> t74095);
        IEnumerable GetPreviousProblem(int empId, string en, string reg);
    }
}
