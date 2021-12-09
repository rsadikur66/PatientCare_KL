using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Menu;
using AmbucareDAL.Repository.Interface.Menu;

namespace AmbucareDAL.Repository.Implementation.Menu
{
    public class MenuRepository:IMenu
    {

        private AmbucareContainer obj = new AmbucareContainer();
        MenuDAL _MenuDal = new MenuDAL();
        public MenuRepository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public MenuRepository()
        {
        }

        public IQueryable<T74066> GetAllData
        {
            get { return obj.T74066.OrderBy(x => x.T_USER_ID); }
        }

        public IEnumerable MenuData(string T_USER_ID, int T_ROLE_CODE, Int32 T_FORM_TYPE_ID, int lang)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //var internetConnectionCheck = CommonClass.CheckForInternetConnection();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 100)) AS RowNumber,t66.T_ORDER,t66.T_USER_ID,t66.T_ROLE_CODE,t66.T_FORM_CODE_ID,t66.T_FORM_CODE,t66.T_LANG1_NAME,t66.T_LANG2_NAME,t66.T_FORM_TYPE_ID,t66.T_INACTIVE_FLAG,t70.T_LANG2_NAME Menu_Name,t90.T_PAGE_TYPE_ID Page_Type,t90.T_LANG2_NAME Page_Type_Name  FROM T74066 t66 JOIN T74070 t70 ON t66.T_FORM_TYPE_ID = t70.T_FORM_TYPE_ID JOIN T74090 t90 ON t66.T_PAGE_TYPE_ID = t90.T_PAGE_TYPE_ID WHERE t66.T_USER_ID = '{T_USER_ID}' and t66.T_FORM_TYPE_ID = '{T_FORM_TYPE_ID}' and t66.T_INACTIVE_FLAG = '1' and t66.T_ROLE_CODE = '{T_ROLE_CODE}' Order by Page_type, t66.T_ORDER asc");

            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            //T_ORDER = row["T_ORDER"].ToString(),
            //            //T_USER_ID = row["T_USER_ID"].ToString(),
            //            //T_ROLE_CODE = row["T_ROLE_CODE"].ToString(),
            //            RowNumber = row["RowNumber"].ToString(),
            //            T_FORM_CODE_ID = row["T_FORM_CODE_ID"].ToString(),
            //           T_FORM_CODE = row["T_FORM_CODE"].ToString(),
            //            T_LANG2_NAME = lang == 1? row["T_LANG1_NAME"].ToString(): row["T_LANG2_NAME"].ToString(),
            //            T_FORM_TYPE_ID = row["T_FORM_TYPE_ID"].ToString(),
            //            //T_INACTIVE_FLAG = row["T_INACTIVE_FLAG"].ToString(),
            //            Menu_Name = row["Menu_Name"].ToString(),
            //            Page_Type = row["Page_Type"].ToString(),
            //            Page_Type_Name = row["Page_Type_Name"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    query = obj.T74066.Join(obj.T74070, t66 => t66.T_FORM_TYPE_ID, t70 => t70.T_FORM_TYPE_ID,
                            (t66, t70) => new { t66, t70 })
                        .Join(obj.T74090, t66_70 => t66_70.t66.T_PAGE_TYPE_ID, t90 => t90.T_PAGE_TYPE_ID, (t66_70, t90) => new { t66_70, t90 })
                        .Select(m => new
                        {
                            T_ORDER = m.t66_70.t66.T_ORDER,
                            T_USER_ID = m.t66_70.t66.T_USER_ID,
                            T_ROLE_CODE = m.t66_70.t66.T_ROLE_CODE,
                            T_FORM_CODE_ID = m.t66_70.t66.T_FORM_CODE_ID,
                            T_FORM_CODE = m.t66_70.t66.T_FORM_CODE,
                            T_LANG2_NAME = lang==1?m.t66_70.t66.T_LANG1_NAME: m.t66_70.t66.T_LANG2_NAME,
                            T_FORM_TYPE_ID = m.t66_70.t66.T_FORM_TYPE_ID,
                            T_INACTIVE_FLAG = m.t66_70.t66.T_INACTIVE_FLAG,
                            Menu_Name =  m.t66_70.t70.T_LANG2_NAME,
                            Page_Type = m.t90.T_PAGE_TYPE_ID,
                            Page_Type_Name = lang == 1 ? m.t90.T_LANG1_NAME: m.t90.T_LANG2_NAME
                        }).Where(a => a.T_USER_ID == T_USER_ID && a.T_FORM_TYPE_ID == T_FORM_TYPE_ID && a.T_INACTIVE_FLAG == "1" && a.T_ROLE_CODE == T_ROLE_CODE).ToList().OrderBy(z => z.Page_Type).ThenBy(z => z.T_ORDER).Select(
                            (a, i) => new
                            {
                                RowNumber = i,
                                T_FORM_TYPE_ID = a.T_FORM_TYPE_ID,
                                T_FORM_CODE = a.T_FORM_CODE,
                                T_LANG2_NAME = a.T_LANG2_NAME,
                                Menu_Name = a.Menu_Name,
                                Page_Type = a.Page_Type,
                                Page_Type_Name = a.Page_Type_Name

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

        public IEnumerable GetAllMenuData(string T_USER_ID, int T_ROLE_CODE, int lang)
        {
            IEnumerable query = Enumerable.Empty<object>();

            try
            {
                query = obj.T74066.Join(obj.T74070, t66 => t66.T_FORM_TYPE_ID, t70 => t70.T_FORM_TYPE_ID,
                        (t66, t70) => new { t66, t70 })
                    .Join(obj.T74090, t66_70 => t66_70.t66.T_PAGE_TYPE_ID, t90 => t90.T_PAGE_TYPE_ID, (t66_70, t90) => new { t66_70, t90 })
                    .Select(m => new
                    {
                        T_ORDER = m.t66_70.t66.T_ORDER,
                        T_USER_ID = m.t66_70.t66.T_USER_ID,
                        T_ROLE_CODE = m.t66_70.t66.T_ROLE_CODE,
                        T_FORM_CODE_ID = m.t66_70.t66.T_FORM_CODE_ID,
                        T_FORM_CODE = m.t66_70.t66.T_FORM_CODE,
                        T_LANG2_NAME = lang == 1 ? m.t66_70.t66.T_LANG1_NAME : m.t66_70.t66.T_LANG2_NAME,
                        T_FORM_TYPE_ID = m.t66_70.t66.T_FORM_TYPE_ID,
                        T_INACTIVE_FLAG = m.t66_70.t66.T_INACTIVE_FLAG,
                        Menu_Name = m.t66_70.t70.T_LANG2_NAME,
                        Page_Type = m.t90.T_PAGE_TYPE_ID,
                        Page_Type_Name = lang == 1 ? m.t90.T_LANG1_NAME : m.t90.T_LANG2_NAME
                    }).Where(a => a.T_USER_ID == T_USER_ID && a.T_INACTIVE_FLAG == "1" && a.T_ROLE_CODE == T_ROLE_CODE).ToList().OrderBy(z => z.Page_Type).ThenBy(z => z.T_ORDER).Select(
                        (a, i) => new
                        {
                            RowNumber = i,
                            T_FORM_TYPE_ID = a.T_FORM_TYPE_ID,
                            T_FORM_CODE = a.T_FORM_CODE,
                            T_LANG2_NAME = a.T_LANG2_NAME,
                            Menu_Name = a.Menu_Name,
                            Page_Type = a.Page_Type,
                            Page_Type_Name = a.Page_Type_Name

                        });
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                       Trace.TraceInformation("Property:{0} Error: {1}",validationError.PropertyName,validationError.ErrorMessage);
                    }
                }
            }

            return query;
        }

