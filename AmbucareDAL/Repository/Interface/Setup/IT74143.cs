using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74143
    {
        DataTable getAllGridData(string lang,string zoneCode);
        bool UpdateT74041(int req);
        bool UpdateT74057(string T_USER_ID);
    }
}
