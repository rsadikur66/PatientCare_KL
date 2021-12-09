using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Setup
{
    public class T74150DAL : CommonDAL
    {
        public DataTable GetItemTypeList()
        {
            return Query(
                $"SELECT T_COST_TYPE_ID,T_LANG1_NAME,T_LANG2_NAME from t74072 where T_COST_TYPE_ID = 121 or T_COST_TYPE_ID = 23");
        }
        public DataTable GetGenList()
        {
            return Query(
                $"select T_GEN_CODE,T_LANG1_NAME,T_LANG2_NAME from T74113");
        }
        public DataTable GetFormList()
        {
            return Query($"SELECT T_FORM_CODE,T_LANG1_NAME,T_LANG2_NAME FROM T74115");
        }

        public DataTable GetItemsList(string itemtype)
        {
            return Query($"SELECT T_COST_TYPE_DTL_ID,T_LANG1_NAME,T_LANG2_NAME FROM T74073 WHERE T_COST_TYPE_ID = {itemtype}");
        }

        //public DataTable GetGridData(int PageIndex, int PageSize, string lang)
        //{
        //    //return Query(
        //    //    $"SELECT DISTINCT * FROM (SELECT ROWNUM RowNumber,T73.T_COST_TYPE_DTL_ID, T73.T_COST_TYPE_ID ITEM_TYPE_CODE,t72.T_LANG{lang}_NAME ITEM_TYPE,T10.T_GEN_CODE,T10.T_LANG{lang}_NAME GENERICNAME, T12.T_FORM_CODE,T12.T_LANG{lang}_NAME FORM_TYPE,T11.T_LANG2_NAME,T11.T_LANG1_NAME, T11.T_ITEM_CODE FROM T74211 T11 JOIN T74210 T10 ON T11.T_GEN_CODE = T10.T_GEN_CODE JOIN T74212 T12 ON T11.T_FORM_CODE = T12.T_FORM_CODE JOIN T74073 T73 ON T11.T_ITEM_CODE = T73.T_ID JOIN T74072 T72  ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID) t where t.RowNumber between(({PageIndex} * {PageSize}) + 1) AND(({PageIndex} + 1) * {PageSize}) ORDER BY RowNumber");
        //    return Query(
        //        $"SELECT DISTINCT * FROM (SELECT ROWNUM RowNumber, T73.T_COST_TYPE_DTL_ID, T73.T_COST_TYPE_ID ITEM_TYPE_CODE, t72.T_LANG{lang}_NAME ITEM_TYPE, T13.T_GEN_CODE, T13.T_LANG{lang}_NAME GENERICNAME, T15.T_FORM_CODE, T15.T_LANG{lang}_NAME FORM_TYPE, T14.T_LANG2_NAME, T14.T_LANG1_NAME, T14.T_ITEM_CODE FROM T74114 T14 JOIN T74113 T13 ON T14.T_GEN_CODE = T13.T_GEN_CODE JOIN T74115 T15 ON T14.T_FORM_CODE = T15.T_FORM_CODE JOIN T74073 T73 ON T14.T_ITEM_CODE = T73.T_ID JOIN T74072 T72 ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID) t where t.RowNumber between(({PageIndex} * {PageSize}) + 1) AND(({PageIndex} + 1) * {PageSize}) ORDER BY RowNumber");

        //}
        public DataTable GetGridData(int itemtype, int PageIndex, int PageSize, string lang)
        {
            //return Query(
            //    $"SELECT DISTINCT * FROM (SELECT ROWNUM RowNumber,T73.T_COST_TYPE_DTL_ID, T73.T_COST_TYPE_ID ITEM_TYPE_CODE,t72.T_LANG{lang}_NAME ITEM_TYPE,T10.T_GEN_CODE,T10.T_LANG{lang}_NAME GENERICNAME, T12.T_FORM_CODE,T12.T_LANG{lang}_NAME FORM_TYPE,T11.T_LANG2_NAME,T11.T_LANG1_NAME, T11.T_ITEM_CODE FROM T74211 T11 JOIN T74210 T10 ON T11.T_GEN_CODE = T10.T_GEN_CODE JOIN T74212 T12 ON T11.T_FORM_CODE = T12.T_FORM_CODE JOIN T74073 T73 ON T11.T_ITEM_CODE = T73.T_ID JOIN T74072 T72  ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID) t where t.RowNumber between(({PageIndex} * {PageSize}) + 1) AND(({PageIndex} + 1) * {PageSize}) ORDER BY RowNumber");
            return Query(
                $"SELECT DISTINCT * FROM (SELECT ROWNUM RowNumber, T73.T_COST_TYPE_DTL_ID, T73.T_COST_TYPE_ID ITEM_TYPE_CODE, t72.T_LANG2_NAME ITEM_TYPE, T13.T_GEN_CODE, T13.T_LANG2_NAME GENERICNAME, T15.T_FORM_CODE, T15.T_LANG2_NAME FORM_TYPE, T73.T_LANG2_NAME, T73.T_LANG1_NAME, T73.T_ID T_ITEM_CODE FROM T74073 T73 LEFT JOIN T74114 T14 ON T73.T_ID = T14.T_ITEM_CODE LEFT JOIN T74113 T13 ON T14.T_GEN_CODE = T13.T_GEN_CODE LEFT JOIN T74115 T15 ON T14.T_FORM_CODE = T15.T_FORM_CODE JOIN T74072 T72 ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID where T73.T_COST_TYPE_ID = {itemtype}) t where t.RowNumber between(({PageIndex} * {PageSize}) + 1) AND(({PageIndex} + 1) * {PageSize}) ORDER BY RowNumber");

        }


        public DataTable GetGridData_Count(int itemtype,string searchValue, int PageIndex, int PageSize, string lang)
        {
            //return Query($"select count(*) cval from T74211 T11 JOIN T74210 T10 ON T11.T_GEN_CODE = T10.T_GEN_CODE JOIN T74212 T12 ON T11.T_FORM_CODE = T12.T_FORM_CODE JOIN T74073 T73 ON T11.T_ITEM_CODE = T73.T_ID JOIN T74072 T72  ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID");
            return Query($"SELECT COUNT(*) cval FROM T74073 T73 LEFT JOIN T74114 T14 ON T73.T_ID = T14.T_ITEM_CODE LEFT JOIN T74113 T13 ON T14.T_GEN_CODE = T13.T_GEN_CODE LEFT JOIN T74115 T15 ON T14.T_FORM_CODE = T15.T_FORM_CODE JOIN T74072 T72 ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID where T73.T_COST_TYPE_ID = {itemtype}");

        }

        public bool insertdata(string itemcode, string itemtype, string gencode, string formcode, string itemnameeng, string itemnameloc, string user)
        {
            Command($"INSERT INTO T74114 (T_ITEM_CODE,T_GEN_CODE,T_FORM_CODE,T_LANG2_NAME,T_LANG1_NAME) values({itemcode}, '{gencode}', '{formcode}', '{itemnameeng}', '{itemnameloc}')");
            Command($"INSERT INTO T74073 (T_ENTRY_USER,T_ENTRY_DATE,T_COST_TYPE_ID, T_LANG1_NAME, T_LANG2_NAME, T_INCHARGABLE, T_ACTIVE, T_ID, T_FORM_ID)VALUES ('{user}',SYSDATE,'{itemtype}', '{itemnameloc}', '{itemnameeng}', '1', '1', '{itemcode}', '{formcode}')");
            return true;
        }
        //T_UPD_USER,T_UPD_DATE,
        public bool updateData(string itemid, string costdtlid, string itemtype, string gencode, string formcode, string itemnameeng, string itemnameloc, string user)
        {
            if (itemtype == "23")
            {
                Command(
                    $"UPDATE T74114 SET T_GEN_CODE = '{gencode}', T_FORM_CODE = '{formcode}',T_LANG1_NAME = '{itemnameloc}',T_LANG2_NAME='{itemnameeng}' WHERE T_ITEM_CODE = {itemid}");
            }
            Command(
                $"UPDATE T74073 SET T_UPD_USER='{user}',T_UPD_DATE =SYSDATE,T_COST_TYPE_ID = '{itemtype}',T_FORM_ID = '{formcode}',T_LANG1_NAME = '{itemnameloc}',T_LANG2_NAME = '{itemnameeng}' WHERE T_COST_TYPE_DTL_ID = {costdtlid}");
            return true;
        }
        public string getmaxItemno()
        {
            return Query("SELECT MAX(T_ITEM_CODE)+1 AS T_ITEM_CODE FROM T74114").Rows[0]["T_ITEM_CODE"].ToString();
        }
        public string ifExist(string itemtype, string itemid, string costdtlid)
        {
            if (itemtype == "23")
            {
                return Query($"select count(*) counts from T74114 WHERE T_ITEM_CODE = {itemid}").Rows[0]["counts"].ToString();
            }
            else
            {
                return Query($"select count(*) counts from T74073 where T_COST_TYPE_DTL_ID={costdtlid}").Rows[0]["counts"].ToString();
            }

        }

        public DataTable GetGridData_Search(string searchValue, int PageIndex, int PageSize, string lang)
        {
            //return Query(
            //    $"SELECT DISTINCT * FROM (SELECT ROWNUM RowNumber, T73.T_COST_TYPE_DTL_ID, T73.T_COST_TYPE_ID ITEM_TYPE_CODE, t72.T_LANG2_NAME ITEM_TYPE, T10.T_GEN_CODE, T10.T_LANG2_NAME GENERICNAME, T12.T_FORM_CODE, T12.T_LANG2_NAME FORM_TYPE, T11.T_LANG2_NAME, T11.T_LANG1_NAME, T11.T_ITEM_CODE FROM T74211 T11 JOIN T74210 T10 ON T11.T_GEN_CODE = T10.T_GEN_CODE JOIN T74212 T12 ON T11.T_FORM_CODE = T12.T_FORM_CODE JOIN T74073 T73 ON T11.T_ITEM_CODE = T73.T_ID JOIN T74072 T72 ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID WHERE lower(T10.T_LANG{lang}_NAME) like '%'||lower('{searchValue}') || '%' OR lower(t72.T_LANG{lang}_NAME) LIKE '%'|| lower('{searchValue}') || '%' OR lower(T12.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR LOWER(T11.T_LANG{lang}_NAME) LIKE '%' || LOWER('{searchValue}') || '%' OR LOWER(T11.T_ITEM_CODE) LIKE '%' || LOWER ('{searchValue}') || '%' ) t WHERE t.RowNumber BETWEEN((0 * 10) + 1) AND((0 + 1) * 10) ORDER BY RowNumber");
            //return Query(
            //                $"SELECT DISTINCT * FROM (SELECT ROWNUM RowNumber, T73.T_COST_TYPE_DTL_ID, T73.T_COST_TYPE_ID ITEM_TYPE_CODE, t72.T_LANG2_NAME ITEM_TYPE, T13.T_GEN_CODE, T13.T_LANG2_NAME GENERICNAME, T15.T_FORM_CODE, T15.T_LANG2_NAME FORM_TYPE, T14.T_LANG2_NAME, T14.T_LANG1_NAME, T14.T_ITEM_CODE FROM T74114 T14 JOIN T74113 T13 ON T14.T_GEN_CODE = T13.T_GEN_CODE JOIN T74115 T15 ON T14.T_FORM_CODE = T15.T_FORM_CODE JOIN T74073 T73 ON T14.T_ITEM_CODE = T73.T_ID JOIN T74072 T72 ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID WHERE lower(T13.T_LANG{lang}_NAME) LIKE '%' ||lower('{searchValue}') || '%' OR lower(t72.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR lower(T15.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR LOWER(T14.T_LANG{lang}_NAME) LIKE '%' || LOWER('{searchValue}') || '%' OR LOWER(T14.T_ITEM_CODE) LIKE '%' || LOWER ('{searchValue}') || '%') t WHERE t.RowNumber BETWEEN((0 * 10) + 1) AND((0 + 1) * 10) ORDER BY RowNumber");

            return Query(
                            $"SELECT DISTINCT * FROM (SELECT ROWNUM RowNumber, T73.T_COST_TYPE_DTL_ID, T73.T_COST_TYPE_ID ITEM_TYPE_CODE, t72.T_LANG2_NAME ITEM_TYPE, T13.T_GEN_CODE, T13.T_LANG2_NAME GENERICNAME, T15.T_FORM_CODE, T15.T_LANG2_NAME FORM_TYPE, T73.T_LANG2_NAME, T73.T_LANG1_NAME, T73.T_ID T_ITEM_CODE FROM  T74073 T73 LEFT JOIN T74072 T72   ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID   LEFT JOIN T74114 T14   ON T14.T_ITEM_CODE = T73.T_ID     LEFT JOIN T74113 T13   ON T14.T_GEN_CODE = T13.T_GEN_CODE   LEFT JOIN T74115 T15   ON T14.T_FORM_CODE = T15.T_FORM_CODE    WHERE lower(T13.T_LANG{lang}_NAME) LIKE '%' ||lower('{searchValue}') || '%' OR lower(t72.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR lower(T15.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR LOWER(T14.T_LANG{lang}_NAME) LIKE '%' || LOWER('{searchValue}') || '%' OR LOWER(T14.T_ITEM_CODE) LIKE '%' || LOWER ('{searchValue}') || '%') t WHERE t.RowNumber BETWEEN((0 * 10) + 1) AND((0 + 1) * 10) ORDER BY RowNumber");



        }

        public DataTable GetGridData_Search_Count(string searchValue, int PageIndex, int PageSize, string lang)
        {
            //return Query(
            //    $"select count(*) cval from T74211 T11 JOIN T74210 T10 ON T11.T_GEN_CODE = T10.T_GEN_CODE JOIN T74212 T12 ON T11.T_FORM_CODE = T12.T_FORM_CODE JOIN T74073 T73 ON T11.T_ITEM_CODE = T73.T_ID JOIN T74072 T72  ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID WHERE lower(T10.T_LANG{lang}_NAME) like '%'||lower('{searchValue}') || '%' OR lower(t72.T_LANG{lang}_NAME) LIKE '%'|| lower('{searchValue}') || '%' OR lower(T12.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR LOWER(T11.T_LANG{lang}_NAME) LIKE '%' || LOWER('{searchValue}') || '%' OR LOWER(T11.T_ITEM_CODE) LIKE '%' || LOWER ('{searchValue}') || '%'");
            return Query(
                            $"SELECT COUNT(*) cval FROM T74114 T14 LEFT JOIN T74113 T13 ON T14.T_GEN_CODE = T13.T_GEN_CODE LEFT JOIN T74115 T15 ON T14.T_FORM_CODE = T15.T_FORM_CODE LEFT JOIN T74073 T73 ON T14.T_ITEM_CODE = T73.T_ID LEFT JOIN T74072 T72 ON T73.T_COST_TYPE_ID = T72.T_COST_TYPE_ID WHERE lower(T13.T_LANG{lang}_NAME) LIKE '%' ||lower('{searchValue}')|| '%' OR lower(t72.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR lower(T15.T_LANG{lang}_NAME) LIKE '%' || lower('{searchValue}') || '%' OR LOWER(T14.T_LANG{lang}_NAME) LIKE '%' || LOWER('{searchValue}') || '%' OR LOWER(T14.T_ITEM_CODE) LIKE '%' || LOWER ('{searchValue}') || '%'");

        }

    }
}