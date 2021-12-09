using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface.Transaction;

public class T74015Repository : IT74015
{
    private AmbucareContainer obj = new AmbucareContainer();
    CommonDAL cDal = new CommonDAL();
    public T74015Repository(AmbucareContainer _Obj)
    {
        _Obj.Configuration.ProxyCreationEnabled = false;
        obj = _Obj;
    }
    public IEnumerable GetEmployeeTypeIdAndEmployeeIdByAmbulanceId(int T_AMBU_REG_ID)
    {
        IEnumerable query = Enumerable.Empty<object>();
        try
        {
            query = (from i in obj.T74015
                join j in obj.T74004
                on i.T_EMP_ID equals j.T_EMP_ID
                join k in obj.T74005 on j.T_EMP_TYP_ID equals k.T_EMP_TYP_ID
                where i.T_AMBU_REG_ID == T_AMBU_REG_ID 
                //&& i.T_ACTIVE_STATUS != null
                select new
                {
                    T_AMBU_REG_ID = i.T_AMBU_REG_ID,
                    T_EMP_ASSIGN_ID = i.T_EMP_ASSIGN_ID,
                    T_EMP_TYP_ID = k.T_EMP_TYP_ID,
                    T_EMP_TYP_NAME = k.T_LANG2_NAME,
                    T_EMP_ID = j.T_EMP_ID,
                    EMP_NAME1 = j.T_FIRST_LANG2_NAME,
                  //  EMP_NAME2 = j.T_FATHER_LANG2_NAME,
                    EMP_NAME3 = j.T_GFATHER_LANG2_NAME,
                   // EMP_NAME4 = j.T_FAMILY_LANG2_NAME,
                    //T_ACTIVE_STATUS = i.T_ACTIVE_STATUS
                }).AsEnumerable().Select((r, i) => new
            {
                T_AMBU_REG_ID = r.T_AMBU_REG_ID,
                T_EMP_ASSIGN_ID = r.T_EMP_ASSIGN_ID,
                T_EMP_TYP_ID = r.T_EMP_TYP_ID,
                T_EMP_TYP_NAME = r.T_EMP_TYP_NAME,
                T_EMP_ID = r.T_EMP_ID,
                FullName = r.EMP_NAME1 + " " + r.EMP_NAME3,
                //T_ACTIVE_STATUS = r.T_ACTIVE_STATUS
            }).ToList();
        }
        catch (DbEntityValidationException dbEx)
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                        validationError.ErrorMessage);
                }
            }
        }
        return query;
    }

    //public IEnumerable GetEmployeeData(int T_EMP_TYP_ID)
    //{
    //    IEnumerable query = Enumerable.Empty<object>();
    //    try
    //    {
    //        query = (from t04 in obj.T74004
    //                 join t05 in obj.T74005 on t04.T_EMP_TYP_ID equals t05.T_EMP_TYP_ID
    //                 where !(from t in obj.T74015
    //                         where !(from j in obj.T74004
    //                                 where j.T_EMP_TYP_ID == T_EMP_TYP_ID
    //                                 select j.T_EMP_ID).Contains(t04.T_EMP_ID) 
    //                         select t.T_EMP_ID).Contains(t04.T_EMP_ID) && t04.T_EMP_TYP_ID == T_EMP_TYP_ID
    //                 select new
    //                 {
    //                     T_EMP_ID = t04.T_EMP_ID,
    //                     EMP_NAME1 = t04.T_FIRST_LANG2_NAME,
    //                     EMP_NAME2 = t04.T_FATHER_LANG2_NAME,
    //                     EMP_NAME3 = t04.T_GFATHER_LANG2_NAME,
    //                     EMP_NAME4 = t04.T_FAMILY_LANG2_NAME,
    //                 }).AsEnumerable().Select((r, i) => new
    //                 {
    //                     T_EMP_ID = r.T_EMP_ID,
    //                     FullName = r.EMP_NAME1 + " " + r.EMP_NAME2 + " " + r.EMP_NAME3 + " " + r.EMP_NAME4
    //                 }).ToList();
    //    }
    //    catch (DbEntityValidationException dbEx)
    //    {
    //        foreach (var validationErrors in dbEx.EntityValidationErrors)
    //        {
    //            foreach (var validationError in validationErrors.ValidationErrors)
    //            {
    //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
    //                    validationError.ErrorMessage);
    //            }
    //        }
    //    }
    //    return query;
    //}


    public IEnumerable GetEmployeeData(string T_EMP_TYP_ID, string entryuser)
    {
        IEnumerable query = Enumerable.Empty<object>();
        try
        {
            DataTable dt = new DataTable();
            dt = cDal.Query($"SELECT t04.T_EMP_ID,t04.T_FIRST_LANG2_NAME ||' '||t04.T_GFATHER_LANG2_NAME FullName,t15.T_EMP_ID EMP15, CASE t15.T_EMP_ID when t15.T_EMP_ID then 'assigned' END STATUS FROM T74004 t04 join T74005 t05 on t04.T_EMP_TYP_ID = t05.T_EMP_TYP_ID left join t74015 t15 on t04.T_EMP_ID = t15.T_EMP_ID where t04.T_EMP_TYP_ID = '{T_EMP_TYP_ID}' AND t04.T_ENTRY_USER = '{entryuser}'");
            query = dt.AsEnumerable().AsQueryable().Select(row =>
                new
                {
                    T_EMP_ID = row["T_EMP_ID"].ToString(),
                    FullName = row["FullName"].ToString(),
                    STATUS = row["STATUS"].ToString()
                }).ToList();
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
    public IEnumerable GetExistingData(int T_AMBU_REG_ID, int T_EMP_ID)
    {
        IEnumerable query = Enumerable.Empty<object>();
        try
        {
            query = (from i in obj.T74015
                join j in obj.T74004 on i.T_EMP_ID equals j.T_EMP_ID
                where i.T_AMBU_REG_ID == T_AMBU_REG_ID && i.T_EMP_ID == T_EMP_ID
                select new
                {
                    T_EMP_ID = j.T_EMP_ID,
                    EMP_NAME1 = j.T_FIRST_LANG2_NAME,
                    EMP_NAME2 = j.T_FATHER_LANG2_NAME,
                    EMP_NAME3 = j.T_GFATHER_LANG2_NAME,
                    EMP_NAME4 = j.T_FAMILY_LANG2_NAME,
                }).AsEnumerable().Select((r, i) => new
            {
                T_EMP_ID = r.T_EMP_ID,
                FullName = r.EMP_NAME1 + " " + r.EMP_NAME2 + " " + r.EMP_NAME3 + " " + r.EMP_NAME4
            }).ToList();
        }
        catch (DbEntityValidationException dbEx)
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                        validationError.ErrorMessage);
                }
            }
        }
        return query;
    }

    public IEnumerable getGridEmployeeTypeData(int T_AMBU_REG_ID)
    {

        IEnumerable query = Enumerable.Empty<object>();
        try
        {
            if (T_AMBU_REG_ID != 0)
            {



                query = (from t05 in obj.T74005
                    where t05.T_EMP_TYP_ID == 3 ||
                          t05.T_EMP_TYP_ID == 6 || t05.T_EMP_TYP_ID == 21 || t05.T_EMP_TYP_ID == 102
                    select new
                    {
                        t05.T_EMP_TYP_ID,
                        t05.T_LANG2_NAME
                    }).ToList();
            }
            else
            {

                query = (from t05 in obj.T74005
                    where t05.T_EMP_TYP_ID == 8 || t05.T_EMP_TYP_ID == 23
                    select new
                    {
                        t05.T_EMP_TYP_ID,
                        t05.T_LANG2_NAME
                    }).ToList();
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
    public string Add_T74015(List<T74015> _T74015List, string entrityUser)
    {
        string msg = "";
        using (var dbContextTransaction = obj.Database.BeginTransaction())
        {
            try
            {
                int? T_ID;

                foreach (var _T74015 in _T74015List)
                {
                    var check = obj.T74015.Where(P => P.T_EMP_ASSIGN_ID == _T74015.T_EMP_ASSIGN_ID).FirstOrDefault();
                    if (check == null)
                    {
                        var getExistingData = GetExistingData(Convert.ToInt32(_T74015.T_AMBU_REG_ID), Convert.ToInt32(_T74015.T_EMP_ID));
                        if (Count(getExistingData) > 0)
                        {
                            var check1 = obj.T74015.Where(P => P.T_AMBU_REG_ID == _T74015.T_AMBU_REG_ID && P.T_EMP_ID == _T74015.T_EMP_ID).FirstOrDefault();
                            check1.T_EMP_ID = _T74015.T_EMP_ID;
                            check1.T_AMBU_REG_ID = _T74015.T_AMBU_REG_ID;
                            //check1.T_ACTIVE_STATUS = "1";
                            DateTime now = DateTime.Now;
                            check1.T_ASSIGN_DATE = now;
                            obj.SaveChanges();
                            //return msg = "Data Already Exists";
                            //return msg;
                        }
                        else
                        {
                            int _T74015_maxValue = obj.T74015.Max(a => a.T_EMP_ASSIGN_ID);

                            _T74015.T_EMP_ASSIGN_ID = _T74015_maxValue != 0 ? _T74015_maxValue + 1 : 1;
                            _T74015.T_ENTRY_USER = entrityUser;
                            //_T74015.T_ACTIVE_STATUS = "1";
                            obj.T74015.Add(_T74015);
                            obj.SaveChanges();
                            T_ID = _T74015.T_AMBU_REG_ID;
                        }
                    }
                    else
                    {
                        check.T_EMP_ID = _T74015.T_EMP_ID;
                        check.T_AMBU_REG_ID = _T74015.T_AMBU_REG_ID;
                        //check.T_ACTIVE_STATUS = _T74015.T_ACTIVE_STATUS;
                        // obj.Entry(_T74015).State = System.Data.Entity.EntityState.Modified;    
                        obj.SaveChanges();
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
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
        return msg = "Save Sucessfully";
    }

    public string Add_t74073(T74073 _t74073)
    {
        var msg = "";
        using (var dbContextTransaction = obj.Database.BeginTransaction())
        {
            try
            {
                var existingValue = obj.T74073.Where(x => x.T_ID == _t74073.T_ID).FirstOrDefault();
                if (existingValue == null)
                {
                    obj.T74073.Add(_t74073);
                    obj.SaveChanges();
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
        return msg = "Save Successfully";
    }
    public void Del_T74015(List<T74015> _T74015Del)
    {
        string msg = "";
        foreach (var _T74015 in _T74015Del)
        {
            var check = obj.T74015.Where(P => P.T_EMP_ASSIGN_ID == _T74015.T_EMP_ASSIGN_ID).FirstOrDefault();
            if (check != null)
            {
                obj.T74015.Remove(check);
                obj.SaveChanges();
            }
        }

    }

    public IEnumerable GetMohTeam(string lang, string zone)
    {
        IEnumerable query = Enumerable.Empty<object>();
        try
        {
            DataTable dt = new DataTable();
            dt = cDal.Query($"select T15.T_AMBU_REG_ID,t57.t_user_id TEAM_DESC from T74015 t15 left join t74057 t57 on t15.T_EMP_ID = t57.T_EMP_ID where t57.T_ZONE_CODE = {zone} and t57.T_ROLE_CODE = 24");
            query = dt.AsEnumerable().AsQueryable().Select(row =>
                new
                {
                    T_AMBU_REG_ID = row["T_AMBU_REG_ID"].ToString(),
                    TEAM_DESC = row["TEAM_DESC"].ToString()
                }).ToList();
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

    public int Count(IEnumerable source)
    {
        int res = 0;

        foreach (var item in source)
            res++;

        return res;
    }


    public DataTable GetEmployeeTypeIdAndEmployeeIdByAmbulanceIdReport()
    {
        IEnumerable query = Enumerable.Empty<object>();
        try
        {
            query = (from i in obj.T74015
                join j in obj.T74004
                on i.T_EMP_ID equals j.T_EMP_ID
                join k in obj.T74005 on j.T_EMP_TYP_ID equals k.T_EMP_TYP_ID
                select new
                {
                    T_AMBU_REG_ID = i.T_AMBU_REG_ID,
                    T_EMP_ASSIGN_ID = i.T_EMP_ASSIGN_ID,
                    T_EMP_TYP_ID = k.T_EMP_TYP_ID,
                    T_EMP_TYP_NAME = k.T_LANG2_NAME,
                    T_EMP_ID = j.T_EMP_ID,
                    EMP_NAME1 = j.T_FIRST_LANG2_NAME,
                    EMP_NAME2 = j.T_FATHER_LANG2_NAME,
                    EMP_NAME3 = j.T_GFATHER_LANG2_NAME,
                    EMP_NAME4 = j.T_FAMILY_LANG2_NAME,
                }).AsEnumerable().Select((r, i) => new
            {
                T_AMBU_REG_ID = r.T_AMBU_REG_ID,
                T_EMP_ASSIGN_ID = r.T_EMP_ASSIGN_ID,
                T_EMP_TYP_ID = r.T_EMP_TYP_ID,
                T_EMP_TYP_NAME = r.T_EMP_TYP_NAME,
                T_EMP_ID = r.T_EMP_ID,
                FullName = r.EMP_NAME1 + " " + r.EMP_NAME2 + " " + r.EMP_NAME3 + " " + r.EMP_NAME4
            }).ToList();
        }
        catch (DbEntityValidationException dbEx)
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                        validationError.ErrorMessage);
                }
            }
        }
        var a = fnConvertIEnumerableToDataTable(query);
        return a;
    }

    public static DataTable fnConvertIEnumerableToDataTable(IEnumerable dataSource)
    {
        System.Reflection.PropertyInfo[] propInfo = null;
        DataTable dt = new DataTable();
        //DateTime objDT = DateTime.Now;

        foreach (object o in dataSource)
        {
            propInfo = o.GetType().GetProperties();

            for (int i = 0; i < propInfo.Length; i++)
            {
                dt.Columns.Add(propInfo[i].Name);

                if (propInfo[i].Name.Contains("Date"))
                    dt.Columns[i].DataType = typeof(DateTime);
            }
            break;
        }

        foreach (object tempObject in dataSource)
        {
            DataRow dr = dt.NewRow();

            for (int i = 0; i < propInfo.Length; i++)
            {
                object t = tempObject.GetType().InvokeMember(propInfo[i].Name, BindingFlags.GetProperty, null, tempObject, new object[] { });
                if (t != null)
                {
                    if (t.GetType().Equals(typeof(DateTime)))
                        dr[i] = Convert.ToDateTime(t);
                    else
                        dr[i] = t.ToString();
                }
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

  public  IEnumerable GetAmbulanceDropdownList( string user)
    {
        IEnumerable query = Enumerable.Empty<object>();
        query = (from t14 in obj.T74014
            where t14.T_ENTRY_USER == user
            select new
            {
                T_AMBU_REG_ID = t14.T_AMBU_REG_ID,
                TEAM_DESC = t14.T_AMBU_REG_NUM
            }).ToList();
            return query;
    }
}