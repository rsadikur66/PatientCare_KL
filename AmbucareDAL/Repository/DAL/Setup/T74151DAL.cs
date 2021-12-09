using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.DAL.Setup
{
    public class T74151DAL: CommonDAL
    {
        public DataTable GetRespData()
        {
            return Query($"SELECT T_ENTRY_DATE,T_ENTRY_USER,T_UPD_DATE,T_UPD_USER,T_RESP,T_RESP_LOWER,T_RESP_UPPER FROM T74116");
        }

        public string InsertResp(T74116 t74116,String user)
        {
            string msg = null;
            bool saveOrUodate = false;

            DataTable dtRespId = Query($"(select max(T_RESP)+1 T_RESP from T74116)");
            var respId = "";
            respId = !string.IsNullOrEmpty(dtRespId.Rows[0]["T_RESP"].ToString()) ? dtRespId.Rows[0]["T_RESP"].ToString() : "1";

            BeginTransaction();
            saveOrUodate = Command(string.IsNullOrEmpty(t74116.T_RESP) ? $"INSERT INTO T74116 (T_ENTRY_DATE,T_ENTRY_USER,T_RESP,T_RESP_LOWER,T_RESP_UPPER) VALUES(LOCALTIMESTAMP,'{user}','{respId}','{t74116.T_RESP_LOWER}','{t74116.T_RESP_UPPER}')" : $"UPDATE T74116 SET T_UPD_DATE=LOCALTIMESTAMP,T_UPD_USER='{user}',T_RESP_LOWER='{t74116.T_RESP_LOWER}',T_RESP_UPPER='{t74116.T_RESP_UPPER}' WHERE T_RESP='{t74116.T_RESP}'");


            if (saveOrUodate)
            {
                CommitTransaction();

                msg = "Data Saved Successfully";
            }
            else
            {
                RollbackTransaction();
                msg = "Data Not Saved";
            }

            return msg;
        }
    }
}