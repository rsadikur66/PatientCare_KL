using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.DAL.Setup;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74112Repository : IT74112
    {
        private AmbucareContainer obj = new AmbucareContainer();
        CommonDAL common = new CommonDAL();
        private readonly T74205DAL _t74205dal;

        public T74112Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
            _t74205dal = new T74205DAL();
        }

        //For GET DATA START

        public IQueryable<T74050> GetItem
        {
            get { return obj.T74050.OrderBy(y => y.T_SEX_CODE); }
        } //Get Gender Item

        public IQueryable<T74051> GetMaritalItem
        {
            get { return obj.T74051.OrderBy(y => y.T_MRTL_STATUS_CODE); }
        }

        public IQueryable<T02003> GetNationalityItem
        {
            get { return obj.T02003.OrderBy(z => z.T_NTNLTY_ID); }
        }

        public IQueryable<T74059> GetReligionItem
        {
            get { return obj.T74059.OrderBy(z => z.T_RLGN_CODE); }
        }

        //For GET DATA END


        public void Dispose()
        {
            obj.Dispose();
        }
        public IQueryable<T74050> GetLabelDataT74050
        {
            get { return obj.T74050; }
        }



        //FOR INSERT AND UPDATE START
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

        public bool InsertOrUpdateT74050(T74050 _T74050)
        {
            try
            {
                bool Status = false;
                if (_T74050.T_SEX_CODE == 0)
                {
                    // New entity
                    obj.T74050.Add(_T74050);
                    Save();
                    Status = true;
                }
                else
                {
                    // Existing entity
                    obj.Entry(_T74050).State = System.Data.Entity.EntityState.Modified;
                    Save();
                    Status = true;
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

        public bool InsertOrUpdateT74051(T74051 _T74051)
        {
            try
            {
                bool Status = false;
                if (_T74051.T_MRTL_STATUS_CODE == 0)
                {
                    // New entity
                    obj.T74051.Add(_T74051);
                    Save();
                    Status = true;
                }
                else
                {
                    // Existing entity
                    obj.Entry(_T74051).State = System.Data.Entity.EntityState.Modified;
                    Save();
                    Status = true;
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

        public bool InsertOrUpdateT02003(T02003 t02003, string userid)
        {
            try
            {
                bool Status = false;
                if (t02003.T_NTNLTY_ID == 0)
                {
                    T02003 t03 = new T02003();
                    t03.T_ENTRY_DATE = common.dateTime();
                    t03.T_ENTRY_USER = userid;
                    decimal a = obj.T02003.Max(x => x.T_NTNLTY_ID);
                   
                    Int16 aa = (Int16)(a + 1);
                    t03.T_NTNLTY_ID = aa  ;
                    //  t74205.T_NTNLTY_CODE =  aa;

                    //byte[] array2 = Encoding.UTF8.GetBytes(_t02003.T_LANG2_NAME);
                    //byte[] array1 = Encoding.UTF8.GetBytes(_t02003.T_LANG1_NAME);
                    t03.T_LANG2_NAME = t02003.T_LANG2_NAME;
                    t03.T_LANG1_NAME = t02003.T_LANG1_NAME;

                    obj.T02003.Add(t03);
                    Save();
                    Status = true;
                }
                else
                {
                    var data = obj.T02003.Where(p => p.T_NTNLTY_ID == t02003.T_NTNLTY_ID).FirstOrDefault();
                    //data.T_UPD_DATE = common.dateTime();
                    // data.T_UPD_USER = userid;
                    //  data.T_LANG2_NAME = _t02003.T_LANG2_NAME;
                    // data.T_LANG1_NAME = _t02003.T_LANG1_NAME;

                    //---------for checking start---------------
                    //const string inputSring = "GÜNEY KIBRIS RUM YÖNETİMİ";
                    //byte[] bArray = Encoding.UTF8.GetBytes(inputSring);
                    //var dd = Encoding.UTF8.GetString(bArray);
                    //---------for checking end---------------

                     //byte[] array2 = Encoding.UTF8.GetBytes(_t02003.T_LANG2_NAME); 
                     //byte[] array1 = Encoding.UTF8.GetBytes(_t02003.T_LANG1_NAME);
                     data.T_LANG2_NAME = t02003.T_LANG2_NAME;
                     data.T_LANG1_NAME = t02003.T_LANG1_NAME;
                    // data.T_PRIM_LANG = _t02003.T_PRIM_LANG;
                    // data.T_SECOND_LANG = _t02003.T_SECOND_LANG;
                    Save();
                    Status = true;
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

        public bool InsertOrUpdateT74059(T74059 _T74059)
        {
            try
            {
                bool Status = false;
                if (_T74059.T_RLGN_CODE == 0)
                {
                    // New entity
                    obj.T74059.Add(_T74059);
                    Save();
                    Status = true;
                }
                else
                {
                    // Existing entity
                    obj.Entry(_T74059).State = System.Data.Entity.EntityState.Modified;
                    Save();
                    Status = true;
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

        
        //FOR DELETE DATA
        public bool Delete(int id)
        {
            try
            {
                var T74050 = obj.T74050.Find(id);
                obj.T74050.Remove(T74050);
                Save();
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

        public bool DeleteMarital(int id)
        {
            try
            {
                var T74051 = obj.T74051.Find(id);
                obj.T74051.Remove(T74051);
                Save();
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

        public bool DeleteNationality(int id)
        {
            try
            {
                var T02003 = obj.T02003.Find(id);
                obj.T02003.Remove(T02003);
                Save();
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

        public bool DeleteReligion(int id)
        {
            try
            {
                var T74059 = obj.T74059.Find(id);
                obj.T74059.Remove(T74059);
                Save();
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

        
        //Searching for Gender
        public IEnumerable GetGridData(int PageIndex, int PageSize)
        {
            var query = obj.T74050.OrderBy(a => a.T_SEX_CODE).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_SEX_CODE = a.T_SEX_CODE,
                T_LANG2_NAME = a.T_LANG2_NAME,
                T_LANG1_NAME = a.T_LANG1_NAME,
                T_SHORT_GNDR_NAME = a.T_SHORT_GNDR_NAME
            }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            return query;
        }

        public int GetGridData_Search_Count(string searchValue)
        {
            int query = 0;
            if (searchValue != "")
            {
                query = (from a in obj.T74050
                         where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               a.T_SHORT_GNDR_NAME.ToUpper().Contains(searchValue.ToUpper())
                         select a).Count();
            }
            else
            {
                query = (from a in obj.T74050 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74050.Where(a => a.T_SEX_CODE.ToString().Contains(searchValue) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_SHORT_GNDR_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_SEX_CODE = a.T_SEX_CODE,
                                                  T_LANG2_NAME = a.T_LANG2_NAME,
                                                  T_LANG1_NAME = a.T_LANG1_NAME,
                                                  T_SHORT_GNDR_NAME = a.T_SHORT_GNDR_NAME
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

        //Searching for Gender End


        public IEnumerable GetGridDataMarital(int PageIndex, int PageSize)
        {
            var query = obj.T74051.OrderBy(a => a.T_MRTL_STATUS_CODE).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_MRTL_STATUS_CODE = a.T_MRTL_STATUS_CODE,
                T_LANG2_NAME = a.T_LANG2_NAME,
                T_LANG1_NAME = a.T_LANG1_NAME
            }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            return query;
        }

        public int GetGridDataMarital_Search_Count(string searchValue)
        {
            int query = 0;
            if (searchValue != "")
            {
                query = (from a in obj.T74051
                         where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                               a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                         select a).Count();
            }
            else
            {
                query = (from a in obj.T74051 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGridMarital_Data_Search(string searchValue, int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74051.Where(a => a.T_MRTL_STATUS_CODE.ToString().Contains(searchValue) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_MRTL_STATUS_CODE = a.T_MRTL_STATUS_CODE,
                                                  T_LANG2_NAME = a.T_LANG2_NAME,
                                                  T_LANG1_NAME = a.T_LANG1_NAME
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
        //Searching Marital End


        public DataTable GetGridDataNationality()
        {
            var data = _t74205dal.GetGridDataNationality();
            return data;
        }


        public IEnumerable GetGridDataNationality(int PageIndex, int PageSize)
        {
            var query = obj.T02003.OrderBy(a => a.T_NTNLTY_ID).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_NTNLTY_ID = a.T_NTNLTY_ID,
                //T_LANG2_NAME = Encoding.UTF8.GetString(a.T_LANG2_NAME),
                //T_LANG1_NAME = Encoding.UTF8.GetString(a.T_LANG1_NAME),
                T_LANG2_NAME = a.T_LANG2_NAME,
                T_LANG1_NAME = a.T_LANG1_NAME
                //T_PRIM_LANG = a.T_PRIM_LANG,
                //T_SECOND_LANG = a.T_SECOND_LANG
            }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            return query;
        }

        public int GetGridDataNationality_Search_Count(string searchValue)
        {
            int query = 0;
            if (searchValue != "")
            {
                var newlist = (from a in obj.T02003
                               select new
                    {
                        T_NTNLTY_ID = a.T_NTNLTY_ID,
                        T_LANG2_NAME = a.T_LANG2_NAME,
                        T_LANG1_NAME = a.T_LANG1_NAME
                    }).ToList().Select((d, i) => new
                {
                    RowNumber = i,
                    T_NTNLTY_ID = d.T_NTNLTY_ID,
                    //T_LANG2_NAME = Encoding.UTF8.GetString(d.T_LANG2_NAME),
                    //T_LANG1_NAME = Encoding.UTF8.GetString(d.T_LANG1_NAME)
                    T_LANG2_NAME = d.T_LANG2_NAME,
                    T_LANG1_NAME = d.T_LANG1_NAME
                });

                query = (from a in newlist
                         where a.T_LANG2_NAME .ToUpper().Contains(searchValue.ToUpper()) ||
                               a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) //||
                             //  a.T_PRIM_LANG.ToUpper().Contains(searchValue.ToUpper()) ||
                              // a.T_SECOND_LANG.ToUpper().Contains(searchValue.ToUpper())
                         select a).Count();
            }
            else
            {
                query = (from a in obj.T02003 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGridNationality_Data_Search(string searchValue, int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                var newlist = (from a in obj.T02003
                               select new
                    {
                        T_NTNLTY_ID=  a.T_NTNLTY_ID,
                        T_LANG2_NAME= a.T_LANG2_NAME,
                        T_LANG1_NAME= a.T_LANG1_NAME
                    }).ToList().Select((d, i) => new
                {
                    RowNumber = i,
                    T_NTNLTY_ID = d.T_NTNLTY_ID,
                        //T_LANG2_NAME = Encoding.UTF8.GetString(d.T_LANG2_NAME),
                        //T_LANG1_NAME = Encoding.UTF8.GetString(d.T_LANG1_NAME)
                        T_LANG2_NAME = d.T_LANG2_NAME,
                        T_LANG1_NAME = d.T_LANG1_NAME
                    });

                query = newlist.Where(b => b.T_NTNLTY_ID.ToString().Contains(searchValue) ||
                                             b.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              b.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) //||
                                             // a.T_PRIM_LANG.ToUpper().Contains(searchValue.ToUpper()) ||
                                             // a.T_SECOND_LANG.ToUpper().Contains(searchValue.ToUpper())
                                              ).ToList().Select((c, i) => new
                                              {
                                                  RowNumber = i,
                                                  T_NTNLTY_ID = c.T_NTNLTY_ID,
                                                  T_LANG2_NAME = c.T_LANG2_NAME,
                                                  T_LANG1_NAME = c.T_LANG1_NAME,
                                                  //  T_PRIM_LANG = a.T_PRIM_LANG,
                                                  // T_SECOND_LANG = a.T_SECOND_LANG
                                              }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();

                //query = obj.T74205.Where(a => a.T_NTNLTY_ID.ToString().Contains(searchValue) ||
                //                              Encoding.UTF8.GetString(a.T_LANG2_NAME).ToUpper().Contains(searchValue.ToUpper()) ||
                //                              Encoding.UTF8.GetString(a.T_LANG1_NAME).ToUpper().Contains(searchValue.ToUpper()) //||
                //    // a.T_PRIM_LANG.ToUpper().Contains(searchValue.ToUpper()) ||
                //    // a.T_SECOND_LANG.ToUpper().Contains(searchValue.ToUpper())
                //).ToList().Select((a, i) => new
                //{
                //    RowNumber = i,
                //    T_NTNLTY_ID = a.T_NTNLTY_ID,
                //    T_LANG2_NAME = Encoding.UTF8.GetString(a.T_LANG2_NAME),
                //    T_LANG1_NAME = Encoding.UTF8.GetString(a.T_LANG1_NAME),
                //    //  T_PRIM_LANG = a.T_PRIM_LANG,
                //    // T_SECOND_LANG = a.T_SECOND_LANG
                //}).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);

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
        //Searching Nationality End


        public IEnumerable GetGridDataReligion(int PageIndex, int PageSize)
        {
            var query = obj.T74059.OrderBy(a => a.T_RLGN_CODE).ToList().Select((a, i) => new
            {
                RowNumber = i,
                T_RLGN_CODE = a.T_RLGN_CODE,
                T_LANG2_NAME = a.T_LANG2_NAME,
                T_LANG1_NAME = a.T_LANG1_NAME
            }).Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize).ToList();
            return query;
        }

        public int GetGridDataReligion_Search_Count(string searchValue)
        {
            int query = 0;
            if (searchValue != "")
            {
                query = (from a in obj.T74059
                    where a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())
                    select a).Count();
            }
            else
            {
                query = (from a in obj.T74059 select a).Count();
            }

            return query;
        }

        public IEnumerable GetGridReligion_Data_Search(string searchValue, int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = obj.T74059.Where(a => a.T_RLGN_CODE.ToString().Contains(searchValue) ||
                                              a.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                                              a.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper())).ToList().Select((a, i) => new
                {
                    RowNumber = i,
                    T_RLGN_CODE = a.T_RLGN_CODE,
                    T_LANG2_NAME = a.T_LANG2_NAME,
                    T_LANG1_NAME = a.T_LANG1_NAME
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
        //Searching Religion End
    }
}