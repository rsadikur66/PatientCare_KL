using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Report;
using AmbucareDAL.Repository.Interface.Report;

namespace AmbucareDAL.Repository.Implementation.Report
{
    public class R74120Repository : IR74120Report
    {
        private AmbucareContainer obj = new AmbucareContainer();

        private readonly R74120DAL _r74120Dal = new R74120DAL();

        public R74120Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public DataTable GetAllData(int storeid)
        {
            var Data = _r74120Dal.GetAllData(storeid);
            return Data;

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
        //Store To DropDownlist Method Start
        public IEnumerable GetStoreListTo(int ambulanceId, int EmployeeId)
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
                             join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
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
                                 Color_Lang2 = t11.T_LANG2_NAME,
                                 t57.T_USER_ID
                             }).AsEnumerable().Select(k => new
                             {
                                 T_STORE_ID = k.T_STORE_ID,
                                 T_LANG2_NAME = k.T_STORE_ID == 1
                                 ? k.Central_Lang2
                                 //: k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                                 : k.T_USER_ID
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

        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize, int ambuRegId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if (ambuRegId == 1)
                {
                    query = (from t41 in obj.T74041
                             join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                             join t39 in obj.T74039 on t41.T_REQUEST_ID equals t39.T_REQUEST_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             orderby t41.T_REQUEST_ID descending
                             select new
                             {

                                 T_REQUEST_ID = t41.T_REQUEST_ID,
                                 T_PAT_ID = t46.T_PAT_ID,
                                 T_PAT_NO = t46.T_PAT_NO,
                                 T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                 T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                 T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                 T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                 T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                                 T_ADDRESS2 = t46.T_ADDRESS2,
                                 T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_REQUEST_ID,
                                 r.T_PAT_ID,
                                 r.T_PAT_NO,
                                 PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                                 r.T_ADDRESS2,
                                 r.T_ALT_MOBILE_NO
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
                }
                else
                {
                    query = (from t41 in obj.T74041
                             join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                             join t39 in obj.T74039 on t41.T_REQUEST_ID equals t39.T_REQUEST_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             where t44.T_STORE_ID == ambuRegId
                             orderby t41.T_REQUEST_ID descending
                             select new
                             {

                                 T_REQUEST_ID = t41.T_REQUEST_ID,
                                 T_PAT_ID = t46.T_PAT_ID,
                                 T_PAT_NO = t46.T_PAT_NO,
                                 T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                 T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                 T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                 T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                 T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                                 T_ADDRESS2 = t46.T_ADDRESS2,
                                 T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_REQUEST_ID,
                                 r.T_PAT_ID,
                                 r.T_PAT_NO,
                                 PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                                 r.T_ADDRESS2,
                                 r.T_ALT_MOBILE_NO
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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
            return query;
        }

        public int GetGridData_Search_Count(string searchValue, int ambuRegId)
        {
            int query = 0;
            try
            {
                if (ambuRegId != 1)
                {
                    if (searchValue != "")
                    {
                        query = (from t41 in obj.T74041
                                 join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                                 join t39 in obj.T74039 on t41.T_REQUEST_ID equals t39.T_REQUEST_ID
                                 join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                                 where ((t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                                  t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                                  t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                  t41.T_REQUEST_ID.ToString().Contains(searchValue))
                                  && t44.T_STORE_ID == ambuRegId
                                 select t46).Count();
                    }
                    else
                    {
                        query = (from t41 in obj.T74041
                                 join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                                 join t39 in obj.T74039 on t41.T_REQUEST_ID equals t39.T_REQUEST_ID
                                 join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                                 where t44.T_STORE_ID == ambuRegId
                                 select t46).Count();
                    }
                }
                else
                {
                    query = (from t41 in obj.T74041
                             join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                             join t39 in obj.T74039 on t41.T_REQUEST_ID equals t39.T_REQUEST_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             where (t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                              t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                              t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                              t41.T_REQUEST_ID.ToString().Contains(searchValue)

                             select t46).Count();
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

        //For Grid Data Search Method
        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, int ambuRegId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if (ambuRegId != 1)
                {
                    query = (from t41 in obj.T74041
                             join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                             join t39 in obj.T74039 on t41.T_REQUEST_ID equals t39.T_REQUEST_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             where ((t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                                   t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                                   t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                   t41.T_REQUEST_ID.ToString().Contains(searchValue))
                                   && t44.T_STORE_ID == ambuRegId
                             orderby t41.T_REQUEST_ID descending
                             select new
                             {

                                 T_REQUEST_ID = t41.T_REQUEST_ID,
                                 T_PAT_ID = t46.T_PAT_ID,
                                 T_PAT_NO = t46.T_PAT_NO,
                                 T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                 T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                 T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                 T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                 T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                                 T_ADDRESS2 = t46.T_ADDRESS2,
                                 T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_REQUEST_ID,
                                 r.T_PAT_ID,
                                 r.T_PAT_NO,
                                 PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                                 r.T_ADDRESS2,
                                 r.T_ALT_MOBILE_NO
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);

                }
                else
                {
                    query = (from t41 in obj.T74041
                             join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                             join t39 in obj.T74039 on t41.T_REQUEST_ID equals t39.T_REQUEST_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             where (t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                    || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                                   t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                                   t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                   t41.T_REQUEST_ID.ToString().Contains(searchValue)
                             orderby t41.T_REQUEST_ID descending
                             select new
                             {

                                 T_REQUEST_ID = t41.T_REQUEST_ID,
                                 T_PAT_ID = t46.T_PAT_ID,
                                 T_PAT_NO = t46.T_PAT_NO,
                                 T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                 T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                 T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                 T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                 T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                                 T_ADDRESS2 = t46.T_ADDRESS2,
                                 T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_REQUEST_ID,
                                 r.T_PAT_ID,
                                 r.T_PAT_NO,
                                 PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                                 r.T_ADDRESS2,
                                 r.T_ALT_MOBILE_NO
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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


        public IEnumerable GetGridDataBill(int PageIndex, int PageSize,int ambuRegId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if(ambuRegId != 1) { 
                query = (from t41 in obj.T74041
                         join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                         join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                         join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                         where t44.T_STORE_ID == ambuRegId
                         select new
                         {
                             T_REQUEST_ID = t41.T_REQUEST_ID,
                             T_PAT_ID = t46.T_PAT_ID,
                             T_PAT_NO = t46.T_PAT_NO,
                             T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                             T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                             T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                             T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                             T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                             T_ADDRESS2 = t46.T_ADDRESS2,
                             T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                         }).AsEnumerable().Select((r, i) => new
                         {
                             RowNumber = i,
                             r.T_REQUEST_ID,
                             r.T_PAT_ID,
                             r.T_PAT_NO,
                             PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                             r.T_ADDRESS2,
                             r.T_ALT_MOBILE_NO
                         }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
                }
                else
                {
                    {
                        query = (from t41 in obj.T74041
                                 join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                                 join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                                 join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                                
                                 select new
                                 {
                                     T_REQUEST_ID = t41.T_REQUEST_ID,
                                     T_PAT_ID = t46.T_PAT_ID,
                                     T_PAT_NO = t46.T_PAT_NO,
                                     T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                     T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                     T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                     T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                     T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                                     T_ADDRESS2 = t46.T_ADDRESS2,
                                     T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                                 }).AsEnumerable().Select((r, i) => new
                                 {
                                     RowNumber = i,
                                     r.T_REQUEST_ID,
                                     r.T_PAT_ID,
                                     r.T_PAT_NO,
                                     PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                                     r.T_ADDRESS2,
                                     r.T_ALT_MOBILE_NO
                                 }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
                    }
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
            return query;
        }

        public int GetGridData_Search_CountBill(string searchValue, int ambuRegId)
        {
            int query = 0;
            try
            {
                if(ambuRegId != 1)
                {
                    if (searchValue != "")
                    {
                        query = (from t41 in obj.T74041
                                 join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                                 join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                                 join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                                 where ((t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                                  t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                                  t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                  t41.T_REQUEST_ID.ToString().Contains(searchValue))
                                  && t44.T_STORE_ID == ambuRegId
                                 select t46).Count();
                    }
                    else
                    {
                        query = (from t41 in obj.T74041
                                 join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                                 join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                                 select t46).Count();
                    }
                }
                else
                {
                    if (searchValue != "")
                    {
                        query = (from t41 in obj.T74041
                                 join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                                 join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                                 join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                                 where (t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                   || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                                  t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                                  t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                  t41.T_REQUEST_ID.ToString().Contains(searchValue)
                                 select t46).Count();
                    }
                    else
                    {
                        query = (from t41 in obj.T74041
                                 join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                                 join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                                 select t46).Count();
                    }
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

        //For Grid Data Search Method
        public IEnumerable GetGrid_Data_SearchBill(string searchValue, Int32 PageIndex, Int32 PageSize, int ambuRegId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if(ambuRegId != 1)
                {
                    query = (from t41 in obj.T74041
                             join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                             join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             where ((t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                              t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                              t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                              t41.T_REQUEST_ID.ToString().Contains(searchValue))
                              && t44.T_STORE_ID== ambuRegId
                             select new
                             {

                                 T_REQUEST_ID = t41.T_REQUEST_ID,
                                 T_PAT_ID = t46.T_PAT_ID,
                                 T_PAT_NO = t46.T_PAT_NO,
                                 T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                 T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                 T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                 T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                 T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                                 T_ADDRESS2 = t46.T_ADDRESS2,
                                 T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_REQUEST_ID,
                                 r.T_PAT_ID,
                                 r.T_PAT_NO,
                                 PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                                 r.T_ADDRESS2,
                                 r.T_ALT_MOBILE_NO
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);

                }
                else
                {
                    query = (from t41 in obj.T74041
                             join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                             join t74 in obj.T74074 on t41.T_REQUEST_ID equals t74.T_REQUEST_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             where (t46.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_MOTHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                               || t46.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                              t46.T_ADDRESS2.ToUpper().Contains(searchValue.ToUpper()) ||
                              t46.T_ALT_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                              t41.T_REQUEST_ID.ToString().Contains(searchValue)
                             select new
                             {

                                 T_REQUEST_ID = t41.T_REQUEST_ID,
                                 T_PAT_ID = t46.T_PAT_ID,
                                 T_PAT_NO = t46.T_PAT_NO,
                                 T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                 T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                 T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                 T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                 T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,
                                 T_ADDRESS2 = t46.T_ADDRESS2,
                                 T_ALT_MOBILE_NO = t46.T_ALT_MOBILE_NO

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_REQUEST_ID,
                                 r.T_PAT_ID,
                                 r.T_PAT_NO,
                                 PATIENT_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                                 r.T_ADDRESS2,
                                 r.T_ALT_MOBILE_NO
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);

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


    }
}