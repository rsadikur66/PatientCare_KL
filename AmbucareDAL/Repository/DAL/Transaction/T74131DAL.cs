using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class T74131DAL:CommonDAL
    {
        CommonDAL common = new CommonDAL();
        public DataTable GetSite(string P_USER_ID)
        {
            return Query("select t96.T_SITE_CODE from t74096 t96 inner join T74015 t15 on t15.T_AMBU_REG_ID = t96.T_AMBU_REG_ID inner join T74041 t41 on t41.T_AMBU_REG_ID = t96.T_AMBU_REG_ID inner join t74004 t04 on t15.T_EMP_ID = t04.T_EMP_ID inner join t74005 t05 on t04.T_EMP_TYP_ID = t05.T_EMP_TYP_ID inner join t74057 t57 on t57.T_EMP_ID = t04.T_EMP_ID inner join t02040 t40 on t41.T_PROB_TYPE_ID = t40.T_SPCLTY_ID where t96.T_AMBU_REG_ID is not null and t96.T_SITE_CODE is not null and t57.T_USER_ID = '" + P_USER_ID + "' and t05.T_EMP_TYP_ID in (21, 2) and t41.T_DISCH_STATUS IS NOT NULL group by  t96.T_SITE_CODE");
            //return Query("select T_SITE_CODE from T74057 where T_USER_ID = '" + P_USER_ID + "'");
        }
        public DataTable GetPatInfo(string P_USER_ID)
        {
            return Query($"SELECT T41.T_REF_DOC_CODE,T41.T_REF_DOC_TIME,(SELECT T_LANG2_NAME FROM  T02065 WHERE T_SITE_CODE=T41.T_REF_DOC_CODE) REFER_HOS,T65.T_LANG2_NAME HOSNAME,T65.T_SITE_CODE HOS_ID,fn_AMB_REG_ID('{P_USER_ID}') T_AMBU_REG_ID,T41.T_REQUEST_ID,nvl(T46.T_FIRST_LANG1_NAME,T46.T_FIRST_LANG2_NAME) ||' '|| nvl(T46.T_FATHER_LANG1_NAME,T46.T_FATHER_LANG2_NAME) || ' '|| nvl(T46.T_GFATHER_LANG1_NAME,T46.T_GFATHER_LANG2_NAME) || ' '|| nvl(T46.T_FAMILY_LANG1_NAME,T46.T_FAMILY_LANG2_NAME) PATNAME/*,DECODE(T46.T_BIRTH_DATE,'01-JAN-01','',TRUNC(MONTHS_BETWEEN(SysDate, T46.T_BIRTH_DATE) / 12, 0)  || ' Yrs '  || TRUNC(MOD(MONTHS_BETWEEN(SysDate, T46.T_BIRTH_DATE), 12), 0)  || ' Months') AGE*/,NVL2(T41.T_AGE,T41.T_AGE || ' Yrs','') AGE,T51.T_LANG2_NAME MARITAL_STATUS,  T03.T_LANG2_NAME NATIONALITY,T50.T_LANG2_NAME GENDER,T59.T_LANG2_NAME RELIGION,T41.T_PROBLEM,T41.T_PROBLEM_DURATION, T41.T_PROB_DETAILS,T40.T_LANG2_NAME PROBLEM_TYPE,T55.T_LANG2_NAME CHIEF_COMPLIENT,T63.T_ICD10_SHORT_DESC2 ICD10,T43.T_HIGHT, T28135.T_LANG2_NAME T_CON_LEVEL, T43.T_BP_SYS, T43.T_BP_DIA, T43.T_TEMP, T43.T_PULS, T43.T_RESP, T43.T_TIME, T43.T_OS SPO2,  T43.T_URINE_TEST RATING, T43.T_TRIAGE_LEVEL TRIAGE_LEVEL ,T43.T_ENTRY_DATE FROM T74041 T41 LEFT JOIN T74046 T46 ON T41.T_PAT_ID=T46.T_PAT_ID LEFT JOIN T74051 T51 ON T46.T_MRTL_STATUS = T51.T_MRTL_STATUS_CODE LEFT JOIN T02003 T03 ON T46.T_NTNLTY_ID = T03.T_NTNLTY_ID LEFT JOIN T74050 T50 ON T46.T_SEX_CODE = T50.T_SEX_CODE LEFT JOIN T74059 T59 ON T46.T_RLGN_CODE = T59.T_RLGN_CODE LEFT JOIN T74043 T43 ON T41.T_REQUEST_ID=T43.T_REQUEST_ID LEFT JOIN T02040 T40 ON T41.T_PROB_TYPE_ID=T40.T_SPCLTY_CODE LEFT JOIN T74055 T55 ON T41.T_CH_COMP=T55.T_CH_COMP LEFT JOIN T06301 T63 ON T41.T_ICD10_CODE=T63.T_ICD10_CODE LEFT JOIN T74015 T15 ON T15.T_AMBU_REG_ID=T41.T_AMBU_REG_ID LEFT JOIN T74057 T57 ON T57.T_EMP_ID=T15.T_EMP_ID LEFT JOIN T28135 ON T43.T_CONCIUS_LEVEL =  T28135.T_CONS_LEVEL LEFT JOIN T02065 T65 ON T41.T_SITE_DIS_CODE=T65.T_SITE_CODE WHERE T57.T_USER_ID='{P_USER_ID}' AND T41.T_DISCH_STATUS IS NULL");
        }
        public DataTable GetPreviousMedicine(int requId)
        {
            //return Query($"SELECT t_index, 'DOSE' ||T_INDEX T_DOSE, ITEM_CODE, GEN_DESC, T_ISSUE_ID, T_ENTRY_TIME, round(T_QUANTITY*tt) T_QTY FROM (SELECT DENSE_RANK() OVER(Order By t74036.T_ISSUE_ID) AS t_index, V30001.ITEM_CODE, V30001.GEN_DESC, t74036.T_ISSUE_ID, t74036.T_ENTRY_TIME, SUM(t74037.T_QUANTITY) T_QUANTITY, (select T_QTY from T74003 where T_ITEM_UM_ID = t74037.T_UOM_ID) tt FROM t74036 INNER JOIN t74037 ON t74036.T_ISSUE_ID = t74037.T_ISSUE_ID INNER JOIN T74074 ON t74036.T_ISSUE_ID = T74074.T_ISSUE_ID INNER JOIN V30001 ON t74037.T_ITEM_ID = V30001.ITEM_CODE WHERE t74036.T_REQUEST_ID = {requId} GROUP BY V30001.ITEM_CODE, V30001.GEN_DESC, t74036.T_ENTRY_TIME,t74037.T_UOM_ID, t74036.T_ISSUE_ID ORDER BY t74036.T_ISSUE_ID DESC )");

            return Query(
                $"SELECT t_index, 'DOSE' ||T_INDEX T_DOSE, ITEM_CODE, GEN_DESC, T_ISSUE_ID, T_ENTRY_TIME, ROUND(T_QUANTITY*tt) T_QTY FROM (SELECT DENSE_RANK() OVER(Order By t74036.T_ISSUE_ID) AS t_index, T74114.T_ITEM_CODE ITEM_CODE, T74113.T_LANG2_NAME GEN_DESC, t74036.T_ISSUE_ID, t74036.T_ENTRY_TIME, SUM(t74037.T_QUANTITY) T_QUANTITY, (SELECT T_QTY FROM T74003 WHERE T_ITEM_UM_ID = t74037.T_UOM_ID ) tt FROM t74036 INNER JOIN t74037 ON t74036.T_ISSUE_ID = t74037.T_ISSUE_ID INNER JOIN T74074 ON t74036.T_ISSUE_ID = T74074.T_ISSUE_ID INNER JOIN T74114 ON t74037.T_ITEM_ID = T74114.T_ITEM_CODE INNER JOIN T74113 on T74113.T_Gen_Code=T74114.T_Gen_Code WHERE t74036.T_REQUEST_ID = {requId} GROUP BY T74114.T_ITEM_CODE, T74113.T_LANG2_NAME, t74036.T_ENTRY_TIME, t74037.T_UOM_ID, t74036.T_ISSUE_ID ORDER BY t74036.T_ISSUE_ID DESC)");

        }
        public DataTable GetChatHistory(int T_REQUEST_ID,string T_SENDER_ID,string T_RECIEVER_ID)
        {
            //return Query($"SELECT * FROM T74098 WHERE T_REQUEST_ID={T_REQUEST_ID} AND (T_SENDER_ID='{T_SENDER_ID}' OR T_SENDER_ID   = '{T_RECIEVER_ID}') AND (T_RECIEVER_ID='{T_SENDER_ID}' OR T_RECIEVER_ID = '{T_RECIEVER_ID}') order by T_AUTO_ID");
            //return Query($"SELECT T12.T_AUTO_ID,T12.T_USER_ID T_SENDER_ID,TO_CHAR(T57.T_EMP_ID )T_RECIEVER_ID,'Pat' T_SENDER_TYPE,T12.T_TEXT, T12.T_TIME, 0 T_REF_ID, NULL T_FILE_LOC, T12.T_REQUEST_ID,  NULL T_RECEIVED_FLAG ,T12.T_USER_ID FROM T74112 T12 LEFT JOIN T74041 T41 ON T41.T_REQUEST_ID=T12.T_REQUEST_ID AND T41.T_CHAT_DOC_ID IS NOT NULL LEFT JOIN T74057 T57 ON T57.T_USER_ID=T41.T_CHAT_DOC_ID WHERE T12.T_REQUEST_ID={T_REQUEST_ID} UNION  SELECT T74098.*,CASE WHEN T74098.T_SENDER_TYPE='Doc' THEN (SELECT T74057.T_USER_ID  FROM T74057 WHERE T74057.T_EMP_ID=T74098.T_SENDER_ID) ELSE T74098.T_SENDER_ID END T_USER_ID FROM T74098 WHERE T74098.T_REQUEST_ID={T_REQUEST_ID} AND (T74098.T_SENDER_ID='{T_SENDER_ID}' OR T74098.T_SENDER_ID   = '{T_RECIEVER_ID}') AND (T74098.T_RECIEVER_ID='{T_SENDER_ID}' OR T74098.T_RECIEVER_ID = '{T_RECIEVER_ID}') order by T_AUTO_ID");
            //------------------Previous was ok, this one has multiple doc history
            return Query($"SELECT L.*,T04.T_FIRST_LANG2_NAME ||' '||T04.T_FATHER_LANG2_NAME||' '||T04.T_GFATHER_LANG2_NAME U_NAME FROM (SELECT T12.T_AUTO_ID,T12.T_USER_ID T_SENDER_ID,TO_CHAR(T57.T_EMP_ID )T_RECIEVER_ID,'Pat' T_SENDER_TYPE,T12.T_TEXT, T12.T_TIME, 0 T_REF_ID, NULL T_FILE_LOC, T12.T_REQUEST_ID,  NULL T_RECEIVED_FLAG ,T12.T_USER_ID FROM T74112 T12 LEFT JOIN T74041 T41 ON T41.T_REQUEST_ID=T12.T_REQUEST_ID AND T41.T_CHAT_DOC_ID IS NOT NULL LEFT JOIN T74057 T57 ON T57.T_USER_ID=T41.T_CHAT_DOC_ID WHERE T12.T_REQUEST_ID={T_REQUEST_ID} UNION  SELECT T74098.*,CASE WHEN T74098.T_SENDER_TYPE='Doc' THEN (SELECT T74057.T_USER_ID  FROM T74057 WHERE T74057.T_EMP_ID=T74098.T_SENDER_ID) ELSE T74098.T_SENDER_ID END T_USER_ID FROM T74098 WHERE T74098.T_REQUEST_ID={T_REQUEST_ID}  ) L LEFT JOIN T74057 T57 ON T57.T_USER_ID=L.T_USER_ID LEFT JOIN T74004 T04 ON T04.T_EMP_ID=T57.T_EMP_ID ORDER BY L.T_AUTO_ID");

        }
        public void chatHistory(T74098 t74098)
        {
            t74098.T_AUTO_ID = Int32.Parse(Query($"SELECT TO_NUMBER(NVL(MAX(T_AUTO_ID),0)+1) T_AUTO_ID FROM T74098").Rows[0]["T_AUTO_ID"].ToString());
            t74098.T_REF_ID= Int32.Parse(Query($"SELECT TO_NUMBER(NVL(MAX(T_AUTO_ID),0)) T_AUTO_ID FROM T74098 WHERE T_SENDER_ID='{t74098.T_SENDER_ID}' OR T_SENDER_ID = '{t74098.T_RECIEVER_ID}' AND T_RECIEVER_ID='{t74098.T_SENDER_ID}' OR T_RECIEVER_ID= '{t74098.T_RECIEVER_ID}'").Rows[0]["T_AUTO_ID"].ToString());
            //if (t74098.T_SENDER_TYPE=="Pat")
            //{
            //    t74098.T_SENDER_ID =Query($"SELECT T74041.T_PAT_ID FROM T74057 INNER JOIN T74015 ON T74015.T_EMP_ID = T74057.T_EMP_ID INNER JOIN T74041 ON T74041.T_AMBU_REG_ID = T74015.T_AMBU_REG_ID WHERE T74057.T_USER_ID = '{t74098.T_SENDER_ID}' AND T74041.T_DISCH_STATUS IS NULL").Rows[0]["T_PAT_ID"].ToString();
            //}
            BeginTransaction();
            if (Command($"INSERT INTO T74098(T_AUTO_ID,T_SENDER_ID,T_RECIEVER_ID,T_SENDER_TYPE,T_TEXT,T_TIME,T_REF_ID,T_REQUEST_ID) VALUES ({t74098.T_AUTO_ID},'{t74098.T_SENDER_ID}','{t74098.T_RECIEVER_ID}','{t74098.T_SENDER_TYPE}','{t74098.T_TEXT}',SYSDATE,{t74098.T_REF_ID},{t74098.T_REQUEST_ID})"))
            {
                CommitTransaction();
            }
            else
            {
                    RollbackTransaction();
            }

        }

        public DataTable getUser(string id)
        {
            return Query($"SELECT * FROM T74057 WHERE T_USER_ID='{id}'");
        }
        public void setDoc(string uid)
        {
           
            int req = 0;
            int typ = Int32.Parse(Query($"SELECT T04.T_EMP_TYP_ID FROM T74057 T57 INNER JOIN T74004 T04 ON T57.T_EMP_ID=T04.T_EMP_ID WHERE T57.T_USER_ID='{uid}'").Rows[0]["T_EMP_TYP_ID"].ToString());
            string zone = Query($"SELECT T57.T_ZONE_CODE FROM T74057 T57 INNER JOIN T74004 T04 ON T57.T_EMP_ID=T04.T_EMP_ID WHERE T57.T_USER_ID='{uid}'").Rows[0]["T_ZONE_CODE"].ToString();
            if (typ==2)
            {
                DataTable dt_chkDoc =Query($"SELECT NVL(MAX(T41.T_REQUEST_ID),0) T_REQUEST_ID FROM T74041 T41 WHERE T41.T_DISCH_STATUS IS NULL AND T_CHAT_DOC_ID='{uid}'");
                if (Int32.Parse(dt_chkDoc.Rows[0]["T_REQUEST_ID"].ToString()) == 0)
                {
                    //DataTable dt =Query($"SELECT MAX(T41.T_REQUEST_ID) T_REQUEST_ID FROM T74041 T41 WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_CHAT_FLAG IS NULL");
                    //if (dt.Rows.Count>0)
                    //{
                    /*req = Int32.Parse(Query($"SELECT MAX(T41.T_REQUEST_ID) T_REQUEST_ID FROM T74041 T41 WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_CHAT_FLAG IS NULL").Rows[0]["T_REQUEST_ID"].ToString());*/
                    DataTable dt= Query($"SELECT NVL(MAX(T41.T_REQUEST_ID),0) T_REQUEST_ID FROM T74041 T41 INNER JOIN T74057 T57 ON T57.T_USER_ID=T41.T_USER_ID WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_USER_ID IS NOT NULL AND T41.T_CANCEL_STATUS IS NULL AND T41.T_CHAT_FLAG IS NULL AND T57.T_ZONE_CODE = {zone}");
                    if (dt.Rows.Count>0)
                    {
                        req = Int32.Parse(dt.Rows[0]["T_REQUEST_ID"].ToString());
                    }
                    //}
                   
                }
                else
                {
                    req = Int32.Parse(dt_chkDoc.Rows[0]["T_REQUEST_ID"].ToString());
                }
                
                
                if (req!=0)
                {
                    BeginTransaction();
                    if (Command($"UPDATE T74041 SET T_CHAT_FLAG='1',T_CHAT_DOC_ID='{uid}' WHERE T_REQUEST_ID='{req}'"))
                    {
                        CommitTransaction();
                        DataTable dt1 = Query($"SELECT * FROM T74105 WHERE T_REQUEST_ID='{req}'");
                        if (dt1.Rows.Count == 0)
                        {

                            BeginTransaction();
                            if (Command($"INSERT INTO T74105 (T_REQUEST_ID,T_CHAT_REQ_ID,T_CHAT_REQ_TIME) VALUES ({req},'{uid}',LOCALTIMESTAMP)"))
                            {
                                CommitTransaction();
                            }
                            else
                            {
                                RollbackTransaction();
                            }

                        }
                    }
                    else
                    {
                            RollbackTransaction();
                    }
                }
                
            }
        }
        //------------------Reconnectivity------------------------------------
        public DataTable reqListForTeam(string user, string zone)
        {
            return Query($"select t05.* from t74105 t05 inner join t74041  t41 on t41.t_request_id=t05.t_request_id inner join t74057 t57 on t41.t_user_id=t57.t_user_id where t05.t_final_flag is null and t05.t_chat_acpt_id is null and t41.t_disch_status is null and T41.T_USER_ID IS NOT NULL  AND T41.T_CANCEL_STATUS IS NULL AND T57.T_ZONE_CODE = '{zone}' and t41.t_user_id='{user}' and t05.t_chat_req_id!='{user}'");
        }
        public DataTable reqListForDoc(string user, string zone)
        {
            return Query($"select t05.* from t74105 t05 inner join t74041  t41 on t41.t_request_id=t05.t_request_id inner join t74057 t57 on t41.t_user_id=t57.t_user_id where t05.t_final_flag is null and t05.t_chat_acpt_id is null and t41.t_disch_status is null and T41.T_USER_ID IS NOT NULL  AND T41.T_CANCEL_STATUS IS NULL AND T57.T_ZONE_CODE = '{zone}' and t41.t_chat_doc_id='{user}' and t05.t_chat_req_id!='{user}'");
        }
        public string acptReqofDoc(string user, string zone)
        {
            string msg = "";
            int req = 0;
            DataTable dt = Query($"SELECT NVL(MAX(T41.T_REQUEST_ID),0) T_REQUEST_ID,t41.t_chat_doc_id FROM T74041 T41 INNER JOIN T74057 T57 ON T57.T_USER_ID=T41.T_USER_ID WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_USER_ID IS NOT NULL AND T41.T_CANCEL_STATUS IS NULL  AND T57.T_ZONE_CODE = {zone} AND t41.t_user_id='{user}' group by t41.t_chat_doc_id");
            if (dt.Rows.Count > 0)
            {
                req = Int32.Parse(dt.Rows[0]["T_REQUEST_ID"].ToString());
            }
            if (req != 0)
            {
                BeginTransaction();
                if (Command($"UPDATE T74105 SET T_CHAT_ACPT_ID='{user}',T_CHAT_ACPT_TIME=localtimestamp where T_REQUEST_ID={req} and T_CHAT_REQ_ID='{dt.Rows[0]["t_chat_doc_id"].ToString()}' and T_FINAL_FLAG is null"))
                {
                    CommitTransaction();
                    msg = $"Request from {dt.Rows[0]["t_chat_doc_id"].ToString()}  has been accepted";
                }
                else
                {
                    RollbackTransaction();
                    msg = $"Request from {dt.Rows[0]["t_chat_doc_id"].ToString()}  can not be accepted";
                }

            }
            return msg;

        }
        public string closeChat(string user, int req, string lang)
        {
            string msg = "";
            BeginTransaction();
            if (Command($"update t74105 set T_CHAT_END_ID='{user}' ,T_CHAT_END_TIME =localtimestamp,t_final_flag=1 where t_request_id={req} and t_final_flag is null"))
            {// && Command($"UPDATE T74041 SET T_CHAT_DOC_ID = NULL WHERE T_REQUEST_ID = {req} ")
                CommitTransaction();
                //msg = $"This patient is in hold you can access this patient later";
                msg = common.GetSingleMsg(lang, "S0059");
            }
            else
            {
                RollbackTransaction();
                //msg = $"Chat with this patient can not be hold off";
                msg = common.GetSingleMsg(lang, "S0060");
            }


            return msg;

        }
        public string onRcvPatReq(string user, int req, string lang)
        {
            string msg = "";
            BeginTransaction();
            if (Command($"update t74105 set T_CHAT_ACPT_ID='{user}' ,T_CHAT_ACPT_TIME =localtimestamp where t_request_id={req} and t_final_flag is null"))
            {
                CommitTransaction();
                //msg = $"Chat request has been accepted with this patient";
                msg = common.GetSingleMsg(lang, "S0061");
            }
            else
            {
                RollbackTransaction();
                //msg = $"Chat request can not be accepted with this patient";
                msg = common.GetSingleMsg(lang, "S0062");
            }


            return msg;

        }
        public string conWthDoc(string user, string zone)
        {
            string msg = "";
            int req = 0;
            DataTable dt = Query($"SELECT NVL(MAX(T41.T_REQUEST_ID),0) T_REQUEST_ID,t41.t_chat_doc_id FROM T74041 T41 INNER JOIN T74057 T57 ON T57.T_USER_ID=T41.T_USER_ID WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_USER_ID IS NOT NULL AND T41.T_CANCEL_STATUS IS NULL  AND T57.T_ZONE_CODE = {zone} AND t41.t_user_id='{user}' group by t41.t_chat_doc_id");
            if (dt.Rows.Count > 0)
            {
                req = Int32.Parse(dt.Rows[0]["T_REQUEST_ID"].ToString());
            }
            
            if (req != 0)
            {
                DataTable dtAccpt = Query($"SELECT T_ACCEPT_STATUS,T_CHAT_DOC_ID,T_START_TIME FROM T74041 WHERE T_REQUEST_ID={req}");
                if (dtAccpt.Rows.Count > 0)
                {
                    string acptStts = dtAccpt.Rows[0]["T_ACCEPT_STATUS"].ToString();
                    string docId = dtAccpt.Rows[0]["T_CHAT_DOC_ID"].ToString();
                    string startDest = dtAccpt.Rows[0]["T_START_TIME"].ToString();
                    if (acptStts == "1")
                    {
                        if (!string.IsNullOrEmpty(startDest))
                        {
                            if (string.IsNullOrEmpty(docId))
                            {
                                msg ="Doctor has not been assigned.Soon a doctor will contact with you.If you need doctor immediately, please give message to doctor Dashboard";
                            }
                            else
                            {
                                DataTable dtchk = Query($"SELECT * FROM T74105 WHERE T_REQUEST_ID={req} AND T_CHAT_REQ_ID='{user}' AND T_CHAT_ACPT_ID IS NULL AND T_FINAL_FLAG IS NULL");
                                if (dtchk.Rows.Count > 0)
                                {
                                    msg = $"You have already sent request to {dt.Rows[0]["t_chat_doc_id"].ToString()}";
                                }
                                else
                                {

                                    BeginTransaction();
                                    if (Command($"INSERT INTO T74105 (T_REQUEST_ID,T_CHAT_REQ_ID,T_CHAT_REQ_TIME) VALUES ({req},'{user}',LOCALTIMESTAMP)"))
                                    {
                                        CommitTransaction();
                                        msg = $"Request has been sent to {dt.Rows[0]["t_chat_doc_id"].ToString()}";
                                    }
                                    else
                                    {
                                        RollbackTransaction();
                                        msg = $"Request to {dt.Rows[0]["t_chat_doc_id"].ToString()}  can not be sent";
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            msg = "In order to contact with doctor, please start destination first";
                        }
                    }
                    else
                    {
                        msg = "Please accept the request first";
                    }
                }
                else
                {
                    msg = "Request can not be sent to doctor";
                }

            }
            return msg;

        }
        public int chkConn(string user, string zone)
        {
            int a = 0;
            int req = 0;
            DataTable dtreq = Query($"SELECT NVL(MAX(T41.T_REQUEST_ID),0) T_REQUEST_ID,t41.t_chat_doc_id FROM T74041 T41 INNER JOIN T74057 T57 ON T57.T_USER_ID=T41.T_USER_ID WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_USER_ID IS NOT NULL AND T41.T_CANCEL_STATUS IS NULL  AND T57.T_ZONE_CODE = {zone} AND t41.t_user_id='{user}' group by t41.t_chat_doc_id");
            if (dtreq.Rows.Count > 0)
            {
                req = Int32.Parse(dtreq.Rows[0]["T_REQUEST_ID"].ToString());
            }
            if (req != 0)
            {
                DataTable dt = Query($"select * from t74105 where T_REQUEST_ID={req} and T_FINAL_FLAG is null");
                a = dt.Rows.Count;
            }
            return a;

        }
        public DataTable getDocID(string user)
            {
                return Query($"select T_CHAT_DOC_ID,T_REQUEST_ID from t74041 where T_USER_ID='{user}' AND T_DISCH_STATUS IS NULL AND T_ACCEPT_STATUS IS NOT NULL");
            }

        public string setDoc_new(string zone, int req, string user, string doc)
        {
            string msg = "";
            DataTable dt_chk =
                Query(
                    $"SELECT T_CHAT_DOC_ID COUNTER FROM T74041 WHERE T_REQUEST_ID ={req} AND T_CHAT_DOC_ID IS NOT NULL");
            if (dt_chk.Rows.Count > 0)
            {
                msg = "This patient can not be assigned." + dt_chk.Rows[0]["T_CHAT_DOC_ID"] +
                      " is busy with this patient.";
            }
            else { 
                DataTable dt =
                    Query(
                        $"select *  from (SELECT 0 sl, T57.T_USER_ID FROM T74057 T57 WHERE T57.T_ZONE_CODE ={zone} AND T57.T_ROLE_CODE =81 AND T57.T_USER_ID NOT IN (SELECT DISTINCT T74041.T_CHAT_DOC_ID FROM T74041 WHERE T74041.T_DISCH_STATUS IS NULL AND T_CHAT_DOC_ID IS NOT NULL AND T74041.T_USER_ID IS NOT NULL AND T74041.T_CANCEL_STATUS IS NULL ) AND T57.T_LOGIN_STATUS = 1 UNION SELECT DISTINCT count(T41.T_CHAT_DOC_ID )sl,T41.T_CHAT_DOC_ID FROM T74041 T41 INNER JOIN T74057 T57 ON T57.T_USER_ID=T41.T_CHAT_DOC_ID WHERE T41.T_DISCH_STATUS IS NULL AND T41.T_CHAT_DOC_ID IS NOT NULL AND T41.T_USER_ID IS NOT NULL AND T41.T_CANCEL_STATUS IS NULL AND T57.T_ROLE_CODE =81 AND T57.T_ZONE_CODE ={zone} AND T57.T_LOGIN_STATUS = 1 group by T41.T_CHAT_DOC_ID order by sl) where T_USER_ID = '{doc}'");


            if (dt.Rows.Count > 0)
            {
                BeginTransaction();
                if (Command(
                        $"UPDATE T74041 SET T_CHAT_FLAG='1',T_CHAT_DOC_ID='{dt.Rows[0]["T_USER_ID"].ToString()}' WHERE T_REQUEST_ID='{req}'") &&
                    Command(
                        $"INSERT INTO T74105 (T_REQUEST_ID,T_CHAT_REQ_ID,T_CHAT_REQ_TIME,T_CHAT_ACPT_ID, T_CHAT_ACPT_TIME) VALUES ({req},'{doc}',LOCALTIMESTAMP,'{doc}',LOCALTIMESTAMP)")
                )
                {
                    CommitTransaction();
                    //msg = dt.Rows[0]["T_USER_ID"].ToString();
                    msg = "Ok";
                }
                else
                {
                    RollbackTransaction();
                }
            }
        }
        return msg;

        }

        public string setNewConv(int req, string doc)
        {
            string msg = "";

                BeginTransaction();
                if (Command($"INSERT INTO T74105 (T_REQUEST_ID,T_CHAT_REQ_ID,T_CHAT_REQ_TIME,T_CHAT_ACPT_ID, T_CHAT_ACPT_TIME) VALUES ({req},'{doc}',LOCALTIMESTAMP,'{doc}',LOCALTIMESTAMP)"))
                {
                    CommitTransaction();
                    msg = "Ok";
                }
                else
                {
                    RollbackTransaction();
                }
            return msg;

        }

        public string emergenceyReq(int req, string text, string user)
        {
            string msg = "";
            DataTable dtAccpt = Query($"SELECT T_ACCEPT_STATUS,T_CHAT_DOC_ID,T_START_TIME FROM T74041 WHERE T_REQUEST_ID={req}");
            if (dtAccpt.Rows.Count>0)
            {
                string acptStts = dtAccpt.Rows[0]["T_ACCEPT_STATUS"].ToString();
                string docId = dtAccpt.Rows[0]["T_CHAT_DOC_ID"].ToString();
                string startDest = dtAccpt.Rows[0]["T_START_TIME"].ToString();
                if (acptStts == "1")
                {
                    if (!string.IsNullOrEmpty(startDest))
                    {
                        if (string.IsNullOrEmpty(docId))
                        {
                            BeginTransaction();
                            if (Command($"INSERT INTO T74112 (T_REQUEST_ID,T_USER_ID,T_TIME,T_TEXT) VALUES ({req},'{user}',LOCALTIMESTAMP,'{text}')"))
                            {
                                CommitTransaction();
                                msg = "Request Sent to Doctor Dashboard. Soon a Doctor will contact you. Thanks";
                            }
                            else
                            {
                                RollbackTransaction();
                            }
                        }
                        else
                        {
                            msg = "Please sent request to " + docId;
                        }

                    }
                    else
                    {
                        msg = "In order to contact with doctor, please start destination first";
                    }
                }
                else
                {
                    msg = "Please accept the request first";
                }
            }
            else
            {
                msg = "Request can not be sent to doctor";
            }
            return msg;

        }

        public DataTable getPatMsg(int req)
        {
            return Query($"SELECT ROWNUM SL,T_TIME,T_TEXT  FROM T74112 WHERE T_REQUEST_ID={req} ORDER BY T_AUTO_ID DESC");
        }
        public int chkPat(string req)
        {
            int res = 0;
            try
            {
                DataTable dt = Query($"SELECT *  FROM T74041 T41 WHERE  T41.T_REQUEST_ID={req} AND (T41.T_DISCH_STATUS   IS NOT NULL  OR T41.T_DISCHARGE_TIME IS NOT  NULL)");
                res = dt.Rows.Count;
            }
            catch (Exception e)
            {
                res = 0;
            }
         
            return res;
        }




        //---------------------New Modification----------------------
        public string chkReqHos(int requestId, string lang)
        {
            string msg= "";

            DataTable dt =
                Query($"select * from t74111 where T_REQUEST_ID={requestId} and T_REQUEST_ACPT_CNCL_TIME is null");
            if (dt.Rows.Count>0)
            {
                msg = common.GetSingleMsg(lang, "S0072");
                //  msg = "Team has not accepted the previous hospital request";
            }
            return msg;
        }

        public string reqHos(int requestId, string hosId, string userId,string lang)
        {
            string msg;
            BeginTransaction();
            if (Command($"INSERT INTO T74111 (T_REQUEST_ID,T_HOS_REQ_USER_ID,T_REQUEST_TIME,T_HOS_SITE_CODE) VALUES ({requestId},'{userId}',LOCALTIMESTAMP,'{hosId}')"))
            {
                CommitTransaction();
                // msg = "Hospital request sent";
                msg = common.GetSingleMsg(lang, "S0073");
            }
            else
            {
                RollbackTransaction();
                msg = common.GetSingleMsg(lang, "S0074");
                // msg = "Hospital request not sent";
            }
            return msg;

        }

        //---------------------For Discharge Verify on T74125Page
        public string verifySummeryReport(int requestId)
        {
            string msg;
            if (chkVerified(requestId))
            {
                BeginTransaction();
                if (Command($"UPDATE T74041 SET T_VERIFY_DISCHARGE='1' WHERE T_REQUEST_ID = {requestId}"))
                {
                    CommitTransaction();
                    msg = "Patient verified successfully";
                }
                else
                {
                    RollbackTransaction();
                    msg = "Patient not verified";
                }
            }
           
            else
            {
                msg = GetSingleMsg("2","S0053");
            }

            return msg;

        }

        public DataTable getPlaceHolder(string label)
        {
            return Query($"SELECT T_LANG2_TEXT LBLMSG  FROM T74000 WHERE T_FORM_CODE = 'T74131' AND T_LABEL_NAME='{label}'");
        }


    }
}