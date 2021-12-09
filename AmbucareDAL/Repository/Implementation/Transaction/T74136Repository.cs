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
    public class T74136Repository : IT74136
    {
        CommonDAL common = new CommonDAL();
        private AmbucareContainer obj = new AmbucareContainer();
        private readonly T74136DAL t74136DAL = new T74136DAL();
        public T74136Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public void Dispose()
        {
            obj.Dispose();
        }
        public void Save()
        {
            obj.SaveChanges();
        }
        public string maxUserId()
        {
            var data = t74136DAL.maxUserId();
            return data;
        }
        public DataTable getEmpList(string lang, string rolcode, string user)
        {
            var data = t74136DAL.getEmpList(lang, rolcode,user);
            return data;
        }
        public DataTable getRole(string lang,string rolcode)
        {
            var data = t74136DAL.getRole(lang, rolcode);
            return data;
        }
        public DataTable getZone(string lang)
        {
            var data = t74136DAL.getZone(lang);
            return data;
        }

        public DataTable CheckUserId(string userId)
        {
            var data = t74136DAL.CheckUserId(userId);
            return data;
        }

        public string saveUser(T74057 _t74057, string entyuser)
        {
            string mesg = "";
            try
            {
                bool Status = false;
                var data = obj.T74057.Where(p => p.T_USER_ID.ToUpper() == _t74057.T_USER_ID.ToUpper()).FirstOrDefault();

                if (data == null)
                {
                    _t74057.T_ENTRY_USER = entyuser;
                    _t74057.T_ENTRY_DATE = common.dateTime();
                    _t74057.T_COMPANY_ID = 1;
                    obj.T74057.Add(_t74057);
                    Save();
                    //t74136DAL.InsertT74026(_t74057.T_USER_ID,_t74057.T_LATITUDE,_t74057.T_LONGITUDE);
                    t74136DAL.InsertT74026(_t74057.T_USER_ID, "", "");
                    mesg = "1";
                }
                else
                {
                    data.T_PWD = _t74057.T_PWD;
                    data.T_ACTIVE_FLAG = _t74057.T_ACTIVE_FLAG;
                    data.T_EMP_ID = _t74057.T_EMP_ID;
                    data.T_ZONE_CODE = _t74057.T_ZONE_CODE;
                    data.T_UPD_USER = _t74057.T_ENTRY_USER;
                    data.T_COMPANY_ID = 1;
                    Save();
                    mesg = "2";


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
            return mesg;

        }

        public DataTable GetGridData(string lang, string user, string zone)
        {
            var data = t74136DAL.GetGridData(lang, user, zone);
            return data;
        }

        public DataTable getMaxValue(string user, string type)
        {
            var data = t74136DAL.getMaxValue(user,type);
            return data;
        }
        //public string saveUser(T74057 _t74057)
        //{
        //    var msg = "";
        //    using (var dbContextTransaction = obj.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            //var existingValue = obj.T74073.Where(x => x.T_ID == _t74057.T_ID).FirstOrDefault();
        //            //if (existingValue == null)
        //            //{
        //                obj.T74057.Add(_t74057);
        //                obj.SaveChanges();
        //            //}
        //            //dbContextTransaction.Commit();
        //        }
        //        catch (DbEntityValidationException dbEx)
        //        {
        //            dbContextTransaction.Rollback();
        //            foreach (var validationErrors in dbEx.EntityValidationErrors)
        //            {
        //                foreach (var validationError in validationErrors.ValidationErrors)
        //                {
        //                    Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                }
        //            }
        //        }
        //    }
        //    return msg = "Save Successfully";
        //}
    }
}