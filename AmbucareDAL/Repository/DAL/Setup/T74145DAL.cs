using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Setup
{
    public class T74145DAL : CommonDAL
    {
        public bool InsertT74104(string T_ENTRY_USER,int dischargeId, string T_LANG1_NAME, string T_LANG2_NAME)
        {
            Command($"INSERT INTO T74104 (T_ENTRY_DATE, T_ENTRY_USER,T_DISCH_ID, T_LANG1_NAME,T_LANG2_NAME)VALUES(TRUNC(SYSDATE), '{T_ENTRY_USER}',{dischargeId},'{T_LANG1_NAME}', '{T_LANG2_NAME}')");
            return true;
        }
        public bool UpdateT74104(string T_UPD_USER, string T_LANG1_NAME, string T_LANG2_NAME, string T_DISCH_ID)
        {
            Command($"UPDATE T74104 SET T_UPD_DATE = TRUNC(SYSDATE),T_UPD_USER = '{T_UPD_USER}',T_LANG1_NAME = '{T_LANG1_NAME}',T_LANG2_NAME = '{T_LANG2_NAME}' WHERE T_DISCH_ID = '{T_DISCH_ID}'");
            return true;
        }
        public bool DeleteT74104(string T_DISCH_ID)
        {
            Command($"DELETE FROM T740104 WHERE T_DISCH_ID = {T_DISCH_ID}");
            return true;
        }

        public DataTable T74104GetGridListData()
        {
            return Query($"SELECT T_DISCH_ID,T_LANG1_NAME,T_LANG2_NAME FROM T74104");
        }
        public string getMaxDischargeNo()
        {
            return Query($"SELECT MAX(T_DISCH_ID)+1 AS T_DISCH_ID FROM T74104").Rows[0]["T_DISCH_ID"].ToString();
        }


    }
}