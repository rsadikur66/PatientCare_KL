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
    public class T74020Repository : IT74020
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public bool saveEducation(T74020 t74020)
        {
            try
            {

                if (t74020.T_DOC_SPECI_ID == 0)
                {
                    obj.T74020.Add(t74020);
                    Save();
                }
                else
                {

                    obj.Entry(t74020).State = System.Data.Entity.EntityState.Modified;
                    Save();
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
            return true;
        }

        public IQueryable<T74020> getGridData
        {

            get { return obj.T74020; }

        }


        public bool deleteEducation(int d)
        {
            try
            {
                var del = obj.T74020.Find(d);
                obj.T74020.Remove(del);
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