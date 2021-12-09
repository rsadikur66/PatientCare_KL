using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Repository.DAL.Query;
using AmbucareDAL.Repository.Interface.Queries;

namespace AmbucareDAL.Repository.Implementation.Queries
{
    public class Q74136Repository:IQ74136
    {
        Q74136DAL _q74136Dal = new Q74136DAL();
        public DataTable GetNotificationData(string language, string zonCode)
        {
            var data = _q74136Dal.GetNotificationData(language, zonCode);
            return data;
        }

        public DataTable GetCriterias(string rolecode, string language)
        {
            var data = _q74136Dal.GetCriterias(rolecode,language);
            return data;
        }
    }
}