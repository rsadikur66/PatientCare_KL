using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74020 : IDisposable
    {
        IQueryable<T74020> getGridData { get; }

        bool saveEducation(T74020 t74020);
        bool deleteEducation(int t_DOC_SPECI_ID);
    }
}
