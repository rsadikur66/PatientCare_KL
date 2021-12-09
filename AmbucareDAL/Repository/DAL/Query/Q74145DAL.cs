using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Query
{
    public class Q74145DAL : CommonDAL
    {

        public DataTable GetAllAcceptRequest(string userid,string from_date,string to_date)
        {

            DataTable query = Query($"SELECT t41.T_REQUEST_ID, t46.T_FIRST_LANG2_NAME || ' ' ||t46.T_FATHER_LANG2_NAME ||' ' ||t46.T_GFATHER_LANG2_NAME FullName, t41.T_AGE, t55.T_LANG2_NAME Cheif_Compliant, t41.T_PROBLEM, t41.T_PROBLEM_DURATION, t50.T_LANG2_NAME Gender, t46.T_MOBILE_NO,  t46.T_ADDRESS1 FROM T74041 t41 LEFT JOIN t74046 t46 ON t41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN T74055 t55 ON t41.T_CH_COMP = t55.T_CH_COMP  LEFT JOIN t74050 t50 ON t46.T_SEX_CODE = t50.T_SEX_CODE  WHERE T_USER_ID ='{userid}' AND T_ACCEPT_STATUS IS NOT NULL AND t41.T_CANCEL_STATUS IS NULL AND TO_Date(T_REQUEST_DATE,'DD-MON-YY') BETWEEN to_date('{from_date}') AND  to_date('{to_date}') and T41.T_REQUEST_ID not in (select T_REQUEST_ID  from t74041 where T_CASE_ARRIVAL is null and T_DISCH_STATUS is not null) ORDER BY T_REQUEST_ID DESC");
            return query;

        }



    }
}