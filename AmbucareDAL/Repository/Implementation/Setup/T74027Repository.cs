using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74027Repository : IT74027
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74027Repository(AmbucareContainer _Obj)
        {
            //_Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        //dropdownlist
        public IQueryable<T74054> GetCompanysData
        {
            get { return obj.T74054; }
        }

        //public IQueryable<T74001> GetItemsData
        // {
        //     get { return obj.T74001; }
        // }

        public IEnumerable GetProductItem(int id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from item in obj.T74073
                         join product in obj.T74072 on item.T_COST_TYPE_ID equals product.T_COST_TYPE_ID
                         where product.T_COST_TYPE_ID == id
                         select new
                         {
                             T_ITEM_ID = item.T_ID,
                             T_LANG2_NAME = item.T_LANG2_NAME
                         }).AsEnumerable().Select((r, i) => new
                         {
                             //T_ITEM_ID = r.T_ITEM_ID,
                             //T_LANG2_NAME = r.T_LANG2_NAME
                             r.T_ITEM_ID,
                             r.T_LANG2_NAME
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

        public IEnumerable GetStoresData(string zone)
        {
            //var query = obj.T74044
            //    .GroupJoin(obj.T74014, t44 => t44.T_AMBU_REG_ID, t14 => t14.T_AMBU_REG_ID, (t44, t14) => new { t44, t14 })
            //    .SelectMany(t44_t14 => t44_t14.t14.DefaultIfEmpty(), (t44_t14, t14) => new
            //    { t44_t14, t14 })
            //    .GroupJoin(obj.T74011, t44_t14_t14 => t44_t14_t14.t14.T_ITEM_COLOR_ID, t11 => t11.T_ITEM_COLOR_ID,
            //        (t44_t14_t14, t11) => new { t44_t14_t14, t11 })
            //    .SelectMany(t44_t14_t14_t11 => t44_t14_t14_t11.t11.DefaultIfEmpty(),
            //        (t44_t14_t14_t11, t11) => new { t44_t14_t14_t11, t11 })
            //    .GroupJoin(obj.T74002,
            //        t44_t14_t14_t11_t11 => t44_t14_t14_t11_t11.t44_t14_t14_t11.t44_t14_t14.t14.T_BRAND_ID,
            //        t02 => t02.T_ITEM_BRA_ID, (t44_t14_t14_t11_t11, t02) => new { t44_t14_t14_t11_t11, t02 })
            //    .SelectMany(t44_t14_t14_t11_t11_t02 => t44_t14_t14_t11_t11_t02.t02.DefaultIfEmpty(),
            //        (t44_t14_t14_t11_t11_t02, t02) => new
            //        {
            //            T_STORE_ID = t44_t14_t14_t11_t11_t02.t44_t14_t14_t11_t11.t44_t14_t14_t11.t44_t14_t14.t44_t14.t44
            //                .T_STORE_ID,
            //            T_AMBU_REG_ID = t44_t14_t14_t11_t11_t02.t44_t14_t14_t11_t11.t44_t14_t14_t11.t44_t14_t14.t44_t14
            //                .t44.T_AMBU_REG_ID,
            //            Central_Lang2 = t44_t14_t14_t11_t11_t02.t44_t14_t14_t11_t11.t44_t14_t14_t11.t44_t14_t14.t44_t14
            //                .t44.T_LANG2_NAME,
            //            T_YEAR_MODEL = t44_t14_t14_t11_t11_t02.t44_t14_t14_t11_t11.t44_t14_t14_t11.t44_t14_t14.t14
            //                .T_YEAR_MODEL,
            //            T_SERIES = t44_t14_t14_t11_t11_t02.t44_t14_t14_t11_t11.t44_t14_t14_t11.t44_t14_t14.t14.T_SERIES,

            //            Brand_Lang2 = t02.T_LANG2_NAME,
            //            Color_Lang2 = t44_t14_t14_t11_t11_t02.t44_t14_t14_t11_t11.t11.T_LANG2_NAME

            //        }).AsEnumerable().Select(k => new
            //        {
            //            T_STORE_ID = k.T_STORE_ID,
            //            T_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : "("+k.T_AMBU_REG_ID +")"+ k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
            //        }).ToList();


           var query = (from t44 in obj.T74044
                join t15 in obj.T74015 on t44.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                join t04 in obj.T74004 on t15.T_EMP_ID equals t04.T_EMP_ID
                join t57 in obj.T74057 on t04.T_EMP_ID equals t57.T_EMP_ID
                     where t04.T_EMP_TYP_ID==21 && t57.T_ZONE_CODE== zone
                     select new
                {
                    t44.T_STORE_ID,
                    T_LANG2_NAME=t57.T_USER_ID
                     }).ToList();
                     
            return query;
        }

        public IEnumerable GetSupplierData()
        {
            return obj.T74045.Where(s => s.T_LANG2_NAME != null).Select(m => new { m.T_LANG2_NAME, m.T_SUPPLIER_ID }).ToList();
        }

        public IEnumerable GetGridAllData(int T_STORE_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from item in obj.T74027
                         from itemname in obj.T74073.Where(x => x.T_ID == item.T_ITEM_ID)
                         from typname in obj.T74072.Where(x => (x.T_COST_TYPE_ID == itemname.T_COST_TYPE_ID))
                         from suppliers in obj.T74045.Where(x => x.T_SUPPLIER_ID == item.T_SUPPLIER_ID).DefaultIfEmpty() //Left Join that is causing an exception
                         from um in obj.T74003.Where(x => x.T_ITEM_UM_ID == item.T_ITEM_UM_ID).DefaultIfEmpty()
                         where (item.T_STORE_ID == T_STORE_ID)
                         select new
                         {
                             Batch = item.T_BATCH,
                             Lot_no = item.T_LOT_NO,
                             stock_date = item.T_STOCK_DATE,
                             T_SUPPLIER_ID = item.T_SUPPLIER_ID,
                             T_LANG2_NAME = suppliers.T_LANG2_NAME,
                             T_CUR_STOCK_ID = item.T_CUR_STOCK_ID,
                             Type_Id = typname.T_COST_TYPE_ID,
                             Type_Name = typname.T_LANG2_NAME,
                             T_ITEM_ID = itemname.T_ID,
                             itemName = itemname.T_LANG2_NAME,
                             UM_id = item.T_ITEM_UM_ID,
                             UM_Name = um.T_LANG2_NAME,
                             T_MF_DATE = item.T_MF_DATE,
                             T_EXPIRE_DATE = item.T_EXPIRE_DATE,
                             T_UNIT_VALUE = item.T_UNIT_VALUE,
                             T_TRNSACTION_QTY = item.T_TRNSACTION_QTY,
                             T_PURCHASE_QTY = item.T_PURCHASE_QTY,
                             T_TOTAL_VALUE = item.T_TOTAL_VALUE
                         }).ToList();

                //query = (from item in obj.T74027
                //         join itemname in obj.T74073 on item.T_ITEM_ID equals itemname.T_ID
                //         join typname in obj.T74072 on itemname.T_COST_TYPE_ID equals typname.T_COST_TYPE_ID
                //         join suppliers in obj.T74045 on item.T_SUPPLIER_ID equals suppliers.T_SUPPLIER_ID
                //         join um in obj.T74003 on item.T_ITEM_UM_ID equals um.T_ITEM_UM_ID into uomm
                //         from um in uomm.DefaultIfEmpty()
                //         where item.T_COMPANY_ID == T_COMPANY_ID && item.T_STORE_ID == T_STORE_ID
                //         select new
                //         {

                //             Batch = item.T_BATCH,
                //             Lot_no = item.T_LOT_NO,
                //             stock_date = item.T_STOCK_DATE,
                //             T_SUPPLIER_ID = suppliers.T_SUPPLIER_ID,
                //             T_LANG2_NAME = suppliers.T_LANG2_NAME,
                //             T_CUR_STOCK_ID = item.T_CUR_STOCK_ID,
                //             Type_Id = typname.T_COST_TYPE_ID,
                //             Type_Name = typname.T_LANG2_NAME,
                //             T_ITEM_ID = itemname.T_ID,
                //             itemName = itemname.T_LANG2_NAME,
                //             UM_id = um.T_ITEM_UM_ID,
                //             UM_Name = um.T_LANG2_NAME,
                //             T_MF_DATE = item.T_MF_DATE,
                //             T_EXPIRE_DATE = item.T_EXPIRE_DATE,
                //             T_UNIT_VALUE = item.T_UNIT_VALUE,
                //             T_TRNSACTION_QTY = item.T_TRNSACTION_QTY,
                //             T_PURCHASE_QTY = item.T_PURCHASE_QTY,
                //             T_TOTAL_VALUE = item.T_TOTAL_VALUE
                //         }).ToList();

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

        public IQueryable<T74072> GetProductTypeData
        {
            get { return obj.T74072; }
        }

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

        public IEnumerable GetUom(int T_TYPE_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74003.Join(obj.T74072, t03 => t03.T_TYPE_ID, t72 => t72.T_COST_TYPE_ID,
                (t03, t72) => new { t03, t72 }).Where(x => x.t03.T_TYPE_ID == T_TYPE_ID).Select(a => new
                {
                    UomName = a.t03.T_LANG2_NAME,
                    T_ITEM_UM_ID = a.t03.T_ITEM_UM_ID
                }).ToList();
            return query;
        }

        public int GetNextSequenceValue()
        {
            var rawQuery = obj.Database.SqlQuery<int>("SELECT T74027_SEQ.NEXTVAL FROM DUAL").First();
            var nextVal = Convert.ToInt32(rawQuery);

            return nextVal;
        }

        public void insert_T74027(List<T74027> _t74027List)
        {

            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    foreach (var _t74027 in _t74027List)
                    {
                        var check = obj.T74027.Where(p => p.T_CUR_STOCK_ID == _t74027.T_CUR_STOCK_ID).FirstOrDefault();
                        if (check == null)
                        {
                            int t27 = obj.T74027.Count() > 0 ? obj.T74027.Max(a => a.T_CUR_STOCK_ID) : 0;
                            _t74027.T_CUR_STOCK_ID = t27 != 0 ? t27 + 1 : 1;
                            obj.T74027.Add(_t74027);
                            //obj.SaveChanges();
                        }
                        else
                        {
                            var upd = obj.T74027.Where(p => p.T_CUR_STOCK_ID == _t74027.T_CUR_STOCK_ID).FirstOrDefault();
                            if (upd != null)
                            {
                                upd.T_ITEM_ID = _t74027.T_ITEM_ID;
                                upd.T_MF_DATE = _t74027.T_MF_DATE;
                                upd.T_EXPIRE_DATE = _t74027.T_EXPIRE_DATE;
                                upd.T_UNIT_VALUE = _t74027.T_UNIT_VALUE;
                                upd.T_TRNSACTION_QTY = _t74027.T_TRNSACTION_QTY;
                                upd.T_PURCHASE_QTY = _t74027.T_PURCHASE_QTY;
                                upd.T_TOTAL_VALUE = _t74027.T_TOTAL_VALUE;
                                upd.T_ITEM_UM_ID = _t74027.T_ITEM_UM_ID;
                            }

                        }
                        Save();

                        //--------------------New changes----------------
                        T74073 chk73 = obj.T74073.Where(a => a.T_ID == _t74027.T_ITEM_ID && (a.T_COST_TYPE_ID == 23 || a.T_COST_TYPE_ID == 121)).FirstOrDefault();
                        if (chk73 != null)
                        {
                            T74089 chk89 = new T74089();


                            if (chk73.T_COST_TYPE_ID == 23)
                            {
                                chk89 = obj.T74089
                                    .Where(x => x.T_COST_TYPE_DTL_ID == chk73.T_COST_TYPE_DTL_ID && x.T_ITEM_UM_ID == _t74027.T_ITEM_UM_ID).FirstOrDefault();
                                if (chk89 != null)
                                {
                                    obj.Database.ExecuteSqlCommand(
                                        "update T74089 set T_ACTIVE = 0 where T_COST_TYPE_DTL_ID = " +
                                        chk73.T_COST_TYPE_DTL_ID + " and T_ITEM_UM_ID = " + _t74027.T_ITEM_UM_ID + "");
                                }
                                //check.T_ACTIVE = "0";
                                //Save();
                                var max = obj.T74089.Count() > 0 ? obj.T74089.Max(x => x.T_PRICE_ID) + 1 : 1;
                                T74089 o89 = new T74089();
                                o89.T_PRICE_ID = max;
                                o89.T_ACTIVE = "1";
                                o89.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                o89.T_ENTRY_DATE = DateTime.Now;
                                o89.T_COST_TYPE_DTL_ID = chk73.T_COST_TYPE_DTL_ID;
                                o89.T_PUR_PRICE = 0;
                                o89.T_SALE_PRICE = 0;
                                o89.T_ITEM_UM_ID = _t74027.T_ITEM_UM_ID;
                                obj.T74089.Add(o89);
                                Save();

                            }
                            else if (chk73.T_COST_TYPE_ID == 121)
                            {
                                chk89 = obj.T74089
                                    .Where(x => x.T_COST_TYPE_DTL_ID == chk73.T_COST_TYPE_DTL_ID).FirstOrDefault();
                                if (chk89 != null)
                                {
                                    obj.Database.ExecuteSqlCommand(
                                        "update T74089 set T_ACTIVE = 0 where T_COST_TYPE_DTL_ID = " +
                                        chk73.T_COST_TYPE_DTL_ID + "");
                                }
                                //check.T_ACTIVE = "0";
                                //Save();
                                var max = obj.T74089.Count() > 0 ? obj.T74089.Max(x => x.T_PRICE_ID) + 1 : 1;
                                T74089 o89 = new T74089();
                                o89.T_PRICE_ID = max;
                                o89.T_ACTIVE = "1";
                                o89.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                o89.T_ENTRY_DATE = DateTime.Now;
                                o89.T_COST_TYPE_DTL_ID = chk73.T_COST_TYPE_DTL_ID;
                                o89.T_PUR_PRICE = 0;
                                o89.T_SALE_PRICE = 0;
                                o89.T_ITEM_UM_ID = _t74027.T_ITEM_UM_ID;
                                obj.T74089.Add(o89);
                                Save();

                            }







                        }







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
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }
                }

            }

            //Save();
        }

        public void Del_t74027(List<T74027> _t74027Del)
        {
            string msg = "";
            foreach (var _t74027 in _t74027Del)
            {
                var check = obj.T74027.Where(P => P.T_CUR_STOCK_ID == _t74027.T_CUR_STOCK_ID).FirstOrDefault();
                if (check != null)
                {
                    obj.T74027.Remove(check);
                    Save();
                }
            }

        }

        public decimal GetStockQuantity(int? ITEM_ID)
        {
            var query = obj.Database.SqlQuery<decimal>(
                    "select V_TOTAL_RCV.ITEM_ID,V_TOTAL_RCV.ITEM_NAME, (V_TOTAL_RCV.RCV_QTY - V_TOTAL_ISSUE.ISSUE_QTY) STOCK FROM V_TOTAL_RCV,V_TOTAL_ISSUE WHERE V_TOTAL_RCV.ITEM_ID = " +
                    ITEM_ID +
                    " AND V_TOTAL_RCV.ITEM_ID = V_TOTAL_ISSUE.ITEM_ID AND V_TOTAL_RCV.STORE_ID = V_TOTAL_ISSUE.STORE_ID")
                .First();
            return query;
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


//