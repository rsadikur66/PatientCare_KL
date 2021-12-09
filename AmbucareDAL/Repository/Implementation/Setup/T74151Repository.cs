using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Setup;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74151Repository : IT74151
    {
        private AmbucareContainer _obj = new AmbucareContainer();
        CommonDAL _common = new CommonDAL();
        readonly T74151DAL _t74151Dal = new T74151DAL();

        public DataTable GetRespData()
        {
            return _t74151Dal.GetRespData();
        }

        public string InsertResp(T74116 t74116,string user)
        {
            string result = _t74151Dal.InsertResp(t74116,user);

            return result;
        }
    }
}