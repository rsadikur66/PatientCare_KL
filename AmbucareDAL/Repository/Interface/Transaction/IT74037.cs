using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74037 : IDisposable
    {
        IQueryable<T74054> GetCompanyData { get;  }
        IQueryable<T74005> GetEmployeeData { get;  }
    }
}
