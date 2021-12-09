using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Report;
using AmbucareDAL.Repository.Interface.Setup;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Report
{
    public class T74015ReportRepository : IT74015Report
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74015ReportRepository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
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
    }
}