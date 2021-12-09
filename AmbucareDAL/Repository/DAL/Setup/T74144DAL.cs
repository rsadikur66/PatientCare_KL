using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Setup
{
    public class T74144DAL:CommonDAL
    {
        public string getCancelReasonId()
        {
            return Query($"SELECT MAX(T_CANCEL_REASON_ID)+1 AS T_CANCEL_REASON_ID FROM T74101").Rows[0]["T_CANCEL_REASON_ID"].ToString();
        }

    }
}