using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Setup;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74150Repository : IT74150
    {
        private AmbucareContainer obj = new AmbucareContainer();
        private T74150DAL _t74150Dal = new T74150DAL();

        public DataTable GetItemTypeList()
        {
            var data = _t74150Dal.GetItemTypeList();
            return data;
        }
        public DataTable GetGenList()
        {
            var data = _t74150Dal.GetGenList();
            return data;
        }

        public DataTable GetItemsList(string itemtype)
        {
            var data = _t74150Dal.GetItemsList(itemtype);
            return data;
        }
        public DataTable GetFormList()
        {
            var data = _t74150Dal.GetFormList();
            return data;
        }

        //public DataTable GetGridData(int PageIndex, int PageSize,string lang)
        //{
        //    var data = _t74150Dal.GetGridData(PageIndex,PageSize,lang);
        //    return data;
        //}
        public DataTable GetGridData(int itemtype, int PageIndex, int PageSize, string lang)
        {
            var data = _t74150Dal.GetGridData(itemtype,PageIndex, PageSize, lang);
            return data;
        }
        public DataTable GetGridData_Count(int itemtype,string searchValue, int PageIndex, int PageSize,string lang)
        {
            var data = _t74150Dal.GetGridData_Count(itemtype,searchValue, PageIndex, PageSize,lang);
            return data;
        }
        public DataTable GetGridData_Search(int itemtype, string searchValue, int PageIndex, int PageSize,string lang)
        {
            var data = _t74150Dal.GetGridData_Search(searchValue, PageIndex, PageSize,lang);
            return data;
        }

        public DataTable GetGridData_Search_Count(int itemtype, string searchValue, int PageIndex, int PageSize,string lang)
        {
            var data = _t74150Dal.GetGridData_Search_Count(searchValue,  PageIndex,  PageSize,lang);
            return data;
        }

       

        public string insertdata(string itemid,string costdtlid, string itemtype, string gencode, string formcode, string itemnameeng, string itemnameloc,string user)
        {
            string message = "";
            if (itemid == null)
            {
                string itemcode = _t74150Dal.getmaxItemno();
                //string itemcode = _t74150Dal.Query("select max(T_ITEM_CODE)+ 1 T_ITEM_CODE from t74211").Rows[0]["T_ITEM_CODE"].ToString();
                var data = _t74150Dal.insertdata(itemcode, itemtype, gencode, formcode, itemnameeng, itemnameloc,user);
                if (data == true)
                {
                    message = "Data Save successfully";
                }

            }
            else
            {
                if (itemtype == "23")
                {
                    string count = _t74150Dal.ifExist(itemtype,itemid,costdtlid);
                    if (count == "1")
                    {
                        var data = _t74150Dal.updateData(itemid, costdtlid, itemtype, gencode, formcode, itemnameeng, itemnameloc, user);
                        if (data == true)
                        {
                            message = "Data Update successfully";
                        }
                    }
                }
                else
                {
                    string counts = _t74150Dal.ifExist(itemtype, itemid, costdtlid);
                    if (counts == "1")
                    {
                        var data = _t74150Dal.updateData(itemid, costdtlid, itemtype, gencode, formcode, itemnameeng, itemnameloc, user);
                        if (data == true)
                        {
                            message = "Data Update successfully";
                        }
                    }
                }
                
            }
            return message;
        }


    }
}