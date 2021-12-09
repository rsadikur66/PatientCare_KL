using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Implementation.Setup;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74126 :IDisposable
    {
        bool insert_T74126(T74004 t74004,List<T74093> t74093);
        IEnumerable GetEmployee();

        IEnumerable GetEmProData(int T_EMP_ID);

    }
}
