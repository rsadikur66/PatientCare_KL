using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74123Repository : IT74123
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74123Repository(AmbucareContainer _obj)
        {
            //obj.Configuration.ProxyCreationEnabled = false;
            this.obj = _obj;

        }
        //IEnumerable PatientDetalisData(int id)
        public IEnumerable GetPatients()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                var userid = HttpContext.Current.Session["T_USER_ID"].ToString();

                var rol = obj.T74066.Where(p => p.T_USER_ID == userid).Select(k=>k.T_ROLE_CODE).FirstOrDefault();

                if (rol == 23)
                {
                    query = (from t46_Pat in obj.T74046
                        join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                        join t15 in obj.T74015 on t41.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                        join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                        where t41.T_DISCH_STATUS == null  //&& t57.T_USER_ID == userid
                        select new
                        {

                            PatentId = t46_Pat.T_PAT_ID,
                            Name = t46_Pat.T_FIRST_LANG2_NAME,
                            Mobile = t46_Pat.T_MOBILE_NO,
                            Problem = t41.T_PROB_DETAILS,
                            BirthDate = t46_Pat.T_BIRTH_DATE,

                        }
                    ).AsEnumerable().Select((r, i) => new
                    {
                        PatentId = r.PatentId,
                        Name = r.Name,
                        Mobile = r.Mobile,
                        Problem = r.Problem,
                        BirthDate = r.BirthDate,
                        RequestId = GetRequest(r.PatentId)
                    }).ToList();//.ToList(); 
                }
                else
                {
                
                    query = (from t46_Pat in obj.T74046
                        join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                        join t15 in obj.T74015 on t41.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                        join t57 in obj.T74057 on t15.T_EMP_ID equals t57.T_EMP_ID
                        where t41.T_DISCH_STATUS == null && t57.T_USER_ID == userid
                             select new
                        {

                            PatentId = t46_Pat.T_PAT_ID,
                            Name = t46_Pat.T_FIRST_LANG2_NAME, 
                            Mobile = t46_Pat.T_MOBILE_NO,
                            Problem = t41.T_PROB_DETAILS,
                            BirthDate = t46_Pat.T_BIRTH_DATE,
                          
                        }
                    ).AsEnumerable().Select((r, i) => new
                    {
                        PatentId = r.PatentId,
                        Name = r.Name,
                        Mobile = r.Mobile,
                        Problem = r.Problem,
                        BirthDate = r.BirthDate,
                        RequestId = GetRequest(r.PatentId)
                    }).ToList();//.ToList();
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


            return query;
        }

       public IEnumerable PatientDetalisData(int id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
               
                query = (from t46_Pat in obj.T74046
                    join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                    join t44_Amb in obj.T74044 on t41.T_AMBU_REG_ID equals t44_Amb.T_AMBU_REG_ID
                    where t46_Pat.T_PAT_ID == id
                         select new
                    {

                        PatentId = t46_Pat.T_PAT_ID,
                        Name = t46_Pat.T_FIRST_LANG2_NAME, //+ t46_Pat.T_FATHER_LANG2_NAME,
                        Mobile = t46_Pat.T_MOBILE_NO,
                        Problem = t41.T_PROB_DETAILS,
                        BirthDate = t46_Pat.T_BIRTH_DATE,
                        Ambulance = t44_Amb.T_LANG2_NAME,
                      
                    }).AsEnumerable().Select((r, i) => new
                {
                    PatentId = r.PatentId,
                    Name = r.Name,
                    Mobile = r.Mobile,
                    Problem = r.Problem,
                    BirthDate = r.BirthDate,
                    Ambulance = r.Ambulance,
                     RequestId = GetRequest(r.PatentId) 
                    }).ToList();//ToList();
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

        public IEnumerable PriscriptionData(int id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                var max = obj.T74041.Where(m=>m.T_PAT_ID ==id).Max(x => x.T_REQUEST_ID );

                query = obj.Database.SqlQuery<CommonModel>("SELECT case when T74040.T_ITEM_CODE is null then (select T_LANG2_NAME from T30004 where T30004.T_GEN_CODE = T74040.T_GEN_CODE)else (select T_LANG2_NAME from T30004 where T30004.T_GEN_CODE = V30001.GEN_CODE) end GEN_NAME, case when T74040.T_ITEM_CODE is null then (select T_LANG2_NAME from T30004 where T30004.T_GEN_CODE = T74040.T_GEN_CODE) else V30001.PRODUCT_DESC end PRODUCT_DESC, (T_MORNING_DOSE_UNIT   ||'+'   ||T_NOON_DOSE_UNIT   ||'+'   || T_NIGHT_DOSE_UNIT)Instruction FROM T74041 INNER JOIN T74039 ON T74041.T_REQUEST_ID = T74039.T_REQUEST_ID INNER JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID LEFT outer JOIN V30001 ON T74040.T_ITEM_CODE     = V30001.ITEM_CODE WHERE T74041.T_REQUEST_ID = '" + max + "'").ToList();

                //  query = obj.Database.SqlQuery<CommonModel>("select TRADE_DESC,(GEN_DESC ||',' || STRENGTH) Medicine ,(T_MORNING_DOSE_UNIT ||'+'||T_NOON_DOSE_UNIT ||'+'|| T_NIGHT_DOSE_UNIT)Instruction  from T74041 INNER join T74039 on T74041.T_REQUEST_ID = T74039.T_REQUEST_ID INNER join T74040 on T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID INNER join V30001 on T74040.T_ITEM_CODE = V30001.ITEM_CODE where T74041.T_REQUEST_ID = '" + max + "'").ToList();

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
            return query ;
        }

        public IEnumerable BodyMeasurementData(int id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
               // var maxRequest = obj.T74041.Where(m => m.T_PAT_ID == id).Max(x => x.T_REQUEST_ID);
                query = (from t41 in obj.T74041
                    join t43 in obj.T74043 on t41.T_REQUEST_ID equals t43.T_REQUEST_ID 
                    where t41.T_PAT_ID == id
                         select new
                    {
                      T_WEIGHT =  t43.T_WEIGHT,
                      T_TEMP=t43.T_TEMP,
                      T_PULS= t43.T_PULS,
                      T_HIGHT=  t43.T_HIGHT,
                      T_BP_SYS=  t43.T_BP_SYS,
                      T_BP_DIA=  t43.T_BP_DIA,
                      T_BSUGAR_F=  t43.T_BSUGAR_F,
                      T_ENTRY_DATE= t43.T_ENTRY_DATE
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
        public void Dispose()
        {
            obj.Dispose();

        }

        public void Save()
        {
            obj.SaveChanges();
        }

        public string GetSiteCode(string sidId)
        {
            var d = obj.Database.SqlQuery<string>(" select T_SITE_CODE from T74057 where T_USER_ID = '"+ sidId + "'").First();
            var re = Convert.ToString(d);
            return re;
        }

        public int GetRequest(int patId)
        {
            var re = obj.T74041.Where(x => x.T_PAT_ID == patId).Max(p => p.T_REQUEST_ID);
            return re;
        }
    }
}