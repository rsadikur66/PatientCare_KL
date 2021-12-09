using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74024 : IDisposable
    {
        void Add_T74024(T74024 Z);
        bool Delete_T74024(int T_RESULT_TYP_ID);
        IQueryable<T74024> Get_T74024 { get; }
        T74024 Find(Int32 id);
        void Save();
        void Dispose();
        IQueryable<T74024> Get_T74024_Search(string searchValue, int pageIndex, int pageSize);
    }
}
