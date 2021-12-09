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
    public class T74023Repository : IT74023
    {
        private AmbucareContainer obj = new AmbucareContainer();
        

        public bool saveEducation(T74023 t74023)
        {
            try
            {

                if (t74023.T_EDU_BOARD_ID == 0)
                {
                    obj.T74023.Add(t74023);
                    Save();
                }
                else
                {

                    obj.Entry(t74023).State = System.Data.Entity.EntityState.Modified;
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

        public IQueryable<T74023> getGridData
        {

            get { return obj.T74023; }

        }


        public bool deleteDepartment(int d)
        {
            try
            {
                var del = obj.T74023.Find(d);
                obj.T74023.Remove(del);
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