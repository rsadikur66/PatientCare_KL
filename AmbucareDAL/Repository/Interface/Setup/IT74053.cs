using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74053 : IDisposable
    {
        IQueryable<T74052> getZoneCode { get; }
        //IQueryable<T74053> getHospital { get;  }
        IQueryable getHospital();

        bool AddHospital(T74053 h);
        bool DeleteHosPital(int t_SITE_NO);
    }
}
