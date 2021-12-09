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
    public class T74120Repository : IT74120
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74120Repository(AmbucareContainer _Obj)
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

        public string Insert_T74120(T74080 t74080, List<T74081> t74081List)
        {
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    t74080.T_TRANSFER_REQ_NO = CommonClass.General_Code("TI");
                    string user = HttpContext.Current.Session["T_USER_ID"].ToString();
                    DateTime? date = DateTime.Now;

                    //T74080-----------------------------------------------------------

                    t74080.T_ENTRY_USER = user;
                    t74080.T_ENTRY_DATE = date;
                    obj.T74080.Add(t74080);
                    Save();

                    int T_TRANSFER_REQ_ID = obj.T74080.Where(m => m.T_ENTRY_USER == t74080.T_ENTRY_USER).Max(a => a.T_TRANSFER_REQ_ID);
                    //T74081-----------------------------------------------------------
                    foreach (T74081 newChild in t74081List)
                    {
                        newChild.T_TRANSFER_REQ_ID = T_TRANSFER_REQ_ID;

                        var productList = (from t27 in obj.T74027
                                           join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                                           join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                                           where t89.T_ACTIVE == "1"
                                           join t80 in obj.T74080 on newChild.T_TRANSFER_REQ_ID equals t80.T_TRANSFER_REQ_ID
                                           where t27.T_TRNSACTION_QTY > 0
                                           && t73.T_ID == newChild.T_ITEM_ID
                                           && t73.T_COST_TYPE_ID == 23
                                           && t27.T_ITEM_UM_ID == newChild.T_UOM_ID
                                           && t27.T_STORE_ID == t80.T_REQ_SET_BY_STORE_ID
                                           group new { t27, t89 } by new { t27.T_EXPIRE_DATE, t27.T_TRNSACTION_QTY, t27.T_ITEM_ID, t27.T_CUR_STOCK_ID }
                                           into grp
                                           select new
                                           {
                                               EXP_DATE = grp.Key.T_EXPIRE_DATE,
                                               Qty = grp.Key.T_TRNSACTION_QTY,
                                               ProdId = grp.Key.T_ITEM_ID,
                                               Stock_Id = grp.Key.T_CUR_STOCK_ID
                                           }).OrderBy(k => k.EXP_DATE).ToList();

                        decimal? quantityRemains = newChild.T_TRANSFER_QTY;
                        //Fifo statement start 
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
                            Int64? t81 = obj.T74081.Count() > 0 ? obj.T74081.Max(a => a.T_TRANSFER_REQ_DTL_ID) : 0;

                            T74081 detailObject = new T74081();
                            detailObject.T_TRANSFER_REQ_DTL_ID = Convert.ToInt64(t81 != 0 ? t81 + 1 : 1);
                            detailObject.T_TRANSFER_REQ_ID = T_TRANSFER_REQ_ID;
                            detailObject.T_ENTRY_USER = user;
                            detailObject.T_ENTRY_DATE = date;
                            detailObject.T_CUR_STOCK_ID = singleObject.Stock_Id;
                            detailObject.T_TRANSFER_QTY = tQuantity;
                            detailObject.T_TOTAL_VALUE = Math.Round(Convert.ToDecimal(tQuantity * (newChild.T_UNIT_VALUE)), 2);
                            detailObject.T_ITEM_ID = newChild.T_ITEM_ID;
                            detailObject.T_UOM_ID = newChild.T_UOM_ID;
                            detailObject.T_UNIT_VALUE = newChild.T_UNIT_VALUE;
                            detailObject.T_TRANSFER_PRORITY = newChild.T_TRANSFER_PRORITY;
                            obj.T74081.Add(detailObject);
                            Save();

                            var t27 = obj.T74027.Where(p => p.T_CUR_STOCK_ID == singleObject.Stock_Id).FirstOrDefault();
                            if (t27 != null)
                            {
                                t27.T_TRNSACTION_QTY = t27.T_TRNSACTION_QTY - tQuantity;
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
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }

            return t74080.T_TRANSFER_REQ_NO;
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
                             T_LANG2_NAME = k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
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
                             T_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
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

        public IEnumerable GetPersonType()
        {
            //get { return obj.T74005; }
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from persontype in obj.T74005
                    select new
                    {
                        T_EMP_TYP_ID = persontype.T_EMP_TYP_ID,
                        T_LANG2_NAME = persontype.T_LANG2_NAME

                    }).AsEnumerable().Select((r, i) => new
                {
                    T_EMP_TYP_ID = r.T_EMP_TYP_ID,
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
        //Person Name Popup Method Start

        public IEnumerable GetPersonName(int id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from person in obj.T74004
                         join personType in obj.T74005 on person.T_EMP_TYP_ID equals personType.T_EMP_TYP_ID
                         where personType.T_EMP_TYP_ID == id
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
        //Get stock Method Start

        public decimal GetStock(int? ITEM_ID, int? UM_ID, int? STORE_ID)
        {
            var query = obj.Database.SqlQuery<decimal>("SELECT NVL(fn_Stock_Qty(" + ITEM_ID + "," + UM_ID + "," + STORE_ID + "),0) FROM DUAL").FirstOrDefault();
            return query;
        }

        //Get Purchase Value Start
        public decimal GetPurPrice(int ITEM_ID, int UM_ID)
        {
            decimal rawQuery = 0;
            try
            {
                 rawQuery = obj.Database.SqlQuery<decimal>("select T_PUR_PRICE from T74072 inner join T74073 on T74072.T_COST_TYPE_ID=T74073.T_COST_TYPE_ID inner join T74089 on T74073.T_COST_TYPE_DTL_ID=T74089.T_COST_TYPE_DTL_ID where T74073.T_ID= '"+ ITEM_ID + "' and T74089.T_ITEM_UM_ID="+ UM_ID + " and T74089.T_ACTIVE = '1'").FirstOrDefault();
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
            return rawQuery;
        }

    }
}