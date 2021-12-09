using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74052 : IDisposable
    {
        IQueryable<T74052> All { get; }
        bool AddZone(T74052 Z);
        IQueryable<T74052> GetZoneData { get; }

        bool DeleteZone(string t_ZONE_CODE);
    }
}
