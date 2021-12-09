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
    public class T74056Repository:IT74056
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74056Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        public void Insert(T74056 _T74056)
        {
            if (_T74056.T_TRIAGE_SEQ == 0)
            {
                obj.T74056.Add(_T74056);
                Save();
            }
            else
            {
                var data = obj.T74056.Where(p => p.T_TRIAGE_SEQ == _T74056.T_TRIAGE_SEQ).FirstOrDefault();
                if (data != null)
                {
                    data.T_UPD_USER = _T74056.T_ENTRY_USER;
                    data.T_UPD_DATE = DateTime.Now.Date;
                    data.T_LANG1_NAME = _T74056.T_LANG1_NAME;
                    data.T_LANG2_NAME = _T74056.T_LANG2_NAME;

                    Save();
                }
            }
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
        public bool Delete(int id)
        {
            T74056 data = new T74056();
            data = obj.T74056.Where(a => a.T_TRIAGE_SEQ == id).FirstOrDefault();
            obj.T74056.Remove(data);
            Save();
            return true;
        }

        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            var query = obj.T74056.OrderBy(a => a.T_TRIAGE_SEQ).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_TRIAGE_SEQ = a.T_TRIAGE_SEQ,
                T_LANG2_NAME = a.T_LANG2_NAME,
                T_LANG1_NAME = a.T_LANG1_NAME
            }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            return query;
        }

        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            if (searchValue != "")
            {
                query = (from a in obj.T74056
                    where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                    select a).Count();
            }
            else
            {
                query = (from a in obj.T74056 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74056.Where(a => a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) || a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_FORM_CODE = a.T_TRIAGE_SEQ,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME
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
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}