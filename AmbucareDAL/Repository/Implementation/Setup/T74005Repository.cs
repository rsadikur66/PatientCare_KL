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
    public class T74005Repository : IT74005
    {
        private AmbucareContainer obj = new AmbucareContainer();
    

        public bool saveEmployee(T74005 _T74005)
        {
            try
            {
               
                if (_T74005.T_EMP_TYP_ID == 0)
                {
                    obj.T74005.Add(_T74005);
                    obj.SaveChanges();
                }
                else
                {
                      obj.Entry(_T74005).State = System.Data.Entity.EntityState.Modified;
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
        public IQueryable<T74005> getGridData
        {

            get { return obj.T74005; }

        }
        public bool deleteEmType(int T_EMP_TYP_ID)
        {
            try
            {
                var del = obj.T74005.Find(T_EMP_TYP_ID);
                obj.T74005.Remove(del);
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
       
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}