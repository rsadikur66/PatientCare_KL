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
    public class T74018Repository:IT74018
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74018Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        //Form Item DropDown Method Start
        public IQueryable<T74072> GetCostTypeData
        {
            get { return obj.T74072; }
        }
     
        public bool Insert(T74018 _T74018)
        {
            try
            {
                bool Status = false;
                if (_T74018.T_ID == 0)
                {
                    // New entity
                    obj.T74018.Add(_T74018);
                    Save();
                    Status = true;
                }
                else
                {

                    var data = obj.T74018.Where(p => p.T_ID == _T74018.T_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_UPD_USER = _T74018.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_COST_TYPE_ID = _T74018.T_COST_TYPE_ID;
                        data.T_AMOUNT = _T74018.T_AMOUNT;
                        data.T_DESCRIPTION = _T74018.T_DESCRIPTION;
                        data.T_ACTIVE = _T74018.T_ACTIVE;
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
            try
            {
                var T74018 = obj.T74018.Find(id);
                obj.T74018.Remove(T74018);
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

        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Ambu in obj.T74018
                    join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID

                         select new
                    {
                        T_ID = Ambu.T_ID,
                        T_AMOUNT = Ambu.T_AMOUNT,
                        T_DESCRIPTION = Ambu.T_DESCRIPTION,
                        CostType = CostType.T_LANG2_NAME,
                        T_COST_TYPE_ID= CostType.T_COST_TYPE_ID,
                        T_ACTIVE = Ambu.T_ACTIVE

                         }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                    r.T_ID,
                    r.T_COST_TYPE_ID,
                    r.T_AMOUNT,
                    r.T_DESCRIPTION,
                    r.CostType,
                    r.T_ACTIVE

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
                    query = (from Ambu in obj.T74018
                        join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID
                             where Ambu.T_AMOUNT.ToString().Contains(searchValue) ||
                                   Ambu.T_DESCRIPTION.ToUpper().Contains(searchValue.ToUpper()) ||
                                   CostType.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                        select Ambu).Count();
                }
                else
                {
                    query = (from Ambu in obj.T74018
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

        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Ambu in obj.T74018
                         join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID
                         where
                         Ambu.T_DESCRIPTION.ToUpper().Contains(searchValue.ToUpper()) ||
                               CostType.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                         select new
                    {
                             
                             T_DESCRIPTION = Ambu.T_DESCRIPTION,
                             CostType = CostType.T_LANG2_NAME

                    }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                   
                    r.T_DESCRIPTION,
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

        public void Dispose()
        {
            obj.Dispose();
        }
    }
}