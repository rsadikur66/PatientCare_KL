using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74115 : IDisposable
    {
        IEnumerable GridData(int PageIndex, int PageSize, string TypeID);
        int GridData(string searchValue, string TypeID);
        IEnumerable GridData(string searchValue, Int32 PageIndex, Int32 PageSize, string TypeID);
    }
}
