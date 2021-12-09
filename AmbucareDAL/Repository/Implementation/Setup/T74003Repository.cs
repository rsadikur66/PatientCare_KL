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
    public class T74003Repository : IT74003
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74003Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        //For Find Method
        public T74003 Find(string id)
        {
            T74003 objEmployee = new T74003();
            try
            {
                objEmployee = obj.T74003.Where(p => p.T_ITEM_UM_ID == Convert.ToInt32(id)).FirstOrDefault();
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
        //Form ItemUM DropDown Method Start
        public IQueryable<T74008> GetTypeData
        {
            get { return obj.T74008; }
        }
        //Form ItemUM DropDown Method end
        //For Insert and Update Method
        public void Insert_T74003(T74003 _T74003)
        {
            try
            {
                var check = obj.T74003.Where(P => P.T_ITEM_UM_ID == _T74003.T_ITEM_UM_ID).FirstOrDefault();
                if (check == null)
                {
                    obj.T74003.Add(_T74003);
                    obj.SaveChanges();
                }
                else
                {
                    var UpD = obj.T74003.Where(P => P.T_ITEM_UM_ID == _T74003.T_ITEM_UM_ID).FirstOrDefault();
                    UpD.T_LANG1_NAME = _T74003.T_LANG1_NAME;
                    UpD.T_LANG2_NAME = _T74003.T_LANG2_NAME;
                    UpD.T_TYPE_ID = _T74003.T_TYPE_ID;
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

        public bool Deleted_T74003(int T_ITEM_UM_ID)
        {
            try
            {
                var T74003 = obj.T74003.Find(T_ITEM_UM_ID);
                obj.T74003.Remove(T74003);
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
                query = (from Ambu in obj.T74003
                         join Type in obj.T74008 on Ambu.T_TYPE_ID equals Type.T_TYPE_ID

                         select new
                         {

                             T_ITEM_UM_ID = Ambu.T_ITEM_UM_ID,
                             T_LANG2_NAME = Ambu.T_LANG2_NAME,
                             T_LANG1_NAME = Ambu.T_LANG1_NAME,
                             T_TYPE_ID = Ambu.T_TYPE_ID,
                             TypeName = Type.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             RowNumber = i,
                             r.T_ITEM_UM_ID,
                             r.T_LANG2_NAME,
                             r.T_LANG1_NAME,
                             r.T_TYPE_ID,
                             r.TypeName
                         }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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
                    query = (from a in obj.T74003
                             join Type in obj.T74008 on a.T_TYPE_ID equals Type.T_TYPE_ID
                             where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())||
                                   Type.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                             select a).Count();
                }
                else
                {
                    query = (from a in obj.T74003
                             join Type in obj.T74008 on a.T_TYPE_ID equals Type.T_TYPE_ID
                             select a).Count();
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
                query = (from a in obj.T74003
                    join Type in obj.T74008 on a.T_TYPE_ID equals Type.T_TYPE_ID
                    where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())||
                          Type.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                         select new
                    {

                        T_ITEM_UM_ID = a.T_ITEM_UM_ID,
                        T_LANG2_NAME = a.T_LANG2_NAME,
                        T_LANG1_NAME = a.T_LANG1_NAME,
                        T_TYPE_ID = a.T_TYPE_ID,
                        TypeName = Type.T_LANG2_NAME

                    }).AsEnumerable().Select((r, i) => new
                {
                    RowNumber = i,
                    r.T_ITEM_UM_ID,
                    r.T_LANG2_NAME,
                    r.T_LANG1_NAME,
                    r.T_TYPE_ID,
                    r.TypeName
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);



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