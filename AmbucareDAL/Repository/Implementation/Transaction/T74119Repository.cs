using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74119Repository :IT74119
    {
        private AmbucareContainer obj = new AmbucareContainer();
        public T74119Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }
        public IEnumerable getStoreName(int T_STORE_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
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
                T_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
            }).ToList().Where(a=>a.T_STORE_ID== T_STORE_ID).Select(b=>b.T_LANG2_NAME).FirstOrDefault();
            return query;
        }

        public decimal getStock(int? ITEM_ID, int? UM_ID, int? STORE_ID)
        {
           
            var  query = obj.Database.SqlQuery<decimal>("SELECT NVL(fn_Stock_Qty(" + ITEM_ID + "," + UM_ID + "," + STORE_ID + "),0) FROM DUAL").First();
            
            return query;
        }
        public decimal? avgSalePrice(int? T_COST_TYPE_DTL_ID,int? T_ITEM_UM_ID)
        {

           decimal? query = obj.T74089.Where(a => a.T_COST_TYPE_DTL_ID == T_COST_TYPE_DTL_ID && a.T_ITEM_UM_ID == T_ITEM_UM_ID && a.T_ACTIVE == "1")
                .Average(r => r.T_SALE_PRICE);
            return query;

        }
        public IEnumerable getSalesData(int T_SL_REQ_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t48 in obj.T74048
                    join t49 in obj.T74049 on t48.T_SL_REQ_ID equals t49.T_SL_REQ_ID
                    join t54 in obj.T74054 on t48.T_COMPANY_ID equals t54.T_COMPANY_ID
                    join t45 in obj.T74045 on t48.T_SUPPLIER_ID equals t45.T_SUPPLIER_ID
                    join t16 in obj.T74016 on t48.T_CURRENCY_ID equals t16.T_CURRENCY_ID

                    join t73 in obj.T74073 on t49.T_ITEM_ID equals t73.T_ID
                    join t03 in obj.T74003 on t49.T_ITEM_UM_ID equals t03.T_ITEM_UM_ID
                    join t89 in obj.T74089.Where(s=>s.T_ACTIVE=="1").GroupBy(g=>new{ g.T_COST_TYPE_DTL_ID , g.T_ITEM_UM_ID }) on t73.T_COST_TYPE_DTL_ID equals  t89.Key.T_COST_TYPE_DTL_ID
                    where t48.T_SL_REQ_ID == T_SL_REQ_ID &&
                    t89.Key.T_ITEM_UM_ID == t49.T_ITEM_UM_ID  
                    //t89.Key.T_ACTIVE == "1"
                         select new
                    {
                        t48.T_COMPANY_ID,
                        t48.T_CURRENCY_ID,
                        t48.T_SUPPLIER_ID,
                        t48.T_SL_REQ_DATE,
                        t48.T_UPD_DATE,
                        t48.T_SL_REQ_STATUS,
                        T_STORE_ID_TO = t48.T_STR_TO_ID,
                        T_STORE_ID_FROM = t48.T_STR_FR_ID,

                        t49.T_SL_REQ_DTL_ID,
                        t49.T_ITEM_ID,
                        t49.T_SL_QTY,
                        t49.T_PRIORITY,
                        t49.T_ITEM_UM_ID,
                        t49.T_RCV_QTY,
                        t49.T_SL_STS_DTL,
                        t89.Key.T_COST_TYPE_DTL_ID,
                        T_ITEM_UOM_ID=t89.Key.T_ITEM_UM_ID,
                        T_COMPANY_NAME = t54.T_LANG2_NAME,
                        T_SUPPLIER_NAME = t45.T_LANG2_NAME,
                        T_CURRENCY_NAME = t16.T_LANG2_NAME,
                        T_ITEM_NAME = t73.T_LANG2_NAME,
                        T_ITEM_UM_NAME = t03.T_LANG2_NAME
                         }).AsEnumerable().Select(k => new
                    {
                    k.T_COMPANY_ID,
                    k.T_COMPANY_NAME,
                    k.T_SUPPLIER_ID,
                    k.T_SUPPLIER_NAME,
                    k.T_CURRENCY_ID,
                    k.T_CURRENCY_NAME,
                    k.T_STORE_ID_TO,
                    k.T_SL_STS_DTL,
                    k.T_SL_REQ_STATUS,
                    T_STORE_TO_LANG2 = getStoreName(Int32.Parse(k.T_STORE_ID_TO.ToString())),
                    k.T_STORE_ID_FROM,
                    T_STORE_FROM_LANG2 = getStoreName(Int32.Parse(k.T_STORE_ID_FROM.ToString())),
                    k.T_SL_REQ_DATE,
                    T_UPD_DATE=k.T_UPD_DATE==null?DateTime.Now:k.T_UPD_DATE,
                    k.T_SL_REQ_DTL_ID,
                    k.T_ITEM_ID,
                    k.T_SL_QTY,
                    k.T_ITEM_UM_ID,
                    k.T_PRIORITY,
                    k.T_ITEM_NAME,
                    k.T_ITEM_UM_NAME,
                    STOCK_QUANTITY  = getStock(k.T_ITEM_ID, k.T_ITEM_UM_ID, k.T_STORE_ID_FROM),
                    T_RCV_QTY = k.T_RCV_QTY!=null?k.T_RCV_QTY:0,
                    T_RATE = avgSalePrice(k.T_COST_TYPE_DTL_ID,k.T_ITEM_UOM_ID)
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

        public IEnumerable getEmpList(int T_STORE_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t15 in obj.T74015
                join t44 in obj.T74044 on t15.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                join t04 in obj.T74004 on t15.T_EMP_ID equals t04.T_EMP_ID
                join t05 in obj.T74005 on t04.T_EMP_TYP_ID equals t05.T_EMP_TYP_ID
                where t44.T_STORE_ID == T_STORE_ID
                select new
                {
                    t04.T_EMP_ID,
                    t04.T_FIRST_LANG2_NAME,
                    t04.T_FATHER_LANG2_NAME,
                    t04.T_GFATHER_LANG2_NAME,
                    t04.T_FAMILY_LANG2_NAME,
                    t05.T_LANG2_NAME
                }).AsEnumerable().Select(k => new
            {
                k.T_EMP_ID,
                EMP_NAME = k.T_FIRST_LANG2_NAME + " "+ k.T_FATHER_LANG2_NAME + " " + k.T_GFATHER_LANG2_NAME + " " + k.T_FAMILY_LANG2_NAME,
                EMP_TYPE=k.T_LANG2_NAME

                }).ToList();
            return query;

        }

        public void Insert(T74027 t74027, T74036 t74036, List<T74037> t74037List, T74048 t74048,
            List<T74049> t74049List)
        {
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {   
                try
            {
                string user = HttpContext.Current.Session["T_USER_ID"].ToString();
                DateTime? date = DateTime.Now;
                //T7436-----------------------------------------------------------
                t74036.T_USER_ID = user;
                t74036.T_COMPANY_ID = Int32.Parse(HttpContext.Current.Session["T_COMPANY_ID"].ToString());
                t74036.T_ENTRY_USER_ = user;
                t74036.T_ENTRY_DATE_ = date;
                obj.T74036.Add(t74036);
                Save();

                int T_ISSUE_ID = obj.T74036.Where(m => m.T_ENTRY_USER_ == t74036.T_ENTRY_USER_).Max(a => a.T_ISSUE_ID);
                //T74037-----------------------------------------------------------
                foreach (T74037 newChild in t74037List)
                {
                    newChild.T_ISSUE_ID = T_ISSUE_ID;

                    var productList = (from t27 in obj.T74027
                        join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                        join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                        join t36 in obj.T74036 on newChild.T_ISSUE_ID equals t36.T_ISSUE_ID
                        where t27.T_TRNSACTION_QTY > 0
                              && t73.T_ID == newChild.T_ITEM_ID
                              //&& t73.T_COST_TYPE_ID == 23
                              && t27.T_ITEM_UM_ID == newChild.T_UOM_ID
                              && t27.T_STORE_ID == t36.T_STORE_ID
                              && t89.T_ACTIVE == "1"
                              && t89.T_ITEM_UM_ID == newChild.T_UOM_ID
                        select new
                        {
                            EXP_DATE = t27.T_EXPIRE_DATE,
                            Qty = t27.T_TRNSACTION_QTY,
                            ProdId = t27.T_ITEM_ID,
                            Rate = t89.T_SALE_PRICE,
                            Stock_Id = t27.T_CUR_STOCK_ID,
                            t27.T_COMPANY_ID,
                            t27.T_UNIT_VALUE,
                            t27.T_MF_DATE,
                            t27.T_PR_RCV_DTL_ID,
                            t27.T_STOCK_TYPE_ID,
                            t27.T_LOT_NO,
                            t27.T_BATCH,
                            t27.T_SUPPLIER_ID,
                            t27.T_ITEM_UM_ID
                        }).OrderBy(k => k.EXP_DATE).ToList();

                    decimal? quantityRemains = newChild.T_QUANTITY;

                    foreach (var singleObject in productList.Where(p => p.ProdId == newChild.T_ITEM_ID)
                        .Where(q => q.Qty > 0))
                    {
                        if (quantityRemains == 0) break;

                        decimal? tQuantity;
                        if (quantityRemains > singleObject.Qty)
                        {
                            tQuantity = singleObject.Qty;
                            quantityRemains = quantityRemains - singleObject.Qty;
                        }
                        else
                        {
                            tQuantity = quantityRemains;
                            quantityRemains = 0;
                        }

                        long t37 = obj.T74037.Count() > 0 ? obj.T74037.Max(a => a.T_ISSUE_DTL_ID) : 0;

                        T74037 detailObject = new T74037();
                        detailObject.T_ISSUE_DTL_ID = t37 != 0 ? t37 + 1 : 1;
                        detailObject.T_ISSUE_ID = T_ISSUE_ID;
                        detailObject.T_ENTRY_USER = user;
                        detailObject.T_ENTRY_DATE_ = date;
                        detailObject.T_CUR_STOCK_ID = singleObject.Stock_Id;
                        detailObject.T_EXPIRE_DATE = singleObject.EXP_DATE;
                        detailObject.T_QUANTITY = tQuantity;
                        detailObject.T_TOTAL_AMOUNT = (tQuantity) * (newChild.T_SALE_PRICE);
                        detailObject.T_ITEM_ID = newChild.T_ITEM_ID;
                        detailObject.T_UOM_ID = newChild.T_UOM_ID;
                        detailObject.T_SALE_PRICE = newChild.T_SALE_PRICE;
                        detailObject.T_SL_REQ_DTL_ID = newChild.T_SL_REQ_DTL_ID;
                        obj.T74037.Add(detailObject);
                        Save();
                        var t27 = obj.T74027.Where(p => p.T_CUR_STOCK_ID == singleObject.Stock_Id).FirstOrDefault();
                        if (t27 != null)
                        {
                            t27.T_TRNSACTION_QTY = t27.T_TRNSACTION_QTY - tQuantity;
                            int key = obj.T74027.Max(a => a.T_CUR_STOCK_ID) + 1;
                            T74027 stk = new T74027();
                            stk.T_CUR_STOCK_ID = key;
                            stk.T_COMPANY_ID = singleObject.T_COMPANY_ID;
                            stk.T_ITEM_ID = singleObject.ProdId;
                            stk.T_UNIT_VALUE = singleObject.T_UNIT_VALUE;
                            stk.T_TOTAL_VALUE = singleObject.T_UNIT_VALUE * tQuantity;
                            stk.T_PURCHASE_QTY = tQuantity;
                            stk.T_TRNSACTION_QTY = tQuantity;
                            stk.T_STORE_ID = t74027.T_STORE_ID;
                            stk.T_STOCK_DATE = date;
                            stk.T_ENTRY_USER_ = user;
                            stk.T_ENTRY_DATE_ = date;
                            stk.T_EXPIRE_DATE = singleObject.EXP_DATE;
                            stk.T_MF_DATE = singleObject.T_MF_DATE;
                            stk.T_PR_RCV_DTL_ID = singleObject.T_PR_RCV_DTL_ID;
                            stk.T_STOCK_TYPE_ID = singleObject.T_STOCK_TYPE_ID;
                            stk.T_LOT_NO = singleObject.T_LOT_NO;
                            stk.T_BATCH = singleObject.T_BATCH;
                            stk.T_SUPPLIER_ID = singleObject.T_SUPPLIER_ID;
                            stk.T_ITEM_UM_ID = singleObject.T_ITEM_UM_ID;
                            obj.T74027.Add(stk);
                            Save();
                        }


                    }


                }

                //T74049------------------------------------------------------------------
                foreach (T74049 t74049 in t74049List)
                {
                    var t49 = obj.T74049.Where(p => p.T_SL_REQ_DTL_ID == t74049.T_SL_REQ_DTL_ID).FirstOrDefault();
                    if (t49 != null)
                    {
                        t49.T_RCV_QTY = t74049.T_RCV_QTY;
                        t49.T_SL_STS_DTL = t74049.T_SL_STS_DTL;
                        Save();
                    }
                }

                //T74048------------------------------------------------------------
                var t48 = obj.T74048.Where(p => p.T_SL_REQ_ID == t74048.T_SL_REQ_ID).FirstOrDefault();
                if (t48 != null)
                {
                    t48.T_UPD_USER = user;
                    t48.T_UPD_DATE = date;
                    int k = 0;
                    var t74049 = obj.T74049.Where(a => a.T_SL_REQ_ID == t74048.T_SL_REQ_ID).ToList();
                    foreach (var chk in t74049)
                    {
                        if (chk.T_SL_STS_DTL == "Partial" || chk.T_SL_STS_DTL == null)
                        {
                            k = 1;
                        }

                    }

                    if (k != 1)
                    {
                        t48.T_SL_REQ_STATUS = "1";
                    }

                    Save();

                }





                dbContextTransaction.Commit();
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
    }
        public void Dispose()
        {
            obj.Dispose();
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
    }
}