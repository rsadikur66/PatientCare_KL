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
using AmbucareDAL.Repository.Interface.Transaction;
using Newtonsoft.Json.Linq;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74130Repository : IT74130
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74130Repository(AmbucareContainer _Obj)
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

        public IEnumerable GetAmbPatList()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from ambu in obj.T74044
                         from t41 in obj.T74041.Where(x => x.T_AMBU_REG_ID == ambu.T_AMBU_REG_ID)
                         from patient in obj.T74046.Where(x => (x.T_PAT_ID == t41.T_PAT_ID))
                        
                         select new
                         {
                             
                             T_AMBU_REG_ID = ambu.T_AMBU_REG_ID,
                             AmbulanceName = ambu.T_LANG2_NAME,
                             T_PAT_ID = t41.T_PAT_ID,
                             T_FIRST_LANG2_NAME = patient.T_FIRST_LANG2_NAME,
                             T_FATHER_LANG2_NAME = patient.T_FATHER_LANG2_NAME,
                             T_GFATHER_LANG2_NAME = patient.T_GFATHER_LANG2_NAME,
                             T_FAMILY_LANG2_NAME = patient.T_FAMILY_LANG2_NAME,
                             T_REQUEST_DATE = t41.T_REQUEST_DATE,
                             T_CANCEL_STATUS = t41.T_CANCEL_STATUS,
                             T_DISCH_STATUS = t41.T_DISCH_STATUS
                         }).AsEnumerable().Select((r, i) => new
                         {
                             T_AMBU_REG_ID = r.T_AMBU_REG_ID,
                             AmbulanceName = r.AmbulanceName,
                             T_PAT_ID = r.T_PAT_ID,
                             T_REQUEST_DATE = r.T_REQUEST_DATE,
                             T_CANCEL_STATUS = r.T_CANCEL_STATUS,
                             T_DISCH_STATUS = r.T_DISCH_STATUS,
                             patientname = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME,
                         }).OrderBy(x => x.T_AMBU_REG_ID).ToList();
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

        public string GetSiteCode(int T_REQUEST_ID, string T_SITE_CODE)
        {
            string query = obj.T74096.Where(x => x.T_REQUEST_ID == T_REQUEST_ID && x.T_SITE_CODE== T_SITE_CODE).Select(x => x.T_SITE_CODE).FirstOrDefault();
            return query;
        }

        public string GetActive(int T_REQUEST_ID, string T_SITE_CODE)
        {
            string query = obj.T74096.Where(x => x.T_REQUEST_ID == T_REQUEST_ID && x.T_SITE_CODE== T_SITE_CODE).Select(x => x.T_ACTIVE.Trim()).FirstOrDefault();
            return query;
        }

        public void Dispose()
        {
            obj.Dispose();
        }

        public void Insert_T74130(List<T74096> T74096)
        {
           
                foreach (var j in T74096)
                {
                    var chk = obj.T74096.Where(p => p.T_REQUEST_ID == j.T_REQUEST_ID && p.T_SITE_CODE==j.T_SITE_CODE).FirstOrDefault();

                    if (chk !=null)
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
            //try
            //{
            //    obj.SaveChanges();
            //}

            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
            //        }
            //    }
            //}
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