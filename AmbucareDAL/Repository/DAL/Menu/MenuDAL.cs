using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Menu
{
    public class MenuDAL : CommonDAL
    {
       
        public void Save_t74041(string userid,string lat,string lon,string address)
        {
            DataTable data = Query($"SELECT T_LATITUDE, T_LONGITUDE FROM T74026 WHERE T_USER_ID='{userid}' ORDER BY ID");
            if (data.Rows.Count>0)
            {
                lat = data.Rows[0]["T_LATITUDE"].ToString();
                lon = data.Rows[0]["T_LONGITUDE"].ToString();
            }
            int T_PANIC_ID = 0;
            string req = "";
            DataTable dt = Query($"select nvl(max(T_PANIC_ID),0)+1 T_PANIC_ID from t74103");
            DataTable dt26 = Query($"select nvl(max(ID),0)+1 ID from t74026");
            int int26Id = 0;
            if (dt26.Rows.Count>0)
            {
                int26Id = Int32.Parse(dt26.Rows[0]["ID"].ToString());

            }
            DataTable dtR =Query($"SELECT T_REQUEST_ID FROM T74041 WHERE T_USER_ID='{userid}' AND T_DISCH_STATUS IS NULL");
            if (dtR.Rows.Count>0)
            {
                req = dtR.Rows[0]["T_REQUEST_ID"].ToString();
            }
            if (dt.Rows.Count>0)
            { T_PANIC_ID = Int32.Parse(dt.Rows[0]["T_PANIC_ID"].ToString());

            }
            if (T_PANIC_ID>0&& int26Id>0)
            {
               BeginTransaction();
                if (Command($"insert into t74103 (T_PANIC_ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_DEVICES,T_REQUEST_ID,T_ADDRESS) VALUES ({T_PANIC_ID},'{userid}',LOCALTIMESTAMP,{lat},{lon},'',{req},'{address}')")&& Command($"INSERT INTO T74026 (ID,T_USER_ID,T_ENTRY_TIME,T_LATITUDE,T_LONGITUDE,T_DEVICES,T_REQUEST_ID,T_EVENT_ID) VALUES ({int26Id},'{userid}',LOCALTIMESTAMP,{lat},{lon},'',{req},16)"))
                {
                   CommitTransaction(); 
                }
                else
                {
                    RollbackTransaction();
                }
            }
            
        }


        public DataTable GetLatLong(string userid)
        {
            return Query($"SELECT * FROM T74026 WHERE T_USER_ID ='{userid}'");
        }
        public DataTable getFormName(string lang, string code)
        {
            return Query($"SELECT T_LANG{lang}_NAME name from t74066 where T_FORM_CODE='{code}'");
        }
    }
}