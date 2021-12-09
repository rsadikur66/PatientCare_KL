using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74024Repository : IT74024
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74024Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        //public T74024 Find(string id)
        //{
        //    T74024 objEmployee = new T74024();
        //    int fid = Convert.ToInt32(id);
        //    objEmployee = obj.T74024.Where(p => p.T_ITEM_BRA_ID == fid).FirstOrDefault();
        //    return objEmployee;
        //}

        public T74024 Find(Int32 id)
        {
            T74024 objEmployee = new T74024();
            try
            {
                objEmployee = obj.T74024.Where(p => p.T_RESULT_TYP_ID == Convert.ToInt32(id)).FirstOrDefault();
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
            return objEmployee;
        }
        public void Add_T74024(T74024 _t74024)
        {
            var check = obj.T74024.Where(P => P.T_RESULT_TYP_ID == _t74024.T_RESULT_TYP_ID).FirstOrDefault();
            if (check == null)
            {
                obj.T74024.Add(_t74024);
                obj.SaveChanges();
            }
            else
            {
                var UpD = obj.T74024.Where(P => P.T_RESULT_TYP_ID == _t74024.T_RESULT_TYP_ID).FirstOrDefault();
                UpD.T_LANG1_NAME = _t74024.T_LANG1_NAME;
                UpD.T_LANG2_NAME = _t74024.T_LANG2_NAME;
                obj.SaveChanges();
            }

        }
        public bool Delete_T74024(int T_RESULT_TYP_ID)
        {
            var T74024 = obj.T74024.Find(T_RESULT_TYP_ID);
            obj.T74024.Remove(T74024);
            Save();
            return true;
        }
        public IQueryable<T74024> Get_T74024
        {
            get { return obj.T74024.OrderBy(x => x.T_RESULT_TYP_ID); }
        }

        public void Save()
        {
            obj.SaveChanges();
        }
        public void Dispose()
        {
            obj.Dispose();
        }

        public IQueryable<T74024> Get_T74024_Search(string searchValue, int pageIndex, int pageSize)
        {
            var data = obj.T74024.Where(x => x.T_LANG2_NAME.Contains(searchValue.ToUpper())).OrderBy(x => x.T_RESULT_TYP_ID);
            return data;
        }
    }
}