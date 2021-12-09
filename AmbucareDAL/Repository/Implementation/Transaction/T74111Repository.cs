using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;
using System.Data.Entity;
using System.Configuration;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74111Repository : IT74111
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74111Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public void Dispose()
        {
            try
            {
                obj.Dispose();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
        }

        //For Insert and Update Method
        public void Insert_T74073(List<T74089> _T74089)
        {
            try
            {
                var aa = GetValue();
                foreach (var a in _T74089)
                {
                    var UpD = obj.T74073.Join(obj.T74089,t73=>t73.T_COST_TYPE_DTL_ID,t89=>t89.T_COST_TYPE_DTL_ID, (t73, t89) => new { t73, t89 }).
                        Where(P => P.t73.T_COST_TYPE_DTL_ID == a.T_COST_TYPE_DTL_ID).FirstOrDefault();
                    UpD.t89.T_SALE_PRICE = a.T_SALE_PRICE;
                    Save();
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

        }

        //For Save change Method
        public void Save()
        {
            try
            {
                obj.SaveChanges();
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
        }

        //AmbulanceDropdownList Method Start
        public IEnumerable GetAmbulanceDropdownList()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from itemcost in obj.T74072
                             //join itemdetail in obj.T74073 on itemcost.T_COST_TYPE_ID equals itemdetail.T_COST_TYPE_ID
                         select new
                         {
                             T_COST_TYPE_ID = itemcost.T_COST_TYPE_ID,
                             T_LANG2_NAME = itemcost.T_LANG2_NAME

                         }).AsEnumerable().Select((r, i) => new
                         {
                             T_COST_TYPE_ID = r.T_COST_TYPE_ID,
                             T_LANG2_NAME = r.T_LANG2_NAME
                         }).ToList();
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
        //AmbulanceDropdownList Method End

        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize, int Id)
        {
            //int vv = GetNextSequenceValue();
           // IEnumerable get = GetValue();
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if (Id == 0)
                {
                    query = (from costid in obj.T74072
                             join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                             join t89 in obj.T74089 on costdetils.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                             select new
                             {

                                 T_COST_TYPE_DTL_ID = costdetils.T_COST_TYPE_DTL_ID,
                                 T_LANG2_NAME = costdetils.T_LANG2_NAME,
                                 T_PRICE = t89.T_SALE_PRICE,
                                 T_PRICE_SET = 0,
                                 T_INCHARGABLE = costdetils.T_INCHARGABLE,
                                 T_ACTIVE = costdetils.T_ACTIVE

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_COST_TYPE_DTL_ID,
                                 r.T_LANG2_NAME,
                                 r.T_PRICE,
                                 r.T_PRICE_SET,
                                 r.T_INCHARGABLE,
                                 r.T_ACTIVE
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
                }
                else
                {
                    query = (from costid in obj.T74072
                             join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                        join t89 in obj.T74089 on costdetils.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                             where costid.T_COST_TYPE_ID == Id
                             select new
                             {

                                 T_COST_TYPE_DTL_ID = costdetils.T_COST_TYPE_DTL_ID,
                                 T_LANG2_NAME = costdetils.T_LANG2_NAME,
                                 T_PRICE = t89.T_SALE_PRICE,
                                 T_INCHARGABLE = costdetils.T_INCHARGABLE,
                                 T_ACTIVE = costdetils.T_ACTIVE

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_COST_TYPE_DTL_ID,
                                 r.T_LANG2_NAME,
                                 T_PRICE = r.T_PRICE != null ? r.T_PRICE : 0,
                                 r.T_INCHARGABLE,
                                 r.T_ACTIVE
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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
        public int GetNextSequenceValue()
        {
            var rawQuery = obj.Database.SqlQuery<int>("SELECT T74001_SEQ.NEXTVAL FROM DUAL").First();           
            var nextVal = Convert.ToInt32(rawQuery); 
          
            return nextVal;
        }
        public IEnumerable GetValue()
        {
            // var obj1 = obj.Database.SqlQuery<string>("EXEC GET_AMBULANCE_REGI_INFO @GID", idParam).ToList();
            //var authors = obj.Database.ExecuteSqlCommand("GET_AMBULANCE_REGI_INFO P_AMBU_REG_ID, P_CHASIS_NO, P_ENGINE_NO OUT");
            var blogs = obj.Database.SqlQuery<T74004>("SELECT T74004.T_FAMILY_LANG2_NAME,T74004.T_NTNLTY_ID FROM T74014 inner join T74004 on T74014.T_EMP_ID = T74004.T_EMP_ID").ToList();

            return blogs;
        }
        //For GridData Count Method
        public int GetGridData_Search_Count(string searchValue, int Id)
        {
            int query = 0;
            try
            {

                if (searchValue != "")
                {
                    if (Id == 0)
                    {
                        query = (from costid in obj.T74072
                                 join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                                 where costdetils.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                 select costid).Count();
                    }
                    else
                    {
                        query = (from costid in obj.T74072
                                 join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                                 where costid.T_COST_TYPE_ID == Id && costdetils.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                 select costid).Count();
                    }
                }
                else
                {
                    if (Id == 0)
                    {
                        query = (from costid in obj.T74072
                                 join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                                 select costid).Count();
                    }
                    else
                    {
                        query = (from costid in obj.T74072
                                 join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                                 where costid.T_COST_TYPE_ID == Id
                                 select costid).Count();
                    }
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
        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, int Id)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                if (Id == 0)
                {
                    query = (from costid in obj.T74072
                             join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                             join t89 in obj.T74089 on costdetils.T_COST_TYPE_DTL_ID equals t89.T_COST_TYPE_DTL_ID
                             where costdetils.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                             select new
                             {

                                 T_COST_TYPE_DTL_ID = costdetils.T_COST_TYPE_DTL_ID,
                                 T_LANG2_NAME = costdetils.T_LANG2_NAME,
                                 T_PRICE = t89.T_SALE_PRICE,
                                 T_INCHARGABLE = costdetils.T_INCHARGABLE,
                                 T_ACTIVE = costdetils.T_ACTIVE

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_COST_TYPE_DTL_ID,
                                 r.T_LANG2_NAME,
                                 T_PRICE = r.T_PRICE != null ? r.T_PRICE : 0,
                                 r.T_INCHARGABLE,
                                 r.T_ACTIVE
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
                }
                else
                {
                    query = (from costid in obj.T74072
                             join costdetils in obj.T74073 on costid.T_COST_TYPE_ID equals costdetils.T_COST_TYPE_ID
                             join t89 in obj.T74089 on costdetils.T_COST_TYPE_DTL_ID equals  t89.T_COST_TYPE_DTL_ID
                             where costid.T_COST_TYPE_ID == Id && costdetils.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                             select new
                             {

                                 T_COST_TYPE_DTL_ID = costdetils.T_COST_TYPE_DTL_ID,
                                 T_LANG2_NAME = costdetils.T_LANG2_NAME,
                                 T_PRICE = t89.T_SALE_PRICE,
                                 T_PRICE_SET = 0,
                                 T_INCHARGABLE = costdetils.T_INCHARGABLE,
                                 T_ACTIVE = costdetils.T_ACTIVE

                             }).AsEnumerable().Select((r, i) => new
                             {
                                 RowNumber = i,
                                 r.T_COST_TYPE_DTL_ID,
                                 r.T_LANG2_NAME,
                                 r.T_PRICE,
                                 r.T_PRICE_SET,
                                 r.T_INCHARGABLE,
                                 r.T_ACTIVE
                             }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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

    }
}