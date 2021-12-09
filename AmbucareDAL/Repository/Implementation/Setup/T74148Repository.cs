using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Setup;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74148Repository:IT74148
    {

        private AmbucareContainer obj = new AmbucareContainer();
        private CommonDAL commonDal = new CommonDAL();

        public T74148Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        public IQueryable<T02040> All
        {
            get { return obj.T02040; }
        }


        //For GridData Method
        public IEnumerable getGridData(int PageIndex, int PageSize)
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
                    T_CANCEL_TYPE_ID = a.T_CANCEL_TYPE_ID
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

        public bool InsertOrUpdate(T02040 _T02040, string userid)
        {
            try
            {
                bool Status = false;
                if (_T02040.T_SPCLTY_ID == 0)
                {
                    int spId = obj.T02040.Max(x => x.T_SPCLTY_ID) + 1;
                    // _T02040.T_SPCLTY_ID = Convert.ToByte(_t74144Dal.getCancelReasonId());
                    // New entity
                    //_T02040.T_ENTRY_DATE = commonDal.dateTime();
                    // _T02040.T_ENTRY_USER = userid;
                     _T02040.T_SPCLTY_ID = (Int16)(spId); 
                    obj.T02040.Add(_T02040);
                    Save();
                    Status = true;
                }
                else
                {

                    var data = obj.T02040.Where(p => p.T_SPCLTY_ID == _T02040.T_SPCLTY_ID).FirstOrDefault();
                    if (data != null)
                    {
                        //data.T_UPD_DATE = commonDal.dateTime();
                        //data.T_UPD_USER = userid;
                        data.T_SPCLTY_CODE = _T02040.T_SPCLTY_CODE;
                        data.T_LANG2_NAME = _T02040.T_LANG2_NAME;
                        data.T_LANG1_NAME = _T02040.T_LANG1_NAME;
                        data.T_MAIN_SPCLTY = _T02040.T_MAIN_SPCLTY;
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


        public DataTable getGridAllData()
        {
            return commonDal.Query($"SELECT T_SPCLTY_ID,T_SPCLTY_CODE,T_LANG1_NAME,T_LANG2_NAME,T_MAIN_SPCLTY FROM T02040");
        }
    }
}