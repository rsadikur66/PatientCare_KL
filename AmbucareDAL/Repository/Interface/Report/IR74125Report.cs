using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Report
{
    public interface IR74125Report
    {
        DataTable GetPatInformation(string T_REQUEST_ID);
        DataTable GetAdministeredMedicine( string T_REQUEST_ID);
        DataTable GetVitalSignData(string T_REQUEST_ID);
        DataTable GetServiceData(string T_REQUEST_ID);
        DataTable GetLabelsData(string lang);
        DataTable GetGlasgowDataE(string T_REQUEST_ID);
        DataTable GetGlasgowDataM(string T_REQUEST_ID);
        DataTable GetGlasgowDataV(string T_REQUEST_ID);
        DataTable GetTriageLevel(string T_REQUEST_ID);

    }
}
