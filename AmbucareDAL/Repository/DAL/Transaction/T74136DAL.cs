using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class T74136DAL : CommonDAL
    {
        public string maxUserId()
        {
            string maxUser = "";
            maxUser = Query($"select LPAD((max(TO_NUMBER(T_USER_ID))+1),5,'0') T_USER_ID from T74057 where length(T_USER_ID)=5").Rows[0]["T_USER_ID"].ToString();
            return maxUser;

        }
        public DataTable getEmpList(string lang, string rolcode, string user)
        {
            DataTable query = new DataTable();
            query = Query(
                $" SELECT T04.T_COMPANY_CODE, T04.T_EMP_ID, T04.T_FIRST_LANG{lang}_NAME ||' ' ||T04.T_LAST_LANG2_NAME EMP_NAME , T05.T_LANG{lang}_NAME EMP_TYPE,  T05.T_EMP_TYP_ID TYPE_ID,T71.T_SHORT_NAME FROM T74004 T04 INNER JOIN T74005 T05 ON T04.T_EMP_TYP_ID =T05.T_EMP_TYP_ID INNER JOIN T74071 T71 ON T71.T_EMP_TYP_ID =T04.T_EMP_TYP_ID inner join t74082 t82 on T71.T_ROLE_CODE =T82.T_ROLE_DET_CODE LEFT JOIN T74057 T57 ON T57.T_EMP_ID=T04.T_EMP_ID WHERE T57.T_EMP_ID IS NULL AND T04.T_ENTRY_USER='{user}' and T82.T_ROLE_CODE={rolcode} ORDER BY T04.T_EMP_TYP_ID");


          //  DataTable query = Query($"SELECT T04.T_COMPANY_CODE,T04.T_EMP_ID,T04.T_FIRST_LANG{lang}_NAME||' '||T04.T_FATHER_LANG{lang}_NAME||' '||T04.T_GFATHER_LANG{lang}_NAME||' '||T04.T_FAMILY_LANG{lang}_NAME EMP_NAME ,T05.T_LANG{lang}_NAME EMP_TYPE FROM T74004 T04 INNER JOIN T74005 T05 ON T04.T_EMP_TYP_ID=T05.T_EMP_TYP_ID WHERE T04.T_EMP_ID NOT IN (SELECT DISTINCT T_EMP_ID FROM T74057) ");
            return query;

        }
        public DataTable getRole(string lang, string rolcode)
        {
            DataTable query = new DataTable();
            query = Query(
                $"SELECT T71.T_ROLE_CODE T_ROLE_CODE,T71.T_LANG{lang}_NAME NAME,T71.T_SHORT_NAME FROM T74071 T71 INNER JOIN T74082 T82 ON T82.T_ROLE_DET_CODE = T71.T_ROLE_CODE WHERE T82.T_ROLE_CODE = {rolcode}");
            
           
            return query;

        }
        public DataTable getZone(string lang)
        {
            DataTable query = Query($"SELECT T_ZONE_CODE,T_LANG{lang}_NAME T_ZONE_NAME FROM T02064 ORDER BY T_ZONE_CODE");
            return query;

        }


        public DataTable CheckUserId(string userId)
        {
            var query = Query($"SELECT T_USER_ID FROM T74057 WHERE T_USER_ID='{userId}'");
            return query;
        }

        public DataTable GetGridData2(string lang)
        {
            return Query($"SELECT T74057.T_USER_ID,T74004.T_FIRST_LANG{lang}_NAME||' '||T74004.T_FATHER_LANG{lang}_NAME||' '||T74004.T_GFATHER_LANG{lang}_NAME EMP_NAME, T74057.T_PWD,T74057.T_ACTIVE_FLAG,T74057.T_EMP_ID,T74057.T_COMPANY_ID,T74057.T_ROLE_CODE, T74071.T_LANG{lang}_NAME ROLE_NAME FROM T74057 INNER JOIN T74071 ON T74057.T_ROLE_CODE=T74071.T_ROLE_CODE INNER JOIN T74004 ON T74057.T_EMP_ID=T74004.T_EMP_ID");
        }
        public DataTable GetGridData(string lang,string user,string zone)
        {
            //return Query($"SELECT T74057.T_USER_ID,T74004.T_FIRST_LANG{lang}_NAME||' '||T74004.T_FATHER_LANG{lang}_NAME||' ' ||T74004.T_GFATHER_LANG{lang}_NAME EMP_NAME, T74057.T_PWD,T74057.T_ACTIVE_FLAG, T74057.T_EMP_ID, T74057.T_COMPANY_ID, T74057.T_ROLE_CODE,  T74071.T_LANG{lang}_NAME ROLE_NAME, T74057.T_ZONE_CODE,T02064.T_LANG{lang}_NAME T_ZONE_NAME FROM T74057 INNER JOIN  T74071 ON T74057.T_ROLE_CODE=T74071.T_ROLE_CODE INNER JOIN T74004 ON T74057.T_EMP_ID=T74004.T_EMP_ID LEFT JOIN T02064 ON T74057.T_ZONE_CODE = T02064.T_ZONE_CODE");
            return Query(
                $"SELECT DISTINCT T57.T_USER_ID, T04.T_FIRST_LANG{lang}_NAME ||' ' ||T04.T_LAST_LANG2_NAME EMP_NAME, T57.T_PWD, T57.T_ACTIVE_FLAG, T57.T_EMP_ID, T57.T_COMPANY_ID, T57.T_ROLE_CODE, T71.T_LANG2_NAME ROLE_NAME, T57.T_ZONE_CODE, T64.T_LANG2_NAME T_ZONE_NAME, T26.T_LATITUDE, T26.T_LONGITUDE FROM T74026 T26 LEFT JOIN T74057 T57 ON T26.T_USER_ID = T57.T_USER_ID INNER JOIN T74071 T71 ON T57.T_ROLE_CODE=T71.T_ROLE_CODE INNER JOIN T74004 T04 ON T57.T_EMP_ID=T04.T_EMP_ID LEFT JOIN T02064 T64 ON T57.T_ZONE_CODE = T64.T_ZONE_CODE WHERE t26.id=(select max(id) from t74026 where t74026.t_user_id=t26.T_USER_ID) AND T57.T_ENTRY_USER = '{user}' ");
        }

        public bool InsertT74026(string userid, string lat, string lng)
        {
            var maxid = Query($"select max(ID) + 1 ID from T74026").Rows[0]["ID"].ToString();
            Command($"INSERT INTO T74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE) VALUES ('{maxid}', '{userid}',SYSDATE, '{lat}', '{lng}')");
            return true;
        }

        public DataTable getMaxValue(string user, string type)
        {
            return Query(
                $"SELECT MAX(T_1.ROWN+1) MAX_1  FROM (SELECT ROWNUM ROWN, T74057.T_USER_ID FROM T74057 INNER JOIN T74004 ON T74057.T_EMP_ID = T74004.T_EMP_ID where T74004.T_EMP_TYP_ID = {type})T_1");
            //return Query($"SELECT USER_ID ||''||MAX_1 USER_NAM FROM( SELECT T_2.MAX_1, (SELECT SUBSTR(T74057.T_USER_ID,1,4)T_USER_ID FROM T74057 INNER JOIN T74004 ON T74057.T_EMP_ID = T74004.T_EMP_ID where T74004.T_EMP_TYP_ID = 23 AND ROWNUM = 1 )USER_ID FROM( SELECT MAX(T_1.ROWN+1) MAX_1 FROM (SELECT ROWNUM ROWN, T74057.T_USER_ID FROM T74057 INNER JOIN T74004 ON T74057.T_EMP_ID = T74004.T_EMP_ID where T74004.T_EMP_TYP_ID = {type})T_1 )T_2 )T_3");
        }
    }
}