        public IEnumerable FormAuthorization(string T_FORM_CODE, string T_USER_ID, int T_ROLE_CODE)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //var internetConnectionCheck = CommonClass.CheckForInternetConnection();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from t74066 where T_FORM_CODE ='{T_FORM_CODE}' and T_USER_ID = '{T_USER_ID}' and T_ROLE_CODE='{T_ROLE_CODE}'");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_ENTRY_USER = row["T_ENTRY_USER"].ToString(),
            //            T_ENTRY_DATE = row["T_ENTRY_DATE"].ToString(),
            //            T_UPD_USER = row["T_UPD_USER"].ToString(),
            //            T_UPD_DATE = row["T_UPD_DATE"].ToString(),
            //            T_FORM_CODE = row["T_FORM_CODE"].ToString(),
            //            T_LANG1_NAME = row["T_LANG1_NAME"].ToString(),
            //            T_LANG2_NAME = row["T_LANG2_NAME"].ToString(),
            //            //T_LANG2_NAME = lang == 1 ? row["T_LANG1_NAME"].ToString() : row["T_LANG2_NAME"].ToString(),
            //            T_FORM_TYPE_ID = row["T_FORM_TYPE_ID"].ToString(),
            //            T_FORM_CODE_ID = row["T_FORM_CODE_ID"].ToString(),
            //            T_USER_ID = row["T_USER_ID"].ToString(),
            //            T_INACTIVE_FLAG = row["T_INACTIVE_FLAG"].ToString(),
            //            T_PAGE_TYPE_ID = row["T_PAGE_TYPE_ID"].ToString(),
            //            T_ORDER = row["T_ORDER"].ToString(),
            //            T_TYPE = row["T_TYPE"].ToString(),
            //            T_ROLE_CODE = row["T_ROLE_CODE"].ToString(),
            //            //Menu_Name = row["Menu_Name"].ToString(),
            //            //Page_Type = row["Page_Type"].ToString(),
            //            //Page_Type_Name = row["Page_Type_Name"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    query = obj.T74066.Where(x => x.T_FORM_CODE == T_FORM_CODE && x.T_USER_ID == T_USER_ID  && x.T_ROLE_CODE == T_ROLE_CODE).ToList();
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

