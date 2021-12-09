using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74115Repository : IT74115
    {
        private AmbucareContainer obj = new AmbucareContainer();
        public IEnumerable GridData(int PageIndex, int PageSize, string TypeID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            if (TypeID == "PR")
            {
                query = (from t44 in obj.T74044
                         join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                         from t14_t44 in t14_44.DefaultIfEmpty()
                         join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                         from t14_t11 in t14_11.DefaultIfEmpty()
                         join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                         from t14_t02 in t14_02.DefaultIfEmpty()
                         join t30 in obj.T74030 on t44.T_STORE_ID equals t30.T_STORE_ID
                         where t30.T_ISSUE_STATUS != "1"
                         orderby t30.T_PUR_ID descending
                         select new
                         {
                             t30.T_PUR_ID,
                             t30.T_STORE_ID,
                             t30.T_PUR_NO,
                             t30.T_PUR_REQ_DATE,
                             t30.T_EXP_DLVRY_DATE,
                             t44.T_AMBU_REG_ID,
                             Central_Lang2 = t44.T_LANG2_NAME,
                             t14_t44.T_YEAR_MODEL,
                             t14_t44.T_SERIES,
                             Brand_Lang2 = t14_t02.T_LANG2_NAME,
                             Color_Lang2 = t14_t11.T_LANG2_NAME
                         }).AsEnumerable().Select((k, i) => new
                         {
                             RowNumber = i,
                             CMN_ID = k.T_PUR_ID,
                             CMN_STORE_ID = k.T_STORE_ID,
                             CMN_NO = k.T_PUR_NO,
                             CMN_DATE = k.T_PUR_REQ_DATE,
                             CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                             CMN_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                         }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            }
            else if (TypeID == "SR")
            {
                query = (from t44 in obj.T74044
                         join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                         from t14_t44 in t14_44.DefaultIfEmpty()
                         join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                         from t14_t11 in t14_11.DefaultIfEmpty()
                         join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                         from t14_t02 in t14_02.DefaultIfEmpty()
                         join t48 in obj.T74048 on t44.T_STORE_ID equals t48.T_STR_FR_ID
                         where t48.T_SL_REQ_STATUS != "1"
                         orderby t48.T_SL_REQ_ID descending
                         select new
                         {
                             t48.T_SL_REQ_ID,
                             t48.T_STR_FR_ID,
                             t48.T_SL_REQ_NO,
                             t48.T_SL_REQ_DATE,
                             t48.T_EXP_DLVRY_DATE,
                             t44.T_AMBU_REG_ID,
                             Central_Lang2 = t44.T_LANG2_NAME,
                             t14_t44.T_YEAR_MODEL,
                             t14_t44.T_SERIES,
                             Brand_Lang2 = t14_t02.T_LANG2_NAME,
                             Color_Lang2 = t14_t11.T_LANG2_NAME
                         }).AsEnumerable().Select((k, i) => new
                         {
                             RowNumber = i,
                             CMN_ID = k.T_SL_REQ_ID,
                             CMN_STORE_ID = k.T_STR_FR_ID,
                             CMN_NO = k.T_SL_REQ_NO,
                             CMN_DATE = k.T_SL_REQ_DATE,
                             CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                             CMN_LANG2_NAME = k.T_STR_FR_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                         }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            }
            else if (TypeID == "TI")
            {
                query = (from t80 in obj.T74080
                    join t44 in obj.T74044 on t80.T_REQ_SET_BY_STORE_ID equals t44.T_STORE_ID
                    join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID
                    join t02 in obj.T74002 on t14.T_BRAND_ID equals t02.T_ITEM_BRA_ID
                    join t11 in obj.T74011 on t14.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID
                    join t81 in obj.T74081 on t80.T_TRANSFER_REQ_ID equals t81.T_TRANSFER_REQ_ID
                    join t27 in obj.T74027 on t81.T_CUR_STOCK_ID equals t27.T_CUR_STOCK_ID
                    where t80.T_STATUS != "1"
                    orderby t80.T_TRANSFER_REQ_ID descending
                    select new
                    {
                        t80.T_TRANSFER_REQ_ID,
                        t80.T_REQ_SET_BY_STORE_ID,
                        t80.T_TRANSFER_REQ_NO,
                        t80.T_REQ_DATE,
                        t44.T_AMBU_REG_ID,
                        Central_Lang2 = t44.T_LANG2_NAME,
                        t14.T_YEAR_MODEL,
                        t14.T_SERIES,
                        Brand_Lang2 = t02.T_LANG2_NAME,
                        Color_Lang2 = t11.T_LANG2_NAME,
                        t27.T_EXPIRE_DATE
                    }).AsEnumerable().Distinct().Select((m, i) => new
                    {
                        RowNumber = i,
                        CMN_ID = m.T_TRANSFER_REQ_ID,
                        CMN_STORE_ID = m.T_REQ_SET_BY_STORE_ID,
                        CMN_NO = m.T_TRANSFER_REQ_NO,
                        CMN_DATE = m.T_REQ_DATE,
                        CMN_DLVRY_DATE = m.T_EXPIRE_DATE,
                        CMN_LANG2_NAME = m.T_REQ_SET_BY_STORE_ID == 1 ? m.Central_Lang2 : m.Brand_Lang2 + "-" + m.T_SERIES + "-" + m.T_YEAR_MODEL
                    }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            }
            return query;
        }
        public int GridData(string searchValue, string TypeID)
        {
            int query = 0;
            if (searchValue != "")
            {
                if (TypeID == "PR")
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                             from t14_t44 in t14_44.DefaultIfEmpty()
                             join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                             from t14_t11 in t14_11.DefaultIfEmpty()
                             join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                             from t14_t02 in t14_02.DefaultIfEmpty()
                             join t30 in obj.T74030 on t44.T_STORE_ID equals t30.T_STORE_ID
                             where t30.T_ISSUE_STATUS != "1"
                             orderby t30.T_PUR_ID descending
                             select new
                             {
                                 t30.T_PUR_ID,
                                 t30.T_STORE_ID,
                                 t30.T_PUR_NO,
                                 t30.T_PUR_REQ_DATE,
                                 t30.T_EXP_DLVRY_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14_t44.T_YEAR_MODEL,
                                 t14_t44.T_SERIES,
                                 Brand_Lang2 = t14_t02.T_LANG2_NAME,
                                 Color_Lang2 = t14_t11.T_LANG2_NAME
                             }).AsEnumerable().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_PUR_ID,
                                 CMN_STORE_ID = k.T_STORE_ID,
                                 CMN_NO = k.T_PUR_NO,
                                 CMN_DATE = k.T_PUR_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                                 CMN_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL

                             }).Where(x =>
                                     x.CMN_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                     x.CMN_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                     x.CMN_DATE.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                                     x.CMN_DLVRY_DATE.ToString().ToUpper().Contains(searchValue.ToUpper())).Count();
                }
                else if (TypeID == "SR")
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                             from t14_t44 in t14_44.DefaultIfEmpty()
                             join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                             from t14_t11 in t14_11.DefaultIfEmpty()
                             join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                             from t14_t02 in t14_02.DefaultIfEmpty()
                             join t48 in obj.T74048 on t44.T_STORE_ID equals t48.T_STR_FR_ID
                             where t48.T_SL_REQ_STATUS != "1"
                             orderby t48.T_SL_REQ_ID descending
                             select new
                             {
                                 t48.T_SL_REQ_ID,
                                 t48.T_STR_FR_ID,
                                 t48.T_SL_REQ_NO,
                                 t48.T_SL_REQ_DATE,
                                 t48.T_EXP_DLVRY_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14_t44.T_YEAR_MODEL,
                                 t14_t44.T_SERIES,
                                 Brand_Lang2 = t14_t02.T_LANG2_NAME,
                                 Color_Lang2 = t14_t11.T_LANG2_NAME
                             }).AsEnumerable().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_SL_REQ_ID,
                                 CMN_STORE_ID = k.T_STR_FR_ID,
                                 CMN_NO = k.T_SL_REQ_NO,
                                 CMN_DATE = k.T_SL_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                                 CMN_LANG2_NAME = k.T_STR_FR_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL

                             }).Where(x =>
                                 x.CMN_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                 x.CMN_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                 x.CMN_DATE.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                                 x.CMN_DLVRY_DATE.ToString().ToUpper().Contains(searchValue.ToUpper())).Count();
                }
                else if (TypeID == "TI")
                {
                    query = (from t80 in obj.T74080
                             join t44 in obj.T74044 on t80.T_REQ_SET_BY_STORE_ID equals t44.T_STORE_ID
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID
                             join t02 in obj.T74002 on t14.T_BRAND_ID equals t02.T_ITEM_BRA_ID
                             join t11 in obj.T74011 on t14.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID
                             join t81 in obj.T74081 on t80.T_TRANSFER_REQ_ID equals t81.T_TRANSFER_REQ_ID
                             join t27 in obj.T74027 on t81.T_CUR_STOCK_ID equals t27.T_CUR_STOCK_ID
                             where t80.T_STATUS != "1"
                             orderby t80.T_TRANSFER_REQ_ID descending
                             select new
                             {
                                 t80.T_TRANSFER_REQ_ID,
                                 t80.T_REQ_SET_BY_STORE_ID,
                                 t80.T_TRANSFER_REQ_NO,
                                 t80.T_REQ_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14.T_YEAR_MODEL,
                                 t14.T_SERIES,
                                 Brand_Lang2 = t02.T_LANG2_NAME,
                                 Color_Lang2 = t11.T_LANG2_NAME,
                                 t27.T_EXPIRE_DATE
                             }).AsEnumerable().Distinct().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_TRANSFER_REQ_ID,
                                 CMN_STORE_ID = k.T_REQ_SET_BY_STORE_ID,
                                 CMN_NO = k.T_TRANSFER_REQ_NO,
                                 CMN_DATE = k.T_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXPIRE_DATE,
                                 CMN_LANG2_NAME = k.T_REQ_SET_BY_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).Where(x => x.CMN_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                           x.CMN_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                 x.CMN_DATE.ToString().ToUpper().Contains(searchValue.ToUpper())).Count();
                }

            }
            else
            {
                if (TypeID == "PR")
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                             from t14_t44 in t14_44.DefaultIfEmpty()
                             join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                             from t14_t11 in t14_11.DefaultIfEmpty()
                             join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                             from t14_t02 in t14_02.DefaultIfEmpty()
                             join t30 in obj.T74030 on t44.T_STORE_ID equals t30.T_STORE_ID
                             where t30.T_ISSUE_STATUS != "1"
                             orderby t30.T_PUR_ID descending
                             select new
                             {
                                 t30.T_PUR_ID,
                                 t30.T_STORE_ID,
                                 t30.T_PUR_NO,
                                 t30.T_PUR_REQ_DATE,
                                 t30.T_EXP_DLVRY_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14_t44.T_YEAR_MODEL,
                                 t14_t44.T_SERIES,
                                 Brand_Lang2 = t14_t02.T_LANG2_NAME,
                                 Color_Lang2 = t14_t11.T_LANG2_NAME
                             }).AsEnumerable().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_PUR_ID,
                                 CMN_STORE_ID = k.T_STORE_ID,
                                 CMN_NO = k.T_PUR_NO,
                                 CMN_DATE = k.T_PUR_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                                 CMN_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).Count();
                }
                if (TypeID == "SR")
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                             from t14_t44 in t14_44.DefaultIfEmpty()
                             join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                             from t14_t11 in t14_11.DefaultIfEmpty()
                             join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                             from t14_t02 in t14_02.DefaultIfEmpty()
                             join t48 in obj.T74048 on t44.T_STORE_ID equals t48.T_STR_FR_ID
                             where t48.T_SL_REQ_STATUS != "1"
                             orderby t48.T_SL_REQ_ID descending
                             select new
                             {
                                 t48.T_SL_REQ_ID,
                                 t48.T_STR_FR_ID,
                                 t48.T_SL_REQ_NO,
                                 t48.T_SL_REQ_DATE,
                                 t48.T_EXP_DLVRY_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14_t44.T_YEAR_MODEL,
                                 t14_t44.T_SERIES,
                                 Brand_Lang2 = t14_t02.T_LANG2_NAME,
                                 Color_Lang2 = t14_t11.T_LANG2_NAME
                             }).AsEnumerable().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_SL_REQ_ID,
                                 CMN_STORE_ID = k.T_STR_FR_ID,
                                 CMN_NO = k.T_SL_REQ_NO,
                                 CMN_DATE = k.T_SL_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                                 CMN_LANG2_NAME = k.T_STR_FR_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).Count();
                }
                if (TypeID == "TI")
                {
                    query = (from t80 in obj.T74080
                             join t44 in obj.T74044 on t80.T_REQ_SET_BY_STORE_ID equals t44.T_STORE_ID
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID
                             join t02 in obj.T74002 on t14.T_BRAND_ID equals t02.T_ITEM_BRA_ID
                             join t11 in obj.T74011 on t14.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID
                             join t81 in obj.T74081 on t80.T_TRANSFER_REQ_ID equals t81.T_TRANSFER_REQ_ID
                             join t27 in obj.T74027 on t81.T_CUR_STOCK_ID equals t27.T_CUR_STOCK_ID
                             where t80.T_STATUS != "1"
                             orderby t80.T_TRANSFER_REQ_ID descending
                             select new
                             {
                                 t80.T_TRANSFER_REQ_ID,
                                 t80.T_REQ_SET_BY_STORE_ID,
                                 t80.T_TRANSFER_REQ_NO,
                                 t80.T_REQ_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14.T_YEAR_MODEL,
                                 t14.T_SERIES,
                                 Brand_Lang2 = t02.T_LANG2_NAME,
                                 Color_Lang2 = t11.T_LANG2_NAME,
                                 t27.T_EXPIRE_DATE
                             }).AsEnumerable().Distinct().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_TRANSFER_REQ_ID,
                                 CMN_STORE_ID = k.T_REQ_SET_BY_STORE_ID,
                                 CMN_NO = k.T_TRANSFER_REQ_NO,
                                 CMN_DATE = k.T_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXPIRE_DATE,
                                 CMN_LANG2_NAME = k.T_REQ_SET_BY_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).Count();
                }
            }

            return query;
        }
        public IEnumerable GridData(string searchValue, Int32 PageIndex, Int32 PageSize, string TypeID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if (TypeID == "PR")
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                             from t14_t44 in t14_44.DefaultIfEmpty()
                             join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                             from t14_t11 in t14_11.DefaultIfEmpty()
                             join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                             from t14_t02 in t14_02.DefaultIfEmpty()
                             join t30 in obj.T74030 on t44.T_STORE_ID equals t30.T_STORE_ID
                             where t30.T_ISSUE_STATUS != "1"
                             orderby t30.T_PUR_ID descending
                             select new
                             {
                                 t30.T_PUR_ID,
                                 t30.T_STORE_ID,
                                 t30.T_PUR_NO,
                                 t30.T_PUR_REQ_DATE,
                                 t30.T_EXP_DLVRY_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14_t44.T_YEAR_MODEL,
                                 t14_t44.T_SERIES,
                                 Brand_Lang2 = t14_t02.T_LANG2_NAME,
                                 Color_Lang2 = t14_t11.T_LANG2_NAME
                             }).AsEnumerable().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_PUR_ID,
                                 CMN_STORE_ID = k.T_STORE_ID,
                                 CMN_NO = k.T_PUR_NO,
                                 CMN_DATE = k.T_PUR_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                                 CMN_LANG2_NAME = k.T_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize &&
                                           x.CMN_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                           x.CMN_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                           x.CMN_DATE.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                                           x.CMN_DLVRY_DATE.ToString().ToUpper().Contains(searchValue.ToUpper())
                    ).ToList();
                }
                if (TypeID == "SR")
                {
                    query = (from t44 in obj.T74044
                             join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID into t14_44
                             from t14_t44 in t14_44.DefaultIfEmpty()
                             join t11 in obj.T74011 on t14_t44.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID into t14_11
                             from t14_t11 in t14_11.DefaultIfEmpty()
                             join t02 in obj.T74002 on t14_t44.T_BRAND_ID equals t02.T_ITEM_BRA_ID into t14_02
                             from t14_t02 in t14_02.DefaultIfEmpty()
                             join t48 in obj.T74048 on t44.T_STORE_ID equals t48.T_STR_FR_ID
                             where t48.T_SL_REQ_STATUS != "1"
                             orderby t48.T_SL_REQ_ID descending
                             select new
                             {
                                 t48.T_SL_REQ_ID,
                                 t48.T_STR_FR_ID,
                                 t48.T_SL_REQ_NO,
                                 t48.T_SL_REQ_DATE,
                                 t48.T_EXP_DLVRY_DATE,
                                 t44.T_AMBU_REG_ID,
                                 Central_Lang2 = t44.T_LANG2_NAME,
                                 t14_t44.T_YEAR_MODEL,
                                 t14_t44.T_SERIES,
                                 Brand_Lang2 = t14_t02.T_LANG2_NAME,
                                 Color_Lang2 = t14_t11.T_LANG2_NAME
                             }).AsEnumerable().Select((k, i) => new
                             {
                                 RowNumber = i,
                                 CMN_ID = k.T_SL_REQ_ID,
                                 CMN_STORE_ID = k.T_STR_FR_ID,
                                 CMN_NO = k.T_SL_REQ_NO,
                                 CMN_DATE = k.T_SL_REQ_DATE,
                                 CMN_DLVRY_DATE = k.T_EXP_DLVRY_DATE,
                                 CMN_LANG2_NAME = k.T_STR_FR_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                             }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize &&
                                           x.CMN_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                           x.CMN_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                           x.CMN_DATE.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                                           x.CMN_DLVRY_DATE.ToString().ToUpper().Contains(searchValue.ToUpper())
                    ).ToList();
                }
                if (TypeID == "TI")
                {
                    query = (

                        from t80 in obj.T74080
                        join t44 in obj.T74044 on t80.T_REQ_SET_BY_STORE_ID equals t44.T_STORE_ID
                        join t14 in obj.T74014 on t44.T_AMBU_REG_ID equals t14.T_AMBU_REG_ID
                        join t02 in obj.T74002 on t14.T_BRAND_ID equals t02.T_ITEM_BRA_ID
                        join t11 in obj.T74011 on t14.T_ITEM_COLOR_ID equals t11.T_ITEM_COLOR_ID
                        join t81 in obj.T74081 on t80.T_TRANSFER_REQ_ID equals t81.T_TRANSFER_REQ_ID
                        join t27 in obj.T74027 on t81.T_CUR_STOCK_ID equals t27.T_CUR_STOCK_ID
                        where t80.T_STATUS != "1"
                        orderby t80.T_TRANSFER_REQ_ID descending
                        select new
                        {
                            t80.T_TRANSFER_REQ_ID,
                            t80.T_REQ_SET_BY_STORE_ID,
                            t80.T_TRANSFER_REQ_NO,
                            t80.T_REQ_DATE,
                            t44.T_AMBU_REG_ID,
                            Central_Lang2 = t44.T_LANG2_NAME,
                            t14.T_YEAR_MODEL,
                            t14.T_SERIES,
                            Brand_Lang2 = t02.T_LANG2_NAME,
                            Color_Lang2 = t11.T_LANG2_NAME,
                            t27.T_EXPIRE_DATE
                        }).AsEnumerable().Distinct().Select((k, i) => new
                        {
                            RowNumber = i,
                            CMN_ID = k.T_TRANSFER_REQ_ID,
                            CMN_STORE_ID = k.T_REQ_SET_BY_STORE_ID,
                            CMN_NO = k.T_TRANSFER_REQ_NO,
                            CMN_DATE = k.T_REQ_DATE,
                            CMN_DLVRY_DATE = k.T_EXPIRE_DATE,
                            CMN_LANG2_NAME = k.T_REQ_SET_BY_STORE_ID == 1 ? k.Central_Lang2 : k.Brand_Lang2 + "-" + k.T_SERIES + "-" + k.T_YEAR_MODEL
                        }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize &&
                                      x.CMN_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                      x.CMN_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                                      x.CMN_DATE.ToString().ToUpper().Contains(searchValue.ToUpper())).ToList();
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return query;
        }
        public void Dispose()
        {
            obj.Dispose();
        }

        public void Save()
        {
            obj.SaveChanges();
        }
    }
}