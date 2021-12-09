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
    public class T74117Repository : IT74117
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74117Repository(AmbucareContainer _Obj)
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
            return query;
        }
        //Supplier Drop downList Method End

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
        //Item Name Popup list End

        //Item Get UOM list start
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
        //Item Get UOM list End

        //For Insert Method Start
        public string insert_T74117(T74048 t74048, List<T74049> t74049List)
        {
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    t74048.T_SL_REQ_NO = CommonClass.General_Code("SR");
                    t74048.T_COMPANY_ID = Int32.Parse(HttpContext.Current.Session["T_COMPANY_ID"].ToString());
                    t74048.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                    t74048.T_ENTRY_DATE = DateTime.Now;
                    obj.T74048.Add(t74048);
                    Save();
                    int T_SL_REQ_ID = obj.T74048.Where(m => m.T_ENTRY_USER == t74048.T_ENTRY_USER).Max(a => a.T_SL_REQ_ID);
                    foreach (T74049 t74049 in t74049List)
                    {
                        int t49 = obj.T74049.Count() > 0 ? obj.T74049.Max(a => a.T_SL_REQ_DTL_ID) : 0;
                        t74049.T_SL_REQ_DTL_ID = t49 != 0 ? t49 + 1 : 1;
                        t74049.T_SL_REQ_ID = T_SL_REQ_ID;
                        t74049.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                        t74049.T_ENTRY_DATE = DateTime.Now;
                        obj.T74049.Add(t74049);
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
            return t74048.T_SL_REQ_NO;
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
        //For Insert Method End 

        //Store From DropDownlist Method Start
        public IEnumerable GetStoreListFrom()
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

                    where t44.T_STORE_ID != 1
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
                    T_LANG2_NAME = "(" + k.T_AMBU_REG_ID + ")" + k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
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
        //Store From DropDownlist Method End

        //Store To DropDownlist Method Start
        public IEnumerable GetStoreListTo()
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
        //Store To DropDownlist Method End

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

        //Person Type Popup Method Start
        public IQueryable<T74005> GetPersonalType
        {
            get { return obj.T74005; }
        }

        //Person Name Popup Method Start
        public IEnumerable GetPersonName(int id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from person in obj.T74004
                    join personType in obj.T74005 on person.T_EMP_TYP_ID equals personType.T_EMP_TYP_ID
                    where personType.T_EMP_TYP_ID==id
                    select new
                    {
                        T_EMP_ID = person.T_EMP_ID,
                        Name = person.T_FIRST_LANG2_NAME,
                        Name1 = person.T_FATHER_LANG2_NAME,
                        Name2 = person.T_GFATHER_LANG2_NAME,
                        Name3 = person.T_MOTHER_LANG2_NAME,
                        Name4 = person.T_FAMILY_LANG2_NAME

                    }).AsEnumerable().Select((r, i) => new
                {
                    T_EMP_ID = r.T_EMP_ID,
                    T_NAME = r.Name + " " + r.Name1 + " " + r.Name2 + " " + r.Name3 + " " + r.Name4
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

    }
}