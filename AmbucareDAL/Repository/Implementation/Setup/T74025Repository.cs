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
    public class T74025Repository : IT74025
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74025Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        //public T74025 Find(string id)
        //{
        //    T74025 objEmployee = new T74025();
        //    int fid = Convert.ToInt32(id);
        //    objEmployee = obj.T74025.Where(p => p.T_ITEM_BRA_ID == fid).FirstOrDefault();
        //    return objEmployee;
        //}

        public T74025 Find(Int32 id)
        {
            T74025 objEmployee = new T74025();
            try
            {
                objEmployee = obj.T74025.Where(p => p.T_USER_TYPE_ID == Convert.ToInt32(id)).FirstOrDefault();
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
        public void Get_T74025(T74025 _T74025)
        {
            var check = obj.T74025.Where(P => P.T_USER_TYPE_ID == _T74025.T_USER_TYPE_ID).FirstOrDefault();
            if (check == null)
            {
                obj.T74025.Add(_T74025);
                obj.SaveChanges();
            }
            else
            {
                var UpD = obj.T74025.Where(P => P.T_USER_TYPE_ID == _T74025.T_USER_TYPE_ID).FirstOrDefault();
                UpD.T_TYPE_NAME1 = _T74025.T_TYPE_NAME1;
                UpD.T_TYPE_NAME2 = _T74025.T_TYPE_NAME2;
                obj.SaveChanges();
            }

        }
        public bool Delete_T74025(int T_ITEM_BRA_ID)
        {
            var T74025 = obj.T74025.Find(T_ITEM_BRA_ID);
            obj.T74025.Remove(T74025);
            Save();
            return true;
        }
         public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74025.OrderBy(a => a.T_USER_TYPE_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_USER_TYPE_ID = a.T_USER_TYPE_ID,
                    T_TYPE_NAME1 = a.T_TYPE_NAME1,
                    T_TYPE_NAME2 = a.T_TYPE_NAME2
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
                    query = (from a in obj.T74025
                             where a.T_USER_TYPE_ID.ToString().Contains(searchValue.ToUpper()) || a.T_TYPE_NAME1.ToUpper().Contains(searchValue.ToUpper()) ||
                                   a.T_TYPE_NAME2.ToUpper().Contains(searchValue.ToUpper())
                             select a).Count();
                }
                else
                {
                    query = (from a in obj.T74025 select a).Count();
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
                query = obj.T74025.Where(a => a.T_USER_TYPE_ID.ToString().Contains(searchValue) ||
                                              a.T_TYPE_NAME1.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_TYPE_NAME2.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_USER_TYPE_ID = a.T_USER_TYPE_ID,
                                                  T_TYPE_NAME1 = a.T_TYPE_NAME1,
                                                  T_TYPE_NAME2 = a.T_TYPE_NAME2
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

        public IQueryable<T74025> GetGridData(string searchValue, int pageIndex, int pageSize)
        {
            var data = obj.T74025.Where(x => x.T_TYPE_NAME2.Contains(searchValue.ToUpper())).OrderBy(x => x.T_USER_TYPE_ID);
            return data;
        }
    }
}