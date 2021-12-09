using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Queries
{
   public interface IQ74144
    {
        DataTable GetAllTeamData(string lang, string zonCode);
    }
}
