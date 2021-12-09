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
    public class T74111ReportRepository : IT74111Report
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private readonly T74111DAL _t74111DAL = new T74111DAL();
        public T74111ReportRepository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public DataTable GetData(int id)
        {
            var data = _t74111DAL.GetData(id);
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