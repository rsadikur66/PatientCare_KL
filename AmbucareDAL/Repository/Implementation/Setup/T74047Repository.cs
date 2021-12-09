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
    public class T74047Repository: IT74047
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74047Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
     
        public bool InsertOrUpdate(T74047 AmbuType)
        {
            try
            {
                bool Status = false;
                if (AmbuType.T_AMBU_TYPE_ID == 0)
                {
                    // New entity
                    obj.T74047.Add(AmbuType);
                    Save();
                    Status = true;
                }
                else
                {
                    var data = obj.T74047.Where(p => p.T_AMBU_TYPE_ID == AmbuType.T_AMBU_TYPE_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_UPD_USER = AmbuType.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_LANG1_NAME = AmbuType.T_LANG1_NAME;
                        data.T_LANG2_NAME = AmbuType.T_LANG2_NAME;

                        Save();
                    }
                }
                return Status;
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
        public void Save()
        {
            obj.SaveChanges();
        }
        public bool Delete(int id)
        {
            T74047 data = new T74047();
            data = obj.T74047.Where(a => a.T_AMBU_TYPE_ID == id).FirstOrDefault();
            obj.T74047.Remove(data);
            Save();
            return true;
        }

        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74047.OrderBy(a => a.T_AMBU_TYPE_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_AMBU_TYPE_ID = a.T_AMBU_TYPE_ID,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_LANG2_NAME = a.T_LANG2_NAME
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
        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            var query = obj.T74047.OrderBy(a => a.T_AMBU_TYPE_ID).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_AMBU_TYPE_ID = a.T_AMBU_TYPE_ID,
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
                query = (from a in obj.T74047
                    where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                    select a).Count();
            }
            else
            {
                query = (from a in obj.T74047 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74047.Where(a => a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) || a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_AMBU_TYPE_ID = a.T_AMBU_TYPE_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME
                }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber <= (PageIndex + 1) * PageSize);
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