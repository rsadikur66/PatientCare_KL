using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Report
{
    public class T74111DAL:CommonDAL
    {
        public DataTable GetData(int id)
        {
            return Query("SELECT T_LANG2_NAME,T_PRICE FROM T74073 WHERE T_COST_TYPE_ID='" + id + "'");
        }
    }
}