using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74114Repository : IT74114
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74114Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
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
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
        }
        //Supplier Drop downList Method Start
        public IEnumerable GetSupplierList()
        {
            IEnumerable query = Enumerable.Empty<object>();
            //CommonDALSQL cSql=new CommonDALSQL();
            DataTable dt=new DataTable();
            //if (CommonClass.CheckForInternetConnection())
            //{
                try
                {
                    query = (from supplier in obj.T74045
                             select new
                             {
                                 T_SUPPLIER_ID = supplier.T_SUPPLIER_ID,
                                 T_LANG2_NAME = supplier.T_LANG2_NAME

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 T_SUPPLIER_ID = r.T_SUPPLIER_ID,
                                 T_LANG2_NAME = r.T_LANG2_NAME
                             }).ToList();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }
                }
            //}
            //else
            //{
            //    try
            //    {
            //        dt = cSql.Query($"Select T_SUPPLIER_ID,T_LANG2_NAME from T74045");
            //        query = dt.AsEnumerable().Select(row =>
            //            new
            //            {
            //                T_SUPPLIER_ID = row["T_SUPPLIER_ID"].ToString(),
            //                T_LANG2_NAME = row["T_LANG2_NAME"].ToString(),

            //            }).ToList();
            //    }
            //    catch (DbEntityValidationException dbEx)
            //    {
            //        foreach (var validationErrors in dbEx.EntityValidationErrors)
            //        {
            //            foreach (var validationError in validationErrors.ValidationErrors)
            //            {
            //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
            //                    validationError.ErrorMessage);
            //            }
            //        }
            //    }
        //}
           
            return query;
        }
        //Supplier Drop downList Method End

        //Store DropDownlist Method Start
        public IEnumerable GetStoreList()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
             
                query = (from t44 in obj.T74044
                         join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                         from t14_t44 in t14_44.DefaultIfEmpty()
                         join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                         from t14_t11 in t14_11.DefaultIfEmpty()
                         join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                         from t14_t02 in t14_02.DefaultIfEmpty()
                         select new
                         {
                             T_STORE_ID = t44.T_STORE_ID,
                             T_AMBU_REG_ID = t44.T_AMBU_REG_ID,
                             Central_Lang2 = t44.T_LANG2_NAME,
                             T_YEAR_MODEL = t14_t44.T_YEAR_MODEL,
                             T_SERIES = t14_t44.T_SERIES,
                             Brand_Lang2 = t14_t02.T_LANG2_NAME,
                             Color_Lang2 = t14_t11.T_LANG2_NAME
                         }).AsEnumerable().Select(k => new
                         {
                             T_STORE_ID = k.T_STORE_ID,
                             T_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : "(" + k.T_AMBU_REG_ID + ")" + k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
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
        //Store DropDownlist Method End

        //Currency DropdownList Method Start
        public IEnumerable GetCurrencyList()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from currency in obj.T74016
                         select new
                         {
                             T_CURRENCY_ID = currency.T_CURRENCY_ID,
                             T_LANG2_NAME = currency.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             T_CURRENCY_ID = r.T_CURRENCY_ID,
                             T_LANG2_NAME = r.T_LANG2_NAME
                         }).ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }
        //Currency DropdownList Method End

        //Item Type Popup list start
        public IEnumerable GetItemTypeList()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Item in obj.T74072
                         select new
                         {
                             T_COST_TYPE_ID = Item.T_COST_TYPE_ID,
                             T_LANG2_NAME = Item.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             T_COST_TYPE_ID = r.T_COST_TYPE_ID,
                             T_LANG2_NAME = r.T_LANG2_NAME
                         }).ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }
        //Item Type Popup list End

        //Item Name Popup list start
        public IEnumerable GetItemNameList(int id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Ambu in obj.T74073
                         join CostType in obj.T74072 on Ambu.T_COST_TYPE_ID equals CostType.T_COST_TYPE_ID
                         where Ambu.T_COST_TYPE_ID == id
                         select new
                         {
                             T_ID = Ambu.T_ID,
                             CostType = Ambu.T_LANG2_NAME

                         }).ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            return query;
            //Item Name Popup list End

        }

        public IEnumerable GetMaxValue()
        {
            IEnumerable query = Enumerable.Empty<object>();
            //int query = 0;
            try
            {
                query = obj.T74030.Max(x => x.T_PUR_ID).ToString();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }

        public IEnumerable GetUom(int T_TYPE_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74003.Join(obj.T74072, t03 => t03.T_TYPE_ID, t72 => t72.T_COST_TYPE_ID,
                (t03, t72) => new { t03, t72 }).Where(x => x.t03.T_TYPE_ID == T_TYPE_ID).Select(a => new
                {
                    T_LANG2_NAME = a.t03.T_LANG2_NAME,
                    T_ITEM_UM_ID = a.t03.T_ITEM_UM_ID
                }).ToList();
            return query;
        }
        //For Insert Method Start

        public string insert_T74114(T74030 t74030, List<T74031> t74031List)
        {
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    t74030.T_PUR_NO = CommonClass.General_Code("PR");
                    t74030.T_COMPANY_ID = Int32.Parse(HttpContext.Current.Session["T_COMPANY_ID"].ToString());
                    t74030.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                    t74030.T_ENTRY_DATE = DateTime.Now;
                    obj.T74030.Add(t74030);
                    Save();
                    int T_PUR_ID = obj.T74030.Where(m => m.T_ENTRY_USER == t74030.T_ENTRY_USER).Max(a => a.T_PUR_ID);
                    foreach (T74031 t74031 in t74031List)
                    {
                        int t31 = obj.T74031.Count() > 0 ? obj.T74031.Max(a => a.T_PUR_DTL_ID) : 0;
                        t74031.T_PUR_DTL_ID = t31 != 0 ? t31 + 1 : 1;
                        t74031.T_PUR_ID = T_PUR_ID;
                        t74031.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                        t74031.T_ENTRY_DATE = DateTime.Now;
                        obj.T74031.Add(t74031);
                        Save();
                    }
                    dbContextTransaction.Commit();
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbContextTransaction.Rollback();
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
            return t74030.T_PUR_NO;

        }

        //private void General_Code(string v)
        //{
        //    throw new NotImplementedException();
        //}

        //For Insert Method End 

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
    }
}