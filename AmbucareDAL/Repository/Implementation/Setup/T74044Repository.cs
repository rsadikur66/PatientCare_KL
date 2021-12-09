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
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74044Repository : IT74044
    {
        CommonDAL cDal = new CommonDAL();
        private AmbucareContainer obj = new AmbucareContainer();

        public T74044Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }
        public IEnumerable GetAmbReg()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {

                query = (from t14 in obj.T74014
                    join t44O in obj.T74044 on t14.T_AMBU_REG_ID equals t44O.T_AMBU_REG_ID into t44L
                    from t44 in t44L.DefaultIfEmpty()
                    where t44.T_AMBU_REG_ID == null
                    select new { t14.T_AMBU_REG_ID, t14.T_AMBU_REG_NUM }).AsEnumerable().ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property :{0} Error :{1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }
        public IEnumerable GetMohTeam(string lang, string zone)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                //DataTable dt = new DataTable();
                //dt = cDal.Query($"SELECT DISTINCT T00.T_TEAM_CODE, T00.T_LANG{lang}_NAME TEAM_DESC FROM T74200 T00 LEFT JOIN T74044 T44 ON T00.T_TEAM_CODE=T44.T_TEAM_CODE WHERE T00.T_CITY_CODE={zone} AND T44.T_TEAM_CODE IS NULL");
                //query = dt.AsEnumerable().AsQueryable().Select(row =>
                //    new
                //    {
                //        T_TEAM_CODE = row["T_TEAM_CODE"].ToString(),
                //        TEAM_DESC = row["TEAM_DESC"].ToString()
                //    }).ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property :{0} Error :{1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }
        public IEnumerable GetMohStation(string lang, string zone)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                //DataTable dt = new DataTable();
                //dt = cDal.Query($"SELECT DISTINCT T20.T_STATION_CODE, T20.T_LANG{lang}_NAME STATION_DESC FROM T74120 T20 WHERE T20.T_CITY_CODE={zone}");
                //query = dt.AsEnumerable().AsQueryable().Select(row =>
                //    new
                //    {
                //        T_STATION_CODE = row["T_STATION_CODE"].ToString(),
                //        STATION_DESC = row["STATION_DESC"].ToString()
                //    }).ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property :{0} Error :{1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }
        public string GetTeamDesc(string P_TEAM_CODE, string lang)
        {
            string desc = "";
            //DataTable dt = new DataTable();
            //if (!string.IsNullOrEmpty(P_TEAM_CODE))
            //{
            //    dt = cDal.Query($"SELECT T_LANG{lang}_NAME NAME FROM T74200 WHERE T_TEAM_CODE='{P_TEAM_CODE}'");
            //}
            //if (dt.Rows.Count > 0)
            //{
            //    desc = dt.Rows[0]["NAME"].ToString();
            //}
            return desc;
        }
        public string GetStationDesc(string P_STATION_CODE, string lang)
        {
            string desc = "";
            //DataTable dt = new DataTable();
            //if (!string.IsNullOrEmpty(P_STATION_CODE))
            //{
            //    dt = cDal.Query($"SELECT T_LANG{lang}_NAME NAME FROM T74120 WHERE T_STATION_CODE='{P_STATION_CODE}'");
            //}
            //if (dt.Rows.Count > 0)
            //{
            //    desc = dt.Rows[0]["NAME"].ToString();
            //}
            return desc;
        }
        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize, string lang, string zone, string user)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                DataTable dt = new DataTable();
                dt = cDal.Query(
                    $"SELECT T44.T_STORE_ID,T44.T_AMBU_REG_ID,T44.T_LANG1_NAME,T44.T_LANG2_NAME,T44.T_AREA,T14.T_AMBU_REG_NUM FROM T74044 T44 LEFT JOIN T74014 T14 ON T14.T_AMBU_REG_ID=T44.T_AMBU_REG_ID WHERE T14.T_ENTRY_USER='{user}'");
                query = dt.AsEnumerable().AsQueryable().Select((a, i) => new
                {
                    RowNumber = i,
                    T_STORE_ID = a["T_STORE_ID"].ToString(),
                    T_AMBU_REG_ID = a["T_AMBU_REG_ID"].ToString(),
                    T_AMBU_REG_NUM = a["T_AMBU_REG_NUM"].ToString(),
                    T_LANG1_NAME = a["T_LANG1_NAME"].ToString(),
                    T_LANG2_NAME = a["T_LANG2_NAME"].ToString(),
                    T_AREA = a["T_AREA"].ToString(),
                    //T_TEAM_CODE = a["T_TEAM_CODE"].ToString(),
                    //TEAM_DESC = a["TEAM_DESC"].ToString(),
                    //T_STATION_CODE = a["T_STATION_CODE"].ToString(),
                    //STATION_DESC = a["STATION_DESC"].ToString()
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
                //query = (from t44 in obj.T74044
                //    select new
                //    {
                //      t44.T_STORE_ID,
                //      t44.T_AMBU_REG_ID,
                //      t44.T_LANG1_NAME,
                //      t44.T_LANG2_NAME,
                //      t44.T_AREA,
                //      t44.T_TEAM_CODE,
                //      t44.T_STATION_CODE
                //    }).AsEnumerable().Select((r, i) => new
                //{
                //    RowNumber = i,
                //    r.T_STORE_ID,
                //    r.T_AMBU_REG_ID,
                //    r.T_LANG1_NAME,
                //    r.T_LANG2_NAME,
                //    r.T_AREA,
                //    r.T_TEAM_CODE,
                //    TEAM_DESC = GetTeamDesc(r.T_TEAM_CODE, lang),
                //    r.T_STATION_CODE,
                //    STATION_DESC = GetStationDesc(r.T_STATION_CODE, lang)
                //    }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);

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
        public int GetGridData_Search_Count(string searchValue, string lang, string zone, string user)
        {
            int query = 0;
            try
            {
                DataTable dt = new DataTable();
                dt = cDal.Query(
                    $"SELECT T44.T_STORE_ID,T44.T_AMBU_REG_ID,T44.T_LANG1_NAME,T44.T_LANG2_NAME,T44.T_AREA,T14.T_AMBU_REG_NUM FROM T74044 T44 LEFT JOIN T74014 T14 ON T14.T_AMBU_REG_ID=T44.T_AMBU_REG_ID WHERE T14.T_ENTRY_USER='{user}'");
                if (searchValue != "")
                {

                    query = dt.AsEnumerable().AsQueryable().Select((a, i) => new
                    {
                        RowNumber = i,
                        T_STORE_ID = a["T_STORE_ID"].ToString(),
                        T_AMBU_REG_ID = a["T_AMBU_REG_ID"].ToString(),
                        T_AMBU_REG_NUM = a["T_AMBU_REG_NUM"].ToString(),
                        T_LANG1_NAME = a["T_LANG1_NAME"].ToString(),
                        T_LANG2_NAME = a["T_LANG2_NAME"].ToString(),
                        T_AREA = a["T_AREA"].ToString(),
                        //T_TEAM_CODE = a["T_TEAM_CODE"].ToString(),
                        //TEAM_DESC = a["TEAM_DESC"].ToString(),
                        //T_STATION_CODE = a["T_STATION_CODE"].ToString(),
                        //STATION_DESC = a["STATION_DESC"].ToString()
                    }).Where(t44 => t44.T_STORE_ID.ToString().Contains(searchValue) ||
                                    t44.T_AMBU_REG_NUM.ToUpper().Contains(searchValue.ToUpper()) ||
                                    t44.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                    t44.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                    t44.T_AREA.ToString().Contains(searchValue.ToUpper())
                                    //t44.T_TEAM_CODE.Contains(searchValue) ||
                                    //t44.TEAM_DESC.Contains(searchValue) ||
                                    //t44.T_STATION_CODE.Contains(searchValue) ||
                                    //t44.STATION_DESC.Contains(searchValue)
                                    ).ToList().Count;

                    //query = (from t44 in obj.T74044
                    //where
                    //t44.T_STORE_ID.ToString().Contains(searchValue) ||
                    //t44.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    //t44.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    //t44.T_AREA.ToString().Contains(searchValue.ToUpper()) ||
                    //t44.T_TEAM_CODE.Contains(searchValue)||
                    //GetTeamDesc(t44.T_TEAM_CODE, lang).Contains(searchValue)||
                    //t44.T_STATION_CODE.Contains(searchValue) ||
                    //GetStationDesc(t44.T_TEAM_CODE, lang).Contains(searchValue)
                    //     select t44).Count();
                }
                else
                {
                    //query = (from t44 in obj.T74044 select t44).Count();
                    query = dt.AsEnumerable().AsQueryable().ToList().Count;
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
        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, string lang, string zone, string user)
        {

            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                DataTable dt = new DataTable();
                dt = cDal.Query(
                    $"SELECT T44.T_STORE_ID,T44.T_AMBU_REG_ID,T44.T_LANG1_NAME,T44.T_LANG2_NAME,T44.T_AREA,T14.T_AMBU_REG_NUM FROM T74044 T44 LEFT JOIN T74014 T14 ON T14.T_AMBU_REG_ID=T44.T_AMBU_REG_ID WHERE T00.T_CITY_CODE={zone} OR T44.T_ENTRY_USER='{user}'");


                query = dt.AsEnumerable().AsQueryable().Select((a, i) => new
                {
                    RowNumber = i,
                    T_STORE_ID = a["T_STORE_ID"].ToString(),
                    T_AMBU_REG_ID = a["T_AMBU_REG_ID"].ToString(),
                    T_AMBU_REG_NUM = a["T_AMBU_REG_NUM"].ToString(),
                    T_LANG1_NAME = a["T_LANG1_NAME"].ToString(),
                    T_LANG2_NAME = a["T_LANG2_NAME"].ToString(),
                    T_AREA = a["T_AREA"].ToString(),
                    //T_TEAM_CODE = a["T_TEAM_CODE"].ToString(),
                    //TEAM_DESC = a["TEAM_DESC"].ToString(),
                    //T_STATION_CODE = a["T_STATION_CODE"].ToString(),
                    //STATION_DESC = a["STATION_DESC"].ToString()
                }).ToList().Where(a =>
                    a.T_STORE_ID.ToString().Contains(searchValue) ||
                    a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    a.T_AMBU_REG_NUM.ToUpper().Contains(searchValue.ToUpper()) ||
                    a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                    a.T_AREA.ToString().Contains(searchValue.ToUpper())
                    //a.T_TEAM_CODE.Contains(searchValue) ||
                    //a.TEAM_DESC.Contains(searchValue) ||
                    //a.T_STATION_CODE.Contains(searchValue) ||
                    //a.STATION_DESC.Contains(searchValue)
                    ).Select(b => b).AsEnumerable().Select((r, i) => new
                {
                    RowNumber = i,
                    r.T_STORE_ID,
                    r.T_AMBU_REG_ID,
                    r.T_LANG1_NAME,
                    r.T_LANG2_NAME,
                    r.T_AREA,
                    //r.T_TEAM_CODE,
                    //r.TEAM_DESC,
                    //r.T_STATION_CODE,
                    //r.STATION_DESC
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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
        public string AddStore(T74044 _t74044, string lang)
        {
            string ret = "";
            try
            {
                if (_t74044.T_STORE_ID == 0)
                {
                    _t74044.T_ENTRY_DATE = cDal.dateTime();
                    obj.T74044.Add(_t74044);
                    obj.SaveChanges();
                    ret = cDal.GetSingleMsg(lang, "S0002");
                }

                else
                {
                    var UpD = obj.T74044.Where(P => P.T_STORE_ID == _t74044.T_STORE_ID).FirstOrDefault();
                    UpD.T_LANG1_NAME = _t74044.T_LANG1_NAME;
                    UpD.T_LANG2_NAME = _t74044.T_LANG2_NAME;
                    UpD.T_AMBU_REG_ID = _t74044.T_AMBU_REG_ID;
                    UpD.T_AREA = _t74044.T_AREA;
                    //UpD.T_TEAM_CODE = _t74044.T_TEAM_CODE;
                    //UpD.T_STATION_CODE = _t74044.T_STATION_CODE;
                    UpD.T_UPD_USER = _t74044.T_ENTRY_USER;
                    UpD.T_UPD_DATE = cDal.dateTime();
                    obj.SaveChanges();
                    ret = cDal.GetSingleMsg(lang, "S0003");
                }

            }
            catch (Exception e)
            {
                ret = cDal.GetSingleMsg(lang, "S0042");
            }
            return ret;


        }
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}