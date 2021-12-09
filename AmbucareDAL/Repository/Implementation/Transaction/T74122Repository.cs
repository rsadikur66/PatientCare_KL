using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;
using Ninject;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74122Repository : IT74122
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74122Repository(AmbucareContainer _Obj)
        {
            //_Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IEnumerable GetStoreName(int T_STORE_ID)
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
                         T_LANG2_NAME =
                     k.T_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                     }).ToList().Where(a => a.T_STORE_ID == T_STORE_ID).Select(b => b.T_LANG2_NAME).FirstOrDefault();
            return query;
        }

        //public IEnumerable GetTransferData(int T_TRANSFER_REQ_ID)
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //    try
        //    {


        //        query = (from t80 in obj.T74080
        //                 from t81 in obj.T74081.Where(x => x.T_TRANSFER_REQ_ID == t80.T_TRANSFER_REQ_ID)
        //                 from t73 in obj.T74073.Where(x => (x.T_ID == t81.T_ITEM_ID)).DefaultIfEmpty()
        //                 from t45 in obj.T74045.Where(x => (x.T_SUPPLIER_ID == t80.T_SUPPLIER_ID)).DefaultIfEmpty()
        //                 from t16 in obj.T74016.Where(x => (x.T_CURRENCY_ID == t80.T_CURRENCY_ID)).DefaultIfEmpty()
        //                 from t03 in obj.T74003.Where(x => (x.T_ITEM_UM_ID == t81.T_UOM_ID)).DefaultIfEmpty()
        //                 where (t80.T_TRANSFER_REQ_ID == T_TRANSFER_REQ_ID)
        //                 group new { t80, t81, t45, t16, t73, t03 } by new
        //                 {
        //                     t80.T_TRANSFER_REQ_ID,
        //                     t80.T_TRANSFER_REQ_NO,
        //                     t80.T_ISSUE_ID,
        //                     t80.T_REQ_DATE,
        //                     t80.T_SEND_TO_STORE_ID,
        //                     t80.T_REQ_SET_BY_STORE_ID,
        //                     t80.T_TRANSFER_FOR,
        //                     t80.T_TRANSFER_DELIVERY_METHOD,
        //                     t80.T_STATUS,
        //                     t80.T_SUPPLIER_ID,
        //                     T_SUPPLIER_NAME = t45.T_LANG2_NAME,
        //                     t80.T_CURRENCY_ID,
        //                     T_CURRENCY_NAME = t16.T_LANG2_NAME,
        //                     t81.T_ITEM_ID,
        //                     T_ITEM_NAME = t73.T_LANG2_NAME,
        //                     //t81.T_TRANSFER_QTY,
        //                     t81.T_UOM_ID,
        //                     T_UM_NAME = t03.T_LANG2_NAME,
        //                     t81.T_RECEIVE_TYPE,
        //                     t81.T_UNIT_VALUE,
        //                     //t81.T_TOTAL_VALUE,
        //                     t81.T_OTHERS,
        //                     t81.T_TRANSFER_PRORITY
        //                 } into i
        //                 select new
        //                 {
        //                     i.Key.T_TRANSFER_REQ_ID,
        //                     i.Key.T_TRANSFER_REQ_NO,
        //                     i.Key.T_ISSUE_ID,
        //                     i.Key.T_REQ_DATE,
        //                     T_STORE_ID_TO = i.Key.T_SEND_TO_STORE_ID,
        //                     T_STORE_ID_FROM = i.Key.T_REQ_SET_BY_STORE_ID,
        //                     i.Key.T_TRANSFER_FOR,
        //                     i.Key.T_TRANSFER_DELIVERY_METHOD,
        //                     i.Key.T_STATUS,
        //                     i.Key.T_SUPPLIER_ID,
        //                     i.Key.T_SUPPLIER_NAME,
        //                     i.Key.T_CURRENCY_ID,
        //                     i.Key.T_CURRENCY_NAME,
        //                     i.Key.T_ITEM_ID,
        //                     i.Key.T_ITEM_NAME,
        //                     //i.Key.T_TRANSFER_QTY,
        //                     Transfer_Quantity = i.Sum(a => a.t81.T_TRANSFER_QTY),
        //                     i.Key.T_UOM_ID,
        //                     i.Key.T_UM_NAME,
        //                     i.Key.T_RECEIVE_TYPE,
        //                     i.Key.T_UNIT_VALUE,
        //                     //i.Key.T_TOTAL_VALUE,
        //                     TotalAmount = i.Sum(b => b.t81.T_TOTAL_VALUE),
        //                     i.Key.T_OTHERS,
        //                     i.Key.T_TRANSFER_PRORITY

        //                 }).AsEnumerable().Select(k => new
        //                 {
        //                     k.T_TRANSFER_REQ_ID,
        //                     k.T_TRANSFER_REQ_NO,
        //                     k.T_ISSUE_ID,
        //                     k.T_REQ_DATE,
        //                     k.T_STORE_ID_FROM,
        //                     T_STORE_FROM_LANG2 = GetStoreName(Int32.Parse(k.T_STORE_ID_FROM.ToString())),
        //                     k.T_STORE_ID_TO,
        //                     T_STORE_TO_LANG2 = GetStoreName(Int32.Parse(k.T_STORE_ID_TO.ToString())),
        //                     k.T_TRANSFER_FOR,
        //                     k.T_TRANSFER_DELIVERY_METHOD,
        //                     k.T_STATUS,
        //                     k.T_SUPPLIER_ID,
        //                     k.T_SUPPLIER_NAME,
        //                     k.T_CURRENCY_ID,
        //                     k.T_CURRENCY_NAME,
        //                     k.T_ITEM_ID,
        //                     k.T_ITEM_NAME,
        //                     k.Transfer_Quantity,
        //                     k.T_UOM_ID,
        //                     k.T_UM_NAME,
        //                     k.T_RECEIVE_TYPE,
        //                     k.T_UNIT_VALUE,
        //                     k.TotalAmount,
        //                     k.T_OTHERS,
        //                     k.T_TRANSFER_PRORITY
        //                 }).ToList();

        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //    return query;
        //}

        public IEnumerable GetReceiveBy(int T_RCV_TO_STR_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {
                query = (from t30 in obj.T74030
                         join t44 in obj.T74044 on t30.T_STORE_ID equals t44.T_STORE_ID
                         join t15 in obj.T74015 on t44.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                         join t04_Emp in obj.T74004 on t15.T_EMP_ID equals t04_Emp.T_EMP_ID
                         join t05_Type in obj.T74005 on t04_Emp.T_EMP_TYP_ID equals t05_Type.T_EMP_TYP_ID

                         where t30.T_PUR_ID == T_RCV_TO_STR_ID

                         // t30 by t30.T_STORE_ID into id
                         select new
                         {
                             T_STORE_ID = t30.T_STORE_ID,
                             T_EMP_ID = t04_Emp.T_EMP_ID,
                             T_FIRST_LANG2_NAME = t04_Emp.T_FIRST_LANG2_NAME,
                             T_LANG2_NAME = t05_Type.T_LANG2_NAME,
                         }).ToList().GroupBy(x => x.T_STORE_ID).FirstOrDefault();
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

        public bool SaveData(List<T74080> t74080, T74084 t74084, List<T74085> t74085, List<T74027> t74027)
        {
            try
            {
                if (t74084.T_TRNS_RCV_ID == 0)
                {
                    //var check = obj.T74084.Where(x => x.T_TRANSFER_REQ_ID == t74084.T_TRANSFER_REQ_ID).FirstOrDefault();
                    t74084.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                    t74084.T_ENTRY_DATE = DateTime.Now;
                    obj.T74084.Add(t74084);
                    Save();
                }
                else
                {

                }

                int T_TRNS_RCV_ID = obj.T74084.Max(a => a.T_TRNS_RCV_ID); //Where(m => m.T_PR_RCV_ID == t74060.T_PR_RCV_ID)
                foreach (var d in t74085)
                {
                    if (d.T_TRNS_QTY != 0)
                    {
                        // var t85 = obj.T74085.Max(a => a.T_TRNS_RCV_DTL_ID);
                        //  var t85 = obj.T74085.Max(a => a.T_TRNS_RCV_DTL_ID != null ? a.T_TRNS_RCV_DTL_ID : 0);
                        var t85 = obj.T74085.Count();
                        // var b = t85 != null ? t85 : 0;
                        if (t85 == 0)
                        {
                            d.T_TRNS_RCV_DTL_ID = t85 + 1;
                            d.T_TRNS_RCV_ID = T_TRNS_RCV_ID;
                            d.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                            d.T_ENTRY_DATE = DateTime.Now;
                            obj.T74085.Add(d);
                            Save();
                        }
                        else
                        {
                            var max = obj.T74085.Max(a => a.T_TRNS_RCV_DTL_ID);
                            d.T_TRNS_RCV_DTL_ID = max + 1;
                            d.T_TRNS_RCV_ID = T_TRNS_RCV_ID;
                            d.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                            d.T_ENTRY_DATE = DateTime.Now;
                            obj.T74085.Add(d);
                            Save();
                        }

                    }

                }

                foreach (var i in t74080)
                {
                    if (i.T_TRANSFER_REQ_ID != 0)
                    {
                        var abc = obj.T74080.Where(x => x.T_TRANSFER_REQ_ID == i.T_TRANSFER_REQ_ID).FirstOrDefault();
                        abc.T_STATUS = i.T_STATUS;
                        abc.T_STATUS = "1";
                        Save();
                    }
                }
                //Int64? t81 = obj.T74081.Count() > 0 ? obj.T74081.Max(a => a.T_TRANSFER_REQ_DTL_ID) : 0;

                //T74081 detailObject = new T74081();
                //detailObject.T_TRANSFER_REQ_DTL_ID = Convert.ToInt64(t81 != 0 ? t81 + 1 : 1);

                //foreach (var i in t74031)
                //{
                //    if (i.T_PUR_DTL_ID != 0)
                //    {
                //        var pur_product = obj.T74031.Where(x => x.T_PUR_DTL_ID == i.T_PUR_DTL_ID).FirstOrDefault();
                //        pur_product.T_RCV_QTY = i.T_RCV_QTY;
                //        pur_product.T_ISSUE_STS_DTL = i.T_ISSUE_STS_DTL;
                //        Save();
                //    }
                //}

                //if (t74030.T_PUR_ID == 0)
                //{

                //}
                //else
                //{
                //    var check = obj.T74030.Where(x => x.T_PUR_ID == t74030.T_PUR_ID).FirstOrDefault();
                //    check.T_ISSUE_STATUS = t74030.T_ISSUE_STATUS;
                //    Save();
                //}

                foreach (var T in t74027)
                {
                    if (T.T_UNIT_VALUE != 0 && T.T_PURCHASE_QTY != 0)
                    {
                        //var rawQuery = obj.Database.SqlQuery<int>(
                        //    "select T_PR_RCV_DTL_ID from T74060 inner join T74061 on T74060.T_PR_RCV_ID = T74061.T_PR_RCV_ID where T74061.T_ITEM_ID = '" +
                        //    T.T_ITEM_ID + "' and T74060.T_PR_RCV_ID = '" + T_PR_RCV_ID + "' and T74061.T_UOM_ID = '" +
                        //    T.T_ITEM_UM_ID + "'").First();
                        //var nextVal = Convert.ToInt32(rawQuery);
                        //T.T_PR_RCV_DTL_ID = nextVal;
                        T.T_CUR_STOCK_ID =
                            Convert.ToInt32(obj.Database.SqlQuery<int>("SELECT T74027_SEQ.NEXTVAL FROM DUAL").First());
                        obj.T74027.Add(T);
                        Save();
                    }

                    //}

                    Save();
                }
                
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

            return true;
        }

        public IEnumerable GetTransferData(int T_TRANSFER_REQ_ID)

        {

            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t80 in obj.T74080
                         from t81 in obj.T74081.Where(x => x.T_TRANSFER_REQ_ID == t80.T_TRANSFER_REQ_ID)
                         from t73 in obj.T74073.Where(x => (x.T_ID == t81.T_ITEM_ID)).DefaultIfEmpty()
                         from t45 in obj.T74045.Where(x => (x.T_SUPPLIER_ID == t80.T_SUPPLIER_ID)).DefaultIfEmpty()
                         from t16 in obj.T74016.Where(x => (x.T_CURRENCY_ID == t80.T_CURRENCY_ID)).DefaultIfEmpty()
                         from t03 in obj.T74003.Where(x => (x.T_ITEM_UM_ID == t81.T_UOM_ID)).DefaultIfEmpty()
                         where (t80.T_TRANSFER_REQ_ID == T_TRANSFER_REQ_ID)
                         group new { t80, t81, t45, t16, t73, t03 } by new
                         {
                             t80.T_TRANSFER_REQ_ID,
                             t80.T_TRANSFER_REQ_NO,
                             t80.T_ISSUE_ID,
                             t80.T_REQ_DATE,
                             t80.T_SEND_TO_STORE_ID,
                             t80.T_REQ_SET_BY_STORE_ID,
                             t80.T_TRANSFER_FOR,
                             t80.T_TRANSFER_DELIVERY_METHOD,
                             t80.T_STATUS,
                             t80.T_SUPPLIER_ID,
                             T_SUPPLIER_NAME = t45.T_LANG2_NAME,
                             t80.T_CURRENCY_ID,
                             T_CURRENCY_NAME = t16.T_LANG2_NAME,
                             t81.T_ITEM_ID,
                             T_ITEM_NAME = t73.T_LANG2_NAME,
                             //t81.T_TRANSFER_QTY,
                             t81.T_UOM_ID,
                             T_UM_NAME = t03.T_LANG2_NAME,
                             t81.T_RECEIVE_TYPE,
                             t81.T_UNIT_VALUE,
                             //t81.T_TOTAL_VALUE,
                             t81.T_OTHERS,
                             t81.T_TRANSFER_PRORITY
                             //t81.T_TRANSFER_REQ_DTL_ID,
                         }
                    into i
                         select new
                         {
                             i.Key.T_TRANSFER_REQ_ID,
                             i.Key.T_TRANSFER_REQ_NO,
                             i.Key.T_ISSUE_ID,
                             i.Key.T_REQ_DATE,
                             T_STORE_ID_TO = i.Key.T_SEND_TO_STORE_ID,
                             T_STORE_ID_FROM = i.Key.T_REQ_SET_BY_STORE_ID,
                             i.Key.T_TRANSFER_FOR,
                             i.Key.T_TRANSFER_DELIVERY_METHOD,
                             i.Key.T_STATUS,
                             i.Key.T_SUPPLIER_ID,
                             i.Key.T_SUPPLIER_NAME,
                             i.Key.T_CURRENCY_ID,
                             i.Key.T_CURRENCY_NAME,
                             i.Key.T_ITEM_ID,
                             i.Key.T_ITEM_NAME,
                             //i.Key.T_TRANSFER_QTY,
                             Transfer_Quantity = i.Sum(a => a.t81.T_TRANSFER_QTY),
                             i.Key.T_UOM_ID,
                             i.Key.T_UM_NAME,
                             i.Key.T_RECEIVE_TYPE,
                             i.Key.T_UNIT_VALUE,
                             //i.Key.T_TOTAL_VALUE,
                             TotalAmount = i.Sum(b => b.t81.T_TOTAL_VALUE),
                             i.Key.T_OTHERS,
                             i.Key.T_TRANSFER_PRORITY
                             //i.Key.T_TRANSFER_REQ_DTL_ID

                         }).AsEnumerable().Select(k => new
                         {
                             k.T_TRANSFER_REQ_ID,
                             k.T_TRANSFER_REQ_NO,
                             k.T_ISSUE_ID,
                             k.T_REQ_DATE,
                             k.T_STORE_ID_FROM,
                             T_STORE_FROM_LANG2 = GetStoreName(Int32.Parse(k.T_STORE_ID_FROM.ToString())),
                             k.T_STORE_ID_TO,
                             T_STORE_TO_LANG2 = GetStoreName(Int32.Parse(k.T_STORE_ID_TO.ToString())),
                             k.T_TRANSFER_FOR,
                             k.T_TRANSFER_DELIVERY_METHOD,
                             k.T_STATUS,
                             k.T_SUPPLIER_ID,
                             k.T_SUPPLIER_NAME,
                             k.T_CURRENCY_ID,
                             k.T_CURRENCY_NAME,
                             k.T_ITEM_ID,
                             k.T_ITEM_NAME,
                             k.Transfer_Quantity,
                             k.T_UOM_ID,
                             k.T_UM_NAME,
                             k.T_RECEIVE_TYPE,
                             k.T_UNIT_VALUE,
                             k.TotalAmount,
                             k.T_OTHERS,
                             k.T_TRANSFER_PRORITY
                             //k.T_TRANSFER_REQ_DTL_ID,
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
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
