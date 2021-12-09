using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74125 : IDisposable
    {
        IEnumerable GetPatientDetails(int patId,string userId,int rquestId);
        IEnumerable GetBill(int patId);
        IEnumerable GetBillDetails(int request);
        IEnumerable GetIssueDetails(int request);
        IEnumerable GetIssueSumary(int request);
        IEnumerable PatData(int T_PAT_ID);
        void UpdateT74046(int T_PAT_ID,string T_DISCH_DEST);
        string UpdateT74041(T74041 t74041);
        DataTable GetDischargeReason();
        string verifySummeryReport(int requestId);
    }
}
