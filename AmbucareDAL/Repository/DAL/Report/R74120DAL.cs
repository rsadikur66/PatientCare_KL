using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.DAL.Report
{
    public class R74120DAL:CommonDAL
    {
        public DataTable GetAllData(int storeid)
        {
          return Query("SELECT Get_StorName_By_Id(T74080.T_REQ_SET_BY_STORE_ID) from_store," +
                       " Get_StorName_By_Id(T74080.T_SEND_TO_STORE_ID) to_store," +
                       "Get_GenName(T74027.T_ITEM_ID,T74073.T_COST_TYPE_ID) GEN_NAME,  T74073.T_LANG2_NAME," +
                       "SUM(DISTINCT T74027.T_PURCHASE_QTY)PURCHASE_QUNT," +
                        "SUM(DISTINCT T74081.T_TRANSFER_QTY)ISSU_QUANTITY, T74027.T_EXPIRE_DATE, T74027.T_MF_DATE," +
                        "T74003.T_LANG2_NAME, T74027.T_UNIT_VALUE FROM T74027 INNER JOIN T74073 ON " +
                        "T74027.T_ITEM_ID = T74073.T_ID INNER JOIN T74003 ON T74003.T_ITEM_UM_ID = T74027.T_ITEM_UM_ID " +
                        "INNER JOIN T74089 ON T74089.T_COST_TYPE_DTL_ID = T74073.T_COST_TYPE_DTL_ID AND " +
                        "T74003.T_ITEM_UM_ID = T74089.T_ITEM_UM_ID INNER JOIN T74081 ON " +
                        "T74027.T_CUR_STOCK_ID = T74081.T_CUR_STOCK_ID LEFT OUTER JOIN T74080 ON " +
                        "T74081.T_TRANSFER_REQ_ID = T74080.T_TRANSFER_REQ_ID AND T74027.T_STORE_ID = T74080.T_REQ_SET_BY_STORE_ID " +
                        "WHERE T74027.T_STORE_ID = '"+ storeid + "' AND T74089.T_ACTIVE = '1'" +
                        "GROUP BY T74073.T_LANG2_NAME, T74027.T_ITEM_ID, T74027.T_EXPIRE_DATE, T74027.T_MF_DATE, T74003.T_LANG2_NAME, " +
                        "T74027.T_UNIT_VALUE, T74073.T_COST_TYPE_ID, T74080.T_REQ_SET_BY_STORE_ID, T74080.T_SEND_TO_STORE_ID ORDER BY T74027.T_EXPIRE_DATE");

        }

        public DataTable GetGridData()

        {
            return Query("SELECT T74041.T_REQUEST_ID,T74046.T_PAT_ID,T74046.T_PAT_NO, T74046.T_FIRST_LANG2_NAME || ' ' || T74046.T_FATHER_LANG2_NAME || ' ' || T74046.T_GFATHER_LANG2_NAME || ' ' || T74046.T_MOTHER_LANG2_NAME || ' ' || T74046.T_FAMILY_LANG2_NAME PATIENT_NAME,T74046.T_ADDRESS2, T74046.T_ALT_MOBILE_NO FROM T74041 INNER JOIN T74046 ON T74041.T_PAT_ID = T74046.T_PAT_ID ORDER BY T74041.T_REQUEST_ID DESC");
        }
    }
}