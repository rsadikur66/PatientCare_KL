using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Queries;

namespace AmbucareDAL.Repository.Implementation.Queries
{
    public class Q74138Repository: IQ74138
    {
        private AmbucareContainer obj = new AmbucareContainer();
        public Q74138Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }
        private readonly Q74138DAL _q74138 = new Q74138DAL();
        public DataTable getMapload(string zonCode)
        {
            var data = _q74138.getMapload(zonCode);
            return data;
        }
        //For GridData Method
        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from t26 in obj.T74026
                    where t26.T_USER_ID != null
                    orderby t26.T_ENTRY_TIME descending
                    select new
                    {
                        // RowNumber = i,
                        T_USER_ID = t26.T_USER_ID,
                        T_ENTRY_TIME = t26.T_ENTRY_TIME,
                        T_LATITUDE = t26.T_LATITUDE,
                        T_LONGITUDE = t26.T_LONGITUDE
                    }).AsEnumerable().Select((a, i) => new
                    {
                        RowNumber = i,
                        T_USER_ID = a.T_USER_ID,
                        T_ENTRY_TIME = a.T_ENTRY_TIME,
                        T_LATITUDE = a.T_LATITUDE,
                        T_LONGITUDE = a.T_LONGITUDE
                    }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
                //query = obj.T74026.Where(d=>d.T_USER_ID !=null).OrderBy(a => a.T_ENTRY_TIME ).ToList().Select((a, i) => new
                //{
                //    RowNumber = i,
                //    T_USER_ID = a.T_USER_ID,
                //    T_ENTRY_TIME = a.T_ENTRY_TIME,
                //    T_LATITUDE = a.T_LATITUDE,
                //    T_LONGITUDE = a.T_LONGITUDE
                //}).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
                return query;
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

        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            try
            {
                if (searchValue != "")
                {
                    query = (from a in obj.T74026
                        where a.T_USER_ID.ToUpper().Contains(searchValue.ToUpper()) 
                            select a).Count();

                    //                query = (from a in (from b in obj.T74026
                    //                    select new
                    //                    {
                    //                        T_USER_ID=b.T_USER_ID,
                    //                        T_ENTRY_TIME =   b.T_ENTRY_TIME.Value.Day.ToString() + "/" + b.T_ENTRY_TIME.Value.Month.ToString() + "/" + b.T_ENTRY_TIME.Value.Year.ToString()
                    //                    }).ToList()

                    //               where  a.T_USER_ID.ToUpper().Contains(searchValue.ToUpper()) ||
                    //                      a.T_ENTRY_TIME.ToString().Contains(searchValue.ToUpper()) //||
                    //                       //  a.T_ENTRY_TIME.Value.Day.ToString().Contains(searchValue.ToUpper())
                    //// a.T_ENTRY_TIME.Value.Day.ToString()+"/"+ a.T_ENTRY_TIME.Value.Month.ToString()+"/"+ a.T_ENTRY_TIME.Value.Year.ToString().Contains(searchValue.ToUpper())
                    //                         // a.T_LATITUDE.ToString().Contains(searchValue.ToString()) ||
                    //                         // a.T_LONGITUDE.ToString().Contains(searchValue.ToString())
                    //                         select a).Count();
                }
                else
                {
                    query = (from a in obj.T74026 select a).Count();
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

        //For Grid Data Search Method
        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74026.Where(a => a.T_USER_ID.ToUpper().Contains(searchValue) //||
                                             // a.T_ENTRY_TIME.ToString().Contains(searchValue.ToString()) //||
                                            //  a.T_LATITUDE.ToString().Contains(searchValue.ToString()) ||
                                            //  a.T_LONGITUDE.ToString().Contains(searchValue.ToString())
                                              ).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_USER_ID = a.T_USER_ID,
                    T_ENTRY_TIME = a.T_ENTRY_TIME,
                    T_LATITUDE = a.T_LATITUDE,
                    T_LONGITUDE = a.T_LONGITUDE
                            }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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
    }
}