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
    public interface IT74041:IDisposable
    {
        IEnumerable Gender();
        IEnumerable getCallReason(string lang);
        IEnumerable Result(int lang);
        DataTable result_1(int lang);
        IEnumerable GetUserIDByAMBRegID(int T_AMBU_REG_ID);
        //IEnumerable GetAllUserLatlong(string zonCode);
        DataTable GetAllUserLatlong(string zonCode);
        IEnumerable GetLoginUserLatlong(string T_USER_ID);
        DataTable GetAllDataOnMap_T74041(int P_AMBU_REG_ID,string P_LANG);
        IEnumerable GetPatInfo();
        int Insert_t74046(T74046 _t74046);
        void Insert_t74041(T74041 _t74041);

        string Insert_T74207(T74207 t74207, T74041 t74041, T74046 t74046, string type,string user,string lang);

        void Save();
        //void GPS_Insert(decimal latitude, decimal longitude,string user);
        DataTable GetPendingRequestData(string user,string lang);

        string CancelReuest(int request, string canCode);
        IEnumerable GetAllHospitalLatlong();
        IEnumerable GetPatientInformation(int requestId);
        string UpdateByOperator(int requestId, string doc);
        IEnumerable GetCancelReasonData();
        DataTable GetActivePatientsData(string code,string lang);
        string SaveRemarks(int req, string rem);

        //------------------Search Crieteria----------------------
        DataTable getAmbulanceList(string zone);
        DataTable getLoggedOutAmbulanceList(string zone);
        DataTable getCleanNeedAmbulanceList(string zone);
        DataTable getMaxProtocol();
        DataTable getAllAmb(string p_zone);//this method needed to copy for main source
        string AsignPatientFromPendinglist(int reqId, string user,string operation);
        DataTable GetCancelPatData(string lang, string user);
        string SaveCancelConfirmData(string user,string cnfm, string reqId,string cnlRsn,int Tid);

        //string acceptcount(string zone, string userid);//this method needed to copy for main source
        //string rejectcount(string zone, string userid);//this method needed to copy for main source
        string saveReq(T74041 t41, T74046 t46, T74120 t20, T74026 t26,string lang);
    }
}
