using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74127Repository : IT74127
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74127Repository(AmbucareContainer _obj)
        {
            obj.Configuration.ProxyCreationEnabled = false;
            this.obj = _obj;

        }
        // bool SaveData(List<T74094> t74094, List<T74095> t74095)

        public IEnumerable GetEmployee()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from emp in obj.T74004
                         where emp.T_EMP_STATUS !=null
                    select new
                    {
                        T_FIRST_LANG2_NAME = emp.T_FIRST_LANG2_NAME,
                        T_EMP_ID = emp.T_EMP_ID

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
        public IEnumerable GetEmployeeDetails(int empId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from emp in obj.T74004
                         where emp.T_EMP_ID == empId
                    select new
                    {
                        T_EMP_ID = emp.T_EMP_ID,
                        T_FIRST_LANG2_NAME = emp.T_FIRST_LANG2_NAME,
                        T_FATHER_LANG2_NAME=emp.T_FATHER_LANG2_NAME,
                        T_MOTHER_LANG2_NAME= emp.T_MOTHER_LANG2_NAME,
                        T_MOBILE_NO= emp.T_MOBILE_NO,
                        T_PHONE_WORK= emp.T_PHONE_WORK,
                        T_ADDRESS1= emp.T_ADDRESS1,
                        T_ADDRESS2 =emp.T_ADDRESS2
                       

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
        public IEnumerable GetItem(int empId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from item in obj.T74093
                         where item.T_EMP_ID == empId
                         select new
                         {
                             T_EMP_ID = item.T_EMP_ID,
                             T_CHESES_NO = item.T_CHESES_NO,
                             T_ENGIN_NO = item.T_ENGIN_NO,
                             T_ITEM_ID = item.T_ITEM_ID,
                             T_REGI_NO = item.T_REGI_NO,
                             T_DESC = item.T_DESC,
                             T_BARCODE = item.T_BARCODE,
                             T_MODEL_NO = item.T_MODEL_NO


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

        public IEnumerable GetPreviousProblem(int empId, string en, string reg)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t94 in obj.T74094
                    join t95 in obj.T74095 on t94.T_ORDER_ID equals t95.T_ORDER_ID 
                    where t94.T_EMP_ID == empId && t94.T_ENGIN_NO == en && t94.T_REGI_NO == reg
                    select new
                    {
                        T_EMP_ID = t94.T_EMP_ID,
                        T_PROB_DELT_LANG2= t95.T_PROB_DELT_LANG2,
                        T_DEL_DATE = t94.T_DEL_DATE,
                        T_REPAIR_INST_DELT=  t95.T_REPAIR_INST_DELT,
                        T_REPAIR_STATUS = t95.T_REPAIR_STATUS !=null? t95.T_REPAIR_STATUS:"Not Repeir"


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

        public bool SaveData(T74094 t74094, List<T74095> t74095)
        {

            //using (var dbContextTransaction = obj.Database.BeginTransaction())
            //{
                try
                {
                   
                        var max = obj.T74094.Max(x => x.T_ORDER_ID);

                         t74094.T_ORDER_ID = max+1;
                         t74094.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                         t74094.T_ENTRY_DATE = DateTime.Now;
                    obj.T74094.Add(t74094);
                            Save();

                            foreach (var p in t74095)
                            {
                        
                                    var max94 = obj.T74094.Max(m => m.T_ORDER_ID);
                                    var max95 = obj.T74095.Max(x => x.T_PROB_DET_ID);
                                    if (max95 != 0)
                                    {
                                        p.T_PROB_DET_ID = max95 + 1;
                                        p.T_ORDER_ID = max94;
                                        p.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                        p.T_ENTRY_DATE = DateTime.Now;


                                        obj.T74095.Add(p);
                                        Save();
                                    }
                                    else
                                    {
                                        p.T_ORDER_ID = max94;
                                        p.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                        p.T_ENTRY_DATE = DateTime.Now;


                                        obj.T74095.Add(p);
                                        Save();
                                    }


                             

                            }
                       
                }
                catch (DbEntityValidationException dbEx)
                {
                   // dbContextTransaction.Rollback();
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
           // }
            return true;
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
//IEnumerable GetPreviousProblem(int empId, string en, string reg);