using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74042Repository:IT74042
    {
        private AmbucareContainer obj = new AmbucareContainer();
        CommonDAL common = new CommonDAL();
        private readonly T74131DAL _t74131Dal = new T74131DAL();
        public T74042Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }


        public IEnumerable GetAllHospitalLatlong(string zonCode)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T02065.Where(x => x.T_ZONE_CODE == zonCode).Select(m => new
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
                join t65 in obj.T02065 on t41.T_SITE_DIS_CODE equals t65.T_SITE_CODE into t41_t65
                from tt65 in t41_t65.DefaultIfEmpty()
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
                    T_OPER_DES_CODE = t41.T_OPER_DES_CODE,
                    T_SITE_DIS_CODE = t41.T_SITE_DIS_CODE,
                    HOSPITAL_NAME = tt65.T_LANG2_NAME,
                   HOS_T_LATITUDE = tt65.T_LATITUDE,
                    HOS_T_LONGITUDE = tt65.T_LONGITUDE,

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
                T_OPER_DES_CODE = r.T_OPER_DES_CODE,
                T_SITE_DIS_CODE=r.T_SITE_DIS_CODE,
                HOSPITAL_NAME= r.HOSPITAL_NAME,
                HOS_T_LATITUDE = r.HOS_T_LATITUDE,
                HOS_T_LONGITUDE = r.HOS_T_LONGITUDE,

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
        public string GetLatForHos(string userId)
        {
            //var id = obj.T74026.Where(x => x.T_USER_ID == userId).Max(x => x.ID);
            //decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId && id.Contains(x.ID)).Select(x => x.T_LATITUDE);
            string query =
                common.Query(
                        $"select T_LATITUDE   from t74026 where T_USER_ID ='{userId}' AND ID IN (SELECT MAX(ID) FROM T74026 WHERE T_USER_ID = '{userId}')")
                    .Rows[0]["T_LATITUDE"].ToString();
            return query;
        }
        public string GetLongForHos(string userId)
        {
            //decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId).Max(x => x.T_LONGITUDE);
            string query =
                common.Query(
                        $"SELECT T_LONGITUDE   from t74026 where T_USER_ID ='{userId}' AND ID IN (SELECT MAX(ID) FROM T74026 WHERE T_USER_ID = '{userId}')")
                    .Rows[0]["T_LONGITUDE"].ToString();
            return query;
        }

        public string UpdateByOperator(int requestId, string doc, string user, string lang)
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
                    sms = _t74131Dal.reqHos(requestId, doc, user,lang);

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
    }
}