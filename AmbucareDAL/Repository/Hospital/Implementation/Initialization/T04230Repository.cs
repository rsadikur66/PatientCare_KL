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
    public class T04230Repository 
    {
        //private HospitalEntities obj = new HospitalEntities();

        //public T04230Repository(HospitalEntities _Obj)
        //{
        //    obj = _Obj;
        //}

        //public T04230 Find(Int32 id)
        //{
        //    T04230 objEmployee = new T04230();
        //    try
        //    {
        //        //objEmployee = obj.T04230.Where(p => p.T_TRIAGE_SEQ == Convert.ToInt32(id)).FirstOrDefault();
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
        //public void AddJob(T04230 _T04230)
        //{
        //    var check = obj.T04230.Where(P => P.T_TRIAGE_SEQ == _T04230.T_TRIAGE_SEQ).FirstOrDefault();
        //    if (check == null)
        //    {
        //        obj.T04230.Add(_T04230);
        //        obj.SaveChanges();
        //    }
        //    else
        //    {
        //        var UpD = obj.T04230.Where(P => P.T_TRIAGE_SEQ == _T04230.T_TRIAGE_SEQ).FirstOrDefault();
        //        UpD.T_LANG1_NAME = _T04230.T_LANG1_NAME;
        //        UpD.T_LANG2_NAME = _T04230.T_LANG2_NAME;
        //        obj.SaveChanges();
        //    }

        //}
        //public bool Delete_T04230(int T_TRIAGE_SEQ)
        //{
        //    var T04230 = obj.T04230.Find(T_TRIAGE_SEQ);
        //    obj.T04230.Remove(T04230);
        //    Save();
        //    return true;
        //}
        ////For GridData Method
        //public IEnumerable GetGridData(int PageIndex, int PageSize)
        //{
        //    IEnumerable query = Enumerable.Empty<object>();
        //    try
        //    {
        //        query = obj.T04230.OrderBy(a => a.T_TRIAGE_SEQ).ToList().Select((a, i) => new
        //        {
        //            RowNumber = i,
        //            T_TRIAGE_SEQ = a.T_TRIAGE_SEQ,
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
        //            query = (from a in obj.T04230
        //                where a.T_TRIAGE_SEQ.ToString().Contains(searchValue.ToUpper()) || a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
        //                      a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
        //                select a).Count();
        //        }
        //        else
        //        {
        //            query = (from a in obj.T04230 select a).Count();
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
        //        query = obj.T04230.Where(a => a.T_TRIAGE_SEQ.ToString().Contains(searchValue) ||
        //                                      a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
        //                                      a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
        //        {
        //            RowNumber = i,
        //            T_TRIAGE_SEQ = a.T_TRIAGE_SEQ,
        //            T_LANG2_NAME = a.T_LANG2_NAME,
        //            T_LANG1_NAME = a.T_LANG1_NAME
        //        }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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