using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class Q74138DAL : CommonDAL
    {
        public DataTable getMapload(string zonCode)
        {
           // DataTable query = Query($"SELECT T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE FROM T74026 WHERE T_USER_ID IS NOT NULL ORDER BY T_ENTRY_TIME DESC");
            DataTable query = Query($"select * from (  SELECT T74026.T_USER_ID,  T74026.T_ENTRY_TIME,  T74026.T_LATITUDE,   T74026.T_LONGITUDE FROM T74026  JOIN T74057 ON T74057.T_USER_ID = T74026.T_USER_ID  WHERE T74026.T_USER_ID IS NOT NULL AND T74057.T_ZONE_CODE ='{zonCode}' ORDER BY T_ENTRY_TIME DESC )where   rownum < 15000");

            return query;

        }
    }
}