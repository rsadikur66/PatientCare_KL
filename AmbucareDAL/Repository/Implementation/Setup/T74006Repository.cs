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
    public class T74006Repository : IT74006
    {
        private AmbucareContainer obj = new AmbucareContainer();
        public bool saveEmpDes(T74006 t74006)
        {
            try
            {
              
                if (t74006.T_EMP_DESI_ID == 0)
                {
                    obj.T74006.Add(t74006);
                    Save();
                }
                else
                {
                   
                      obj.Entry(t74006).State = System.Data.Entity.EntityState.Modified;
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
        public IQueryable<T74006> getGridData
        {

            get { return obj.T74006; }

        }

        public bool DeleteEmpDes(int d)
        {
            try
            {
                var del = obj.T74006.Find(d);
                obj.T74006.Remove(del);
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

        //bool DeleteEmpDes(int d)
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