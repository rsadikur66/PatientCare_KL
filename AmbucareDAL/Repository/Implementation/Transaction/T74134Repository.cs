using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Schema;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74134Repository: IT74134
    {
        CommonDAL common = new CommonDAL();
        private AmbucareContainer obj = new AmbucareContainer();
         private readonly T74134DAL _t74134DAL = new T74134DAL();
        public T74134Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IEnumerable GetPatInfo(int reId)
        {
            IEnumerable query = Enumerable.Empty<object>();

           var uId= HttpContext.Current.Session["T_USER_ID"].ToString();
            
            query = (from T41 in obj.T74041
                join T15 in obj.T74015 on T41.T_AMBU_REG_ID equals T15.T_AMBU_REG_ID
                join T57 in obj.T74057 on T15.T_EMP_ID equals T57.T_EMP_ID
                join T46 in obj.T74046 on T41.T_PAT_ID equals T46.T_PAT_ID
                join T50 in obj.T74050 on T46.T_SEX_CODE equals T50.T_SEX_CODE into T74050
                from T50 in T74050.DefaultIfEmpty()
                join T51 in obj.T74051 on T46.T_MRTL_STATUS equals T51.T_MRTL_STATUS_CODE into T74051
                from T51 in T74051.DefaultIfEmpty()
                join T03 in obj.T02003 on T46.T_NTNLTY_ID equals T03.T_NTNLTY_ID into T02003
                from T03 in T02003.DefaultIfEmpty()
               // join T43 in obj.T74043 on T41.T_REQUEST_ID equals T43.T_REQUEST_ID into T74043
               // from T43 in T74043.DefaultIfEmpty()
              //  join T39 in obj.T74039 on T41.T_REQUEST_ID equals T39.T_REQUEST_ID into T74039
               // from T39 in T74039.DefaultIfEmpty()

               // where T57.T_USER_ID == uId && T41.T_DISCH_STATUS == null
                where T41.T_REQUEST_ID == reId
                select new
                {
                    T_REQUEST_ID = T41.T_REQUEST_ID,
                    T_PAT_ID = T46.T_PAT_ID,
                    T_FIRST_LANG2_NAME = T46.T_FIRST_LANG2_NAME,
                    T_FATHER_LANG2_NAME = T46.T_FATHER_LANG2_NAME,
                    T_GFATHER_LANG2_NAME = T46.T_GFATHER_LANG2_NAME,
                    T_FAMILY_LANG2_NAME = T46.T_FAMILY_LANG2_NAME,
                    Gender = T50.T_LANG2_NAME,
                    Age = T46.T_BIRTH_DATE,
                    MaritalStatus = T51.T_LANG2_NAME,
                    Nationality = T03.T_LANG2_NAME,
                  //  T_HIGHT = T43 == null ? 0 : T43.T_HIGHT,
                  //  T_WEIGHT = T43 == null ? 0 : T43.T_WEIGHT,
                    T_AMBU_REG_ID = T41.T_AMBU_REG_ID,
                  //  T_REMARKS = T39.T_REMARKS,
                   // T_PRESCRIPTION_ID = T39 == null ? 0 : T39.T_PRESCRIPTION_ID
                }).ToList();
            return query;
        }

       public DataTable GetPreviousMedicine(int requId)
        {
            var data = _t74134DAL.GetPreviousMedicine(requId);
            return data;
        }
     public IEnumerable GetVitalSign(int reqId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            decimal chk = obj.T74043.Where(k => k.T_REQUEST_ID == reqId).Count();
            if (chk > 0)
            {
                var max = obj.T74043.Where(m => m.T_REQUEST_ID == reqId).Max(a => a.T_PCHECKUP_ID);
                // var uId = HttpContext.Current.Session["T_USER_ID"].ToString();
                query = (from t43 in obj.T74043
                    where t43.T_REQUEST_ID == reqId && t43.T_PCHECKUP_ID == max
                    // orderby t43.T_PCHECKUP_ID descending
                    select new
                    {
                        T_PCHECKUP_ID = t43.T_PCHECKUP_ID,
                        T_TEMP = t43.T_TEMP,
                        T_PULS = t43.T_PULS,
                        T_BP_SYS = t43.T_BP_SYS,
                        T_BP_DIA = t43.T_BP_DIA,
                        T_WEIGHT = t43.T_WEIGHT,
                        T_HIGHT = t43.T_HIGHT,
                        T_RESP = t43.T_RESP,
                        T_OS = t43.T_OS,
                        T_ECG_TEST = t43.T_ECG_TEST,
                        T_VERIFY_LEVEL = t43.T_VERIFY_LEVEL,
                        T_ENTRY_DATE = t43.T_ENTRY_DATE
                        //.ToString("yyyy-MM-dd"),a.OrderDate.Month + "/" + a.OrderDate.Day + "/" + a.OrderDate.Year
                    }).ToList();
                //    .AsEnumerable().Select((r, i) => new
                //{
                //    r.T_PCHECKUP_ID,
                //    r.T_TEMP,
                //    r.T_PULS,
                //    r.T_BP_SYS,
                //    r.T_BP_DIA,
                //    r.T_WEIGHT,
                //    r.T_HIGHT,
                //    r.T_RESP,
                //    r.T_OS,
                //    r.T_ECG_TEST,
                //    r.T_VERIFY_LEVEL,
                //    r.T_ENTRY_DATE
                //}).ToList();
            }

            return query;

          

        }

       public DataTable GetItem(int ambu,int rquestId)
       {
           //int request = obj.T74041
           //    .Where(h => h.T_AMBU_REG_ID == ambu && h.T_DISCH_STATUS == null)
           //    .Max(m => m.T_REQUEST_ID);

            var data = _t74134DAL.GetItem(ambu, rquestId);
            return data;
        }

      public  DataTable GetStockData(int ambu, int item)
        {
            var data = _t74134DAL.GetStockData(ambu, item);
            return data;
        }

        public string SaveData(string lang,int rquestId, T74036 t74036, List<T74037> t74037)
        {
            var sms = "";
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    var com = common.chkVerified(rquestId);
                    if (com)
                    {
                   
                    string user = HttpContext.Current.Session["T_USER_ID"].ToString();
                    DateTime? date = DateTime.Now;
                    decimal? grandTotal = 0;
                   
                    //T7436-----------------------------------------------------------
                   
                        t74036.T_USER_ID = user;
                        t74036.T_COMPANY_ID = Int32.Parse(HttpContext.Current.Session["T_COMPANY_ID"].ToString());
                        //  t74036.T_PAT_NO = obj.T74041.Where(a => a.T_REQUEST_ID == t74036.T_REQUEST_ID)
                        //   .Select(x => x.T_PAT_ID).First().ToString();
                        // t74036.T_PRSCRPTN_ID = obj.T74039.Where(a => a.T_REQUEST_ID == t74036.T_REQUEST_ID)
                        //   .Select(x => x.T_PRESCRIPTION_ID).First().ToString();
                        t74036.T_ENTRY_USER_ = user;
                        t74036.T_ENTRY_DATE_ = date;
                        t74036.T_ISSUE_DATE = common.dateTime();
                        t74036.T_ENTRY_TIME = common.Time();
                        t74036.T_ISSUED_BY = obj.T74057.Where(a => a.T_USER_ID == user).Select(x => x.T_EMP_ID)
                            .FirstOrDefault();
                        obj.T74036.Add(t74036);
                        Save();
                       int T_ISSUE_ID36 = obj.T74036.Where(m => m.T_ENTRY_USER_ == t74036.T_ENTRY_USER_).Max(a => a.T_ISSUE_ID);

                    //T74037-----------------------------------------------------------
                    foreach (T74037 newChild in t74037)
                    {
                        newChild.T_ISSUE_ID = T_ISSUE_ID36;

                        var productList = (from t27 in obj.T74027
                            join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                            // join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                            join t03 in obj.T74003 on t27.T_ITEM_UM_ID equals t03.T_ITEM_UM_ID
                            where t27.T_TRNSACTION_QTY > 0
                                  && t73.T_ID == newChild.T_ITEM_ID
                                  && t73.T_COST_TYPE_ID == 23
                                  // && t27.T_ITEM_UM_ID == newChild.T_UOM_ID
                                  && t27.T_STORE_ID == t74036.T_STORE_ID
                            //  && t89.T_ACTIVE == "1"
                            //  && t89.T_ITEM_UM_ID == newChild.T_UOM_ID
                            select new
                            {
                                EXP_DATE = t27.T_EXPIRE_DATE,
                                Qty = t27.T_TRNSACTION_QTY * t03.T_QTY,
                                ProdId = t27.T_ITEM_ID,
                                ItemPiece = t03.T_QTY,
                                Stock_Id = t27.T_CUR_STOCK_ID,
                                t27.T_COMPANY_ID,
                                t27.T_UNIT_VALUE,
                                t27.T_MF_DATE,
                                t27.T_PR_RCV_DTL_ID,
                                t27.T_STOCK_TYPE_ID,
                                t27.T_LOT_NO,
                                t27.T_BATCH,
                                t27.T_SUPPLIER_ID,
                                t27.T_ITEM_UM_ID,
                                t73.T_COST_TYPE_DTL_ID

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

                            decimal? slPrice = obj.T74089.Where(b => b.T_ITEM_UM_ID == 123 && b.T_ACTIVE == "1" && b.T_COST_TYPE_DTL_ID == singleObject.T_COST_TYPE_DTL_ID).Select(m => m.T_SALE_PRICE)
                                .FirstOrDefault();

                            T74037 detailObject = new T74037();
                            detailObject.T_ISSUE_DTL_ID = t37 != 0 ? t37 + 1 : 1;
                            detailObject.T_ISSUE_ID = T_ISSUE_ID36;
                            detailObject.T_ENTRY_USER = user;
                            detailObject.T_ENTRY_DATE_ = date;
                            detailObject.T_CUR_STOCK_ID = singleObject.Stock_Id;
                            detailObject.T_EXPIRE_DATE = singleObject.EXP_DATE;
                           // decimal? box36== tQuantity / singleObject.ItemPiece;
                            decimal box36 = Convert.ToDecimal(Math.Round(Convert.ToDouble(tQuantity / singleObject.ItemPiece), 2));
                            detailObject.T_QUANTITY = box36;
                            detailObject.T_TOTAL_AMOUNT = (tQuantity) * (slPrice);
                            detailObject.T_ITEM_ID = newChild.T_ITEM_ID;
                            detailObject.T_UOM_ID = singleObject.T_ITEM_UM_ID;
                            detailObject.T_SALE_PRICE = slPrice;
                            grandTotal = grandTotal+(tQuantity) * (slPrice);
                             obj.T74037.Add(detailObject);
                             Save();
                            var t27 = obj.T74027.Where(p => p.T_CUR_STOCK_ID == singleObject.Stock_Id).FirstOrDefault();
                            if (t27 != null)
                            {
                                decimal? box = t27.T_TRNSACTION_QTY - (tQuantity / singleObject.ItemPiece);
                              decimal dd  = Convert.ToDecimal(Math.Round(Convert.ToDouble(box), 2));
                                t27.T_TRNSACTION_QTY = dd;
                                 Save();
                            }
                        }
                    }

                    //T74074------------------------------------------------------------
                    T74074 newObj74 = new T74074();
                    newObj74.T_ISSUE_ID = T_ISSUE_ID36; 
                    newObj74.T_REQUEST_ID = t74036.T_REQUEST_ID; 
                    newObj74.T_TOTAL_PRICE = grandTotal;
                    newObj74.T_ENTRY_USER = user;
                    newObj74.T_ENTRY_DATE = date;
                    obj.T74074.Add(newObj74);
                    Save();
                    // int T_BILL_ID = obj.T74074.Count() > 0 ? obj.T74074.Where(m => m.T_ENTRY_USER == t74074.T_ENTRY_USER).Max(a => a.T_BILL_ID) : 1;

                    //T74036------------------------------------------------------------
                    var chk36 = obj.T74036.Where(n => n.T_ISSUE_ID == T_ISSUE_ID36).FirstOrDefault();
                    if (chk36!=null)
                    {
                        chk36.T_TOTAL = grandTotal;
                        chk36.T_GRAND_TOTAL = grandTotal;
                         Save();
                        sms = common.GetSingleMsg(lang, "S0012");
                        }
                    dbContextTransaction.Commit();

                    }
                    else
                    {
                        sms = common.GetSingleMsg(lang, "S0053");
                    }



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
                return sms;
            }

            

        }

       public DataTable GetServiceData(int requId)
        {
            var data = _t74134DAL.GetServiceData(requId);
            return data;

            //IEnumerable query = Enumerable.Empty<object>();
            //query = (from t27 in obj.T74027
            //    join t44 in obj.T74044 on t27.T_STORE_ID equals t44.T_STORE_ID
            //    join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
            //    join t41 in obj.T74041 on t44.T_AMBU_REG_ID equals t41.T_AMBU_REG_ID
            //    join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
            //    where t73.T_COST_TYPE_ID == 121 && t41.T_REQUEST_ID == requId && t89.T_ACTIVE == "1"
            //    select new
            //    {
            //        t73.T_LANG2_NAME,
            //        T_PRICE = t89.T_SALE_PRICE,
            //        t89.T_COST_TYPE_DTL_ID
            //    }).ToList();
            //return query;

        }

       public string SaveServiceData(string lang, int rquestId, T74074 t74074, List<T74079> t74079)
       {
           var sms = "";
           int T_BILL_DTL_ID = 0;
            var user = HttpContext.Current.Session["T_USER_ID"].ToString();
           using (var dbContextTransaction = obj.Database.BeginTransaction())
           {
               try
               {
                   var com = common.chkVerified(rquestId);
                   if (com)
                   {
                       
                   

                        t74074.T_ENTRY_USER = user;
                   t74074.T_ENTRY_DATE = DateTime.Now;
                   obj.T74074.Add(t74074);
                   Save();
                   int T_BILL_ID = obj.T74074.Where(m => m.T_ENTRY_USER == t74074.T_ENTRY_USER).Max(a => a.T_BILL_ID);

                   foreach (var j in t74079)
                   {
                       T_BILL_DTL_ID = obj.T74079.Count() > 0 ? obj.T74079.Max(a => a.T_BILL_DTL_ID) : 0;
                       T74079 dd = new T74079();
                       dd.T_BILL_DTL_ID = T_BILL_DTL_ID != 0 ? T_BILL_DTL_ID + 1 : 1;
                       dd.T_ENTRY_USER = user;
                       dd.T_BILL_ID = T_BILL_ID; 
                       dd.T_COST_TYPE_DTL_ID = j.T_COST_TYPE_DTL_ID; 
                       dd.T_PRICE = j.T_PRICE; 
                       dd.T_ENTRY_DATE = DateTime.Now;
                       dd.T_ENTRY_TIME = common.dateTime();
                       obj.T74079.Add(dd);
                       Save();
                       //T_BILL_DTL_ID = obj.T74079.Where(m => m.T_ENTRY_USER == user).Max(a => a.T_BILL_DTL_ID);
                    }
                        // Save();
                       sms = common.GetSingleMsg(lang, "S0012");
                        dbContextTransaction.Commit();

                   }
                   else
                   {
                       sms = common.GetSingleMsg(lang, "S0053");
                    }

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
          

            return sms;
       }
        
        public void Dispose()
        {
            obj.Dispose();
        }
        public void Save()
        {
            obj.SaveChanges();
        }
    }
}