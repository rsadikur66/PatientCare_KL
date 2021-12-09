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
    public class T74073Repository:IT74073
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74073Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IQueryable<T74073> All
        {
            get { return obj.T74073; }
        }


        //Form Item DropDown Method Start
        public IQueryable<T74072> GetCostType
        {
            get { return obj.T74072; }
        }
        //Form Item DropDown Method end

        //Form Label Data Method Start
        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Ambu in obj.T74073
                    join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID

                         select new
                    {

                        T_COST_TYPE_DTL_ID = Ambu.T_COST_TYPE_DTL_ID,
                        T_LANG1_NAME = Ambu.T_LANG1_NAME,
                        T_LANG2_NAME = Ambu.T_LANG2_NAME,
                        T_COST_TYPE_ID = Ambu.T_COST_TYPE_ID,
                        CostType = CostType.T_LANG2_NAME
                       
                    }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                    r.T_COST_TYPE_DTL_ID,
                    r.T_LANG1_NAME,
                    r.T_LANG2_NAME,
                    r.T_COST_TYPE_ID,
                    r.CostType
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
                    query = (from Ambu in obj.T74073
                        join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID
                             where
                             Ambu.T_COST_TYPE_DTL_ID.ToString().Contains(searchValue) ||
                             Ambu.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                             Ambu.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                             CostType.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                             
                        select Ambu).Count();
                }
                else
                {
                    query = (from Ambu in obj.T74073
                        join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID
                             select Ambu).Count();
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
                query = (from Ambu in obj.T74073
                    join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID
                    where 
                          Ambu.T_COST_TYPE_DTL_ID.ToString().Contains(searchValue)||
                          Ambu.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          Ambu.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          CostType.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) 
                    select new
                    {
                        T_COST_TYPE_DTL_ID=Ambu.T_COST_TYPE_DTL_ID,
                        T_COST_TYPE_ID = Ambu.T_COST_TYPE_ID,
                        T_LANG1_NAME = Ambu.T_LANG1_NAME,
                        T_LANG2_NAME = Ambu.T_LANG2_NAME,
                        CostType = CostType.T_LANG2_NAME
                        
                    }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                   r.T_COST_TYPE_DTL_ID,
                    r.T_COST_TYPE_ID,
                    r.T_LANG1_NAME,
                    r.T_LANG2_NAME,
                    r.CostType
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
        
        public bool InsertOrUpdate(T74073 _T74073)
        {
            try
            {
                bool Status = false;
                if (_T74073.T_COST_TYPE_DTL_ID == 0)
                {
                    // New entity
                    obj.T74073.Add(_T74073);
                    Save();
                    Status = true;
                }
                else
                {

                    var data = obj.T74073.Where(p => p.T_COST_TYPE_DTL_ID == _T74073.T_COST_TYPE_DTL_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_COST_TYPE_DTL_ID = _T74073.T_COST_TYPE_DTL_ID;
                        data.T_UPD_USER = _T74073.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_COST_TYPE_ID = _T74073.T_COST_TYPE_ID;
                        data.T_LANG1_NAME = _T74073.T_LANG1_NAME;
                        data.T_LANG2_NAME = _T74073.T_LANG2_NAME;
                        data.T_ACTIVE = _T74073.T_ACTIVE;
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

        public bool InsertOrUpdate72(T74072 _T74072)
        {
            try
            {
                bool Status = false;
                if (_T74072.T_COST_TYPE_ID == 0)
                {
                    // New entity
                    obj.T74072.Add(_T74072);
                    Save();
                    Status = true;
                }
                else
                {

                    var data = obj.T74072.Where(p => p.T_COST_TYPE_ID == _T74072.T_COST_TYPE_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_UPD_USER = _T74072.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_COST_TYPE_ID = _T74072.T_COST_TYPE_ID;
                        data.T_LANG1_NAME = _T74072.T_LANG1_NAME;
                        data.T_LANG2_NAME = _T74072.T_LANG2_NAME;

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
                var T74073 = obj.T74073.Find(id);
                obj.T74073.Remove(T74073);
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