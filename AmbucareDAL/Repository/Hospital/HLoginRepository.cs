using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Hospital.Interface;

namespace AmbucareDAL.Repository.Hospital
{
    public class HLoginRepository: HILogin
    {
        private AmbucareContainer obj = new AmbucareContainer();
       

        public HLoginRepository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        
        public IEnumerable GetUserInfo(string LOGIN_NAME, string PWD)
        {
            IEnumerable query = Enumerable.Empty<object>();

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
                         EMP_NAME1 = m.c.T_FIRST_LANG2_NAME,
                         EMP_NAME2 = m.c.T_FATHER_LANG2_NAME,
                         EMP_NAME3 = m.c.T_GFATHER_LANG2_NAME,
                         EMP_NAME4 = m.c.T_FAMILY_LANG2_NAME,

                     }).Where(a => a.T_USER_ID == LOGIN_NAME && a.T_PWD == PWD).ToList().Select(
                         (a, i) => new
                         {
                             RowNumber = i,
                             T_USER_ID = a.T_USER_ID,
                             T_PWD = a.T_PWD,
                             T_EMP_ID = a.T_EMP_ID,
                             T_COMPANY_ID = a.T_COMPANY_ID,                            
                             EMP_NAME = a.EMP_NAME1 + " " + a.EMP_NAME2 + " " + a.EMP_NAME3 + " " + a.EMP_NAME4
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

        public void Dispose()
        {
            obj.Dispose();
        }
    }
}