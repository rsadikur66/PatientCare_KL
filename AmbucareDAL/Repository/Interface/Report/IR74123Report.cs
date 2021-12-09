using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Report
{
    public interface IR74123Report
    {
        DataTable GetPatientDetails(int requestid);
        DataTable GetPatientBodMeasure(int t_REQUEST_ID);
    }
}
