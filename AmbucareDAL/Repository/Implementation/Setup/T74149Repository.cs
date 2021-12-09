using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74149Repository:IT74149
    {
        CommonDAL _commonDal = new CommonDAL();
        private AmbucareContainer obj = new AmbucareContainer();
        public T74149Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
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

        public void insertStatus(T06301 _t06301)
        {
            try
            {
                var kkk = _t06301.T_ICD10_MAIN_CODE + '.' + _t06301.T_ICD10_SUB_CODE;
                _t06301.T_ICD10_CODE = kkk;
                //_t06301.T_ENTRY_USER = "0001";
                var check = obj.T06301.Where(P => P.T_ICD10_CODE == _t06301.T_ICD10_CODE).FirstOrDefault();
                if (check == null)
                {
                    obj.T06301.Add(_t06301);
                    obj.SaveChanges();
                }
                else
                {
                    var UpD = obj.T06301.Where(P => P.T_ICD10_CODE == _t06301.T_ICD10_CODE).FirstOrDefault();
                    UpD.T_ICD10_LONG_DESC1 = _t06301.T_ICD10_LONG_DESC1;
                    UpD.T_ICD10_LONG_DESC2 = _t06301.T_ICD10_LONG_DESC2;
                    UpD.T_ICD10_SHORT_DESC1 = _t06301.T_ICD10_SHORT_DESC1;
                    UpD.T_ICD10_SHORT_DESC2 = _t06301.T_ICD10_SHORT_DESC2;
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

        public DataTable getGridAllData()
        {
            return _commonDal.Query($"SELECT * FROM T06301");
        }

        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            var query = obj.T06301.OrderBy(a => a.T_ICD10_MAIN_CODE).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_ICD10_MAIN_CODE = a.T_ICD10_MAIN_CODE,
                T_ICD10_SUB_CODE = a.T_ICD10_SUB_CODE,
                T_ICD10_LONG_DESC1 = a.T_ICD10_LONG_DESC1,
                T_ICD10_LONG_DESC2 = a.T_ICD10_LONG_DESC2,
                T_ICD10_SHORT_DESC2 = a.T_ICD10_SHORT_DESC2,
                T_ICD10_SHORT_DESC1 = a.T_ICD10_SHORT_DESC1,
                T_ICD10_TYPE = a.T_ICD10_TYPE,
                T_ICD10_CODE = a.T_ICD10_CODE,

            }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            return query;
        }

        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            if (searchValue != "")
            {
                query = (from a in obj.T06301
                         where a.T_ICD10_LONG_DESC2.ToUpper().Contains(searchValue.ToUpper()) ||
                          a.T_ICD10_LONG_DESC2.ToUpper().Contains(searchValue.ToUpper())
                    select a).Count();
            }
            else
            {
                query = (from a in obj.T06301 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T06301.Where(a => a.T_ICD10_MAIN_CODE.ToUpper().Contains(searchValue.ToUpper()) || a.T_ICD10_SHORT_DESC2.ToUpper().Contains(searchValue.ToUpper())|| a.T_ICD10_SHORT_DESC1.ToUpper().Contains(searchValue.ToUpper()) || a.T_ICD10_LONG_DESC1.ToUpper().Contains(searchValue.ToUpper())||  a.T_ICD10_LONG_DESC2.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_ICD10_MAIN_CODE = a.T_ICD10_MAIN_CODE,
                    T_ICD10_SUB_CODE = a.T_ICD10_SUB_CODE,
                    T_ICD10_LONG_DESC1 = a.T_ICD10_LONG_DESC1,
                    T_ICD10_LONG_DESC2 = a.T_ICD10_LONG_DESC2,
                    T_ICD10_SHORT_DESC2 = a.T_ICD10_SHORT_DESC2,
                    T_ICD10_SHORT_DESC1 = a.T_ICD10_SHORT_DESC1,
                    T_ICD10_TYPE = a.T_ICD10_TYPE,
                    T_ICD10_CODE = a.T_ICD10_CODE
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
    }
}