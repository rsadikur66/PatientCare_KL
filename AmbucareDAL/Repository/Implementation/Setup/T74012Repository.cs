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
    public class T74012Repository: IT74012
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74012Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        //For Find Method
        public T74012 Find(string id)
        {
            T74012 objEmployee = new T74012();
            try
            {
                objEmployee = obj.T74012.Where(p => p.T_DETAIL_ID == Convert.ToInt32(id)).FirstOrDefault();
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

        //For Insert and Update Method
        public void Insert_T74012(T74012 _T74012)
        {
            var check = obj.T74012.Where(P => P.T_DETAIL_ID == _T74012.T_DETAIL_ID).FirstOrDefault();
            if (check == null)
            {
                obj.T74012.Add(_T74012);
                obj.SaveChanges();
            }
            else
            {
                var UpD = obj.T74012.Where(P => P.T_DETAIL_ID == _T74012.T_DETAIL_ID).FirstOrDefault();
                UpD.T_REQUEST_ID = _T74012.T_REQUEST_ID;
                UpD.T_SITE_NO = _T74012.T_SITE_NO;
                UpD.T_USER_ID = _T74012.T_USER_ID;
                obj.SaveChanges();
            }

        }

        //For Save change Method
        public void Save()
        {
            obj.SaveChanges();
        }

        //For Deleted Method

        public bool Deleted_T74012(int T_DETAIL_ID)
        {
            var T74012 = obj.T74012.Find(T_DETAIL_ID);
            obj.T74012.Remove(T74012);
            Save();
            return true;
        }

        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74012.OrderBy(a => a.T_DETAIL_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_DETAIL_ID = a.T_DETAIL_ID,
                    T_REQUEST_ID = a.T_REQUEST_ID,
                    T_SITE_NO = a.T_SITE_NO,
                    T_USER_ID = a.T_USER_ID,
                }).Where(x => x.RowNumber >= PageIndex * PageSize + 1 && x.RowNumber <(PageIndex + 1) * PageSize).ToList();
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
                    query = (from a in obj.T74012
                        where a.T_DETAIL_ID.ToString().Contains(searchValue) ||
                              a.T_REQUEST_ID.ToString().Contains(searchValue) ||
                              a.T_SITE_NO.Contains(searchValue) ||
                              a.T_USER_ID.ToString().Contains(searchValue)
                        select a).Count();
                }
                else
                {
                    query = (from a in obj.T74012 select a).Count();
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
                query = obj.T74012.Where(a => a.T_DETAIL_ID.ToString().Contains(searchValue) ||
                                              a.T_REQUEST_ID.ToString().Contains(searchValue) ||
                                              a.T_SITE_NO.Contains(searchValue) ||
                                              a.T_USER_ID.ToString().Contains(searchValue)).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_DETAIL_ID = a.T_DETAIL_ID,
                    T_REQUEST_ID = a.T_REQUEST_ID,
                    T_SITE_NO = a.T_SITE_NO,
                    T_USER_ID=a.T_USER_ID
                                              }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber <(PageIndex + 1) * PageSize);
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
            obj.Dispose();
        }
        
    }
}