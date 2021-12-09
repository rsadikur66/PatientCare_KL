using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Repository.Interface.Setup;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74055Repository:IT74055
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74055Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        
        public void Insert(T74055 _T74055)
        {
            if (_T74055.T_CH_COMP == 0)
            {
                obj.T74055.Add(_T74055);
                Save();
            }
            else
            {
                var data = obj.T74055.Where(p => p.T_CH_COMP == _T74055.T_CH_COMP).FirstOrDefault();
                if (data != null)
                {
                    data.T_UPD_USER = _T74055.T_ENTRY_USER;
                    data.T_UPD_DATE = DateTime.Now.Date;
                    data.T_LANG1_NAME = _T74055.T_LANG1_NAME;
                    data.T_LANG2_NAME = _T74055.T_LANG2_NAME;
                    
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
            T74055 data = new T74055();
            data = obj.T74055.Where(a => a.T_CH_COMP == id).FirstOrDefault();
            obj.T74055.Remove(data);
            Save();
            return true;
        }

        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            var query = obj.T74055.OrderBy(a => a.T_CH_COMP).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_CH_COMP = a.T_CH_COMP,
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
                query = (from a in obj.T74055
                    where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                    select a).Count();
            }
            else
            {
                query = (from a in obj.T74055 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74055.Where(a => a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())|| a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_CH_COMP = a.T_CH_COMP,
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