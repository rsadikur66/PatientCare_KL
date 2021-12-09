using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Query;
using AmbucareDAL.Repository.Interface.Queries;

namespace AmbucareDAL.Repository.Implementation.Queries
{
    public class Q74145Repository : IQ74145
    {
        private readonly Q74145DAL _q74145 = new Q74145DAL();
        private AmbucareContainer obj = new AmbucareContainer();
        CommonDAL common = new CommonDAL();
        public Q74145Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        public DataTable GetAllAcceptRequest(string userid,string from_date,string to_date)
        {
            var data = _q74145.GetAllAcceptRequest(userid,from_date,to_date);
            return data;
        }

    }
}