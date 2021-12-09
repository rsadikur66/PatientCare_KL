using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74000Reposatory :IT74000
    {
        private AmbucareContainer obj = new AmbucareContainer();
        public T74000Reposatory(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IQueryable<T74000> All
        {
            get { return obj.T74000; }
        }

        //Form Item DropDown Method Start
        public IQueryable<T74066> GetFormData
        {
            get { return obj.T74066; }
        }
        //Form Item DropDown Method end

        //Form Label Data Method Start
        public IEnumerable getLabelTextData(string T_FORM_CODE,int lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //var internetConnectionCheck = CommonClass.CheckForInternetConnection();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"SELECT T_LABEL_NAME,T_LANG1_TEXT,T_LANG2_TEXT FROM t74000 WHERE T_FORM_CODE = '{T_FORM_CODE}'");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_LABEL_NAME = row["T_LABEL_NAME"].ToString(),
            //            T_LANG2_TEXT = lang ==1?row["T_LANG1_TEXT"].ToString() : row["T_LANG2_TEXT"].ToString()
            //        }).ToList();
            //}
            //else
            //{

                try
                {
                    query = (from t00 in obj.T74000
                        where t00.T_FORM_CODE == T_FORM_CODE
                        select new { t00.T_LABEL_NAME, T_LANG2_TEXT = lang == 1 ? t00.T_LANG1_TEXT : t00.T_LANG2_TEXT }).ToList();
                }
                catch (EntityCommandExecutionException e)
                {
                    //foreach (var validationErrors in dbEx.EntityValidationErrors)
                    //{
                    //    foreach (var validationError in validationErrors.ValidationErrors)
                    //    {
                    //        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    //    }
                    //}

                }
                
            //}
            
            return query;
        }
        
        //Form Label Data Method end
        public T74000 Find(string id)
        {
            T74000 objEmployee = new T74000();
            try
            {
                objEmployee = obj.T74000.Where(p => p.T_LABEL_ID == Convert.ToInt32(id)).FirstOrDefault();
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

        //For GridData Method
        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74000.OrderBy(a => a.T_LABEL_ID).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_LABEL_ID=a.T_LABEL_ID,
                    T_FORM_CODE = a.T_FORM_CODE,
                    T_LABEL_NAME = a.T_LABEL_NAME,
                    T_LANG1_TEXT = a.T_LANG1_TEXT,
                    T_LANG2_TEXT = a.T_LANG2_TEXT
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
                    query = (from a in obj.T74000
                        where a.T_LABEL_ID.ToString().Contains(searchValue) ||
                              a.T_FORM_CODE.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LABEL_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LANG1_TEXT.ToUpper().Contains(searchValue.ToUpper()) ||
                              a.T_LANG2_TEXT.ToUpper().Contains(searchValue.ToUpper())
                             select a).Count();
                }
                else
                {
                    query = (from a in obj.T74000 select a).Count();
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
                query = obj.T74000.Where(a => a.T_LABEL_ID.ToString().Contains(searchValue) ||
                                              a.T_FORM_CODE.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LABEL_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_TEXT.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG2_TEXT.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_LABEL_ID = a.T_LABEL_ID,
                    T_FORM_CODE = a.T_FORM_CODE,
                    T_LABEL_NAME = a.T_LABEL_NAME,
                    T_LANG1_TEXT = a.T_LANG1_TEXT,
                    T_LANG2_TEXT = a.T_LANG2_TEXT
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

        public bool InsertOrUpdate(T74000 labelField)
        {
            try
            {
                bool Status = false;
                if (labelField.T_LABEL_ID == 0)
                {
                    // New entity
                    obj.T74000.Add(labelField);
                    Save();
                    Status = true;
                }
                else
                {
                    var data = obj.T74000.Where(p => p.T_LABEL_ID == labelField.T_LABEL_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_UPD_USER = labelField.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_LANG1_TEXT = labelField.T_LANG1_TEXT;
                        data.T_LANG2_TEXT = labelField.T_LANG2_TEXT;
                        data.T_FORM_CODE = labelField.T_FORM_CODE;

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
                var T74000 = obj.T74000.Find(id);
                obj.T74000.Remove(T74000);
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