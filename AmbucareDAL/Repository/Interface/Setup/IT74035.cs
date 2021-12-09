﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74035:IDisposable
    {
        IEnumerable GetGridDataT74035(int PageIndex, int PageSize);

        int GetGridData_Search_Count(string searchValue);
        IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize);

        void InsertUpdate(T74035 _T74035);

        bool deleteData(int T_DEPET_ID);
    }
}
