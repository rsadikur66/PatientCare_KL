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
    public interface IT74046 : IDisposable
    {
        IEnumerable MaritalData(string lang);
        IEnumerable ReligionData(string lang);
        IEnumerable BloodGroupData(string lang);
        IEnumerable GenderData(string lang);
        IEnumerable NationalityData(string lang);
        DataTable NationalityData_1(string lang);
        IEnumerable DesignationData(string lang);
        IEnumerable ChiefComplaintData(string lang);
        IEnumerable ProblemTypeData(string lang);

        IEnumerable GetAmCuRe(string lang, int reqId);

        //// IEnumerable MaritalData();
        //IQueryable<T74059> ReligionData { get; }
        //IQueryable<T74069> BloodGroupData { get; }
        //IQueryable<T74050> GenderData { get; }
        /* IQueryable<T02003> NationalityData { get; }*/ //IQueryable<T74058> NationalityData { get; }
        //IQueryable<T74006> DesignationData { get; }
        //IQueryable<T74055> ChiefComplaintData { get; }
        //IQueryable<T02040> ProblemTypeData { get; }
        //  IQueryable<T74077> ProblemTypeData { get; }
        IEnumerable RequestData();
        IEnumerable AmbulanceData(int t_AMBU_REG_ID);
        string SaveEmployee(string lang, T74046 t74046,T74041 t41);

        //For Grid Patient
        IEnumerable GetGridDataPat(int pageIndex, int pageSize);
        int GetPatData_Search_Count(string searchValue);
        IEnumerable GetGrid_Data_SearchPat(string searchValue, int pageIndex, int pageSize);
        //====================
        string SaveDiagnosis(int rquestId, string lang,T74043 t74043);
        string SaveDiagT74041(string lang,T74041 t74041);

        //For Grid Dianosis
        IEnumerable GetGridDataDiagnosis(int pageIndex, int pageSize, int PatId);
        int GetDiagnosisData_Search_Count(string searchValue, int PatId);
        IEnumerable GetGrid_Data_SearchDiagnosis(string searchValue, int pageIndex, int pageSize);
        //IEnumerable GetAmCuRe();

        //=============
        IEnumerable GetServiceData(int AmbuID);
        IEnumerable GetDiagnosis(int t_REQUEST_ID);
        IQueryable<T74018> GetVetDiscount { get; }

        IEnumerable GetICD10DataInSearch(string icd);

        //Billing Part-------------Imran-------------Start
        IEnumerable AmbulancePrice(int T_REQUEST_ID);
        IEnumerable getId(string uId);
        int chkT39(int id);
        int chkT36(int id);
        IEnumerable DoctorPrice(int T_Doc_ID);
        IEnumerable healthScreenAllData(int reqID);
        bool CancelData(T74041 can_t41);
        IEnumerable ServicePrice(int T_REQUEST_ID);
        IEnumerable DiagonosisByReq(int T_REQUEST_ID);
        IEnumerable GetDocId(int T_REQUEST_ID);

        DataTable GetMedicineList(int T_request_Id);

        DataTable GetMedicineListByGen(int T_request_Id, string T_GEN_CODE, string T_REQUEST_STRENGTH,
            string T_DRUG_ROUTE_CODE, string T_FORM_ID);

        DataTable GetStock(int T_STORE_ID, int T_ITEM_ID);

        void SaveBill(T74036 t74036, List<T74037> t74037List, T74074 t74074, List<T74079> t74079List);

        //Billing Part-------------Imran-------------End
        IEnumerable getHealthScreenData(int request);
        int chkBill(int rId);
        IEnumerable prvServices(int rId);
        DataTable GetConLevel();
        void GPS_Insert(decimal latitude, decimal longitude,string user);
        //void AcceptPatient(int requId, string user);
        string AcceptPatient(int requId, string user, string zone);
        void SeenPatient(int requId, string user);
        void caseRecieved(int requId, string user);
        void reqAcceptofOper(int requId, string hosCode, string user);
        DataTable getSuggestedHospital(int requId);
        DataTable getSuggestedHospitalOper(int requId);
        string confirmDocHos(int T_REQUEST_ID, string T_SITE_CODE, string T_ROLE_CODE);

        string CancelAndRerequest(int ambId, T74041 canT41,string company);
        IEnumerable getGCS(int lang);
        IEnumerable GetCancelReasonData(int lang);
        IEnumerable getECGImg(int T_REQUEST_ID);
        DataTable getStationArrivalTime(int requId);
        IEnumerable getCancelReasonDataForType3();
        string Cancel_Suggested_Hospital(int t_REQUEST_ID, string t_SITE_CODE);
        IEnumerable GetRequestID();
        string CleanAmbulance(T74117 t74117,string user,string lang);
        string CleanConfirmAmbulance(T74117 t74117,string user,string lang);
        IEnumerable GetNewReqId(string user);
        DataTable getMewsData(string user,int reId);
    }
}
