using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Setup;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74144Repository: IT74144
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private T74144DAL _t74144Dal = new T74144DAL();

        public T74144Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        public IQueryable<T74101> All
        {
            get { return obj.T74101; }
        }
        

        //For GridData Method
        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74101.OrderBy(a => a.T_CANCEL_REASON_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_CANCEL_REASON_ID = a.T_CANCEL_REASON_ID,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_CANCEL_TYPE_ID=a.T_CANCEL_TYPE_ID
                }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
                return query;
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

        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            try
            {
                if (searchValue != "")
                {
                    query = (from a in obj.T74101
                             where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                             select a).Count();
                }
                else
                {
                    query = (from a in obj.T74101 select a).Count();
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
            return query;
        }

        //For Grid Data Search Method
        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74101.Where(a => a.T_CANCEL_REASON_ID.ToString().Contains(searchValue) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_CANCEL_REASON_ID = a.T_CANCEL_REASON_ID,
                                                  T_LANG1_NAME = a.T_LANG1_NAME,
                                                  T_LANG2_NAME = a.T_LANG2_NAME,
                                                  T_CANCEL_TYPE_ID = a.T_CANCEL_TYPE_ID
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

        public T74101 Find(string id)
        {
            T74101 objEmployee = new T74101();
            try
            {
                objEmployee = obj.T74101.Where(p => p.T_CANCEL_REASON_ID == Convert.ToInt32(id)).FirstOrDefault();
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

        public bool InsertOrUpdate(T74101 _T74101)
        {
            try
            {
                bool Status = false;
                if (_T74101.T_CANCEL_REASON_ID == 0)
                {
                    _T74101.T_CANCEL_REASON_ID = Convert.ToByte(_t74144Dal.getCancelReasonId());
                    // New entity
                    obj.T74101.Add(_T74101);
                    Save();
                    Status = true;
                }
                else
                {

                    var data = obj.T74101.Where(p => p.T_CANCEL_REASON_ID == _T74101.T_CANCEL_REASON_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_UPD_USER = _T74101.T_ENTRY_USER;
                        data.T_LANG1_NAME = _T74101.T_LANG1_NAME;
                        data.T_LANG2_NAME = _T74101.T_LANG2_NAME;
                        data.T_CANCEL_TYPE_ID = _T74101.T_CANCEL_TYPE_ID;
                        Save();

                    }
                }
                return Status;
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

        public bool Delete(int id)
        {
            try
            {
                var T74144 = obj.T74101.Find(id);
                obj.T74101.Remove(T74144);
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

        public void Dispose()
        {
            try
            {
                obj.Dispose();
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
    }
}