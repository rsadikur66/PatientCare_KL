using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74006 : IDisposable
    {
        IQueryable<T74006> getGridData { get; }

        bool saveEmpDes(T74006 t74006);
        bool DeleteEmpDes(int d);
    }
}
