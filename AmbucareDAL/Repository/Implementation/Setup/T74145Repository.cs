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
    public class T74145Repository: IT74145
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private T74145DAL _t74145Dal = new T74145DAL();

        public DataTable getGridData()
        {
            var data = _t74145Dal.T74104GetGridListData();
            return data;
        }

        public string insertStatus(string T_ENTRY_USER, string T_LANG1_NAME, string T_LANG2_NAME)
        {
            var message = "";
            int dischIdmax =Convert.ToInt32(_t74145Dal.getMaxDischargeNo());
            var data = _t74145Dal.InsertT74104(T_ENTRY_USER, dischIdmax, T_LANG1_NAME, T_LANG2_NAME);
            if (data == true)
            {
                message = "Insert successful";
            }
            return message;
        }

        public string updateStatus(string T_UPD_USER, string T_LANG1_NAME, string T_LANG2_NAME, string T_DISCH_ID)
        {
            var message = "";
            var data = _t74145Dal.UpdateT74104(T_UPD_USER, T_LANG1_NAME, T_LANG2_NAME, T_DISCH_ID);
            if (data == true)
            {
                message = "Update successful";
            }
            return message;
        }
    }
}