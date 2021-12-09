using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Schema;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74113Repository : IT74113
    {
        private AmbucareContainer obj = new AmbucareContainer();

        private readonly T74113DAL _t74113DAL = new T74113DAL();
        public T74113Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;

        }

        public IEnumerable GetPatInfo(string uId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //query = obj.T74046.Join(obj.T74050, t46 => t46.T_SEX_CODE, t50 => t50.T_SEX_CODE,
            //        (t46, t50) => new { t46, t50 })
            //    .Join(obj.T74051, t46_50 => t46_50.t46.T_MRTL_STATUS, t51 => t51.T_MRTL_STATUS_CODE,
            //        (t46_50, t51) => new { t46_50, t51 })
            //    .Join(obj.T02003, t46_50_51 => t46_50_51.t46_50.t46.T_NTNLTY_ID, t58 => t58.T_NTNLTY_ID,
            //        (t46_50_51, t58) => new { t46_50_51, t58 })
            //    .Join(obj.T74041, t46_50_51_58 => t46_50_51_58.t46_50_51.t46_50.t46.T_PAT_ID, t41 => t41.T_PAT_ID,
            //        (t46_50_51_58, t41) => new { t46_50_51_58, t41 })
            //    .Join(obj.T74043, t46_50_51_58_41 => t46_50_51_58_41.t41.T_REQUEST_ID, t43 => t43.T_REQUEST_ID,
            //        (t46_50_51_58_41, t43) => new { t46_50_51_58_41, t43 }).Where(m => m.t43.T_REQUEST_ID == T_REQUEST_ID)
            //    .Select(k => new
            //    {
            //        T_REQUEST_ID = k.t43.T_REQUEST_ID,
            //        T_PAT_ID = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t46_50.t46.T_PAT_ID,
            //        T_FIRST_LANG2_NAME = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t46_50.t46.T_FIRST_LANG2_NAME,
            //        T_FATHER_LANG2_NAME = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t46_50.t46.T_FATHER_LANG2_NAME,
            //        T_GFATHER_LANG2_NAME = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t46_50.t46.T_GFATHER_LANG2_NAME,
            //        T_FAMILY_LANG2_NAME = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t46_50.t46.T_FAMILY_LANG2_NAME,
            //        Gender = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t46_50.t50.T_LANG2_NAME,
            //        Age = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t46_50.t46.T_BIRTH_DATE,
            //        MaritalStatus = k.t46_50_51_58_41.t46_50_51_58.t46_50_51.t51.T_LANG2_NAME,
            //        Nationality = k.t46_50_51_58_41.t46_50_51_58.t58.T_LANG2_NAME,
            //        T_HIGHT = k.t43.T_HIGHT,
            //        T_WEIGHT = k.t43.T_WEIGHT,
            //        T_AMBU_REG_ID = k.t46_50_51_58_41.t41.T_AMBU_REG_ID
            //    }).ToList();
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
                join T43 in obj.T74043 on T41.T_REQUEST_ID equals T43.T_REQUEST_ID into T74043
                from T43 in T74043.DefaultIfEmpty()
                join T39 in obj.T74039 on T41.T_REQUEST_ID equals  T39.T_REQUEST_ID into T74039
                from T39 in T74039.DefaultIfEmpty()
                where T57.T_USER_ID == uId && T41.T_DISCH_STATUS == null
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
                    T_HIGHT = T43==null? "0" : T43.T_HIGHT,
                    T_WEIGHT = T43 == null ? "0" : T43.T_WEIGHT,
                    T_AMBU_REG_ID = T41.T_AMBU_REG_ID,
                    T_REMARKS = T39.T_REMARKS,
                    T_PRESCRIPTION_ID= T39==null?0:T39.T_PRESCRIPTION_ID
                }).ToList();
            return query;
        }

        public IEnumerable GetDocInfoByReq(int T_AMBU_REG_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74004.Join(obj.T74005, t04 => t04.T_EMP_TYP_ID, t05 => t05.T_EMP_TYP_ID,
                    (t04, t05) => new { t04, t05 })
                .Join(obj.T74015, t04_05 => t04_05.t04.T_EMP_ID, t15 => t15.T_EMP_ID,
                    (t04_05, t15) => new { t04_05, t15 }).Where(m => m.t04_05.t04.T_EMP_TYP_ID == 2 && m.t15.T_AMBU_REG_ID == T_AMBU_REG_ID)
                .Select(k => new
                {
                    T_EMP_ID = k.t04_05.t04.T_EMP_ID,
                    T_FIRST_LANG2_NAME = k.t04_05.t04.T_FIRST_LANG2_NAME,
                    T_FATHER_LANG2_NAME = k.t04_05.t04.T_FATHER_LANG2_NAME,
                    T_GFATHER_LANG2_NAME = k.t04_05.t04.T_GFATHER_LANG2_NAME,
                    T_FAMILY_LANG2_NAME = k.t04_05.t04.T_FAMILY_LANG2_NAME
                }).ToList().Select(f => new
                {

                    T_EMP_ID = f.T_EMP_ID,
                    Full_Name = f.T_FIRST_LANG2_NAME + " " + f.T_FATHER_LANG2_NAME + " " +
                              f.T_GFATHER_LANG2_NAME + " " +
                              f.T_FAMILY_LANG2_NAME
                }).ToList();
            return query;
        }

        public IEnumerable GetDocDept(int T_EMP_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74004
                .Join(obj.T74021, t04 => t04.T_DOC_DEPT_ID, t21 => t21.T_DOC_DEPT_ID, (t04, t21) => new { t04, t21 }).Where(s => s.t04.T_EMP_ID == T_EMP_ID)
                .Select(m => new { Dept_Name = m.t21.T_LANG2_NAME }).ToList();
            return query;
        }

        public IEnumerable DiagonosisList()
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74073.Where(a => a.T_COST_TYPE_ID == 1)
                .Join(obj.T74089.Where(s=>s.T_ACTIVE=="1") ,t73=>t73.T_COST_TYPE_DTL_ID,t89=>t89.T_COST_TYPE_DTL_ID,(t73,t89)=>new{ t73, t89 })
                .Select(m => new { Id = m.t73.T_COST_TYPE_DTL_ID, Name = m.t73.T_LANG2_NAME, Price = m.t89.T_SALE_PRICE }).ToList();
            return query;
        }

        public IEnumerable GetFrequency()
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T30209.Select(m => new { m.T_FREQUENCY_CODE, m.T_LANG2_NAME,m.T_QTY_MON }).ToList();
            return query;
        }
        public IEnumerable GetDuration(int Frequency_Code)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T30010.Where(a=> a.T_QTY>=30/Frequency_Code).Select(m => new { m.T_DOSE_DURATION_CODE, m.T_LANG2_NAME,m.T_QTY }).ToList();
            return query;
        }
         public IEnumerable GetAdviceList(string T_LANG2_NAME)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t97 in obj.T74097
                    where t97.T_LANG2_NAME.ToUpper().Contains(T_LANG2_NAME.ToUpper())
                    select new
                    {
                        t97.T_ADVICE_ID,
                        t97.T_LANG2_NAME
                    }).ToList();

                //Take(1000)
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
        public DataTable GetMedicineByTrade()
        {
            //DbRawSqlQuery<string>  query = obj.Database.SqlQuery<string>("select ITEM_CODE, TRADE_CODE, PRODUCT_DESC, STRENGTH, ROUTE_CODE, ROUTE_DESC, FORM_CODE, FORM_DESC  from RUFAIDA.V30001 inner join T74073  on V30001.ITEM_CODE = T74073.T_ID Where T_COST_TYPE_ID = 23");
            //return query;

            var data = _t74113DAL.GetMedicineByTrade();
            return data;
        }
        public DataTable GetGen()
        {
            var data = _t74113DAL.GetGen();
            return data;
        }
        public DataTable GetStrengthByGen(string T_GEN_CODE)
        {
            var data = _t74113DAL.GetStrengthByGen(T_GEN_CODE);
            return data;
        }
        public DataTable GetRouteByGen(string T_GEN_CODE)
        {
            var data = _t74113DAL.GetRouteByGen(T_GEN_CODE);
            return data;
        }
        public DataTable GetFormByGen(string T_GEN_CODE)
        {
            var data = _t74113DAL.GetFormByGen(T_GEN_CODE);
            return data;
        }

        public IEnumerable GetPrscrptnByDoc(int T_DOC_ID, int T_PAT_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74039.Join(obj.T74041, t39 => t39.T_REQUEST_ID, t41 => t41.T_REQUEST_ID, (t39, t41) => new
            {t39,t41}).Where(s=>s.t39.T_DOC_ID == T_DOC_ID && s.t41.T_PAT_ID == T_PAT_ID).Select(k=>new
            {
                k.t39.T_PRESCRIPTION_ID,
                k.t39.T_ENTRY_DATE,
                k.t39.T_REMARKS
            });
            return query;
        }
        void Sample(List<T74037> childObjectList, T74036 masterObjectList)
        {                    
            List<string> DistinctList = new List<string>();
            T74036 masterObject = null;
            try
            {
                foreach (T74037 newChild in childObjectList)
                {
                    var productList = (from i in obj.T74027.Where(p => p.T_TRNSACTION_QTY >0)
                                       join j in obj.T74073.Where(q => q.T_ID == newChild.T_ITEM_ID).Where(r => r.T_COST_TYPE_ID == 23) on i.T_ITEM_ID equals j.T_ID
                        join t89 in obj.T74089 on j.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID 
                                       select new
                                       {
                                           BatchNo = i.T_BATCH,                                                                                    
                                           EXP_DATE = i.T_EXPIRE_DATE,
                                           Qty = i.T_TRNSACTION_QTY,
                                           ProdId = i.T_ITEM_ID,
                                           Rate = t89.T_SALE_PRICE
                                       }).OrderBy(k => k.EXP_DATE).ToList();

                    decimal? quantityRemains = newChild.T_QUANTITY;
                 

                   
                        masterObject = new T74036();      
                    masterObject.T_PAT_NO = masterObjectList.T_PAT_NO;
                    masterObject.T_ENTRY_USER_ = HttpContext.Current.User.Identity.Name;
                    masterObject.T_ENTRY_DATE_ = DateTime.Now;
                    obj.T74036.Add(masterObject);
                    foreach (var singleObject in productList.Where(p => p.ProdId == newChild.T_ITEM_ID).Where(q => q.Qty > 0))
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

                        T74037 detailObject = new T74037();
                        detailObject.T_ITEM_ID = singleObject.ProdId;
                        detailObject.T_SALE_PRICE = newChild.T_SALE_PRICE;
                        detailObject.T_QUANTITY = tQuantity;
                        detailObject.T_TOTAL_AMOUNT = (tQuantity) * (decimal.Parse(singleObject.Rate.ToString()));
                        
                        masterObject.T74037.Add(detailObject);                                                                    
                    }

                    obj.SaveChanges();
                }

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
        public string Insert(T74039 t74039, List<T74040> t74040List, List<T74078> t74078List)
        {
            string sms = "";
            if (t74039.T_PRESCRIPTION_ID==0)
            {

            bool dis_sts = obj.T74041.Any(a => a.T_REQUEST_ID == t74039.T_REQUEST_ID && a.T_DISCH_STATUS != "1");
            bool Ex_Pat = obj.T74039.Any(a => a.T_REQUEST_ID == t74039.T_REQUEST_ID);
            int T_PRESCRIPTION_ID;
            if (Ex_Pat && dis_sts)
            {
                T_PRESCRIPTION_ID = obj.T74039.Where(m => m.T_REQUEST_ID == t74039.T_REQUEST_ID).Max(a => a.T_PRESCRIPTION_ID);
            }
            else
            {
                obj.T74039.Add(t74039);
                Save();
                T_PRESCRIPTION_ID = obj.T74039.Where(m => m.T_ENTRY_USER == t74039.T_ENTRY_USER).Max(a => a.T_PRESCRIPTION_ID);
                

            }

            if (t74040List != null)
            {
                foreach (T74040 t74040 in t74040List)
                {
                    int t40 = obj.T74040.Max(a => a.T_PRSCRPTN_DTL_ID);
                    t74040.T_PRSCRPTN_DTL_ID = t40 != 0 ? t40 + 1 : 1;
                    t74040.T_PRESCRIPTION_ID = T_PRESCRIPTION_ID;
                    obj.T74040.Add(t74040);
                    Save();
                }
            }

            if (t74078List != null)
            {
                foreach (T74078 t74078 in t74078List)
                {
                    int t78 = obj.T74078.Max(a => a.T_DIAGONOSIS_ID);
                    t74078.T_DIAGONOSIS_ID = t78 != 0 ? t78 + 1 : 1;
                    t74078.T_PRESCRIPTION_ID = T_PRESCRIPTION_ID;
                    obj.T74078.Add(t74078);
                    Save();

                }
            }
                sms = "Save successfully";
            }
            else
            {
                string r = Convert.ToString(t74039.T_PRESCRIPTION_ID);
                var chk36 = obj.T74036.Where(x => x.T_PRSCRPTN_ID == r  &&
                                                  x.T_REQUEST_ID == t74039.T_REQUEST_ID).FirstOrDefault();
                if (chk36==null)
                {
               
                var chek = obj.T74039.Where(x => x.T_PRESCRIPTION_ID == t74039.T_PRESCRIPTION_ID &&
                                                 x.T_REQUEST_ID == t74039.T_REQUEST_ID).FirstOrDefault();

                chek.T_DOC_ID = t74039.T_DOC_ID;
                chek.T_REMARKS = t74039.T_REMARKS;
                chek.T_UPD_DATE = t74039.T_ENTRY_DATE;
                chek.T_UPD_USER = t74039.T_ENTRY_USER;
                Save();

                if (t74040List != null)
                {
                    int remove40 = 0;
                    var chkforDelete = obj.T74040.Where(x => x.T_PRESCRIPTION_ID == t74039.T_PRESCRIPTION_ID)
                        .ToList();
                    if (chkforDelete !=null)
                    {
                        obj.T74040.RemoveRange(chkforDelete);
                        Save();
                        remove40 = 1;
                    }
                    if (remove40 == 1)
                    {
                        foreach (T74040 t74040 in t74040List)
                        {
                            int t40 = obj.T74040.Max(a => a.T_PRSCRPTN_DTL_ID);
                            t74040.T_PRSCRPTN_DTL_ID = t40 != 0 ? t40 + 1 : 1;
                            t74040.T_PRESCRIPTION_ID = t74039.T_PRESCRIPTION_ID;
                            obj.T74040.Add(t74040);
                            Save();
                        }
                    }
                    
                }
                //-------------
                if (t74078List != null)
                {
                    int remove78 = 0;
                    var chkforDelete78 = obj.T74078.Where(x => x.T_PRESCRIPTION_ID == t74039.T_PRESCRIPTION_ID)
                        .ToList();
                    if (chkforDelete78 != null)
                    {
                        obj.T74078.RemoveRange(chkforDelete78);
                        Save();
                        remove78 = 1;
                    }
                    if (remove78 == 1)
                    {
                        foreach (T74078 t74078 in t74078List)
                        {
                            int t78 = obj.T74078.Max(a => a.T_DIAGONOSIS_ID);
                            t74078.T_DIAGONOSIS_ID = t78 != 0 ? t78 + 1 : 1;
                            t74078.T_PRESCRIPTION_ID = t74039.T_PRESCRIPTION_ID;
                            obj.T74078.Add(t74078);
                            Save();

                        }
                    }
                    
                }
                    sms = "Update successfully";
                }
                else
                {
                    sms = "you are not able to Update because of Billing has been completed";
                }
            }
            return sms;
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
        public void Dispose()
        {
            obj.Dispose();
        }

        public IEnumerable GetDiagonosisByPres(int Pres)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query=(from t78 in obj.T74078 
                    join t89 in obj.T74089 on t78.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                    join t73 in obj.T74073 on t78.T_COST_TYPE_DTL_ID equals t73.T_COST_TYPE_DTL_ID
                   where t78.T_PRESCRIPTION_ID==Pres
                    select new { t73.T_COST_TYPE_DTL_ID, t73.T_LANG2_NAME, T_PRICE = t89.T_SALE_PRICE }
                    ).ToList();
            return query;
        }
        public DataTable GetMedListByPres(int Pres)
        {
            var data = _t74113DAL.GetMedListByPres(Pres);
            return data;
        }
    }
}