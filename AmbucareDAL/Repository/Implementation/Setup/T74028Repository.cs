using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74028Repository : IT74028
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74028Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        public T74028 Find(Int32 id)
        {
            T74028 objEmployee = new T74028();
            try
            {
                objEmployee = obj.T74028.Where(p => p.T_PUR_REQ_ID == Convert.ToInt32(id)).FirstOrDefault();
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
        public void Add_T74028(T74028 _t74028)
        {
            var check = obj.T74028.Where(P => P.T_PUR_REQ_ID == _t74028.T_PUR_REQ_ID).FirstOrDefault();
            if (check == null)
            {
                obj.T74028.Add(_t74028);
                obj.SaveChanges();
            }
            else
            {
                var UpD = obj.T74028.Where(P => P.T_PUR_REQ_ID == _t74028.T_PUR_REQ_ID).FirstOrDefault();
                UpD.T_COMPANY_ID = _t74028.T_COMPANY_ID;
                UpD.T_USER_ID = _t74028.T_USER_ID;
                UpD.T_PURCHASE_DATE = _t74028.T_PURCHASE_DATE;
                obj.SaveChanges();
            }

        }
        public bool Delete_T74028(int T_PUR_REQ_ID)
        {
            var T74028 = obj.T74028.Find(T_PUR_REQ_ID);
            obj.T74028.Remove(T74028);
            Save();
            return true;
        }
        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74028.OrderBy(a => a.T_PUR_REQ_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_PUR_REQ_ID = a.T_PUR_REQ_ID,
                    T_COMPANY_ID = a.T_COMPANY_ID,
                    T_USER_ID = a.T_USER_ID,
                    T_PURCHASE_DATE = a.T_PURCHASE_DATE

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
                    query = (from a in obj.T74028
                        where a.T_PUR_REQ_ID.ToString().Contains(searchValue.ToUpper()) || a.T_USER_ID.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_COMPANY_ID.ToString().Contains(searchValue.ToUpper())
                        select a).Count();
                }
                else
                {
                    query = (from a in obj.T74028 select a).Count();
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
                query = obj.T74028.Where(a => a.T_PUR_REQ_ID.ToString().Contains(searchValue) ||
                                              a.T_COMPANY_ID.ToString().Contains(searchValue.ToUpper()) ||
                                              a.T_USER_ID.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_PUR_REQ_ID = a.T_PUR_REQ_ID,
                    T_COMPANY_ID = a.T_COMPANY_ID,
                    T_USER_ID = a.T_USER_ID,
                    T_PURCHASE_DATE = a.T_PURCHASE_DATE
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