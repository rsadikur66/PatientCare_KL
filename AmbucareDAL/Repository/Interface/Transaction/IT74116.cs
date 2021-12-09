using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
using System.Collections;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74116 : IDisposable
    {
        IEnumerable getPursData(int Pur_ID);
        IEnumerable getreceiveBy(int store_ID);
        IEnumerable getpurProduct(int pur_ID);
        bool SaveData(T74060 t74060, List<T74061> t74061, List<T74031> t74031, T74030 t74030, List<T74027> t74027);
        IEnumerable GetUom(int T_ITEM_ID);
    }
}
