﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class T74134DAL:CommonDAL
    {
        public DataTable GetItem(int ambu,int requestId)
        {
            return Query($"SELECT DISTINCT T74044.T_STORE_ID, T74041.T_AMBU_REG_ID, T74073.T_LANG2_NAME, fn_Stock_Qty_Instant(t74027.T_ITEM_ID,t74044.T_STORE_ID) STOCK , t74027.T_ITEM_ID , (SELECT DISTINCT T74037.T_ITEM_ID FROM t74037 JOIN T74036 ON T74037.T_ISSUE_ID = T74036.T_ISSUE_ID WHERE t74036.t_request_id=t74041.t_request_id AND T74037.T_ITEM_ID =t74027.T_ITEM_ID ) ISSED_ITEM, (SELECT SUM(ROUND(T74037.T_QUANTITY*T74003.T_QTY)) ADMINISTED_QTY FROM t74037 JOIN T74036 ON T74037.T_ISSUE_ID = T74036.T_ISSUE_ID JOIN T74003 ON T74037.T_UOM_ID = T74003.T_ITEM_UM_ID WHERE t74036.t_request_id=t74041.t_request_id AND T74037.T_ITEM_ID =t74027.T_ITEM_ID ) ADMINISTERED_QUTY,fn_Admin_Qty(t74027.T_ITEM_ID,'{requestId}') T_DOSE_DURATION_CODE FROM T74041 JOIN T74044 ON T74041.T_AMBU_REG_ID = T74044.T_AMBU_REG_ID JOIN t74027 ON T74044.T_STORE_ID = t74027.T_STORE_ID JOIN T74073 ON t74027.T_ITEM_ID = T74073.T_ID LEFT JOIN T74039 ON T74041.T_REQUEST_ID = T74039.T_REQUEST_ID LEFT JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID WHERE T74041.T_AMBU_REG_ID ={ambu} AND T_COST_TYPE_ID ='23' and t74041.t_request_id='{requestId}'  GROUP BY T74044.T_STORE_ID, T74041.T_AMBU_REG_ID, t74027.T_ITEM_ID, T74073.T_LANG2_NAME , T_DOSE_DURATION_CODE, T74040.T_PRESCRIPTION_ID , t74041.t_request_id ORDER BY T74073.T_LANG2_NAME ASC");


           // return Query($"SELECT DISTINCT T74044.T_STORE_ID, T74041.T_AMBU_REG_ID, T74073.T_LANG2_NAME, fn_Stock_Qty_Instant(t74027.T_ITEM_ID,t74044.T_STORE_ID) STOCK , t74027.T_ITEM_ID , (SELECT DISTINCT T74037.T_ITEM_ID FROM t74037 JOIN T74036 ON T74037.T_ISSUE_ID = T74036.T_ISSUE_ID WHERE t74036.t_request_id=t74041.t_request_id AND T74037.T_ITEM_ID =t74027.T_ITEM_ID ) ISSED_ITEM, (SELECT SUM(ROUND(T74037.T_QUANTITY*T74003.T_QTY)) ADMINISTED_QTY FROM t74037 JOIN T74036 ON T74037.T_ISSUE_ID = T74036.T_ISSUE_ID JOIN T74003 ON T74037.T_UOM_ID = T74003.T_ITEM_UM_ID WHERE t74036.t_request_id=t74041.t_request_id AND T74037.T_ITEM_ID =t74027.T_ITEM_ID ) ADMINISTERED_QUTY,fn_Admin_Qty(t74027.T_ITEM_ID,'{requestId}') T_DOSE_DURATION_CODE FROM T74041 JOIN T74044 ON T74041.T_AMBU_REG_ID = T74044.T_AMBU_REG_ID JOIN t74027 ON T74044.T_STORE_ID = t74027.T_STORE_ID JOIN T74073 ON t74027.T_ITEM_ID = T74073.T_ID LEFT JOIN T74039 ON T74041.T_REQUEST_ID = T74039.T_REQUEST_ID LEFT JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID WHERE T74041.T_AMBU_REG_ID ={ambu} AND T_COST_TYPE_ID ='23' and t74041.t_request_id='{requestId}' AND T_DISCH_STATUS IS NULL GROUP BY T74044.T_STORE_ID, T74041.T_AMBU_REG_ID, t74027.T_ITEM_ID, T74073.T_LANG2_NAME , T_DOSE_DURATION_CODE, T74040.T_PRESCRIPTION_ID , t74041.t_request_id ORDER BY T74073.T_LANG2_NAME ASC");

            // return Query($"SELECT DISTINCT T74044.T_STORE_ID,  T74041.T_AMBU_REG_ID,  T74073.T_LANG2_NAME,     fn_Stock_Qty_Instant(t74027.T_ITEM_ID,t74044.T_STORE_ID) STOCK ,  t74027.T_ITEM_ID ,   (SELECT DISTINCT T74037.T_ITEM_ID  FROM t74037  JOIN T74036  ON T74037.T_ISSUE_ID     = T74036.T_ISSUE_ID  WHERE t74036.t_request_id=t74041.t_request_id  AND T74037.T_ITEM_ID     =t74027.T_ITEM_ID  ) ISSED_ITEM,  (SELECT DISTINCT  ROUND(T74037.T_QUANTITY*T74003.T_QTY) ADMINISTED_QTY   FROM t74037  JOIN T74036   ON T74037.T_ISSUE_ID     = T74036.T_ISSUE_ID  JOIN T74003   ON T74037.T_UOM_ID     = T74003.T_ITEM_UM_ID  WHERE t74036.t_request_id=t74041.t_request_id  AND T74037.T_ITEM_ID     =t74027.T_ITEM_ID  ) ADMINISTERED_QUTY    ,(select t.T_DOSE_DURATION_CODE from  t74040 t  where  t.T_PRESCRIPTION_ID=t74040.T_PRESCRIPTION_ID and t.T_ITEM_CODE= t74027.T_ITEM_ID) T_DOSE_DURATION_CODE FROM T74041 JOIN T74044 ON T74041.T_AMBU_REG_ID = T74044.T_AMBU_REG_ID JOIN t74027 ON T74044.T_STORE_ID = t74027.T_STORE_ID JOIN T74073 ON t74027.T_ITEM_ID       = T74073.T_ID left JOIN T74039 ON T74041.T_REQUEST_ID = T74039.T_REQUEST_ID left JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID WHERE T74041.T_AMBU_REG_ID={ambu} AND T_COST_TYPE_ID        ='23' AND T_DISCH_STATUS       IS NULL GROUP BY T74044.T_STORE_ID,   T74041.T_AMBU_REG_ID,  t74027.T_ITEM_ID,  T74073.T_LANG2_NAME ,  T_DOSE_DURATION_CODE,  T74040.T_PRESCRIPTION_ID ,  t74041.t_request_id ORDER BY T74073.T_LANG2_NAME ASC");



        }

        public DataTable GetStockData(int ambu, int item)
        {
            return Query($"Select distinct T74044.T_STORE_ID,T74041.T_AMBU_REG_ID,T74073.T_LANG2_NAME, fn_Stock_Qty_Instant(t74027.T_ITEM_ID,t74044.T_STORE_ID) STOCK,to_char(T_EXPIRE_DATE,'dd-MM-yyyy')T_EXPIRE_DATE, T_LOT_NO, fn_Stock_Qty_BY_Lot_EXP(t74027.T_ITEM_ID,t74044.T_STORE_ID,T_LOT_NO,T_EXPIRE_DATE) STOCK_LOT_EXP ,t74027.T_ITEM_ID from T74041 JOIN T74044 on T74041.T_AMBU_REG_ID = T74044.T_AMBU_REG_ID JOIN t74027 on T74044.T_STORE_ID = t74027.T_STORE_ID JOIN T74073 ON t74027.T_ITEM_ID = T74073.T_ID where T74041.T_AMBU_REG_ID='{ambu}' AND T_COST_TYPE_ID='23'AND T_ITEM_ID ='{item}' GROUP BY T74044.T_STORE_ID,T74041.T_AMBU_REG_ID,t74027.T_ITEM_ID,T74073.T_LANG2_NAME,T_EXPIRE_DATE, T_LOT_NO");
        }
        
        public DataTable GetPreviousMedicine(int requId)
        {
            return Query($"SELECT t_index, 'DOSE' ||T_INDEX T_DOSE, ITEM_CODE, GEN_DESC, T_LANG2_NAME ITEM_DESC, T_ISSUE_ID, T_ENTRY_TIME, ROUND(T_QUANTITY*tt) T_QTY FROM (SELECT DENSE_RANK() OVER(Order By t74036.T_ISSUE_ID) AS t_index,T74114.T_ITEM_CODE ITEM_CODE, T74113.T_LANG2_NAME GEN_DESC, T74073.T_LANG2_NAME, t74036.T_ISSUE_ID, t74036.T_ENTRY_TIME, SUM(t74037.T_QUANTITY) T_QUANTITY, (SELECT T_QTY FROM T74003 WHERE T_ITEM_UM_ID = t74037.T_UOM_ID ) tt FROM t74036 INNER JOIN t74037 ON t74036.T_ISSUE_ID = t74037.T_ISSUE_ID INNER JOIN T74074 ON t74036.T_ISSUE_ID = T74074.T_ISSUE_ID INNER JOIN T74114 ON t74037.T_ITEM_ID = T74114.T_ITEM_CODE INNER JOIN T74113 on T74113.T_Gen_Code=T74114.T_Gen_Code LEFT JOIN T74073 ON t74037.T_ITEM_ID = T74073.T_ID WHERE t74036.T_REQUEST_ID = {requId} GROUP BY T74114.T_ITEM_CODE,T74113.T_LANG2_NAME, T74073.T_LANG2_NAME, t74036.T_ENTRY_TIME, t74037.T_UOM_ID, t74036.T_ISSUE_ID ORDER BY t74036.T_ISSUE_ID DESC)");

            //return Query($"SELECT t_index, 'DOSE' ||T_INDEX T_DOSE, ITEM_CODE, GEN_DESC, T_ISSUE_ID, T_ENTRY_TIME, round(T_QUANTITY*tt) T_QTY FROM (SELECT DENSE_RANK() OVER(Order By t74036.T_ISSUE_ID) AS t_index, V30001.ITEM_CODE, V30001.GEN_DESC, t74036.T_ISSUE_ID, t74036.T_ENTRY_TIME, SUM(t74037.T_QUANTITY) T_QUANTITY, (select T_QTY from T74003 where T_ITEM_UM_ID = t74037.T_UOM_ID) tt FROM t74036 INNER JOIN t74037 ON t74036.T_ISSUE_ID = t74037.T_ISSUE_ID INNER JOIN T74074 ON t74036.T_ISSUE_ID = T74074.T_ISSUE_ID INNER JOIN V30001 ON t74037.T_ITEM_ID = V30001.ITEM_CODE WHERE t74036.T_REQUEST_ID = {requId} GROUP BY V30001.ITEM_CODE, V30001.GEN_DESC, t74036.T_ENTRY_TIME,t74037.T_UOM_ID, t74036.T_ISSUE_ID ORDER BY t74036.T_ISSUE_ID DESC )");
            //  return Query($"select 'DOSE'||T_INDEX T_DOSE, ITEM_CODE, GEN_DESC, T_ISSUE_ID, SALE_PRICE, T_ENTRY_TIME, T_QUANTITY, T_TOTAL_AMOUNT from ( SELECT DENSE_RANK() OVER(Order By t74036.T_ISSUE_ID) AS t_index,V30001.ITEM_CODE, V30001.GEN_DESC, t74036.T_ISSUE_ID, t74037.T_SALE_PRICE SALE_PRICE, t74036.T_ENTRY_TIME, SUM(t74037.T_QUANTITY) T_QUANTITY, SUM(t74037.T_TOTAL_AMOUNT) T_TOTAL_AMOUNT FROM t74036 INNER JOIN t74037 ON t74036.T_ISSUE_ID = t74037.T_ISSUE_ID INNER JOIN T74074 ON t74036.T_ISSUE_ID = T74074.T_ISSUE_ID INNER JOIN V30001 ON t74037.T_ITEM_ID = V30001.ITEM_CODE WHERE t74036.T_REQUEST_ID = {requId} GROUP BY V30001.ITEM_CODE, V30001.GEN_DESC, t74037.T_SALE_PRICE,t74036.T_ENTRY_TIME,t74036.T_ISSUE_ID order by t74036.T_ISSUE_ID DESC)");
        }

        public DataTable GetServiceData(int requId)
        {
            return Query($"select t.*, (select distinct t74079.T_COST_TYPE_DTL_ID from t74079 join t74074 on t74079.T_BILL_ID =t74074.T_BILL_ID where t74074.T_REQUEST_ID=t.T_REQUEST_ID and t74079.T_COST_TYPE_DTL_ID=t.T_COST_TYPE_DTL_ID) issd_Item from (SELECT t41.T_REQUEST_ID,t73.T_LANG2_NAME, t89.T_SALE_PRICE, t89.T_COST_TYPE_DTL_ID FROM T74041 t41 JOIN t74044 t44 ON t41.T_AMBU_REG_ID = t44.T_AMBU_REG_ID JOIN t74027 t27 ON t44.T_STORE_ID = t27.T_STORE_ID JOIN t74073 t73 ON t27.T_ITEM_ID = t73.T_ID JOIN t74089 t89 ON t73.T_COST_TYPE_DTL_ID = t89.T_COST_TYPE_DTL_ID WHERE T_REQUEST_ID = '{requId}' AND t73.T_COST_TYPE_ID ='121' AND t89.T_ACTIVE ='1' UNION SELECT t41.T_REQUEST_ID,t73.T_LANG2_NAME, t89.T_SALE_PRICE, t89.T_COST_TYPE_DTL_ID FROM T74041 t41 JOIN t74073 t73 ON t41.T_AMBU_REG_ID = t73.T_ID JOIN t74089 t89 ON t73.T_COST_TYPE_DTL_ID = t89.T_COST_TYPE_DTL_ID WHERE T_REQUEST_ID = '{requId}' AND t73.T_COST_TYPE_ID ='81' AND t89.T_ACTIVE ='1' UNION SELECT t41.T_REQUEST_ID,t73.T_LANG2_NAME, t89.T_SALE_PRICE, t89.T_COST_TYPE_DTL_ID FROM T74041 t41 JOIN t74073 t73 ON t41.T_AMBU_REG_ID = t73.T_ID JOIN t74089 t89 ON t73.T_COST_TYPE_DTL_ID = t89.T_COST_TYPE_DTL_ID WHERE T_REQUEST_ID = '{requId}' AND t73.T_COST_TYPE_ID ='104' AND t89.T_ACTIVE ='1') t");
            //return Query($"SELECT t73.T_LANG2_NAME, t89.T_SALE_PRICE, t89.T_COST_TYPE_DTL_ID FROM T74041 t41 JOIN t74044 t44 ON t41.T_AMBU_REG_ID = t44.T_AMBU_REG_ID JOIN t74027 t27 ON t44.T_STORE_ID = t27.T_STORE_ID JOIN t74073 t73 ON t27.T_ITEM_ID = t73.T_ID JOIN t74089 t89 ON t73.T_COST_TYPE_DTL_ID = t89.T_COST_TYPE_DTL_ID WHERE T_REQUEST_ID = '{requId}' AND t73.T_COST_TYPE_ID ='121' AND t89.T_ACTIVE='1' UNION SELECT t73.T_LANG2_NAME, t89.T_SALE_PRICE, t89.T_COST_TYPE_DTL_ID FROM T74041 t41 JOIN t74073 t73 ON t41.T_AMBU_REG_ID = t73.T_ID JOIN t74089 t89 ON t73.T_COST_TYPE_DTL_ID = t89.T_COST_TYPE_DTL_ID WHERE T_REQUEST_ID = '{requId}' AND t73.T_COST_TYPE_ID ='81' AND t89.T_ACTIVE='1' UNION SELECT t73.T_LANG2_NAME, t89.T_SALE_PRICE, t89.T_COST_TYPE_DTL_ID FROM T74041 t41 JOIN t74073 t73 ON t41.T_AMBU_REG_ID = t73.T_ID JOIN t74089 t89 ON t73.T_COST_TYPE_DTL_ID = t89.T_COST_TYPE_DTL_ID WHERE T_REQUEST_ID = '{requId}' AND t73.T_COST_TYPE_ID ='104' AND t89.T_ACTIVE='1'");
        }
    }
}