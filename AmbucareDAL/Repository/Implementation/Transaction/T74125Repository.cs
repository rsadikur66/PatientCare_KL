using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;
using Microsoft.Ajax.Utilities;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74125Repository : IT74125
    {
        private AmbucareContainer obj = new AmbucareContainer();

        CommonDAL common = new CommonDAL();
        T74131DAL _t74131Dal = new T74131DAL();
        public T74125Repository(AmbucareContainer _obj)
        {
            obj.Configuration.ProxyCreationEnabled = false;
            this.obj = _obj;

        }

        public string getReason(string T_DISCH_RSN_CODE, string lang)
        {
            string msg = "";
            byte reason = Convert.ToByte(T_DISCH_RSN_CODE);

            msg = (from t04 in obj.T74104
                where t04.T_DISCH_ID == reason
                select lang=="2"?t04.T_LANG2_NAME: t04.T_LANG1_NAME).FirstOrDefault();
            return msg;

        }
        public IEnumerable GetPatientDetails(int patId, string userId,int rquestId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                //var maxReq = obj.T74041.Where(x => x.T_PAT_ID == patId && x.T_USER_ID==userId && x.T_DISCH_STATUS== null).Max(a => a.T_REQUEST_ID);
                query = (from t46_Pat in obj.T74046
                    join t41 in obj.T74041 on t46_Pat.T_PAT_ID equals t41.T_PAT_ID
                    join t44_Amb in obj.T74044 on t41.T_AMBU_REG_ID equals t44_Amb.T_AMBU_REG_ID
                    //join t04 in obj.T74104 on Int32.Parse(t41.T_DISCH_RSN_CODE) equals t04.T_DISCH_ID
                    //into T74104
                    //from to4L in T74104.DefaultIfEmpty()
                        // join t04 in obj.T74104 on t41.T_DISCH_RSN_CODE equals t04.T_DISCH_ID 
                        // where t46_Pat.T_PAT_ID == patId && t41.T_DISCH_STATUS == null && t41.T_REQUEST_ID == maxReq
                         where t41.T_REQUEST_ID == rquestId
                         select new
                    {
                        RequeID = t41.T_REQUEST_ID,
                        T_DISCH_RSN_CODE = t41.T_DISCH_RSN_CODE,
                        T_DISCH_RSN_REMARKS = t41.T_DISCH_RSN_REMARKS,
                        PatentId = t46_Pat.T_PAT_ID,
                        Name = t46_Pat.T_FIRST_LANG2_NAME,
                        FatherName=t46_Pat.T_FATHER_LANG2_NAME,
                        GrandFatherName = t46_Pat.T_GFATHER_LANG2_NAME,
                        MotherName=t46_Pat.T_MOTHER_LANG2_NAME,
                        Mobile = t46_Pat.T_MOBILE_NO,
                        Problem = t41.T_PROB_DETAILS,
                        BirthDate = t41.T_AGE,
                        Ambulance = t44_Amb.T_LANG2_NAME,
                        T_DISCH_STATUS=t41.T_DISCH_STATUS,
                        T_EVENT_FLAG = t41.T_EVENT_FLAG,
                             // Bill = Bill( Convert.ToInt32( t46_Pat.T_PAT_ID))   T_DISCH_RSN_CODE T_DISCH_RSN_REMARKS
                             //  }).ToList();
                         }).AsEnumerable().Select((r, i) => new
                         {
                             RowNumber = i,
                             RequeID = r.RequeID,
                             T_DISCH_RSN_CODE = r.T_DISCH_RSN_CODE,
                             T_DISCH_NAME = getReason(r.T_DISCH_RSN_CODE, "2"),
                             T_DISCH_RSN_REMARKS = r.T_DISCH_RSN_REMARKS,
                             PatentId = r.PatentId,
                             Name = r.Name +" "+r.FatherName + " " +r.GrandFatherName,
                            // FatherName = t46_Pat.T_FATHER_LANG2_NAME,
                            // GrandFatherName = t46_Pat.T_GFATHER_LANG2_NAME,
                            // MotherName = t46_Pat.T_MOTHER_LANG2_NAME,
                             Mobile = r.Mobile,
                             Problem = r.Problem,
                             BirthDate = r.BirthDate,
                             Ambulance = r.Ambulance,
                             T_DISCH_STATUS=r.T_DISCH_STATUS,
                             T_EVENT_FLAG = r.T_EVENT_FLAG,
                         }).ToList();//.ToList();
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

        public IEnumerable GetBill(int patId)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {
                var maxReq = obj.T74041.Where(x => x.T_PAT_ID == patId).Max(a => a.T_REQUEST_ID);
                query = (from t74 in obj.T74074
                    join t74_Bil in obj.T74074 on t74.T_REQUEST_ID equals t74_Bil.T_REQUEST_ID 
                    where t74_Bil.T_REQUEST_ID == maxReq
                    select new
                    {
                        Request = t74_Bil.T_REQUEST_ID,
                        Bill = t74_Bil.T_TOTAL_PRICE
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
        public IEnumerable GetBillDetails(int request)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {
              
                query = (from t74 in obj.T74074
                    join t79 in obj.T74079 on t74.T_BILL_ID equals t79.T_BILL_ID
                    join t73 in obj.T74073 on t79.T_COST_TYPE_DTL_ID equals t73.T_COST_TYPE_DTL_ID
                   // join t37 in obj.T74037 on t74.T_ISSUE_ID equals t37.T_ISSUE_ID
                    where t74.T_REQUEST_ID == request
                         select new
                    {
                        T_LANG2_NAME= t73.T_LANG2_NAME,
                        T_PRICE= t79.T_PRICE,
                        T_VAT=  t79.T_VAT != null? t79.T_VAT:0,
                        T_DISCOUNT=t79.T_DISCOUNT != null ? t79.T_DISCOUNT : 0,

                      //  price = t37.T_SALE_PRICE + t37.T_QUANTITY
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

        public IEnumerable GetIssueDetails(int request)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {
                query = obj.Database.SqlQuery<CommonModel>("select ITEM_CODE, GEN_DESC, t74037.T_SALE_PRICE SALE_PRICE,  sum(t74037.T_QUANTITY) T_QUANTITY,  sum(t74037.T_TOTAL_AMOUNT) T_TOTAL_AMOUNT from T74074 inner join t74037 on T74074.T_ISSUE_ID = t74037.T_ISSUE_ID inner join V30001 on t74037.T_ITEM_ID = V30001.ITEM_CODE where T74074.T_REQUEST_ID = '" + request + "' group by ITEM_CODE,  GEN_DESC,T_SALE_PRICE").ToList();

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

        public IEnumerable GetIssueSumary(int request)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {

                query = (from t36 in obj.T74036
                    join t36_sumary in obj.T74036 on t36.T_ISSUE_ID equals t36_sumary.T_ISSUE_ID
                    where t36_sumary.T_REQUEST_ID == request
                    select new
                    {
                        T_DISCOUNT= t36.T_DISCOUNT !=null? t36.T_DISCOUNT:0,
                        T_VAT= t36.T_VAT != null ? t36.T_VAT : 0,
                        T_GRAND_TOTAL=  t36.T_GRAND_TOTAL != null ? t36.T_GRAND_TOTAL : 0,
                        T_TOTAL= t36.T_TOTAL != null ? t36.T_TOTAL : 0
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
        //GetBillDetails(int request);
        public IEnumerable PatData(int T_PAT_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {
                int req = obj.T74041.Where(p => p.T_PAT_ID == T_PAT_ID).Max(a => a.T_REQUEST_ID);
                query = (from t46 in obj.T74046
                    join t41 in obj.T74041 on t46.T_PAT_ID equals t41.T_PAT_ID
                    join t43 in obj.T74043 on t41.T_REQUEST_ID equals t43.T_REQUEST_ID
                    join t03 in obj.T02003 on t46.T_NTNLTY_ID equals t03.T_NTNLTY_ID into t03_t46
                    from a in t03_t46.DefaultIfEmpty()
                    where t46.T_PAT_ID == T_PAT_ID
                          && t41.T_REQUEST_ID == req
                         select new
                    {
                        t41.T_ENTRY_DATE,
                        t46.T_ENTRY_USER,
                        t46.T_FIRST_LANG2_NAME,
                        t46.T_FIRST_LANG1_NAME,
                        t46.T_FATHER_LANG2_NAME,
                        t46.T_FATHER_LANG1_NAME,
                        t46.T_GFATHER_LANG2_NAME,
                        t46.T_GFATHER_LANG1_NAME,
                        t46.T_FAMILY_LANG2_NAME,
                        t46.T_FAMILY_LANG1_NAME,
                        t46.T_MOTHER_LANG2_NAME,
                        t46.T_MOTHER_LANG1_NAME,
                        t46.T_BIRTH_DATE,
                        t46.T_RLGN_CODE,
                        t46.T_MRTL_STATUS,
                        t46.T_SEX_CODE,
                        a.T_NTNLTY_CODE,
                        t46.T_NTNLTY_ID,
                        t46.T_PAT_ID,
                        t46.T_ADDRESS1,
                        t46.T_POSTAL_CODE,
                        t46.T_PHONE_HOME,
                        t46.T_PASSPORT_NO,
                        t43.T_REQUEST_ID,
                        t43.T_HIGHT,
                        t43.T_WEIGHT,
                        t43.T_BP_SYS,
                        t43.T_BP_DIA,
                        t43.T_TEMP,
                        t43.T_PULS,
                        t41.T_CH_COMP,
                        t41.T_PROB_TYPE_ID
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
        public void UpdateT74046(int T_PAT_ID,string T_DISCH_DEST)
        {
            int req = obj.T74041.Where(p => p.T_PAT_ID == T_PAT_ID).Max(a => a.T_REQUEST_ID);
            T74041 t41 = obj.T74041.Where(a => a.T_REQUEST_ID == req).FirstOrDefault();
                if (t41 != null)
                {
                    t41.T_DISCH_DEST = T_DISCH_DEST;
                    Save();
                }
            
        }

        public string UpdateT74041(T74041 t74041)
        {
            string sms = "";
            string dischargeCondition = "";
            try
            {
                var chk = obj.T74041.Where(x => x.T_REQUEST_ID == t74041.T_REQUEST_ID).FirstOrDefault();
                if (chk != null)
                {
                    //chk.T_DISCH_STATUS = t74041.T_DISCH_STATUS;
                    chk.T_DISCH_RSN_CODE = t74041.T_DISCH_RSN_CODE;
                    chk.T_DISCH_RSN_REMARKS = t74041.T_DISCH_RSN_REMARKS;
                   
                    chk.T_CHAT_FLAG = null; //t74041.T_DISCHARGE_TIME;
                    if (t74041.T_EVENT_FLAG ==3)
                    {
                        chk.T_EVENT_FLAG = 11;
                        chk.T_RDY_FOR_NXT_PAT = common.dateTime();
                        chk.T_DISCH_STATUS = t74041.T_DISCH_STATUS;
                        // sms = "Discharge Successfully";
                        dischargeCondition = "Menu";
                    }
                    else
                    {
                        chk.T_DISCHARGE_DATE = common.dateTime();//t74041.T_DISCHARGE_DATE;
                        chk.T_DISCHARGE_TIME = common.dateTime(); //t74041.T_DISCHARGE_TIME;
                        chk.T_EVENT_FLAG = 8;
                       // sms = "Discharge Successfully";
                        dischargeCondition = "None";
                    }
                   
                    Save();
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
            return dischargeCondition;
        }

       public DataTable GetDischargeReason()
       {
           return common.Query($"select T_DISCH_ID,T_LANG2_NAME from t74104");
       }
        public void Save()
        {
            obj.SaveChanges();
        }
        public void Dispose()
        {
            obj.Dispose();
        }


        //----------------------Verify disharge summery report------
        public string verifySummeryReport(int requestId)
        {
            var data = _t74131Dal.verifySummeryReport(requestId);
            return data;
        }
    }
}