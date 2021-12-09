using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74008 :IDisposable
    {
        IQueryable<T74008> getGridData { get; }

        bool saveProType(T74008 t74008);
        bool deleteProd(int d);
    }
}
