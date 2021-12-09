using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74124 : IDisposable
    {
        IEnumerable GetAmbulance(int ambulanceId, int EmployeeId);
        IEnumerable GetPatint(int ambId);

        string AmbulanceId(string userId);

        string EmployeeId(string userId);
    }
}
