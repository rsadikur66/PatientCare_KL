using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Report;
using AmbucareDAL.Repository.Interface.Report;

namespace AmbucareDAL.Repository.Implementation.Report
{
    public class R74027Repository :IR74027Report
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private readonly R74027DAL _r74027DAL = new R74027DAL();
        public R74027Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public DataTable GetStoresData()
        {
            var result = obj.T74044.Where(s => s.T_LANG2_NAME != null).Select(m => new { m.T_LANG2_NAME, m.T_STORE_ID }).ToList();
           var dtStoresData = fnConvertIEnumerableToDataTable(result);
            return dtStoresData;
        }

        public DataTable GetSuppliersData()
        {
            var data = obj.T74045.Where(s => s.T_LANG2_NAME != null).Select(m => new { m.T_LANG2_NAME, m.T_SUPPLIER_ID }).ToList();
            var dtSuppliersData = fnConvertIEnumerableToDataTable(data);
            return dtSuppliersData;
        }

        public DataTable GetGridAllData( int storeid)
        {
            var data = _r74027DAL.GetGridAllData(storeid);
            return data;
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