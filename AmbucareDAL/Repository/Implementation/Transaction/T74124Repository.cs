using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74124Repository : IT74124
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74124Repository(AmbucareContainer _obj)
        {
            //obj.Configuration.ProxyCreationEnabled = false;
            this.obj = _obj;

        }

        public IEnumerable GetAmbulance(int ambulanceId, int EmployeeId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if (ambulanceId == 0)
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                             from t14_t44 in t14_44.DefaultIfEmpty()
                             join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                             from t14_t11 in t14_11.DefaultIfEmpty()
                             join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                             from t14_t02 in t14_02.DefaultIfEmpty()
                             select new
                             {
                                 T_STORE_ID = t44.T_STORE_ID,
                                 T_AMBU_REG_ID = t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 T_YEAR_MODEL = t14_t44.T_YEAR_MODEL,
                                 T_SERIES = t14_t44.T_SERIES,
                                 Brand_Lang2 = t14_t02.T_LANG2_NAME,
                                 Color_Lang2 = t14_t11.T_LANG2_NAME
                             }).AsEnumerable().Select(k => new
                             {
                                 T_STORE_ID = k.T_STORE_ID,
                                 T_AMBU_REG_ID = k.T_AMBU_REG_ID,
                                 T_LANG2_NAME = k.T_STORE_ID == 1
                                 ? k.Central_Lang2
                                 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).ToList();
                }
                else
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID
                             join t11 in obj.T74011 on t14.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID
                             join t02 in obj.T74002 on t14.T_BRAND_ID equals t02.T_ITEM_BRA_ID
                             join t15 in obj.T74015 on t44.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                             where t15.T_AMBU_REG_ID == ambulanceId
                             && t15.T_EMP_ID == EmployeeId
                             select new
                             {
                                 T_STORE_ID = t44.T_STORE_ID,
                                 T_AMBU_REG_ID = t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 T_YEAR_MODEL = t14.T_YEAR_MODEL,
                                 T_SERIES = t14.T_SERIES,
                                 Brand_Lang2 = t44.T_LANG2_NAME,
                                 Color_Lang2 = t11.T_LANG2_NAME
                             }).AsEnumerable().Select(k => new
                             {
                                 T_STORE_ID = k.T_STORE_ID,
                                 T_AMBU_REG_ID = k.T_AMBU_REG_ID,
                                 T_LANG2_NAME = k.T_STORE_ID == 1
                                 ? k.Central_Lang2
                                 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).ToList();

                }

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }

        public IEnumerable GetPatint(int ambId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t46 in obj.T74046
                    join t41_amb in obj.T74041 on t46.T_PAT_ID equals t41_amb.T_PAT_ID
                    join t74_bill in obj.T74074 on t41_amb.T_REQUEST_ID equals t74_bill.T_REQUEST_ID into t41_74
                    from t74_a in t41_74.DefaultIfEmpty()
                    where t41_amb.T_AMBU_REG_ID == ambId && t46.T_PAT_TYPE !="Hos" && t41_amb.T_DISCH_STATUS == null
                    group new { t46, t41_amb, t74_a } by new { t46.T_PAT_ID, t46.T_FIRST_LANG2_NAME, t46.T_FATHER_LANG2_NAME, t46.T_ALT_MOBILE_NO, t46.T_ADDRESS1 } into a
                         select new 
                    {
                        T_PAT_ID= a.Key.T_PAT_ID,
                        T_FIRST_LANG2_NAME = a.Key.T_FIRST_LANG2_NAME,
                        T_FATHER_LANG2_NAME= a.Key.T_FATHER_LANG2_NAME,
                      //  T_MOTHER_LANG2_NAME= t46.T_MOTHER_LANG2_NAME,
                        T_ALT_MOBILE_NO = a.Key.T_ALT_MOBILE_NO,
                        T_ADDRESS1 = a.Key.T_ADDRESS1,

                    }).AsEnumerable().Select((r, i) => new
                    {
                        RowNumber = i,
                        T_PAT_ID = r.T_PAT_ID,
                        T_FIRST_LANG2_NAME =  r.T_FIRST_LANG2_NAME +" "+ r.T_FATHER_LANG2_NAME ,
                        T_ADDRESS1= r.T_ADDRESS1,
                        T_ALT_MOBILE_NO= r.T_ALT_MOBILE_NO



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

        public string AmbulanceId(string userId)
        {
            var ambulance = (from t15 in obj.T74015
                join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                where t57.T_USER_ID == userId
                select new { t15.T_AMBU_REG_ID }).Single();
            var ambuId = ambulance.T_AMBU_REG_ID;
            return ambuId.ToString();
        }
        public string EmployeeId(string userId)
        {
            var ambulance = (from t15 in obj.T74015
                join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                where t57.T_USER_ID == userId
                select new { t57.T_EMP_ID }).Single();
            var ambuId = ambulance.T_EMP_ID;
            return ambuId.ToString();
        }

        public void Dispose()
        {
            obj.Dispose();

        }

        public void Save()
        {
            obj.SaveChanges();
        }
    }
}