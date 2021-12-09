using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74116Repository: IT74116
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74116Repository(AmbucareContainer _obj)
        {
            obj.Configuration.ProxyCreationEnabled = false;
            this.obj = _obj;

        }
        public IEnumerable getPursData( int Pur_ID)
        {

            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t74030 in obj.T74030
                    join t54_company in obj.T74054 on t74030.T_COMPANY_ID equals t54_company.T_COMPANY_ID
                    join t45_Supplier in obj.T74045 on t74030.T_SUPPLIER_ID equals t45_Supplier.T_SUPPLIER_ID
                    join t44_Store in obj.T74044 on t74030.T_STORE_ID equals t44_Store.T_STORE_ID
                    join t16_Currency in obj.T74016 on t74030.T_CURRENCY_ID equals t16_Currency.T_CURRENCY_ID
                     where t74030.T_PUR_ID == Pur_ID
                         //from Pa in Patient.DefaultIfEmpty()
                         select new
                    {
                        T_PUR_ID= t74030.T_PUR_ID,
                        T_COMPANY_ID= t74030.T_COMPANY_ID,
                        T_SUPPLIER_ID= t74030.T_SUPPLIER_ID,
                        T_STORE_ID=  t74030.T_STORE_ID,
                        T_CURRENCY_ID=  t74030.T_CURRENCY_ID,
                        T_PUR_REQ_DATE =t74030.T_PUR_REQ_DATE,
                        Com_T_LANG2_NAME =  t54_company.T_LANG2_NAME,
                        Su_T_LANG2_NAME =  t45_Supplier.T_LANG2_NAME,
                        Sto_T_LANG2_NAME=t44_Store.T_LANG2_NAME,
                        Cur_T_LANG2_NAME= t16_Currency.T_LANG2_NAME
                       
                         }
                ).ToList();

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

        public IEnumerable GetUom(int T_ITEM_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query =// obj.T74030.Join(obj.T74031, t30 => t30.T_PUR_ID, t31 => t31.T_PUR_ID,
                       // (t30, t31) => new {t30, t31})
                    //.Join(obj.T74073, t30_t31 => t30_t31.t31.T_ITEM_ID, t73 => t73.T_ID,
                       // (t30_t31, t73) => new {t30_t31, t73})
                obj.T74073.Join(obj.T74072,  t73=>t73.T_COST_TYPE_ID, t72 => t72.T_COST_TYPE_ID,
                        (t73, t72) => new {t73, t72})
                    .Join(obj.T74003, t73_t72 => t73_t72.t72.T_COST_TYPE_ID, t03 => t03.T_TYPE_ID,
                        (t73_t72, t03) => new {t73_t72, t03})
                    .Where(x => x.t73_t72.t73.T_ID == T_ITEM_ID).Select(m => new
                    {
                        T_LANG2_NAME = m.t03.T_LANG2_NAME,
                        T_ITEM_UM_ID = m.t03.T_ITEM_UM_ID,
                    }).ToList();
                //query = obj.T74003.Join(obj.T74072, t03 => t03.T_TYPE_ID, t72 => t72.T_COST_TYPE_ID,
                //    (t03, t72) => new { t03, t72 }).Where(x=>x.).Select(a => new
                //{
                //    T_LANG2_NAME = a.t03.T_LANG2_NAME,
                //    T_ITEM_UM_ID = a.t03.T_ITEM_UM_ID
                //}).ToList();
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
        public IEnumerable getreceiveBy(int store_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t30 in obj.T74030
                         join t44 in obj.T74044 on t30.T_STORE_ID equals t44.T_STORE_ID
                         join t15 in obj.T74015 on t44.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                         join  t04_Emp in obj.T74004 on t15.T_EMP_ID equals t04_Emp.T_EMP_ID
                         join t05_Type in obj.T74005 on t04_Emp.T_EMP_TYP_ID equals t05_Type.T_EMP_TYP_ID
                        
                         where t30.T_PUR_ID == store_ID 
                         
                         // t30 by t30.T_STORE_ID into id
                         select new 
                    {
                        T_STORE_ID =  t30.T_STORE_ID,
                        T_EMP_ID = t04_Emp.T_EMP_ID,
                        T_FIRST_LANG2_NAME=  t04_Emp.T_FIRST_LANG2_NAME,
                        T_LANG2_NAME=  t05_Type.T_LANG2_NAME,
                    }).ToList().GroupBy(x=>x.T_STORE_ID).FirstOrDefault();
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

        public IEnumerable getpurProduct(int pur_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t30_Pur in obj.T74030
                    join t31_PurDe in obj.T74031 on t30_Pur.T_PUR_ID equals t31_PurDe.T_PUR_ID
                    join t73_Item in obj.T74073 on t31_PurDe.T_ITEM_ID equals t73_Item.T_ID
                    join t03_Um in obj.T74003 on t31_PurDe.T_ITEM_UM_ID equals t03_Um.T_ITEM_UM_ID
                   // join t89_price in obj.T74089 on t73_Item.T_COST_TYPE_DTL_ID equals t89_price.T_COST_TYPE_DTL_ID into price
                   // from a in price.DefaultIfEmpty() 
                         where t30_Pur.T_PUR_ID == pur_ID 
                         select new
                    {
                        T_PUR_ID = t30_Pur.T_PUR_ID,
                        T_PUR_DTL_ID = t31_PurDe.T_PUR_DTL_ID,
                        T_ITEM_ID = t31_PurDe.T_ITEM_ID,
                        T_LANG2_NAME= t73_Item.T_LANG2_NAME,
                        T_ISSUE_STS_DTL = t31_PurDe.T_ISSUE_STS_DTL,
                        T_UNIT_VALUE = 0,
                        Issue =0,
                        T_TOTAL_VALUE = 0,
                        
                        Reject = t31_PurDe.T_ISSUE_STS_DTL != "Reject" ? t31_PurDe.T_ISSUE_STS_DTL : "1",
                             T_PUR_QTY = t31_PurDe.T_PUR_QTY,
                        T_RCV_QTY = t31_PurDe.T_RCV_QTY !=null? t31_PurDe.T_RCV_QTY:0,
                        T_PRIORITY =  t31_PurDe.T_PRIORITY,
                        T_ITEM_UM_ID = t31_PurDe.T_ITEM_UM_ID,
                       // Issue = t31_PurDe.T_PUR_QTY - T_RCV_QTY,
                        UM_T_LANG2_NAME =  t03_Um.T_LANG2_NAME
                         }). AsEnumerable().Select((r, i) => new
                {
                    T_PUR_ID = r.T_PUR_ID,
                    T_PUR_DTL_ID = r.T_PUR_DTL_ID,
                    T_ITEM_ID = r.T_ITEM_ID,
                    T_LANG2_NAME =r.T_LANG2_NAME,
                    T_ISSUE_STS_DTL =r.T_ISSUE_STS_DTL,
                    T_UNIT_VALUE = Price(r.T_ITEM_ID, r.T_ITEM_UM_ID),
                    Issue = r.T_PUR_QTY - r.T_RCV_QTY,
                    T_TOTAL_VALUE = Price(r.T_ITEM_ID, r.T_ITEM_UM_ID)* (r.T_PUR_QTY - r.T_RCV_QTY),
                    Reject = r.Reject,
                    T_PUR_QTY =r.T_PUR_QTY,
                    T_RCV_QTY =r.T_RCV_QTY,
                    T_PRIORITY =r.T_PRIORITY,
                    T_ITEM_UM_ID =r.T_ITEM_UM_ID,
                    UM_T_LANG2_NAME=r.UM_T_LANG2_NAME
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

        public decimal Price(int? T_ITEM_ID, int? T_ITEM_UM_ID)
        {
            //IEnumerable query = Enumerable.Empty<object>();
            decimal query = 0;   //decimal.Empty<object>();
            try
            {
                query = obj.Database.SqlQuery<decimal>(" SELECT   nvl(fn_Sale_Price("+ T_ITEM_ID + ","+ T_ITEM_UM_ID + "),0) from dual ").First();
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



        public bool SaveData(T74060 t74060, List<T74061> t74061, List<T74031> t74031, T74030 t74030, List<T74027> t74027)
        {
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    if (t74060.T_PR_RCV_ID == 0)
                    {
                        t74060.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                        t74060.T_ENTRY_DATE = DateTime.Now;
                        obj.T74060.Add(t74060);
                        Save();
                    }
                    else
                    {

                    }
                    int T_PR_RCV_ID =
                        obj.T74060.Max(a => a.T_PR_RCV_ID); //Where(m => m.T_PR_RCV_ID == t74060.T_PR_RCV_ID)
                    foreach (var d in t74061)
                    {
                        if (d.T_RCV_QTY != 0 && d.T_UNIT_VALUE != 0)
                        {
                            int t61 = Convert.ToInt32(obj.T74061.Max(a => a.T_PR_RCV_DTL_ID));
                            d.T_PR_RCV_DTL_ID = t61 + 1;
                            d.T_PR_RCV_ID = T_PR_RCV_ID;
                            d.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                            d.T_ENTRY_DATE = DateTime.Now;
                            obj.T74061.Add(d);
                            Save();
                        }

                    }

                    foreach (var i in t74031)
                    {
                        if (i.T_PUR_DTL_ID != 0)
                        {
                            var pur_product = obj.T74031.Where(x => x.T_PUR_DTL_ID == i.T_PUR_DTL_ID).FirstOrDefault();
                            pur_product.T_RCV_QTY = i.T_RCV_QTY;
                            pur_product.T_ISSUE_STS_DTL = i.T_ISSUE_STS_DTL;
                            Save();
                        }
                    }

                    if (t74030.T_PUR_ID == 0)
                    {

                    }
                    else
                    {
                        var check = obj.T74030.Where(x => x.T_PUR_ID == t74030.T_PUR_ID).FirstOrDefault();
                        check.T_ISSUE_STATUS = t74030.T_ISSUE_STATUS;
                        Save();
                    }

                    foreach (var T in t74027)
                    {
                        if (T.T_UNIT_VALUE != 0 && T.T_PURCHASE_QTY != 0)
                        {
                            var rawQuery = obj.Database
                                .SqlQuery<int>(
                                    "select T_PR_RCV_DTL_ID from T74060 inner join T74061 on T74060.T_PR_RCV_ID = T74061.T_PR_RCV_ID where T74061.T_ITEM_ID = '" +
                                    T.T_ITEM_ID + "' and T74060.T_PR_RCV_ID = '" + T_PR_RCV_ID +
                                    "' and T74061.T_UOM_ID = '" + T.T_ITEM_UM_ID + "'").First();
                            var nextVal = Convert.ToInt32(rawQuery);
                            T.T_PR_RCV_DTL_ID = nextVal;
                            T.T_CUR_STOCK_ID = Convert.ToInt32(obj.Database
                                .SqlQuery<int>("SELECT T74027_SEQ.NEXTVAL FROM DUAL").First());
                            obj.T74027.Add(T);
                            Save();
                        }

                    }
                    dbContextTransaction.Commit();
                    // Save();
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbContextTransaction.Rollback();
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
            return true;
        }
        public int GetNextSequenceValue()
        {
            var rawQuery = obj.Database.SqlQuery<int>("SELECT T74027_SEQ.NEXTVAL FROM DUAL").First();
            var nextVal = Convert.ToInt32(rawQuery);

            return nextVal;
        }
        //SaveData(T74060 t74060, List<T74061> t74061)
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