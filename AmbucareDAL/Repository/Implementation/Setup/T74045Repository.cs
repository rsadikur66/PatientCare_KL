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
    public class T74045Repository: IT74045
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74045Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        //For Find Method Start
        public T74045 Find(string id)
        {
            T74045 objCurrency = new T74045();
            try
            {
                objCurrency = obj.T74045.Where(a => a.T_SUPPLIER_ID == Convert.ToInt32(id)).FirstOrDefault();
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
        public void Insert_T74045(T74045 _T74045)
        {
            try
            {
                var check = obj.T74045.Where(a => a.T_SUPPLIER_ID == _T74045.T_SUPPLIER_ID).FirstOrDefault();
                if (check == null)
                {
                    obj.T74045.Add(_T74045);
                }
                else
                {
                    var UpD = obj.T74045.Where(a => a.T_SUPPLIER_ID == _T74045.T_SUPPLIER_ID).FirstOrDefault();
                    UpD.T_SUPPLIER_ID = _T74045.T_SUPPLIER_ID;
                    UpD.T_LANG1_NAME = _T74045.T_LANG1_NAME;
                    UpD.T_LANG2_NAME = _T74045.T_LANG2_NAME;
                    UpD.T_PRES_ADDRESS = _T74045.T_PRES_ADDRESS;
                    UpD.T_PER_ADDRESS = _T74045.T_PER_ADDRESS;
                    UpD.T_PHONE_NUMBER = _T74045.T_PHONE_NUMBER;
                    UpD.T_MOBILE_NUMBER = _T74045.T_MOBILE_NUMBER;
                    UpD.T_EMAIL_ID = _T74045.T_EMAIL_ID;
                    UpD.T_UPD_USER = _T74045.T_UPD_USER;
                    UpD.T_UPD_DATE = _T74045.T_UPD_DATE;
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

        public bool Deleted_T74045(int T_SUPPLIER_ID)
        {
            try
            {
                var T74045 = obj.T74045.Find(T_SUPPLIER_ID);
                obj.T74045.Remove(T74045);
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

                query = (from Supplier in obj.T74045
                    select new
                    {

                        T_SUPPLIER_ID = Supplier.T_SUPPLIER_ID,
                        T_LANG2_NAME = Supplier.T_LANG2_NAME,
                        T_LANG1_NAME = Supplier.T_LANG1_NAME,
                        T_PRES_ADDRESS = Supplier.T_PRES_ADDRESS,
                        T_PER_ADDRESS = Supplier.T_PER_ADDRESS,
                        T_PHONE_NUMBER = Supplier.T_PHONE_NUMBER,
                        T_MOBILE_NUMBER = Supplier.T_MOBILE_NUMBER,
                        T_EMAIL_ID = Supplier.T_EMAIL_ID

                    }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                    r.T_SUPPLIER_ID,
                    r.T_LANG2_NAME,
                    r.T_LANG1_NAME,
                    r.T_PRES_ADDRESS,
                    r.T_PER_ADDRESS,
                    r.T_PHONE_NUMBER,
                    r.T_MOBILE_NUMBER,
                    r.T_EMAIL_ID
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
                    query = (from a in obj.T74045
                        where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_PRES_ADDRESS.ToUpper().Contains(searchValue.ToUpper())||
                              a.T_PER_ADDRESS.ToUpper().Contains(searchValue.ToUpper())||
                              a.T_PHONE_NUMBER.ToUpper().Contains(searchValue.ToUpper())||
                              a.T_MOBILE_NUMBER.ToUpper().Contains(searchValue.ToUpper())||
                              a.T_EMAIL_ID.ToUpper().Contains(searchValue.ToUpper())
                        select a).Count();
                }
                else
                {
                    query = (from a in obj.T74045 select a).Count();
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
                query = obj.T74045.Where(a => a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_PRES_ADDRESS.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_PER_ADDRESS.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_PHONE_NUMBER.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_MOBILE_NUMBER.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_EMAIL_ID.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_SUPPLIER_ID = a.T_SUPPLIER_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_PRES_ADDRESS = a.T_PRES_ADDRESS,
                    T_PER_ADDRESS = a.T_PER_ADDRESS,
                    T_PHONE_NUMBER = a.T_PHONE_NUMBER,
                    T_MOBILE_NUMBER = a.T_MOBILE_NUMBER,
                    T_EMAIL_ID = a.T_EMAIL_ID
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