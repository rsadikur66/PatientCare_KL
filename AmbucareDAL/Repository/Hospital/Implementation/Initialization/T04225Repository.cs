using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Hospital.Interface.Initialization;

namespace AmbucareDAL.Repository.Hospital.Implementation.Initialization
{
    public class T04225Repository 
    {
        //private HospitalEntities obj = new HospitalEntities();

        //public T04225Repository(HospitalEntities _Obj)
        //{
        //    obj = _Obj;
        //}


        //public T04225 Find(Int32 id)
        //{
        //    T04225 objEmployee = new T04225();
        //    try
        //    {
        //        objEmployee = obj.T04225.Where(p => p.T_CH_COMP_ID == Convert.ToInt32(id)).FirstOrDefault();
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //    return objEmployee;
        //}
        //public void AddJob(T04225 _T04225)
        //{
        //    var check = obj.T04225.Where(P => P.T_CH_COMP_ID == _T04225.T_CH_COMP_ID).FirstOrDefault();
        //    if (check == null)
        //    {
        //        obj.T04225.Add(_T04225);
        //        obj.SaveChanges();
        //    }
        //    else
        //    {
        //        var UpD = obj.T04225.Where(P => P.T_CH_COMP_ID == _T04225.T_CH_COMP_ID).FirstOrDefault();
        //        UpD.T_LANG1_NAME = _T04225.T_LANG1_NAME;
        //        UpD.T_LANG2_NAME = _T04225.T_LANG2_NAME;
        //        obj.SaveChanges();
        //    }

        //}
        //public bool Delete_T04225(int T_CH_COMP_ID)
        //{
        //    var T04225 = obj.T04225.Find(T_CH_COMP_ID);
        //    obj.T04225.Remove(T04225);
        //    Save();
        //    return true;
        //}
        ////For GridData Method
        //public IEnumerable GetGridData(int PageIndex, int PageSize)
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //    try
        //    {
        //        query = obj.T04225.OrderBy(a => a.T_CH_COMP_ID).ToList().Select((a, i) => new
        //        {
        //            RowNumber = i,
        //            T_CH_COMP_ID = a.T_CH_COMP_ID,
        //            T_LANG2_NAME = a.T_LANG2_NAME,
        //            T_LANG1_NAME = a.T_LANG1_NAME
        //        }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
        //        return query;
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //    return query;
        //}

        //public int GetGridData_Search_Count(string searchValue)
        //{
        //    int query = 0;
        //    try
        //    {
        //        if (searchValue != "")
        //        {
        //            query = (from a in obj.T04225
        //                     where a.T_CH_COMP_ID.ToString().Contains(searchValue.ToUpper()) || a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
        //                           a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
        //                     select a).Count();
        //        }
        //        else
        //        {
        //            query = (from a in obj.T04225 select a).Count();
        //        }
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //    return query;
        //}

        ////For Grid Data Search Method
        //public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //    try
        //    {
        //        query = obj.T04225.Where(a => a.T_CH_COMP_ID.ToString().Contains(searchValue) ||
        //                                      a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
        //                                      a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
        //                                      {
        //                                          RowNumber = i,
        //                                          T_CH_COMP_ID = a.T_CH_COMP_ID,
        //                                          T_LANG2_NAME = a.T_LANG2_NAME,
        //                                          T_LANG1_NAME = a.T_LANG1_NAME
        //                                      }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //    return query;
        //}

        //public void Save()
        //{
        //    obj.SaveChanges();
        //}
        //public void Dispose()
        //{
        //    obj.Dispose();
        //}
    }
}