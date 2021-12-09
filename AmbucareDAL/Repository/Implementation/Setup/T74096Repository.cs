using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;
using System.Data;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74096Repository : IT74096
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74096Repository(AmbucareContainer _Obj)
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
        public IEnumerable GetSite(string zone)
        {
            var query = obj.T02065.Where(s => s.T_ZONE_CODE == zone).Select(m => new { m.T_LANG2_NAME, m.T_SITE_CODE }).ToList();
            return query;
        }

        public T74096 Find(Int32 id)
        {
            T74096 objEmployee = new T74096();
            try
            {
                objEmployee = obj.T74096.Where(p => p.T_PER_ID == Convert.ToInt32(id)).FirstOrDefault();
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
            return objEmployee;
        }

        public IEnumerable GetAmbPatList(string T_SITE_CODE)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t44 in obj.T74044 where t44.T_AMBU_REG_ID != 0
                         select new
                         {
                             T_AMBU_REG_ID = t44.T_AMBU_REG_ID,
                             AmbulanceName = t44.T_LANG2_NAME
                         }).AsEnumerable().Select((r, i) => new
                         {
                             T_AMBU_REG_ID = r.T_AMBU_REG_ID,
                             AmbulanceName = r.AmbulanceName,
                             T_SITE_CODE = GetSiteCode(r.T_AMBU_REG_ID,T_SITE_CODE),
                             T_ACTIVE = GetActive(r.T_AMBU_REG_ID, T_SITE_CODE)
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

        public string GetSiteCode(int? T_AMBU_REG_ID, string T_SITE_CODE)
        {
            string query = obj.T74096.Where(x => x.T_AMBU_REG_ID == T_AMBU_REG_ID && x.T_SITE_CODE == T_SITE_CODE).Select(x => x.T_SITE_CODE).FirstOrDefault();
            return query;
        }


        public string GetActive( int? T_AMBU_REG_ID, string T_SITE_CODE)
        {
            string query = obj.T74096.Where(x => x.T_AMBU_REG_ID == T_AMBU_REG_ID && x.T_SITE_CODE == T_SITE_CODE).Select(x => x.T_ACTIVE.Trim()).FirstOrDefault();
            return query;
        }

        public void Insert_T74096(List<T74096> T74096)
        {

            foreach (var j in T74096)
            {
                var chk = obj.T74096.Where(p => p.T_AMBU_REG_ID == j.T_AMBU_REG_ID && p.T_SITE_CODE == j.T_SITE_CODE).FirstOrDefault();

                if (chk != null)
                {
                    // if (j.T_ACTIVE == "1")
                    // {
                    chk.T_ACTIVE = j.T_ACTIVE;
                    Save();
                    // }
                    // else
                    // {
                    //chk.T_ACTIVE = j.T_ACTIVE;
                    //    Save();
                    // }                       
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

        public void Dispose()
        {
            obj.Dispose();
        }
        public static DataTable fnConvertIEnumerableToDataTable(IEnumerable dataSource)
        {
            System.Reflection.PropertyInfo[] propInfo = null;
            DataTable dt = new DataTable();
            //DateTime objDT = DateTime.Now;

            foreach (object o in dataSource)
            {
                propInfo = o.GetType().GetProperties();

                for (int i = 0; i < propInfo.Length; i++)
                {
                    dt.Columns.Add(propInfo[i].Name);

                    if (propInfo[i].Name.Contains("Date"))
                        dt.Columns[i].DataType = typeof(DateTime);
                }
                break;
            }

            foreach (object tempObject in dataSource)
            {
                DataRow dr = dt.NewRow();

                for (int i = 0; i < propInfo.Length; i++)
                {
                    object t = tempObject.GetType().InvokeMember(propInfo[i].Name, BindingFlags.GetProperty, null, tempObject, new object[] { });
                    if (t != null)
                    {
                        if (t.GetType().Equals(typeof(DateTime)))
                            dr[i] = Convert.ToDateTime(t);
                        else
                            dr[i] = t.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


    }
}