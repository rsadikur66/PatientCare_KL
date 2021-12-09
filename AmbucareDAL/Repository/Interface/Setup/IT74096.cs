using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
using System.Collections;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74096 : IDisposable
    {
        IEnumerable GetZone();
        IEnumerable GetSite(string zone);
        IEnumerable GetAmbPatList(string T_SITE_CODE);
        void Insert_T74096(List<T74096> T74096);
        string GetSiteCode(int? T_AMBU_REG_ID, string T_SITE_CODE);
        string GetActive( int? T_AMBU_REG_ID, string T_SITE_CODE);
        void Save();
        void Dispose();
    }
}
