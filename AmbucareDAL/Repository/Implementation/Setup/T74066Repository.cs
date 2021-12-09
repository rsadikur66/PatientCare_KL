using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
//using AmbucareDAL.Models.Initialization;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74066Repository : IT74066
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74066Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IEnumerable GetRoleList()
        {
            return obj.T74071.Select(a => new {CODE = a.T_ROLE_CODE, NAME = a.T_LANG2_NAME}).AsEnumerable().ToList();
        }

        public IEnumerable GetFormTypeList()
        {
            return obj.T74070.Where(b=>b.T_FORM_TYPE_ID>0).Select(a => new { CODE = a.T_FORM_TYPE_ID, NAME = a.T_LANG2_NAME }).AsEnumerable().ToList();
        }

        public IEnumerable GetPageTypeList()
        {
            return obj.T74090.Select(a => new { CODE = a.T_PAGE_TYPE_ID, NAME = a.T_LANG2_NAME }).AsEnumerable().ToList();
        }
        public IEnumerable GetFormList(string P_FORM_CODE)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t66 in obj.T74066
                where t66.T_FORM_CODE.ToUpper().Contains(P_FORM_CODE.ToUpper())
                group t66 by new {t66.T_FORM_CODE}
                into m
                select new {FORM_CODE = m.Key.T_FORM_CODE}).AsEnumerable().ToList();
            return query;
        }

        public int GetMaxOrderNo(int P_ROLE_CODE, int P_FORM_TYPE_ID, int P_PAGE_TYPE_ID)
        {
            byte? order= obj.T74066
                .Where(a => a.T_ROLE_CODE == P_ROLE_CODE && a.T_FORM_TYPE_ID == P_FORM_TYPE_ID &&
                            a.T_PAGE_TYPE_ID == P_PAGE_TYPE_ID && a.T_USER_ID == "00001").Max(a=>a.T_ORDER);
            int order_no = order == null ? 1 : Int32.Parse(order.ToString()) + 1;
            return order_no;
        }
        public bool Insert(T74066 _T74066)
        {
            try
            {
                bool Status = false;
                if (_T74066.T_FORM_CODE_ID == 0)
                {
                    _T74066.T_USER_ID = "00001";
                    _T74066.T_TYPE = "w";
                    obj.T74066.Add(_T74066);
                    Save();
                    Status = true;
                }
                else
                {

                    var data = obj.T74066.Where(p => p.T_FORM_CODE_ID == _T74066.T_FORM_CODE_ID).FirstOrDefault();
                    if (data != null)
                    {
                        data.T_UPD_USER = _T74066.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_FORM_CODE = _T74066.T_FORM_CODE;
                        data.T_LANG1_NAME = _T74066.T_LANG1_NAME;
                        data.T_LANG2_NAME = _T74066.T_LANG2_NAME;
                        data.T_VERSION_NO = _T74066.T_VERSION_NO;
                        data.T_FORM_TYPE_ID = _T74066.T_FORM_TYPE_ID;
                        data.T_FORM_CODE_ID = _T74066.T_FORM_CODE_ID;
                        data.T_INACTIVE_FLAG = _T74066.T_INACTIVE_FLAG;
                        data.T_PAGE_TYPE_ID = _T74066.T_PAGE_TYPE_ID;
                        data.T_ORDER = _T74066.T_ORDER;
                        data.T_ROLE_CODE = _T74066.T_ROLE_CODE;

                        Save();

                    }
                }
                return Status;
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
            return true;
        }

        public IEnumerable GetGridData()
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t66 in obj.T74066
                join t71 in obj.T74071 on t66.T_ROLE_CODE equals t71.T_ROLE_CODE
                join t70 in obj.T74070 on t66.T_FORM_TYPE_ID equals t70.T_FORM_TYPE_ID
                join t90 in obj.T74090 on t66.T_PAGE_TYPE_ID equals t90.T_PAGE_TYPE_ID
                where t66.T_USER_ID=="00001"
                select new
                {
                    t66.T_FORM_CODE_ID,
                    FORM_CODE = t66.T_FORM_CODE,
                    FORM_NAME1 = t66.T_LANG1_NAME,
                    FORM_NAME2 = t66.T_LANG2_NAME,
                    FORM_TYPE_CODE = t66.T_FORM_TYPE_ID,
                    FORM_TYPE_NAME = t70.T_LANG2_NAME,
                    PAGE_TYPE_CODE = t66.T_PAGE_TYPE_ID,
                    PAGE_TYPE_NAME = t90.T_LANG2_NAME,
                    ROLE_CODE = t66.T_ROLE_CODE,
                    ROLE_NAME = t71.T_LANG2_NAME,
                    t66.T_ORDER,
                    t66.T_INACTIVE_FLAG
                }).AsEnumerable().OrderByDescending(q=>q.FORM_TYPE_CODE).ToList();
            return query;
        }
        public void Save()
        {
            obj.SaveChanges();
        }
        public void Dispose()
        {
            obj.Dispose();
        }  
    }
}