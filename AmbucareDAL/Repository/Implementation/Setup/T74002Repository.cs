using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74002Repository: IT74002
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74002Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        

        public T74002 Find(Int32 id)
        {
            T74002 objEmployee = new T74002();
            try
            {
                objEmployee = obj.T74002.Where(p => p.T_ITEM_BRA_ID == Convert.ToInt32(id)).FirstOrDefault();
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
        public void AddItemBrand(T74002 _t74002)
        {
            var check = obj.T74002.Where(P => P.T_ITEM_BRA_ID == _t74002.T_ITEM_BRA_ID).FirstOrDefault();
            if (check == null)
            {
                obj.T74002.Add(_t74002);
                obj.SaveChanges();
            }
            else
            {
                var UpD = obj.T74002.Where(P => P.T_ITEM_BRA_ID == _t74002.T_ITEM_BRA_ID).FirstOrDefault();
                UpD.T_LANG1_NAME = _t74002.T_LANG1_NAME;
                UpD.T_LANG2_NAME = _t74002.T_LANG2_NAME;
                obj.SaveChanges();
            }

        }
        public bool Delete_T74002(int T_ITEM_BRA_ID)
        {
            var T74002 = obj.T74002.Find(T_ITEM_BRA_ID);
            obj.T74002.Remove(T74002);
            Save();
            return true;
        }
        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74002.OrderBy(a => a.T_ITEM_BRA_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_ITEM_BRA_ID = a.T_ITEM_BRA_ID,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME
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
                    query = (from a in obj.T74002
                             where a.T_ITEM_BRA_ID.ToString().Contains(searchValue.ToUpper()) || a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                             select a).Count();
                }
                else
                {
                    query = (from a in obj.T74002 select a).Count();
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
                query = obj.T74002.Where(a => a.T_ITEM_BRA_ID.ToString().Contains(searchValue) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_ITEM_BRA_ID = a.T_ITEM_BRA_ID,
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