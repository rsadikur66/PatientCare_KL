using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
//using AmbucareDAL.Models.Initialization;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74066:IDisposable
    {
        IEnumerable GetRoleList();
        IEnumerable GetFormTypeList();
        IEnumerable GetPageTypeList();
        IEnumerable GetFormList(string P_FORM_CODE);
        int GetMaxOrderNo(int P_ROLE_CODE,int P_FORM_TYPE_ID,int P_PAGE_TYPE_ID);
        bool Insert(T74066 T74066);
        IEnumerable GetGridData();
        void Save();
        
        
    }
}
