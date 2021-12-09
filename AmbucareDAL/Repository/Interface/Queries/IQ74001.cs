using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Queries
{
   public interface IQ74001
   {
       IEnumerable GetUserIDByAMBRegID();
       IEnumerable GetActiveAmbulance();
       IEnumerable GetDischargeAmbulance();
       IEnumerable GetallPatients();
       IEnumerable WetwaitingAmbulance();
       IEnumerable GetMaleAmbulance();
       IEnumerable GetfemalAmbulance();
       DataTable GetPatientDetails(String Condition);
       DataTable GetWaittingAmbulanceDetails();
       DataTable ReportPatientDetails(int T_AMBU_REG_ID);
    }
}
