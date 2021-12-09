using AmbucareDAL.Models;
using AmbucareDAL.Repository.Hospital.Interface.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.Hospital.Implementation.Menu
{
    public class HMenuRepository 
    {
        //private HospitalEntities obj = new HospitalEntities();
        //public HMenuRepository(HospitalEntities _Obj)
        //{
        //    _Obj.Configuration.ProxyCreationEnabled = false;
        //    obj = _Obj;
        //}
        //public IQueryable<T02001> GetAllData
        //{
        //    get { return obj.T02001.OrderBy(x => x.T_USER_ID); }
        //}
        //public IEnumerable MenuData(string T_USER_ID, Int32 T_FORM_TYPE_ID)
        //{
        //    IEnumerable query = Enumerable.Empty<object>();

        //    try
        //    {
        //        query = obj.T02001.Join(obj.T02095, t01 => t01.T_FORM_TYPE_ID, t95 => t95.T_FORM_TYPE_ID,
        //                (t01, t95) => new { t01, t95 })
        //                .Join(obj.T02096, t01_70 => t01_70.t01.T_PAGE_TYPE_ID, t96 => t96.T_PAGE_TYPE_ID, (t01_70, t96) => new { t01_70, t96 })
        //            .Select(m => new
        //            {
        //                T_ORDER = m.t01_70.t01.T_ORDER,
        //                T_USER_ID = m.t01_70.t01.T_USER_ID,
        //                T_FORM_CODE_ID = m.t01_70.t01.T_FORM_CODE_ID,
        //                T_FORM_CODE = m.t01_70.t01.T_FORM_CODE,
        //                T_LANG2_NAME = m.t01_70.t01.T_LANG2_NAME,
        //                T_FORM_TYPE_ID = m.t01_70.t01.T_FORM_TYPE_ID,
        //                T_INACTIVE_FLAG = m.t01_70.t01.T_INACTIVE_FLAG,
        //                Menu_Name = m.t01_70.t95.T_LANG2_NAME,
        //                Page_Type = m.t96.T_PAGE_TYPE_ID,
        //                Page_Type_Name = m.t96.T_LANG2_NAME
        //            }).Where(a => a.T_USER_ID == T_USER_ID && a.T_FORM_TYPE_ID == T_FORM_TYPE_ID && a.T_INACTIVE_FLAG == "1").ToList().OrderBy(z => z.Page_Type).ThenBy(z => z.T_ORDER).Select(
        //                (a, i) => new
        //                {
        //                    RowNumber = i,
        //                    T_FORM_TYPE_ID = a.T_FORM_TYPE_ID,
        //                    T_FORM_CODE = a.T_FORM_CODE,
        //                    T_LANG2_NAME = a.T_LANG2_NAME,
        //                    Menu_Name = a.Menu_Name,
        //                    Page_Type = a.Page_Type,
        //                    Page_Type_Name = a.Page_Type_Name

        //                });


        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }

        //    return query;
        //}

        //public IQueryable<T02001> FormAuthorization(string T_USER_ID, string T_FORM_CODE)
        //{
        //    var data = obj.T02001.Where(x => x.T_FORM_CODE == T_FORM_CODE && x.T_USER_ID == T_USER_ID);
        //    return data;
        //}

        //public IQueryable<T01200> LabelData(string T_FORM_CODE, string T_LABEL_NAME)
        //{
        //    var data = obj.T01200.Where(x => x.T_FORM_CODE == T_FORM_CODE && x.T_LABEL_NAME == T_LABEL_NAME);
        //    return data;
        //}

        //public IEnumerable LoginData(string T_USER_ID)
        //{
        //    AmbucareContainer obj = new AmbucareContainer();
        //    IEnumerable query = Enumerable.Empty<object>();

        //    try
        //    {
        //        query = obj.T74057.Join(obj.T74071, a => a.T_ROLE_CODE, b => b.T_ROLE_CODE, (a, b) => new { a, b })
        //             .Join(obj.T74004, ab => ab.a.T_EMP_ID, c => c.T_EMP_ID, (ab, c) => new { ab, c })
        //             .Select(m => new
        //             {
        //                 T_USER_ID = m.ab.a.T_USER_ID,
        //                 T_PWD = m.ab.a.T_PWD,
        //                 T_ROLE_CODE = m.ab.a.T_ROLE_CODE,
        //                 T_EMP_ID = m.ab.a.T_EMP_ID,
        //                 T_COMPANY_ID = m.ab.a.T_COMPANY_ID,
        //                 ROLE_NAME = m.ab.b.T_LANG2_NAME,
        //                 EMP_NAME1 = m.c.T_FIRST_LANG2_NAME,
        //                 EMP_NAME2 = m.c.T_FATHER_LANG2_NAME,
        //                 EMP_NAME3 = m.c.T_GFATHER_LANG2_NAME,
        //                 EMP_NAME4 = m.c.T_FAMILY_LANG2_NAME,

        //             }).Where(a => a.T_USER_ID == T_USER_ID).ToList().Select(
        //                 (a, i) => new
        //                 {
        //                     RowNumber = i,
        //                     T_USER_ID = a.T_USER_ID,
        //                     T_PWD = a.T_PWD,
        //                     T_ROLE_CODE = a.T_ROLE_CODE,
        //                     T_EMP_ID = a.T_EMP_ID,
        //                     T_COMPANY_ID = a.T_COMPANY_ID,
        //                     ROLE_NAME = a.ROLE_NAME,
        //                     EMP_NAME = a.EMP_NAME1 + " " + a.EMP_NAME2 + " " + a.EMP_NAME3 + " " + a.EMP_NAME4
        //                 });




        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //    return query;

        //}

        //public void Dispose()
        //{
        //    obj.Dispose();
        //}
    }
}