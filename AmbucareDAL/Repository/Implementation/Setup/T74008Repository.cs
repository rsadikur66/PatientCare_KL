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
    public class T74008Repository : IT74008
    {
        private AmbucareContainer obj = new AmbucareContainer();
        
        public bool saveProType(T74008 t74008)
        {
            try
            {

                if (t74008.T_TYPE_ID == 0)
                {
                    obj.T74008.Add(t74008);
                    Save();
                }
                else
                {

                    obj.Entry(t74008).State = System.Data.Entity.EntityState.Modified;
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
            return true;

        }

        public IQueryable<T74008> getGridData
        {

            get { return obj.T74008; }

        }
        public bool deleteProd(int d)
        {
            try
            {
                var del = obj.T74008.Find(d);
                obj.T74008.Remove(del);
                Save();
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