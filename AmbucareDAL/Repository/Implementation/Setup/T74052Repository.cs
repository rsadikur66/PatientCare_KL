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
    public class T74052Repository:IT74052
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74052Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }
        public IQueryable<T74052> All
        {
            get { return obj.T74052; }
        }
        public T74052 Find(string id)
        {
            T74052 objEmployee = new T74052();
            int fid = Convert.ToInt32(id);
            objEmployee = obj.T74052.Where(p => p.T_SL_NO == fid).FirstOrDefault();
            return objEmployee;
        }
        public bool AddZone(T74052 Z)
        {
            try
            {
                if (Z.T_ZONE_CODE != null)
                {
                    obj.T74052.Add(Z);
                    obj.SaveChanges();
                }
                else
                {
                    obj.Entry(Z).State = System.Data.Entity.EntityState.Modified;
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
       
        public IQueryable<T74052> GetZoneData
        {
            get { return obj.T74052; }
        }
        public bool DeleteZone(string t_ZONE_CODE)
        {
            try
            {
                var del = obj.T74052.Find(t_ZONE_CODE);
                obj.T74052.Remove(del);
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
        //DeleteZone(int t_ZONE_CODE);

        public void Save()
        {
            obj.SaveChanges();
        }
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}