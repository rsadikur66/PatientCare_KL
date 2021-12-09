using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;
using WebGrease.Css.Ast.Selectors;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74121Repository : IT74121
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74121Repository(AmbucareContainer _obj)
        {
            obj.Configuration.ProxyCreationEnabled = false;
            this.obj = _obj;

        }
        //IEnumerable GetProTypeData(int type_id)

        public IEnumerable GetTypeData()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Item in obj.T74072
                    select new
                    {
                        T_COST_TYPE_ID = Item.T_COST_TYPE_ID,
                        T_LANG2_NAME = Item.T_LANG2_NAME

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

        public IEnumerable GetProTypeData(int type_id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t72 in obj.T74072
                    join t73 in obj.T74073 on t72.T_COST_TYPE_ID equals t73.T_COST_TYPE_ID
                    join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                    join t03 in obj.T74003 on t72.T_COST_TYPE_ID equals t03.T_TYPE_ID into um
                    from umId in um.DefaultIfEmpty()
                    where t72.T_COST_TYPE_ID == type_id && t89.T_ACTIVE == "1"
                         select new
                    {
                        Type_ID = t73.T_COST_TYPE_DTL_ID,
                        Type_NAME = t72.T_LANG2_NAME,
                        Item_Name = t73.T_LANG2_NAME,
                        Pre_Price = t89.T_PUR_PRICE,
                        Curr_Price = t89.T_SALE_PRICE,
                        T_LANG2_NAME= umId.T_LANG2_NAME,
                        T_ITEM_UM_ID= umId.T_ITEM_UM_ID !=null? umId.T_ITEM_UM_ID:0
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
        public IEnumerable GenericData()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                //query = obj.Database.SqlQuery<CommonModel>("select GEN_CODE, GEN_DESC from Rufaida.V30001 group by GEN_CODE, GEN_DESC").ToList();
                query = obj.Database.SqlQuery<CommonModel>("select T_GEN_CODE GEN_CODE, T_LANG2_NAME GEN_DESC from T74113").ToList();
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
        public IEnumerable GetProductList(string GEN_CODE)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {

                //--------------------New-------------------------
                query = obj.Database.SqlQuery<CommonModel>("SELECT T74072.T_COST_TYPE_ID,  T74072.T_LANG2_NAME TYPE_NAME,  T74073.T_ID T_ITEM_ID,   T74073.T_LANG2_NAME ITEM_NAME,   T74073.T_COST_TYPE_DTL_ID,   T74114.T_GEN_CODE FROM T74072 INNER JOIN T74073 ON T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID INNER JOIN T74003 ON T74072.T_COST_TYPE_ID = T74003.T_TYPE_ID INNER JOIN T74114 ON T74073.T_ID = T74114.T_ITEM_CODE WHERE T74072.T_COST_TYPE_ID   = 23 AND T74114.T_GEN_CODE = '" + GEN_CODE + "' GROUP BY T74072.T_COST_TYPE_ID,  T74072.T_LANG2_NAME,  T74073.T_ID,  T74073.T_LANG2_NAME,  T74073.T_COST_TYPE_DTL_ID, T74114.T_GEN_CODE").ToList();

                //--------------------data comes from rufaida------------------------
                //query =obj.Database.SqlQuery<CommonModel>("SELECT T74072.T_COST_TYPE_ID,  T74072.T_LANG2_NAME TYPE_NAME,  T74073.T_ID T_ITEM_ID,   T74073.T_LANG2_NAME ITEM_NAME,   T74073.T_COST_TYPE_DTL_ID,   V30001.GEN_CODE FROM T74072 INNER JOIN T74073 ON T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID INNER JOIN T74003 ON T74072.T_COST_TYPE_ID = T74003.T_TYPE_ID INNER JOIN V30001 ON T74073.T_ID = V30001.ITEM_CODE WHERE T74072.T_COST_TYPE_ID   = 23 AND V30001.GEN_CODE = '"+ GEN_CODE + "' GROUP BY T74072.T_COST_TYPE_ID,  T74072.T_LANG2_NAME,  T74073.T_ID,  T74073.T_LANG2_NAME,  T74073.T_COST_TYPE_DTL_ID,  V30001.GEN_CODE").ToList();


                // query = obj.Database.SqlQuery<CommonModel>("select T74072.T_COST_TYPE_ID, T74072.T_LANG2_NAME TYPE_NAME, T74073.T_ID T_ITEM_ID, T74073.T_LANG2_NAME ITEM_NAME, T74073.T_COST_TYPE_DTL_ID, Rufaida.V30001.GEN_CODE from T74072 inner join T74073 on T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID inner join T74089 on T74073.T_COST_TYPE_DTL_ID = T74089.T_COST_TYPE_DTL_ID inner join T74003 on T74072.T_COST_TYPE_ID = T74003.T_TYPE_ID INNER JOIN Rufaida.V30001 ON T74073.T_ID = Rufaida.V30001.ITEM_CODE left outer join T74027 on T74073.T_ID = T74027.T_ITEM_ID where T74089.T_ACTIVE = '1' and T74072.T_COST_TYPE_ID = 23 AND Rufaida.V30001.GEN_CODE = '" + GEN_CODE + "' group by T74072.T_COST_TYPE_ID, T74072.T_LANG2_NAME, T74073.T_ID, T74073.T_LANG2_NAME, T74073.T_COST_TYPE_DTL_ID,Rufaida.V30001.GEN_CODE ").ToList();


                // query = obj.Database.SqlQuery<CommonModel>("select T74003.T_LANG2_NAME TYPE_NAME,T74001.T_LANG2_NAME ITEM_NAME from T74001 inner join T74003 on T74001.T_TYPE_ID = T74003.T_TYPE_ID").ToList();
                //query = (from t73 in obj.T74073
                //    join t27 in obj.T74027 on t73.T_ID equals t27.T_ITEM_ID
                //    join t54_comp in obj.T74054 on t27.T_COMPANY_ID equals t54_comp.T_COMPANY_ID 
                //    select new
                //    {
                //        T_ID=  t73.T_ID,
                //        T_ITEM_ID= t27.T_ITEM_ID,
                //        T_LANG2_NAME= t73.T_LANG2_NAME,
                //        Comp_T_LANG2_NAME= t54_comp.T_LANG2_NAME,
                //    }
                //).ToList();
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

        public IEnumerable GetUomtList(int TypeId, string GEN_CODE, string ITEM_CODE)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {

                query = obj.Database.SqlQuery<CommonModel>($"SELECT T74073.T_ID T_ITEM_ID, T74073.T_LANG2_NAME ITEM_NAME, T74003.T_ITEM_UM_ID, T74073.T_COST_TYPE_DTL_ID, T74003.T_LANG2_NAME PACK_SIZE, NVL(fn_AVG_PUR_PRICE(T74073.T_ID, T74003.T_ITEM_UM_ID), 0) PUR_PRICE, fn_AVG_SALE_PRICE(T74073.T_COST_TYPE_DTL_ID, T74003.T_ITEM_UM_ID) SALE_PRICE FROM T74003 INNER JOIN T74072 ON T74003.T_TYPE_ID = T74072.T_COST_TYPE_ID INNER JOIN T74073 ON T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID INNER JOIN T74114 ON T74073.T_ID = T74114.T_ITEM_CODE WHERE T74114.T_GEN_CODE = '{GEN_CODE}' AND T74073.T_ID = '{ITEM_CODE}' GROUP BY T74073.T_ID, T74073.T_LANG2_NAME, T74003.T_ITEM_UM_ID, T74073.T_COST_TYPE_DTL_ID, T74003.T_LANG2_NAME ORDER BY T74003.T_ITEM_UM_ID, T74073.T_ID ASC").ToList();
                //query = obj.Database.SqlQuery<CommonModel>("SELECT T74073.T_ID T_ITEM_ID, T74073.T_LANG2_NAME ITEM_NAME, T74003.T_ITEM_UM_ID, T74073.T_COST_TYPE_DTL_ID, T74003.T_LANG2_NAME PACK_SIZE, NVL(fn_AVG_PUR_PRICE(T74073.T_ID, T74003.T_ITEM_UM_ID), 0) PUR_PRICE,fn_AVG_SALE_PRICE(T74073.T_COST_TYPE_DTL_ID, T74003.T_ITEM_UM_ID)  SALE_PRICE FROM T74003 INNER JOIN T74072 ON T74003.T_TYPE_ID = T74072.T_COST_TYPE_ID INNER JOIN T74073 ON T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID INNER JOIN V30001 ON T74073.T_ID = V30001.ITEM_CODE WHERE V30001.GEN_CODE = '" + GEN_CODE + "' AND T74073.T_ID = '" + ITEM_CODE + "' GROUP BY T74073.T_ID, T74073.T_LANG2_NAME, T74003.T_ITEM_UM_ID, T74073.T_COST_TYPE_DTL_ID, T74003.T_LANG2_NAME  order by  T74003.T_ITEM_UM_ID, T74073.T_ID ASC").ToList();
                // query = obj.Database.SqlQuery<CommonModel>("SELECT T74073.T_ID T_ITEM_ID, T74073.T_LANG2_NAME ITEM_NAME, T74003.T_ITEM_UM_ID, T74073.T_COST_TYPE_DTL_ID, T74003.T_LANG2_NAME PACK_SIZE, NVL(fn_AVG_PUR_PRICE(T74073.T_ID, T74003.T_ITEM_UM_ID), 0) PUR_PRICE, nvl(fn_AVG_SALE_PRICE(T74073.T_COST_TYPE_DTL_ID, T74003.T_ITEM_UM_ID),0 ) SALE_PRICE FROM T74003 INNER JOIN T74072 ON T74003.T_TYPE_ID = T74072.T_COST_TYPE_ID INNER JOIN T74073 ON T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID INNER JOIN V30001 ON T74073.T_ID = V30001.ITEM_CODE WHERE V30001.GEN_CODE = '" + GEN_CODE + "' AND T74073.T_ID = '" + ITEM_CODE + "' GROUP BY T74073.T_ID, T74073.T_LANG2_NAME, T74003.T_ITEM_UM_ID, T74073.T_COST_TYPE_DTL_ID, T74003.T_LANG2_NAME  order by  T74003.T_ITEM_UM_ID, T74073.T_ID ASC").ToList();
                //  query = obj.Database.SqlQuery<CommonModel>("SELECT T74073.T_ID T_ITEM_ID,T74073.T_LANG2_NAME ITEM_NAME,   T74003.T_ITEM_UM_ID,T74073.T_COST_TYPE_DTL_ID,   T74003.T_LANG2_NAME PACK_SIZE,  NVL(fn_AVG_PUR_PRICE(T74073.T_ID,T74003.T_ITEM_UM_ID),0) PUR_PRICE,   T74089.T_SALE_PRICE SALE_PRICE FROM T74003  INNER JOIN T74072 ON T74003.T_TYPE_ID = T74072.T_COST_TYPE_ID INNER JOIN T74073 ON T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID INNER JOIN T74089 ON T74073.T_COST_TYPE_DTL_ID = T74089.T_COST_TYPE_DTL_ID INNER JOIN V30001 ON T74073.T_ID = V30001.ITEM_CODE WHERE T74089.T_ACTIVE     = '1' AND V30001.GEN_CODE       = '" + GEN_CODE + "' AND T74073.T_ID      = '"+ ITEM_CODE + "' GROUP BY T74073.T_ID,T74073.T_LANG2_NAME,T74089.T_SALE_PRICE,  T74003.T_ITEM_UM_ID, T74073.T_COST_TYPE_DTL_ID, T74003.T_LANG2_NAME").ToList();


                //query = obj.Database.SqlQuery<CommonModel>("select T74073.T_ID,T74003.T_ITEM_UM_ID,T74003.T_LANG2_NAME PACK_SIZE,NVL(fn_AVG_PUR_PRICE('" + ITEM_CODE + "',101),0) PUR_PRICE,0 SALE_PRICE FROM T74003 left outer join T74072 on T74003.T_TYPE_ID = T74072.T_COST_TYPE_ID left outer join T74073 on T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID left outer join T74089 ON T74073.T_COST_TYPE_DTL_ID = T74089.T_COST_TYPE_DTL_ID left outer join V30001 on T74073.T_ID = V30001.ITEM_CODE WHERE T74089.T_ACTIVE   = '1' AND T74072.T_COST_TYPE_ID    = '" + TypeId + "' AND V30001.GEN_CODE    = '" + GEN_CODE + "' AND V30001.ITEM_CODE  = '" + ITEM_CODE + "' group by T74073.T_ID,T74003.T_ITEM_UM_ID,T74003.T_LANG2_NAME ").ToList();




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

        public bool SaveData(List<T74089> t74089)
        {
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    foreach (var i in t74089)
                    {
                        var check = obj.T74089
                            .Where(x => x.T_COST_TYPE_DTL_ID == i.T_COST_TYPE_DTL_ID &&
                                        x.T_ITEM_UM_ID == i.T_ITEM_UM_ID).FirstOrDefault();

                        if (i.T_COST_TYPE_DTL_ID != null && i.T_ITEM_UM_ID != null) //for pharmecy 
                        {
                            if (check != null)
                            {
                                //  var check = obj.T74089.Where(x => x.T_COST_TYPE_DTL_ID == i.T_COST_TYPE_DTL_ID && x.T_ITEM_UM_ID ==i.T_ITEM_UM_ID).FirstOrDefault();
                                obj.Database.ExecuteSqlCommand(
                                    "update T74089 set T_ACTIVE = 0 where T_COST_TYPE_DTL_ID = " +
                                    i.T_COST_TYPE_DTL_ID + " and T_ITEM_UM_ID = " + i.T_ITEM_UM_ID + "");
                                //check.T_ACTIVE = "0";
                                //Save();

                                var max = obj.T74089.Max(x => x.T_PRICE_ID);
                                i.T_PRICE_ID = max + 1;
                                i.T_ACTIVE = "1";
                                i.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                i.T_ENTRY_DATE = DateTime.Now;


                                obj.T74089.Add(i);
                                Save();

                            }
                            else
                            {


                                obj.Database.ExecuteSqlCommand(
                                    "update T74089 set T_ACTIVE = 0 where T_COST_TYPE_DTL_ID = " +
                                    i.T_COST_TYPE_DTL_ID + "and T_ITEM_UM_ID = " + i.T_ITEM_UM_ID + "");
                                //and T_ITEM_UM_ID = " + i.T_ITEM_UM_ID + "

                                var max = obj.T74089.Max(x => x.T_PRICE_ID);
                                i.T_PRICE_ID = max + 1;
                                i.T_ACTIVE = "1";

                                i.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                i.T_ENTRY_DATE = DateTime.Now;

                                obj.T74089.Add(i);
                                Save();

                               

                            }

                        }
                        else //for Type
                        {
                            if (i.T_SALE_PRICE !=0)
                            {
                                // var check = obj.T74089.Where(x => x.T_COST_TYPE_DTL_ID == i.T_COST_TYPE_DTL_ID).FirstOrDefault();
                                obj.Database.ExecuteSqlCommand(
                                    "update T74089 set T_ACTIVE = 0 where T_COST_TYPE_DTL_ID = " + i.T_COST_TYPE_DTL_ID +
                                    "");
                                //check.T_ACTIVE = "0";
                                //Save();

                                var max = obj.T74089.Max(x => x.T_PRICE_ID);
                                i.T_PRICE_ID = max + 1;
                                i.T_ACTIVE = "1";

                                i.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                i.T_ENTRY_DATE = DateTime.Now;

                                obj.T74089.Add(i);
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
            return true;
        }
        public void Dispose()
        {
            obj.Dispose();

        }

        public void Save()
        {
            obj.SaveChanges();
        }

        //public int GetNextSequenceValue()
        //{
        //    var rawQuery = obj.Database.SqlQuery<int>("SELECT T74089_SEQ.NEXTVAL FROM DUAL").First();
        //    var nextVal = Convert.ToInt32(rawQuery);

        //    return nextVal;
        //}
    }
}