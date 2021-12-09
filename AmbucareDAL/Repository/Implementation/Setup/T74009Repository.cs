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
    public class T74009Repository : IT74009
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74009Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        //public T74009 Find(string id)
        //{
        //    T74009 objEmployee = new T74009();
        //    int fid = Convert.ToInt32(id);
        //    objEmployee = obj.T74009.Where(p => p.T_ITEM_BRA_ID == fid).FirstOrDefault();
        //    return objEmployee;
        //}

        public T74009 Find(string id)
        {
            T74009 objEmployee = new T74009();
            try
            {
                objEmployee = obj.T74009.Where(p => p.T_MEDIC_TYPE_ID == Convert.ToInt32(id)).FirstOrDefault();
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
        public void AddMedicalType(T74009 _T74009)
        {
            var check = obj.T74009.Where(P => P.T_MEDIC_TYPE_ID == _T74009.T_MEDIC_TYPE_ID).FirstOrDefault();
            if (check == null)
            {
                obj.T74009.Add(_T74009);
                obj.SaveChanges();
            }
            else
            {
                var UpD = obj.T74009.Where(P => P.T_MEDIC_TYPE_ID == _T74009.T_MEDIC_TYPE_ID).FirstOrDefault();
                UpD.T_LANG1_NAME = _T74009.T_LANG1_NAME;
                UpD.T_LANG2_NAME = _T74009.T_LANG2_NAME;
                UpD.T_COMPANY_ID = _T74009.T_COMPANY_ID;
                obj.SaveChanges();
            }

        }

        public bool Delete_T74009(int T_MEDIC_TYPE_ID)
        {
            var T74009 = obj.T74009.Find(T_MEDIC_TYPE_ID);
            obj.T74009.Remove(T74009);
            Save();
            return true;
        }

        public IQueryable<T74009> GetMedicalData
        {
            get { return obj.T74009.OrderBy(x => x.T_MEDIC_TYPE_ID); }
        }

        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74009.OrderBy(a => a.T_MEDIC_TYPE_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_MEDIC_TYPE_ID = a.T_MEDIC_TYPE_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_COMPANY_ID = a.T_COMPANY_ID
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
                    query = (from a in obj.T74009
                        where a.T_MEDIC_TYPE_ID.ToString().Contains(searchValue.ToUpper()) || a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                        select a).Count();
                }
                else
                {
                    query = (from a in obj.T74009 select a).Count();
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
                query = obj.T74009.Where(a => a.T_MEDIC_TYPE_ID.ToString().Contains(searchValue) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_MEDIC_TYPE_ID = a.T_MEDIC_TYPE_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_COMPANY_ID = a.T_COMPANY_ID
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

        public IQueryable<T74009> GetMedicalDataSearch(string searchValue, int pageIndex, int pageSize)
        {
            var data = obj.T74009.Where(x => x.T_LANG2_NAME.Contains(searchValue.ToUpper())).OrderBy(x => x.T_MEDIC_TYPE_ID);
            return data;
        }
    }
}