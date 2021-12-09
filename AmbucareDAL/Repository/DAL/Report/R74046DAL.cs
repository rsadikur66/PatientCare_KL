﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Report
{
    public class R74046DAL : CommonDAL
    {
        public DataTable GetPrescription(int T_REQUEST_ID)
        {
            //return Query("SELECT T74054.T_LANG2_NAME COMPANY_NAME,T74054.T_LANG2_ADRS_NAME COMPANY_ADDRESS,T74054.T_EMAIL COMPANY_EMAIL,T74054.T_PHONE COMPANY_CONTACT_NO,T_WEB_URL COMPANY_WEB_ADDRESS,Get_Driver_Contact_No(T74041.T_AMBU_REG_ID) EMERGENCY_CONTACT,T74039.T_PRESCRIPTION_ID,T74046.T_PAT_ID,T74046.T_FIRST_LANG2_NAME|| ' ' ||T74046.T_FATHER_LANG2_NAME || ' ' || T74046.T_GFATHER_LANG2_NAME || ' ' || T74046.T_FAMILY_LANG2_NAME PAT_NAME,T74050.T_LANG2_NAME GENDER,TRUNC(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE) / 12, 0)|| ' Yrs '|| TRUNC(MOD(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE), 12), 0)|| ' Months' AGE,T74051.T_LANG2_NAME MARITAL_STATUS,T02003.T_LANG2_NAME NATIONALITY,T74046.T_MOBILE_NO,T74046.T_ADDRESS1||'-'|| T74046.T_POSTAL_CODE ADDRESS,T74004.T_FIRST_LANG2_NAME||' '||T74004.T_FATHER_LANG2_NAME|| ' '|| T74004.T_GFATHER_LANG2_NAME|| ' '|| T74004.T_FAMILY_LANG2_NAME DOC_NAME,T74022.T_LANG2_NAME DEGREE_NAME,T74020.T_LANG2_NAME SPECIALITY_NAME,T74021.T_LANG2_NAME DEPT_NAME,T74041.T_PROBLEM,T74041.T_PROBLEM_DURATION,T74041.T_PROB_DETAILS,T74055.T_LANG2_NAME CH_COMPLIENT,T74043.T_HIGHT,T74043.T_WEIGHT,T74043.T_BP_SYS,T74043.T_BP_DIA,T74043.T_TEMP,T74043.T_PULS,T74043.T_BSUGAR_F,T74039.T_REMARKS,T74039.T_ENTRY_DATE ,'Trade Name' TRADE_GEN,V30001.PRODUCT_DESC MED_NAME,GetDoseQuantity(T74040.T_PRSCRPTN_DTL_ID) DOSE_QUANTITY,T74040.T_MORNING_DOSE_UNIT ||' + '|| T74040.T_NOON_DOSE_UNIT ||' + '|| T74040.T_NIGHT_DOSE_UNIT ||' unit '|| DECODE(T74040.T_MEAL_INSTRUCTION, 1 , 'Before '|| T74040.T_INS_TIME||' Minutes of Meal', 2 , 'During Meal', 3 , 'After '|| T74040.T_INS_TIME||' Minutes of Meal','N/A') DOSES ,T74040.T_FREQUENCY_CODE ||' Per '||Get_T74076_Lang2(T74040.T_FREQUENCY_INS_ID) FREQUENCY ,' for '||T74040.T_DOSE_DURATION_CODE ||' '|| Get_T74076_Lang2(T74040.T_DOSE_DURATION_INS_ID) DURATION ,T74040.T_REMARKS_DTL,T74040.T_SATURDAY,T74040.T_SUNDAY,T74040.T_MONDAY,T74040.T_TUESDAY,T74040.T_WEDNESDAY,T74040.T_THURSDAY ,T74040.T_FRIDAY,T74040.T_DRUG_INACTIVE_FLAG FROM T74046 INNER JOIN T74050 ON T74046.T_SEX_CODE = T74050.T_SEX_CODE INNER JOIN T74051 ON T74046.T_MRTL_STATUS = T74051.T_MRTL_STATUS_CODE INNER JOIN T02003 ON T74046.T_NTNLTY_ID = T02003.T_NTNLTY_ID INNER JOIN T74041 ON T74046.T_PAT_ID = T74041.T_PAT_ID INNER JOIN T74043 ON T74041.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74039 ON T74039.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74015 ON T74041.T_AMBU_REG_ID = T74015.T_AMBU_REG_ID INNER JOIN T74004 ON T74015.T_EMP_ID = T74004.T_EMP_ID INNER JOIN T74005 ON T74004.T_EMP_TYP_ID = T74005.T_EMP_TYP_ID  INNER JOIN T74020 ON T74020.T_DOC_SPECI_ID  = T74004.T_DOC_SPECI_ID INNER JOIN T74021 ON T74021.T_DOC_DEPT_ID  = T74004.T_DOC_DEPT_ID INNER JOIN T74022 ON T74022.T_DOC_DEGREE_ID  = T74004.T_DOC_DEGREE_ID INNER JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID INNER JOIN T74073 ON T74073.T_ID = T74040.T_ITEM_CODE INNER JOIN T74054 ON T74054.T_COMPANY_ID  = T74041.T_COMPANY_ID INNER JOIN T74055 ON T74055.T_CH_COMP = T74041.T_CH_COMP INNER JOIN RUFAIDA.V30001 ON V30001.ITEM_CODE = T74040.T_ITEM_CODE WHERE T74039.T_DOC_ID    = T74015.T_EMP_ID AND (T74005.T_EMP_TYP_ID = 2) AND T74039.T_REQUEST_ID = " + T_REQUEST_ID + " UNION SELECT T74054.T_LANG2_NAME COMPANY_NAME ,T74054.T_LANG2_ADRS_NAME COMPANY_ADDRESS,T74054.T_EMAIL COMPANY_EMAIL ,T74054.T_PHONE COMPANY_CONTACT_NO,T_WEB_URL COMPANY_WEB_ADDRESS ,Get_Driver_Contact_No(T74041.T_AMBU_REG_ID) EMERGENCY_CONTACT,T74039.T_PRESCRIPTION_ID ,T74046.T_PAT_ID, T74046.T_FIRST_LANG2_NAME  || ' ' || T74046.T_FATHER_LANG2_NAME || ' ' || T74046.T_GFATHER_LANG2_NAME   || ' ' || T74046.T_FAMILY_LANG2_NAME PAT_NAME,  T74050.T_LANG2_NAME GENDER, TRUNC(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE) / 12, 0) || ' Yrs ' || TRUNC(MOD(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE), 12), 0)  || ' Months' AGE, T74051.T_LANG2_NAME MARITAL_STATUS, T02003.T_LANG2_NAME NATIONALITY,   T74046.T_MOBILE_NO,  T74046.T_ADDRESS1||'-'|| T74046.T_POSTAL_CODE ADDRESS, T74004.T_FIRST_LANG2_NAME || ' '  || T74004.T_FATHER_LANG2_NAME  || ' ' || T74004.T_GFATHER_LANG2_NAME || ' ' || T74004.T_FAMILY_LANG2_NAME DOC_NAME, T74022.T_LANG2_NAME DEGREE_NAME ,T74020.T_LANG2_NAME SPECIALITY_NAME ,T74021.T_LANG2_NAME DEPT_NAME ,T74041.T_PROBLEM ,T74041.T_PROBLEM_DURATION ,T74041.T_PROB_DETAILS ,T74055.T_LANG2_NAME CH_COMPLIENT ,T74043.T_HIGHT ,T74043.T_WEIGHT ,T74043.T_BP_SYS ,T74043.T_BP_DIA ,T74043.T_TEMP ,T74043.T_PULS ,T74043.T_BSUGAR_F  ,T74039.T_REMARKS, T74039.T_ENTRY_DATE ,'Gen Name' TRADE_GEN, V30001.GEN_DESC || ' ' || T74040.T_REQUEST_STRENGTH || ' ' || V30001.ROUTE_DESC || ' ' || V30001.FORM_DESC MED_NAME, GetDoseQuantity(T74040.T_PRSCRPTN_DTL_ID) DOSE_QUANTITY  ,T74040.T_MORNING_DOSE_UNIT ||' + '|| T74040.T_NOON_DOSE_UNIT ||' + '|| T74040.T_NIGHT_DOSE_UNIT ||' unit '|| DECODE(T74040.T_MEAL_INSTRUCTION, 1 , 'Before '|| T74040.T_INS_TIME||' Minutes of Meal', 2 , 'During Meal', 3 , 'After '|| T74040.T_INS_TIME||' Minutes of Meal','N/A') DOSES ,T74040.T_FREQUENCY_CODE ||' Per '||Get_T74076_Lang2(T74040.T_FREQUENCY_INS_ID) FREQUENCY ,' for '||T74040.T_DOSE_DURATION_CODE ||' '|| Get_T74076_Lang2(T74040.T_DOSE_DURATION_INS_ID) DURATION ,T74040.T_REMARKS_DTL,T74040.T_SATURDAY ,T74040.T_SUNDAY ,T74040.T_MONDAY ,T74040.T_TUESDAY ,T74040.T_WEDNESDAY ,T74040.T_THURSDAY ,T74040.T_FRIDAY ,T74040.T_DRUG_INACTIVE_FLAG FROM T74046 INNER JOIN T74050 ON T74046.T_SEX_CODE = T74050.T_SEX_CODE INNER JOIN T74051 ON T74046.T_MRTL_STATUS = T74051.T_MRTL_STATUS_CODE INNER JOIN T02003 ON T74046.T_NTNLTY_ID = T02003.T_NTNLTY_ID INNER JOIN T74041 ON T74046.T_PAT_ID = T74041.T_PAT_ID INNER JOIN T74043 ON T74041.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74039 ON T74039.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74015 ON T74041.T_AMBU_REG_ID = T74015.T_AMBU_REG_ID INNER JOIN T74004 ON T74015.T_EMP_ID = T74004.T_EMP_ID INNER JOIN T74005 ON T74004.T_EMP_TYP_ID = T74005.T_EMP_TYP_ID INNER JOIN T74020 ON T74020.T_DOC_SPECI_ID  = T74004.T_DOC_SPECI_ID INNER JOIN T74021 ON T74021.T_DOC_DEPT_ID  = T74004.T_DOC_DEPT_ID INNER JOIN T74022 ON T74022.T_DOC_DEGREE_ID  = T74004.T_DOC_DEGREE_ID INNER JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID INNER JOIN T74054 ON T74054.T_COMPANY_ID  = T74041.T_COMPANY_ID INNER JOIN T74055 ON T74055.T_CH_COMP = T74041.T_CH_COMP INNER JOIN RUFAIDA.V30001 ON T74040.T_GEN_CODE = V30001.GEN_CODE WHERE T74039.T_DOC_ID    = T74015.T_EMP_ID AND (T74005.T_EMP_TYP_ID = 2) AND  T74040.T_ITEM_CODE IS NULL AND T74040.T_GEN_CODE = V30001.GEN_CODE AND  T74040.T_FORM_ID = V30001.FORM_CODE AND  T74040.T_DRUG_ROUTE_CODE = V30001.ROUTE_CODE AND  T74040.T_REQUEST_STRENGTH = V30001.STRENGTH AND T74039.T_REQUEST_ID = " + T_REQUEST_ID + " GROUP BY  T74054.T_LANG2_NAME  ,T74054.T_LANG2_ADRS_NAME ,T74054.T_EMAIL ,T74054.T_PHONE ,T_WEB_URL  ,T74046.T_PAT_ID,  T74043.T_REQUEST_ID,  T74041.T_AMBU_REG_ID,  T74046.T_FIRST_LANG2_NAME  ,T74046.T_FATHER_LANG2_NAME   ,T74046.T_GFATHER_LANG2_NAME  ,T74046.T_FAMILY_LANG2_NAME  ,T74050.T_LANG2_NAME  ,T74046.T_BIRTH_DATE  ,T74051.T_LANG2_NAME   ,T02003.T_LANG2_NAME   ,T74046.T_MOBILE_NO  ,T74046.T_ADDRESS1  ,T74046.T_POSTAL_CODE  ,T74022.T_LANG2_NAME   ,T74020.T_LANG2_NAME    ,T74041.T_PROBLEM   ,T74041.T_PROBLEM_DURATION   ,T74041.T_PROB_DETAILS   ,T74055.T_LANG2_NAME   ,T74043.T_HIGHT  ,T74043.T_WEIGHT ,T74043.T_BP_SYS   ,T74043.T_BP_DIA   ,T74043.T_TEMP   ,T74043.T_PULS   ,T74043.T_BSUGAR_F   ,T74039.T_REMARKS  ,T74004.T_FIRST_LANG2_NAME   ,T74004.T_FATHER_LANG2_NAME   ,T74004.T_GFATHER_LANG2_NAME   ,T74004.T_FAMILY_LANG2_NAME ,T74021.T_LANG2_NAME   ,T74039.T_PRESCRIPTION_ID   ,T74039.T_ENTRY_DATE   ,V30001.GEN_DESC ,T74040.T_REQUEST_STRENGTH ,V30001.ROUTE_DESC ,V30001.FORM_DESC  ,T74040.T_PRSCRPTN_DTL_ID ,T74040.T_MORNING_DOSE_UNIT ,T74040.T_NOON_DOSE_UNIT ,T74040.T_NIGHT_DOSE_UNIT  ,T74040.T_MEAL_INSTRUCTION ,T74040.T_INS_TIME ,T74040.T_FREQUENCY_CODE ,T74040.T_FREQUENCY_INS_ID ,T74040.T_DOSE_DURATION_CODE ,T74040.T_DOSE_DURATION_INS_ID ,T74040.T_REMARKS_DTL ,T74040.T_SATURDAY ,T74040.T_SUNDAY ,T74040.T_MONDAY ,T74040.T_TUESDAY ,T74040.T_WEDNESDAY ,T74040.T_THURSDAY ,T74040.T_FRIDAY ,T74040.T_DRUG_INACTIVE_FLAG");

            //return Query("SELECT T74054.T_LANG2_NAME COMPANY_NAME,T74054.T_LANG2_ADRS_NAME COMPANY_ADDRESS,T74054.T_EMAIL COMPANY_EMAIL,T74054.T_PHONE COMPANY_CONTACT_NO,T_WEB_URL COMPANY_WEB_ADDRESS,Get_Driver_Contact_No(T74041.T_AMBU_REG_ID) EMERGENCY_CONTACT,T74039.T_PRESCRIPTION_ID,T74046.T_PAT_ID,T74046.T_FIRST_LANG2_NAME|| ' ' ||T74046.T_FATHER_LANG2_NAME || ' ' || T74046.T_GFATHER_LANG2_NAME || ' ' || T74046.T_FAMILY_LANG2_NAME PAT_NAME,T74050.T_LANG2_NAME GENDER,TRUNC(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE) / 12, 0)|| ' Yrs '|| TRUNC(MOD(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE), 12), 0)|| ' Months' AGE,T74051.T_LANG2_NAME MARITAL_STATUS,T02003.T_LANG2_NAME NATIONALITY,T74046.T_MOBILE_NO,T74046.T_ADDRESS1||'-'|| T74046.T_POSTAL_CODE ADDRESS,T74004.T_FIRST_LANG2_NAME||' '||T74004.T_FATHER_LANG2_NAME|| ' '|| T74004.T_GFATHER_LANG2_NAME|| ' '|| T74004.T_FAMILY_LANG2_NAME DOC_NAME,T74022.T_LANG2_NAME DEGREE_NAME,T74020.T_LANG2_NAME SPECIALITY_NAME,T74021.T_LANG2_NAME DEPT_NAME,T74041.T_PROBLEM,T74041.T_PROBLEM_DURATION,T74041.T_PROB_DETAILS,T74055.T_LANG2_NAME CH_COMPLIENT,T74043.T_HIGHT,T74043.T_WEIGHT,T74043.T_BP_SYS,T74043.T_BP_DIA,T74043.T_TEMP,T74043.T_PULS,T74043.T_BSUGAR_F,T74039.T_REMARKS,T74039.T_ENTRY_DATE ,'Trade Name' TRADE_GEN,V30001.PRODUCT_DESC MED_NAME,T74040.T_DOSE_DURATION_CODE DOSE_QUANTITY,T74040.T_MORNING_DOSE_UNIT ||' + '|| T74040.T_NOON_DOSE_UNIT ||' + '|| T74040.T_NIGHT_DOSE_UNIT ||' unit '|| DECODE(T74040.T_MEAL_INSTRUCTION, 1 , 'Before '|| T74040.T_INS_TIME||' Minutes of Meal', 2 , 'During Meal', 3 , 'After '|| T74040.T_INS_TIME||' Minutes of Meal','N/A') DOSES ,T74040.T_FREQUENCY_CODE ||' Per '||Get_T74076_Lang2(T74040.T_FREQUENCY_INS_ID) FREQUENCY ,' for '||T74040.T_DOSE_DURATION_CODE ||' '|| Get_T74076_Lang2(T74040.T_DOSE_DURATION_INS_ID) DURATION ,T74040.T_REMARKS_DTL,T74040.T_SATURDAY,T74040.T_SUNDAY,T74040.T_MONDAY,T74040.T_TUESDAY,T74040.T_WEDNESDAY,T74040.T_THURSDAY ,T74040.T_FRIDAY,T74040.T_DRUG_INACTIVE_FLAG FROM T74046 INNER JOIN T74050 ON T74046.T_SEX_CODE = T74050.T_SEX_CODE INNER JOIN T74051 ON T74046.T_MRTL_STATUS = T74051.T_MRTL_STATUS_CODE INNER JOIN T02003 ON T74046.T_NTNLTY_ID = T02003.T_NTNLTY_ID INNER JOIN T74041 ON T74046.T_PAT_ID = T74041.T_PAT_ID INNER JOIN T74043 ON T74041.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74039 ON T74039.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74015 ON T74041.T_AMBU_REG_ID = T74015.T_AMBU_REG_ID INNER JOIN T74004 ON T74015.T_EMP_ID = T74004.T_EMP_ID INNER JOIN T74005 ON T74004.T_EMP_TYP_ID = T74005.T_EMP_TYP_ID  INNER JOIN T74020 ON T74020.T_DOC_SPECI_ID  = T74004.T_DOC_SPECI_ID INNER JOIN T74021 ON T74021.T_DOC_DEPT_ID  = T74004.T_DOC_DEPT_ID INNER JOIN T74022 ON T74022.T_DOC_DEGREE_ID  = T74004.T_DOC_DEGREE_ID INNER JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID INNER JOIN T74073 ON T74073.T_ID = T74040.T_ITEM_CODE INNER JOIN T74054 ON T74054.T_COMPANY_ID  = T74041.T_COMPANY_ID INNER JOIN T74055 ON T74055.T_CH_COMP = T74041.T_CH_COMP INNER JOIN RUFAIDA.V30001 ON V30001.ITEM_CODE = T74040.T_ITEM_CODE WHERE T74039.T_DOC_ID    = T74015.T_EMP_ID AND (T74005.T_EMP_TYP_ID = 2) AND T74039.T_REQUEST_ID = " + T_REQUEST_ID + " UNION SELECT T74054.T_LANG2_NAME COMPANY_NAME ,T74054.T_LANG2_ADRS_NAME COMPANY_ADDRESS,T74054.T_EMAIL COMPANY_EMAIL ,T74054.T_PHONE COMPANY_CONTACT_NO,T_WEB_URL COMPANY_WEB_ADDRESS ,Get_Driver_Contact_No(T74041.T_AMBU_REG_ID) EMERGENCY_CONTACT,T74039.T_PRESCRIPTION_ID ,T74046.T_PAT_ID, T74046.T_FIRST_LANG2_NAME  || ' ' || T74046.T_FATHER_LANG2_NAME || ' ' || T74046.T_GFATHER_LANG2_NAME   || ' ' || T74046.T_FAMILY_LANG2_NAME PAT_NAME,  T74050.T_LANG2_NAME GENDER, TRUNC(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE) / 12, 0) || ' Yrs ' || TRUNC(MOD(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE), 12), 0)  || ' Months' AGE, T74051.T_LANG2_NAME MARITAL_STATUS, T02003.T_LANG2_NAME NATIONALITY,   T74046.T_MOBILE_NO,  T74046.T_ADDRESS1||'-'|| T74046.T_POSTAL_CODE ADDRESS, T74004.T_FIRST_LANG2_NAME || ' '  || T74004.T_FATHER_LANG2_NAME  || ' ' || T74004.T_GFATHER_LANG2_NAME || ' ' || T74004.T_FAMILY_LANG2_NAME DOC_NAME, T74022.T_LANG2_NAME DEGREE_NAME ,T74020.T_LANG2_NAME SPECIALITY_NAME ,T74021.T_LANG2_NAME DEPT_NAME ,T74041.T_PROBLEM ,T74041.T_PROBLEM_DURATION ,T74041.T_PROB_DETAILS ,T74055.T_LANG2_NAME CH_COMPLIENT ,T74043.T_HIGHT ,T74043.T_WEIGHT ,T74043.T_BP_SYS ,T74043.T_BP_DIA ,T74043.T_TEMP ,T74043.T_PULS ,T74043.T_BSUGAR_F  ,T74039.T_REMARKS, T74039.T_ENTRY_DATE ,'Gen Name' TRADE_GEN, V30001.GEN_DESC || ' ' || T74040.T_REQUEST_STRENGTH || ' ' || V30001.ROUTE_DESC || ' ' || V30001.FORM_DESC MED_NAME, T74040.T_DOSE_DURATION_CODE DOSE_QUANTITY  ,T74040.T_MORNING_DOSE_UNIT ||' + '|| T74040.T_NOON_DOSE_UNIT ||' + '|| T74040.T_NIGHT_DOSE_UNIT ||' unit '|| DECODE(T74040.T_MEAL_INSTRUCTION, 1 , 'Before '|| T74040.T_INS_TIME||' Minutes of Meal', 2 , 'During Meal', 3 , 'After '|| T74040.T_INS_TIME||' Minutes of Meal','N/A') DOSES ,T74040.T_FREQUENCY_CODE ||' Per '||Get_T74076_Lang2(T74040.T_FREQUENCY_INS_ID) FREQUENCY ,' for '||T74040.T_DOSE_DURATION_CODE ||' '|| Get_T74076_Lang2(T74040.T_DOSE_DURATION_INS_ID) DURATION ,T74040.T_REMARKS_DTL,T74040.T_SATURDAY ,T74040.T_SUNDAY ,T74040.T_MONDAY ,T74040.T_TUESDAY ,T74040.T_WEDNESDAY ,T74040.T_THURSDAY ,T74040.T_FRIDAY ,T74040.T_DRUG_INACTIVE_FLAG FROM T74046 INNER JOIN T74050 ON T74046.T_SEX_CODE = T74050.T_SEX_CODE INNER JOIN T74051 ON T74046.T_MRTL_STATUS = T74051.T_MRTL_STATUS_CODE INNER JOIN T02003 ON T74046.T_NTNLTY_ID = T02003.T_NTNLTY_ID INNER JOIN T74041 ON T74046.T_PAT_ID = T74041.T_PAT_ID INNER JOIN T74043 ON T74041.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74039 ON T74039.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74015 ON T74041.T_AMBU_REG_ID = T74015.T_AMBU_REG_ID INNER JOIN T74004 ON T74015.T_EMP_ID = T74004.T_EMP_ID INNER JOIN T74005 ON T74004.T_EMP_TYP_ID = T74005.T_EMP_TYP_ID INNER JOIN T74020 ON T74020.T_DOC_SPECI_ID  = T74004.T_DOC_SPECI_ID INNER JOIN T74021 ON T74021.T_DOC_DEPT_ID  = T74004.T_DOC_DEPT_ID INNER JOIN T74022 ON T74022.T_DOC_DEGREE_ID  = T74004.T_DOC_DEGREE_ID INNER JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID INNER JOIN T74054 ON T74054.T_COMPANY_ID  = T74041.T_COMPANY_ID INNER JOIN T74055 ON T74055.T_CH_COMP = T74041.T_CH_COMP INNER JOIN RUFAIDA.V30001 ON T74040.T_GEN_CODE = V30001.GEN_CODE WHERE T74039.T_DOC_ID    = T74015.T_EMP_ID AND (T74005.T_EMP_TYP_ID = 2) AND  T74040.T_ITEM_CODE IS NULL AND T74040.T_GEN_CODE = V30001.GEN_CODE AND  T74040.T_FORM_ID = V30001.FORM_CODE AND  T74040.T_DRUG_ROUTE_CODE = V30001.ROUTE_CODE AND  T74040.T_REQUEST_STRENGTH = V30001.STRENGTH AND T74039.T_REQUEST_ID = " + T_REQUEST_ID + " GROUP BY  T74054.T_LANG2_NAME  ,T74054.T_LANG2_ADRS_NAME ,T74054.T_EMAIL ,T74054.T_PHONE ,T_WEB_URL  ,T74046.T_PAT_ID,  T74043.T_REQUEST_ID,  T74041.T_AMBU_REG_ID,  T74046.T_FIRST_LANG2_NAME  ,T74046.T_FATHER_LANG2_NAME   ,T74046.T_GFATHER_LANG2_NAME  ,T74046.T_FAMILY_LANG2_NAME  ,T74050.T_LANG2_NAME  ,T74046.T_BIRTH_DATE  ,T74051.T_LANG2_NAME   ,T02003.T_LANG2_NAME   ,T74046.T_MOBILE_NO  ,T74046.T_ADDRESS1  ,T74046.T_POSTAL_CODE  ,T74022.T_LANG2_NAME   ,T74020.T_LANG2_NAME    ,T74041.T_PROBLEM   ,T74041.T_PROBLEM_DURATION   ,T74041.T_PROB_DETAILS   ,T74055.T_LANG2_NAME   ,T74043.T_HIGHT  ,T74043.T_WEIGHT ,T74043.T_BP_SYS   ,T74043.T_BP_DIA   ,T74043.T_TEMP   ,T74043.T_PULS   ,T74043.T_BSUGAR_F   ,T74039.T_REMARKS  ,T74004.T_FIRST_LANG2_NAME   ,T74004.T_FATHER_LANG2_NAME   ,T74004.T_GFATHER_LANG2_NAME   ,T74004.T_FAMILY_LANG2_NAME ,T74021.T_LANG2_NAME   ,T74039.T_PRESCRIPTION_ID   ,T74039.T_ENTRY_DATE   ,V30001.GEN_DESC ,T74040.T_REQUEST_STRENGTH ,V30001.ROUTE_DESC ,V30001.FORM_DESC  ,T74040.T_PRSCRPTN_DTL_ID ,T74040.T_MORNING_DOSE_UNIT ,T74040.T_NOON_DOSE_UNIT ,T74040.T_NIGHT_DOSE_UNIT  ,T74040.T_MEAL_INSTRUCTION ,T74040.T_INS_TIME ,T74040.T_FREQUENCY_CODE ,T74040.T_FREQUENCY_INS_ID ,T74040.T_DOSE_DURATION_CODE ,T74040.T_DOSE_DURATION_INS_ID ,T74040.T_REMARKS_DTL ,T74040.T_SATURDAY ,T74040.T_SUNDAY ,T74040.T_MONDAY ,T74040.T_TUESDAY ,T74040.T_WEDNESDAY ,T74040.T_THURSDAY ,T74040.T_FRIDAY ,T74040.T_DRUG_INACTIVE_FLAG");
            return Query("SELECT T74054.T_LANG2_NAME COMPANY_NAME, T74054.T_LANG2_ADRS_NAME COMPANY_ADDRESS,  T74054.T_EMAIL COMPANY_EMAIL,T74054.T_PHONE COMPANY_CONTACT_NO,  T_WEB_URL COMPANY_WEB_ADDRESS, Get_Driver_Contact_No(T74041.T_AMBU_REG_ID) EMERGENCY_CONTACT,  T74039.T_PRESCRIPTION_ID, T74046.T_PAT_ID,  T74046.T_FIRST_LANG2_NAME || ' ' ||T74046.T_FATHER_LANG2_NAME  || ' '|| T74046.T_GFATHER_LANG2_NAME  || ' '  || T74046.T_FAMILY_LANG2_NAME PAT_NAME,  T74050.T_LANG2_NAME GENDER,  TRUNC(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE) / 12, 0)  || ' Yrs '  || TRUNC(MOD(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE), 12), 0)  || ' Months' AGE,  T74051.T_LANG2_NAME MARITAL_STATUS,  T02003.T_LANG2_NAME NATIONALITY,  T74046.T_MOBILE_NO,  T74046.T_ADDRESS1  ||'-'  || T74046.T_POSTAL_CODE ADDRESS,  T74004.T_FIRST_LANG2_NAME  ||' '  ||T74004.T_FATHER_LANG2_NAME  || ' '  || T74004.T_GFATHER_LANG2_NAME  || ' '  || T74004.T_FAMILY_LANG2_NAME DOC_NAME,  T74022.T_LANG2_NAME DEGREE_NAME,  T74020.T_LANG2_NAME SPECIALITY_NAME,  T74021.T_LANG2_NAME DEPT_NAME,  T74041.T_PROBLEM,  T74041.T_PROBLEM_DURATION,  T74041.T_PROB_DETAILS,  T74055.T_LANG2_NAME CH_COMPLIENT,  T74043.T_HIGHT,  T74043.T_WEIGHT,  T74043.T_BP_SYS,  T74043.T_BP_DIA,  T74043.T_TEMP,  T74043.T_PULS,  T74043.T_BSUGAR_F,  T74039.T_REMARKS,  T74039.T_ENTRY_DATE ,  'Trade Name' TRADE_GEN,  V30001.PRODUCT_DESC MED_NAME,  T74040.T_DOSE_DURATION_CODE DOSE_QUANTITY,  'Take '|| T74040.T_FREQUENCY_CODE    ||' unit '  || DECODE(T74040.T_MEAL_INSTRUCTION, 1 , 'Before '  || T74040.T_INS_TIME  ||' Minutes of Meal', 2 , 'During Meal', 3 , 'After ' || T74040.T_INS_TIME  ||' Minutes of Meal','N/A') DOSES ,    Get_T30209_Lang2(T74040.T_FREQUENCY_INS_ID,T74040.T_PRSCRPTN_DTL_ID) FREQUENCY ,  ' for '|| Get_T30010_Lang2(T74040.T_DOSE_DURATION_INS_ID) DURATION ,  T74040.T_REMARKS_DTL,  T74040.T_SATURDAY,  T74040.T_SUNDAY,  T74040.T_MONDAY,  T74040.T_TUESDAY,  T74040.T_WEDNESDAY,  T74040.T_THURSDAY ,  T74040.T_FRIDAY,  T74040.T_DRUG_INACTIVE_FLAG FROM T74046 INNER JOIN T74050 ON T74046.T_SEX_CODE = T74050.T_SEX_CODE INNER JOIN T74051 ON T74046.T_MRTL_STATUS = T74051.T_MRTL_STATUS_CODE INNER JOIN T02003 ON T74046.T_NTNLTY_ID = T02003.T_NTNLTY_ID INNER JOIN T74041 ON T74046.T_PAT_ID = T74041.T_PAT_ID INNER JOIN T74043 ON T74041.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74039 ON T74039.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74015 ON T74041.T_AMBU_REG_ID = T74015.T_AMBU_REG_ID INNER JOIN T74004 ON T74015.T_EMP_ID = T74004.T_EMP_ID INNER JOIN T74005 ON T74004.T_EMP_TYP_ID = T74005.T_EMP_TYP_ID INNER JOIN T74020 ON T74020.T_DOC_SPECI_ID = T74004.T_DOC_SPECI_ID INNER JOIN T74021 ON T74021.T_DOC_DEPT_ID = T74004.T_DOC_DEPT_ID INNER JOIN T74022 ON T74022.T_DOC_DEGREE_ID = T74004.T_DOC_DEGREE_ID INNER JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID INNER JOIN T74073 ON T74073.T_ID = T74040.T_ITEM_CODE INNER JOIN T74054 ON T74054.T_COMPANY_ID = T74041.T_COMPANY_ID INNER JOIN T74055 ON T74055.T_CH_COMP = T74041.T_CH_COMP INNER JOIN RUFAIDA.V30001 ON V30001.ITEM_CODE      = T74040.T_ITEM_CODE WHERE T74039.T_DOC_ID    = T74015.T_EMP_ID AND (T74005.T_EMP_TYP_ID = 2) AND T74039.T_REQUEST_ID  = "+ T_REQUEST_ID + " UNION SELECT T74054.T_LANG2_NAME COMPANY_NAME ,  T74054.T_LANG2_ADRS_NAME COMPANY_ADDRESS,  T74054.T_EMAIL COMPANY_EMAIL ,  T74054.T_PHONE COMPANY_CONTACT_NO,  T_WEB_URL COMPANY_WEB_ADDRESS ,  Get_Driver_Contact_No(T74041.T_AMBU_REG_ID) EMERGENCY_CONTACT,  T74039.T_PRESCRIPTION_ID ,  T74046.T_PAT_ID,  T74046.T_FIRST_LANG2_NAME  || ' '  || T74046.T_FATHER_LANG2_NAME  || ' '  || T74046.T_GFATHER_LANG2_NAME  || ' '  || T74046.T_FAMILY_LANG2_NAME PAT_NAME,  T74050.T_LANG2_NAME GENDER,  TRUNC(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE) / 12, 0)  || ' Yrs '  || TRUNC(MOD(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE), 12), 0)  || ' Months' AGE,  T74051.T_LANG2_NAME MARITAL_STATUS,  T02003.T_LANG2_NAME NATIONALITY,  T74046.T_MOBILE_NO,  T74046.T_ADDRESS1  ||'-'  || T74046.T_POSTAL_CODE ADDRESS,  T74004.T_FIRST_LANG2_NAME  || ' '  || T74004.T_FATHER_LANG2_NAME  || ' '  || T74004.T_GFATHER_LANG2_NAME  || ' '  || T74004.T_FAMILY_LANG2_NAME DOC_NAME,  T74022.T_LANG2_NAME DEGREE_NAME ,  T74020.T_LANG2_NAME SPECIALITY_NAME ,  T74021.T_LANG2_NAME DEPT_NAME ,  T74041.T_PROBLEM ,  T74041.T_PROBLEM_DURATION ,  T74041.T_PROB_DETAILS ,  T74055.T_LANG2_NAME CH_COMPLIENT ,  T74043.T_HIGHT ,  T74043.T_WEIGHT ,  T74043.T_BP_SYS ,  T74043.T_BP_DIA ,  T74043.T_TEMP ,  T74043.T_PULS ,  T74043.T_BSUGAR_F ,  T74039.T_REMARKS,  T74039.T_ENTRY_DATE ,  'Gen Name' TRADE_GEN,  V30001.GEN_DESC  || ' '  || T74040.T_REQUEST_STRENGTH  || ' '  || V30001.ROUTE_DESC  || ' '  || V30001.FORM_DESC MED_NAME, T74040.T_DOSE_DURATION_CODE DOSE_QUANTITY,  'Take '|| T74040.T_FREQUENCY_CODE    ||' unit '  || DECODE(T74040.T_MEAL_INSTRUCTION, 1 , 'Before '  || T74040.T_INS_TIME  ||' Minutes of Meal', 2 , 'During Meal', 3 , 'After '  || T74040.T_INS_TIME  ||' Minutes of Meal','N/A') DOSES ,    Get_T30209_Lang2(T74040.T_FREQUENCY_INS_ID,T74040.T_PRSCRPTN_DTL_ID) FREQUENCY ,  ' for '|| Get_T30010_Lang2(T74040.T_DOSE_DURATION_INS_ID) DURATION ,  T74040.T_REMARKS_DTL,  T74040.T_SATURDAY ,  T74040.T_SUNDAY ,  T74040.T_MONDAY ,  T74040.T_TUESDAY ,  T74040.T_WEDNESDAY ,  T74040.T_THURSDAY ,  T74040.T_FRIDAY , T74040.T_DRUG_INACTIVE_FLAG FROM T74046 INNER JOIN T74050 ON T74046.T_SEX_CODE = T74050.T_SEX_CODE INNER JOIN T74051 ON T74046.T_MRTL_STATUS = T74051.T_MRTL_STATUS_CODE INNER JOIN T02003 ON T74046.T_NTNLTY_ID = T02003.T_NTNLTY_ID INNER JOIN T74041 ON T74046.T_PAT_ID = T74041.T_PAT_ID INNER JOIN T74043 ON T74041.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74039 ON T74039.T_REQUEST_ID = T74043.T_REQUEST_ID INNER JOIN T74015 ON T74041.T_AMBU_REG_ID = T74015.T_AMBU_REG_ID INNER JOIN T74004 ON T74015.T_EMP_ID = T74004.T_EMP_ID INNER JOIN T74005 ON T74004.T_EMP_TYP_ID = T74005.T_EMP_TYP_ID INNER JOIN T74020 ON T74020.T_DOC_SPECI_ID = T74004.T_DOC_SPECI_ID INNER JOIN T74021 ON T74021.T_DOC_DEPT_ID = T74004.T_DOC_DEPT_ID INNER JOIN T74022 ON T74022.T_DOC_DEGREE_ID = T74004.T_DOC_DEGREE_ID INNER JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID INNER JOIN T74054 ON T74054.T_COMPANY_ID = T74041.T_COMPANY_ID INNER JOIN T74055 ON T74055.T_CH_COMP = T74041.T_CH_COMP INNER JOIN RUFAIDA.V30001 ON T74040.T_GEN_CODE          = V30001.GEN_CODE WHERE T74039.T_DOC_ID         = T74015.T_EMP_ID AND (T74005.T_EMP_TYP_ID      = 2) AND T74040.T_ITEM_CODE       IS NULL AND T74040.T_GEN_CODE         = V30001.GEN_CODE AND T74040.T_FORM_ID          = V30001.FORM_CODE AND T74040.T_DRUG_ROUTE_CODE  = V30001.ROUTE_CODE AND T74040.T_REQUEST_STRENGTH = V30001.STRENGTH AND T74039.T_REQUEST_ID       = " + T_REQUEST_ID + " GROUP BY T74054.T_LANG2_NAME ,  T74054.T_LANG2_ADRS_NAME ,  T74054.T_EMAIL ,  T74054.T_PHONE ,  T_WEB_URL ,  T74046.T_PAT_ID,  T74043.T_REQUEST_ID,  T74041.T_AMBU_REG_ID,  T74046.T_FIRST_LANG2_NAME ,  T74046.T_FATHER_LANG2_NAME ,  T74046.T_GFATHER_LANG2_NAME ,  T74046.T_FAMILY_LANG2_NAME ,  T74050.T_LANG2_NAME ,  T74046.T_BIRTH_DATE ,  T74051.T_LANG2_NAME ,  T02003.T_LANG2_NAME ,  T74046.T_MOBILE_NO ,  T74046.T_ADDRESS1 ,  T74046.T_POSTAL_CODE ,  T74022.T_LANG2_NAME ,  T74020.T_LANG2_NAME ,  T74041.T_PROBLEM ,  T74041.T_PROBLEM_DURATION ,  T74041.T_PROB_DETAILS ,  T74055.T_LANG2_NAME ,  T74043.T_HIGHT ,  T74043.T_WEIGHT ,  T74043.T_BP_SYS ,  T74043.T_BP_DIA ,  T74043.T_TEMP ,  T74043.T_PULS ,  T74043.T_BSUGAR_F ,  T74039.T_REMARKS ,  T74004.T_FIRST_LANG2_NAME ,  T74004.T_FATHER_LANG2_NAME ,  T74004.T_GFATHER_LANG2_NAME ,  T74004.T_FAMILY_LANG2_NAME ,  T74021.T_LANG2_NAME ,  T74039.T_PRESCRIPTION_ID ,  T74039.T_ENTRY_DATE ,  V30001.GEN_DESC ,  T74040.T_REQUEST_STRENGTH ,  V30001.ROUTE_DESC ,  V30001.FORM_DESC ,  T74040.T_PRSCRPTN_DTL_ID ,  T74040.T_MORNING_DOSE_UNIT ,  T74040.T_NOON_DOSE_UNIT ,  T74040.T_NIGHT_DOSE_UNIT ,  T74040.T_MEAL_INSTRUCTION ,  T74040.T_INS_TIME ,  T74040.T_FREQUENCY_CODE ,  T74040.T_FREQUENCY_INS_ID ,  T74040.T_DOSE_DURATION_CODE ,  T74040.T_DOSE_DURATION_INS_ID ,  T74040.T_REMARKS_DTL ,  T74040.T_SATURDAY ,  T74040.T_SUNDAY ,  T74040.T_MONDAY ,  T74040.T_TUESDAY ,  T74040.T_WEDNESDAY ,  T74040.T_THURSDAY ,  T74040.T_FRIDAY ,  T74040.T_DRUG_INACTIVE_FLAG,  T74040.T_DOSE_DURATION_CODE");
        }
        public DataTable GetBill(int T_REQUEST_ID)
        {
            return Query("WITH TBL1 AS (SELECT T74041.T_REQUEST_ID, T74074.T_ENTRY_DATE BILL_DATE, T74074.T_BILL_ID, T74054.T_LANG2_NAME COMPANY_NAME, T74054.T_LANG2_ADRS_NAME COMPANY_ADDRESS, T74054.T_EMAIL COMPANY_EMAIL,    T74054.T_PHONE COMPANY_CONTACT_NO, T_WEB_URL COMPANY_WEB_ADDRESS, Get_Driver_Contact_No(T74041.T_AMBU_REG_ID) EMERGENCY_CONTACT, T74046.T_PAT_ID, T74046.T_FIRST_LANG2_NAME || ' ' ||T74046.T_FATHER_LANG2_NAME || ' '  || T74046.T_GFATHER_LANG2_NAME || ' ' || T74046.T_FAMILY_LANG2_NAME PAT_NAME, T74050.T_LANG2_NAME GENDER, TRUNC(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE) / 12, 0) || ' Yrs ' || TRUNC(MOD(MONTHS_BETWEEN(SysDate, T74046.T_BIRTH_DATE), 12), 0) || ' Months' AGE,    T74051.T_LANG2_NAME MARITAL_STATUS,    T02003.T_LANG2_NAME NATIONALITY,    T74046.T_MOBILE_NO,    T74046.T_ADDRESS1 ||'-' || T74046.T_POSTAL_CODE ADDRESS  FROM T74046  INNER JOIN T74050  ON T74046.T_SEX_CODE = T74050.T_SEX_CODE  INNER JOIN T74051  ON T74046.T_MRTL_STATUS = T74051.T_MRTL_STATUS_CODE  INNER JOIN T02003  ON T74046.T_NTNLTY_ID = T02003.T_NTNLTY_ID  INNER JOIN T74041  ON T74046.T_PAT_ID = T74041.T_PAT_ID  INNER JOIN T74054   ON T74054.T_COMPANY_ID = T74041.T_COMPANY_ID  INNER JOIN T74074 ON T74074.T_REQUEST_ID = T74041.T_REQUEST_ID  WHERE T74041.T_REQUEST_ID = " + T_REQUEST_ID + " ),  TBL2 AS  (SELECT T74074.T_REQUEST_ID,  T74074.T_BILL_ID,  T74074.T_ENTRY_DATE BILL_DATE, T74079.T_COST_TYPE_DTL_ID,T74072.T_COST_TYPE_ID,DECODE(T74072.T_COST_TYPE_ID,1,'DIAGONOSIS',81,'AMBULANCE',104,'DOCTOR',121,'SERVICES') COST_TYPE ,    T74073.T_LANG2_NAME COST_NAME ,    '' BOX_PCS ,    1 QUANTITY ,    0 RATE ,    T74079.T_PRICE LINE_TOTAL ,NVL(T74079.T_VAT,0) VAT ,NVL(T74079.T_DISCOUNT,0) DISCOUNT  FROM T74079  INNER JOIN T74073  ON T74079.T_COST_TYPE_DTL_ID = T74073.T_COST_TYPE_DTL_ID  INNER JOIN T74072  ON T74072.T_COST_TYPE_ID = T74073.T_COST_TYPE_ID  INNER JOIN T74074  ON T74074.T_BILL_ID      = T74079.T_BILL_ID    WHERE T74074.T_REQUEST_ID= " + T_REQUEST_ID + "  UNION ALL  SELECT T36.T_REQUEST_ID,  T74.T_BILL_ID,T74.T_ENTRY_DATE BILL_DATE,    T73.T_COST_TYPE_DTL_ID ,    T72.T_COST_TYPE_ID ,    T72.T_LANG2_NAME COST_TYPE ,    T73.T_LANG2_NAME COST_NAME ,    DECODE(T37.T_PCS_BOX,1,'PCS',2,'BOX') BOX_PCS ,    DECODE(T37.T_PCS_BOX,1,(T03.T_QTY        *SUM(T37.T_QUANTITY)),SUM(T37.T_QUANTITY)) QUANTITY ,    DECODE(T37.T_PCS_BOX,1,(T37.T_SALE_PRICE /T03.T_QTY),T37.T_SALE_PRICE ) RATE , SUM(T37.T_TOTAL_AMOUNT) LINE_TOTAL ,    T36.T_VAT VAT  ,    T36.T_DISCOUNT DISCOUNT FROM T74037 T37  INNER JOIN T74073 T73  ON T37.T_ITEM_ID = T73.T_ID  INNER JOIN T74036 T36  ON T36.T_ISSUE_ID = T37.T_ISSUE_ID  INNER JOIN T74003 T03  ON T03.T_ITEM_UM_ID = T37.T_UOM_ID  INNER JOIN T74072 T72  ON T72.T_COST_TYPE_ID  = T73.T_COST_TYPE_ID  INNER JOIN T74074 T74 ON T74.T_REQUEST_ID = T36.T_REQUEST_ID  WHERE T36.T_REQUEST_ID =  " + T_REQUEST_ID + "  GROUP BY T73.T_COST_TYPE_DTL_ID,    T72.T_COST_TYPE_ID,    T72.T_LANG2_NAME ,    T73.T_LANG2_NAME ,    T37.T_SALE_PRICE,    T36.T_DISCOUNT,    T36.T_VAT, T37.T_PCS_BOX, T03.T_QTY,T36.T_REQUEST_ID,T74.T_BILL_ID,T74.T_ENTRY_DATE  )SELECT * FROM TBL1 INNER JOIN TBL2 ON TBL1.T_REQUEST_ID = TBL2.T_REQUEST_ID");
        }
    }
}