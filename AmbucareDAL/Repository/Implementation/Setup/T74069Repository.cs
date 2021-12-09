using AmbucareDAL.Repository.Interface.Setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74069Repository : IT74069
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public bool SaveBloodGroup(T74069 t74069)
        {
            try
            {

                if (t74069.T_BLD_GROUP_ID == 0)
                {
                    obj.T74069.Add(t74069);
                    Save();
                }
                else
                {
                    obj.Entry(t74069).State = System.Data.Entity.EntityState.Modified;
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
        public IQueryable<T74069> getGridData
        {

            get { return obj.T74069; }

        }

        public bool Delete(Int32 blood)
        {
            try
            {
                var T74069 = obj.T74069.Find(blood);
                obj.T74069.Remove(T74069);
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