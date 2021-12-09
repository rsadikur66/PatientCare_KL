using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Transaction
{
   public interface IT74042 : IDisposable
    {
        IEnumerable GetAllHospitalLatlong(string zonCode);
        IEnumerable GetPatientInformation(int requestId);
        string UpdateByOperator(int requestId, string doc, string user, string lang);
    }
}
