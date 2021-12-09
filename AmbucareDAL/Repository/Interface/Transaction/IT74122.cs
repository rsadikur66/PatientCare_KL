using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74122 : IDisposable
    {
        IEnumerable GetStoreName(int T_STORE_ID);
        IEnumerable GetTransferData(int T_TRANSFER_REQ_ID);
        IEnumerable GetReceiveBy(int T_RCV_TO_STR_ID); 
        bool SaveData(List<T74080> t74080,T74084 t74084, List<T74085> t74085, List<T74027> t74027);
        //IEnumerable GetTransferList_85(int T_TRANSFER_REQ_ID);
    }
}
