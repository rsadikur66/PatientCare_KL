using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74129
    {
        IEnumerable GetGridData();
        bool SaveData(T74095 t95);
        IEnumerable GetSearchData(string stl);
    }
}
