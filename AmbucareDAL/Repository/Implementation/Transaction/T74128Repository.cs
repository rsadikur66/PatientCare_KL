using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74128Repository: IT74128

    {
    private AmbucareContainer obj = new AmbucareContainer();
    private readonly T74128DAL _t74128 = new T74128DAL();
    public T74128Repository(AmbucareContainer _obj)
    {
        obj.Configuration.ProxyCreationEnabled = false;
        this.obj = _obj;

    }

        public IEnumerable GetTrade()
        {
            var userId = HttpContext.Current.Session["T_USER_ID"].ToString();
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t57 in obj.T74057
                join t15 in obj.T74015 on t57.T_EMP_ID equals t15.T_EMP_ID
                join t44 in obj.T74044 on t15.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                join t27 in obj.T74027 on t44.T_STORE_ID equals t27.T_STORE_ID
                join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                where t57.T_USER_ID == userId && t73.T_COST_TYPE_ID == 23

                     group t73 by new{ t73.T_LANG2_NAME, t73.T_COST_TYPE_DTL_ID , t27.T_ITEM_ID , t27.T_STORE_ID } into t733
                     select new
                {
                    T_STORE_ID=t733.Key.T_STORE_ID,
                    T_ITEM_ID = t733.Key.T_ITEM_ID,
                    T_LANG2_NAME = t733.Key.T_LANG2_NAME,
                    T_COST_TYPE_DTL_ID = t733.Key.T_COST_TYPE_DTL_ID
                     }).ToList();
            //  var data = _t74128.GetTrade();
             return query;
        }

        public IEnumerable GetPackSize(int cosTyDel, int storeid)
        {
            IEnumerable query = Enumerable.Empty<object>();
            
            try
            {
                query = (from t73 in obj.T74073
                         join t27 in obj.T74027 on t73.T_ID equals t27.T_ITEM_ID
                         join t03 in obj.T74003 on t27.T_ITEM_UM_ID equals t03.T_ITEM_UM_ID
                         where t27.T_STORE_ID == storeid  && t73.T_COST_TYPE_DTL_ID == cosTyDel
                         group t03 by new { t03.T_LANG2_NAME, t03.T_ITEM_UM_ID, t03.T_QTY } into um
                         select new
                    {
                        T_ITEM_UM_ID= um.Key.T_ITEM_UM_ID,
                        T_LANG2_NAME= um.Key.T_LANG2_NAME,
                        T_QTY = um.Key.T_QTY
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

        public decimal GetStock(int item, int store, int umId)
        {
           // var dd = HttpContext.Current.Session["T_USER_ID"].ToString();
           // IEnumerable query = Enumerable.Empty<object>();
            decimal query = 0;
            try
            {
                 query = obj.Database.SqlQuery<decimal>(" SELECT   nvl(fn_Stock_Qty(" + item + "," + umId + "," + store + "),0) from dual ").First();
               // return query;
                //query = (from t27 in obj.T74027
                //        join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                //        where t27.T_ITEM_UM_ID == umId && t73.T_COST_TYPE_DTL_ID == cosTyDel

                //         select new
                //    {
                //        T_ITEM_UM_ID = t27.T_ITEM_UM_ID,
                //        T_ITEM_ID=  t27.T_ITEM_ID,
                //        T_STORE_ID= t27.T_STORE_ID

                //    }).AsEnumerable().Select((r, i) => new
                //{
                //   // T_ITEM_UM_ID = r.T_ITEM_UM_ID,
                //    Stock = Sum(r.T_ITEM_UM_ID,r.T_ITEM_ID,r.T_STORE_ID)
                //    }).ToList();
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

        public decimal Sum(int? umid, int? itemid,int? storeid)
        {
            //var query = obj.Database.SqlQuery<decimal>(" SELECT   nvl(fn_Stock_Qty(" + itemid + "," + umid + "," + storeid + "),0) from dual ").First();
            //return query;
            var re = obj.T74027.Where(p => p.T_ITEM_UM_ID == umid && p.T_ITEM_ID == itemid && p.T_STORE_ID == storeid).Sum(m => m.T_TRNSACTION_QTY);
            decimal va = Convert.ToDecimal(re);
            return va;
        }

        public IEnumerable GetPrice(int costype, int umId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t89 in obj.T74089
                    where t89.T_COST_TYPE_DTL_ID == costype && t89.T_ITEM_UM_ID == umId && t89.T_ACTIVE == "1"
                    select new
                    {
                        T_SALE_PRICE = t89.T_SALE_PRICE,
                        T_COST_TYPE_DTL_ID = t89.T_COST_TYPE_DTL_ID
                    }).AsEnumerable().Select((r, i) => new
                {
                    T_SALE_PRICE = r.T_SALE_PRICE ,
                    PiecePrice = Piece(r.T_COST_TYPE_DTL_ID)
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

        public decimal Piece(int? costype)
        {
            var re = obj.T74089.Where(p => p.T_ITEM_UM_ID == 123 && p.T_COST_TYPE_DTL_ID == costype && p.T_ACTIVE == "1").Max(m=>m.T_SALE_PRICE);
            decimal va = Convert.ToDecimal(re);
            return va;
        }

        public IEnumerable GetVat()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t18 in obj.T74018
                    where t18.T_ID == 26 && t18.T_ACTIVE == 1
                    select new
                    {
                        t18.T_ID,
                        t18.T_AMOUNT,
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

        public IEnumerable GetPatienRequest()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t41 in obj.T74041
                    join t46 in obj.T74046 on t41.T_PAT_ID equals t46.T_PAT_ID
                    orderby t41.T_REQUEST_ID descending 
                    select new
                    {
                        T_PAT_ID= t41.T_PAT_ID,
                        T_REQUEST_ID=  t41.T_REQUEST_ID,
                        T_FIRST_LANG2_NAME= t46.T_FIRST_LANG2_NAME,
                        T_MOBILE_NO= t46.T_MOBILE_NO,
                        T_ADDRESS1= t46.T_ADDRESS1,
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

        public bool SaveData(T74036 t36, List<T74037> t37)
        {
            try
            {
                string user = HttpContext.Current.Session["T_USER_ID"].ToString();
                DateTime? date = DateTime.Now;
                //T7436-----------------------------------------------------------

                t36.T_USER_ID = user;
                t36.T_ENTRY_USER_ = user;
                t36.T_ENTRY_DATE_ = date;
                t36.T_ISSUE_DATE = date;
                //t36.T_DIS_AMOUNT = T_DIS_AMOUNT;
                t36.T_COMPANY_ID = Int32.Parse(HttpContext.Current.Session["T_COMPANY_ID"].ToString());
                obj.T74036.Add(t36);
                 Save();
             
                int T_ISSUE_ID = obj.T74036.Where(m => m.T_ENTRY_USER_ == t36.T_ENTRY_USER_).Max(a => a.T_ISSUE_ID);
                //----------------------------------------------------
                //---------T7437---------------------------------------
                foreach (var i in t37)
                {
                    i.T_ISSUE_ID = T_ISSUE_ID;

                    var productList = (from t27 in obj.T74027
                       // join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                        //join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                        join tt36 in obj.T74036 on i.T_ISSUE_ID equals tt36.T_ISSUE_ID
                        where t27.T_TRNSACTION_QTY > 0
                              && t27.T_ITEM_ID == i.T_ITEM_ID
                             // && t73.T_COST_TYPE_ID == 23
                              && t27.T_ITEM_UM_ID == i.T_UOM_ID
                              && t27.T_STORE_ID == tt36.T_STORE_ID
                             // && t89.T_ACTIVE == "1"
                            //  && t89.T_ITEM_UM_ID == i.T_UOM_ID
                        select new
                        {
                            EXP_DATE = t27.T_EXPIRE_DATE,
                            Qty = t27.T_TRNSACTION_QTY,
                            ProdId = t27.T_ITEM_ID,
                          //  Rate = t89.T_SALE_PRICE,
                            Stock_Id = t27.T_CUR_STOCK_ID,
                            t27.T_COMPANY_ID,
                            t27.T_UNIT_VALUE,
                            t27.T_MF_DATE,
                            t27.T_PR_RCV_DTL_ID,
                            t27.T_STOCK_TYPE_ID,
                           // t27.T_LOT_NO,
                           // t27.T_BATCH,
                            t27.T_SUPPLIER_ID,
                            t27.T_ITEM_UM_ID
                        }).OrderBy(k => k.EXP_DATE).ToList();
                    var dd = productList;


                    decimal? quantityRemains = i.T_QUANTITY;

                    foreach (var singleObject in productList.Where(p => p.ProdId == i.T_ITEM_ID)
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
                        long tt37 = obj.T74037.Count() > 0 ? obj.T74037.Max(a => a.T_ISSUE_DTL_ID) : 0;

                        T74037 detailObject = new T74037();
                        detailObject.T_ISSUE_DTL_ID = tt37 != 0 ? tt37 + 1 : 1;
                        detailObject.T_ISSUE_ID = T_ISSUE_ID;
                        detailObject.T_ENTRY_USER = user;
                        detailObject.T_ENTRY_DATE_ = date;
                        detailObject.T_CUR_STOCK_ID = singleObject.Stock_Id;
                        detailObject.T_EXPIRE_DATE = singleObject.EXP_DATE;
                        detailObject.T_QUANTITY = tQuantity;
                        detailObject.T_DISCOUNT = i.T_DISCOUNT;
                       detailObject.T_TOTAL_AMOUNT = (tQuantity) * (i.T_SALE_PRICE);
                        detailObject.T_ITEM_ID = i.T_ITEM_ID;
                        detailObject.T_UOM_ID = i.T_UOM_ID;
                        detailObject.T_SALE_PRICE = i.T_SALE_PRICE;
                        detailObject.T_PCS_BOX = i.T_PCS_BOX;

                        var d = detailObject;
                        obj.T74037.Add(detailObject);
                        Save();
                        var t27 = obj.T74027.Where(p => p.T_CUR_STOCK_ID == singleObject.Stock_Id && p.T_TRNSACTION_QTY>0).FirstOrDefault();
                        if (t27 != null)
                        {
                            t27.T_TRNSACTION_QTY = t27.T_TRNSACTION_QTY - tQuantity;
                            Save();
                        }


                    }
                }
                //-------------------------------
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
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}