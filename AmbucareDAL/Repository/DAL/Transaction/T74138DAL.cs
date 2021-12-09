using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.DAL.Transaction
{
    public class T74138DAL:CommonDAL
    {
        public DataTable getDocSite(string P_USER_ID)
        {
            return Query($"SELECT T_SITE_DIS_CODE FROM T74041 WHERE T_DISCH_STATUS IS NULL AND T_USER_ID='{P_USER_ID}' AND T_DOC_REQ_ACPT_DATE IS NOT NULL");
        }
        public string reqHos(int requestId, string hosId, string userId)
        {
            string msg;
            BeginTransaction();
            if (Command($"INSERT INTO T74111 (T_REQUEST_ID,T_HOS_REQ_USER_ID,T_REQUEST_TIME,T_REQUEST_ACPT_CNCL_TIME,T_HOS_SITE_CODE) VALUES ({requestId},'{userId}',LOCALTIMESTAMP,LOCALTIMESTAMP,'{hosId}')"))
            {
                CommitTransaction();
                //msg = "Hospital request sent";
                msg = "Data saved successfully";

            }
            else
            {
                RollbackTransaction();
                //msg = "Hospital request not sent";
                msg = "Data not saved";
            }
            return msg;

        }

    }
}