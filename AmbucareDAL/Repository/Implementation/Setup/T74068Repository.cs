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
    public class T74068Repository : IT74068
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74068Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        //For Find Method
        public T74068 Find(string id)
        {
            T74068 objEmployee = new T74068();
            try
            {
                objEmployee = obj.T74068.Where(p => p.T_MSG_CODE == Convert.ToInt32(id)).FirstOrDefault();
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
        public void Insert_T74068(T74068 _T74068)
        {
            try
            {
                var check = obj.T74068.Where(P => P.T_MSG_CODE == _T74068.T_MSG_CODE).FirstOrDefault();
                if (check == null)
                {
                    obj.T74068.Add(_T74068);
                    obj.SaveChanges();
                }
                else
                {
                    var UpD = obj.T74068.Where(P => P.T_MSG_CODE == _T74068.T_MSG_CODE).FirstOrDefault();
                    UpD.T_LANG2_MSG = _T74068.T_LANG2_MSG;
                    UpD.T_LANG1_MSG = _T74068.T_LANG1_MSG;
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

        public bool Deleted_T74068(int T_MSG_CODE)
        {
            try
            {
                var T74068 = obj.T74068.Find(T_MSG_CODE);
                obj.T74068.Remove(T74068);
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
                query = obj.T74068.OrderBy(a => a.T_MSG_CODE).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_MSG_CODE = a.T_MSG_CODE,
                    T_LANG2_MSG = a.T_LANG2_MSG,
                    T_LANG1_MSG = a.T_LANG1_MSG
                }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber <(PageIndex + 1) * PageSize).ToList();
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
                    query = (from a in obj.T74068
                             where a.T_LANG2_MSG.ToUpper().Contains(searchValue.ToUpper()) ||
                                   a.T_LANG1_MSG.ToUpper().Contains(searchValue.ToUpper())
                             select a).Count();
                }
                else
                {
                    query = (from a in obj.T74068 select a).Count();
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
                query = obj.T74068.Where(a => a.T_MSG_CODE.ToString().Contains(searchValue) ||
                                              a.T_LANG2_MSG.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_MSG.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_MSG_CODE = a.T_MSG_CODE,
                                                  T_LANG2_MSG = a.T_LANG2_MSG,
                                                  T_LANG1_MSG = a.T_LANG1_MSG
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