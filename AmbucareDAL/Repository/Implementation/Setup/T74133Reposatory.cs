using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74133Reposatory : IT74133
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74133Reposatory(AmbucareContainer _Obj)
        {

            obj = _Obj;
        }

        public IEnumerable GetZone()
        {
            var query = obj.T02064.OrderBy(a => a.T_ZONE_CODE).ToList().Select((a, i) => new
            {

                T_ZONE_CODE = a.T_ZONE_CODE,
                T_LANG2_NAME = a.T_LANG2_NAME,

            }).ToList();
            return query;
        }

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
                                 : k.T_AMBU_REG_ID + "-" + k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
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
                                 T_AMBU_REG_ID = k.T_AMBU_REG_ID,
                                 T_LANG2_NAME = k.T_STORE_ID == 1
                                 ? k.Central_Lang2
                                 : k.T_AMBU_REG_ID +"-"+ k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
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

        public IEnumerable GetGridAllData(string ambuId, string zone)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t65 in obj.T02065
                         where t65.T_ZONE_CODE == zone
                         select new
                         {
                             T_REGION_CODE = t65.T_ZONE_CODE,
                             T_SITE_CODE = t65.T_SITE_CODE,
                             T_LANG2_NAME = t65.T_LANG2_NAME
                         }).AsEnumerable().Select((r, i) => new
                         {
                             T_REGION_CODE = r.T_REGION_CODE,
                             T_SITE_CODE = r.T_SITE_CODE,
                             T_LANG2_NAME = r.T_LANG2_NAME,
                             T_ACTIVE = GetActive(Convert.ToInt32(ambuId), r.T_SITE_CODE)
                         }).ToList();
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


        public string GetActive(int? T_AMBU_REG_ID, string T_SITE_CODE)
        {
            string query = obj.T74096.Where(x => x.T_AMBU_REG_ID == T_AMBU_REG_ID && x.T_SITE_CODE == T_SITE_CODE).Select(x => x.T_ACTIVE.Trim()).FirstOrDefault();
            return query==null?"3": query;
        }

        public void Insert_T74133(List<T74096> T74096)
        {

            foreach (var j in T74096)
            {
                var chk = obj.T74096.Where(p => p.T_AMBU_REG_ID == j.T_AMBU_REG_ID && p.T_SITE_CODE == j.T_SITE_CODE).FirstOrDefault();

                if (chk != null)
                {
                    chk.T_ACTIVE = j.T_ACTIVE;
                    Save();

                }
                else
                {
                    if (j.T_ACTIVE == "1")
                    {
                        int t96 = obj.T74096.Count() > 0 ? obj.T74096.Max(a => a.T_PER_ID) : 0;
                        j.T_PER_ID = t96 != 0 ? t96 + 1 : 1;
                        obj.T74096.Add(j);
                        Save();
                    }
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
    }
}