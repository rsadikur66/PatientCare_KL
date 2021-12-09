using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Query;
using AmbucareDAL.Repository.DAL.Setup;
using AmbucareDAL.Repository.Interface.Queries;

namespace AmbucareDAL.Repository.Implementation.Queries
{
    public class Q74143Repository: IQ74143
    {
        private readonly Q74143DAL _q74143 = new Q74143DAL();
        private AmbucareContainer obj = new AmbucareContainer();
        CommonDAL common = new CommonDAL();
        public Q74143Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        public DataTable getAllGridData(string lang)
        {
            var data = _q74143.getAllGridData(lang);
            return data;
        }

        public bool UpdateT74041(int req)
        {
            try
            {
                T74041 t41 = obj.T74041.Where(a => a.T_REQUEST_ID == req).FirstOrDefault();
                if (t41 != null)
                {
                    t41.T_DISCH_STATUS = "1";
                    t41.T_DISCHARGE_DATE = common.dateTime();
                    t41.T_DISCHARGE_TIME = common.dateTime();
                    t41.T_CHAT_FLAG = null;
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
            return true;
        }
        public bool UpdateT74057(string T_USER_ID)
        {
            try
            {
                T74057 t57 = obj.T74057.Where(a => a.T_USER_ID == T_USER_ID).FirstOrDefault();
                if (t57 != null)
                {
                    t57.T_LOGIN_STATUS = "1";
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
            return true;
        }
        public void Save()
        {
            obj.SaveChanges();
        }
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}