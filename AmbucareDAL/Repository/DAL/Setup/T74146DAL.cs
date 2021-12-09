using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Setup
{
    public class T74146DAL : CommonDAL
    {
        public bool InsertT74099(int T_MSG_ID, string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG, string T_ENTRY_USER, string T_FORM_CODE, string T_ACTV_STTS)
        {
            Command($"INSERT INTO T74099 (T_MSG_ID, T_MSG_CODE, T_LANG1_MSG, T_LANG2_MSG, T_ENTRY_USER, T_ENTRY_DATE,T_FORM_CODE, T_ACTV_STTS)VALUES ({T_MSG_ID}, '{T_MSG_CODE}', '{T_LANG1_MSG}', '{T_LANG2_MSG}', '{T_ENTRY_USER}', TRUNC(SYSDATE),'{T_FORM_CODE}', '{T_ACTV_STTS}')");
            return true;
        }
        public bool UpdateT74099(string T_MSG_ID, string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG, string T_UPD_USER, string T_FORM_CODE, string T_ACTV_STTS)
        {
            Command($"UPDATE T74099 SET T_MSG_CODE = '{T_MSG_CODE}',  T_LANG2_MSG ='{T_LANG2_MSG}',  T_LANG1_MSG = '{T_LANG1_MSG}',T_UPD_USER ='{T_UPD_USER}',T_UPD_DATE=TRUNC(SYSDATE),T_FORM_CODE='{T_FORM_CODE}' , T_ACTV_STTS = '{T_ACTV_STTS}' WHERE T_MSG_ID = '{T_MSG_ID}'");
            return true;
        }
        public bool DeleteT74099(string T_MSG_ID)
        {
            Command($"DELETE FROM T74099 WHERE T_MSG_ID = {T_MSG_ID}");
            return true;
        }

        public DataTable T74099GetGridListData()
        {
            return Query($"SELECT T_MSG_ID,T_MSG_CODE,T_LANG2_MSG,T_LANG1_MSG,T_ACTV_STTS,T_FORM_CODE FROM T74099");
        }

        public DataTable getFormCode()
        {
            return Query($"SELECT DISTINCT T_FORM_CODE FROM T74066");
        }
        public string getMaxmessageNo()
        {
            return Query($"SELECT MAX(T_MSG_ID)+1 AS T_MSG_ID FROM T74099").Rows[0]["T_MSG_ID"].ToString();
        }


    }
}