using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Setup
{
    public interface IT74150
    {
        DataTable GetItemTypeList();
        DataTable GetGenList();
        DataTable GetItemsList(string itemtype);
        DataTable GetFormList();

        //DataTable GetGridData(int PageIndex, int PageSize,string lang);
        DataTable GetGridData(int itemtype, int PageIndex, int PageSize, string lang);
        
        DataTable GetGridData_Search(int itemtype, string searchValue, int PageIndex, int PageSize,string lang);
        DataTable GetGridData_Count(int itemtype, string searchValue, int PageIndex, int PageSize,string lang);
        DataTable GetGridData_Search_Count(int itemtype, string searchValue, int PageIndex, int PageSize,string lang);

        string insertdata(string itemid, string costdtlid,string itemtype, string gencode, string formcode, string itemnameeng, string itemnameloc,string user);

    }
}
