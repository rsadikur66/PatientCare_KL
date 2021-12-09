using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74004Repository : IT74004
    {
        private AmbucareContainer obj = new AmbucareContainer();
        //private T74150DAL _t74150Dal = new T74150DAL();
        public T74004Repository(AmbucareContainer _Obj)
        {
            // _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IQueryable<T74051> MaritalData
        {

            get { return obj.T74051; }

        }

        public IQueryable<T74069> BloodGroupData
        {

            get { return obj.T74069; }

        }
        public IQueryable<T74050> GenderData
        {

            get { return obj.T74050; }

        }
        public IQueryable<T74059> ReligionData
        {

            get { return obj.T74059; }

        }

        public IQueryable<T74004> GetEmployeeData
        {

            get { return obj.T74004; }

        }
        public IQueryable<T74005> EmployeeTypeData
        {

            get { return obj.T74005; }

        }
        //public IQueryable<T02003> NationalityData
        //{
        //    get { return obj.T02003; }
        //}

        public IQueryable<T02003> NationalityData
        {
            get { return obj.T02003; }
        }



        //NationalityData
        public IEnumerable CheckPassportData(T74004 _t74004)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Em in obj.T74004
                    join Ge in obj.T74050 on Em.T_SEX_CODE equals Ge.T_SEX_CODE into EmGe
                    from Ge1 in EmGe.DefaultIfEmpty()
                    join Ma in obj.T74051 on Em.T_MRTL_STATUS_CODE equals Ma.T_MRTL_STATUS_CODE into EmMa
                    from Ma1 in EmMa.DefaultIfEmpty()
                    join Re in obj.T74059 on Em.T_RLGN_CODE equals Re.T_RLGN_CODE into EmRe
                    from Re1 in EmRe.DefaultIfEmpty()
                    join Bl in obj.T74069 on Em.T_BLD_GROUP_ID equals Bl.T_BLD_GROUP_ID into EmBl
                    from Bl1 in EmBl.DefaultIfEmpty()
                    //join Na in obj.T02003 on Em.T_NTNLTY_ID equals Na.T_NTNLTY_ID
                    join Na in obj.T02003 on Em.T_NTNLTY_ID equals (short)Na.T_NTNLTY_ID into EmNa
                    from Na1 in EmNa.DefaultIfEmpty()
                    join EmTy in obj.T74005 on Em.T_EMP_TYP_ID equals EmTy.T_EMP_TYP_ID into EmEmTy
                    from EmTy1 in EmEmTy.DefaultIfEmpty()
                    where Em.T_PASSPORT_NO == _t74004.T_PASSPORT_NO
                    orderby Em.T_EMP_NO descending
                    select new
                    {
                        T_EMP_ID = Em.T_EMP_ID,
                        T_EMP_NO = Em.T_EMP_NO,
                        T_FIRST_LANG1_NAME = Em.T_FIRST_LANG1_NAME,
                        T_FIRST_LANG2_NAME = Em.T_FIRST_LANG2_NAME,
                        T_FATHER_LANG1_NAME = Em.T_FATHER_LANG1_NAME,
                        T_FATHER_LANG2_NAME = Em.T_FATHER_LANG2_NAME,

                        T_MOTHER_LANG1_NAME = Em.T_MOTHER_LANG1_NAME,
                        T_MOTHER_LANG2_NAME = Em.T_MOTHER_LANG2_NAME,
                        T_GFATHER_LANG1_NAME = Em.T_GFATHER_LANG1_NAME,
                        T_GFATHER_LANG2_NAME = Em.T_GFATHER_LANG2_NAME,
                        T_INACTIVE_FLAG = Em.T_INACTIVE_FLAG,

                        T_ADDRESS1 = Em.T_ADDRESS1,
                        T_ADDRESS2 = Em.T_ADDRESS2,
                        T_EMP_LANG = Em.T_EMP_LANG,
                        T_NTNLTY_ID = Em.T_NTNLTY_ID,
                        T_PASSPORT_NO = Em.T_PASSPORT_NO,


                        T_PHONE_HOME = Em.T_PHONE_HOME,
                        T_PHONE_WORK = Em.T_PHONE_WORK,
                        T_MOBILE_NO = Em.T_MOBILE_NO,
                        T_POSTAL_CODE = Em.T_POSTAL_CODE,
                        T_SMOKER_FLAG = Em.T_SMOKER_FLAG,

                        T_EMP_DEATH = Em.T_EMP_DEATH,
                        T_IP_EPISODES = Em.T_IP_EPISODES,
                        T_BALANCE_AMOUNT = Em.T_BALANCE_AMOUNT,
                        T_EMAIL_ID = Em.T_EMAIL_ID,
                        T_EMP_TYP_ID = Em.T_EMP_TYP_ID,



                        T_RLGN_CODE = Em.T_RLGN_CODE,
                        T_SEX_CODE = Em.T_SEX_CODE,
                        T_BLD_GROUP_ID = Em.T_BLD_GROUP_ID,
                        T_MRTL_STATUS_CODE = Em.T_MRTL_STATUS_CODE,
                        T_ACCEPTED = Em.T_ACCEPTED,

                        GE_T_LANG2_NAME = Ge1.T_LANG2_NAME,
                        Ma_T_LANG2_NAME = Ma1.T_LANG2_NAME,
                        Re_T_LANG2_NAME = Re1.T_LANG2_NAME,
                        Bl_T_BLOOD_GORUP = Bl1.T_BLOOD_GORUP,
                        Na_T_LANG2_NAME = Na1.T_LANG2_NAME,
                        EmTy_T_LANG2_NAME = EmTy1.T_LANG2_NAME

                    }).AsEnumerable().Select((r, i) => new
                {
                    RowNumber = i,
                    r.T_EMP_ID,
                    r.T_EMP_NO,
                    r.T_FIRST_LANG1_NAME,
                    r.T_FIRST_LANG2_NAME,
                    r.T_FATHER_LANG1_NAME,
                    r.T_FATHER_LANG2_NAME,

                    r.T_MOTHER_LANG1_NAME,
                    r.T_MOTHER_LANG2_NAME,
                    r.T_GFATHER_LANG1_NAME,
                    r.T_GFATHER_LANG2_NAME,
                    r.T_INACTIVE_FLAG,

                    r.T_ADDRESS1,
                    r.T_ADDRESS2,
                    r.T_EMP_LANG,
                    r.T_NTNLTY_ID,
                    r.T_PASSPORT_NO,

                    r.T_PHONE_HOME,
                    r.T_PHONE_WORK,
                    r.T_MOBILE_NO,
                    r.T_POSTAL_CODE,
                    r.T_SMOKER_FLAG,

                    r.T_EMP_DEATH,
                    r.T_IP_EPISODES,
                    r.T_BALANCE_AMOUNT,
                    r.T_EMAIL_ID,
                    r.T_EMP_TYP_ID,


                    r.T_RLGN_CODE,
                    r.T_SEX_CODE,
                    r.T_BLD_GROUP_ID,
                    r.T_MRTL_STATUS_CODE,
                    r.T_ACCEPTED,

                    r.GE_T_LANG2_NAME,
                    r.Ma_T_LANG2_NAME,
                    r.Re_T_LANG2_NAME,
                    r.Bl_T_BLOOD_GORUP,
                    r.Na_T_LANG2_NAME,
                    r.EmTy_T_LANG2_NAME,
                    T_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " +
                             r.T_MOTHER_LANG2_NAME
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

        public string saveEmployeeInfo(T74004 _t74004, string entryuser)
        {
            string msg = "";
            try
            {

                int result = obj.T74004.Max(x => x.T_EMP_ID);
                // int result = getID + 1;
                //  var rawQuery = obj.Database.SqlQuery<string>("SELECT LPAD(MAX(T_EMP_ID) ,6,'0') T_EMP_NO from T74004").First();
                // var nextVal = Convert.ToInt32(rawQuery);

                if (_t74004.T_EMP_ID == 0)
                {
                    _t74004.T_EMP_NO = result + 1;
                    _t74004.T_ENTRY_USER = entryuser;
                    obj.T74004.Add(_t74004);
                    Save();
                    msg = "1";
                }
                else
                {
                    int check = obj.T74015.Where(x => x.T_EMP_ID == _t74004.T_EMP_ID).Count();
                    if (check == 0)
                    {
                        _t74004.T_UPD_USER = entryuser;
                        obj.Entry(_t74004).State = System.Data.Entity.EntityState.Modified;
                        //var check = obj.T74004.Where(p => p.T_EMP_ID == t74004.T_EMP_ID).FirstOrDefault();
                        //if (check != null)
                        //{
                        //    check.T_UPD_DATE = t74004.T_ENTRY_DATE;
                        //}

                        Save();
                        msg = "2";
                    }
                    else
                    {
                        msg = "3";
                    }


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
            return msg;

        }

        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                string lang = HttpContext.Current.Session["T_LANG"].ToString();
                string entryuser = HttpContext.Current.Session["T_USER_ID"].ToString();
                query = (from Em in obj.T74004
                    join Ge in obj.T74050 on Em.T_SEX_CODE equals Ge.T_SEX_CODE into EmGe
                    from Ge1 in EmGe.DefaultIfEmpty()
                    join Ma in obj.T74051 on Em.T_MRTL_STATUS_CODE equals Ma.T_MRTL_STATUS_CODE into EmMa
                    from Ma1 in EmMa.DefaultIfEmpty()
                    join Re in obj.T74059 on Em.T_RLGN_CODE equals Re.T_RLGN_CODE into EmRe
                    from Re1 in EmRe.DefaultIfEmpty()
                    join Bl in obj.T74069 on Em.T_BLD_GROUP_ID equals Bl.T_BLD_GROUP_ID into EmBl
                    from Bl1 in EmBl.DefaultIfEmpty()
                    //join Na in obj.T02003 on Em.T_NTNLTY_ID equals Na.T_NTNLTY_ID
                    join Na in obj.T02003 on Em.T_NTNLTY_ID equals (short)Na.T_NTNLTY_ID into EmNa
                    from Na1 in EmNa.DefaultIfEmpty()
                    join EmTy in obj.T74005 on Em.T_EMP_TYP_ID equals EmTy.T_EMP_TYP_ID into EmEmTy
                    from EmTy1 in EmEmTy.DefaultIfEmpty()
                    where Em.T_ENTRY_USER == entryuser
                    orderby Em.T_EMP_NO descending
                    select new
                    {
                        T_EMP_ID = Em.T_EMP_ID,
                        T_EMP_NO = Em.T_EMP_NO,
                        T_FIRST_LANG1_NAME = Em.T_FIRST_LANG1_NAME,
                        T_FIRST_LANG2_NAME = Em.T_FIRST_LANG2_NAME,
                        T_FATHER_LANG1_NAME = Em.T_FATHER_LANG1_NAME,
                        T_FATHER_LANG2_NAME = Em.T_FATHER_LANG2_NAME,

                        T_MOTHER_LANG1_NAME = Em.T_MOTHER_LANG1_NAME,
                        T_MOTHER_LANG2_NAME = Em.T_MOTHER_LANG2_NAME,
                        T_GFATHER_LANG1_NAME = Em.T_GFATHER_LANG1_NAME,
                        T_GFATHER_LANG2_NAME = Em.T_GFATHER_LANG2_NAME,
                        T_INACTIVE_FLAG = Em.T_INACTIVE_FLAG,

                        T_ADDRESS1 = Em.T_ADDRESS1,
                        T_ADDRESS2 = Em.T_ADDRESS2,
                        T_EMP_LANG = Em.T_EMP_LANG,
                        T_NTNLTY_ID = Em.T_NTNLTY_ID,
                        T_PASSPORT_NO = Em.T_PASSPORT_NO,


                        T_PHONE_HOME = Em.T_PHONE_HOME,
                        T_PHONE_WORK = Em.T_PHONE_WORK,
                        T_MOBILE_NO = Em.T_MOBILE_NO,
                        T_POSTAL_CODE = Em.T_POSTAL_CODE,
                        T_SMOKER_FLAG = Em.T_SMOKER_FLAG,

                        T_EMP_DEATH = Em.T_EMP_DEATH,
                        T_IP_EPISODES = Em.T_IP_EPISODES,
                        T_BALANCE_AMOUNT = Em.T_BALANCE_AMOUNT,
                        T_EMAIL_ID = Em.T_EMAIL_ID,
                        T_EMP_TYP_ID = Em.T_EMP_TYP_ID,



                        T_RLGN_CODE = Em.T_RLGN_CODE,
                        T_SEX_CODE = Em.T_SEX_CODE,
                        T_BLD_GROUP_ID = Em.T_BLD_GROUP_ID,
                        T_MRTL_STATUS_CODE = Em.T_MRTL_STATUS_CODE,
                        T_ACCEPTED = Em.T_ACCEPTED,

                        GE_T_LANG2_NAME = Ge1.T_LANG2_NAME,
                        Ma_T_LANG2_NAME = Ma1.T_LANG2_NAME,
                        Re_T_LANG2_NAME = Re1.T_LANG2_NAME,
                        Bl_T_BLOOD_GORUP = Bl1.T_BLOOD_GORUP,
                        Na_T_LANG2_NAME = Na1.T_LANG2_NAME,
                        EmTy_T_LANG2_NAME = EmTy1.T_LANG2_NAME

                    }).AsEnumerable().Select((r, i) => new
                {
                    RowNumber = i,
                    r.T_EMP_ID,
                    r.T_EMP_NO,
                    r.T_FIRST_LANG1_NAME,
                    r.T_FIRST_LANG2_NAME,
                    r.T_FATHER_LANG1_NAME,
                    r.T_FATHER_LANG2_NAME,

                    r.T_MOTHER_LANG1_NAME,
                    r.T_MOTHER_LANG2_NAME,
                    r.T_GFATHER_LANG1_NAME,
                    r.T_GFATHER_LANG2_NAME,
                    r.T_INACTIVE_FLAG,

                    r.T_ADDRESS1,
                    r.T_ADDRESS2,
                    r.T_EMP_LANG,
                    r.T_NTNLTY_ID,
                    r.T_PASSPORT_NO,

                    r.T_PHONE_HOME,
                    r.T_PHONE_WORK,
                    r.T_MOBILE_NO,
                    r.T_POSTAL_CODE,
                    r.T_SMOKER_FLAG,

                    r.T_EMP_DEATH,
                    r.T_IP_EPISODES,
                    r.T_BALANCE_AMOUNT,
                    r.T_EMAIL_ID,
                    r.T_EMP_TYP_ID,


                    r.T_RLGN_CODE,
                    r.T_SEX_CODE,
                    r.T_BLD_GROUP_ID,
                    r.T_MRTL_STATUS_CODE,
                    r.T_ACCEPTED,

                    r.GE_T_LANG2_NAME,
                    r.Ma_T_LANG2_NAME,
                    r.Re_T_LANG2_NAME,
                    r.Bl_T_BLOOD_GORUP,
                    r.Na_T_LANG2_NAME,
                    r.EmTy_T_LANG2_NAME,
                    //T_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME
                    T_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber <= (PageIndex + 1) * PageSize);
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

        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            string entryuser = HttpContext.Current.Session["T_USER_ID"].ToString();
            try
            {
                if (searchValue != "")
                {
                    query = (from Em in obj.T74004
                        join Ge in obj.T74050 on Em.T_SEX_CODE equals Ge.T_SEX_CODE into EmGe
                        from Ge in EmGe.DefaultIfEmpty()
                        join Ma in obj.T74051 on Em.T_MRTL_STATUS_CODE equals Ma.T_MRTL_STATUS_CODE into EmMa
                        from Ma in EmMa.DefaultIfEmpty()
                        join Re in obj.T74059 on Em.T_RLGN_CODE equals Re.T_RLGN_CODE into EmRe
                        from Re in EmRe.DefaultIfEmpty()
                        join Bl in obj.T74069 on Em.T_BLD_GROUP_ID equals Bl.T_BLD_GROUP_ID into EmBl
                        from Bl in EmBl.DefaultIfEmpty()
                        join Na in obj.T02003 on Em.T_NTNLTY_ID equals (short)Na.T_NTNLTY_ID into EmNa
                        from Na in EmNa.DefaultIfEmpty()
                        join EmTy in obj.T74005 on Em.T_EMP_TYP_ID equals EmTy.T_EMP_TYP_ID into EmEmTy
                        from EmTy in EmEmTy.DefaultIfEmpty()
                        where Em.T_ENTRY_USER == entryuser &&
                              Em.T_FIRST_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Em.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Em.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Ge.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Na.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Ma.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Re.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              //Em.T_EMP_NO.ToString().Contains(searchValue) ||
                              Bl.T_BLOOD_GORUP.ToUpper().Contains(searchValue.ToUpper())
                        select Em).Count();
                }
                else
                {
                    query = (from Em in obj.T74004
                        join Ge in obj.T74050 on Em.T_SEX_CODE equals Ge.T_SEX_CODE into EmGe
                        from Ge in EmGe.DefaultIfEmpty()
                        join Ma in obj.T74051 on Em.T_MRTL_STATUS_CODE equals Ma.T_MRTL_STATUS_CODE into EmMa
                        from Ma in EmMa.DefaultIfEmpty()
                        join Re in obj.T74059 on Em.T_RLGN_CODE equals Re.T_RLGN_CODE into EmRe
                        from Re in EmRe.DefaultIfEmpty()
                        join Bl in obj.T74069 on Em.T_BLD_GROUP_ID equals Bl.T_BLD_GROUP_ID into EmBl
                        from Bl in EmBl.DefaultIfEmpty()
                        join Na in obj.T02003 on Em.T_NTNLTY_ID equals (short)Na.T_NTNLTY_ID into EmNa
                        from Na in EmNa.DefaultIfEmpty()
                        join EmTy in obj.T74005 on Em.T_EMP_TYP_ID equals EmTy.T_EMP_TYP_ID into EmEmTy
                        from EmTy in EmEmTy.DefaultIfEmpty()
                        where Em.T_ENTRY_USER == entryuser
                        select Em).Count();
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

        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Em in obj.T74004
                    join Ge in obj.T74050 on Em.T_SEX_CODE equals Ge.T_SEX_CODE into EmGe
                    from Ge in EmGe.DefaultIfEmpty()
                    join Ma in obj.T74051 on Em.T_MRTL_STATUS_CODE equals Ma.T_MRTL_STATUS_CODE into EmMa
                    from Ma in EmMa.DefaultIfEmpty()
                    join Re in obj.T74059 on Em.T_RLGN_CODE equals Re.T_RLGN_CODE into EmRe
                    from Re in EmRe.DefaultIfEmpty()
                    join Bl in obj.T74069 on Em.T_BLD_GROUP_ID equals Bl.T_BLD_GROUP_ID into EmBl
                    from Bl in EmBl.DefaultIfEmpty()
                    join Na in obj.T02003 on Em.T_NTNLTY_ID equals (short)Na.T_NTNLTY_ID into EmNa
                    from Na in EmNa.DefaultIfEmpty()
                    join EmTy in obj.T74005 on Em.T_EMP_TYP_ID equals EmTy.T_EMP_TYP_ID into EmEmTy
                    from EmTy in EmEmTy.DefaultIfEmpty()
                    orderby Em.T_EMP_NO descending
                    where
                    Em.T_FIRST_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    Em.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    Em.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    Ge.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    Na.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    Ma.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    Re.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    // Em.T_EMP_NO.ToString().Contains(searchValue) ||
                    Bl.T_BLOOD_GORUP.ToUpper().Contains(searchValue.ToUpper())


                    select new
                    {
                        T_EMP_ID = Em.T_EMP_ID,
                        T_EMP_NO = Em.T_EMP_NO,
                        T_FIRST_LANG1_NAME = Em.T_FIRST_LANG1_NAME,
                        T_FIRST_LANG2_NAME = Em.T_FIRST_LANG2_NAME,
                        T_FATHER_LANG1_NAME = Em.T_FATHER_LANG1_NAME,
                        T_FATHER_LANG2_NAME = Em.T_FATHER_LANG2_NAME,

                        T_MOTHER_LANG1_NAME = Em.T_MOTHER_LANG1_NAME,
                        T_MOTHER_LANG2_NAME = Em.T_MOTHER_LANG2_NAME,
                        T_GFATHER_LANG1_NAME = Em.T_GFATHER_LANG1_NAME,
                        T_GFATHER_LANG2_NAME = Em.T_GFATHER_LANG2_NAME,
                        T_INACTIVE_FLAG = Em.T_INACTIVE_FLAG,

                        T_ADDRESS1 = Em.T_ADDRESS1,
                        T_ADDRESS2 = Em.T_ADDRESS2,
                        T_NTNLTY_ID = Em.T_NTNLTY_ID,
                        T_PASSPORT_NO = Em.T_PASSPORT_NO,


                        T_PHONE_HOME = Em.T_PHONE_HOME,
                        T_PHONE_WORK = Em.T_PHONE_WORK,
                        T_MOBILE_NO = Em.T_MOBILE_NO,
                        T_POSTAL_CODE = Em.T_POSTAL_CODE,
                        T_SMOKER_FLAG = Em.T_SMOKER_FLAG,

                        T_EMP_DEATH = Em.T_EMP_DEATH,
                        T_IP_EPISODES = Em.T_IP_EPISODES,
                        T_BALANCE_AMOUNT = Em.T_BALANCE_AMOUNT,
                        T_EMAIL_ID = Em.T_EMAIL_ID,
                        T_EMP_LANG = Em.T_EMP_LANG,
                        T_EMP_TYP_ID = Em.T_EMP_TYP_ID,

                        T_RLGN_CODE = Em.T_RLGN_CODE,
                        T_SEX_CODE = Em.T_SEX_CODE,
                        T_BLD_GROUP_ID = Em.T_BLD_GROUP_ID,
                        T_MRTL_STATUS_CODE = Em.T_MRTL_STATUS_CODE,
                        T_ACCEPTED = Em.T_ACCEPTED,

                        GE_T_LANG2_NAME = Ge.T_LANG2_NAME,
                        Ma_T_LANG2_NAME = Ma.T_LANG2_NAME,
                        Re_T_LANG2_NAME = Re.T_LANG2_NAME,
                        Bl_T_BLOOD_GORUP = Bl.T_BLOOD_GORUP,
                        Na_T_LANG2_NAME = Na.T_LANG2_NAME,
                        EmTy_T_LANG2_NAME = EmTy.T_LANG2_NAME
                    }).AsEnumerable().Select((r, i) => new
                {
                    RowNumber = i,
                    r.T_EMP_ID,
                    r.T_EMP_NO,
                    r.T_FIRST_LANG1_NAME,
                    r.T_FIRST_LANG2_NAME,
                    r.T_FATHER_LANG1_NAME,
                    r.T_FATHER_LANG2_NAME,

                    r.T_MOTHER_LANG1_NAME,
                    r.T_MOTHER_LANG2_NAME,
                    r.T_GFATHER_LANG1_NAME,
                    r.T_GFATHER_LANG2_NAME,
                    r.T_INACTIVE_FLAG,

                    r.T_ADDRESS1,
                    r.T_ADDRESS2,
                    r.T_EMP_LANG,
                    r.T_NTNLTY_ID,
                    r.T_PASSPORT_NO,

                    r.T_PHONE_HOME,
                    r.T_PHONE_WORK,
                    r.T_MOBILE_NO,
                    r.T_POSTAL_CODE,
                    r.T_SMOKER_FLAG,

                    r.T_EMP_DEATH,
                    r.T_IP_EPISODES,
                    r.T_BALANCE_AMOUNT,
                    r.T_EMAIL_ID,
                    r.T_EMP_TYP_ID,
                    r.T_RLGN_CODE,
                    r.T_SEX_CODE,
                    r.T_BLD_GROUP_ID,
                    r.T_MRTL_STATUS_CODE,
                    r.T_ACCEPTED,

                    r.GE_T_LANG2_NAME,
                    r.Ma_T_LANG2_NAME,
                    r.Re_T_LANG2_NAME,
                    r.Bl_T_BLOOD_GORUP,
                    r.Na_T_LANG2_NAME,
                    r.EmTy_T_LANG2_NAME,
                    T_NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_MOTHER_LANG2_NAME
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber <= (PageIndex + 1) * PageSize);
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