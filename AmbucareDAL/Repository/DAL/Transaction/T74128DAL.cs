using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class T74128DAL :CommonDAL
    {
        public DataTable GetTrade()
        {
            return Query("select ITEM_CODE, TRADE_CODE,GEN_CODE, PRODUCT_DESC, STRENGTH, ROUTE_CODE, ROUTE_DESC, FORM_CODE, FORM_DESC  from RUFAIDA.V30001 inner join T74073  on V30001.ITEM_CODE = T74073.T_ID Where T_COST_TYPE_ID = 23");
        }
    }
}