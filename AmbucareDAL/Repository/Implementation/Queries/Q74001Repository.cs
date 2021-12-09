using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Report;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Queries;

namespace AmbucareDAL.Repository.Implementation.Queries
{
    public class Q74001Repository: IQ74001
    {
        private AmbucareContainer obj = new AmbucareContainer();

        private readonly R74001DAL _r74001DAL = new R74001DAL();

        //private readonly Q74001DAL _q74001DAL = new Q74001DAL();
        public Q74001Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IEnumerable GetUserIDByAMBRegID()
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
                    m => m.t26_57_t04_t05.t05.T_EMP_TYP_ID == 21 
                ).Select(m => new { T_USER_ID = m.t26_57_t04_t05.t26_57_t04.t26_57.t26.T_USER_ID }).ToList();
            return query;
        }

        public IEnumerable GetActiveAmbulance()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t46_Pat in obj.T74046
                    join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                    join t15 in obj.T74015 on t41.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                    join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                    where t41.T_DISCH_STATUS == null && t46_Pat.T_FIRST_LANG2_NAME!=null
                         group new { t46_Pat, t41, t15, t57 } by new { t46_Pat.T_PAT_ID, t46_Pat.T_FIRST_LANG2_NAME, t46_Pat.T_MOBILE_NO, t41.T_PROB_DETAILS, t46_Pat.T_BIRTH_DATE,t41.T_REQUEST_DATE,t41.T_REQUEST_TIME,t41.T_SEEN_DATE,t41.T_SEEN_TIME } into a
                    select new
                    {
                        PatentId = a.Key.T_PAT_ID,
                        Name = a.Key.T_FIRST_LANG2_NAME,
                        Mobile = a.Key.T_MOBILE_NO,
                        Problem = a.Key.T_PROB_DETAILS,
                        BirthDate = a.Key.T_BIRTH_DATE,
                        RequistDate = a.Key.T_REQUEST_DATE,
                        RequistTime = a.Key.T_REQUEST_TIME,
                        SeenDate = a.Key.T_SEEN_DATE,
                        SeenTime = a.Key.T_SEEN_TIME
                    }
                ).AsEnumerable().Select((r, i) => new
                {
                    PatentId = r.PatentId,
                    Name = r.Name,
                    Mobile = r.Mobile,
                    Problem = r.Problem,
                    BirthDate = r.BirthDate,
                    RequistDate = r.RequistDate,
                    RequistTime=r.RequistTime,
                    SeenDate=r.SeenDate,
                    SeenTime=r.SeenTime
                }).ToList();//.ToList(); 
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
        public IEnumerable GetDischargeAmbulance()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t46_Pat in obj.T74046
                    join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                    join t15 in obj.T74015 on t41.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                    join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                    where t41.T_DISCH_STATUS == "1" && t46_Pat.T_FIRST_LANG2_NAME != null
                         group new { t46_Pat, t41, t15, t57 } by new { t46_Pat.T_PAT_ID, t46_Pat.T_FIRST_LANG2_NAME, t46_Pat.T_MOBILE_NO, t41.T_PROB_DETAILS, t46_Pat.T_BIRTH_DATE, t41.T_REQUEST_DATE, t41.T_REQUEST_TIME, t41.T_SEEN_DATE, t41.T_SEEN_TIME,t41.T_DISCHARGE_DATE,t41.T_DISCHARGE_TIME } into a
                    select new
                    {

                        PatentId = a.Key.T_PAT_ID,
                        Name = a.Key.T_FIRST_LANG2_NAME,
                        Mobile = a.Key.T_MOBILE_NO,
                        Problem = a.Key.T_PROB_DETAILS,
                        BirthDate = a.Key.T_BIRTH_DATE,
                        RequistDate = a.Key.T_REQUEST_DATE,
                        RequistTime = a.Key.T_REQUEST_TIME,
                        SeenDate = a.Key.T_SEEN_DATE,
                        SeenTime = a.Key.T_SEEN_TIME,
                        DischargeDate=a.Key.T_DISCHARGE_DATE,
                        DischargeTime=a.Key.T_DISCHARGE_TIME

                    }
                ).AsEnumerable().Select((r, i) => new
                {
                    PatentId = r.PatentId,
                    Name = r.Name,
                    Mobile = r.Mobile,
                    Problem = r.Problem,
                    BirthDate = r.BirthDate,
                    RequistDate = r.RequistDate,
                    RequistTime = r.RequistTime,
                    SeenDate = r.SeenDate,
                    SeenTime = r.SeenTime,
                    DischargeDate=r.DischargeDate,
                    DischargeTime=r.DischargeTime

                }).ToList();//.ToList(); 
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

        public IEnumerable GetallPatients()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t46_Pat in obj.T74046
                    join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                    join t15 in obj.T74015 on t41.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                    join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                    where t46_Pat.T_FIRST_LANG2_NAME != null
                         group new { t46_Pat, t41, t15, t57 } by new { t46_Pat.T_PAT_ID, t46_Pat.T_FIRST_LANG2_NAME, t46_Pat.T_MOBILE_NO, t41.T_PROB_DETAILS, t46_Pat.T_BIRTH_DATE } into g
                    select new
                    {
                        PatentId = g.Key.T_PAT_ID,
                        Name = g.Key.T_FIRST_LANG2_NAME,
                        Mobile = g.Key.T_MOBILE_NO,
                        Problem = g.Key.T_PROB_DETAILS,
                        BirthDate = g.Key.T_BIRTH_DATE
                    }).ToList();
                //    }
                //).AsEnumerable().Select((r, i) => new
                //{
                //    PatentId = r.PatentId,
                //    Name = r.Name,
                //    Mobile = r.Mobile,
                //    Problem = r.Problem,
                //    BirthDate = r.BirthDate
                // RequestId = GetRequest(r.PatentId)
                //.ToList(); 
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
        public IEnumerable WetwaitingAmbulance()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t15 in obj.T74015

                    join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                    join t44 in obj.T74044 on t15.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                    where t57.T_LOGIN_STATUS == "1" && t44.T_AMBU_REG_ID != 0
                    group new {t15, t57, t44} by new {t44.T_LANG2_NAME, t44.T_AMBU_REG_ID, t57.T_LOGIN_STATUS}into g select new
                    {
                        Ambulance = g.Key.T_LANG2_NAME,
                        AmbulanceID = g.Key.T_AMBU_REG_ID,
                        LogingStatus = g.Key.T_LOGIN_STATUS
                    }
                ).ToList();
                //AsEnumerable().Select((r, i) => new
                //{

                //    Ambulance =r.Ambulance,
                //    AmbulanceID =r.AmbulanceID,
                //    LogingStatus = r.LogingStatus
                //    // RequestId = GetRequest(r.PatentId)
                //}).ToList();//.ToList(); 
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
        public IEnumerable GetMaleAmbulance()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t46_Pat in obj.T74046
                    join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                    join t15 in obj.T74015 on t41.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                    join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                    where t46_Pat.T_SEX_CODE == 1 && t41.T_DISCH_STATUS == null 
                    && t46_Pat.T_FIRST_LANG2_NAME != null
                         group new {t46_Pat, t41, t15, t57} by new
                    {
                        t46_Pat.T_PAT_ID,
                        t46_Pat.T_FIRST_LANG2_NAME,
                        t46_Pat.T_MOBILE_NO,
                        t41.T_PROB_DETAILS,
                        t46_Pat.T_BIRTH_DATE,
                        t41.T_AMBU_REG_ID
                    }
                    into g
                    select new
                    {
                        PatentId=g.Key.T_PAT_ID,
                        Name = g.Key.T_FIRST_LANG2_NAME,
                        Mobile = g.Key.T_MOBILE_NO,
                        Problem = g.Key.T_PROB_DETAILS,
                        BirthDate = g.Key.T_BIRTH_DATE,
                        AmbulanceID = g.Key.T_AMBU_REG_ID
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
        public IEnumerable GetfemalAmbulance()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t46_Pat in obj.T74046
                    join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                    join t15 in obj.T74015 on t41.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                    join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                    where t46_Pat.T_SEX_CODE == 2 && t41.T_DISCH_STATUS == null 
                    && t46_Pat.T_FIRST_LANG2_NAME != null
                         group new {t46_Pat, t41, t15, t57} by new
                    {
                        t46_Pat.T_PAT_ID,
                        t46_Pat.T_FIRST_LANG2_NAME,
                        t46_Pat.T_MOBILE_NO,
                        t41.T_PROB_DETAILS,
                        t46_Pat.T_BIRTH_DATE,
                        t41.T_AMBU_REG_ID
                    }
                    into g
                    select new
                    {

                        PatentId = g.Key.T_PAT_ID,
                        Name = g.Key.T_FIRST_LANG2_NAME,
                        Mobile = g.Key.T_MOBILE_NO,
                        Problem = g.Key.T_PROB_DETAILS,
                        BirthDate = g.Key.T_BIRTH_DATE,
                        AmbulanceID = g.Key.T_AMBU_REG_ID
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
        public int GetRequest(int patId)
        {
            var re = obj.T74041.Where(x => x.T_PAT_ID == patId).Max(p => p.T_REQUEST_ID);
            return re;
        }

        public DataTable GetPatientDetails(String Condition)
        {
            var data = _r74001DAL.GetPatientDetails(Condition);
            return data;
            
        }
        public DataTable GetWaittingAmbulanceDetails()
        {
            var data = _r74001DAL.GetWaittingAmbulanceDetails();
            return data;

        }
        public DataTable ReportPatientDetails(int T_AMBU_REG_ID)
        {
            var data = _r74001DAL.ReportPatientDetails(T_AMBU_REG_ID);
            return data;

        }
    }
}