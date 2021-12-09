using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Repository.DAL.Query;
using AmbucareDAL.Repository.Interface.Queries;

namespace AmbucareDAL.Repository.Implementation.Queries
{
    public class Q74144Repository: IQ74144
    {
        Q74144DAL _q74144Dal = new Q74144DAL();
        
        public  DataTable GetAllTeamData(string lang, string zonCode)
        {
            var data = _q74144Dal.GetAllTeamData(lang, zonCode);
            return data;
        }
    }
}