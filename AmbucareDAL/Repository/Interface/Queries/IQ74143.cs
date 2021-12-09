using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Queries
{
   public interface IQ74143
    {
        DataTable getAllGridData(string lang);
        bool UpdateT74041(int req);
        bool UpdateT74057(string T_USER_ID);
    }
}
