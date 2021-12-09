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
    public class T74054Repository : IT74054
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74054Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }


        public T74054 Find(Int32 id)
        {
            T74054 objEmployee = new T74054();
            try
            {
                objEmployee = obj.T74054.Where(p => p.T_COMPANY_ID == Convert.ToInt32(id)).FirstOrDefault();
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
        public void AddItemBrand(T74054 _t74054)
        {
            var check = obj.T74054.Where(P => P.T_COMPANY_ID == _t74054.T_COMPANY_ID).FirstOrDefault();
            if (check == null)
            {
                obj.T74054.Add(_t74054);
                obj.SaveChanges();
            }
            else
            {
                var UpD = obj.T74054.Where(P => P.T_COMPANY_ID == _t74054.T_COMPANY_ID).FirstOrDefault();
                UpD.T_LANG1_NAME = _t74054.T_LANG1_NAME;
                UpD.T_LANG2_NAME = _t74054.T_LANG2_NAME;
                UpD.T_LANG1_ADRS_NAME = _t74054.T_LANG1_ADRS_NAME;
                UpD.T_LANG2_ADRS_NAME = _t74054.T_LANG2_ADRS_NAME;
                UpD.T_EMAIL = _t74054.T_EMAIL;
                UpD.T_PHONE = _t74054.T_PHONE;
                UpD.T_WEB_URL = _t74054.T_WEB_URL;
                obj.SaveChanges();
            }

        }
        public bool Delete_T74054(int T_COMPANY_ID)
        {
            var T74054 = obj.T74054.Find(T_COMPANY_ID);
            obj.T74054.Remove(T74054);
            Save();
            return true;
        }
        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74054.OrderBy(a => a.T_COMPANY_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_COMPANY_ID = a.T_COMPANY_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_LANG1_ADRS_NAME = a.T_LANG1_ADRS_NAME,
                    T_LANG2_ADRS_NAME = a.T_LANG2_ADRS_NAME,
                    T_EMAIL = a.T_EMAIL,
                    T_PHONE = a.T_PHONE,
                    T_WEB_URL = a.T_WEB_URL
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
                    query = (from a in obj.T74054
                             where a.T_COMPANY_ID.ToString().Contains(searchValue.ToUpper()) || a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                             select a).Count();
                }
                else
                {
                    query = (from a in obj.T74054 select a).Count();
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
                query = obj.T74054.Where(a => a.T_COMPANY_ID.ToString().Contains(searchValue) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())||
                                              a.T_LANG1_ADRS_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG2_ADRS_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_EMAIL.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_PHONE.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_WEB_URL.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_COMPANY_ID = a.T_COMPANY_ID,
                                                  T_LANG2_NAME = a.T_LANG2_NAME,
                                                  T_LANG1_NAME = a.T_LANG1_NAME,
                                                  T_LANG1_ADRS_NAME = a.T_LANG1_ADRS_NAME,
                                                  T_LANG2_ADRS_NAME = a.T_LANG2_ADRS_NAME,
                                                  T_EMAIL = a.T_EMAIL,
                                                  T_PHONE = a.T_PHONE,
                                                  T_WEB_URL = a.T_WEB_URL
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