using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74136 : IDisposable
    {
        string maxUserId();
        DataTable getEmpList(string lang, string rolcode, string user);
        DataTable getRole(string lang,string rolcode);
        DataTable getZone(string lang);
        DataTable CheckUserId(string userId);
        string saveUser(T74057 _t74057, string entyuser);

        DataTable GetGridData(string lang,string user,string zone);

        DataTable getMaxValue(string user, string type);
    }
}
