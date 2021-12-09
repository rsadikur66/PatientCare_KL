using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
using System.Collections;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74130 : IDisposable
    {
        IEnumerable GetZone();
        IEnumerable GetSite(string zone);
        IEnumerable GetAmbPatList();

        void Insert_T74130(List<T74096> T74096);
        string GetSiteCode(int T_REQUEST_ID, string T_SITE_CODE);
        string GetActive(int T_REQUEST_ID ,string T_SITE_CODE);
        void Save();
        void Dispose();
    }
}
