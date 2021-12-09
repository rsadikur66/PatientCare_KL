using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;
using Microsoft.Ajax.Utilities;


namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74046Repository : IT74046
    {
        CommonDAL common = new CommonDAL();
        private AmbucareContainer obj = new AmbucareContainer();
        private readonly T74046DAL _t74046DAL = new T74046DAL();
        //bool internetConnectionCheck = CommonClass.CheckForInternetConnection();
        public IEnumerable MaritalData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T74051");
            //    query = dt.AsEnumerable().AsQueryable().Select(row =>
            //        new
            //        {
            //            T_MRTL_STATUS_CODE = row["T_MRTL_STATUS_CODE"].ToString(),
            //            T_LANG2_NAME = lang == "2" ? row["T_LANG2_NAME"].ToString() : row["T_LANG1_NAME"].ToString()
            //            //T_LANG1_NAME = row["T_LANG1_NAME"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T74051.ToList();
                    query = (from maritalStatus in obj.T74051
                             select new
                             {
                                 T_MRTL_STATUS_CODE = maritalStatus.T_MRTL_STATUS_CODE,
                                 T_LANG2_NAME = lang == "2" ? maritalStatus.T_LANG2_NAME : maritalStatus.T_LANG1_NAME
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
            //}
            return query;
        }
        public IEnumerable ReligionData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T74059");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_RLGN_CODE = row["T_RLGN_CODE"].ToString(),
            //            T_LANG2_NAME = lang == "2" ? row["T_LANG2_NAME"].ToString() : row["T_LANG1_NAME"].ToString(),
            //            //T_LANG1_NAME = row["T_LANG1_NAME"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T74059.ToList();
                    query = (from religionStatus in obj.T74059
                             select new
                             {
                                 T_RLGN_CODE = religionStatus.T_RLGN_CODE,
                                 T_LANG2_NAME = lang == "2" ? religionStatus.T_LANG2_NAME : religionStatus.T_LANG1_NAME
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
            //}
            return query;
        }
        public IEnumerable BloodGroupData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T74069");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_BLD_GROUP_ID = row["T_BLD_GROUP_ID"].ToString(),
            //            T_SHOT_DESC2 = lang == "2" ? row["T_SHOT_DESC2"].ToString() : row["T_SHOT_DESC1"].ToString(),
            //            //T_SHOT_DESC1 = row["T_SHOT_DESC1"].ToString(),
            //            T_BLOOD_GORUP = row["T_BLOOD_GORUP"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T74069.ToList();
                    query = (from bloodgroupStatus in obj.T74069
                             select new
                             {
                                 T_BLD_GROUP_ID = bloodgroupStatus.T_BLD_GROUP_ID,
                                 T_SHOT_DESC2 = lang == "2" ? bloodgroupStatus.T_SHOT_DESC2 : bloodgroupStatus.T_SHOT_DESC1,
                                 T_BLOOD_GORUP = bloodgroupStatus.T_BLOOD_GORUP
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
            //}
            return query;
        }
        public IEnumerable GenderData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T74050");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_SEX_CODE = row["T_SEX_CODE"].ToString(),
            //            T_LANG2_NAME = lang == "2" ? row["T_LANG2_NAME"].ToString() : row["T_LANG1_NAME"].ToString(),
            //            //T_LANG1_NAME = row["T_LANG1_NAME"].ToString(),
            //            T_SHORT_GNDR_NAME = row["T_SHORT_GNDR_NAME"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T74050.ToList();
                    query = (from genderStatus in obj.T74050
                             select new
                             {
                                 T_SEX_CODE = genderStatus.T_SEX_CODE,
                                 T_LANG2_NAME = lang == "2" ? genderStatus.T_LANG2_NAME : genderStatus.T_LANG1_NAME
                             }).OrderBy(a=>a.T_SEX_CODE).ToList();
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
            //}
            return query;
        }
        public IEnumerable NationalityData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T02003");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_NTNLTY_CODE = row["T_NTNLTY_CODE"].ToString(),
            //            T_LANG2_NAME = lang == "2" ? row["T_LANG2_NAME"].ToString() : row["T_LANG1_NAME"].ToString()
            //            //T_LANG2_NAME = row["T_LANG2_NAME"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T02003.ToList();
                    //query = (from nationalityStatus in obj.T02003
                    //         select new
                    //         {
                    //             T_NTNLTY_ID = nationalityStatus.T_NTNLTY_CODE,
                    //             T_LANG2_NAME = lang == "2" ? nationalityStatus.T_LANG2_NAME : nationalityStatus.T_LANG1_NAME
                    //         }).ToList();

                    query = (from nationalityStatus in obj.T02003
                             select new
                             {
                                 T_NTNLTY_ID = nationalityStatus.T_NTNLTY_ID,
                                 T_LANG2_NAME = nationalityStatus.T_LANG2_NAME,
                                 T_LANG1_NAME = nationalityStatus.T_LANG1_NAME,
                                 // T_LANG1_NAME =  nationalityStatus.T_LANG1_NAME,
                                 // T_LANG2_NAME = lang == "2" ? nationalityStatus.T_LANG2_NAME : nationalityStatus.T_LANG1_NAME,
                                 // T_LANG2_NAME = ObjectToByteArray(name)
                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 T_NTNLTY_ID = r.T_NTNLTY_ID,
                                 //T_LANG2_NAME = ObjectToByteArray(r.T_LANG2_NAME),
                                 T_LANG2_NAME = lang=="2"?r.T_LANG2_NAME: r.T_LANG1_NAME,
                                 // T_LANG1_NAME=ObjectToByteArray(r.T_LANG1_NAME)

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
            //}
            return query;
        }

        public DataTable NationalityData_1(string lang)
        {
            var data = _t74046DAL.NationalityData(lang);
            return data;

        }
        public IEnumerable ObjectToByteArray(byte[] data)
        {
            //var dataObject = data;
            var result = Encoding.UTF8.GetString(data);

            return result;
        }
        public IEnumerable DesignationData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T74006");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_EMP_DESI_ID = row["T_EMP_DESI_ID"].ToString(),
            //            T_LANG1_NAME = lang == "2" ? row["T_LANG2_NAME"].ToString() : row["T_LANG1_NAME"].ToString(),
            //            T_LANG2_NAME = row["T_LANG2_NAME"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T74006.ToList();
                    query = (from designationStatus in obj.T74006
                             select new
                             {
                                 T_SEX_CODE = designationStatus.T_EMP_DESI_ID,
                                 T_LANG2_NAME = lang == "2" ? designationStatus.T_LANG2_NAME : designationStatus.T_LANG1_NAME
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
            //}
            return query;
        }
        public IEnumerable ChiefComplaintData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T74055");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_CH_COMP = row["T_CH_COMP"].ToString(),
            //            T_LANG2_NAME = lang == "2" ? row["T_LANG2_NAME"].ToString() : row["T_LANG1_NAME"].ToString()
            //            //T_LANG2_NAME = row["T_LANG2_NAME"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T74055.ToList();
                    query = (from chiefComplaintStatus in obj.T74055
                             select new
                             {
                                 T_CH_COMP = chiefComplaintStatus.T_CH_COMP,
                                 T_LANG2_NAME = lang == "2" ? chiefComplaintStatus.T_LANG2_NAME : chiefComplaintStatus.T_LANG1_NAME
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
            //}
            return query;
        }
        public IEnumerable ProblemTypeData(string lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from T02040");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_SPCLTY_CODE = row["T_SPCLTY_CODE"].ToString(),
            //            T_LANG2_NAME = lang == "2" ? row["T_LANG2_NAME"].ToString() : row["T_LANG1_NAME"].ToString(),
            //            //T_LANG2_NAME = row["T_LANG2_NAME"].ToString(),
            //            T_MAIN_SPCLTY = row["T_MAIN_SPCLTY"].ToString(),
            //            T_ACTIVE_FLAG = row["T_ACTIVE_FLAG"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //query = obj.T02040.ToList();
                    query = (from problemtypeStatus in obj.T02040
                             select new
                             {
                                 T_SPCLTY_ID = problemtypeStatus.T_SPCLTY_ID,
                                 T_SPCLTY_CODE = problemtypeStatus.T_SPCLTY_CODE,
                                 T_LANG2_NAME = lang == "2" ? problemtypeStatus.T_LANG2_NAME : problemtypeStatus.T_LANG1_NAME,
                                 T_MAIN_SPCLTY = problemtypeStatus.T_MAIN_SPCLTY,
                                 T_ACTIVE_FLAG = problemtypeStatus.T_ACTIVE_FLAG
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
            //}
            return query;
        }


        //public IQueryable<T74051> MaritalData
        //{
        //    get { return obj.T74051; }
        //}

        //public IEnumerable MaritalData()
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //   query = cSql.Query($"select T_MRTL_STATUS_CODE,T_LANG2_NAME  from T74051").AsEnumerable();
        //    return query;

        //}

        //public IQueryable<T74059> ReligionData
        //{
        //    get { return obj.T74059; }
        //}
        //public IQueryable<T74069> BloodGroupData
        //{

        //    get { return obj.T74069; }

        //}
        //public IQueryable<T74050> GenderData
        //{

        //    get { return obj.T74050; }

        //}
        //public IQueryable<T02003> NationalityData
        //{

        //    get { return obj.T02003; }

        //}
        //public IQueryable<T74006> DesignationData
        //{

        //    get { return obj.T74006; }

        //}
        //public IQueryable<T74055> ChiefComplaintData
        //{

        //    get { return obj.T74055; }

        //}
        //public IQueryable<T02040> ProblemTypeData
        //{

        //    get { return obj.T02040; }

        //}

        public IQueryable<T74018> GetVetDiscount
        {
            get { return obj.T74018; }

        }

       public  IEnumerable GetRequestID()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                var userid = HttpContext.Current.Session["T_USER_ID"].ToString();
                var t57Em = obj.T74057.Where(p => p.T_USER_ID == userid).Select(x => x.T_EMP_ID).FirstOrDefault();
                var t15Amb = obj.T74015.Where(k => k.T_EMP_ID == t57Em).Max(v => v.T_AMBU_REG_ID);//.Select(y => y.T_AMBU_REG_ID);
                query = (from t46 in obj.T74046
                    join t41 in obj.T74041 on t46.T_PAT_ID equals t41.T_PAT_ID
                    join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                    //  join re in obj.T74059 on t46.T_RLGN_CODE equals re.T_RLGN_CODE into Religion
                    //  from re_ligion in Religion.DefaultIfEmpty()
                    //  join mrtl in obj.T74051 on t46.T_MRTL_STATUS equals mrtl.T_MRTL_STATUS_CODE into Marrital
                    //  from ma_Tal in Marrital.DefaultIfEmpty()
                    // join gnd in obj.T74050 on t46.T_SEX_CODE equals gnd.T_SEX_CODE into Gender
                    // from ge_der in Gender.DefaultIfEmpty()
                    //join Na in obj.T02003 on t46.T_NTNLTY_ID equals Na.T_NTNLTY_ID into nationality
                    // from Na_Lity in nationality.DefaultIfEmpty()
                    // join Na in obj.T74205 on t46.T_NTNLTY_ID equals (Int16)Na.T_NTNLTY_ID into nationality
                    //  from Na_Lity in nationality.DefaultIfEmpty()

                    where t44.T_AMBU_REG_ID == t15Amb && t41.T_DISCH_STATUS == null && t41.T_CANCEL_STATUS == null

                    select new
                    {
                        T_REQUEST_ID = t41.T_REQUEST_ID,
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
        public IEnumerable GetAmCuRe(string lang, int reqId)
        {
            var userid = HttpContext.Current.Session["T_USER_ID"].ToString();
            DataTable dtl = _t74046DAL.getTeamLatLng(userid);
            string lat = "";
            string lng = "";
            if (dtl.Rows.Count > 0)
            {
                lat = dtl.Rows[0]["T_LATITUDE"].ToString();
                lng = dtl.Rows[0]["T_LONGITUDE"].ToString();
            }
            IEnumerable query = Enumerable.Empty<object>();
            //if (internetConnectionCheck == false)
            //{
            //    var emp_id = cSql.Query($"select t_emp_id from t74057 where t_user_id ='{userid}'").Rows[0]["T_EMP_ID"].ToString();
            //    var amb_id = cSql.Query($" select max(t_ambu_reg_id)t_ambu_reg_id from t74015 where t_emp_id ='{emp_id}'")
            //        .Rows[0]["T_AMBU_REG_ID"].ToString();
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"SELECT t41.T_REQUEST_ID,t41.T_PAT_ID,t46.T_FIRST_LANG2_NAME, t46.T_MOBILE_NO,t41.T_PROB_DETAILS,t41.T_AMBU_REG_ID,t41.T_PROBLEM,t41.T_PROBLEM_DURATION,t46.T_FIRST_LANG1_NAME,t46.T_FATHER_LANG1_NAME,t46.T_FATHER_LANG2_NAME,t46.T_MOTHER_LANG1_NAME,t46.T_MOTHER_LANG2_NAME,t46.T_GFATHER_LANG1_NAME,t46.T_GFATHER_LANG2_NAME, t46.T_FAMILY_LANG1_NAME, t46.T_FAMILY_LANG2_NAME, t46.T_ADDRESS1, t46.T_ADDRESS2, t46.T_BIRTH_DATE, t46.T_NTNLTY_ID, t46.T_PASSPORT_NO, t46.T_SEX_CODE, t46.T_MRTL_STATUS, t46.T_RLGN_CODE, t46.T_POSTAL_CODE, t46.T_NATIONAL_ID, t46.T_OFFICE_NAME, t46.T_PHONE_WORK, t46.T_UPD_USER,re.T_LANG1_NAME RlgName_1, re.T_LANG2_NAME RlgName_2,mrtl.T_LANG1_NAME mrtlName_1, mrtl.T_LANG2_NAME mrtlName_2, gender.T_LANG1_NAME Gender_1,gender.T_LANG2_NAME Gender_2, ntnlty.T_LANG1_NAME nationality_1,ntnlty.T_LANG2_NAME nationality_2 FROM T74046 t46 JOIN T74041 t41 on t46.T_PAT_ID = t41.T_PAT_ID JOIN T74044 t44 on t41.T_AMBU_REG_ID = t44.T_AMBU_REG_ID LEFT JOIN T74059 re on t46.T_RLGN_CODE = re.T_RLGN_CODE LEFT JOIN T74051 mrtl ON t46.T_MRTL_STATUS = mrtl.T_MRTL_STATUS_CODE LEFT JOIN T74050 gender ON t46.T_SEX_CODE = gender.T_SEX_CODE LEFT JOIN T02003 ntnlty ON t46.T_NTNLTY_ID = ntnlty.T_NTNLTY_ID WHERE t44.T_AMBU_REG_ID = '{amb_id}' and t41.T_DISCH_STATUS is null and t41.T_CANCEL_STATUS is null");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_REQUEST_ID = row["T_REQUEST_ID"].ToString(),
            //            T_PAT_ID = row["T_PAT_ID"].ToString(),
            //            T_FIRST_LANG2_NAME = row["T_FIRST_LANG2_NAME"].ToString(),
            //            T_MOBILE_NO = row["T_MOBILE_NO"].ToString(),
            //            T_PROB_DETAILS = row["T_PROB_DETAILS"].ToString(),
            //            T_AMBU_REG_ID = row["T_AMBU_REG_ID"].ToString(),
            //            T_PROBLEM = row["T_PROBLEM"].ToString(),
            //            T_PROBLEM_DURATION = row["T_PROBLEM_DURATION"].ToString(),
            //            T_FIRST_LANG1_NAME = row["T_FIRST_LANG1_NAME"].ToString(),
            //            T_FATHER_LANG1_NAME = row["T_FATHER_LANG1_NAME"].ToString(),
            //            T_FATHER_LANG2_NAME = row["T_FATHER_LANG2_NAME"].ToString(),
            //            T_MOTHER_LANG1_NAME = row["T_MOTHER_LANG1_NAME"].ToString(),
            //            T_MOTHER_LANG2_NAME = row["T_MOTHER_LANG2_NAME"].ToString(),
            //            T_GFATHER_LANG1_NAME = row["T_GFATHER_LANG1_NAME"].ToString(),
            //            T_GFATHER_LANG2_NAME = row["T_GFATHER_LANG2_NAME"].ToString(),
            //            T_FAMILY_LANG1_NAME = row["T_FAMILY_LANG1_NAME"].ToString(),
            //            T_FAMILY_LANG2_NAME = row["T_FAMILY_LANG2_NAME"].ToString(),
            //            T_ADDRESS1 = row["T_ADDRESS1"].ToString(),
            //            T_ADDRESS2 = row["T_ADDRESS2"].ToString(),
            //            T_BIRTH_DATE = row["T_BIRTH_DATE"].ToString(),
            //            T_NTNLTY_ID = row["T_NTNLTY_ID"].ToString(),
            //            T_PASSPORT_NO = row["T_PASSPORT_NO"].ToString(),
            //            T_SEX_CODE = row["T_SEX_CODE"].ToString(),
            //            T_MRTL_STATUS = row["T_MRTL_STATUS"].ToString(),
            //            T_POSTAL_CODE = row["T_POSTAL_CODE"].ToString(),
            //            T_NATIONAL_ID = row["T_NATIONAL_ID"].ToString(),
            //            T_OFFICE_NAME = row["T_OFFICE_NAME"].ToString(),
            //            T_PHONE_WORK = row["T_PHONE_WORK"].ToString(),
            //            T_UPD_USER = row["T_UPD_USER"].ToString(),
            //            Re_T_LANG2_NAME = lang == "2" ? row["RlgName_2"].ToString() : row["RlgName_1"].ToString(),
            //            MRTL_T_LANG2_NAME = lang == "2" ? row["mrtlName_2"].ToString() : row["mrtlName_1"].ToString(),
            //            Ge_T_LANG2_NAME = lang == "2" ? row["Gender_2"].ToString() : row["Gender_1"].ToString(),
            //            Nationality_T_LANG2_NAME = lang == "2" ? row["nationality_2"].ToString() : row["nationality_1"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    //var userid = HttpContext.Current.Session["T_USER_ID"].ToString();

                   // var t57Em = obj.T74057.Where(p => p.T_USER_ID == userid).Select(x => x.T_EMP_ID).FirstOrDefault();
                   // var t15Amb = obj.T74015.Where(k => k.T_EMP_ID == t57Em).Max(v => v.T_AMBU_REG_ID);//.Select(y => y.T_AMBU_REG_ID);
                    query = (from t46 in obj.T74046
                             join t41 in obj.T74041 on t46.T_PAT_ID equals t41.T_PAT_ID
                             join t44 in obj.T74044 on t41.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID
                             join re in obj.T74059 on t46.T_RLGN_CODE equals re.T_RLGN_CODE into Religion
                             from re_ligion in Religion.DefaultIfEmpty()
                             join mrtl in obj.T74051 on t46.T_MRTL_STATUS equals mrtl.T_MRTL_STATUS_CODE into Marrital
                             from ma_Tal in Marrital.DefaultIfEmpty()
                             join gnd in obj.T74050 on t46.T_SEX_CODE equals gnd.T_SEX_CODE into Gender
                             from ge_der in Gender.DefaultIfEmpty()
                                 //join Na in obj.T02003 on t46.T_NTNLTY_ID equals Na.T_NTNLTY_ID into nationality
                                 // from Na_Lity in nationality.DefaultIfEmpty()
                             join Na in obj.T02003 on t46.T_NTNLTY_ID equals (Int16)Na.T_NTNLTY_ID into nationality
                             from Na_Lity in nationality.DefaultIfEmpty()

                                 //where t44.T_AMBU_REG_ID == t15Amb && t41.T_DISCH_STATUS == null && t41.T_CANCEL_STATUS == null
                             where t41.T_REQUEST_ID == reqId
                             select new
                             {
                                 T_REQUEST_ID = t41.T_REQUEST_ID,
                                 T_PAT_ID = t41.T_PAT_ID,
                                 T_FIRST_LANG2_NAME = t46.T_FIRST_LANG2_NAME,
                                 T_MOBILE_NO = t46.T_MOBILE_NO,
                                 T_PROB_DETAILS = t41.T_PROB_DETAILS,
                                 T_EVENT_FLAG = t41.T_EVENT_FLAG,

                                 T_AMBU_REG_ID = t41.T_AMBU_REG_ID,
                                 T_PROBLEM = t41.T_PROBLEM,
                                 T_PROBLEM_DURATION = t41.T_PROBLEM_DURATION,
                                 T_FIRST_LANG1_NAME = t46.T_FIRST_LANG1_NAME,
                                 T_CANCEL_REASON = "",
                                 T_FATHER_LANG1_NAME = t46.T_FATHER_LANG1_NAME,
                                 T_FATHER_LANG2_NAME = t46.T_FATHER_LANG2_NAME,
                                 T_MOTHER_LANG1_NAME = t46.T_MOTHER_LANG1_NAME,
                                 T_MOTHER_LANG2_NAME = t46.T_MOTHER_LANG2_NAME,
                                 T_GFATHER_LANG1_NAME = t46.T_GFATHER_LANG1_NAME,
                                 T_GFATHER_LANG2_NAME = t46.T_GFATHER_LANG2_NAME,
                                 T_FAMILY_LANG1_NAME = t46.T_FAMILY_LANG1_NAME,
                                 T_FAMILY_LANG2_NAME = t46.T_FAMILY_LANG2_NAME,

                                 T_ADDRESS1 = t46.T_ADDRESS1,
                                 T_ADDRESS2 = t46.T_ADDRESS2,
                                 T_BIRTH_DATE = t46.T_BIRTH_DATE,
                                 T_NTNLTY_ID = t46.T_NTNLTY_ID,
                                 T_PASSPORT_NO = t46.T_PASSPORT_NO,
                                 T_SEX_CODE = t46.T_SEX_CODE,
                                 T_AGE = t41.T_AGE,
                                 T_MRTL_STATUS = t46.T_MRTL_STATUS,
                                 T_RLGN_CODE = t46.T_RLGN_CODE,
                                 T_POSTAL_CODE = t46.T_POSTAL_CODE,
                                 T_NATIONAL_ID = t46.T_NATIONAL_ID,
                                 T_OFFICE_NAME = t46.T_OFFICE_NAME,
                                 T_PHONE_WORK = t46.T_PHONE_WORK,
                                 T_UPD_USER = t46.T_UPD_USER,
                                 // T_UPD_USER_41 = t41.T_UPD_USER,
                                 T_ACCEPT_STATUS = t41.T_ACCEPT_STATUS,
                                 T_SEEN_TIME = t41.T_SEEN_TIME,
                                 T_START_TIME = t41.T_START_TIME,
                                 T_CASE_ARRIVAL = t41.T_CASE_ARRIVAL,
                                 T_CAN_DATE = t41.T_CAN_DATE,
                                 patLat = t41.T_LATITUDE,
                                 patLng = t41.T_LONGITUDE,
                                 teamLat = lat,
                                 teamLng = lng,
                                 T_APPRX_TIME = t41.T_APPRX_TIME,
                                 T_APPRX_DIST = t41.T_APPRX_DIST,

                                 Re_T_LANG2_NAME = lang == "2" ? re_ligion.T_LANG2_NAME : re_ligion.T_LANG1_NAME,
                                 MRTL_T_LANG2_NAME = lang == "2" ? ma_Tal.T_LANG2_NAME : ma_Tal.T_LANG1_NAME,
                                 Ge_T_LANG2_NAME = lang == "2" ? ge_der.T_LANG2_NAME : ge_der.T_LANG1_NAME,
                                 Nationality_T_LANG2_NAME = lang == "2" ? Na_Lity.T_LANG2_NAME : Na_Lity.T_LANG1_NAME,
                                 // Nationality_T_LANG2_NAME = Encoding.UTF8.GetString(lang == "2" ? Na_Lity.T_LANG2_NAME : Na_Lity.T_LANG1_NAME)
                             }).AsEnumerable().Select((r, i) => new
                             {
                                 T_REQUEST_ID = r.T_REQUEST_ID,
                                 T_PAT_ID = r.T_PAT_ID,
                                 T_FIRST_LANG2_NAME = r.T_FIRST_LANG2_NAME,
                                 T_MOBILE_NO = r.T_MOBILE_NO,
                                 T_PROB_DETAILS = r.T_PROB_DETAILS,
                                 T_EVENT_FLAG = r.T_EVENT_FLAG,

                                 T_AMBU_REG_ID = r.T_AMBU_REG_ID,
                                 T_PROBLEM = r.T_PROBLEM,
                                 T_PROBLEM_DURATION = r.T_PROBLEM_DURATION,
                                 T_FIRST_LANG1_NAME = r.T_FIRST_LANG1_NAME,
                                 T_CANCEL_REASON = "",
                                 T_FATHER_LANG1_NAME = r.T_FATHER_LANG1_NAME,
                                 T_FATHER_LANG2_NAME = r.T_FATHER_LANG2_NAME,
                                 T_MOTHER_LANG1_NAME = r.T_MOTHER_LANG1_NAME,
                                 T_MOTHER_LANG2_NAME = r.T_MOTHER_LANG2_NAME,
                                 T_GFATHER_LANG1_NAME = r.T_GFATHER_LANG1_NAME,
                                 T_GFATHER_LANG2_NAME = r.T_GFATHER_LANG2_NAME,
                                 T_FAMILY_LANG1_NAME = r.T_FAMILY_LANG1_NAME,
                                 T_FAMILY_LANG2_NAME = r.T_FAMILY_LANG2_NAME,

                                 T_ADDRESS1 = r.T_ADDRESS1,
                                 T_ADDRESS2 = r.T_ADDRESS2,
                                 T_BIRTH_DATE = r.T_BIRTH_DATE,
                                 T_NTNLTY_ID = r.T_NTNLTY_ID,
                                 T_PASSPORT_NO = r.T_PASSPORT_NO,
                                 T_SEX_CODE = r.T_SEX_CODE,
                                 T_AGE = r.T_AGE,
                                 T_MRTL_STATUS = r.T_MRTL_STATUS,
                                 T_RLGN_CODE = r.T_RLGN_CODE,
                                 T_POSTAL_CODE = r.T_POSTAL_CODE,
                                 T_NATIONAL_ID = r.T_NATIONAL_ID,
                                 T_OFFICE_NAME = r.T_OFFICE_NAME,
                                 T_PHONE_WORK = r.T_PHONE_WORK,
                                 T_UPD_USER = r.T_UPD_USER,
                                 // T_UPD_USER_41 = t41.T_UPD_USER,
                                 T_ACCEPT_STATUS = r.T_ACCEPT_STATUS,
                                 T_SEEN_TIME = r.T_SEEN_TIME,
                                 T_START_TIME = r.T_START_TIME,
                                 T_CASE_ARRIVAL = r.T_CASE_ARRIVAL,
                                 T_CAN_DATE = r.T_CAN_DATE,
                                 patLat = r.patLat,
                                 patLng = r.patLng,
                                 teamLat = lat,
                                 teamLng = lng,
                                 T_APPRX_TIME = r.T_APPRX_TIME,
                                 T_APPRX_DIST = r.T_APPRX_DIST,

                                 Re_T_LANG2_NAME = r.Re_T_LANG2_NAME,
                                 MRTL_T_LANG2_NAME = r.MRTL_T_LANG2_NAME,
                                 Ge_T_LANG2_NAME = r.Ge_T_LANG2_NAME,
                                 Nationality_T_LANG2_NAME = r.Nationality_T_LANG2_NAME == null ? null : r.Nationality_T_LANG2_NAME
                                
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
            //}
            return query;
        }


        public IEnumerable getHealthScreenData(int request)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                // decimal chk = obj.T74043.Where(k => k.T_REQUEST_ID == request).Count();
                // if (chk > 0)
                // {
                // decimal max = obj.T74043.Where(k=>k.T_REQUEST_ID==request).Max(x => x.T_PCHECKUP_ID);

                query = (from tt41 in obj.T74041
                         join tt43 in obj.T74043 on tt41.T_REQUEST_ID equals tt43.T_REQUEST_ID into tt41_t43
                         from t43 in tt41_t43.DefaultIfEmpty()
                         join t40 in obj.T02040 on tt41.T_PROB_TYPE_ID equals t40.T_SPCLTY_ID into t40_41
                         from a in t40_41.DefaultIfEmpty()
                         join t55 in obj.T74055 on tt41.T_CH_COMP equals t55.T_CH_COMP into t55_41
                         from b in t55_41.DefaultIfEmpty()
                         join t301 in obj.T06301 on tt41.T_ICD10_CODE equals t301.T_ICD10_CODE into t301_41
                         from c in t301_41.DefaultIfEmpty()
                         where tt41.T_REQUEST_ID == request //&& t43.T_PCHECKUP_ID == max

                         select new
                         {
                             T_PCHECKUP_ID = t43 == null ? 0 : t43.T_PCHECKUP_ID,
                             T_TEMP = t43 == null ? "" : t43.T_TEMP,
                             T_PULS = t43 == null ? "" : t43.T_PULS,
                             T_BP_SYS = t43 == null ? "" : t43.T_BP_SYS,
                             T_BP_DIA = t43 == null ? "" : t43.T_BP_DIA,
                             T_WEIGHT = t43 == null ? "" : t43.T_WEIGHT,
                             T_HIGHT = t43 == null ? "" : t43.T_HIGHT,
                             T_RESP = t43 == null ? "" : t43.T_RESP,
                             T_OS = t43 == null ? "" : t43.T_OS,
                             T_PROBLEM = tt41.T_PROBLEM,
                             T_PROB_DETAILS = tt41.T_PROB_DETAILS,
                             T_PROBLEM_DURATION = tt41.T_PROBLEM_DURATION,
                             T_ICD10_CODE = tt41.T_ICD10_CODE,
                             T_ICD10_SHORT_DESC2 = c.T_ICD10_SHORT_DESC2,
                             T_PROB_TYPE_ID = tt41.T_PROB_TYPE_ID,

                             T_CH_COMP = tt41.T_CH_COMP,
                             T_SEEN_TIME = tt41.T_SEEN_TIME,
                             PROBLEM_TYPE = a.T_LANG2_NAME,
                             CH_COM = b.T_LANG2_NAME,
                             tt41.T_EYE_OPEN,
                             tt41.T_VERBAL_RESPONSE,
                             tt41.T_BEST_MOTOR

                         }).ToList();
                //  }

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

        public string conciousLevel(string con)
        {
            string a = "";
            DataTable dt = common.Query($"SELECT T_LANG2_NAME T_LANG1_NAME FROM T28135 WHERE T_CONS_LEVEL='{con}'");
            if (dt.Rows.Count > 0)
            {
                a = dt.Rows[0]["T_LANG1_NAME"].ToString();
            }
            return a;
        }
        public IEnumerable healthScreenAllData(int reqID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t43 in obj.T74043
                         // join T28135 in obj.T28 on EXPR1 equals EXPR2 
                     where t43.T_REQUEST_ID == reqID


                     // orderby t43.T_PCHECKUP_ID descending
                     select new
                     {
                         T_PCHECKUP_ID = t43.T_PCHECKUP_ID,
                         T_TEMP = t43.T_TEMP,
                         T_PULS = t43.T_PULS,
                         T_BP_SYS = t43.T_BP_SYS,
                         T_BP_DIA = t43.T_BP_DIA,
                         T_CONCIUS_LEVEL = t43.T_CONCIUS_LEVEL,
                         T_HIGHT = t43.T_HIGHT,
                         T_RESP = t43.T_RESP,
                         T_OS = t43.T_OS,
                         T_ECG_TEST = t43.T_ECG_TEST,
                         T_URINE_TEST = t43.T_URINE_TEST,
                         T_VERIFY_LEVEL = t43.T_VERIFY_LEVEL,
                         T_TRIAGE_LEVEL = t43.T_TRIAGE_LEVEL,
                         T_ENTRY_DATE = t43.T_ENTRY_DATE,
                         T_TIME = t43.T_TIME,
                         T_SCORE = t43.T_SCORE
                         //hr =t43.T_TIME.Value.Hour,
                         //mn = t43.T_TIME.Value.Minute,
                         //sc = t43.T_TIME.Value.Second
                         //.ToString("yyyy-MM-dd"),a.OrderDate.Month + "/" + a.OrderDate.Day + "/" + a.OrderDate.Year
                     }).AsEnumerable().Select((r, i) => new
                     {
                         T_PCHECKUP_ID=r.T_PCHECKUP_ID,
                         T_TEMP=Convert.ToDecimal(r.T_TEMP),
                         T_PULS=Convert.ToDecimal(r.T_PULS),
                         T_BP_SYS=Convert.ToDecimal(r.T_BP_SYS),
                         T_BP_DIA=Convert.ToDecimal(r.T_BP_DIA),
                         T_CONCIUS_LEVEL_id = r.T_CONCIUS_LEVEL,
                         T_CONCIUS_LEVEL = conciousLevel(r.T_CONCIUS_LEVEL),
                         T_HIGHT= Convert.ToDecimal(r.T_HIGHT) ,
                         T_RESP= Convert.ToDecimal(r.T_RESP),
                         T_OS= Convert.ToDecimal(r.T_OS),
                         T_SCORE = Convert.ToDecimal(r.T_SCORE),
                         T_LASTSCORE = LastScore(),
                         r.T_ECG_TEST,
                         //T_URINE_TEST= Convert.ToDecimal(r.T_URINE_TEST),
                         T_URINE_TEST= r.T_URINE_TEST,
                         r.T_VERIFY_LEVEL,
                         r.T_TRIAGE_LEVEL,
                         r.T_ENTRY_DATE,
                         T_TIME = r.T_TIME == null ? 0 : (r.T_TIME.Value.Hour * 60 + r.T_TIME.Value.Minute),
                         HR = r.T_TIME == null ? 0 : r.T_TIME.Value.Hour,
                         MN = r.T_TIME == null ? 0 : r.T_TIME.Value.Minute
                     }).ToList().OrderByDescending(a => a.T_PCHECKUP_ID);

            return query;

        }

        public decimal LastScore()
        {
            decimal id = obj.T74043.Max(i => i.T_PCHECKUP_ID);
            return id;
        }
        //IQueryable<T74018> GetVetDiscount { get; }
        public IEnumerable GetICD10DataInSearch(string icd)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                //query = obj.T06301.Where(p =>p.T_ICD10_SHORT_DESC2.ToUpper().StartsWith(icd.ToUpper())).ToList();
                query = (from t301 in obj.T06301
                         where t301.T_ICD10_SHORT_DESC2.ToUpper().Contains(icd.ToUpper())
                         select new
                         {
                             t301.T_ICD10_SHORT_DESC2,
                             t301.T_ICD10_CODE
                         }).Take(1000).ToList();
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
        public IEnumerable RequestData()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from request in obj.T74041
                         join Pa in obj.T74046 on request.T_PAT_ID equals Pa.T_PAT_ID //into Patient
                         join re in obj.T74059 on Pa.T_RLGN_CODE equals re.T_RLGN_CODE into Religion
                         from re_ligion in Religion.DefaultIfEmpty()
                         join mrtl in obj.T74051 on Pa.T_MRTL_STATUS equals mrtl.T_MRTL_STATUS_CODE into Marrital
                         from ma_Tal in Marrital.DefaultIfEmpty()
                         join gnd in obj.T74050 on Pa.T_SEX_CODE equals gnd.T_SEX_CODE into Gender
                         from ge_der in Gender.DefaultIfEmpty()
                         where request.T_PAT_ID == Pa.T_PAT_ID
                         orderby request.T_PAT_ID descending
                         //from Pa in Patient.DefaultIfEmpty()
                         select new
                         {
                             T_PAT_ID = Pa.T_PAT_ID,
                             T_REQUEST_ID = request.T_REQUEST_ID,
                             T_PAT_NO = Pa.T_PAT_NO,
                             T_AMBU_REG_ID = request.T_AMBU_REG_ID,
                             T_PROBLEM = request.T_PROBLEM,
                             T_PROBLEM_DURATION = request.T_PROBLEM_DURATION,
                             T_FIRST_LANG1_NAME = Pa.T_FIRST_LANG1_NAME,
                             T_FIRST_LANG2_NAME = Pa.T_FIRST_LANG2_NAME,
                             T_FATHER_LANG1_NAME = Pa.T_FATHER_LANG1_NAME,
                             T_FATHER_LANG2_NAME = Pa.T_FATHER_LANG2_NAME,

                             T_MOTHER_LANG1_NAME = Pa.T_MOTHER_LANG1_NAME,
                             T_MOTHER_LANG2_NAME = Pa.T_MOTHER_LANG2_NAME,
                             T_GFATHER_LANG1_NAME = Pa.T_GFATHER_LANG1_NAME,
                             T_GFATHER_LANG2_NAME = Pa.T_GFATHER_LANG2_NAME,
                             T_FAMILY_LANG1_NAME = Pa.T_FAMILY_LANG1_NAME,
                             T_FAMILY_LANG2_NAME = Pa.T_FAMILY_LANG2_NAME,

                             T_ADDRESS1 = Pa.T_ADDRESS1,
                             T_ADDRESS2 = Pa.T_ADDRESS2,
                             T_BIRTH_DATE = Pa.T_BIRTH_DATE,
                             T_NTNLTY_ID = Pa.T_NTNLTY_ID,
                             T_PASSPORT_NO = Pa.T_PASSPORT_NO,
                             T_MOBILE_NO = Pa.T_MOBILE_NO,
                             T_SEX_CODE = Pa.T_SEX_CODE,
                             T_MRTL_STATUS = Pa.T_MRTL_STATUS,
                             T_RLGN_CODE = Pa.T_RLGN_CODE,
                             T_POSTAL_CODE = Pa.T_POSTAL_CODE,
                             T_NATIONAL_ID = Pa.T_NATIONAL_ID,

                             Re_T_LANG2_NAME = re_ligion.T_LANG2_NAME,
                             MRTL_T_LANG2_NAME = ma_Tal.T_LANG2_NAME,
                             Ge_T_LANG2_NAME = ge_der.T_LANG2_NAME

                         }
                ).ToList();

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

        public IEnumerable AmbulanceData(int t_AMBU_REG_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from ambu in obj.T74014
                         join emp in obj.T74004 on ambu.T_EMP_ID equals emp.T_EMP_ID
                         join empType in obj.T74005 on emp.T_EMP_TYP_ID equals empType.T_EMP_TYP_ID
                         join ambPrice in obj.T74044 on ambu.T_AMBU_REG_ID equals ambPrice.T_AMBU_REG_ID
                         join docPrice in obj.T74015 on ambu.T_EMP_ID equals docPrice.T_EMP_ID
                         //from Emp in obj.T74004
                         //join EmpType in obj.T74005 on Emp.T_EMP_TYP_ID equals EmpType.T_EMP_TYP_ID
                         //join AmbRg in obj.T74014 on Emp.T_EMP_ID equals AmbRg.T_EMP_ID
                         //join Price in obj.T74044 on AmbRg.T_AMBU_REG_ID equals Price.T_AMBU_REG_ID
                         //join Doc in obj.T74039 on Emp.T_EMP_ID equals Doc.T_DOC_ID
                         // join Pres in obj.T74040 on Request.T_REQUEST_ID equals Pres.T_REQUEST_ID into Pres_Request
                         // from PreRequ in Pres_Request.DefaultIfEmpty()
                         where ambu.T_AMBU_REG_ID == t_AMBU_REG_ID && (empType.T_EMP_TYP_ID == 2 || empType.T_EMP_TYP_ID == 22)

                         // CASE WHEN T74005.T_EMP_TYP_ID = 2 THEN 'Doctor Consultancy' ELSE 'Ambulance Charge' END Type, T74044.T_PRICE

                         select new
                         {
                             T_EMP_TYP_ID = empType.T_EMP_TYP_ID == 2 ? "Doctor Consultancy" : "Ambulance Charge",
                             T_EMP_TYP_NAME = empType.T_LANG2_NAME,
                             Name = empType.T_EMP_TYP_ID == 2 ? "Doctor Consultancy" : "Ambulance Charge",
                             T_EMP_ID = emp.T_EMP_ID,
                             T_PRICE = ambPrice.T_PRICE,

                             T_EMP_NAME = emp.T_FIRST_LANG2_NAME, //+ " " + Emp.T_FATHER_LANG2_NAME + " " + Emp.T_GFATHER_LANG2_NAME + " " + Emp.T_FAMILY_LANG2_NAME,
                             //T_PRICE = T_COST_TYPE_DTL_ID.T_PRICE
                         }

                    ).ToList();
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

        public IEnumerable GetDiagnosis(int t_REQUEST_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Diag in obj.T74039
                         join Pres in obj.T74078 on Diag.T_PRESCRIPTION_ID equals Pres.T_PRESCRIPTION_ID
                         join CosTypDtl in obj.T74073 on Pres.T_COST_TYPE_DTL_ID equals CosTypDtl.T_COST_TYPE_DTL_ID
                         join t89 in obj.T74089 on CosTypDtl.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                         where Diag.T_REQUEST_ID == t_REQUEST_ID
                         select new
                         {
                             T_LANG2_NAME = CosTypDtl.T_LANG2_NAME,
                             T_PRICE = t89.T_SALE_PRICE
                         }

                    ).ToList();
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
        //IEnumerable GetDiagnosis(int t_REQUEST_ID);
        public IEnumerable GetServiceData(int AmbuID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from T74044 in obj.T74044
                         join T74036 in obj.T74036 on T74044.T_STORE_ID equals T74036.T_STORE_ID
                         join T74037 in obj.T74037 on T74036.T_ISSUE_ID equals T74037.T_ISSUE_ID
                         join T74001 in obj.T74001 on T74037.T_ITEM_ID equals T74001.T_ITEM_ID
                         join T74073 in obj.T74073 on T74001.T_ITEM_ID equals T74073.T_ID
                         join t89 in obj.T74089 on T74073.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                         where T74044.T_AMBU_REG_ID == AmbuID //&& Emp.T_EMP_TYP_ID == T_COST_TYPE_DTL_ID.T_ID
                         select new
                         {
                             T_EMP_TYP_ID = T74044.T_AMBU_REG_ID,
                             T_SERVICE_NAME = T74001.T_LANG2_NAME,
                             T_PRICE = t89.T_SALE_PRICE
                         }

                ).ToList();
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
        public IEnumerable GetGridDataPat(int pageIndex, int pageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Pa in obj.T74046
                         join Ge in obj.T74050 on Pa.T_SEX_CODE equals Ge.T_SEX_CODE
                         join Ma in obj.T74051 on Pa.T_MRTL_STATUS equals Ma.T_MRTL_STATUS_CODE
                         join Re in obj.T74059 on Pa.T_RLGN_CODE equals Re.T_RLGN_CODE
                         //  join De in obj.T74006 on Pa.T_EMP_DESI_ID equals De.T_EMP_DESI_ID
                         join Na in obj.T02003 on Pa.T_NTNLTY_ID equals Na.T_NTNLTY_ID
                         select new
                         {
                             T_PAT_ID = Pa.T_PAT_ID,
                             T_PAT_NO = Pa.T_PAT_NO,
                             T_FIRST_LANG1_NAME = Pa.T_FIRST_LANG1_NAME,
                             T_FIRST_LANG2_NAME = Pa.T_FIRST_LANG2_NAME,
                             T_FATHER_LANG1_NAME = Pa.T_FATHER_LANG1_NAME,
                             T_FATHER_LANG2_NAME = Pa.T_FATHER_LANG2_NAME,

                             T_MOTHER_LANG1_NAME = Pa.T_MOTHER_LANG1_NAME,
                             T_MOTHER_LANG2_NAME = Pa.T_MOTHER_LANG2_NAME,
                             T_GFATHER_LANG1_NAME = Pa.T_GFATHER_LANG1_NAME,
                             T_GFATHER_LANG2_NAME = Pa.T_GFATHER_LANG2_NAME,
                             // T_INACTIVE_FLAG = Pa.T_INACTIVE_FLAG,

                             T_ADDRESS1 = Pa.T_ADDRESS1,
                             T_ADDRESS2 = Pa.T_ADDRESS2,
                             T_BIRTH_DATE = Pa.T_BIRTH_DATE,
                             T_NTNLTY_ID = Pa.T_NTNLTY_ID,
                             T_PASSPORT_NO = Pa.T_PASSPORT_NO,


                             T_PHONE_HOME = Pa.T_PHONE_HOME,
                             T_PHONE_WORK = Pa.T_PHONE_WORK,
                             T_MOBILE_NO = Pa.T_MOBILE_NO,
                             T_POSTAL_CODE = Pa.T_POSTAL_CODE,
                             T_OFFICE_NAME = Pa.T_OFFICE_NAME,

                             T_MRTL_STATUS_CODE = Ma.T_MRTL_STATUS_CODE,

                             //T_BALANCE_AMOUNT = Pa.T_BALANCE_AMOUNT,
                             //  T_EMAIL_ID = Pa.T_EMAIL_ID,



                             T_NATIONAL_ID = Pa.T_NATIONAL_ID,
                             T_RLGN_CODE = Pa.T_RLGN_CODE,
                             T_SEX_CODE = Pa.T_SEX_CODE,
                             // T_BLD_GROUP_ID = Em.T_BLD_GROUP_ID,
                             T_MRTL_STATUS = Pa.T_MRTL_STATUS,
                             T_EMP_DESI_ID = Pa.T_EMP_DESI_ID,
                             //T_ACCEPTED = Em.T_ACCEPTED,

                             GE_T_LANG2_NAME = Ge.T_LANG2_NAME,
                             Ma_T_LANG2_NAME = Ma.T_LANG2_NAME,
                             Re_T_LANG2_NAME = Re.T_LANG2_NAME,
                             //  De_T_LANG2_NAME = De.T_LANG2_NAME,
                             Na_T_LANG2_NAME = Na.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             RowNumber = i,
                             r.T_PAT_ID,
                             r.T_PAT_NO,
                             r.T_FIRST_LANG1_NAME,
                             r.T_FIRST_LANG2_NAME,
                             r.T_FATHER_LANG1_NAME,
                             r.T_FATHER_LANG2_NAME,

                             r.T_MOTHER_LANG1_NAME,
                             r.T_MOTHER_LANG2_NAME,
                             r.T_GFATHER_LANG1_NAME,
                             r.T_GFATHER_LANG2_NAME,
                             r.T_BIRTH_DATE,

                             r.T_ADDRESS1,
                             r.T_ADDRESS2,
                             r.T_MRTL_STATUS,
                             r.T_NTNLTY_ID,
                             r.T_PASSPORT_NO,

                             r.T_PHONE_HOME,
                             r.T_PHONE_WORK,
                             r.T_MOBILE_NO,
                             r.T_POSTAL_CODE,
                             r.T_NATIONAL_ID,

                             // r.T_EMP_DEATH,
                             // r.T_IP_EPISODES,
                             //  r.T_BALANCE_AMOUNT,
                             //  r.T_EMAIL_ID,



                             r.T_RLGN_CODE,
                             r.T_SEX_CODE,
                             r.T_OFFICE_NAME,
                             r.T_EMP_DESI_ID,
                             //  r.T_ACCEPTED,

                             r.GE_T_LANG2_NAME,
                             r.Ma_T_LANG2_NAME,
                             r.Re_T_LANG2_NAME,
                             // r.De_T_LANG2_NAME,
                             r.Na_T_LANG2_NAME
                         }).ToList().Where(x => x.RowNumber >= pageIndex * pageSize + 0 && x.RowNumber <= (pageIndex + 1) * pageSize);
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
        public int GetPatData_Search_Count(string searchValue)
        {
            int query = 0;
            try
            {
                if (searchValue != "")
                {
                    query = (from Pa in obj.T74046
                             join Ge in obj.T74050 on Pa.T_SEX_CODE equals Ge.T_SEX_CODE
                             join Ma in obj.T74051 on Pa.T_MRTL_STATUS equals Ma.T_MRTL_STATUS_CODE
                             join Re in obj.T74059 on Pa.T_RLGN_CODE equals Re.T_RLGN_CODE
                             // join De in obj.T74006 on Pa.T_EMP_DESI_ID equals De.T_EMP_DESI_ID
                             join Na in obj.T02003 on Pa.T_NTNLTY_ID equals Na.T_NTNLTY_ID
                             where
                                   Pa.T_FIRST_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   //  Pa.T_PAT_NO.ToString().Contains(searchValue) ||
                                   Pa.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Pa.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Pa.T_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Ge.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Na.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Ma.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   //  De.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Re.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                             select Pa).Count();
                }
                else
                {
                    query = (from Pa in obj.T74046
                             join Ge in obj.T74050 on Pa.T_SEX_CODE equals Ge.T_SEX_CODE
                             join Ma in obj.T74051 on Pa.T_MRTL_STATUS equals Ma.T_MRTL_STATUS_CODE
                             join Re in obj.T74059 on Pa.T_RLGN_CODE equals Re.T_RLGN_CODE
                             // join De in obj.T74006 on Pa.T_EMP_DESI_ID equals De.T_EMP_DESI_ID
                             join Na in obj.T02003 on Pa.T_NTNLTY_ID equals Na.T_NTNLTY_ID
                             select Pa).Count();
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

        public IEnumerable GetGrid_Data_SearchPat(string searchValue, int pageIndex, int pageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Pa in obj.T74046
                         join Ge in obj.T74050 on Pa.T_SEX_CODE equals Ge.T_SEX_CODE
                         join Ma in obj.T74051 on Pa.T_MRTL_STATUS equals Ma.T_MRTL_STATUS_CODE
                         join Re in obj.T74059 on Pa.T_RLGN_CODE equals Re.T_RLGN_CODE
                         // join De in obj.T74006 on Pa.T_EMP_DESI_ID equals De.T_EMP_DESI_ID
                         join Na in obj.T02003 on Pa.T_NTNLTY_ID equals Na.T_NTNLTY_ID
                         where
                               Pa.T_FIRST_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               // Pa.T_PAT_NO.ToUpper().Contains(searchValue) ||
                               Pa.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               Pa.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               Pa.T_MOBILE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                               Ge.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               Na.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               Ma.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               // De.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               Re.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                         select new
                         {
                             T_PAT_ID = Pa.T_PAT_ID,
                             T_PAT_NO = Pa.T_PAT_NO,
                             T_FIRST_LANG1_NAME = Pa.T_FIRST_LANG1_NAME,
                             T_FIRST_LANG2_NAME = Pa.T_FIRST_LANG2_NAME,
                             T_FATHER_LANG1_NAME = Pa.T_FATHER_LANG1_NAME,
                             T_FATHER_LANG2_NAME = Pa.T_FATHER_LANG2_NAME,

                             T_MOTHER_LANG1_NAME = Pa.T_MOTHER_LANG1_NAME,
                             T_MOTHER_LANG2_NAME = Pa.T_MOTHER_LANG2_NAME,
                             T_GFATHER_LANG1_NAME = Pa.T_GFATHER_LANG1_NAME,
                             T_GFATHER_LANG2_NAME = Pa.T_GFATHER_LANG2_NAME,
                             // T_INACTIVE_FLAG = Pa.T_INACTIVE_FLAG,

                             T_ADDRESS1 = Pa.T_ADDRESS1,
                             T_ADDRESS2 = Pa.T_ADDRESS2,
                             T_BIRTH_DATE = Pa.T_BIRTH_DATE,
                             T_NTNLTY_ID = Pa.T_NTNLTY_ID,
                             T_PASSPORT_NO = Pa.T_PASSPORT_NO,


                             T_PHONE_HOME = Pa.T_PHONE_HOME,
                             T_PHONE_WORK = Pa.T_PHONE_WORK,
                             T_MOBILE_NO = Pa.T_MOBILE_NO,
                             T_POSTAL_CODE = Pa.T_POSTAL_CODE,
                             T_OFFICE_NAME = Pa.T_OFFICE_NAME,

                             T_MRTL_STATUS_CODE = Ma.T_MRTL_STATUS_CODE,

                             //T_BALANCE_AMOUNT = Pa.T_BALANCE_AMOUNT,
                             //  T_EMAIL_ID = Pa.T_EMAIL_ID,



                             T_NATIONAL_ID = Pa.T_NATIONAL_ID,
                             T_RLGN_CODE = Pa.T_RLGN_CODE,
                             T_SEX_CODE = Pa.T_SEX_CODE,
                             // T_BLD_GROUP_ID = Em.T_BLD_GROUP_ID,
                             T_MRTL_STATUS = Pa.T_MRTL_STATUS,
                             T_EMP_DESI_ID = Pa.T_EMP_DESI_ID,
                             //T_ACCEPTED = Em.T_ACCEPTED,

                             GE_T_LANG2_NAME = Ge.T_LANG2_NAME,
                             Ma_T_LANG2_NAME = Ma.T_LANG2_NAME,
                             Re_T_LANG2_NAME = Re.T_LANG2_NAME,
                             // De_T_LANG2_NAME = De.T_LANG2_NAME,
                             Na_T_LANG2_NAME = Na.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             RowNumber = i,
                             r.T_PAT_ID,
                             r.T_PAT_NO,
                             r.T_FIRST_LANG1_NAME,
                             r.T_FIRST_LANG2_NAME,
                             r.T_FATHER_LANG1_NAME,
                             r.T_FATHER_LANG2_NAME,

                             r.T_MOTHER_LANG1_NAME,
                             r.T_MOTHER_LANG2_NAME,
                             r.T_GFATHER_LANG1_NAME,
                             r.T_GFATHER_LANG2_NAME,
                             r.T_BIRTH_DATE,

                             r.T_ADDRESS1,
                             r.T_ADDRESS2,
                             r.T_MRTL_STATUS,
                             r.T_NTNLTY_ID,
                             r.T_PASSPORT_NO,

                             r.T_PHONE_HOME,
                             r.T_PHONE_WORK,
                             r.T_MOBILE_NO,
                             r.T_POSTAL_CODE,
                             r.T_NATIONAL_ID,

                             // r.T_EMP_DEATH,
                             // r.T_IP_EPISODES,
                             //  r.T_BALANCE_AMOUNT,
                             //  r.T_EMAIL_ID,



                             r.T_RLGN_CODE,
                             r.T_SEX_CODE,
                             r.T_OFFICE_NAME,
                             r.T_EMP_DESI_ID,
                             //  r.T_ACCEPTED,

                             r.GE_T_LANG2_NAME,
                             r.Ma_T_LANG2_NAME,
                             r.Re_T_LANG2_NAME,
                             //  r.De_T_LANG2_NAME,
                             r.Na_T_LANG2_NAME
                         }).ToList().Where(x => x.RowNumber >= pageIndex * pageSize + 0 && x.RowNumber <= (pageIndex + 1) * pageSize);
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
        //AmbulanceData(int t_AMBU_REG_ID)

        //For grid Diagnosis
        public IEnumerable GetGridDataDiagnosis(int pageIndex, int pageSize, int PatId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Pa in obj.T74046
                         join Requet in obj.T74041 on Pa.T_PAT_ID equals Requet.T_PAT_ID
                         join PatChkup in obj.T74043 on Requet.T_REQUEST_ID equals PatChkup.T_REQUEST_ID
                         join chief in obj.T74055 on Requet.T_CH_COMP equals chief.T_CH_COMP
                         join Problem in obj.T74077 on Requet.T_PROB_TYPE_ID equals Problem.T_PROB_TYPE_ID
                         // from request in obj.T74041
                         where Requet.T_PAT_ID == PatId
                         select new
                         {
                             //T_PAT_ID= request.T_PAT_ID,
                             T_PAT_ID = Requet.T_PAT_ID,
                             T_ENTRY_DATE = Requet.T_ENTRY_DATE,
                             T_PAT_NO = Pa.T_PAT_NO,
                             T_FIRST_LANG1_NAME = Pa.T_FIRST_LANG1_NAME,
                             T_FIRST_LANG2_NAME = Pa.T_FIRST_LANG2_NAME,
                             T_REQUEST_ID = Requet.T_REQUEST_ID,
                             T_PCHECKUP_ID = PatChkup.T_PCHECKUP_ID,
                             T_TEMP = PatChkup.T_TEMP,
                             T_HIGHT = PatChkup.T_HIGHT,
                             T_WEIGHT = PatChkup.T_WEIGHT,
                             T_BP_SYS = PatChkup.T_BP_SYS,
                             T_BP_DIA = PatChkup.T_BP_DIA,
                             T_PULS = PatChkup.T_PULS,
                             T_BSUGAR_F = PatChkup.T_BSUGAR_F,
                             T_ECG_TEST = PatChkup.T_ECG_TEST,
                             T_URINE_TEST = PatChkup.T_URINE_TEST,
                             T_CH_COMP = Requet.T_CH_COMP,
                             T_PROB_TYPE_ID = Requet.T_PROB_TYPE_ID,
                             T_PROB_DETAILS = Requet.T_PROB_DETAILS,

                             Chie_T_LANG2_NAME = chief.T_LANG2_NAME,
                             Prob_T_LANG2_NAME = Problem.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             RowNumber = i,
                             r.T_PAT_ID,
                             r.T_PAT_NO,
                             r.T_ENTRY_DATE,
                             r.T_FIRST_LANG1_NAME,
                             r.T_FIRST_LANG2_NAME,
                             r.T_REQUEST_ID,
                             r.T_PCHECKUP_ID,
                             r.T_TEMP,
                             r.T_HIGHT,
                             r.T_WEIGHT,
                             r.T_BP_SYS,
                             r.T_BP_DIA,
                             r.T_PULS,
                             r.T_BSUGAR_F,
                             r.T_ECG_TEST,
                             r.T_URINE_TEST,
                             r.T_CH_COMP,
                             r.T_PROB_TYPE_ID,
                             r.T_PROB_DETAILS,
                             r.Chie_T_LANG2_NAME,
                             r.Prob_T_LANG2_NAME
                         }).ToList().Where(x => x.RowNumber >= pageIndex * pageSize + 0 && x.RowNumber <= (pageIndex + 1) * pageSize);
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

        public int GetDiagnosisData_Search_Count(string searchValue, int PatId)
        {
            int query = 0;
            try
            {
                if (searchValue != "")
                {
                    query = (from Pa in obj.T74046
                             join Requet in obj.T74041 on Pa.T_PAT_ID equals Requet.T_PAT_ID
                             join PatChkup in obj.T74043 on Requet.T_REQUEST_ID equals PatChkup.T_REQUEST_ID
                             join chief in obj.T74055 on Requet.T_CH_COMP equals chief.T_CH_COMP
                             join Problem in obj.T74077 on Requet.T_PROB_TYPE_ID equals Problem.T_PROB_TYPE_ID
                             where
                                   Pa.T_FIRST_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Pa.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                   Pa.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                             // Pa.T_PAT_NO.ToString().Contains(searchValue) ||
                             // PatChkup.T_TEMP.ToString().Contains(searchValue) ||
                             // Requet.T_PAT_ID.ToString().Contains(searchValue)
                             select Pa).Count();
                }
                else
                {
                    query = (from Pa in obj.T74046
                             join Requet in obj.T74041 on Pa.T_PAT_ID equals Requet.T_PAT_ID
                             join PatChkup in obj.T74043 on Requet.T_REQUEST_ID equals PatChkup.T_REQUEST_ID
                             join chief in obj.T74055 on Requet.T_CH_COMP equals chief.T_CH_COMP
                             join Problem in obj.T74077 on Requet.T_PROB_TYPE_ID equals Problem.T_PROB_TYPE_ID
                             where Requet.T_PAT_ID == PatId
                             select Pa).Count();
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

        public IEnumerable GetGrid_Data_SearchDiagnosis(string searchValue, int pageIndex, int pageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Pa in obj.T74046
                         join Requet in obj.T74041 on Pa.T_PAT_ID equals Requet.T_PAT_ID
                         join PatChkup in obj.T74043 on Requet.T_REQUEST_ID equals PatChkup.T_REQUEST_ID
                         join chief in obj.T74055 on Requet.T_CH_COMP equals chief.T_CH_COMP
                         join Problem in obj.T74077 on Requet.T_PROB_TYPE_ID equals Problem.T_PROB_TYPE_ID
                         where
                          Pa.T_FIRST_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          Pa.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          Pa.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                         //Pa.T_PAT_NO.ToString().Contains(searchValue) ||
                         //PatChkup.T_TEMP.ToString().Contains(searchValue)||
                         //Requet.T_PAT_ID.ToString().Contains(searchValue)
                         select new
                         {
                             T_PAT_ID = Pa.T_PAT_ID,
                             T_PAT_NO = Pa.T_PAT_NO,
                             T_ENTRY_DATE = Requet.T_ENTRY_DATE,
                             T_FIRST_LANG1_NAME = Pa.T_FIRST_LANG1_NAME,
                             T_FIRST_LANG2_NAME = Pa.T_FIRST_LANG2_NAME,
                             T_REQUEST_ID = Requet.T_REQUEST_ID,
                             T_PCHECKUP_ID = PatChkup.T_PCHECKUP_ID,
                             T_TEMP = PatChkup.T_TEMP,
                             T_HIGHT = PatChkup.T_HIGHT,
                             T_WEIGHT = PatChkup.T_WEIGHT,
                             T_BP_SYS = PatChkup.T_BP_SYS,
                             T_BP_DIA = PatChkup.T_BP_DIA,
                             T_PULS = PatChkup.T_PULS,
                             T_BSUGAR_F = PatChkup.T_BSUGAR_F,
                             T_ECG_TEST = PatChkup.T_ECG_TEST,
                             T_URINE_TEST = PatChkup.T_URINE_TEST,
                             T_CH_COMP = Requet.T_CH_COMP,
                             T_PROB_TYPE_ID = Requet.T_PROB_TYPE_ID,
                             T_PROB_DETAILS = Requet.T_PROB_DETAILS,

                             Chie_T_LANG2_NAME = chief.T_LANG2_NAME,
                             Prob_T_LANG2_NAME = Problem.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             RowNumber = i,
                             r.T_PAT_ID,
                             r.T_PAT_NO,
                             r.T_ENTRY_DATE,
                             r.T_FIRST_LANG1_NAME,
                             r.T_FIRST_LANG2_NAME,
                             r.T_REQUEST_ID,
                             r.T_PCHECKUP_ID,
                             r.T_TEMP,
                             r.T_HIGHT,
                             r.T_WEIGHT,
                             r.T_BP_SYS,
                             r.T_BP_DIA,
                             r.T_PULS,
                             r.T_BSUGAR_F,
                             r.T_ECG_TEST,
                             r.T_URINE_TEST,
                             r.T_CH_COMP,
                             r.T_PROB_TYPE_ID,
                             r.T_PROB_DETAILS,
                             r.Chie_T_LANG2_NAME,
                             r.Prob_T_LANG2_NAME
                         }).ToList().Where(x => x.RowNumber >= pageIndex * pageSize + 0 && x.RowNumber <= (pageIndex + 1) * pageSize);
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
        //===========
        public string SaveEmployee(string lang,T74046 t74046, T74041 t41)
        {
            var sms = "";
            try
            {
                var con = common.chkVerified(t41.T_REQUEST_ID);
           if (con==true)
         {
                 if (t74046.T_PAT_ID == 0)
                 {
                    obj.T74046.Add(t74046);
                    Save();
                     sms = common.GetSingleMsg(lang, "S0012");
                    }
                else
                {
                    var check = obj.T74046.Where(x => x.T_PAT_ID == t74046.T_PAT_ID).FirstOrDefault();
                    check.T_FIRST_LANG2_NAME = t74046.T_FIRST_LANG2_NAME;
                    check.T_FIRST_LANG1_NAME = t74046.T_FIRST_LANG1_NAME;
                    check.T_FATHER_LANG2_NAME = t74046.T_FATHER_LANG2_NAME;
                    check.T_FATHER_LANG1_NAME = t74046.T_FATHER_LANG1_NAME;
                    check.T_MOTHER_LANG2_NAME = t74046.T_MOTHER_LANG2_NAME;
                    check.T_MOTHER_LANG1_NAME = t74046.T_MOTHER_LANG1_NAME;
                    check.T_GFATHER_LANG2_NAME = t74046.T_GFATHER_LANG2_NAME;
                    check.T_GFATHER_LANG1_NAME = t74046.T_GFATHER_LANG1_NAME;
                    check.T_FAMILY_LANG2_NAME = t74046.T_FAMILY_LANG2_NAME;
                    check.T_FAMILY_LANG1_NAME = t74046.T_FAMILY_LANG1_NAME;

                    check.T_BIRTH_DATE = t74046.T_BIRTH_DATE;
                    check.T_RLGN_CODE = t74046.T_RLGN_CODE;
                    check.T_MRTL_STATUS = t74046.T_MRTL_STATUS;
                    check.T_SEX_CODE = t74046.T_SEX_CODE;

                    check.T_NTNLTY_ID = t74046.T_NTNLTY_ID;
                    check.T_ADDRESS1 = t74046.T_ADDRESS1;
                    check.T_POSTAL_CODE = t74046.T_POSTAL_CODE;

                    check.T_NATIONAL_ID = t74046.T_NATIONAL_ID;
                    check.T_PASSPORT_NO = t74046.T_PASSPORT_NO;
                    check.T_MOBILE_NO = t74046.T_MOBILE_NO;

                    check.T_OFFICE_NAME = t74046.T_OFFICE_NAME;
                    check.T_ADDRESS2 = t74046.T_ADDRESS2;
                    check.T_EMP_DESI_ID = t74046.T_EMP_DESI_ID;
                    check.T_PHONE_WORK = t74046.T_PHONE_WORK;
                    check.T_UPD_DATE = DateTime.Now;
                    check.T_UPD_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                    var _t41 = obj.T74041.Where(x => x.T_REQUEST_ID == t41.T_REQUEST_ID).FirstOrDefault();
                    _t41.T_AGE = t41.T_AGE;
                    Save();
                    sms = common.GetSingleMsg(lang, "M0001"); ;
                        // obj.Entry(t74046).State = System.Data.Entity.EntityState.Modified;

                    }

             }
                else
           {
               sms = common.GetSingleMsg(lang, "S0053");
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
            return sms;

        }

        public bool CancelData(T74041 can_t41)
        {
            try
            {
                var cheReq = obj.T74041.Where(p => p.T_REQUEST_ID == can_t41.T_REQUEST_ID).FirstOrDefault();
                if (cheReq != null)
                {
                    cheReq.T_CANCEL_STATUS = "1";
                    //cheReq.T_DISCH_STATUS = "1";
                    cheReq.T_CANCEL_REASON = can_t41.T_CANCEL_REASON;

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
        public string SaveDiagnosis(int rquestId,string lang,T74043 t74043)
        {
            var sms = "";
            
            try
            {
                var con = common.chkVerified(rquestId);
                if (con)
                {
                    //----------------Description ---------------
                    //You have to take 'SCORE' from  query.
                    //int nxtReview = SCORE >= 5 ? 60 : 240;
                    //--Insert T74043
                    //    --You have to Insert 'SCORE' in t74043 from  query.
                    //T_NEXT_REVIEW = to_char((sysdate + (1 / 24 / 60) *{ nxtReview}), 'HH:MI AM')
                    //---------------------------------------
                    var SCORE = _t74046DAL.getMewsScoreData(t74043);
                    byte? mews = byte.Parse(SCORE);
                    t74043.T_SCORE = mews;
                    int nr = mews >= 5 ? 60 : 240;
                    t74043.T_NEXT_REVIEW =nr.ToString() ;

                    //if (t74043.T_PCHECKUP_ID == 0)
                    //{


                    // t74043.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                    t74043.T_ENTRY_DATE = DateTime.Now;
                t74043.T_TIME = DateTime.Now;
               
                obj.T74043.Add(t74043);
                Save();
                sms = common.GetSingleMsg(lang, "S0012");


                    //}
                    //else
                    //{
                    //    var check = obj.T74043.Where(x => x.T_PCHECKUP_ID == t74043.T_PCHECKUP_ID).FirstOrDefault();

                    //    check.T_TEMP = t74043.T_TEMP;
                    //    check.T_HIGHT = t74043.T_HIGHT;
                    //    check.T_WEIGHT = t74043.T_WEIGHT;
                    //    check.T_BP_SYS = t74043.T_BP_SYS;
                    //    check.T_BP_DIA = t74043.T_BP_DIA;
                    //    check.T_PULS = t74043.T_PULS;
                    //    check.T_RESP = t74043.T_RESP;
                    //    check.T_OS = t74043.T_OS;
                    //    check.T_VERIFY_LEVEL = t74043.T_VERIFY_LEVEL;
                    //    check.T_ECG_TEST = t74043.T_ECG_TEST;
                    //    check.T_UPD_DATE = DateTime.Now;
                    //   // obj.Entry(t74043).State = System.Data.Entity.EntityState.Modified;
                    //    Save();
                    //}
                }
                else
                {
                    sms = common.GetSingleMsg(lang, "S0053");
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
            return sms;
        }

        public string SaveDiagT74041(string lang,T74041 t74041)
        {
            var sms = "";
            try
            {
                var con = common.chkVerified(t74041.T_REQUEST_ID);
                if (con)
                {

              if (t74041.T_REQUEST_ID == 0)
                {
                    obj.T74041.Add(t74041);
                    Save();
                    sms = common.GetSingleMsg(lang, "S0012");
                    }
                else
                {
                    var Re = obj.T74041.Where(P => P.T_REQUEST_ID == t74041.T_REQUEST_ID).FirstOrDefault();
                    //Re.T_PAT_ID = t74041.T_PAT_ID;
                    Re.T_PROBLEM = t74041.T_PROBLEM;
                    Re.T_PROB_DETAILS = t74041.T_PROBLEM_DURATION;
                    Re.T_PROB_DETAILS = t74041.T_PROB_DETAILS;
                    Re.T_PROB_TYPE_ID = t74041.T_PROB_TYPE_ID;
                    Re.T_CH_COMP = t74041.T_CH_COMP;
                    Re.T_ICD10_CODE = t74041.T_ICD10_CODE;
                    Re.T_EYE_OPEN = t74041.T_EYE_OPEN;
                    Re.T_BEST_MOTOR = t74041.T_BEST_MOTOR;
                    Re.T_VERBAL_RESPONSE = t74041.T_VERBAL_RESPONSE;
                    //obj.Entry(t74041).State = System.Data.Entity.EntityState.Modified;
                    Save();
                    sms = common.GetSingleMsg(lang, "M0001"); ;
                    }

              }
                else
                  {
                      sms = common.GetSingleMsg(lang, "S0053");
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
            return sms;

        }
        public void Dispose()
        {
            obj.Dispose();
        }

        public void Save()
        {
            obj.SaveChanges();
        }
        //SaveDiagT74041
        //Billing Part-------------Imran-------------Start
        public IEnumerable getId(string uId)
        {
            IEnumerable query = Enumerable.Empty<object>();

            query = (from T41 in obj.T74041
                     join T15 in obj.T74015 on T41.T_AMBU_REG_ID equals T15.T_AMBU_REG_ID
                     join T57 in obj.T74057 on T15.T_EMP_ID equals T57.T_EMP_ID
                     join T39 in obj.T74039 on T41.T_REQUEST_ID equals T39.T_REQUEST_ID
                     //into T74039 from T39 in T74039.DefaultIfEmpty()
                     where T57.T_USER_ID == uId && T41.T_DISCH_STATUS == null
                     select new
                     {
                         T41.T_REQUEST_ID,
                         T39.T_DOC_ID
                     }).ToList();
            return query;
        }
        public IEnumerable AmbulancePrice(int T_REQUEST_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = obj.T74041.Join(obj.T74073, t41 => t41.T_AMBU_REG_ID, t73 => t73.T_ID, (t41, t73) => new { t41, t73 })
                .Join(obj.T74089, t41_73 => t41_73.t73.T_COST_TYPE_DTL_ID, t89 => t89.T_COST_TYPE_DTL_ID, (t41_73, t89) => new { t41_73, t89 })
                .Where(a => a.t41_73.t41.T_REQUEST_ID == T_REQUEST_ID && a.t41_73.t73.T_COST_TYPE_ID == 81 && a.t89.T_ACTIVE == "1")
                .Select(x => new { x.t89.T_SALE_PRICE, x.t89.T_COST_TYPE_DTL_ID });
            return query;
        }

        public IEnumerable DoctorPrice(int T_Doc_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t73 in obj.T74073
                     join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                     where t73.T_COST_TYPE_ID == 104
                     && t73.T_ID == T_Doc_ID
                           && t89.T_ACTIVE == "1"
                     select new
                     {
                         T_PRICE = t89.T_SALE_PRICE,
                         t89.T_COST_TYPE_DTL_ID
                     }
                     ).ToList();



            //query = obj.T74073.Where(a => a.T_COST_TYPE_ID == 104 && a.T_ID == T_Doc_ID)
            //    .Join(obj.T74089, t73=>t73.T_COST_TYPE_DTL_ID,t89=>t89.T_COST_TYPE_DTL_ID,(t73, t89)=>new{ t73, t89 })
            //    .Select(x => new{T_PRICE=x.t89.T_SALE_PRICE });
            return query;
        }

        public IEnumerable ServicePrice(int T_REQUEST_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t27 in obj.T74027
                     join t44 in obj.T74044 on t27.T_STORE_ID equals t44.T_STORE_ID
                     join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                     join t41 in obj.T74041 on t44.T_AMBU_REG_ID equals t41.T_AMBU_REG_ID
                     join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                     where t73.T_COST_TYPE_ID == 121 && t41.T_REQUEST_ID == T_REQUEST_ID && t89.T_ACTIVE == "1"
                     select new
                     {
                         t73.T_LANG2_NAME,
                         T_PRICE = t89.T_SALE_PRICE,
                         t89.T_COST_TYPE_DTL_ID
                     }).ToList();
            //query = (from t36 in obj.T74036
            //    join t37 in obj.T74037 on t36.T_ISSUE_ID equals t37.T_ISSUE_ID
            //    join t44 in obj.T74044 on t36.T_STORE_ID equals t44.T_STORE_ID
            //    join t73 in obj.T74073 on t37.T_ITEM_ID equals t73.T_ID
            //    join t41 in obj.T74041 on t44.T_AMBU_REG_ID equals t41.T_AMBU_REG_ID
            //    join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
            //    where t73.T_COST_TYPE_ID == 121 && t41.T_REQUEST_ID == T_REQUEST_ID && t89.T_ACTIVE == "1"
            //         select new
            //    {
            //        t73.T_LANG2_NAME,
            //        T_PRICE=t89.T_SALE_PRICE,
            //        t89.T_COST_TYPE_DTL_ID
            //         }).ToList();

            return query;
        }
        public IEnumerable DiagonosisByReq(int T_REQUEST_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t39 in obj.T74039
                     join t78 in obj.T74078 on t39.T_PRESCRIPTION_ID equals t78.T_PRESCRIPTION_ID
                     join t73 in obj.T74073 on t78.T_COST_TYPE_DTL_ID equals t73.T_COST_TYPE_DTL_ID
                     join t89 in obj.T74089 on t78.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                     where t39.T_REQUEST_ID == T_REQUEST_ID && t89.T_ACTIVE == "1"
                     select new
                     {
                         t78.T_DIAGONOSIS_ID,
                         t73.T_LANG2_NAME,
                         t89.T_COST_TYPE_DTL_ID,
                         T_PRICE = t89.T_SALE_PRICE
                     }).ToList();



            //query = obj.T74039
            //    .Join(obj.T74078, t39 => t39.T_PRESCRIPTION_ID, t78 => t78.T_PRESCRIPTION_ID,
            //        (t39, t78) => new {t39, t78})
            //    .Join(obj.T74073, t39_78 => t39_78.t78.T_COST_TYPE_DTL_ID, t73 => t73.T_COST_TYPE_DTL_ID,
            //        (t39_78, t73) => new {t39_78, t73}).Where(a => a.t39_78.t39.T_REQUEST_ID == T_REQUEST_ID)
            //        .Join(obj.T74089, t39_78_73=> t39_78_73.t73.T_COST_TYPE_DTL_ID, t89=>t89.T_COST_TYPE_DTL_ID, (t39_78_73,t89)=>new{ t39_78_73, t89 })
            //    .Select(c => new {c.t39_78_73.t73.T_LANG2_NAME, T_PRICE=c.t89.T_SALE_PRICE});
            return query;
        }
        public IEnumerable GetDocId(int T_REQUEST_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t39 in obj.T74039
                     where t39.T_REQUEST_ID == T_REQUEST_ID
                     select t39.T_DOC_ID).ToList();
            return query;
        }

        public DataTable GetMedicineList(int T_request_Id)
        {
            var data = _t74046DAL.GetMedicineList(T_request_Id);
            return data;
        }
        public DataTable GetMedicineListByGen(int T_request_Id, string T_GEN_CODE, string T_REQUEST_STRENGTH, string T_DRUG_ROUTE_CODE, string T_FORM_ID)
        {
            var data = _t74046DAL.GetMedicineListByGen(T_request_Id, T_GEN_CODE, T_REQUEST_STRENGTH, T_DRUG_ROUTE_CODE, T_FORM_ID);
            return data;
        }
        public DataTable GetStock(int T_STORE_ID, int T_ITEM_ID)
        {
            var data = _t74046DAL.GetStock(T_STORE_ID, T_ITEM_ID);
            return data;
        }
        public int chkT36(int id)
        {

            int query = (from T41 in obj.T74041
                         join T36 in obj.T74036 on T41.T_REQUEST_ID equals T36.T_REQUEST_ID
                         where T41.T_REQUEST_ID == id && T41.T_DISCH_STATUS == null
                         select T41.T_REQUEST_ID).ToList().Count;
            return query;
        }
        public int chkT39(int id)
        {

            int query = (from T41 in obj.T74041
                         join T39 in obj.T74039 on T41.T_REQUEST_ID equals T39.T_REQUEST_ID
                         where T41.T_REQUEST_ID == id && T41.T_DISCH_STATUS == null
                         select T41.T_REQUEST_ID).ToList().Count;
            return query;
        }
        public void SaveBill(T74036 t74036, List<T74037> t74037List, T74074 t74074, List<T74079> t74079List)
        {
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    string user = HttpContext.Current.Session["T_USER_ID"].ToString();
                    DateTime? date = DateTime.Now;
                    //T7436-----------------------------------------------------------

                    t74036.T_USER_ID = user;
                    t74036.T_COMPANY_ID = Int32.Parse(HttpContext.Current.Session["T_COMPANY_ID"].ToString());
                    t74036.T_PAT_NO = obj.T74041.Where(a => a.T_REQUEST_ID == t74036.T_REQUEST_ID)
                        .Select(x => x.T_PAT_ID).First().ToString();
                    t74036.T_PRSCRPTN_ID = obj.T74039.Where(a => a.T_REQUEST_ID == t74036.T_REQUEST_ID)
                        .Select(x => x.T_PRESCRIPTION_ID).First().ToString();
                    t74036.T_ENTRY_USER_ = user;
                    t74036.T_ENTRY_DATE_ = date;
                    t74036.T_ISSUE_DATE = date;
                    t74036.T_ISSUED_BY = obj.T74057.Where(a => a.T_USER_ID == user).Select(x => x.T_EMP_ID)
                        .FirstOrDefault();
                    obj.T74036.Add(t74036);
                    Save();
                    int T_ISSUE_ID = obj.T74036.Where(m => m.T_ENTRY_USER_ == t74036.T_ENTRY_USER_).Max(a => a.T_ISSUE_ID);
                    //T74037-----------------------------------------------------------
                    foreach (T74037 newChild in t74037List)
                    {
                        newChild.T_ISSUE_ID = T_ISSUE_ID;

                        var productList = (from t27 in obj.T74027
                                           join t73 in obj.T74073 on t27.T_ITEM_ID equals t73.T_ID
                                           join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                                           join t36 in obj.T74036 on newChild.T_ISSUE_ID equals t36.T_ISSUE_ID
                                           where t27.T_TRNSACTION_QTY > 0
                                           && t73.T_ID == newChild.T_ITEM_ID
                                           && t73.T_COST_TYPE_ID == 23
                                           && t27.T_ITEM_UM_ID == newChild.T_UOM_ID
                                           && t27.T_STORE_ID == t36.T_STORE_ID
                                           && t89.T_ACTIVE == "1"
                                           && t89.T_ITEM_UM_ID == newChild.T_UOM_ID
                                           select new
                                           {
                                               EXP_DATE = t27.T_EXPIRE_DATE,
                                               Qty = t27.T_TRNSACTION_QTY,
                                               ProdId = t27.T_ITEM_ID,
                                               Rate = t89.T_SALE_PRICE,
                                               Stock_Id = t27.T_CUR_STOCK_ID,
                                               t27.T_COMPANY_ID,
                                               t27.T_UNIT_VALUE,
                                               t27.T_MF_DATE,
                                               t27.T_PR_RCV_DTL_ID,
                                               t27.T_STOCK_TYPE_ID,
                                               t27.T_LOT_NO,
                                               t27.T_BATCH,
                                               t27.T_SUPPLIER_ID,
                                               t27.T_ITEM_UM_ID
                                           }).OrderBy(k => k.EXP_DATE).ToList();

                        decimal? quantityRemains = newChild.T_QUANTITY;

                        foreach (var singleObject in productList.Where(p => p.ProdId == newChild.T_ITEM_ID)
                            .Where(q => q.Qty > 0))
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
                            long t37 = obj.T74037.Count() > 0 ? obj.T74037.Max(a => a.T_ISSUE_DTL_ID) : 0;

                            T74037 detailObject = new T74037();
                            detailObject.T_ISSUE_DTL_ID = t37 != 0 ? t37 + 1 : 1;
                            detailObject.T_ISSUE_ID = T_ISSUE_ID;
                            detailObject.T_ENTRY_USER = user;
                            detailObject.T_ENTRY_DATE_ = date;
                            detailObject.T_CUR_STOCK_ID = singleObject.Stock_Id;
                            detailObject.T_EXPIRE_DATE = singleObject.EXP_DATE;
                            detailObject.T_QUANTITY = tQuantity;
                            detailObject.T_TOTAL_AMOUNT = (tQuantity) * (newChild.T_SALE_PRICE);
                            detailObject.T_ITEM_ID = newChild.T_ITEM_ID;
                            detailObject.T_UOM_ID = newChild.T_UOM_ID;
                            detailObject.T_SALE_PRICE = newChild.T_SALE_PRICE;
                            detailObject.T_PCS_BOX = newChild.T_PCS_BOX;
                            obj.T74037.Add(detailObject);
                            Save();
                            var t27 = obj.T74027.Where(p => p.T_CUR_STOCK_ID == singleObject.Stock_Id).FirstOrDefault();
                            if (t27 != null)
                            {
                                t27.T_TRNSACTION_QTY = t27.T_TRNSACTION_QTY - tQuantity;
                                Save();
                            }


                        }


                    }
                    //T74074------------------------------------------------------------
                    t74074.T_ISSUE_ID = T_ISSUE_ID;
                    t74074.T_ENTRY_USER = user;
                    t74074.T_ENTRY_DATE = date;
                    obj.T74074.Add(t74074);
                    Save();
                    int T_BILL_ID = obj.T74074.Count() > 0 ? obj.T74074.Where(m => m.T_ENTRY_USER == t74074.T_ENTRY_USER).Max(a => a.T_BILL_ID) : 1;

                    //T74079------------------------------------------------------------
                    foreach (T74079 t74079 in t74079List)
                    {
                        int T_BILL_DTL_ID = obj.T74079.Count() > 0 ? obj.T74079.Max(a => a.T_BILL_DTL_ID) : 0;
                        T74079 t79 = new T74079();
                        t79.T_BILL_DTL_ID = T_BILL_DTL_ID != 0 ? T_BILL_DTL_ID + 1 : 1;
                        t79.T_BILL_ID = T_BILL_ID;
                        t79.T_DIAGONOSIS_ID = t74079.T_DIAGONOSIS_ID;
                        t79.T_COST_TYPE_DTL_ID = t74079.T_COST_TYPE_DTL_ID;
                        t79.T_PRICE = t74079.T_PRICE;
                        t79.T_VAT = t74079.T_VAT;
                        t79.T_DISCOUNT = t74079.T_DISCOUNT;
                        t79.T_ENTRY_USER = user;
                        t79.T_ENTRY_DATE = date;
                        obj.T74079.Add(t79);
                        Save();

                    }




















                    dbContextTransaction.Commit();
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbContextTransaction.Rollback();
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        public int chkBill(int rId)
        {
            int query = (from T36 in obj.T74036 where T36.T_REQUEST_ID == rId select T36.T_REQUEST_ID).ToList().Count;
            return query;
        }
        public IEnumerable prvServices(int rId)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t79 in obj.T74079
                     join t74 in obj.T74074 on t79.T_BILL_ID equals t74.T_BILL_ID
                     join t73 in obj.T74073 on t79.T_COST_TYPE_DTL_ID equals t73.T_COST_TYPE_DTL_ID
                     join t89 in obj.T74089 on t73.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                     where t74.T_REQUEST_ID == rId && t73.T_COST_TYPE_ID == 121 && t89.T_ACTIVE == "1"
                     select new
                     { t73.T_COST_TYPE_DTL_ID, t73.T_LANG2_NAME, t89.T_SALE_PRICE }).ToList();
            return query;
        }
        public DataTable GetConLevel()
        {

            var data = _t74046DAL.GetConLevel();
            return data;
        }

        //Billing Part-------------Imran-------------End
        public void GPS_Insert(decimal latitude, decimal longitude, string user)
        {
            var time = common.dateTime();
            // var user = HttpContext.Current.Session["T_USER_ID"].ToString();
            var check = obj.T74026.Where(x => x.T_USER_ID == user).FirstOrDefault();
            check.T_LATITUDE = latitude;
            check.T_LONGITUDE = longitude;
            check.T_ENTRY_TIME = time;
            Save();
        }

        //public void AcceptPatient(int requId, string user)
        //{
        //    var check = obj.T74041.Where(x => x.T_REQUEST_ID == requId).FirstOrDefault();
        //    // check.T_UPD_USER = user;
        //    check.T_ACCEPT_STATUS = "1";
        //    check.T_EVENT_FLAG = 1;
        //    check.T_ACCEPT_TIME = common.dateTime();
        //    Save();
        //}

        public string AcceptPatient(int requId, string user, string zone)
        {
            string msg = "";
            var check = obj.T74041.Where(x => x.T_REQUEST_ID == requId).FirstOrDefault();
            // check.T_UPD_USER = user;
            check.T_ACCEPT_STATUS = "1";
            check.T_EVENT_FLAG = 1;
            check.T_ACCEPT_TIME = common.dateTime();
            Save();
            //if (check.T_CHAT_DOC_ID.IsNullOrWhiteSpace())
            //{
                //msg= _t74046DAL.setDoc(zone, check.T_REQUEST_ID, check.T_USER_ID);

            //}
            return msg;

        }


        public string CleanAmbulance(T74117 t74117,string user,string lang)
        {
            var data = _t74046DAL.InsertCleanAmbulance(t74117,user,lang);
            return data;
        }

        public string CleanConfirmAmbulance(T74117 t74117, string user,string lang)
        {
            var data = _t74046DAL.InsertConfirmCleanAmbulance(t74117, user,lang);
            return data;
        }

        public void SeenPatient(int requId, string user)
        {
            var check = obj.T74041.Where(x => x.T_REQUEST_ID == requId).FirstOrDefault();
            check.T_SEEN_TIME = common.dateTime();
            check.T_EVENT_FLAG = 3;
            check.T_SEEN_DATE = common.dateTime();
            Save();
        }
        public void caseRecieved(int requId, string user)
        {
            var check = obj.T74041.Where(x => x.T_REQUEST_ID == requId).FirstOrDefault();
            check.T_CASE_ARRIVAL = common.dateTime();
            check.T_EVENT_FLAG = 4;
            Save();
        }
        public void reqAcceptofOper(int requId, string hosCode, string user)
        {
            var check = obj.T74041.Where(x => x.T_REQUEST_ID == requId).FirstOrDefault();
            check.T_SITE_DIS_CODE = hosCode;
            check.T_OPER_ACPT_DATE = common.dateTime(); //DateTime.Now;
            Save();
        }
        public DataTable getSuggestedHospital(int requId)
        {
            //IEnumerable query = Enumerable.Empty<object>();
            //query = (from t41 in obj.T74041
            //    join t65 in obj.T02065 on t41.T_REF_DOC_CODE equals t65.T_SITE_CODE
            //    where t41.T_REQUEST_ID == requId
            //    select new
            //    {
            //        //T_SITE_CODE=  t65.T_SITE_CODE
            //        T_REF_DOC_CODE= t41.T_REF_DOC_CODE,
            //        T_LANG2_NAME = t65.T_LANG2_NAME
            //    }).ToList();
            //return query;
            return _t74046DAL.getDocHos(requId);
        }
        public DataTable getSuggestedHospitalOper(int requId)
        {
            //IEnumerable query = Enumerable.Empty<object>();
            //query = (from t41 in obj.T74041
            //    join t65 in obj.T02065 on t41.T_REF_DOC_CODE equals t65.T_SITE_CODE
            //    where t41.T_REQUEST_ID == requId
            //    select new
            //    {
            //        //T_SITE_CODE=  t65.T_SITE_CODE
            //        T_REF_DOC_CODE= t41.T_REF_DOC_CODE,
            //        T_LANG2_NAME = t65.T_LANG2_NAME
            //    }).ToList();
            //return query;
            return _t74046DAL.getOprHos(requId);
        }
        public string confirmDocHos(int T_REQUEST_ID, string T_SITE_CODE, string T_ROLE_CODE)
        {
            string msg = "";
            int ch = 0;
            var check = obj.T74041.Where(x => x.T_REQUEST_ID == T_REQUEST_ID).FirstOrDefault();
            if (check != null)
            {
                if (T_ROLE_CODE == "81")
                {
                    check.T_SITE_DIS_CODE = T_SITE_CODE;
                    check.T_DOC_REQ_ACPT_DATE = common.dateTime(); //DateTime.Now;
                    Save();
                  //  msg = "s";
                    ch =1;
                }
                else if (T_ROLE_CODE == "86")
                {
                    check.T_SITE_DIS_CODE = T_SITE_CODE;
                    check.T_OPER_ACPT_DATE = common.dateTime(); //DateTime.Now;
                    Save();
                    ch = 1;
                   // msg = "s";
                }
                else if (T_ROLE_CODE == "111")
                {
                    check.T_SITE_DIS_CODE = T_SITE_CODE;
                    //check.T_OPER_ACPT_DATE = common.dateTime(); //DateTime.Now;
                    Save();
                    ch = 1;
                    // msg = "s";
                }

            }
            if (ch == 1)
            {
                msg = _t74046DAL.UpdateT74111(T_REQUEST_ID, T_SITE_CODE, T_ROLE_CODE);
            }
            // string msg = _t74046DAL.confirmDocHos(T_REQUEST_ID, T_SITE_CODE);
             return msg;
        }

        public string Cancel_Suggested_Hospital(int t_REQUEST_ID, string t_SITE_CODE)
        {
            string sms = _t74046DAL.Cancel_Suggested_Hospital(t_REQUEST_ID, t_SITE_CODE);
            return sms;
        }
        public string CancelAndRerequest(int ambId, T74041 canT41, string company)
        {
            string sms = "";
            int dd = 0;
            // IEnumerable query = Enumerable.Empty<object>();
            if (ambId != 0)
            {

                var query = obj.T74026.Join(obj.T74057, t26 => t26.T_USER_ID, t57 => t57.T_USER_ID,
                        (t26, t57) => new { t26, t57 })
                    .Join(obj.T74004, t26_57 => t26_57.t57.T_EMP_ID, t04 => t04.T_EMP_ID,
                        (t26_57, t04) => new { t26_57, t04 })
                    .Join(obj.T74005, t26_57_t04 => t26_57_t04.t04.T_EMP_TYP_ID, t05 => t05.T_EMP_TYP_ID,
                        (t26_57_t04, t05) => new { t26_57_t04, t05 })
                    .Join(obj.T74015, t26_57_t04_t05 => t26_57_t04_t05.t26_57_t04.t04.T_EMP_ID, t15 => t15.T_EMP_ID,
                        (t26_57_t04_t05, t15) => new { t26_57_t04_t05, t15 }).Where(
                        m => m.t26_57_t04_t05.t05.T_EMP_TYP_ID == 21 && m.t15.T_AMBU_REG_ID == ambId
                    ).Select(m => new { T_USER_ID = m.t26_57_t04_t05.t26_57_t04.t26_57.t26.T_USER_ID }).FirstOrDefault();
                //IEnumerable queryee = Enumerable.Empty<object>();

                var queryee = (from t41 in obj.T74041
                               where t41.T_REQUEST_ID == canT41.T_REQUEST_ID
                               select new
                               {
                                   T_REQUEST_ID = t41.T_REQUEST_ID,
                                   T_PAT_ID = t41.T_PAT_ID,
                                   T_AGE = t41.T_AGE,
                                   T_CH_COMP = t41.T_CH_COMP,
                                   T_REQUEST_DATE = t41.T_REQUEST_DATE,
                                   T_REQUEST_TIME = t41.T_REQUEST_TIME,
                                   T_PROBLEM = t41.T_PROBLEM,
                                   T_PROBLEM_DURATION = t41.T_PROBLEM_DURATION,
                                   T_APPRX_TIME = t41.T_APPRX_TIME,
                                   T_APPRX_DIST = t41.T_APPRX_DIST,
                                   T_USER_ID = query.T_USER_ID,
                                   T_LATITUDE = t41.T_LATITUDE,
                                   T_LONGITUDE = t41.T_LONGITUDE,
                                   T_MAP_LOC = t41.T_MAP_LOC,
                                   T_CALLER_ID = t41.T_CALLER_ID,
                                   T_CALL_REASON_ID = t41.T_CALL_REASON_ID
                               }).ToList();
                if (queryee != null)
                {
                    foreach (var i in queryee)
                    {
                        T74041 newObject = new T74041();
                        newObject.T_PAT_ID = i.T_PAT_ID;
                        newObject.T_USER_ID = i.T_USER_ID;
                        newObject.T_AMBU_REG_ID = ambId;
                        newObject.T_AGE = i.T_AGE;
                        newObject.T_CH_COMP = i.T_CH_COMP;
                        newObject.T_REQUEST_DATE = i.T_REQUEST_DATE;
                        newObject.T_REQUEST_TIME = i.T_REQUEST_TIME;
                        newObject.T_PROBLEM = i.T_PROBLEM;
                        newObject.T_PROBLEM_DURATION = i.T_PROBLEM_DURATION;
                        newObject.T_APPRX_TIME = i.T_APPRX_TIME;
                        newObject.T_APPRX_DIST = i.T_APPRX_DIST;
                        newObject.T_REF_ID = i.T_REQUEST_ID;
                        newObject.T_LATITUDE = i.T_LATITUDE;
                        newObject.T_LONGITUDE = i.T_LONGITUDE;
                        newObject.T_MAP_LOC = i.T_MAP_LOC;
                        newObject.T_CALLER_ID = i.T_CALLER_ID;
                        newObject.T_CALL_REASON_ID = i.T_CALL_REASON_ID;
                        newObject.T_COMPANY_ID = Convert.ToInt32(company);
                        newObject.T_ASSIGN_TIME = common.dateTime();
                        obj.T74041.Add(newObject);
                        Save();
                        dd = 1;
                    }

                }
                if (dd == 1)
                {
                    var chck = obj.T74041.Where(x => x.T_REQUEST_ID == canT41.T_REQUEST_ID).FirstOrDefault();
                    // chck.T_CANCEL_STATUS = "1";
                    // chck.T_DISCH_STATUS = "1";
                    chck.T_EVENT_FLAG = 2;
                    chck.T_CANCEL_REASON = canT41.T_CANCEL_REASON;
                    chck.T_CHAT_FLAG = null;
                    chck.T_CAN_DATE = common.dateTime();
                    Save();
                }//  return query;

            }
            else
            {
                var queryee = (from t41 in obj.T74041
                               where t41.T_REQUEST_ID == canT41.T_REQUEST_ID
                               select new
                               {
                                   T_REQUEST_ID = t41.T_REQUEST_ID,
                                   T_PAT_ID = t41.T_PAT_ID,
                                   T_AGE = t41.T_AGE,
                                   T_CH_COMP = t41.T_CH_COMP,
                                   T_REQUEST_DATE = t41.T_REQUEST_DATE,
                                   T_REQUEST_TIME = t41.T_REQUEST_TIME,
                                   T_PROBLEM = t41.T_PROBLEM,
                                   T_PROBLEM_DURATION = t41.T_PROBLEM_DURATION,
                                   T_APPRX_TIME = t41.T_APPRX_TIME,
                                   T_APPRX_DIST = t41.T_APPRX_DIST,
                                   //T_USER_ID = query.T_USER_ID,
                                   T_LATITUDE = t41.T_LATITUDE,
                                   T_LONGITUDE = t41.T_LONGITUDE,
                                   T_MAP_LOC = t41.T_MAP_LOC,
                                   T_PROTOCAL_NO = t41.T_PROTOCAL_NO,
                                   T_CALLER_ID = t41.T_CALLER_ID,
                                   T_CALL_REASON_ID = t41.T_CALL_REASON_ID

                               }).ToList();
                if (queryee != null)
                {
                    // Comment code by Ruhul Amin , this code for Creating New Request 

                    foreach (var i in queryee)
                    {
                        T74041 newObject = new T74041();
                        newObject.T_PAT_ID = i.T_PAT_ID;
                        //newObject.T_USER_ID = i.T_USER_ID;
                        //newObject.T_AMBU_REG_ID = ambId;
                        newObject.T_AGE = i.T_AGE;
                        newObject.T_CH_COMP = i.T_CH_COMP;
                        newObject.T_REQUEST_DATE = i.T_REQUEST_DATE;
                        newObject.T_REQUEST_TIME = i.T_REQUEST_TIME;
                        newObject.T_PROBLEM = i.T_PROBLEM;
                        newObject.T_PROBLEM_DURATION = i.T_PROBLEM_DURATION;
                        //newObject.T_APPRX_TIME = i.T_APPRX_TIME;
                        //newObject.T_APPRX_DIST = i.T_APPRX_DIST;
                        newObject.T_REF_ID = i.T_REQUEST_ID;
                        newObject.T_LATITUDE = i.T_LATITUDE;
                        newObject.T_LONGITUDE = i.T_LONGITUDE;
                        //newObject.T_MAP_LOC = i.T_MAP_LOC;
                        newObject.T_COMPANY_ID = Convert.ToInt32(company);
                        newObject.T_ASSIGN_TIME = common.dateTime();
                        newObject.T_PROTOCAL_NO = i.T_PROTOCAL_NO;
                        newObject.T_CALLER_ID = i.T_CALLER_ID;
                        newObject.T_CALL_REASON_ID = i.T_CALL_REASON_ID;

                        obj.T74041.Add(newObject);
                        Save();
                        dd = 1;
                    }

                    //var data = _t74046DAL.insert117(canT41);
                    //if (data == "1")
                    //{
                    //    dd = 1;
                    //}
                    


                }
                if (dd == 1)
                {
                    var chck = obj.T74041.Where(x => x.T_REQUEST_ID == canT41.T_REQUEST_ID).FirstOrDefault();
                    // chck.T_CANCEL_STATUS = "1";
                    // chck.T_DISCH_STATUS = "1";
                    chck.T_EVENT_FLAG = 2;
                    chck.T_CANCEL_REASON = canT41.T_CANCEL_REASON;
                    chck.T_CHAT_FLAG = null;
                    chck.T_CAN_DATE = common.dateTime();
                    Save();
                }//  return query;
            }
            return sms;

        }
        public IEnumerable getGCS(int lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t02 in obj.T74102
                     select new
                     {
                         t02.T_GCS_ID,
                         t02.T_GCS_TYPE,
                         t02.T_GCS_VALUE,
                         T_GCS_TEXT = lang == 2 ? t02.T_LANG2_NAME : t02.T_LANG1_NAME
                     }).ToList();
            return query;
        }

        public IEnumerable GetCancelReasonData(int lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t01 in obj.T74101
                     where t01.T_CANCEL_TYPE_ID == 2
                     select new
                     {
                         T_LANG2_NAME = t01.T_LANG2_NAME,
                         T_CANCEL_TYPE_ID = t01.T_CANCEL_REASON_ID,
                     }).ToList();
            return query;
        }

       public IEnumerable getCancelReasonDataForType3()
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t01 in obj.T74101
                where t01.T_CANCEL_TYPE_ID == 3
                select new
                {
                    T_LANG2_NAME = t01.T_LANG2_NAME,
                    T_CANCEL_TYPE_ID = t01.T_CANCEL_REASON_ID,
                }).ToList();
            return query;
        }
        public IEnumerable getECGImg(int T_REQUEST_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t43 in obj.T74043
                     where t43.T_REQUEST_ID == T_REQUEST_ID && t43.T_ECG_TYPE != null
                     select new
                {

                    t43.T_TIME,
                    t43.T_ECG_TEST,
                    t43.T_ECG_TYPE,
                    t43.T_REQUEST_ID,
                    t43.T_PCHECKUP_ID
                     }).AsEnumerable().Select(l=>new
            {
                ECGNAME = l.T_ECG_TYPE+"-"+l.T_TIME.Value.Hour + ":" +
                          l.T_TIME.Value.Minute + ":" +
                          l.T_TIME.Value.Second + ":" +
                          l.T_TIME.Value.Millisecond.ToString().Substring(0, 3),
                l.T_ECG_TEST,
                l.T_ECG_TYPE,
                l.T_REQUEST_ID,
                l.T_PCHECKUP_ID

                }).ToList();
            return query;
        }

       public DataTable getStationArrivalTime(int requId)
       {
           return _t74046DAL.getStationArrivalTime(requId);
       }

        public IEnumerable GetNewReqId(string user)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t41 in obj.T74041
                where t41.T_ACCEPT_TIME == null && t41.T_CAN_DATE==null && t41.T_DISCH_STATUS==null && t41.T_USER_ID== user
                     select new
                {
                    T_REQUEST_ID = t41.T_REQUEST_ID,
                    
                }).ToList();
            return query;
        }

       public DataTable getMewsData(string user, int reId)
       {
           var data = _t74046DAL.getMewsData(user, reId);
           return data;
       }
       
    }
}