using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;
using System.Collections;
using System.Data;
using System.Reflection;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Hospital.Implementation.Initialization;
using AmbucareDAL.Repository.Interface.Transaction;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74131Repository :IT74131
    {
        CommonDAL common = new CommonDAL();
        T74131DAL _t74131Dal=new T74131DAL();
        private AmbucareContainer obj = new AmbucareContainer();
        
        public T74131Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }
        public string GetSite()
        {
            IEnumerable query = Enumerable.Empty<object>();
             
            try
            {
                string id = HttpContext.Current.Session["T_USER_ID"].ToString();
                query = (from t96 in obj.T74096.Where(a => a.T_AMBU_REG_ID != null && a.T_SITE_CODE != null)
                    join t15 in obj.T74015 on t96.T_AMBU_REG_ID equals t15.T_AMBU_REG_ID
                    join t41 in obj.T74041.Where(a => a.T_DISCH_STATUS != "1") on
                        t96.T_AMBU_REG_ID equals t41.T_AMBU_REG_ID
                    join t04 in obj.T74004 on t15.T_EMP_ID equals t04.T_EMP_ID
                    join t05 in obj.T74005.Where(a => a.T_EMP_TYP_ID == 21 || a.T_EMP_TYP_ID == 2) on t04.T_EMP_TYP_ID equals t05.T_EMP_TYP_ID
                    join t57 in obj.T74057.Where(a => a.T_USER_ID ==id ) on t04.T_EMP_ID equals t57.T_EMP_ID
                    join t40 in obj.T02040 on t41.T_PROB_TYPE_ID equals t40.T_SPCLTY_ID
                    select t96.T_SITE_CODE).ToList();
                      
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
            
                string list = "";
                foreach (var siteCode in query)
                {
                    list += siteCode + ",";

                }

               string Code = list.TrimEnd(',');
          
           

            return Code;
        }
        public DataTable GetPatInfo(string P_USER_ID)
        {
            var data = _t74131Dal.GetPatInfo(P_USER_ID);
            return data;
        }
        public DataTable GetPreviousMedicine(int requId)
        {
            var data = _t74131Dal.GetPreviousMedicine(requId);
            return data;
        }
        public DataTable GetChatHistory(int T_REQUEST_ID,string T_SENDER_ID, string T_RECIEVER_ID)
        {
            var data = _t74131Dal.GetChatHistory(T_REQUEST_ID,T_SENDER_ID, T_RECIEVER_ID);
            return data;
        }
        public void chatHistory(T74098 t74098)
        {
            t74098.T_AUTO_ID = Int32.Parse(common.Query($"SELECT TO_NUMBER(NVL(MAX(T_AUTO_ID),0)+1) T_AUTO_ID FROM T74098").Rows[0]["T_AUTO_ID"].ToString());
            t74098.T_REF_ID = Int32.Parse(common.Query($"SELECT TO_NUMBER(NVL(MAX(T_AUTO_ID),0)) T_AUTO_ID FROM T74098 WHERE T_SENDER_ID='{t74098.T_SENDER_ID}' OR T_SENDER_ID = '{t74098.T_RECIEVER_ID}' AND T_RECIEVER_ID='{t74098.T_SENDER_ID}' OR T_RECIEVER_ID= '{t74098.T_RECIEVER_ID}'").Rows[0]["T_AUTO_ID"].ToString());
            t74098.T_TIME = common.dateTime();
            obj.T74098.Add(t74098);
            obj.SaveChanges();
            // _t74131Dal.chatHistory(t74098);
            
        }
        public string SaveSuggestedMedicine(int requestID, List<T74040> save40List, string lang)
       {
           string sms = "";
            T74039 t74039 = new T74039();
           var doc = (from t41 in obj.T74041
               join t57 in obj.T74057 on t41.T_USER_ID equals t57.T_USER_ID
               where t41.T_REQUEST_ID == requestID
               select new
               {
                   t57.T_EMP_ID
               }).Single();
                     
                  

           t74039.T_REQUEST_ID = requestID;
           t74039.T_DOC_ID = doc.T_EMP_ID;
           obj.T74039.Add(t74039);
           obj.SaveChanges();
            int T_PRESCRIPTION_ID = obj.T74039.Where(x => x.T_REQUEST_ID == requestID).Max(b => b.T_PRESCRIPTION_ID);
           foreach (var ta40 in save40List)
           {
               int T_PRSCRPTN_DTL_ID = obj.T74040.Count() > 0 ? obj.T74040.Max(a => a.T_PRSCRPTN_DTL_ID) : 0;
                T74040 t40 = new T74040();
               t40.T_PRSCRPTN_DTL_ID = T_PRSCRPTN_DTL_ID != 0 ? T_PRSCRPTN_DTL_ID + 1 : 1;
                t40.T_PRESCRIPTION_ID = T_PRESCRIPTION_ID;
               t40.T_ITEM_CODE = ta40.T_ITEM_CODE;
               t40.T_DOSE_DURATION_CODE = ta40.T_DOSE_DURATION_CODE;
               obj.T74040.Add(t40);
               obj.SaveChanges();
                // sms = "Save successfully";
               sms = common.GetSingleMsg(lang, "S0012");
            }
           return sms;
       }

       public string saveRequestHospital(int requestId, string hosId, string lang)
       {
           string sms = "";
           var chk = obj.T74041.Where(b => b.T_REQUEST_ID == requestId).FirstOrDefault();
           if (chk != null)
           {
               chk.T_REF_DOC_TIME = common.dateTime();
               chk.T_REF_DOC_CODE = hosId;
               obj.SaveChanges();
               //sms = common.GetSingleMsg(lang, "S0073");
                 sms = _t74131Dal.reqHos(requestId, hosId,chk.T_CHAT_DOC_ID, lang);
            }
           
            return sms;

       }
        public string setDoc(string uid)
        {
            //int req = _t74131Dal.setDoc(uid);
            //if (req != 0)
            //{
            //    T74041 t41 = obj.T74041.Where(a => a.T_REQUEST_ID == req).FirstOrDefault();
            //    if (t41 != null)
            //    {
            //        t41.T_CHAT_FLAG = "1";
            //        t41.T_CHAT_DOC_ID = uid;
            //        obj.SaveChanges();
            //    }

            //}
            return "";

        }

        //----------------------Connectivity---------------------------------
        public DataTable reqListForTeam(string user, string zone)
        {
            var data = _t74131Dal.reqListForTeam(user, zone);
            return data;

        }
        public DataTable reqListForDoc(string user, string zone)
        {
            var data = _t74131Dal.reqListForDoc(user, zone);
            return data;

        }
        public string acptReqofDoc(string user, string zone)
        {
            var data = _t74131Dal.acptReqofDoc(user, zone);
            return data;

        }
        public string closeChat(string user, int req,string lang)
        {
            var data = _t74131Dal.closeChat(user, req, lang);
            return data;

        }
        public string onRcvPatReq(string user, int req, string lang)
        {
            var data = _t74131Dal.onRcvPatReq(user, req, lang);
            return data;

        }
        public string conWthDoc(string user, string zone)
        {
            var data = _t74131Dal.conWthDoc(user, zone);
            return data;

        }
        public int chkConn(string user, string zone)
        {
            var data = _t74131Dal.chkConn(user, zone);
            return data;

        }
        public DataTable getDocID(string user)
        {
            var data = _t74131Dal.getDocID(user);
            return data;

        }


        //---------------------------------------------------

        public string chkReqHos(int requestId, string lang)
        {
            var data = _t74131Dal.chkReqHos(requestId,lang);
            return data;

        }
        public string setDoc_new(string zone,int req, string user,string doc)
        {
            var data = _t74131Dal.setDoc_new(zone,req, user, doc);
            return data;

        }
        public string setNewConv(int req, string doc)
        {
            var data = _t74131Dal.setNewConv(req,doc);
            return data;

        }
        public string emergenceyReq(int req, string text, string user)
        {
            var data = _t74131Dal.emergenceyReq(req, text, user);
            return data;

        }
        public DataTable getPatMsg(int req)
        {
            var data = _t74131Dal.getPatMsg(req);
            return data;

        }
        public int chkPat(string req)
        {
            var data = _t74131Dal.chkPat(req);
            return data;

        }
        public DataTable getPlaceHolder(string label)
        {
            var data = _t74131Dal.getPlaceHolder(label);
            return data;

        }
        public void Dispose()
        {
            obj.Dispose();
        }

    }
}