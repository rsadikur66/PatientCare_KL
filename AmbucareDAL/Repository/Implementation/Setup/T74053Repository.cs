using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74053Repository : IT74053
    {
        private AmbucareContainer obj = new AmbucareContainer();
        
        public T74053Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }
        public IQueryable<T74052> getZoneCode
        {
            get { return obj.T74052; }

        }

        //public IQueryable<T74053> getHospital
        //{

        //    get
        //    {
        //        return obj.T74053;
        //    }

        //}

        public IQueryable getHospital()
        {
            var re = from Hos in obj.T74053
                      join zone in obj.T74052 on Hos.T_ZONE_CODE equals zone.T_ZONE_CODE
                      where Hos.T_ZONE_CODE == zone.T_ZONE_CODE
                      select new 
                      {
                          T_SITE_NO = Hos.T_SITE_NO,
                          T_ZONE_CODE=Hos.T_ZONE_CODE,
                          ZONE = zone.T_LANG2_NAME,
                          T_SITE_CODE = Hos.T_SITE_CODE,
                          T_LANG1_NAME = Hos.T_LANG1_NAME,
                          T_LANG2_NAME = Hos.T_LANG2_NAME,
                          T_CONTACT_NAME = Hos.T_CONTACT_NAME,
                          T_CONTACT_MOBILE = Hos.T_CONTACT_MOBILE,
                          T_SITE_URL = Hos.T_SITE_URL,
                          T_LATITUDE = Hos.T_LATITUDE,
                          T_LONGITUDE = Hos.T_LONGITUDE,
                          T_IMPLEMENTATION_YEAR = Hos.T_IMPLEMENTATION_YEAR,
                          T_SITE_STATUS = Hos.T_SITE_STATUS,
                          T_PRIVATE_FLAG = Hos.T_PRIVATE_FLAG
                      };
            return re.ToList().AsQueryable();
        }

        public bool AddHospital(T74053 h)
        {
            try
            {

                if (h.T_SITE_NO == null)
                {
                    obj.T74053.Add(h);
                    obj.SaveChanges();
                }
                else
                {
                    obj.Entry(h).State = System.Data.Entity.EntityState.Modified;
                    obj.SaveChanges();
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
            return true;

        }
        public bool DeleteHosPital(int t_SITE_NO)
        {
            try
            {
                var del = obj.T74053.Find(t_SITE_NO);
                obj.T74053.Remove(del);
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
            return true;
        }
        //DeleteHosPital(int t_SITE_NO)
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}