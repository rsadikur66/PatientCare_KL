using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface;
using Newtonsoft.Json;


namespace AmbucareDAL.Repository
{
    public class LoginRepository : CommonDAL,ILogin
    {
        private AmbucareContainer obj = new AmbucareContainer();
        //public LoginRepository() { }
        public LoginRepository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        
        public IEnumerable GetUserInfo(string LOGIN_NAME, string PWD)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //string emessage = "";
            try
            {

                query = obj.T74057
                    .Join(obj.T74004, ab => ab.T_EMP_ID, c => c.T_EMP_ID, (ab, c) => new { ab, c })
                    .Select(m => new
                    {
                        T_USER_ID = m.ab.T_USER_ID,
                        T_PWD = m.ab.T_PWD,
                        T_EMP_ID = m.ab.T_EMP_ID,
                        T_COMPANY_ID = m.ab.T_COMPANY_ID,
                        T_ROLE_CODE = m.ab.T_ROLE_CODE,
                        EMP_NAME1 = m.c.T_FIRST_LANG2_NAME,
                        EMP_NAME2 = m.c.T_LAST_LANG2_NAME,
                        //EMP_NAME2 = m.c.T_FATHER_LANG2_NAME,
                        //EMP_NAME3 = m.c.T_GFATHER_LANG2_NAME,
                        //EMP_NAME4 = m.c.T_FAMILY_LANG2_NAME,
                        T_SITE_CODE = m.ab.T_SITE_CODE,
                        T_ZONE_CODE = m.ab.T_ZONE_CODE,

                    }).Where(a => a.T_USER_ID == LOGIN_NAME.ToUpper() && a.T_PWD == PWD.ToUpper()).ToList().Select(
                        (a, i) => new
                        {
                            RowNumber = i,
                            T_USER_ID = a.T_USER_ID,
                            T_PWD = a.T_PWD,
                            T_EMP_ID = a.T_EMP_ID,
                            T_COMPANY_ID = a.T_COMPANY_ID,
                            T_ROLE_CODE = a.T_ROLE_CODE,
                            T_SITE_CODE = a.T_SITE_CODE,
                            T_ZONE_CODE = a.T_ZONE_CODE,
                            EMP_NAME = a.EMP_NAME1 + " " + a.EMP_NAME2
                        });

                // chekkBlob();



            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
            //        }
            //    }
            //}
            catch (Exception e)
            {
                //query = e.InnerException != null ? e.InnerException.Message : e.Message;

            }
            return query;
           
        }

        public IEnumerable chekkBlob()
        {
            //query
            var query = from a in obj.T02003
                            //where a.T_NTNLTY_ID == 152
                        select a.T_LANG1_NAME;
           
          //var result= ObjectToByteArray(query);
            //get objects into memory
            var datadaa = query.ToList();

            //apply transformation:
            //var result = (from a in datadaa select new { Article = Encoding.UTF8.GetString(a) }).ToList();

            return "";

        }

        public IEnumerable<object> ObjectToByteArray(IQueryable<byte[]> data)
        {
            var dataObject = data.ToList();
            var result = (from a in dataObject select new {Article = Encoding.UTF8.GetString(a)}).ToList();
            
            return result;
        }
        public IEnumerable checkAmbulance(string USER_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {
                //query=(from t57 in obj.t74)






                query = obj.T74057
                     .Join(obj.T74094, ab => ab.T_EMP_ID, c => c.T_EMP_ID, (ab, c) => new { ab, c })
                     .Join(obj.T74095, pq => pq.c.T_ORDER_ID, q => q.T_ORDER_ID, (pq, q)=>new {pq,q})
                     .Select(m => new
                     {
                         T_USER_ID=m.pq.ab.T_USER_ID,
                         T_REPAIR_STATUS = m.q.T_REPAIR_STATUS

                     }).Where(a => a.T_USER_ID == USER_ID).ToList().Select(
                         (a, i) => new
                         {
                             RowNumber = i,
                             T_USER_ID = a.T_USER_ID,
                             T_PWD = a.T_REPAIR_STATUS
                         });




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
        public void UpdateLogin(string userId)
        {
            BeginTransaction();

            if (Command($"UPDATE T74057 SET T_LOGIN_STATUS = 1  WHERE T_USER_ID ='{userId.ToUpper()}'"))
            {
                CommitTransaction();
            }
            else
            {
                RollbackTransaction();
            }
            //if (CommonClass.CheckForInternetConnection())
            //{
                var data = obj.T74057.Where(a => a.T_USER_ID == userId).FirstOrDefault();
                if (data != null)
                {
                    data.T_LOGIN_STATUS = "1";
                    obj.SaveChanges();
                }
            //}
            

        }
        public void UpdateLogout()
        {
            string id = null;
            if (HttpContext.Current.Session["T_USER_ID"]!=null)
            {
                id = HttpContext.Current.Session["T_USER_ID"].ToString();
                BeginTransaction();
                if (Command($"UPDATE T74057 SET T_LOGIN_STATUS = ''  WHERE T_USER_ID ='{id}'"))
                {
                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                }

                //if (CommonClass.CheckForInternetConnection())
                //{
                    var data = obj.T74057.Where(a => a.T_USER_ID == id).FirstOrDefault();
                if (data != null)
                {
                    data.T_LOGIN_STATUS = "";
                    obj.SaveChanges();
                }
                //}
            }
         

        }

        public void Dispose()
        {
            obj.Dispose();
        }
    }
}