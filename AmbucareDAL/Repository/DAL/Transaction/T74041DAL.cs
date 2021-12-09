using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using AmbucareDAL.Models;
using Oracle.ManagedDataAccess.Client;
using AmbucareDAL.Repository.DAL;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class T74041DAL : CommonDAL
    {
        CommonDAL common = new CommonDAL();
        public DataTable GetAllDataOnMap_T74041(int P_AMBU_REG_ID, string P_LANG)
        {
            try
            {
                var parameters = new[]
                {
                    new OracleParameter("P_AMBU_REG_ID", P_AMBU_REG_ID),
                    new OracleParameter("P_LANG", P_LANG)
                };
                return ExecuteSelectProcedure("GetAllDataOnMap_T74041_new", parameters);
            }
            catch (Exception ex)
            {
                LogError("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + "." +
                         MethodBase.GetCurrentMethod() + " \n\nError: " + ex.Message);
                throw new Exception("Contact with service provider...");
            }


        }

        public DataTable GetPendingRequestData(string user,string lang)
        {
            return Query(
                $"SELECT DISTINCT t41_new.T_USER_ID, t41_new.T_CANCEL_STATUS, t41_new.T_CANCEL_REASON, T101.T_Lang{lang}_Name CANCELPROBLEM, TO_CHAR(t41_new.T_CAN_DATE,'dd-MON-yyyy') T_CAN_DATE, TO_CHAR(t41_new.T_CAN_DATE,'HH24:MI') T_CAN_TIME, t41_old.* FROM (SELECT t41.T_REQUEST_ID, T41.T_USER_ID, T41.T_Ref_Id, T41.T_ACTV_OPER, t46.T_FIRST_LANG{lang}_NAME ||' ' ||t46.T_FATHER_LANG{lang}_NAME || ' ' ||t46.T_GFATHER_LANG{lang}_NAME PAT_NAME, t46.T_FATHER_LANG{lang}_NAME , t46.T_GFATHER_LANG{lang}_NAME , t46.T_FIRST_LANG{lang}_NAME , t41.T_LATITUDE , t41.T_LONGITUDE , t46.T_MOBILE_NO, t46.T_ENTRY_DATE , t46.T_ALT_MOBILE_NO , t41.T_AGE , t46.T_SEX_CODE , t06.T_LANG{lang}_NAME GENDER , t46.T_NATIONAL_ID , t41.T_PROBLEM , t41.T_PROBLEM_DURATION , TO_CHAR(t41.T_REQUEST_TIME,'dd-mm-yyyy') REQUEST_DATE , TO_CHAR(t41.T_REQUEST_TIME,'HH24:MI')REQUEST_TIME, t41.T_MAP_LOC, t41.T_CALL_REASON_ID, t21.T_LANG2_NAME CALL_REASON_NAME,t20.T_CALLER_ID, t20.T_FIRST_LANG{lang}_NAME CALLER_FIRST_NAME, t20.T_LAST_LANG{lang}_NAME CALLER_LAST_NAME, t20.T_MOBILE_NO CALLER_MOBILE_NO, t20.T_ADDRESS CALLER_ADDRESS FROM T74041 t41 JOIN T74046 t46 ON t41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN t74050 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE LEFT JOIN T74120 t20 ON t41.T_CALLER_ID = t20.T_CALLER_ID LEFT JOIN T74121 t21 ON t41.T_CALL_REASON_ID = t21.T_CALL_REASON_ID  WHERE t41.T_USER_ID IS NULL AND t41.T_AMBU_REG_ID IS NULL AND t41.T_CANCEL_STATUS IS NULL ) t41_old LEFT JOIN t74041 t41_new ON T41_Old.T_Ref_Id=t41_new.T_Request_Id LEFT JOIN T74101 t101 ON t41_new.T_CANCEL_REASON=TO_CHAR( t101.T_CANCEL_REASON_ID) ORDER BY t41_old.T_REQUEST_ID DESC");
            // return Query($"select distinct t41_new.T_USER_ID, t41_new.T_CANCEL_STATUS, t41_new.T_CANCEL_REASON, T101.T_Lang{lang}_Name CANCELPROBLEM, TO_CHAR(t41_new.T_CAN_DATE,'dd-MON-yyyy') T_CAN_DATE, TO_CHAR(t41_new.T_CAN_DATE,'HH24:MI') T_CAN_TIME, t41_old.* from (SELECT t41.T_REQUEST_ID, T41.T_USER_ID,T41.T_Ref_Id,T41.T_ACTV_OPER, t46.T_FIRST_LANG{lang}_NAME ||' ' ||t46.T_FATHER_LANG{lang}_NAME || ' ' ||t46.T_GFATHER_LANG{lang}_NAME PAT_NAME, t46.T_FATHER_LANG{lang}_NAME , t46.T_GFATHER_LANG{lang}_NAME , t46.T_FIRST_LANG{lang}_NAME , t41.T_LATITUDE , t41.T_LONGITUDE , t46.T_MOBILE_NO, t46.T_ENTRY_DATE , t46.T_ALT_MOBILE_NO , t41.T_AGE , t46.T_SEX_CODE , t06.T_LANG{lang}_NAME GENDER , t46.T_NATIONAL_ID , t41.T_PROBLEM , t41.T_PROBLEM_DURATION , TO_CHAR(t41.T_REQUEST_TIME,'dd-mm-yyyy') REQUEST_DATE , TO_CHAR(t41.T_REQUEST_TIME,'HH24:MI')REQUEST_TIME, t41.T_MAP_LOC, t07.T_PROTOCAL_NO, t07.T_KAYIT_PROTOKOL, t07.T_PROTOKOL_ACIKLAMA, t07.T_OLAY_NO, t07.T_EKIP_NO, t07.T_TELEFON_NO, t07.T_CAGRI_YAPAN_KISI, t07.T_CAGRI_YAPAN_TIPI, t07.T_CAGRI_NEDENI, t07.T_ADRES, t07.T_BULUSMA_NOKTASI, t07.T_KAYIT_TARIH_ZAMAN, t07.T_CAGRI_TARIH_ZAMAN, t07.T_KAYIT_YAPAN, t07.T_ONAYLAYAN, t07.T_ACIKLAMA, t07.T_ID, t07.T_CALL_LATITUDE_Y, t07.T_CALL_LONGTITUDE_X, t07.T_IL, t07.T_ILCE, t07.T_MAHALLE, t07.T_SOKAK_ADI, t07.T_KAPI_NO, t07.T_SES_KAYIT, t07.T_ARAYANIN_ADI_SOYADI,t07.T_VAKA_ADRESI FROM T74041 t41 JOIN T74046 t46 ON t41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN t74050 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE LEFT JOIN T74207 t07 ON t41.T_PROTOCAL_NO = t07.T_PROTOCAL_NO WHERE t41.T_USER_ID IS NULL AND t41.T_AMBU_REG_ID IS NULL AND t41.T_CANCEL_STATUS IS NULL ) t41_old left join t74041 t41_new on T41_Old.T_Ref_Id=t41_new.T_Request_Id left join T74101 t101 on t41_new.T_CANCEL_REASON=to_char( t101.T_CANCEL_REASON_ID) left join t74207 t007 on t41_old.T_PROTOCAL_NO=t007.T_PROTOCAL_NO ORDER BY  t41_old.T_REQUEST_ID DESC"); // it was in where condition ( t007.T_USER_ID='{user}' or)

            //return Query($"select t41.T_REQUEST_ID,T41.T_PROBLEM,t46.T_FIRST_LANG2_NAME ||' ' ||t46.T_FATHER_LANG2_NAME || ' '||t46.T_GFATHER_LANG2_NAME PAT_NAME, t46.T_FATHER_LANG2_NAME ,t46.T_GFATHER_LANG2_NAME ,t46.T_FIRST_LANG2_NAME ,t41.T_LATITUDE ,t41.T_LONGITUDE ,t46.T_MOBILE_NO, t46.T_ENTRY_DATE ,t46.T_NATIONAL_ID ,t46.T_ALT_MOBILE_NO ,t41.T_AGE ,t46.T_SEX_CODE ,t06.T_LANG2_NAME GENDER ,t46.T_NATIONAL_ID , t41.T_PROBLEM ,t41.T_PROBLEM_DURATION ,to_char(t41.T_REQUEST_TIME,'dd-mm-yyyy') REQUEST_DATE , to_char(t41.T_REQUEST_TIME,'HH24:MI')REQUEST_TIME,t41.T_MAP_LOC,t07.T_PROTOCAL_NO,t07.T_KAYIT_PROTOKOL,t07.T_PROTOKOL_ACIKLAMA,t07.T_OLAY_NO,t07.T_EKIP_NO,t07.T_TELEFON_NO,t07.T_CAGRI_YAPAN_KISI,t07.T_CAGRI_YAPAN_TIPI,t07.T_CAGRI_NEDENI,t07.T_ADRES,t07.T_BULUSMA_NOKTASI,t07.T_KAYIT_TARIH_ZAMAN,t07.T_CAGRI_TARIH_ZAMAN,t07.T_KAYIT_YAPAN,t07.T_ONAYLAYAN,t07.T_ACIKLAMA,t07.T_ID,t07.T_CALL_LATITUDE_Y,t07.T_CALL_LONGTITUDE_X,t07.T_IL,t07.T_ILCE,t07.T_MAHALLE,t07.T_SOKAK_ADI,t07.T_KAPI_NO,t07.T_SES_KAYIT from T74041 t41 JOIN T74046 t46 ON t41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN t74050 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE LEFT JOIN T74207 t07 ON t41.T_PROTOCAL_NO = t07.T_PROTOCAL_NO where T_USER_ID is null AND T_AMBU_REG_ID is null AND T_CANCEL_STATUS IS NULL ORDER BY T_REQUEST_ID ASC");
        }

        public DataTable GetActivePatientsData(string code,string lang)
        {
            //return Query($"SELECT t41.T_REQUEST_ID,T41.T_USER_ID,T41.T_PROBLEM,t41.T_REMARKS,t46.T_FIRST_LANG2_NAME || ' ' || t46.T_FATHER_LANG2_NAME || ' ' || t46.T_GFATHER_LANG2_NAME PAT_NAME,t46.T_FATHER_LANG2_NAME,t46.T_GFATHER_LANG2_NAME,t46.T_FIRST_LANG2_NAME,t41.T_LATITUDE,t41.T_LONGITUDE,t46.T_MOBILE_NO,t46.T_ENTRY_DATE,t46.T_NATIONAL_ID,t46.T_ALT_MOBILE_NO,t41.T_AGE,t46.T_SEX_CODE,t06.T_LANG2_NAME GENDER,t46.T_NATIONAL_ID,t41.T_PROBLEM,t41.T_PROBLEM_DURATION,TO_CHAR(t41.T_REQUEST_TIME, 'dd-mm-yyyy') REQUEST_DATE,TO_CHAR(t41.T_REQUEST_TIME, 'HH24:MI')REQUEST_TIME,t41.T_MAP_LOC FROM T74041 t41 inner JOIN T74046 t46 ON t41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN T02006 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE left join t74057 t57 on T57.T_USER_ID = T41.T_USER_ID WHERE T_AMBU_REG_ID   IS not NULL AND T_CANCEL_STATUS IS  NULL and T41.T_DISCH_STATUS is null and T57.T_ZONE_CODE = '{code}' ORDER BY T_REQUEST_ID ASC");

            //return Query($"select t.* from (SELECT t41.T_REQUEST_ID, (select t74041.T_REQUEST_ID from t74041 where t74041.T_REF_ID=t41.T_REQUEST_ID) req_41,T41.T_USER_ID,T41.T_PROBLEM, t41.T_REMARKS, t46.T_FIRST_LANG2_NAME || ' ' || t46.T_FATHER_LANG2_NAME || ' ' || t46.T_GFATHER_LANG2_NAME PAT_NAME,t46.T_FATHER_LANG2_NAME,t46.T_GFATHER_LANG2_NAME,t46.T_FIRST_LANG2_NAME,t41.T_LATITUDE,t41.T_LONGITUDE,t46.T_MOBILE_NO,t46.T_ENTRY_DATE,t46.T_NATIONAL_ID,t46.T_ALT_MOBILE_NO,t41.T_AGE,t46.T_SEX_CODE,t06.T_LANG2_NAME GENDER,t41.T_PROBLEM_DURATION,TO_CHAR(t41.T_REQUEST_TIME, 'dd-mm-yyyy') REQUEST_DATE,TO_CHAR(t41.T_REQUEST_TIME, 'HH24:MI')REQUEST_TIME,t41.T_MAP_LOC FROM T74041 t41 inner JOIN T74046 t46 ON t41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN T74050 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE left join t74057 t57 on T57.T_USER_ID = T41.T_USER_ID WHERE T_AMBU_REG_ID   IS not NULL AND T_CANCEL_STATUS IS  NULL and T41.T_DISCH_STATUS is null and T57.T_ZONE_CODE = '{code}' ORDER BY T_REQUEST_ID ASC)  t where t.req_41 is null");
            return Query($"SELECT t41_new.T_USER_ID CANCEL_USER, t41_new.T_CANCEL_STATUS, t41_new.T_CANCEL_REASON, T101.T_Lang{lang}_Name CANCELPROBLEM, TO_CHAR(t41_new.T_CAN_DATE,'dd-MON-yyyy') T_CAN_DATE, TO_CHAR(t41_new.T_CAN_DATE,'HH24:MI') T_CAN_TIME, t.* FROM (SELECT t41.T_REQUEST_ID, (SELECT t74041.T_REQUEST_ID FROM t74041 WHERE t74041.T_REF_ID=t41.T_REQUEST_ID ) req_41, T41.T_USER_ID, T41.T_Ref_Id, T41.T_PROTOCAL_NO, T41.T_PROBLEM, t41.T_REMARKS, t46.T_FIRST_LANG{lang}_NAME || ' ' || t46.T_FATHER_LANG{lang}_NAME || ' ' || t46.T_GFATHER_LANG{lang}_NAME PAT_NAME, t46.T_FATHER_LANG{lang}_NAME, t46.T_GFATHER_LANG{lang}_NAME, t46.T_FIRST_LANG{lang}_NAME, t41.T_LATITUDE, t41.T_LONGITUDE, t46.T_MOBILE_NO, t46.T_ENTRY_DATE, t46.T_NATIONAL_ID, t46.T_ALT_MOBILE_NO, t41.T_AGE, t46.T_SEX_CODE, t06.T_LANG{lang}_NAME GENDER, t41.T_PROBLEM_DURATION, TO_CHAR(t41.T_REQUEST_TIME, 'dd-mm-yyyy') REQUEST_DATE, TO_CHAR(t41.T_REQUEST_TIME, 'HH24:MI')REQUEST_TIME, t41.T_MAP_LOC FROM T74041 t41 INNER JOIN T74046 t46 ON t41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN T74050 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE LEFT JOIN t74057 t57 ON T57.T_USER_ID = T41.T_USER_ID WHERE T_AMBU_REG_ID IS NOT NULL AND T_CANCEL_STATUS IS NULL AND T41.T_DISCH_STATUS IS NULL AND t41.T_DISCHARGE_TIME  IS NULL AND T_ACCEPT_STATUS IS NOT NULL AND T57.T_ZONE_CODE = '{code}' ORDER BY T_REQUEST_ID ASC ) t left join t74041 t41_new on t.T_Ref_Id=t41_new.T_Request_Id left join T74101 t101 on t41_new.T_CANCEL_REASON=to_char( t101.T_CANCEL_REASON_ID) left join t74207 t007 on t.T_PROTOCAL_NO=t007.T_PROTOCAL_NO where  t007.T_USER_ID is null Or t.req_41 IS NULL");
        }
        public string SaveRemarks(int req, string rem)
        {
            string msg = "";
            BeginTransaction();
            if (Command($"UPDATE T74041 SET T_REMARKS='{rem}' WHERE T_REQUEST_ID={req}"))
            {
                CommitTransaction();
                msg = "Data saved successfully";
            }
            else
            {
                CommitTransaction();
                msg = "Data not saved";
            }
            return msg;

        }

        //--------------------------------Search Crieteria---------------

        //using CTE
        //public DataTable getAmbulanceList(string zone)
        //{
        //    return Query($"WITH cte_products AS ( SELECT DISTINCT t26.ID,T44.T_AMBU_REG_ID,t57.T_USER_ID AMB_CAPASITY,t26.T_LATITUDE,t26.T_LONGITUDE,t41.t_request_id, RANK() OVER( PARTITION BY t57.T_USER_ID ORDER BY Id DESC) t_rank FROM T74057 t57 LEFT JOIN T74041 t41 on t57.t_user_id = t41.t_user_id LEFT JOIN T74026 t26 on t57.T_USER_ID = t26.T_USER_ID left join t74015 t15 on t57.T_EMP_ID = t15.T_EMP_ID left join t74014 t14 on t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID left join t74047 t47 on t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID!=0 WHERE t57.t_zone_code = '{zone}' and t_role_code=24 and t41.T_ACCEPT_STATUS is not null and t41.T_DISCH_STATUS is null and t41.T_CANCEL_STATUS is null ORDER BY ID DESC ) SELECT T_AMBU_REG_ID,AMB_CAPASITY, T_LATITUDE as  \"latitude\", T_LONGITUDE \"longitude\",'Amb_MarkerG'  \"markImg\" FROM cte_products WHERE t_rank = 1");
        //}

        //using udf
        public DataTable getAmbulanceList(string zone)
        {
            return Query($"SELECT DISTINCT T44.T_AMBU_REG_ID, t57.T_USER_ID AMB_CAPASITY, GETLAT(t57.T_USER_ID) \"latitude\", GETLONG(t57.T_USER_ID) \"longitude\", 'Amb_MarkerG' \"markImg\", t41.t_request_id, 0 AS \"text\", COUNT(t41.T_ACCEPT_TIME) \"acceptCount\", t65.T_LATITUDE HOS_LAT, t65.T_LONGITUDE HOS_LON FROM T74057 t57 LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 LEFT JOIN T02065 t65 ON t65.T_SITE_CODE = t41.T_SITE_DIS_CODE WHERE t57.t_zone_code = '{zone}' AND t_role_code =24 AND t41.T_ACCEPT_STATUS IS NOT NULL AND t41.T_DISCH_STATUS IS NULL AND t41.T_DISCHARGE_TIME  IS NULL  AND t41.T_CANCEL_STATUS IS NULL GROUP BY T44.T_AMBU_REG_ID, t57.T_USER_ID, t41.t_request_id, t65.T_LATITUDE , t65.T_LONGITUDE");
            // return Query($"SELECT DISTINCT T44.T_AMBU_REG_ID, t57.T_USER_ID AMB_CAPASITY, GETLAT(t57.T_USER_ID) \"latitude\", GETLONG(t57.T_USER_ID) \"longitude\", 'Amb_MarkerG' \"markImg\", t41.t_request_id, 0 AS \"text\", COUNT(t41.T_ACCEPT_TIME) \"acceptCount\", t65.T_LATITUDE HOS_LAT, t65.T_LONGITUDE HOS_LON FROM T74057 t57 LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 LEFT JOIN T02065 t65 ON t65.T_SITE_CODE = t41.T_SITE_DIS_CODE AND T65.T_SITE_CODE = t41.T_SITE_DIS_CODE WHERE t57.t_zone_code = '{zone}' AND t_role_code =24 AND t41.T_ACCEPT_STATUS IS NOT NULL AND t41.T_DISCH_STATUS IS NULL AND t41.T_CANCEL_STATUS IS NULL and t57.T_USER_ID NOT IN (SELECT T_REQ_USER_ID FROM T74117 WHERE T74117.T_Final_Flag IS NULL) GROUP BY T44.T_AMBU_REG_ID, t57.T_USER_ID, t41.t_request_id, t65.T_LATITUDE, t65.T_LONGITUDE");
            // return Query($"SELECT DISTINCT T44.T_AMBU_REG_ID, t57.T_USER_ID AMB_CAPASITY, GETLAT(t57.T_USER_ID) \"latitude\", GETLONG(t57.T_USER_ID) \"longitude\", 'Amb_MarkerG' \"markImg\", t41.t_request_id,0 as \"text\",count(t41.T_ACCEPT_TIME) \"acceptCount\" FROM T74057 t57 LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 WHERE t57.t_zone_code = '{zone}' AND t_role_code =24 AND t41.T_ACCEPT_STATUS IS NOT NULL AND t41.T_DISCH_STATUS IS NULL AND t41.T_CANCEL_STATUS IS NULL group by T44.T_AMBU_REG_ID,t57.T_USER_ID, t41.t_request_id");
        }

        public DataTable GetAllUserLatlong(string zone)
        {
            return Query(
                $"SELECT T44.T_Ambu_Reg_Id,t26.T_USER_ID AMB_CAPASITY, T26.T_Latitude \"latitude\", T26.T_Longitude \"longitude\", 'Amb_Marker' as \"markImg\", '0' as \"text\",0 as \"distance\",count(t41.T_Accept_Time) \"acceptCount\",count(t41.T_CAN_DATE) \"rejectCount\" FROM T74026 T26 INNER JOIN T74057 T57 on t26.T_USER_ID = t57.T_USER_ID INNER JOIN T74015 t15 on t57.T_EMP_ID = t15.T_EMP_ID INNER JOIN T74014 t14 on t15.T_AMBU_REG_ID = t14.T_AMBU_REG_ID INNER JOIN T74047 t47 on t14.T_AMB_TYPE_ID = t47.T_AMBU_TYPE_ID INNER JOIN T74044 T44 ON t15.T_AMBU_REG_ID = t44.T_AMBU_REG_ID left join T74041 t41 on t41.T_AMBU_REG_ID = t15.T_AMBU_REG_ID and TO_CHAR(DECODE(T41.T_Accept_Status,1,t41.T_Accept_Time,t41.T_CAN_DATE),'DD-MM-YY') = TO_CHAR(SYSDATE,'DD-MM-YY') WHERE t57.T_LOGIN_STATUS = 1 AND t57.T_ZONE_CODE ={zone} AND t57.T_ROLE_CODE = 24 AND T44.T_Ambu_Reg_Id NOT IN (SELECT DISTINCT T41.T_AMBU_REG_ID FROM T74041 T41 WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_AMBU_REG_ID IS NOT NULL) and t26.id=(select max(id) from t74026 where t74026.t_user_id=t26.T_USER_ID) AND T57.T_User_Id NOT IN (SELECT T_REQ_USER_ID FROM T74117 WHERE T74117.T_Final_Flag IS NULL) GROUP BY T44.T_Ambu_Reg_Id,t26.T_USER_ID,T26.T_Latitude,T26.T_Longitude ORDER BY \"distance\" ASC");
        }
        //with CTE--common table expression
        //public DataTable getLoggedOutAmbulanceList(string zone)
        //{
        //    return Query($"WITH cte_products AS ( SELECT DISTINCT t26.ID,T44.T_AMBU_REG_ID,t57.T_USER_ID AMB_CAPASITY,t26.T_LATITUDE,t26.T_LONGITUDE,t41.t_request_id, RANK() OVER( PARTITION BY t57.T_USER_ID ORDER BY Id DESC) t_rank FROM T74057 t57 LEFT JOIN T74041 t41 on t57.t_user_id = t41.t_user_id LEFT JOIN T74026 t26 on t57.T_USER_ID = t26.T_USER_ID left join t74015 t15 on t57.T_EMP_ID = t15.T_EMP_ID left join t74014 t14 on t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID left join t74047 t47 on t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID!=0 WHERE t57.t_zone_code = '{zone}' and t57.t_role_code=24 and t57.t_login_status is null and t57.T_USER_ID not in (select distinct t_user_id from t74041 t41 where t41.T_ACCEPT_STATUS is not null and t41.T_DISCH_STATUS is null and t41.T_CANCEL_STATUS is null and t_user_id is not null) ) SELECT DISTINCT T_AMBU_REG_ID,AMB_CAPASITY, T_LATITUDE \"latitude\", T_LONGITUDE \"longitude\",'Amb_MarkerR' \"markImg\" FROM cte_products WHERE t_rank = 1");
        //}

        //with multiple query
        public DataTable getLoggedOutAmbulanceList(string zone)
        {
            //DataTable newtable = new DataTable();
            //newtable.Columns.Add("T_AMBU_REG_ID");
            //newtable.Columns.Add("ID");
            //newtable.Columns.Add("AMB_CAPASITY");
            //newtable.Columns.Add("latitude");
            //newtable.Columns.Add("longitude");
            //newtable.Columns.Add("markImg");
            //DataTable userId =
            //    Query(
            //        $"select distinct t57.T_USER_ID from t74057 t57 LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id LEFT JOIN T74026 t26 ON t57.T_USER_ID = t26.T_USER_ID LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 where t57.t_zone_code = '{zone}' AND t57.t_role_code =24 AND t57.t_login_status IS NULL AND t57.T_USER_ID NOT IN (SELECT DISTINCT t_user_id FROM t74041 t41 WHERE t41.T_ACCEPT_STATUS IS NOT NULL AND t41.T_DISCH_STATUS IS NULL AND t41.T_CANCEL_STATUS IS NULL AND t_user_id IS NOT NULL )");
            //for (int i = 0; i < userId.Rows.Count; i++)
            //{
            //    var id = userId.Rows[i]["T_USER_ID"].ToString();
            //    var ddd = Query(
            //        $"select T_AMBU_REG_ID,ID,T_USER_ID,T_LATITUDE \"latitude\", T_LONGITUDE \"longitude\",'Amb_MarkerR' \"markImg\" from (select T44.T_AMBU_REG_ID,t26.ID,t26.T_USER_ID,t26.T_LATITUDE, t26.T_LONGITUDE from t74026 t26 left join t74057 t57 on t26.T_USER_ID = t57.T_USER_ID LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 where t26.T_USER_ID = '{id}' order by t26.ID desc) where ROWNUM = 1");

            //    foreach (DataRow dr in ddd.Rows)
            //    {
            //        newtable.Rows.Add(dr.ItemArray);
            //    }
            //}
            //return newtable;
            return Query($"SELECT T14.T_AMBU_REG_ID, T26.ID, T57.T_USER_ID AMB_CAPASITY, T26.T_LATITUDE \"latitude\", T26.T_LONGITUDE \"longitude\", 'Amb_MarkerR' \"markImg\" FROM T74057 T57 LEFT JOIN T74026 T26 ON T57.T_User_Id=T26.T_User_Id LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 WHERE t26.id=(select max(id) from t74026 where t74026.t_user_id=t26.T_USER_ID) AND t57.t_zone_code = '{zone}' AND t57.t_role_code =24 AND t57.t_login_status IS NULL AND t57.T_USER_ID NOT IN (SELECT DISTINCT t_user_id FROM t74041 t41 WHERE t41.T_DISCH_STATUS IS NULL AND t_user_id IS NOT NULL)");
        }

        //with oracle function
        //public DataTable getLoggedOutAmbulanceList(string zone)
        //{
        //    return Query($"SELECT DISTINCT T44.T_AMBU_REG_ID, t57.T_USER_ID AMB_CAPASITY,(select getLat(t57.T_USER_ID) from dual) \"latitude\", (select getLong(t57.T_USER_ID) from dual) \"longitude\", 'Amb_MarkerR' \"markImg\" FROM t74057 t57 LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id LEFT JOIN T74026 t26 ON t57.T_USER_ID = t26.T_USER_ID LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 WHERE t57.t_zone_code = '{zone}' AND t57.t_role_code = 24 AND t57.t_login_status IS NULL AND t57.T_USER_ID NOT IN (SELECT DISTINCT t_user_id FROM t74041 t41 WHERE t41.T_ACCEPT_STATUS IS NOT NULL AND t41.T_DISCH_STATUS IS NULL AND t41.T_CANCEL_STATUS IS NULL AND t_user_id IS NOT NULL) order by T_AMBU_REG_ID");
        //}


        //public DataTable getCleanNeedAmbulanceList(string zone)
        //{
        //    return Query($"WITH cte_products AS ( SELECT distinct t26.ID,T44.T_AMBU_REG_ID,t57.T_USER_ID AMB_CAPASITY,t26.T_LATITUDE,t26.T_LONGITUDE,t41.t_request_id, RANK() OVER( PARTITION BY t57.T_USER_ID ORDER BY Id DESC) t_rank FROM T74057 t57 LEFT JOIN T74041 t41 on t57.t_user_id = t41.t_user_id LEFT JOIN T74026 t26 on t57.T_USER_ID = t26.T_USER_ID left join t74015 t15 on t57.T_EMP_ID = t15.T_EMP_ID left join t74014 t14 on t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID left join t74047 t47 on t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID!=0 WHERE t57.t_zone_code = '{zone}' and t57.t_role_code=24 and t41.T_TEAM_ARVL_STN is not null and t41.T_DISCH_STATUS is null and t41.T_RDY_FOR_NXT_PAT is null and t41.T_ACCEPT_STATUS is not null and t41.T_CANCEL_STATUS is null ORDER BY ID DESC ) SELECT T_AMBU_REG_ID,AMB_CAPASITY, T_LATITUDE \"latitude\", T_LONGITUDE \"longitude\",'Amb_MarkerY' \"markImg\" FROM cte_products WHERE t_rank = 1");
        //}

        //using udf
        public DataTable getCleanNeedAmbulanceList(string zone)
        {
            //return Query($"SELECT DISTINCT T44.T_AMBU_REG_ID, t57.T_USER_ID AMB_CAPASITY, GETLAT(t57.T_USER_ID) \"latitude\", GETLONG(t57.T_USER_ID) \"longitude\", 'Amb_MarkerY' \"markImg\" FROM T74057 t57 LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! = 0 LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id WHERE t57.t_zone_code = '{zone}' AND t57.t_role_code = 24");
            //  return Query($"SELECT DISTINCT T44.T_AMBU_REG_ID,t57.T_USER_ID AMB_CAPASITY,GETLAT(t57.T_USER_ID) \"latitude\",GETLONG(t57.T_USER_ID) \"longitude\",'Amb_MarkerY' \"markImg\",t17.t_request_id FROM T74057 t57 LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! = 0 LEFT JOIN T74117 T17 ON T57.T_User_Id = T17.T_Req_User_Id LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id WHERE t57.t_zone_code = '{zone}' AND t57.t_role_code = 24 AND T17.T_Final_Flag IS NULL AND T17.T_Type = 11");
            return Query($"SELECT DISTINCT T44.T_AMBU_REG_ID, t57.T_USER_ID AMB_CAPASITY, GETLAT(t57.T_USER_ID) \"latitude\", GETLONG(t57.T_USER_ID) \"longitude\",'Amb_MarkerY' \"markImg\", t41.t_request_id FROM T74057 t57 LEFT JOIN T74041 t41 ON t57.t_user_id = t41.t_user_id LEFT JOIN t74015 t15 ON t57.T_EMP_ID = t15.T_EMP_ID LEFT JOIN t74014 t14 ON t14.T_AMBU_REG_ID = t15.T_AMBU_REG_ID LEFT JOIN t74047 t47 ON t47.T_AMBU_TYPE_ID = t14.T_AMB_TYPE_ID LEFT JOIN T74044 T44 ON t44.T_AMBU_REG_ID = t15.T_AMBU_REG_ID AND t44.T_AMBU_REG_ID! =0 WHERE t57.t_zone_code = '{zone}' AND t57.t_role_code =24 AND t41.T_TEAM_ARVL_STN IS NOT NULL AND t41.T_DISCH_STATUS IS NULL AND t41.T_RDY_FOR_NXT_PAT IS NULL AND t41.T_ACCEPT_STATUS IS NOT NULL AND t41.T_CANCEL_STATUS IS NULL");

        }


        //----------------------Save  & update T74207 MOH Data------------
        public string Insert_T74207(T74207 t07, T74041 t74041, T74046 t74046, string type,string user,string lang)
        {
            bool res = false;
            string msg = "";
            string cnchk = "";
            bool saveOrUpdate = false;
            bool update41 = false;
            bool update46 = false;
            string date = Query($"select to_char( LOCALTIMESTAMP ,'yyyy-MM-dd') time from dual").Rows[0]["time"]
                .ToString();
            string time = Query($"select to_char( LOCALTIMESTAMP ,'HH24:MI:SS') time from dual").Rows[0]["time"]
                .ToString();

            string tarikZamam = "";

            if(string.IsNullOrEmpty(t07.T_CAGRI_TARIH_ZAMAN) && string.IsNullOrEmpty(t07.T_KAYIT_TARIH_ZAMAN))
                tarikZamam= date + "T" + time;

            else if (t07.T_KAYIT_TARIH_ZAMAN != null)
            {
                tarikZamam = t07.T_KAYIT_TARIH_ZAMAN;
            }
            else
            {
                tarikZamam = t07.T_CAGRI_TARIH_ZAMAN;
            }

            string team = null;

            if (t74041.T_AMBU_REG_ID != null)
            {
                DataTable dtTeam =
                    Query($"select T_TEAM_CODE from t74044 where t74044.T_AMBU_REG_ID={t74041.T_AMBU_REG_ID}");

                if (dtTeam.Rows.Count > 0)
                {
                    team = dtTeam.Rows[0]["T_TEAM_CODE"].ToString();
                }
                else
                {
                    team = null;
                }

            }
            DataTable dtCheck = Query($"SELECT T_PROTOCAL_NO FROM T74207 WHERE T_PROTOCAL_NO={t07.T_PROTOCAL_NO}");

            // Initialize data----------------

            BeginTransaction();
            if (type == "")
            {
                if (dtCheck.Rows.Count > 0)
                {
                   // msg = "Protocal No already exists.";
                    msg = common.GetSingleMsg(lang, "S0058");
                }
                else
                {
                    //t07.T_ENTRY_TIME = dateTime();
                    t07.T_KAYIT_TARIH_ZAMAN = dateTime() + "T" + time;

                   


                    saveOrUpdate =
                        Command(
                            $"INSERT INTO T74207 (T_PROTOCAL_NO,T_KAYIT_PROTOKOL,T_PROTOKOL_ACIKLAMA, T_OLAY_NO, T_EKIP_NO,T_TELEFON_NO,T_CAGRI_YAPAN_KISI,T_CAGRI_YAPAN_TIPI,T_CAGRI_NEDENI, T_ADRES,T_BULUSMA_NOKTASI,T_KAYIT_TARIH_ZAMAN,T_CAGRI_TARIH_ZAMAN,T_KAYIT_YAPAN,T_ONAYLAYAN,T_ACIKLAMA,T_CALL_LATITUDE_Y,T_CALL_LONGTITUDE_X,T_IL,T_ILCE,T_MAHALLE,T_SOKAK_ADI,T_KAPI_NO,T_SES_KAYIT,T_ENTRY_TIME,T_ENTRY_USER,T_ASSIGN_TEAM_FLAG,T_SEND_FINAL_FLAG,T_USER_ID,T_VAKA_ADRESI,T_ARAYANIN_ADI_SOYADI) VALUES ({t07.T_PROTOCAL_NO},{t07.T_KAYIT_PROTOKOL}, '{t07.T_PROTOKOL_ACIKLAMA}',{t07.T_OLAY_NO}, '{team}', '{t07.T_TELEFON_NO}', '{t07.T_CAGRI_YAPAN_KISI}', '{t07.T_CAGRI_YAPAN_TIPI}', '{t07.T_CAGRI_NEDENI}', '{t07.T_ADRES}', '{t07.T_BULUSMA_NOKTASI}', '{tarikZamam}', '{tarikZamam}', '{t07.T_KAYIT_YAPAN}','{t07.T_ONAYLAYAN}','{t07.T_ACIKLAMA}','{t74041.T_LATITUDE}', '{t74041.T_LONGITUDE}', '{t07.T_IL}', '{t07.T_ILCE}', '{t07.T_MAHALLE}','{t07.T_SOKAK_ADI}','{t07.T_KAPI_NO}',{t07.T_SES_KAYIT}, LOCALTIMESTAMP, '{user}', '{t07.T_ASSIGN_TEAM_FLAG}', '{t07.T_SEND_FINAL_FLAG}', '{user}', '{t07.T_VAKA_ADRESI}','{t07.T_ARAYANIN_ADI_SOYADI}')");


                }
            }
            else if (type == "u")
            {

                saveOrUpdate = Command($"UPDATE T74207 SET  T_KAYIT_PROTOKOL={t07.T_KAYIT_PROTOKOL} , T_PROTOKOL_ACIKLAMA='{t07.T_PROTOKOL_ACIKLAMA}', T_OLAY_NO={t07.T_OLAY_NO}, T_EKIP_NO='{team}', T_TELEFON_NO='{t07.T_TELEFON_NO}', T_CAGRI_YAPAN_KISI ='{t07.T_CAGRI_YAPAN_KISI}',T_CAGRI_YAPAN_TIPI='{t07.T_CAGRI_YAPAN_TIPI}',T_CAGRI_NEDENI='{t07.T_CAGRI_NEDENI}', T_ADRES='{t07.T_ADRES}', T_BULUSMA_NOKTASI = '{t07.T_BULUSMA_NOKTASI}',T_KAYIT_YAPAN='{t07.T_KAYIT_YAPAN}',T_ONAYLAYAN='{t07.T_ONAYLAYAN}',T_ACIKLAMA='{t07.T_ACIKLAMA}',T_CALL_LATITUDE_Y='{t74041.T_LATITUDE}',T_CALL_LONGTITUDE_X='{t74041.T_LONGITUDE}',T_IL='{t07.T_IL}',T_ILCE='{t07.T_ILCE}',T_MAHALLE='{t07.T_MAHALLE}',T_SOKAK_ADI='{t07.T_SOKAK_ADI}',T_KAPI_NO='{t07.T_KAPI_NO}',T_SES_KAYIT={t07.T_SES_KAYIT}, T_UPDATE_TIME='{t07.T_ENTRY_TIME}', T_UPDATE_USER='{t07.T_ENTRY_USER}' WHERE T_PROTOCAL_NO={t07.T_PROTOCAL_NO}");

                //saveOrUpdate = true;
            }
            else if (type == "GeneratCancelRequest")
            {
                bool in41 = false;
                bool up46 = false;
                bool up207 = false;

                if (t07.T_TYPE == "11")
                {
                     Command($"UPDATE T74117 SET T_ACPT_USER_ID='{user}',T_ACPT_DATE= LOCALTIMESTAMP ,T_STATUS ='1' WHERE T_ID={t07.T_ID_17}");
                }
                else
                {
                     Command($"UPDATE T74117 SET T_ACPT_USER_ID='{user}',T_ACPT_DATE= LOCALTIMESTAMP ,T_STATUS ='1',T_FINAL_FLAG='1' WHERE T_ID={t07.T_ID_17}");
                }

                if (team != null)
                {
                    in41 = Command($"INSERT INTO T74041 (T_PROTOCAL_NO,T_USER_ID,T_AMBU_REG_ID,T_PAT_ID, T_AGE, T_CH_COMP, T_PROBLEM, T_PROBLEM_DURATION, T_APPRX_TIME , T_APPRX_DIST , T_LATITUDE , T_LONGITUDE , T_MAP_LOC, T_COMPANY_ID, T_ASSIGN_TIME,T_REF_ID,T_ENTRY_DATE,T_ENTRY_USER,T_REQUEST_DATE,T_REQUEST_TIME) VALUES ({t07.T_PROTOCAL_NO}, '{t74041.T_USER_ID}', '{t74041.T_AMBU_REG_ID}',{t74041.T_PAT_ID},'{t74041.T_AGE}', '{t74041.T_CH_COMP}', '{t74041.T_PROBLEM}', '{t74041.T_PROBLEM_DURATION}', '{t74041.T_APPRX_TIME}', '{t74041.T_APPRX_DIST}', '{t74041.T_LATITUDE}', '{t74041.T_LONGITUDE}','{t74041.T_MAP_LOC}',{t74041.T_COMPANY_ID},LOCALTIMESTAMP,{t74041.T_REQUEST_ID},LOCALTIMESTAMP,'{user}',SYSDATE,LOCALTIMESTAMP)");
                }
                else
                {
                    in41 = Command($"INSERT INTO T74041 (T_PROTOCAL_NO,T_PAT_ID, T_AGE, T_CH_COMP, T_PROBLEM, T_PROBLEM_DURATION, T_APPRX_TIME , T_APPRX_DIST , T_LATITUDE , T_LONGITUDE , T_MAP_LOC, T_COMPANY_ID, T_ASSIGN_TIME,T_REF_ID,T_ENTRY_DATE,T_ENTRY_USER,T_REQUEST_DATE,T_REQUEST_TIME) VALUES ({t07.T_PROTOCAL_NO}, {t74041.T_PAT_ID},'{t74041.T_AGE}', '{t74041.T_CH_COMP}', '{t74041.T_PROBLEM}', '{t74041.T_PROBLEM_DURATION}', '{t74041.T_APPRX_TIME}', '{t74041.T_APPRX_DIST}', '{t74041.T_LATITUDE}', '{t74041.T_LONGITUDE}','{t74041.T_MAP_LOC}',{t74041.T_COMPANY_ID},LOCALTIMESTAMP,{t74041.T_REQUEST_ID}, LOCALTIMESTAMP,'{user}',SYSDATE,LOCALTIMESTAMP)");
                }
                if (in41)
                {
                    up207 = Command($"UPDATE T74207 SET  T_KAYIT_PROTOKOL={t07.T_KAYIT_PROTOKOL} , T_PROTOKOL_ACIKLAMA='{t07.T_PROTOKOL_ACIKLAMA}', T_OLAY_NO={t07.T_OLAY_NO}, T_EKIP_NO='{team}', T_TELEFON_NO='{t07.T_TELEFON_NO}', T_CAGRI_YAPAN_KISI ='{t07.T_CAGRI_YAPAN_KISI}', T_VAKA_ADRESI = '{t07.T_VAKA_ADRESI}', T_CAGRI_YAPAN_TIPI='{t07.T_CAGRI_YAPAN_TIPI}',T_CAGRI_NEDENI='{t07.T_CAGRI_NEDENI}', T_ADRES='{t07.T_ADRES}', T_BULUSMA_NOKTASI = '{t07.T_BULUSMA_NOKTASI}', T_KAYIT_YAPAN='{t07.T_KAYIT_YAPAN}', T_ONAYLAYAN='{t07.T_ONAYLAYAN}', T_ACIKLAMA='{t07.T_ACIKLAMA}', T_ARAYANIN_ADI_SOYADI  ='{t07.T_ARAYANIN_ADI_SOYADI}', T_CALL_LATITUDE_Y='{t74041.T_LATITUDE}', T_CALL_LONGTITUDE_X='{t74041.T_LONGITUDE}', T_IL='{t07.T_IL}',T_ILCE='{t07.T_ILCE}', T_MAHALLE='{t07.T_MAHALLE}',T_SOKAK_ADI='{t07.T_SOKAK_ADI}',T_KAPI_NO='{t07.T_KAPI_NO}',T_SES_KAYIT={t07.T_SES_KAYIT}, T_UPDATE_TIME='{t07.T_ENTRY_TIME}', T_UPDATE_USER='{t07.T_ENTRY_USER}' WHERE T_PROTOCAL_NO={t07.T_PROTOCAL_NO}");

                    up46= Command($"UPDATE T74046 SET T_FIRST_LANG2_NAME ='{t74046.T_FIRST_LANG2_NAME}',T_GFATHER_LANG2_NAME='{t74046.T_GFATHER_LANG2_NAME}',T_SEX_CODE='{t74046.T_SEX_CODE}',T_MOBILE_NO='{t74046.T_MOBILE_NO}',T_ALT_MOBILE_NO='{t74046.T_ALT_MOBILE_NO}',T_NATIONAL_ID='{t74046.T_NATIONAL_ID}',T_IDENTITY_ID='{t74046.T_NATIONAL_ID}' where T_PAT_ID={t74041.T_PAT_ID}");
                    if (up207&& up46)
                    {
                        CommitTransaction();
                        cnchk = "ok";
                        msg = common.GetSingleMsg(lang, "S0012");
                    }
                    else
                    {
                        RollbackTransaction();
                        msg = common.GetSingleMsg(lang, "S0042");//"Data Not Saved";
                    }
                }
                
            }

         if (saveOrUpdate)
            {
                CommitTransaction();

                BeginTransaction();


                
                    if (team != null)
                    {
                        if (t74041.T_REQUEST_ID==0)
                        {
                        update41 =
                            Command($"UPDATE T74041 SET T_AMBU_REG_ID ={t74041.T_AMBU_REG_ID},T_USER_ID ='{t74041.T_USER_ID}',T_APPRX_TIME ='{t74041.T_APPRX_TIME}',T_APPRX_DIST = '{t74041.T_APPRX_DIST}',T_ASSIGN_TIME = LOCALTIMESTAMP,T_AGE='{t74041.T_AGE}',T_LATITUDE={t74041.T_LATITUDE},T_LONGITUDE={t74041.T_LONGITUDE},T_PROBLEM='{t74041.T_PROBLEM}',T_PROBLEM_DURATION='{t74041.T_PROBLEM_DURATION}' where T_PROTOCAL_NO={t07.T_PROTOCAL_NO}");
                    }
                        else
                        {
                        update41 =
                            Command($"UPDATE T74041 SET T_AMBU_REG_ID ={t74041.T_AMBU_REG_ID},T_USER_ID ='{t74041.T_USER_ID}',T_APPRX_TIME ='{t74041.T_APPRX_TIME}',T_APPRX_DIST = '{t74041.T_APPRX_DIST}',T_ASSIGN_TIME = LOCALTIMESTAMP,T_AGE='{t74041.T_AGE}',T_LATITUDE={t74041.T_LATITUDE},T_LONGITUDE={t74041.T_LONGITUDE},T_PROBLEM='{t74041.T_PROBLEM}',T_PROBLEM_DURATION='{t74041.T_PROBLEM_DURATION}' where T_PROTOCAL_NO={t07.T_PROTOCAL_NO} AND T_REQUEST_ID={t74041.T_REQUEST_ID}");
                    }

                        
                    }
                    else
                    {
                        if (t74041.T_REQUEST_ID == 0)
                        {
                            update41 =
                                Command($"UPDATE T74041 SET T_AGE='{t74041.T_AGE}',T_LATITUDE='{t74041.T_LATITUDE}',T_LONGITUDE='{t74041.T_LONGITUDE}',T_PROBLEM='{t74041.T_PROBLEM}',T_PROBLEM_DURATION='{t74041.T_PROBLEM_DURATION}' where T_PROTOCAL_NO={t07.T_PROTOCAL_NO}");
                    }
                        else
                        {
                        update41 =
                            Command($"UPDATE T74041 SET T_AGE='{t74041.T_AGE}',T_LATITUDE='{t74041.T_LATITUDE}',T_LONGITUDE='{t74041.T_LONGITUDE}',T_PROBLEM='{t74041.T_PROBLEM}',T_PROBLEM_DURATION='{t74041.T_PROBLEM_DURATION}' where T_PROTOCAL_NO={t07.T_PROTOCAL_NO} AND T_REQUEST_ID={t74041.T_REQUEST_ID}");
                    }
                        
                    }
                DataTable dtPat = Query($"SELECT T_PAT_ID FROM T74041 WHERE T_PROTOCAL_NO='{t07.T_PROTOCAL_NO}'");
                string patId = "";
                if (dtPat.Rows.Count>0)
                {
                    patId = dtPat.Rows[0]["T_PAT_ID"].ToString();
                }

                    update46=
                    Command($"UPDATE T74046 SET T_FIRST_LANG2_NAME ='{t74046.T_FIRST_LANG2_NAME}',T_GFATHER_LANG2_NAME='{t74046.T_GFATHER_LANG2_NAME}',T_SEX_CODE='{t74046.T_SEX_CODE}',T_MOBILE_NO='{t74046.T_MOBILE_NO}',T_ALT_MOBILE_NO='{t74046.T_ALT_MOBILE_NO}',T_NATIONAL_ID='{t74046.T_NATIONAL_ID}',T_IDENTITY_ID='{t74046.T_NATIONAL_ID}' where T_PAT_ID={patId}");

                    if (update41 && update46)
                    {
                        CommitTransaction();
                      //  msg = "Data Saved Successfully";
                    msg = common.GetSingleMsg(lang, "S0012");
                res = true;
                }
                    else
                    {
                        RollbackTransaction();
                        msg = common.GetSingleMsg(lang, "S0042");//"Data Not Saved";
                    res = false;
                }

                
            }
         else if (cnchk=="ok")
            {
             // It is check for Rollback ----(Ruhul)
           }
         else
            {
                RollbackTransaction();

               
            }




            return msg;

        }

        public DataTable getMaxProtocol()
        {
            return Query($"SELECT NVL(MAX(T_PROTOCAL_NO),0)+1 T_PROTOCAL_NO FROM T74207");
        }
        public DataTable getAllAmb(string p_zone)//this method needed to copy for main source
        {
            return Query($"select distinct t26.T_USER_ID,t44.T_AMBU_REG_ID,GETLAT(t26.T_USER_ID) LAT,GETLONG(t26.T_USER_ID) LON from T74026 t26 join T74057 t57 on t26.T_USER_ID = t57.T_USER_ID join T74015 t15 on t57.T_EMP_ID = t15.T_EMP_ID join T74014 t14 on t15.T_AMBU_REG_ID = t14.T_AMBU_REG_ID join T74047 t47 on t14.T_AMB_TYPE_ID = t47.T_AMBU_TYPE_ID JOIN T74044 t44 on t15.T_AMBU_REG_ID = t44.T_AMBU_REG_ID join t74041 t41 on t44.T_AMBU_REG_ID = t41.T_AMBU_REG_ID where t57.T_LOGIN_STATUS = '1' and t57.T_ZONE_CODE = {p_zone} and t57.T_ROLE_CODE = 24 and t41.T_AMBU_REG_ID not in (select t41.T_AMBU_REG_ID from t74041 t41 where t41.T_DISCH_STATUS is null and t41.T_AMBU_REG_ID is not null group by t41.T_AMBU_REG_ID) group by t26.T_USER_ID,t44.T_AMBU_REG_ID order by t26.T_USER_ID");
        }

        //public string acceptcount(string zone, string userid)//this method needed to copy for main source
        //{
        //    return Query($"SELECT COUNT(T_ACCEPT_STATUS) No_of_count FROM T74041 t41 JOIN T74046 t46 ON t41.T_PAT_ID= t46.T_PAT_ID JOIN T74057 t57 ON t41.T_USER_ID = t57.T_USER_ID WHERE t57.T_ZONE_CODE = '{zone}' AND T_ACCEPT_STATUS ='1' AND t41.t_user_id ='{userid}' AND T_ACCEPT_TIME BETWEEN TO_DATE('20-JUN-19', 'DD-MON-YY') AND TO_DATE('20-JUL-19', 'DD-MON-YY')").Rows[0]["No_of_count"].ToString();
        //}
        //public string rejectcount(string zone, string userid)//this method needed to copy for main source
        //{
        //    return Query($"SELECT count(t41.T_CAN_DATE) No_of_count FROM T74041 t41 JOIN T74046 t46 ON t41.T_PAT_ID= t46.T_PAT_ID JOIN T74057 t57 ON t41.T_USER_ID= t57.T_USER_ID JOIN T74101 t01 ON t41.T_CANCEL_REASON = TO_CHAR(t01.T_CANCEL_REASON_ID) WHERE t57.T_ZONE_CODE = '{zone}' AND t57.T_USER_ID = '{userid}' AND T_CAN_DATE BETWEEN TO_DATE('1-JUN-19', 'DD-MON-YY') AND TO_DATE('20-JUL-19', 'DD-MON-YY')").Rows[0]["No_of_count"].ToString();
        //}

        public string AsignPatientFromPendinglist(int reqId, string user,string operation)
        {
            string sms = "";
            bool update41 = false;
            BeginTransaction();
            if (operation== "Asign")
            {
                update41 = Command($"UPDATE T74041 SET T_ACTV_OPER='{user}' WHERE T_REQUEST_ID={reqId}");
            }
            else if (operation == "Clear")
            {
                update41 = Command($"UPDATE T74041 SET T_ACTV_OPER='' WHERE T_REQUEST_ID={reqId}");
            }
            else if (operation == "ClearAll")
            {
                update41 = Command($"UPDATE T74041 SET T_ACTV_OPER='' WHERE T_ACTV_OPER='{user}'");
            }
            
          
            if (update41==true)
            {
                CommitTransaction();
                sms = "1";
            }
            else
            {
                RollbackTransaction();
            }
            return sms;
        }

        public DataTable GetCancelPatData(string lang, string user)
        {
            return Query($"SELECT T17.T_ID, T17.T_TYPE T_CANCEL_REASON, t41.T_PAT_ID, t41.T_COMPANY_ID, t41.T_CH_COMP, T101.T_Lang2_Name CANCELPROBLEM, TO_CHAR(t17.T_REQ_DATE,'dd-MON-yyyy') T_CAN_DATE, TO_CHAR(t17.T_REQ_DATE,'HH24:MI') T_CAN_TIME, t41.T_ACTV_OPER, t46.T_FIRST_LANG2_NAME ||' ' ||t46.T_FATHER_LANG2_NAME || ' ' ||t46.T_GFATHER_LANG2_NAME PAT_NAME, t46.T_FATHER_LANG2_NAME , t46.T_GFATHER_LANG2_NAME , t46.T_FIRST_LANG2_NAME , t41.T_LATITUDE , t41.T_LONGITUDE , t46.T_MOBILE_NO, t46.T_ENTRY_DATE , t46.T_ALT_MOBILE_NO , t41.T_AGE , t46.T_SEX_CODE , t06.T_LANG2_NAME GENDER , t46.T_NATIONAL_ID , t41.T_PROBLEM , t41.T_PROBLEM_DURATION , TO_CHAR(t41.T_REQUEST_TIME,'dd-mm-yyyy') REQUEST_DATE , TO_CHAR(t41.T_REQUEST_TIME,'HH24:MI')REQUEST_TIME, t41.T_MAP_LOC, t07.T_PROTOCAL_NO, t07.T_KAYIT_PROTOKOL, t07.T_PROTOKOL_ACIKLAMA, t07.T_OLAY_NO, t07.T_EKIP_NO, t07.T_TELEFON_NO, t07.T_CAGRI_YAPAN_KISI, t07.T_CAGRI_YAPAN_TIPI, t07.T_CAGRI_NEDENI, t07.T_ADRES, t07.T_BULUSMA_NOKTASI, t07.T_KAYIT_TARIH_ZAMAN, t07.T_CAGRI_TARIH_ZAMAN, t07.T_KAYIT_YAPAN, t07.T_ONAYLAYAN, t07.T_ACIKLAMA, t07.T_ID, t07.T_CALL_LATITUDE_Y, t07.T_CALL_LONGTITUDE_X, t07.T_IL, t07.T_ILCE, t07.T_MAHALLE, t07.T_SOKAK_ADI, t07.T_KAPI_NO, t07.T_SES_KAYIT, t07.T_VAKA_ADRESI,T_ARAYANIN_ADI_SOYADI, t17. T_REQ_USER_ID T_User_Id, T41.T_Request_Id, T41.T_CAN_DATE T_CAN_DATE_TIME FROM T74117 T17 LEFT JOIN T74041 T41 ON T41.T_Request_Id =t17.T_Request_Id LEFT JOIN T74046 t46 ON T41.T_PAT_ID = t46.T_PAT_ID LEFT JOIN t74050 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE LEFT JOIN T74207 t07 ON T41.T_PROTOCAL_NO = t07.T_PROTOCAL_NO LEFT JOIN T74101 t101 ON T17.T_TYPE =TO_CHAR( t101.T_CANCEL_REASON_ID) WHERE T17.T_FINAL_FLAG IS NULL AND T17.T_ACPT_USER_ID IS NULL");
            // return Query($"select t41_New.T_CANCEL_REASON,t41_New.T_PAT_ID,t41_New.T_COMPANY_ID,t41_New.T_CH_COMP, T101.T_Lang{lang}_Name CANCELPROBLEM, TO_CHAR(t41_New.T_CAN_DATE,'dd-MON-yyyy') T_CAN_DATE, TO_CHAR(t41_New.T_CAN_DATE,'HH24:MI') T_CAN_TIME, t41_New.T_ACTV_OPER, t46.T_FIRST_LANG{lang}_NAME ||' ' ||t46.T_FATHER_LANG{lang}_NAME || ' ' ||t46.T_GFATHER_LANG{lang}_NAME PAT_NAME, t46.T_FATHER_LANG{lang}_NAME , t46.T_GFATHER_LANG{lang}_NAME , t46.T_FIRST_LANG{lang}_NAME , t41_New.T_LATITUDE , t41_New.T_LONGITUDE , t46.T_MOBILE_NO, t46.T_ENTRY_DATE , t46.T_ALT_MOBILE_NO , t41_New.T_AGE , t46.T_SEX_CODE , t06.T_LANG{lang}_NAME GENDER , t46.T_NATIONAL_ID , t41_New.T_PROBLEM , t41_New.T_PROBLEM_DURATION , TO_CHAR(t41_New.T_REQUEST_TIME,'dd-mm-yyyy') REQUEST_DATE , TO_CHAR(t41_New.T_REQUEST_TIME,'HH24:MI')REQUEST_TIME, t41_New.T_MAP_LOC, t07.T_PROTOCAL_NO, t07.T_KAYIT_PROTOKOL, t07.T_PROTOKOL_ACIKLAMA, t07.T_OLAY_NO, t07.T_EKIP_NO, t07.T_TELEFON_NO, t07.T_CAGRI_YAPAN_KISI, t07.T_CAGRI_YAPAN_TIPI, t07.T_CAGRI_NEDENI, t07.T_ADRES, t07.T_BULUSMA_NOKTASI, t07.T_KAYIT_TARIH_ZAMAN, t07.T_CAGRI_TARIH_ZAMAN, t07.T_KAYIT_YAPAN, t07.T_ONAYLAYAN, t07.T_ACIKLAMA, t07.T_ID, t07.T_CALL_LATITUDE_Y, t07.T_CALL_LONGTITUDE_X, t07.T_IL, t07.T_ILCE, t07.T_MAHALLE, t07.T_SOKAK_ADI, t07.T_KAPI_NO, t07.T_SES_KAYIT, t41_old.* from (SELECT DISTINCT t41.T_User_Id ,max(t41.T_Request_Id) OVER (PARTITION BY T_User_Id) AS T_Request_Id ,first_value(t41.T_CAN_DATE) OVER (PARTITION BY t41.T_User_Id ORDER BY t41.T_Request_Id DESC) AS T_CAN_DATE_TIME FROM t74041 t41 WHERE T_USER_ID is not null order by T_User_Id desc)t41_old JOIN T74041 t41_New on t41_old.T_Request_Id =t41_New.T_Request_Id JOIN T74046 t46 ON t41_New.T_PAT_ID = t46.T_PAT_ID LEFT JOIN t74050 t06 ON t46.T_SEX_CODE = t06.T_SEX_CODE LEFT JOIN T74207 t07 ON t41_New.T_PROTOCAL_NO = t07.T_PROTOCAL_NO LEFT JOIN T74101 t101 ON t41_New.T_CANCEL_REASON=TO_CHAR( t101.T_CANCEL_REASON_ID) where t41_old.T_CAN_DATE_TIME is not null AND t41_New.T_APRVL_FLAG IS NULL");
        }

        public string SaveCancelConfirmData(string user,string cnfm, string reqId,string cnlRsn,int Tid)
        {
            string sms = "";
            string chk = "";
            bool update41 = false;
            DateTime dat= common.dateTime();
            BeginTransaction();
            if (cnfm== "Cancel_Confirm")
            {
                //update41 = Command($"UPDATE T74041 SET T_APRVL_USER='{user}',T_APRVL_DATE= LOCALTIMESTAMP ,T_APRVL_FLAG ='1' WHERE T_REQUEST_ID={reqId}");
                if (cnlRsn=="11")
                {
                    update41 = Command($"UPDATE T74117 SET T_ACPT_USER_ID='{user}',T_ACPT_DATE= LOCALTIMESTAMP ,T_STATUS ='1' WHERE T_ID={Tid}");
                }
                else
                {
                    update41 = Command($"UPDATE T74117 SET T_ACPT_USER_ID='{user}',T_ACPT_DATE= LOCALTIMESTAMP ,T_STATUS ='1',T_FINAL_FLAG='1' WHERE T_ID={Tid}");
                }
                
                
            }
            else if (cnfm == "Cancel_Not_Confirm")
            {
                //T_DISCHARGE_TIME= {dat}, T_DISCHARGE_DATE=SYSDATE,
                if (reqId !=null)
                {
                    update41 = Command($"UPDATE T74041 SET T_DISCH_STATUS='1', T_EVENT_FLAG ='11', T_RDY_FOR_NXT_PAT=LOCALTIMESTAMP  WHERE T_REQUEST_ID={reqId}");
                }
                else
                {
                    
                }
               
                Command($"UPDATE T74117 SET T_ACPT_USER_ID='{user}',T_ACPT_DATE= LOCALTIMESTAMP ,T_STATUS='1',T_FINAL_FLAG ='1' WHERE T_ID={Tid}");
                //  bool up26 = Command($"INSERT INTO T74026 (T_USER_ID,T_ENTRY_TIME,T_REQUEST_ID,T_EVENT_ID) VALUES ('{user}',SYSDATE, {reqId},'32')");

                if (update41)
                {
                    chk = "1";
                }
                else
                {
                    chk = "2";
                    update41 = true;
                    sms = "1";
                }
                if (chk == "1")
                {
                    var getData = Query($"SELECT T_REQUEST_ID ,T_PROTOCAL_NO,T_USER_ID,T_AMBU_REG_ID, T_PAT_ID , T_AGE , T_CH_COMP , TO_CHAR(T_REQUEST_DATE,'dd-MON-yy')T_REQUEST_DATE , TO_CHAR(T_REQUEST_TIME, 'dd-MON-yy HH24:MI')T_REQUEST_TIME , T_PROBLEM , T_PROBLEM_DURATION , T_APPRX_TIME , T_APPRX_DIST , T_LATITUDE , T_LONGITUDE , T_MAP_LOC,T_COMPANY_ID FROM T74041 WHERE T_REQUEST_ID = {reqId}");
                    for (int i = 0; i < getData.Rows.Count; i++)
                    {
                        Command($"INSERT INTO T74041 (T_PROTOCAL_NO,T_USER_ID,T_AMBU_REG_ID,T_PAT_ID, T_AGE, T_CH_COMP, T_REQUEST_DATE, T_REQUEST_TIME, T_PROBLEM, T_PROBLEM_DURATION, T_APPRX_TIME , T_APPRX_DIST , T_LATITUDE , T_LONGITUDE , T_MAP_LOC,T_COMPANY_ID,T_ASSIGN_TIME,T_REF_ID,T_ENTRY_DATE,T_ENTRY_USER) VALUES ({getData.Rows[i]["T_PROTOCAL_NO"]},'{getData.Rows[i]["T_USER_ID"]}','{getData.Rows[i]["T_AMBU_REG_ID"]}',{getData.Rows[i]["T_PAT_ID"]},'{getData.Rows[i]["T_AGE"]}', '{getData.Rows[i]["T_CH_COMP"]}', SYSDATE,LOCALTIMESTAMP,'{getData.Rows[i]["T_PROBLEM"]}','{getData.Rows[i]["T_PROBLEM_DURATION"]}','{getData.Rows[i]["T_APPRX_TIME"]}','{getData.Rows[i]["T_APPRX_DIST"]}','{getData.Rows[i]["T_LATITUDE"]}','{getData.Rows[i]["T_LONGITUDE"]}','{getData.Rows[i]["T_MAP_LOC"]}',{getData.Rows[i]["T_COMPANY_ID"]},LOCALTIMESTAMP,{getData.Rows[i]["T_REQUEST_ID"]},LOCALTIMESTAMP,'{user}')");
                    }
                   
                }
            }
            else if (cnfm == "Send_to_PendingList")
            {
                var getData = Query($"SELECT T_REQUEST_ID ,T_USER_ID,T_AMBU_REG_ID,T_PROTOCAL_NO, T_PAT_ID , T_AGE , T_CH_COMP , TO_CHAR(T_REQUEST_DATE,'dd-MON-yy')T_REQUEST_DATE , TO_CHAR(T_REQUEST_TIME, 'dd-MON-yy HH24:MI')T_REQUEST_TIME , T_PROBLEM , T_PROBLEM_DURATION , T_APPRX_TIME , T_APPRX_DIST , T_LATITUDE , T_LONGITUDE , T_MAP_LOC,T_COMPANY_ID FROM T74041 WHERE T_REQUEST_ID = {reqId}");
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    update41 = Command($"INSERT INTO T74041 (T_PROTOCAL_NO,T_PAT_ID, T_AGE, T_CH_COMP, T_REQUEST_DATE, T_REQUEST_TIME, T_PROBLEM, T_PROBLEM_DURATION, T_APPRX_TIME , T_APPRX_DIST , T_LATITUDE , T_LONGITUDE , T_MAP_LOC,T_COMPANY_ID,T_ASSIGN_TIME,T_REF_ID,T_ENTRY_DATE,T_ENTRY_USER) VALUES ({getData.Rows[i]["T_PROTOCAL_NO"]},{getData.Rows[i]["T_PAT_ID"]},'{getData.Rows[i]["T_AGE"]}', '{getData.Rows[i]["T_CH_COMP"]}',SYSDATE,LOCALTIMESTAMP,'{getData.Rows[i]["T_PROBLEM"]}','{getData.Rows[i]["T_PROBLEM_DURATION"]}','{getData.Rows[i]["T_APPRX_TIME"]}','{getData.Rows[i]["T_APPRX_DIST"]}','{getData.Rows[i]["T_LATITUDE"]}','{getData.Rows[i]["T_LONGITUDE"]}','{getData.Rows[i]["T_MAP_LOC"]}',{getData.Rows[i]["T_COMPANY_ID"]},LOCALTIMESTAMP,{getData.Rows[i]["T_REQUEST_ID"]}, LOCALTIMESTAMP,'{user}')");
                }
                if (update41)
                {
                    if (cnlRsn == "11")
                    {
                         Command($"UPDATE T74117 SET T_ACPT_USER_ID='{user}',T_ACPT_DATE= LOCALTIMESTAMP ,T_STATUS ='1' WHERE T_ID={Tid}");
                    }
                    else
                    {
                        Command($"UPDATE T74117 SET T_ACPT_USER_ID='{user}',T_ACPT_DATE= LOCALTIMESTAMP ,T_STATUS ='1',T_FINAL_FLAG='1' WHERE T_ID={Tid}");
                    }
                }
            }

            if (update41)
            {
                CommitTransaction();
            }
            else
            {
                RollbackTransaction();
            }

            return sms;
        }

        public DataTable Result(int lang)
        {
            return Query($"SELECT T_ID,T_CODE_VALUE,T_LANG{lang}_NAME T_LANG1_NAME FROM T74204");
        }
    }
}