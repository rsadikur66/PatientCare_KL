using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74005 : IDisposable
    {
        IQueryable<T74005> getGridData { get;  }

        bool saveEmployee(T74005 e);
        bool deleteEmType(int T_EMP_TYP_ID);
    }
}
