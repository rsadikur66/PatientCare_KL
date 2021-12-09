using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{

    public class T74035Repository : IT74035
    {
        private AmbucareContainer obj = new AmbucareContainer();


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

        public void InsertUpdate(T74035 _T74035)
        {
            try
            {
                var check = obj.T74035.Where(P => P.T_DEPET_ID == _T74035.T_DEPET_ID).FirstOrDefault();
                if (check == null)
                {
                    obj.T74035.Add(_T74035);
                    obj.SaveChanges();
                }
                else
                {
                    var UpD = obj.T74035.Where(P => P.T_DEPET_ID == _T74035.T_DEPET_ID).FirstOrDefault();
                    UpD.T_LANG1_NAME = _T74035.T_LANG1_NAME;
                    UpD.T_LANG2_NAME = _T74035.T_LANG2_NAME;
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
        }

        public bool deleteData(int T_DEPET_ID)
        {
            try
            {
                var T74035 = obj.T74035.Find(T_DEPET_ID);
                obj.T74035.Remove(T74035);
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

        public void Dispose()
        {
            
        }

        public IEnumerable GetGridDataT74035(int PageIndex, int PageSize)
        {
            var query = obj.T74035.OrderBy(b=> b.T_DEPET_ID).ToList().Select((a,i) => new
            {
                RowNumber = i,
                T_DEPET_ID = a.T_DEPET_ID,
                T_LANG2_NAME = a.T_LANG2_NAME,
                T_LANG1_NAME = a.T_LANG1_NAME
            }).Where(x=> x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            return query;//need to edit with t74035
        }

        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            if (searchValue != "")
            {
                query = (from a in obj.T74035
                    where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                    select a).Count();
            }
            else
            {
                query = (from a in obj.T74035 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGrid_Data_Search(string searchValue, int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74035.Where(a => a.T_DEPET_ID.ToString().Contains(searchValue) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_DEPET_ID = a.T_DEPET_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME
                }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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
            return query;
        }
    }
}