using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74069 : IDisposable
    {
        IQueryable<T74069> getGridData { get; }

        bool SaveBloodGroup(T74069 t74069);
        bool Delete(Int32 blood);
    }
}
