using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Setup;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74146Repository : IT74146
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private T74146DAL _t74146Dal = new T74146DAL();

        public DataTable getGridData()
        {
            var data = _t74146Dal.T74099GetGridListData();
            return data;
        }

        public DataTable getFormCode()
        {
            var data = _t74146Dal.getFormCode();
            return data;
        }

        public string insertStatus(string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG, string T_ENTRY_USER,string T_FORM_CODE, string T_ACTV_STTS)
        {
            var message = "";
            int dischIdmax = Convert.ToInt32(_t74146Dal.getMaxmessageNo());
            var data = _t74146Dal.InsertT74099(dischIdmax, T_MSG_CODE, T_LANG1_MSG, T_LANG2_MSG, T_ENTRY_USER, T_FORM_CODE,T_ACTV_STTS);
            if (data == true)
            {
                message = "Insert successful";
            }
            return message;
        }

        public string updateStatus(string T_MSG_ID, string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG, string T_UPD_USER, string T_FORM_CODE, string T_ACTV_STTS)
        {
            var message = "";
            var data = _t74146Dal.UpdateT74099(T_MSG_ID, T_MSG_CODE, T_LANG1_MSG, T_LANG2_MSG, T_UPD_USER, T_FORM_CODE,T_ACTV_STTS);
            if (data == true)
            {
                message = "Update successful";
            }
            return message;
        }
    }
}