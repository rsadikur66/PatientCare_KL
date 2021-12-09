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
    public class T74011Repository : IT74011
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74011Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        public IQueryable<T74011> All
        {
            get { return obj.T74011; }
        }

        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74011.OrderBy(a => a.T_ITEM_COLOR_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_ITEM_COLOR_ID = a.T_ITEM_COLOR_ID,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_LANG2_NAME = a.T_LANG2_NAME
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
                    query = (from a in obj.T74011
                        where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                        select a).Count();
                }
                else
                {
                    query = (from a in obj.T74011 select a).Count();
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
                query = obj.T74011.Where(a => a.T_ITEM_COLOR_ID.ToString().Contains(searchValue) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_ITEM_COLOR_ID = a.T_ITEM_COLOR_ID,
                    T_LANG1_NAME = a.T_LANG1_NAME,
                    T_LANG2_NAME = a.T_LANG2_NAME
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

        public T74011 Find(string id)
        {
            T74011 objEmployee = new T74011();
            try
            {
                objEmployee = obj.T74011.Where(p => p.T_ITEM_COLOR_ID == Convert.ToInt32(id)).FirstOrDefault();
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

        public bool InsertOrUpdate(T74011 ItemColor)
        {
            try
            {
                bool Status = false;
                if (ItemColor.T_ITEM_COLOR_ID == 0)
                {
                    // New entity
                    obj.T74011.Add(ItemColor);
                    Save();
                    Status = true;
                }
                else
                {
                    var data = obj.T74011.Where(p => p.T_ITEM_COLOR_ID == ItemColor.T_ITEM_COLOR_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_UPD_USER = ItemColor.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_LANG1_NAME = ItemColor.T_LANG1_NAME;
                        data.T_LANG2_NAME = ItemColor.T_LANG2_NAME;

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

        public bool Delete(int id)
        {
            try
            {
                var T74011= obj.T74011.Find(id);
                obj.T74011.Remove(T74011);
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
    }
}