        public IQueryable<T74000> LabelData(string T_FORM_CODE, string T_LABEL_NAME)
        {
            IEnumerable query = Enumerable.Empty<object>();
            //var internetConnectionCheck = CommonClass.CheckForInternetConnection();
            //if (internetConnectionCheck == false)
            //{
            //    DataTable dt = new DataTable();
            //    dt = cSql.Query($"select * from t74000 where T_FORM_CODE ='{T_FORM_CODE}' and T_LABEL_NAME ='{T_LABEL_NAME}'");
            //    query = dt.AsEnumerable().Select(row =>
            //        new
            //        {
            //            T_LABEL_ID = row["T_LABEL_ID"].ToString(),
            //            T_FORM_CODE = row["T_FORM_CODE"].ToString(),
            //            T_LABEL_NAME = row["T_LABEL_NAME"].ToString(),
            //            T_LANG1_TEXT = row["T_LANG1_TEXT"].ToString(),
            //            T_LANG2_TEXT = row["T_LANG2_TEXT"].ToString(),
            //            T_ENTRY_USER = row["T_LANG2_TEXT"].ToString(),
            //            T_ENTRY_DATE = row["T_LANG2_TEXT"].ToString(),
            //            T_UPD_USER = row["T_LANG2_TEXT"].ToString(),
            //            T_UPD_DATE = row["T_LANG2_TEXT"].ToString()
            //        }).ToList();
            //}
            //else
            //{
                try
                {
                    query = obj.T74000.Where(x => x.T_FORM_CODE == T_FORM_CODE && x.T_LABEL_NAME == T_LABEL_NAME);
                 
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
            return query as IQueryable<T74000>;
            
        }
        public void GPS_Insert(decimal latitude, decimal longitude, string user)
        {
            var time = DateTime.Now;
            // var user = HttpContext.Current.Session["T_USER_ID"].ToString();
            var check = obj.T74026.Where(x => x.T_USER_ID == user).FirstOrDefault();
            check.T_LATITUDE = latitude;
            check.T_LONGITUDE = longitude;
            check.T_ENTRY_TIME = time;
            obj.SaveChanges();
        }

        public void Save_t74041(string userid,string lat,string lon,string address)
        {
            _MenuDal.Save_t74041(userid,lat,lon,address);
        }

        public DataTable GetLatLong(string userid)
        {
            var data = _MenuDal.GetLatLong(userid);
            return data;
        }

        public string SaveLatLong(T74026 t74026)
        {
            string status = "";
            try
            {
                //------------------temp------------------start--------
                //T74026 t26=obj.T74026.Where(a=>a.T_USER_ID== t74026.T_USER_ID).OrderByDescending(b=>b.ID).Take(1).FirstOrDefault(); 
                T74026 t26 = obj.T74026.Where(a => a.T_USER_ID == t74026.T_USER_ID).OrderBy(b => b.ID).Take(1).FirstOrDefault();
                //------------------temp------------------end----------





                t74026.ID = obj.T74026.Max(a => a.ID) + 1;
                t74026.T_LATITUDE = t26.T_LATITUDE;
                t74026.T_LONGITUDE = t26.T_LONGITUDE;
                t74026.T_ENTRY_TIME = _MenuDal.dateTime();
                obj.T74026.Add(t74026);
                Save();
                status = "OK";
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

           
            return status;
        }

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

        public DataTable GetAllUserMsg(string LANGUAGE, string T_FORM_CODE)
        {
            var Data = _MenuDal.GetAllUserMsg(LANGUAGE, T_FORM_CODE);
            return Data;
        }
        public DataTable getFormName(string lang, string code)
        {
            var Data = _MenuDal.getFormName(lang,code);
            return Data;
        }
        public string updateForm(string user, string form)
        {
            var Data = _MenuDal.updateForm(user,form);
            return Data;
        }
    }
}