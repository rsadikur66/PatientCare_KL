using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Hospital.Implementation.Initialization;
using AmbucareDAL.Repository.Interface.Transaction;
using Microsoft.Ajax.Utilities;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74132Repository : IT74132
    {
        private AmbucareContainer obj = new AmbucareContainer();
        CommonDAL common = new CommonDAL();
        public T74132Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IEnumerable GetRole(string P_USER_ID)
        {
            string USER_ID = P_USER_ID != null ? P_USER_ID : "00001";
            IEnumerable query = Enumerable.Empty<object>();
           
            DataTable dt = common.Query($"SELECT T_ROLE_CODE CODE,T_LANG2_NAME NAME , T_EMP_TYP_ID TYP_ID FROM T74071 where T_EMP_TYP_ID is not Null");

            query =  dt.AsEnumerable().AsQueryable().Select(i => new
                {
                    CODE = i["CODE"].ToString(),
                    NAME = i["NAME"].ToString(),
                    TYP_ID = i["TYP_ID"].ToString(),
                }).ToList();
            //query = (from n in (from t66 in obj.T74066
            //                    join t71 in obj.T74071 on t66.T_ROLE_CODE equals t71.T_ROLE_CODE
            //                    where t66.T_USER_ID == USER_ID
            //                    select new { CODE = t66.T_ROLE_CODE, NAME = t71.T_LANG2_NAME }).AsEnumerable().ToList()
            //         group n by new { n.CODE, n.NAME }
            //        into t
            //         select new { t.Key.CODE, t.Key.NAME }).OrderBy(q => q.CODE).AsEnumerable().ToList();
            return query;
        }
        public IEnumerable GetUserType(string P_TYPE)
        {
            IEnumerable query = Enumerable.Empty<object>();
            if (P_TYPE=="w")
            {
                query = (from t05 in obj.T74005
                    join t04 in obj.T74004 on t05.T_EMP_TYP_ID equals t04.T_EMP_TYP_ID
                    join t57 in obj.T74057 on t04.T_EMP_ID equals t57.T_EMP_ID
                    group new { t05, t04, t57 } by new { t05.T_EMP_TYP_ID, t05.T_LANG2_NAME }
                    into t
                    select new { CODE = t.Key.T_EMP_TYP_ID, NAME = t.Key.T_LANG2_NAME }).OrderBy(a => a.NAME).AsEnumerable().ToList();
            }
            else if (P_TYPE=="m")
            {
                query = (from t05 in obj.T74005
                    join tm in obj.T74M01 on t05.T_EMP_TYP_ID equals tm.T_USER_TYPE
                    group new { t05, tm } by new { t05.T_EMP_TYP_ID, t05.T_LANG2_NAME }
                    into t
                    select new { CODE = t.Key.T_EMP_TYP_ID, NAME = t.Key.T_LANG2_NAME }).OrderBy(a => a.NAME).AsEnumerable().ToList();
            }
            
            return query;
        }
        public IEnumerable GetFormType(int? P_ROLE_CODE, string P_USER_ID)
        {
            string USER_ID = P_USER_ID != null ? P_USER_ID : "00001";

            IEnumerable query = Enumerable.Empty<object>();
            DataTable dt =
                common.Query($"SELECT t70.T_FORM_TYPE_ID, t70.T_LANG2_NAME NAME FROM T74070 t70 WHERE t70.T_FORM_TYPE_ID != '0'");
            query = dt.AsEnumerable().AsQueryable().Select(i => new
            {
                CODE = i["T_FORM_TYPE_ID"].ToString(),
                NAME = i["NAME"].ToString(),
            }).ToList();
            
            //query = (from t66 in obj.T74066
            //    join t70 in obj.T74070 on t66.T_FORM_TYPE_ID equals t70.T_FORM_TYPE_ID
            //    where t66.T_USER_ID == USER_ID && t66.T_ROLE_CODE == P_ROLE_CODE
            //         group new { t66, t70} by new { t70.T_FORM_TYPE_ID, t70.T_LANG2_NAME }
            //    into t
            //    select new { CODE = t.Key.T_FORM_TYPE_ID, NAME = t.Key.T_LANG2_NAME }).OrderBy(a => a.CODE).AsEnumerable().ToList();
            return query;

        }





















        public IEnumerable GetUserList(string P_USER_TYPE, int P_EMP_TYP_ID, string P_USER_STATUS, int P_FORM_TYPE_ID, string siteCode)
        {
            IEnumerable query = Enumerable.Empty<object>();
            if (P_USER_TYPE == "m")
            {
                if (P_EMP_TYP_ID == 61)
                {
                    if (P_USER_STATUS == "Ins")
                    {
                        query = (from t46 in obj.T74046
                                 join tm01 in obj.T74M01 on t46.T_PAT_ID equals tm01.T_EMP_ID
                                 where tm01.T_IS_ACTIVE == "1" && tm01.T_USER_TYPE == P_EMP_TYP_ID && !(from s in obj.T74066 select s.T_USER_ID).Contains(tm01.T_USER_ID)
                                 group tm01 by new
                                 {
                                     t46.T_PAT_ID,
                                     t46.T_FATHER_LANG2_NAME,
                                     t46.T_GFATHER_LANG2_NAME,
                                     t46.T_FIRST_LANG2_NAME,
                                     t46.T_FAMILY_LANG2_NAME
                                 } into m
                                 select new
                                 {
                                     m.Key.T_PAT_ID,
                                     m.Key.T_FATHER_LANG2_NAME,
                                     m.Key.T_GFATHER_LANG2_NAME,
                                     m.Key.T_FIRST_LANG2_NAME,
                                     m.Key.T_FAMILY_LANG2_NAME
                                 }).AsEnumerable().Select((r, i) => new
                                 {
                                     SLNO = i,
                                     CODE = r.T_PAT_ID,
                                     NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME

                                 }).OrderBy(s => s.NAME).ToList();
                    }
                    else if (P_USER_STATUS == "Upd")
                    {
                        query = (from t46 in obj.T74046
                                 join tm01 in obj.T74M01 on t46.T_PAT_ID equals tm01.T_EMP_ID
                                 join t66 in obj.T74066 on tm01.T_USER_ID equals t66.T_USER_ID
                                 where tm01.T_IS_ACTIVE == "1" && tm01.T_USER_TYPE == P_EMP_TYP_ID && t66.T_TYPE == "m"
                                 group tm01 by new
                                 {
                                     t46.T_PAT_ID,
                                     t46.T_FATHER_LANG2_NAME,
                                     t46.T_GFATHER_LANG2_NAME,
                                     t46.T_FIRST_LANG2_NAME,
                                     t46.T_FAMILY_LANG2_NAME
                                 } into m
                                 select new
                                 {
                                     m.Key.T_PAT_ID,
                                     m.Key.T_FATHER_LANG2_NAME,
                                     m.Key.T_GFATHER_LANG2_NAME,
                                     m.Key.T_FIRST_LANG2_NAME,
                                     m.Key.T_FAMILY_LANG2_NAME
                                 }).AsEnumerable().Select((r, i) => new
                                 {
                                     SLNO = i,
                                     CODE = r.T_PAT_ID,
                                     NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME

                                 }).OrderBy(s => s.NAME).ToList();
                    }
                }
                else
                {
                    if (P_USER_STATUS == "Ins")
                    {
                        query = (from t04 in obj.T74004
                                 join tm01 in obj.T74M01 on t04.T_EMP_ID equals tm01.T_EMP_ID
                                 where tm01.T_IS_ACTIVE == "1" && tm01.T_USER_TYPE == P_EMP_TYP_ID && !(from s in obj.T74066 select s.T_USER_ID).Contains(tm01.T_USER_ID)
                                 group tm01 by new
                                 {
                                     t04.T_EMP_ID,
                                     t04.T_FATHER_LANG2_NAME,
                                     t04.T_GFATHER_LANG2_NAME,
                                     t04.T_FIRST_LANG2_NAME,
                                     t04.T_FAMILY_LANG2_NAME
                                 } into m
                                 select new
                                 {
                                     m.Key.T_EMP_ID,
                                     m.Key.T_FATHER_LANG2_NAME,
                                     m.Key.T_GFATHER_LANG2_NAME,
                                     m.Key.T_FIRST_LANG2_NAME,
                                     m.Key.T_FAMILY_LANG2_NAME
                                 }).AsEnumerable().Select((r, i) => new
                                 {
                                     SLNO = i,
                                     CODE = r.T_EMP_ID,
                                     NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME

                                 }).OrderBy(s => s.NAME).ToList();
                    }
                    else if (P_USER_STATUS == "Upd")
                    {
                        query = (from t04 in obj.T74004
                                 join tm01 in obj.T74M01 on t04.T_EMP_ID equals tm01.T_EMP_ID
                                 join t66 in obj.T74066 on tm01.T_USER_ID equals t66.T_USER_ID
                                 where tm01.T_IS_ACTIVE == "1" && tm01.T_USER_TYPE == P_EMP_TYP_ID && t66.T_TYPE == "m"
                                 group tm01 by new
                                 {
                                     t04.T_EMP_ID,
                                     t04.T_FATHER_LANG2_NAME,
                                     t04.T_GFATHER_LANG2_NAME,
                                     t04.T_FIRST_LANG2_NAME,
                                     t04.T_FAMILY_LANG2_NAME
                                 } into m
                                 select new
                                 {
                                     m.Key.T_EMP_ID,
                                     m.Key.T_FATHER_LANG2_NAME,
                                     m.Key.T_GFATHER_LANG2_NAME,
                                     m.Key.T_FIRST_LANG2_NAME,
                                     m.Key.T_FAMILY_LANG2_NAME
                                 }).AsEnumerable().Select((r, i) => new
                                 {
                                     SLNO = i,
                                     CODE = r.T_EMP_ID,
                                     NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME

                                 }).OrderBy(s => s.NAME).ToList();
                    }

                }
            }
            else if (P_USER_TYPE == "w")
            {
                if (P_USER_STATUS == "Ins" )
                {
                 DataTable dt=   common.Query($"SELECT DISTINCT t57.T_USER_ID, T_FIRST_LANG2_NAME ||' ' ||T_LAST_LANG2_NAME NAME, t66.T_INACTIVE_FLAG  FROM T74004 t04 JOIN T74057 t57 ON t04.T_EMP_ID = t57.T_EMP_ID LEFT JOIN T74066 t66 ON t57.T_USER_ID = t66.T_USER_ID AND t66.T_INACTIVE_FLAG IS NOT NULL AND t66.T_INACTIVE_FLAG != 'i' AND T_FORM_TYPE_ID = '{P_FORM_TYPE_ID}' WHERE t57.T_ACTIVE_FLAG = 1 AND t04.T_EMP_TYP_ID = '{P_EMP_TYP_ID}' AND t57.T_ZONE_CODE = '{siteCode}'");
                // DataTable dt=   common.Query($"SELECT t57.T_USER_ID,T_FIRST_LANG2_NAME ||' '||T_LAST_LANG2_NAME NAME FROM T74004 t04 JOIN T74057 t57 ON t04.T_EMP_ID = t57.T_EMP_ID where t57.T_ACTIVE_FLAG = 1 AND t04.T_EMP_TYP_ID = '{P_EMP_TYP_ID}'");
                    query = dt.AsEnumerable().AsQueryable().Select(i => new
                    {
                        CODE =i["T_USER_ID"].ToString(),
                        NAME = i["NAME"].ToString(),
                        T_CHK_SINGLE_USER = i["T_INACTIVE_FLAG"].ToString(),
                        T_INACTIVE_FLAG = i["T_INACTIVE_FLAG"].ToString(),
                        T_CHCK ="",
                    }).ToList();
                    //query = (from t04 in obj.T74004
                    //         join t57 in obj.T74057 on t04.T_EMP_ID equals t57.T_EMP_ID
                    //         where t57.T_ACTIVE_FLAG == 1 && t57.T_USER_ID!="00001" && t04.T_EMP_TYP_ID == P_EMP_TYP_ID && !(from s in obj.T74066.Distinct() where s.T_FORM_TYPE_ID == P_FORM_TYPE_ID select s.T_USER_ID).Contains(t57.T_USER_ID)
                    //         group t57 by new
                    //         {
                    //             t57.T_USER_ID,
                    //             t04.T_FATHER_LANG2_NAME,
                    //             t04.T_GFATHER_LANG2_NAME,
                    //             t04.T_FIRST_LANG2_NAME,
                    //             t04.T_FAMILY_LANG2_NAME
                    //         } into m
                    //         select new
                    //         {
                    //             m.Key.T_USER_ID,
                    //             m.Key.T_FATHER_LANG2_NAME,
                    //             m.Key.T_GFATHER_LANG2_NAME,
                    //             m.Key.T_FIRST_LANG2_NAME,
                    //             m.Key.T_FAMILY_LANG2_NAME,
                    //         }).AsEnumerable().Select((r, i) => new
                    //         {
                    //             SLNO = i,
                    //             CODE = r.T_USER_ID,
                    //             NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME

                    //         }).OrderBy(v => v.NAME).ToList();
                }
                else if (P_USER_STATUS == "Upd")
                {
                    DataTable dt = common.Query($"SELECT DISTINCT t57.T_USER_ID, T_FIRST_LANG2_NAME ||' ' ||T_LAST_LANG2_NAME NAME FROM T74004 t04 JOIN T74057 t57 ON t04.T_EMP_ID = t57.T_EMP_ID LEFT JOIN T74066 t66 ON t57.T_USER_ID = t66.T_USER_ID WHERE t57.T_ACTIVE_FLAG = 1 AND t57.T_ZONE_CODE = '{siteCode}' AND t04.T_EMP_TYP_ID = '{P_EMP_TYP_ID}'");

                    query = dt.AsEnumerable().AsQueryable().Select(i => new
                    {
                        CODE = i["T_USER_ID"].ToString(),
                        NAME = i["NAME"].ToString(),

                    }).ToList();

                    //query = (from t04 in obj.T74004
                    //         join t57 in obj.T74057 on t04.T_EMP_ID equals t57.T_EMP_ID
                    //         join t66s in obj.T74066 on t57.T_USER_ID equals t66s.T_USER_ID into t6s
                    //         from t66 in t6s.DefaultIfEmpty()
                    //         where t57.T_ACTIVE_FLAG == 1
                    //         // && t57.T_USER_ID != "00001" 
                    //         && t57.T_ZONE_CODE == siteCode
                    //         && t04.T_EMP_TYP_ID == P_EMP_TYP_ID
                    //         && t66.T_TYPE == "w"
                    //         group t57 by new
                    //         {
                    //             t57.T_USER_ID,
                    //             t04.T_FATHER_LANG2_NAME,
                    //             t04.T_GFATHER_LANG2_NAME,
                    //             t04.T_FIRST_LANG2_NAME,
                    //             t04.T_FAMILY_LANG2_NAME
                    //         } into m
                    //         select new
                    //         {
                    //             m.Key.T_USER_ID,
                    //             m.Key.T_FATHER_LANG2_NAME,
                    //             m.Key.T_GFATHER_LANG2_NAME,
                    //             m.Key.T_FIRST_LANG2_NAME,
                    //             m.Key.T_FAMILY_LANG2_NAME,
                    //         }).AsEnumerable().Select((r, i) => new
                    //         {
                    //             SLNO = i,
                    //             CODE = r.T_USER_ID,
                    //             NAME = r.T_FIRST_LANG2_NAME + " " + r.T_FATHER_LANG2_NAME + " " + r.T_GFATHER_LANG2_NAME + " " + r.T_FAMILY_LANG2_NAME

                    //         }).OrderBy(v => v.NAME).ToList();
                }


            }
            return query;
        }
        
        public string GetFormAccess(string P_FORM_CODE, string P_USER_ID, int P_ROLE_CODE)
        {
            string USER_ID = P_USER_ID == "00001" ? null:P_USER_ID ;
            var query = obj.T74066.Where(a => a.T_FORM_CODE == P_FORM_CODE && a.T_USER_ID == USER_ID && a.T_ROLE_CODE == P_ROLE_CODE).ToList();
            string res = query.Count > 0 ? "A" : "B";
            return res;
        }
        public IEnumerable GetFormList(int P_FORM_TYPE_ID, int P_ROLE_CODE, string P_USER_ID,string user)
        {
            string USER_ID = P_USER_ID != null ? P_USER_ID : "00001";
            IEnumerable query = Enumerable.Empty<object>();

            DataTable dt =
                common.Query($"SELECT DISTINCT T_FORM_CODE,T_LANG2_NAME NAME,T_PAGE_TYPE_ID,T_ORDER, T_TYPE,T_LANG1_NAME,NULL AS T_INACTIVE_FLAG FROM T74066 WHERE T_FORM_TYPE_ID = '{P_FORM_TYPE_ID}' AND T_INACTIVE_FLAG = '1' AND T_USER_ID = '00001' AND T_TYPE = '{user}'");

            query = dt.AsEnumerable().AsQueryable().Select(i => new
            {
                CODE = i["T_FORM_CODE"].ToString(),
                NAME = i["NAME"].ToString(),
                T_PAGE_TYPE_ID = i["T_PAGE_TYPE_ID"].ToString(),
                T_ORDER = i["T_ORDER"].ToString(),
                T_TYPE = i["T_TYPE"].ToString(),
                T_LANG1_NAME = i["T_LANG1_NAME"].ToString(),
                T_INACTIVE_FLAG = i["T_INACTIVE_FLAG"].ToString(),
                UPDATED ="",
               // UPDATED = GetFormAccess(i["T_FORM_CODE"].ToString(), USER_ID, P_ROLE_CODE)
            }).ToList();

            


                    //query = obj.T74066.Where(a => a.T_FORM_TYPE_ID == P_FORM_TYPE_ID && a.T_LANG2_NAME != null && a.T_ROLE_CODE == P_ROLE_CODE && a.T_INACTIVE_FLAG=="1"&& a.T_USER_ID== "00001")
                    //    .GroupBy(s => new { s.T_FORM_CODE, s.T_LANG2_NAME,s.T_PAGE_TYPE_ID,s.T_ORDER, s.T_TYPE,s.T_LANG1_NAME, s.T_INACTIVE_FLAG }).AsEnumerable()
                    //    .Select(x => new { CODE = x.Key.T_FORM_CODE, NAME = x.Key.T_LANG2_NAME,x.Key.T_PAGE_TYPE_ID,x.Key.T_ORDER,x.Key.T_TYPE,x.Key.T_LANG1_NAME,x.Key.T_INACTIVE_FLAG,UPDATED = GetFormAccess(x.Key.T_FORM_CODE, USER_ID, P_ROLE_CODE) }).OrderBy(z => z.UPDATED).ThenBy(c=>c.T_PAGE_TYPE_ID).ThenBy(d => d.T_ORDER).ToList();


            
            return query;


        }

        public string Insert(List<T74066> t74066, string status)
        {
            string msg = "Nothing Has Saved Yet";
            using (var dbContextTransaction = obj.Database.BeginTransaction())
            {
                try
                {
                    if (status == "Ins")
                    {
                        foreach (var t66 in t74066)
                        {
                            var chck = obj.T74066
                                .Where(x => x.T_USER_ID == t66.T_USER_ID && x.T_FORM_CODE == t66.T_FORM_CODE &&
                                            x.T_FORM_TYPE_ID == t66.T_FORM_TYPE_ID &&
                                            x.T_ROLE_CODE == t66.T_ROLE_CODE && x.T_INACTIVE_FLAG != "i")
                                .FirstOrDefault();

                            if (chck != null)
                            {
                                if (t66.T_INACTIVE_FLAG == "F")
                                {
                                    chck.T_INACTIVE_FLAG = "";
                                    Save();
                                }
                                else if (t66.T_INACTIVE_FLAG == "T")
                                {
                                    chck.T_INACTIVE_FLAG = "1";
                                    Save();
                                }

                            }
                            else
                            {
                                if (t66.T_INACTIVE_FLAG == "F")
                                {
                                    
                                }
                                else if (t66.T_INACTIVE_FLAG == "T")
                                {
                                    T74066 tn = new T74066();
                                    tn.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                    tn.T_ENTRY_DATE = DateTime.Now;
                                    tn.T_UPD_USER = t66.T_UPD_USER;
                                    tn.T_UPD_DATE = t66.T_UPD_DATE;
                                    tn.T_FORM_CODE = t66.T_FORM_CODE;
                                    tn.T_LANG1_NAME = t66.T_LANG1_NAME;
                                    tn.T_LANG2_NAME = t66.T_LANG2_NAME;
                                    tn.T_VERSION_NO = t66.T_VERSION_NO;
                                    tn.T_REPORT_FLAG = t66.T_REPORT_FLAG;
                                    tn.T_FORM_TYPE_ID = t66.T_FORM_TYPE_ID;
                                    tn.T_USER_ID = t66.T_USER_ID;
                                    tn.T_INACTIVE_FLAG = "1";
                                    tn.T_PAGE_TYPE_ID = t66.T_PAGE_TYPE_ID;
                                    tn.T_ORDER = t66.T_ORDER;
                                    tn.T_TYPE = t66.T_TYPE;
                                    tn.T_ROLE_CODE = t66.T_ROLE_CODE;
                                    tn.T_FORM_CODE_ID = obj.T74066.Max(a => a.T_FORM_CODE_ID) + 1;
                                    obj.T74066.Add(tn);
                                    Save();
                                }

                                
                            }
                            //int p = obj.T74066.Where(a => a.T_USER_ID == t66.T_USER_ID && a.T_ROLE_CODE == t66.T_ROLE_CODE && a.T_INACTIVE_FLAG == "i").ToList().Count;
                            //if (p == 0)
                            //{
                            //    List<T74066> t66_internal = obj.T74066.Where(a => a.T_USER_ID == "00001" && a.T_ROLE_CODE == t66.T_ROLE_CODE && a.T_INACTIVE_FLAG == "i").ToList();
                            //    foreach (var t in t66_internal)
                            //    {
                            //        T74066 tn = new T74066();
                            //        tn.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                            //        tn.T_ENTRY_DATE = DateTime.Now;
                            //        tn.T_UPD_USER = t.T_UPD_USER;
                            //        tn.T_UPD_DATE = t.T_UPD_DATE;
                            //        tn.T_FORM_CODE = t.T_FORM_CODE;
                            //        tn.T_LANG1_NAME = t.T_LANG1_NAME;
                            //        tn.T_LANG2_NAME = t.T_LANG2_NAME;
                            //        tn.T_VERSION_NO = t.T_VERSION_NO;
                            //        tn.T_REPORT_FLAG = t.T_REPORT_FLAG;
                            //        tn.T_FORM_TYPE_ID = t.T_FORM_TYPE_ID;
                            //        tn.T_USER_ID = t66.T_USER_ID;
                            //        tn.T_INACTIVE_FLAG = t.T_INACTIVE_FLAG;
                            //        tn.T_PAGE_TYPE_ID = t.T_PAGE_TYPE_ID;
                            //        tn.T_ORDER = t.T_ORDER;
                            //        tn.T_TYPE = t.T_TYPE;
                            //        tn.T_ROLE_CODE = t.T_ROLE_CODE;
                            //        tn.T_FORM_CODE_ID = obj.T74066.Max(a => a.T_FORM_CODE_ID) + 1;
                            //        obj.T74066.Add(tn);
                            //        Save();

                            //    }
                            //}
                            //if (t66.T_INACTIVE_FLAG=="A")
                            //{
                            //    t66.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                            //    t66.T_ENTRY_DATE = DateTime.Now;
                            //    t66.T_FORM_CODE_ID = obj.T74066.Max(a => a.T_FORM_CODE_ID) + 1;
                            //    t66.T_INACTIVE_FLAG = "1";
                            //    obj.T74066.Add(t66);
                            //    Save();
                            //}

                        }
                        msg = "Data Saved Sucessfully";


                    }
                    else if (status == "Upd")
                    {
                      
                        foreach (var t66 in t74066)
                        {
                 var chck = obj.T74066
                                .Where(x =>x.T_USER_ID == t66.T_USER_ID && x.T_FORM_CODE == t66.T_FORM_CODE &&
                                            x.T_FORM_TYPE_ID == t66.T_FORM_TYPE_ID &&
                                            x.T_ROLE_CODE == t66.T_ROLE_CODE && x.T_INACTIVE_FLAG != "i")
                                .FirstOrDefault();

                            if (chck!=null)
                            {
                                if (t66.T_INACTIVE_FLAG =="F")
                                {
                                    chck.T_INACTIVE_FLAG = "";
                                    Save();
                                }
                                else if (t66.T_INACTIVE_FLAG == "T")
                                {
                                    chck.T_INACTIVE_FLAG = "1";
                                    Save();
                                }
                                
                            }
                            else
                            {
                                T74066 tn = new T74066();
                       tn.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                        tn.T_ENTRY_DATE = DateTime.Now;
                        tn.T_UPD_USER = t66.T_UPD_USER;
                        tn.T_UPD_DATE = t66.T_UPD_DATE;
                        tn.T_FORM_CODE = t66.T_FORM_CODE;
                        tn.T_LANG1_NAME = t66.T_LANG1_NAME;
                        tn.T_LANG2_NAME = t66.T_LANG2_NAME;
                        tn.T_VERSION_NO = t66.T_VERSION_NO;
                        tn.T_REPORT_FLAG = t66.T_REPORT_FLAG;
                        tn.T_FORM_TYPE_ID = t66.T_FORM_TYPE_ID;
                        tn.T_USER_ID = t66.T_USER_ID;
                        tn.T_INACTIVE_FLAG = "1";
                        tn.T_PAGE_TYPE_ID = t66.T_PAGE_TYPE_ID;
                        tn.T_ORDER = t66.T_ORDER;
                        tn.T_TYPE = t66.T_TYPE;
                        tn.T_ROLE_CODE = t66.T_ROLE_CODE;
                        tn.T_FORM_CODE_ID = obj.T74066.Max(a => a.T_FORM_CODE_ID) + 1;
                                obj.T74066.Add(tn);
                                Save();
                            }
                            //    List<T74066> u = obj.T74066.Where(a => a.T_USER_ID == t66.T_USER_ID && a.T_ROLE_CODE == t66.T_ROLE_CODE && a.T_INACTIVE_FLAG == "i").ToList();
                            //    List<T74066> m = obj.T74066.Where(a => a.T_USER_ID == "00001" && a.T_ROLE_CODE == t66.T_ROLE_CODE && a.T_INACTIVE_FLAG == "i").ToList();

                            //    if (m.Count > u.Count)
                            //    {
                            //        foreach (var t in m)
                            //        {
                            //            if (u.Where(f => f.T_FORM_CODE == t.T_FORM_CODE).ToList().Count == 0)
                            //            {
                            //                T74066 tn = new T74066();
                            //                tn.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                            //                tn.T_ENTRY_DATE = DateTime.Now;
                            //                tn.T_UPD_USER = t.T_UPD_USER;
                            //                tn.T_UPD_DATE = t.T_UPD_DATE;
                            //                tn.T_FORM_CODE = t.T_FORM_CODE;
                            //                tn.T_LANG1_NAME = t.T_LANG1_NAME;
                            //                tn.T_LANG2_NAME = t.T_LANG2_NAME;
                            //                tn.T_VERSION_NO = t.T_VERSION_NO;
                            //                tn.T_REPORT_FLAG = t.T_REPORT_FLAG;
                            //                tn.T_FORM_TYPE_ID = t.T_FORM_TYPE_ID;
                            //                tn.T_USER_ID = t66.T_USER_ID;
                            //                tn.T_INACTIVE_FLAG = t.T_INACTIVE_FLAG;
                            //                tn.T_PAGE_TYPE_ID = t.T_PAGE_TYPE_ID;
                            //                tn.T_ORDER = t.T_ORDER;
                            //                tn.T_TYPE = t.T_TYPE;
                            //                tn.T_ROLE_CODE = t.T_ROLE_CODE;
                            //                tn.T_FORM_CODE_ID = obj.T74066.Max(a => a.T_FORM_CODE_ID) + 1;
                            //                obj.T74066.Add(tn);
                            //                Save();
                            //            }

                            //        }
                            //    }


                            //    List<T74066> updateUser = obj.T74066.Where(a => a.T_USER_ID == t66.T_USER_ID && a.T_ROLE_CODE == t66.T_ROLE_CODE && a.T_INACTIVE_FLAG != "i" && a.T_FORM_TYPE_ID == t66.T_FORM_TYPE_ID).ToList();
                            //    if (updateUser.Where(f => f.T_FORM_CODE == t66.T_FORM_CODE).ToList().Count == 0)
                            //    {
                            //        if (t66.T_INACTIVE_FLAG == "T")
                            //        {
                            //            t66.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                            //            t66.T_ENTRY_DATE = DateTime.Now;
                            //            t66.T_FORM_CODE_ID = obj.T74066.Max(a => a.T_FORM_CODE_ID) + 1;
                            //            t66.T_INACTIVE_FLAG = "1";
                            //            obj.T74066.Add(t66);
                            //        }
                            //        else if (t66.T_INACTIVE_FLAG=="F")
                            //        {
                            //            T74066 d = updateUser.Where(f => f.T_FORM_CODE == t66.T_FORM_CODE).FirstOrDefault();
                            //            if (d != null)
                            //            {
                            //                obj.T74066.Remove(d);

                            //            }
                            //        }
                            //        Save();

                            //    }
                            //    List<T74066> deleteUser = obj.T74066.Where(a => a.T_USER_ID == t66.T_USER_ID && a.T_ROLE_CODE == t66.T_ROLE_CODE && a.T_INACTIVE_FLAG != "i").ToList();
                            //    if (deleteUser.Count==0)
                            //    {
                            //        foreach (var d in m)
                            //        {
                            //            obj.T74066.Remove(d);
                            //            Save();
                            //        }
                            //    }

                        }

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
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }
                }
            }
                
            return msg;
        }

        public IEnumerable GetFdataByUser(string fmCode, string rCode, string uData)
        {
            IEnumerable query = Enumerable.Empty<object>();
            int rocd = Int32.Parse(rCode);
            DataTable dt =
                common.Query($"SELECT T_1.*,t6.T_INACTIVE_FLAG FROM ( SELECT DISTINCT T_USER_ID, T_FORM_CODE, T_LANG2_NAME NAME, T_PAGE_TYPE_ID, T_ORDER, T_TYPE, T_LANG1_NAME FROM T74066 WHERE T_FORM_TYPE_ID = '{fmCode}' AND T_INACTIVE_FLAG = '1' AND T_USER_ID = '00001' AND T_TYPE = 'w' )T_1 LEFT JOIN T74066 t6 on T_1.T_FORM_CODE = t6.T_FORM_CODE AND t6.T_USER_ID = '{uData}' AND t6.T_INACTIVE_FLAG = '1'");

            query = dt.AsEnumerable().AsQueryable().Select(i => new
            {
                CODE = i["T_FORM_CODE"].ToString(),
                NAME = i["NAME"].ToString(),
                T_PAGE_TYPE_ID = i["T_PAGE_TYPE_ID"].ToString(),
                T_ORDER = i["T_ORDER"].ToString(),
                T_TYPE = i["T_TYPE"].ToString(),
                T_LANG1_NAME = i["T_LANG1_NAME"].ToString(),
                T_INACTIVE_FLAG = i["T_INACTIVE_FLAG"].ToString(),
                UPDATED ="",
               // UPDATED = GetFormAccess(i["T_FORM_CODE"].ToString(), "00001", rocd)
            }).ToList();
            return query;
        }

        public IEnumerable GetSiteData(string site)
        {
            IEnumerable query = Enumerable.Empty<object>();
            DataTable dt =
                common.Query($"SELECT DISTINCT t64.T_LANG2_NAME T_NAME,t64.T_ZONE_CODE FROM T02064 t64 JOIN T74057 t57 ON t64.T_ZONE_CODE = t57.T_ZONE_CODE WHERE t64.T_ZONE_CODE = NVL('{site}',t64.T_ZONE_CODE)");

            query = dt.AsEnumerable().AsQueryable().Select(i => new
            {
                CODE = i["T_ZONE_CODE"].ToString(),
                NAME = i["T_NAME"].ToString(),
            }).ToList();
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
        
    }

}