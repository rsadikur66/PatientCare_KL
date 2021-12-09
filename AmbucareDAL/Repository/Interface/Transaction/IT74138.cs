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
  public interface IT74138
    {
      //  IEnumerable Gender();
     //   IEnumerable GetUserIDByAMBRegID(int T_AMBU_REG_ID);
        IEnumerable GetAllUserLatlong(string zonCode);
        IEnumerable GetLoginUserLatlong(int rquestId, string T_USER_ID);
        IEnumerable getDestination(string T_USER_ID,int e);
        string setHandoverHospital(string T_USER_ID, string site);

        string getDocSite(string P_USER_ID);

        string saveEvent(string user, string evt, int req, decimal? lat, decimal? lng);

        bool getArrivedDuration(string T_USER_ID, int req);

        //  DataTable GetAllDataOnMap_T74041(int P_AMBU_REG_ID);
        // IEnumerable GetPatInfo();
        //   int Insert_t74046(T74046 _t74046);
        // void Insert_t74041(T74041 _t74041);
        // void Save();
        string saveRequestHospitalbyTeam(int requestId, string hosId);
    }
}
