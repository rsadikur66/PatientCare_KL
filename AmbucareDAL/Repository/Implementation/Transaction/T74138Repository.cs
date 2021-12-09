using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public class T74138Repository: IT74138
    {
        private AmbucareContainer obj = new AmbucareContainer();
        CommonDAL common = new CommonDAL();
        private readonly T74046DAL _t74046DAL = new T74046DAL();
        private readonly T74138DAL _t74138DAL = new T74138DAL();
        //private readonly T74041DAL _t74041DAL = new T74041DAL();
        public T74138Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        //public IEnumerable GetUserIDByAMBRegID(int T_AMBU_REG_ID)
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //    query = obj.T74026.Join(obj.T74057, t26 => t26.T_USER_ID, t57 => t57.T_USER_ID,
        //            (t26, t57) => new { t26, t57 })
        //        .Join(obj.T74004, t26_57 => t26_57.t57.T_EMP_ID, t04 => t04.T_EMP_ID,
        //            (t26_57, t04) => new { t26_57, t04 })
        //        .Join(obj.T74005, t26_57_t04 => t26_57_t04.t04.T_EMP_TYP_ID, t05 => t05.T_EMP_TYP_ID,
        //            (t26_57_t04, t05) => new { t26_57_t04, t05 })
        //        .Join(obj.T74015, t26_57_t04_t05 => t26_57_t04_t05.t26_57_t04.t04.T_EMP_ID, t15 => t15.T_EMP_ID,
        //            (t26_57_t04_t05, t15) => new { t26_57_t04_t05, t15 }).Where(
        //            m => m.t26_57_t04_t05.t05.T_EMP_TYP_ID == 21 && m.t15.T_AMBU_REG_ID == T_AMBU_REG_ID
        //        ).Select(m => new { T_USER_ID = m.t26_57_t04_t05.t26_57_t04.t26_57.t26.T_USER_ID }).ToList();
        //    return query;
        //}
        //public IEnumerable Gender()
        //{
        //    return obj.T74050.Select(m => new { m.T_LANG2_NAME, m.T_SEX_CODE }).ToList();
        //}
        public decimal? GetLat(DateTime? entrTime, string userId)
        {
            //decimal? query = obj.T74026.Where(x => x.T_ENTRY_TIME == entrTime && x.T_USER_ID == userId).Select(x => x.T_LATITUDE).FirstOrDefault();
            decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId).Select(x => x.T_LATITUDE).FirstOrDefault();
            return query;
        }
        public decimal? GetLong(DateTime? entrTime, string userId)
        {
            //decimal? query = obj.T74026.Where(x => x.T_ENTRY_TIME == entrTime && x.T_USER_ID == userId).Select(x => x.T_LONGITUDE).FirstOrDefault();
            decimal? query = obj.T74026.Where(x => x.T_USER_ID == userId).Select(x => x.T_LONGITUDE).FirstOrDefault();
            return query;
        }
        public IEnumerable GetAllUserLatlong(string zonCode)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T02065.Where(x => x.T_ZONE_CODE == zonCode).Select(m => new
                {
                    latitude = m.T_LATITUDE,
                    longitude = m.T_LONGITUDE,
                    T_LANG2_NAME = m.T_LANG2_NAME,
                    T_SITE_CODE = m.T_SITE_CODE
                }).ToList();
                //query = (from t26 in (from t in obj.T74026 group t by new { t.T_USER_ID } into t_n select new { t_n.Key.T_USER_ID, T_ENTRY_TIME = t_n.Max(q => q.T_ENTRY_TIME) })
                //         join t57 in obj.T74057 on t26.T_USER_ID equals t57.T_USER_ID
                //         where t57.T_LOGIN_STATUS == "1"
                //         join t15 in obj.T74015 on t57.T_EMP_ID equals t15.T_EMP_ID
                //         join t14 in obj.T74014 on t15.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID
                //         join t47 in obj.T74047 on t14.T_AMB_TYPE_ID equals t47.T_AMBU_TYPE_ID
                //         join t44 in (from t44 in obj.T74044
                //                      where !(from t in obj.T74041 where t.T_DISCH_STATUS == null group t by new { t.T_AMBU_REG_ID } into t_n select t_n.Key.T_AMBU_REG_ID)
                //                                .Contains(t44.T_AMBU_REG_ID) && t44.T_AMBU_REG_ID != 0
                //                      select t44) on t15.T_AMBU_REG_ID equals t44.T_AMBU_REG_ID

                //         group new { t26, t57, t15, t14, t47, t44 } by new { t26.T_ENTRY_TIME, t26.T_USER_ID, t47.T_LANG2_NAME, t14.T_AMBU_REG_ID } into a
                //         select new
                //         {
                //             //latitude = t26.T_LATITUDE,
                //             //longitude = t26.T_LONGITUDE,
                //             entrTime = a.Key.T_ENTRY_TIME,
                //             userId = a.Key.T_USER_ID,
                //             AMB_CAPASITY = a.Key.T_LANG2_NAME,
                //             T_AMBU_REG_ID = a.Key.T_AMBU_REG_ID

                //         }).AsEnumerable().Select((r, i) => new
                //         {
                //             T_AMBU_REG_ID = r.T_AMBU_REG_ID,
                //             AMB_CAPASITY = r.AMB_CAPASITY + "( " + r.userId + " )",
                //             latitude = GetLat(r.entrTime, r.userId),
                //             longitude = GetLong(r.entrTime, r.userId)
                //         }).ToList();

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
        public IEnumerable GetLoginUserLatlong(int rquestId, string T_USER_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                //query = obj.T02065.Where(x=>x.T_SITE_CODE == "0371").Select(m => new
                //{
                //    latitude = m.T_LATITUDE,
                //    longitude = m.T_LONGITUDE
                //}).ToList();

              




                query = obj.T74026.Join(obj.T74041, t26 => t26.T_USER_ID, t41 => t41.T_USER_ID, (t26, t41) => new { t26, t41 }).Select(m=>new
                {
                    latitude = m.t26.T_LATITUDE,
                    longitude = m.t26.T_LONGITUDE,
                    T_ENTRY_TIME = m.t26.T_ENTRY_TIME,
                    T_REQUEST_ID=m.t41.T_REQUEST_ID,
                    T_USER_ID=m.t26.T_USER_ID,
                    T_DISCH_STATUS=m.t41.T_DISCH_STATUS,
                    T_TEAM_ARVL_HOS=m.t41.T_TEAM_ARVL_HOS,
                    T_TEAM_DEPT_HOS=m.t41.T_TEAM_DEPT_HOS,
                    T_TEAM_ARVL_STN=m.t41.T_TEAM_ARVL_STN,
                    T_RDY_FOR_NXT_PAT = m.t41.T_RDY_FOR_NXT_PAT

                }).Where(a => a.T_REQUEST_ID == rquestId ).OrderByDescending(b => b.T_ENTRY_TIME).Take(1);
              //  }).Where(a => a.T_USER_ID == T_USER_ID ).OrderByDescending(b => b.T_ENTRY_TIME).Take(1);
              //  }).Where(a => a.T_USER_ID == T_USER_ID && a.T_DISCH_STATUS==null).OrderByDescending(b => b.T_ENTRY_TIME).Take(1);

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
        //public DataTable GetAllDataOnMap_T74041(int P_AMBU_REG_ID)
        //{
        //    var data = _t74041DAL.GetAllDataOnMap_T74041(P_AMBU_REG_ID);
        //    return data;
        //}

        //public IEnumerable GetPatInfo()
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //    query = obj.T74046.Join(obj.T74050, t46 => t46.T_SEX_CODE, t50 => t50.T_SEX_CODE,
        //        (t46, t50) => new { t46, t50 }).Select(m => new
        //    {
        //        T_PAT_ID = m.t46.T_PAT_ID,
        //        T_FIRST_LANG2_NAME = m.t46.T_FIRST_LANG2_NAME,
        //        T_FATHER_LANG2_NAME = m.t46.T_FATHER_LANG2_NAME,
        //        T_GFATHER_LANG2_NAME = m.t46.T_GFATHER_LANG2_NAME,
        //        T_FAMILY_LANG2_NAME = m.t46.T_FAMILY_LANG2_NAME,

        //        T_SEX_CODE = m.t46.T_SEX_CODE,
        //        Gender = m.t50.T_LANG2_NAME,
        //        T_MOBILE_NO = m.t46.T_MOBILE_NO,
        //        T_ALT_MOBILE_NO = m.t46.T_ALT_MOBILE_NO,
        //        Age = m.t46.T_BIRTH_DATE
        //    }).ToList();
        //    return query;
        //}
        //public int Insert_t74046(T74046 _t74046)
        //{
        //    obj.T74046.Add(_t74046);
        //    Save();
        //    return obj.T74046.Where(m => m.T_ENTRY_USER == _t74046.T_ENTRY_USER).Max(a => a.T_PAT_ID);

        //}

        //public void Insert_t74041(T74041 _t74041)
        //{
        //    obj.T74041.Add(_t74041);
        //    Save();
        //}
        public void Save()
        {
            try
            {
                obj.SaveChanges();
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
        }
        public void Dispose()
        {
            obj.Dispose();
        }

        public string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;

            return String.Format("{0} Year(s) {1} Month(s) {2} Day(s)", Years, Months, Days);
        }
        public IEnumerable getDestination(string T_USER_ID,int e)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if (e==1)
                {
                    T74041 t41 = obj.T74041.Where(a => a.T_USER_ID == T_USER_ID && string.IsNullOrEmpty(a.T_DISCH_STATUS)).FirstOrDefault();
                if (t41!=null)
                {
                    if (t41.T_START_TIME==null)
                    {
                        t41.T_START_TIME = common.dateTime();
                        obj.SaveChanges();
                    }
                }
                }
                int id = obj.T74026.Where(m => m.T_USER_ID == T_USER_ID).Max(h => h.ID);
                query = obj.T74026.Where(a => a.T_USER_ID == T_USER_ID && a.ID==id).Select(m => new
                {
                    latitude = m.T_LATITUDE,
                    longitude = m.T_LONGITUDE
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

        public string setHandoverHospital(string T_USER_ID, string site)
        {
            string msg = "";
            // IEnumerable query = Enumerable.Empty<object>();
            try
            {
               
                T74041 t41 = obj.T74041.Where(a => a.T_USER_ID == T_USER_ID && a.T_DISCH_STATUS==null).FirstOrDefault();
                if (t41 != null)
                {
                    msg = _t74046DAL.setHandoverHospital(t41.T_REQUEST_ID, site);
                    //t41.T_SITE_DIS_CODE = site;
                    //    obj.SaveChanges();
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


        //public void GPS_Insert(decimal latitude, decimal longitude,string user)
        //{
        //    var time = DateTime.Now;
        //   // var user = HttpContext.Current.Session["T_USER_ID"].ToString();
        //    var check = obj.T74026.Where(x => x.T_USER_ID == user).FirstOrDefault();
        //    check.T_LATITUDE = latitude;
        //    check.T_LONGITUDE = longitude;
        //    check.T_ENTRY_TIME = time;
        //    Save();
        //}
        public string getDocSite(string P_USER_ID)
        {
            string msg = "";
            DataTable dt = _t74138DAL.getDocSite(P_USER_ID);
            if (dt.Rows.Count>0)
            {
                msg = dt.Rows[0]["T_SITE_DIS_CODE"].ToString();
            }

            return msg;
        }
        public string saveEvent(string user, string evt, int req, decimal? lat, decimal? lng)
        {
            var msg = "";

            //var com = common.chkVerified(req);
            //if (com)
            //{
               
            T74041 t41 = obj.T74041.Where(a => a.T_REQUEST_ID == req).FirstOrDefault();
            int t26Id = obj.T74026.Max(a => a.ID) + 1;
            bool chk = false;
            T74026 t26 = new T74026();
            if (t41 != null)
            {
                if (evt == "ah")
                {
                    t41.T_TEAM_ARVL_HOS = common.dateTime();
                    t41.T_EVENT_FLAG = 7;
                    t26.T_EVENT_ID = 12;
                    chk = true;
                }
                else if (evt == "dh")
                {
                    //--------------------Comment out if  any problem occurs---------------------
                    //t41.T_TEAM_DEPT_HOS = common.dateTime();
                    //t41.T_EVENT_FLAG = 9;
                    //t26.T_EVENT_ID = 14;
                    //chk = true;
                    //--------------------Comment if  any problem occurs---------------------
                    var discharge = (from a in obj.T74041 where a.T_REQUEST_ID == req select new { T_DISCHARGE_TIME = a.T_DISCHARGE_TIME, T_EVENT_FLAG = a.T_EVENT_FLAG, T_CAN_DATE = a.T_CAN_DATE }).Single();
                    if (discharge.T_EVENT_FLAG == 2 || discharge.T_CAN_DATE != null || discharge.T_DISCHARGE_TIME != null)
                    {
                        t41.T_TEAM_DEPT_HOS = common.dateTime();
                        t41.T_EVENT_FLAG = 9;
                        t26.T_EVENT_ID = 14;
                        chk = true;
                    }
                    else
                    {
                        chk = false;
                    }
                }
                else if (evt == "as")
                {
                    t41.T_TEAM_ARVL_STN = common.dateTime();
                    t41.T_EVENT_FLAG = 10;
                    t26.T_EVENT_ID = 15;
                    chk = true;

                }
                else if (evt == "rd")
                {
                    var discharge = (from a in obj.T74041 where a.T_REQUEST_ID==req select new{T_DISCHARGE_TIME=    a.T_DISCHARGE_TIME, T_EVENT_FLAG=a.T_EVENT_FLAG, T_CAN_DATE =a.T_CAN_DATE }).Single();
                    if (discharge.T_EVENT_FLAG == 2 || discharge.T_CAN_DATE != null|| discharge.T_DISCHARGE_TIME != null)
                    {
                        t41.T_RDY_FOR_NXT_PAT = common.dateTime();
                        t41.T_EVENT_FLAG = 11;
                        t41.T_DISCH_STATUS = "1";
                        t26.T_EVENT_ID = 32;
                        chk = true;
                    }
                    else
                    {
                        chk = false;
                    }
                   
                    
                }


            }
            if (chk)
            {
                //------------------temp------------------start--------
                //T74026 tT26 = obj.T74026.Where(a => a.T_USER_ID == user).OrderByDescending(b => b.ID).Take(1).FirstOrDefault();
                T74026 tT26 = obj.T74026.Where(a => a.T_USER_ID == user).OrderBy(b => b.ID).Take(1).FirstOrDefault();
                //------------------temp------------------end----------

                t26.ID = t26Id;
                t26.T_REQUEST_ID = req;
                t26.T_USER_ID = user;
                //t26.T_LATITUDE = lat;
                //t26.T_LONGITUDE = lng;

                t26.T_LATITUDE = tT26.T_LATITUDE;
                t26.T_LONGITUDE = tT26.T_LONGITUDE;

                t26.T_ENTRY_TIME = common.dateTime();
                obj.T74026.Add(t26);
                obj.SaveChanges();
                msg = "s";
            }
            else
            {
                msg = "ns";
            }

            //}
            //else
            //{
            //    msg = "1^"+ common.GetSingleMsg("2", "S0053");
            //}


            return msg;
        }

        public bool getArrivedDuration(string user, int req)
        {
            bool status = false;
            TimeSpan span=new TimeSpan(-1,-1,-1);
            var t41 = obj.T74041.Where(a => a.T_REQUEST_ID == req).FirstOrDefault();
            
            if (t41.T_TEAM_ARVL_STN != null && t41.T_RDY_FOR_NXT_PAT==null)
            {
                span = DateTime.Now.Subtract(t41.T_TEAM_ARVL_STN??DateTime.Now);

                if (span.TotalMinutes > 15)
                {
                    status = true;
                }

            }

            return status;
        }

        public string saveRequestHospitalbyTeam(int requestId, string hosId)
        {
            string sms = "";
            if (_t74138DAL.chkVerified(requestId))
            {
             
            var chk = obj.T74041.Where(b => b.T_REQUEST_ID == requestId).FirstOrDefault();
            if (chk != null)
            {
                chk.T_SITE_DIS_CODE = hosId;
                chk.T_DOC_REQ_ACPT_DATE = common.dateTime();
                obj.SaveChanges();
                sms = _t74138DAL.reqHos(requestId, hosId, chk.T_USER_ID);
            }  
            }
            else
            {
                sms = _t74138DAL.GetSingleMsg("2", "S0053");
            }
            

            return sms;

        }
    }
}