using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class T74113DAL:CommonDAL
    {
        public DataTable GetMedicineByTrade()
        {
            return Query("select ITEM_CODE, TRADE_CODE,GEN_CODE, PRODUCT_DESC, STRENGTH, ROUTE_CODE, ROUTE_DESC, FORM_CODE, FORM_DESC  from RUFAIDA.V30001 inner join T74073  on V30001.ITEM_CODE = T74073.T_ID Where T_COST_TYPE_ID = 23");
        }
        public DataTable GetGen()
        {
            return Query("Select GEN_CODE, GEN_DESC  from RUFAIDA.V30001 inner join T74073  on V30001.ITEM_CODE = T74073.T_ID Group BY GEN_CODE,GEN_DESC");
        }
        public DataTable GetStrengthByGen(string T_GEN_CODE)
        {
            return Query("select Strength  from RUFAIDA.V30001 inner join T74073  on V30001.ITEM_CODE = T74073.T_ID Where Gen_CODE = '"+ T_GEN_CODE + "' and Strength is not null GROUP BY Strength ");
        }
        public DataTable GetRouteByGen(string T_GEN_CODE)
        {
            return Query("select Route_CODE, Route_DESC  from RUFAIDA.V30001 inner join T74073  on V30001.ITEM_CODE = T74073.T_ID Where Gen_CODE = '"+ T_GEN_CODE + "' and Route_CODE is not null and Route_DESC is not null GROUP BY Route_CODE, Route_DESC");
        }
        public DataTable GetFormByGen(string T_GEN_CODE)
        {
            return Query("select Form_CODE, Form_DESC  from RUFAIDA.V30001 inner join T74073  on V30001.ITEM_CODE = T74073.T_ID Where Gen_CODE = '"+ T_GEN_CODE + "' and Form_CODE is not null and Form_DESC is not null GROUP BY Form_CODE, Form_DESC");
        }
        public DataTable GetMedListByPres(int Pres)
        {
            return Query($"SELECT T40.T_ITEM_CODE,V31.PRODUCT_DESC, T40.T_TRADE_CODE,T40.T_GEN_CODE,(SELECT V31.GEN_DESC FROM  V30001 V31 WHERE V31.GEN_CODE=T40.T_GEN_CODE AND ROWNUM=1) GEN_DESC,T40.T_REQUEST_STRENGTH,T_DRUG_ROUTE_CODE,(SELECT V31.ROUTE_DESC FROM  V30001 V31 WHERE V31.ROUTE_CODE=T40.T_DRUG_ROUTE_CODE AND ROWNUM=1) ROUTE_DESC,T40.T_FORM_ID,(SELECT V31.FORM_DESC FROM  V30001 V31 WHERE V31.FORM_CODE=T40.T_FORM_ID AND ROWNUM=1) FORM_DESC,T40.T_MORNING_DOSE_UNIT,T40.T_NOON_DOSE_UNIT,T40.T_NIGHT_DOSE_UNIT,T40.T_MEAL_INSTRUCTION,DECODE(T40.T_MEAL_INSTRUCTION,1,'Before Meal',2,'During  Meal',3,'After  Meal') T_MEAL_INSTRUCTION_DESC,T40.T_INS_TIME,T40.T_SATURDAY,T40.T_SUNDAY,T40.T_MONDAY,T40.T_TUESDAY,T40.T_WEDNESDAY,T40.T_THURSDAY,T40.T_FRIDAY,T40.T_REMARKS_DTL,T40.T_FREQUENCY_CODE,T40.T_FREQUENCY_INS_ID,T09.T_LANG2_NAME T_FREQUENCY_DESC,T09.T_QTY_MON,T40.T_DOSE_DURATION_CODE ,T40.T_DOSE_DURATION_INS_ID,T10.T_LANG2_NAME T_DOSE_DURATION_DESC ,T40.T_DRUG_INACTIVE_FLAG FROM T74040 T40   LEFT JOIN V30001 V31 ON V31.ITEM_CODE  = T40.T_ITEM_CODE LEFT JOIN T30209 T09 ON T09.T_FREQUENCY_CODE =T40.T_FREQUENCY_INS_ID LEFT JOIN T30010 T10 ON T10.T_DOSE_DURATION_CODE =T40.T_DOSE_DURATION_INS_ID WHERE T_PRESCRIPTION_ID={Pres}");
        }
    }
}