using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74129Repository : IT74129
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private readonly T74128DAL _t74128 = new T74128DAL();
        public T74129Repository(AmbucareContainer _obj)
        {
            obj.Configuration.ProxyCreationEnabled = false;
            this.obj = _obj;

        }

        public IEnumerable GetGridData()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t94 in obj.T74094
                    join t95 in obj.T74095 on t94.T_ORDER_ID equals t95.T_ORDER_ID
                    join t04 in obj.T74004 on t94.T_EMP_ID equals t04.T_EMP_ID 
                    orderby t95.T_PROB_DET_ID descending 
                    select new
                    {
                    T_FIRST_LANG2_NAME= t04.T_FIRST_LANG2_NAME,
                    T_PROB_DET_ID = t95.T_PROB_DET_ID,
                    T_PROB_DELT_LANG2= t95.T_PROB_DELT_LANG2,
                    T_REPAIR_INST_DELT= t95.T_REPAIR_INST_DELT,
                    T_APR_DELV_DATE= t95.T_APR_DELV_DATE,
                    T_ENTRY_DATE= t94.T_ENTRY_DATE,
                    T_REPAIR_STATUS=  t95.T_REPAIR_STATUS
                    }).ToList();
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

        public bool SaveData(T74095 t95)
        {
            try
            {
                var chk = obj.T74095.Where(x => x.T_PROB_DET_ID == t95.T_PROB_DET_ID).FirstOrDefault();
                if (chk!=null)
                {
                    chk.T_REPAIR_INST_DELT = t95.T_REPAIR_INST_DELT;
                    chk.T_REPAIR_DATE = t95.T_REPAIR_DATE;
                    chk.T_REPAIR_STATUS = "Done";
                    Save();
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

            return true;
        }

        public IEnumerable GetSearchData(string stl)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74095.Where(p => p.T_PROB_DELT_LANG2.ToUpper().Contains(stl.ToUpper())).ToList();
              

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