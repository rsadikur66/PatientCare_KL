using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74023 : IDisposable
    {
        IQueryable<T74023> getGridData { get;}

        bool saveEducation(T74023 t74023);
        bool deleteDepartment(int t_EDU_BOARD_ID);
    }
}
