using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74123 : IDisposable
    {
        IEnumerable GetPatients();
        IEnumerable PatientDetalisData(int id);
        IEnumerable PriscriptionData(int id);
        IEnumerable BodyMeasurementData(int id);
    }
}
