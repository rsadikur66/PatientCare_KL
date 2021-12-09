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
    public class T74016Repository:IT74016
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74016Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        //For Find Method Start
        public T74016 Find(string id)
        {
            T74016 objCurrency = new T74016();
            try
            {
                objCurrency = obj.T74016.Where(a => a.T_CURRENCY_ID == Convert.ToInt32(id)).FirstOrDefault();
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
            return objCurrency;
        }

        //For Find Method end

        //For Insert and Update Method
        public void Insert_T74016(T74016 _T74016)
        {
            try
            {
                var check = obj.T74016.Where(a => a.T_CURRENCY_ID == _T74016.T_CURRENCY_ID).FirstOrDefault();
                if (check == null)
                {
                    obj.T74016.Add(_T74016);
                }
                else
                {
                    var UpD = obj.T74016.Where(a => a.T_CURRENCY_ID == _T74016.T_CURRENCY_ID).FirstOrDefault();
                    UpD.T_CURRENCY_ID = _T74016.T_CURRENCY_ID;
                    UpD.T_LANG1_NAME = _T74016.T_LANG1_NAME;
                    UpD.T_LANG2_NAME = _T74016.T_LANG2_NAME;
                    UpD.T_CURRENCY_NAME = _T74016.T_CURRENCY_NAME;
                    UpD.T_CONVERTION_VALUE_TK = _T74016.T_CONVERTION_VALUE_TK;
                    UpD.T_UPD_USER = _T74016.T_UPD_USER;
                    UpD.T_UPD_DATE = _T74016.T_UPD_DATE;
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
            Save();
        }

        //For Save change Method
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

        //For Deleted Method

        public bool Deleted_T74016(int T_CURRENCY_ID)
        {
            try
            {
                var T74016 = obj.T74016.Find(T_CURRENCY_ID);
                obj.T74016.Remove(T74016);
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

        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {

                query = (from Currency in obj.T74016
                    select new
                    {

                        T_CURRENCY_ID = Currency.T_CURRENCY_ID,
                        T_LANG2_NAME = Currency.T_LANG2_NAME,
                        T_LANG1_NAME = Currency.T_LANG1_NAME,
                        T_CURRENCY_NAME = Currency.T_CURRENCY_NAME,
                        T_CONVERTION_VALUE_TK = Currency.T_CONVERTION_VALUE_TK,

                    }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                    r.T_CURRENCY_ID,
                    r.T_LANG2_NAME,
                    r.T_LANG1_NAME,
                    r.T_CURRENCY_NAME,
                    r.T_CONVERTION_VALUE_TK
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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

        //For Grid Data Search Count Method
        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            try
            {
                if (searchValue != "")
                {
                    query = (from a in obj.T74016
                        where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_CURRENCY_NAME.ToUpper().Contains(searchValue.ToUpper())
                        select a).Count();
                }
                else
                {
                    query = (from a in obj.T74016 select a).Count();
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
                query = obj.T74016.Where(a => a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_CURRENCY_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_CURRENCY_ID = a.T_CURRENCY_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_CURRENCY_NAME = a.T_CURRENCY_NAME,
                    T_CONVERTION_VALUE_TK = a.T_CONVERTION_VALUE_TK
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
        //For Dispose Method
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