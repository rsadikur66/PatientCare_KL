using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Transaction;
//using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74041Repository : IT74041
    {

        private AmbucareContainer obj = new AmbucareContainer();
        CommonDAL common = new CommonDAL();
        private readonly T74041DAL _t74041DAL = new T74041DAL();
        public T74041Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IEnumerable GetUserIDByAMBRegID(int T_AMBU_REG_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74026.Join(obj.T74057, t26 => t26.T_USER_ID, t57 => t57.T_USER_ID,
                    (t26, t57) => new { t26, t57 })
                .Join(obj.T74004, t26_57 => t26_57.t57.T_EMP_ID, t04 => t04.T_EMP_ID,
                    (t26_57, t04) => new { t26_57, t04 })
                .Join(obj.T74005, t26_57_t04 => t26_57_t04.t04.T_EMP_TYP_ID, t05 => t05.T_EMP_TYP_ID,
                    (t26_57_t04, t05) => new { t26_57_t04, t05 })
                .Join(obj.T74015, t26_57_t04_t05 => t26_57_t04_t05.t26_57_t04.t04.T_EMP_ID, t15 => t15.T_EMP_ID,
                    (t26_57_t04_t05, t15) => new { t26_57_t04_t05, t15 }).Where(
                    m => m.t26_57_t04_t05.t05.T_EMP_TYP_ID == 21 && m.t15.T_AMBU_REG_ID == T_AMBU_REG_ID
                ).Select(m => new { T_USER_ID = m.t26_57_t04_t05.t26_57_t04.t26_57.t26.T_USER_ID }).Take(1).ToList();
            return query;
        }
        public IEnumerable Gender()
        {
            return obj.T74050.Select(m => new { m.T_LANG2_NAME, m.T_SEX_CODE }).OrderBy(a => a.T_SEX_CODE).ToList();
        }
        public IEnumerable getCallReason(string lang)
        {
            return obj.T74121.Select(m => new { m.T_CALL_REASON_ID,T_REASON_NAME = lang == "2"? m.T_LANG2_NAME: m.T_LANG1_NAME }).OrderBy(a => a.T_CALL_REASON_ID).ToList();
        }
        public IEnumerable Result(int lang)
        {
            IEnumerable dd = Enumerable.Empty<object>();
            //var data = _t74041DAL.Result(lang);


            //var dd = (from t04 in obj.T74204
            //          select new
            //          {
            //              T_ID = t04.T_ID,
            //              T_CODE_VALUE = t04.T_CODE_VALUE,
            //              T_LANG1_NAME = t04.T_OPTION_NAME_H,
            //              T_LANG2_NAME = t04.T_OPTION_NAME_H,
            //          }).AsEnumerable().Select((a, i) => new
            //          {
            //              T_ID = a.T_ID,
            //              T_CODE_VALUE = a.T_CODE_VALUE,
            //              //T_LANG1_NAME =lang==1? Encoding.UTF8.GetString(a.T_LANG1_NAME) : Encoding.UTF8.GetString(a.T_LANG2_NAME),
            //              T_LANG1_NAME = lang == 1 ? a.T_LANG1_NAME : a.T_LANG2_NAME
            //          }).ToList();
            return dd;
        }

        public DataTable result_1(int lang)
        {
            var data = _t74041DAL.Result(lang);
            return data;
        }
        public decimal? GetLat(DateTime? entrTime, string userId)
        {
            //decimal? query = obj.T74026.Where(x => x.T_ENTRY_TIME == entrTime && x.T_USER_ID == userId).Select(x => x.T_LATITUDE).FirstOrDefault();
            decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId).Select(x => x.T_LATITUDE).FirstOrDefault();
            return query;
        }
        public decimal? GetLong(DateTime? entrTime, string userId)
        {
            //decimal? query = obj.T74026.Where(x => x.T_ENTRY_TIME == entrTime && x.T_USER_ID == userId).Select(x => x.T_LONGITUDE).FirstOrDefault();
            decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId).Select(x => x.T_LONGITUDE).FirstOrDefault();
            return query;
        }

        //public IEnumerable GetAllUserLatlong(string zonCode)//this method needed to copy for main source
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //    try
        //    {

        //        query = (from t26 in (from t in obj.T74026 group t by new { t.T_USER_ID } into t_n select new { t_n.Key.T_USER_ID, T_ENTRY_TIME = t_n.Max(q => q.T_ENTRY_TIME) })
        //                 join t57 in obj.T74057 on t26.T_USER_ID equals t57.T_USER_ID
        //                 where t57.T_LOGIN_STATUS == "1" && t57.T_ZONE_CODE == zonCode && t57.T_ROLE_CODE == 24
        //                 join t15 in obj.T74015 on t57.T_EMP_ID equals t15.T_EMP_ID
        //                 join t14 in obj.T74014 on t15.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID
        //                 join t47 in obj.T74047 on t14.T_AMB_TYPE_ID equals t47.T_AMBU_TYPE_ID
        //                 join t44 in (from t44 in obj.T74044
        //                              where !(from t in obj.T74041 where t.T_DISCH_STATUS == null group t by new { t.T_AMBU_REG_ID } into t_n select t_n.Key.T_AMBU_REG_ID)
        //                                        .Contains(t44.T_AMBU_REG_ID) && t44.T_AMBU_REG_ID != 0
        //                              select t44) on t15.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID

        //                 group new { t26, t57, t15, t14, t47, t44 } by new { t26.T_ENTRY_TIME, t26.T_USER_ID, t47.T_LANG2_NAME, t14.T_AMBU_REG_ID } into a
        //                 select new
        //                 {
        //                     //latitude = t26.T_LATITUDE,
        //                     //longitude = t26.T_LONGITUDE,
        //                     entrTime = a.Key.T_ENTRY_TIME,
        //                     userId = a.Key.T_USER_ID,
        //                     AMB_CAPASITY = a.Key.T_LANG2_NAME,
        //                     T_AMBU_REG_ID = a.Key.T_AMBU_REG_ID

        //                 }).AsEnumerable().Select((r, i) => new
        //                 {
        //                     T_AMBU_REG_ID = r.T_AMBU_REG_ID,
        //                     //AMB_CAPASITY = r.AMB_CAPASITY + "( " + r.userId + " )",
        //                     AMB_CAPASITY = r.userId,
        //                     latitude = GetLat(r.entrTime, r.userId),
        //                     longitude = GetLong(r.entrTime, r.userId),
        //                     markImg = "Amb_Marker",
        //                     text = "",
        //                     distance = 0,
        //                     acceptcount = acceptcount(zonCode, r.userId),
        //                     rejectcount = rejectcount(zonCode, r.userId)
        //                     //,T_PAT_ID = 0,
        //                     //PAT_NAME="",
        //                     //AGE = "",
        //                     //GENDER = "",
        //                     //T_MAP_LOC = ""
        //                 }).OrderBy(m => m.distance).ToList();

        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //    return query;
        //}

        public DataTable GetAllUserLatlong(string zonCode)
        {
            var data = _t74041DAL.GetAllUserLatlong(zonCode);
            return data;
        }
        public IEnumerable GetLoginUserLatlong(string T_USER_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74026.Where(a => a.T_USER_ID == T_USER_ID).Select(m => new
                {
                    latitude = m.T_LATITUDE,
                    longitude = m.T_LONGITUDE
                }).ToList();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }
        public DataTable GetAllDataOnMap_T74041(int P_AMBU_REG_ID, string P_LANG)
        {
            var data = _t74041DAL.GetAllDataOnMap_T74041(P_AMBU_REG_ID, P_LANG);
            return data;
        }

        public IEnumerable GetPatInfo()
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74046.Join(obj.T74050, t46 => t46.T_SEX_CODE, t50 => t50.T_SEX_CODE,
                (t46, t50) => new { t46, t50 }).Select(m => new
                {
                    T_PAT_ID = m.t46.T_PAT_ID,
                    T_FIRST_LANG2_NAME = m.t46.T_FIRST_LANG2_NAME,
                    T_FATHER_LANG2_NAME = m.t46.T_FATHER_LANG2_NAME,
                    T_GFATHER_LANG2_NAME = m.t46.T_GFATHER_LANG2_NAME,
                    T_FAMILY_LANG2_NAME = m.t46.T_FAMILY_LANG2_NAME,

                    T_SEX_CODE = m.t46.T_SEX_CODE,
                    Gender = m.t50.T_LANG2_NAME,
                    T_MOBILE_NO = m.t46.T_MOBILE_NO,
                    T_ALT_MOBILE_NO = m.t46.T_ALT_MOBILE_NO,
                    Age = m.t46.T_BIRTH_DATE
                }).ToList();
            return query;
        }
        public int Insert_t74046(T74046 _t74046)
        {
            obj.T74046.Add(_t74046);
            Save();
            return obj.T74046.Where(m => m.T_ENTRY_USER == _t74046.T_ENTRY_USER).Max(a => a.T_PAT_ID);

        }

        public void Insert_t74041(T74041 _t74041)
        {
            if (_t74041.T_REQUEST_ID == 0)
            {
                if (_t74041.T_AMBU_REG_ID == null)
                {
                    _t74041.T_REQUEST_TIME = _t74041DAL.dateTime();
                    _t74041.T_REQUEST_DATE = _t74041DAL.dateTime();
                    _t74041.T_ENTRY_DATE = _t74041DAL.dateTime();
                    // _t74041.T_ASSIGN_TIME = _t74041DAL.dateTime();
                    obj.T74041.Add(_t74041);
                    Save();
                }
                else
                {
                    _t74041.T_REQUEST_TIME = _t74041DAL.dateTime();
                    _t74041.T_REQUEST_DATE = _t74041DAL.dateTime();
                    _t74041.T_ENTRY_DATE = _t74041DAL.dateTime();
                    _t74041.T_ASSIGN_TIME = _t74041DAL.dateTime();
                    obj.T74041.Add(_t74041);
                    Save();
                }


            }
            else
            {
                var check = obj.T74041.Where(x => x.T_REQUEST_ID == _t74041.T_REQUEST_ID).FirstOrDefault();
                if (check != null)
                {
                    check.T_AMBU_REG_ID = _t74041.T_AMBU_REG_ID;
                    check.T_USER_ID = _t74041.T_USER_ID;
                    check.T_APPRX_TIME = _t74041.T_APPRX_TIME;
                    check.T_APPRX_DIST = _t74041.T_APPRX_DIST;
                    check.T_ASSIGN_TIME = _t74041DAL.dateTime();
                    Save();
                }

            }
        }



        public void Save()
        {
            try
            {
                obj.SaveChanges();
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
        public void Dispose()
        {
            obj.Dispose();
        }

        public string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;

            return String.Format("{0} Year(s) {1} Month(s) {2} Day(s)", Years, Months, Days);
        }

        public DataTable GetPendingRequestData(string user, string lang)
        {
            var data = _t74041DAL.GetPendingRequestData(user, lang);
            return data;
        }

        public string CancelReuest(int request, string canCode)
        {
            string sms = "";
            var chk = obj.T74041.Where(x => x.T_REQUEST_ID == request).FirstOrDefault();
            if (chk != null)
            {
                chk.T_OPER_CNCL_FLAG = "1";
                chk.T_CANCEL_REASON = canCode;
                chk.T_OPER_CNCL_DATE = common.dateTime();
                chk.T_CANCEL_STATUS = "1";
                chk.T_DISCH_STATUS = "1";
                Save();
                sms = "Cancel Successfully ";
            }

            return sms;
        }
        public IEnumerable GetCancelReasonData()
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t01 in obj.T74101
                     where t01.T_CANCEL_TYPE_ID == 1
                     select new
                     {
                         T_LANG2_NAME = t01.T_LANG2_NAME,
                         T_CANCEL_REASON_ID = t01.T_CANCEL_REASON_ID,
                     }).ToList();
            return query;
        }
        public IEnumerable GetAllHospitalLatlong()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T02065.Where(x => x.T_ZONE_CODE == "23").Select(m => new
                {
                    latitude = m.T_LATITUDE,
                    longitude = m.T_LONGITUDE,
                    T_LANG2_NAME = m.T_LANG2_NAME,
                    T_SITE_CODE = m.T_SITE_CODE

                }).ToList();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }

        public IEnumerable GetPatientInformation(int requestId)
        {
            IEnumerable query = Enumerable.Empty<object>();

            query = (from t41 in obj.T74041
                     join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID into t41_t46
                     from tt46 in t41_t46.DefaultIfEmpty()
                     join t06 in obj.T74050 on tt46.T_SEX_CODE equals t06.T_SEX_CODE into t46_t06
                     from tt06 in t46_t06.DefaultIfEmpty()
                     where t41.T_REQUEST_ID == requestId
                     select new
                     {
                         T_USER_ID = t41.T_USER_ID,
                         T_REQUEST_ID = t41.T_REQUEST_ID,
                         T_FIRST_LANG2_NAME = tt46.T_FIRST_LANG2_NAME,
                         T_FATHER_LANG2_NAME = tt46.T_FATHER_LANG2_NAME,
                         T_GFATHER_LANG2_NAME = tt46.T_GFATHER_LANG2_NAME,
                         T_MOBILE_NO = tt46.T_MOBILE_NO,
                         T_NATIONAL_ID = tt46.T_NATIONAL_ID,
                         T_ALT_MOBILE_NO = tt46.T_ALT_MOBILE_NO,
                         T_AGE = t41.T_AGE,
                         T_PROBLEM = t41.T_PROBLEM,
                         T_PROBLEM_DURATION = t41.T_PROBLEM_DURATION,
                         T_SEX_CODE = tt46.T_SEX_CODE,
                         GENDER = tt06.T_LANG2_NAME,

                     }).AsEnumerable().Select((r, i) => new
                     {
                         T_REQUEST_ID = r.T_REQUEST_ID,
                         T_FIRST_LANG2_NAME = r.T_FIRST_LANG2_NAME,
                         T_FATHER_LANG2_NAME = r.T_FATHER_LANG2_NAME,
                         T_GFATHER_LANG2_NAME = r.T_GFATHER_LANG2_NAME,
                         T_MOBILE_NO = r.T_MOBILE_NO,
                         T_ALT_MOBILE_NO = r.T_ALT_MOBILE_NO,
                         T_AGE = r.T_AGE,
                         T_PROBLEM = r.T_PROBLEM,
                         T_PROBLEM_DURATION = r.T_PROBLEM_DURATION,
                         T_SEX_CODE = r.T_SEX_CODE,
                         GENDER = r.GENDER,

                         latitude = GetLatForHos(r.T_USER_ID),
                         longitude = GetLongForHos(r.T_USER_ID)

                         //T_FIRST_LANG2_NAME = tt46.T_FIRST_LANG2_NAME,
                         //T_FATHER_LANG2_NAME = tt46.T_FATHER_LANG2_NAME,
                         //T_GFATHER_LANG2_NAME = tt46.T_GFATHER_LANG2_NAME,
                         //T_MOBILE_NO = tt46.T_MOBILE_NO,
                         //T_NATIONAL_ID = tt46.T_NATIONAL_ID,
                         //T_ALT_MOBILE_NO = tt46.T_ALT_MOBILE_NO,
                         //T_AGE = t41.T_AGE,
                         //T_PROBLEM = t41.T_PROBLEM,
                         //T_PROBLEM_DURATION = t41.T_PROBLEM_DURATION,
                         //T_SEX_CODE = tt46.T_SEX_CODE,
                         //GENDER = tt06.T_LANG2_NAME,



                     }).ToList();
            return query;
        }
        public decimal? GetLatForHos(string userId)
        {
            decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId).Max(x => x.T_LATITUDE);
            return query;
        }
        public decimal? GetLongForHos(string userId)
        {
            decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId).Max(x => x.T_LONGITUDE);
            return query;
        }

        public string UpdateByOperator(int requestId, string doc)
        {
            string sms = "";
            try
            {
                var chk = obj.T74041.Where(x => x.T_REQUEST_ID == requestId).FirstOrDefault();
                if (chk != null)
                {
                    chk.T_OPER_DES_CODE = doc;
                    chk.T_OPER_DES_TIME = common.dateTime();
                    Save();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return sms;
        }
        //public void GPS_Insert(decimal latitude, decimal longitude,string user)
        //{
        //    var time = DateTime.Now;
        //   // var user = HttpContext.Current.Session["T_USER_ID"].ToString();
        //    var check = obj.T74026.Where(x => x.T_USER_ID == user).FirstOrDefault();
        //    check.T_LATITUDE = latitude;
        //    check.T_LONGITUDE = longitude;
        //    check.T_ENTRY_TIME = time;
        //    Save();
        //}


        public DataTable GetActivePatientsData(string code, string lang)
        {
            var data = _t74041DAL.GetActivePatientsData(code, lang);
            return data;
        }
        public string SaveRemarks(int req, string rem)
        {
            var data = _t74041DAL.SaveRemarks(req, rem);
            return data;
        }

        //------------------Search Crieteria----------------------
        public DataTable getAmbulanceList(string zone)
        {
            var data = _t74041DAL.getAmbulanceList(zone);
            return data;
        }
        public DataTable getLoggedOutAmbulanceList(string zone)
        {
            var data = _t74041DAL.getLoggedOutAmbulanceList(zone);
            return data;
        }
        public DataTable getCleanNeedAmbulanceList(string zone)
        {
            var data = _t74041DAL.getCleanNeedAmbulanceList(zone);
            return data;
        }


        //----------------------Save  & update T74207 MOH Data------------
        public string Insert_T74207(T74207 t74207, T74041 t74041, T74046 t74046, string type, string user, string lang)
        {

            string res = _t74041DAL.Insert_T74207(t74207, t74041, t74046, type, user, lang);

            return res;

        }
        public DataTable getMaxProtocol()
        {
            var data = _t74041DAL.getMaxProtocol();
            return data;
        }
        public DataTable getAllAmb(string p_zone)//this method needed to copy for main source
        {
            var data = _t74041DAL.getAllAmb(p_zone);
            return data;
        }

        public string AsignPatientFromPendinglist(int reqId, string user, string operation)
        {
            var sms = "";
            sms = _t74041DAL.AsignPatientFromPendinglist(reqId, user, operation);
            return sms;

        }

        public DataTable GetCancelPatData(string lang, string user)
        {
            var data = _t74041DAL.GetCancelPatData(lang, user);
            return data;
        }

        public string SaveCancelConfirmData(string user, string cnfm, string reqId,string cnlRsn,int Tid)
        {
            var data = _t74041DAL.SaveCancelConfirmData(user, cnfm, reqId, cnlRsn, Tid);
            return data;
        }

        //public string acceptcount(string zone, string userid)//this method needed to copy for main source
        //{
        //    var data = _t74041DAL.acceptcount(zone, userid);
        //    return data;
        //}
        //public string rejectcount(string zone, string userid)//this method needed to copy for main source
        //{
        //    var data = _t74041DAL.rejectcount(zone, userid);
        //    return data;
        //}
        public string saveReq(T74041 t41, T74046 t46, T74120 t20, T74026 t26, string lang)
        {
            string msg = "";
            string msgS = "";
            string msgE = "";
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    string user = t41.T_ENTRY_USER;
                    DateTime date = common.dateTime();
                    
                    T74041 t041 = obj.T74041.Where(a => a.T_REQUEST_ID == t41.T_REQUEST_ID).FirstOrDefault();
                    if (t041==null)
                    {//-------insert
                       msgE = "S0137";
                        //----------------T74046
                        t46.T_ENTRY_USER = user;
                        t46.T_ENTRY_DATE = date;
                        t46.T_FIRST_LANG1_NAME = t46.T_FIRST_LANG2_NAME;
                        t46.T_GFATHER_LANG1_NAME = t46.T_GFATHER_LANG2_NAME;
                        obj.T74046.Add(t46);
                        obj.SaveChanges();
                        int patId = obj.T74046.Where(m => m.T_ENTRY_USER == user).Max(a => a.T_PAT_ID);
                        //----------------T74120
                        long callerId = 0;
                        if (t20.T_FIRST_LANG2_NAME!=null)
                        {
                            t20.T_ENTRY_USER = user;
                            t20.T_ENTRY_DATE = date;
                            t20.T_FIRST_LANG1_NAME = t20.T_FIRST_LANG2_NAME;
                            t20.T_LAST_LANG1_NAME = t20.T_LAST_LANG2_NAME;
                            obj.T74120.Add(t20);
                            obj.SaveChanges();
                            callerId = obj.T74120.Where(m => m.T_ENTRY_USER == user).Max(a => a.T_CALLER_ID);
                        }

                        //----------------T74041
                        t41.T_ENTRY_USER = user;
                        t41.T_ENTRY_DATE = date;
                        t41.T_REQUEST_DATE = date;
                        t41.T_REQUEST_TIME = date;
                        
                        t41.T_PAT_ID = patId;
                        if (callerId != 0)
                        {
                            t41.T_CALLER_ID = callerId;
                        }
                        if (t41.T_AMBU_REG_ID != null)
                        {
                            t41.T_ASSIGN_TIME = date;
                        }
                        obj.T74041.Add(t41);
                        obj.SaveChanges();
                        long reqId = obj.T74041.Where(m => m.T_ENTRY_USER == user).Max(a => a.T_REQUEST_ID); ;
                        //----------------T74026
                        T74026 t74026 = obj.T74026.Where(a => a.T_USER_ID ==user).OrderBy(b => b.ID).Take(1).FirstOrDefault();
                        if (t74026 != null && reqId!=0)
                        {
                            t26.ID = obj.T74026.Max(a => a.ID) + 1;
                            //t26.T_REQUEST_ID = unchecked((int)reqId);
                            t26.T_REQUEST_ID = (int)reqId;
                            t26.T_EVENT_ID = 1;
                            t26.T_LATITUDE = t74026.T_LATITUDE;
                            t26.T_LONGITUDE = t74026.T_LONGITUDE;
                            t26.T_ENTRY_TIME = date;
                            obj.T74026.Add(t26);
                            obj.SaveChanges();

                            if (t41.T_AMBU_REG_ID != null)
                            {
                                T74026 t026=new T74026();
                                //t026 = t26;
                                t026.ID = obj.T74026.Max(a => a.ID) + 1;
                                t026.T_REQUEST_ID = (int)reqId;
                                t026.T_EVENT_ID = 2;
                                t026.T_LATITUDE = t74026.T_LATITUDE;
                                t026.T_LONGITUDE = t74026.T_LONGITUDE;
                                t026.T_ENTRY_TIME = date;
                                obj.T74026.Add(t026);
                                obj.SaveChanges();
                            }

                            
                        }
                        msgS = "S0012";
                    }
                    else
                    {//-------update
                        msgE = "S0138";
                        //----------------T74041
                        t041.T_AMBU_REG_ID = t41.T_AMBU_REG_ID;
                        t041.T_USER_ID = t41.T_USER_ID;
                        t041.T_APPRX_TIME = t41.T_APPRX_TIME;
                        t041.T_APPRX_DIST = t41.T_APPRX_DIST;
                        t041.T_AGE = t41.T_AGE;
                        t041.T_LATITUDE = t41.T_LATITUDE;
                        t041.T_LONGITUDE = t41.T_LONGITUDE;
                        t041.T_PROBLEM = t41.T_PROBLEM;
                        t041.T_PROBLEM_DURATION = t41.T_PROBLEM_DURATION;
                        t041.T_CALL_REASON_ID = t41.T_CALL_REASON_ID;
                        if (t41.T_AMBU_REG_ID != null)
                        {
                            t041.T_ASSIGN_TIME = date;
                        }
                        t041.T_UPD_USER = user;
                        t041.T_UPD_DATE = date;
                        obj.SaveChanges();
                        //----------------T74046
                        int? patId = obj.T74041.Where(i => i.T_REQUEST_ID == t41.T_REQUEST_ID).Select(k => k.T_PAT_ID)
                            .FirstOrDefault();
                        T74046 t046 = obj.T74046.Where(a => a.T_PAT_ID == patId).FirstOrDefault();
                        if (t046!=null)
                        {
                            t046.T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME;
                            t046.T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME;
                            t046.T_SEX_CODE = t46.T_SEX_CODE;
                            t046.T_MOBILE_NO = t46.T_MOBILE_NO;
                            t046.T_NATIONAL_ID = t46.T_NATIONAL_ID;
                            t046.T_UPD_USER = user;
                            t046.T_UPD_DATE = date;
                            obj.SaveChanges();

                        }
                        //----------------T74120
                        T74120 t120 = obj.T74120.Where(a => a.T_CALLER_ID == t41.T_CALLER_ID).FirstOrDefault();
                        if (t120 != null)
                        {
                            t120.T_FIRST_LANG2_NAME = t20.T_FIRST_LANG2_NAME;
                            t120.T_LAST_LANG2_NAME = t20.T_LAST_LANG2_NAME;
                            t120.T_MOBILE_NO = t20.T_MOBILE_NO;
                            t120.T_ADDRESS = t20.T_ADDRESS;
                            t120.T_UPD_USER = user;
                            t120.T_UPD_DATE = date;
                            obj.SaveChanges();

                        }
                        //----------------T74026
                        T74026 t74026 = obj.T74026.Where(a => a.T_USER_ID == user).OrderBy(b => b.ID).Take(1).FirstOrDefault();
                        if (t74026!=null)
                        {
                            if (t41.T_AMBU_REG_ID != null && t041.T_REQUEST_ID != 0)
                            {
                                t26.ID = obj.T74026.Max(a => a.ID) + 1;
                                t26.T_REQUEST_ID = t041.T_REQUEST_ID;
                                t26.T_EVENT_ID = 2;
                                t26.T_LATITUDE = t74026.T_LATITUDE;
                                t26.T_LONGITUDE = t74026.T_LONGITUDE;
                                t26.T_ENTRY_TIME = date;
                                obj.T74026.Add(t26);
                                obj.SaveChanges();
                            }
                        }
                        msgS = "S0003";
                        
                    }
                    dbContextTransaction.Commit();
                    msg = common.GetSingleMsg(lang, msgS);


                }
                catch (DbEntityValidationException dbEx)
                {
                    dbContextTransaction.Rollback();
                    msg = common.GetSingleMsg(lang, msgE);
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
            return msg;
        }

    }
}