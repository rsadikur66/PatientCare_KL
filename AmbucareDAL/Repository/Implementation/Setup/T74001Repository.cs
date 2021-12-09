using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74001Repository:IT74001
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74001Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }
            
        //Form Item DropDown Method Start
        public IQueryable<T74002> GetItemBrandData
        {
            get { return obj.T74002; }
        }
        //Form Item DropDown Method end

        //Form ItemUM DropDown Method Start
        public IEnumerable GetItemUMData(int T_TYPE_ID)
        {
            var TypeId = obj.T74003.Where(p => p.T_TYPE_ID == T_TYPE_ID).ToList();
            return TypeId;
        }
        //Form ItemUM DropDown Method end

        //Form Type DropDown Method Start
        public IQueryable<T74008> GetIType
        {
            get { return obj.T74008; }
        }
        //Form Type DropDown Method end

        public IQueryable<T74001> GetAllItemData
        {
            get { return obj.T74001; }
        }

        public IEnumerable GetLabelData(int PageIndex, int PageSize)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Ambu in obj.T74001
                         join ItemUM in obj.T74003 on Ambu.T_ITEM_UM_ID equals ItemUM.T_ITEM_UM_ID
                         join Type in obj.T74008 on Ambu.T_TYPE_ID equals Type.T_TYPE_ID
                         join Brand in obj.T74002 on Ambu.T_ITEM_BRA_ID equals Brand.T_ITEM_BRA_ID
                         select new
                         {
                    
                            T_ITEM_ID = Ambu.T_ITEM_ID,
                            T_LANG1_NAME = Ambu.T_LANG1_NAME,
                            T_LANG2_NAME = Ambu.T_LANG2_NAME,
                            T_ITEM_BRA_ID=Ambu.T_ITEM_BRA_ID,
                            BrandName = Brand.T_LANG2_NAME,
                            T_TYPE_ID=Ambu.T_TYPE_ID,
                            TypeName = Type.T_LANG2_NAME,
                            T_ITEM_UM_ID=Ambu.T_ITEM_UM_ID,
                            ItemUmName = ItemUM.T_LANG2_NAME
                         }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                    r.T_ITEM_ID,
                    r.T_LANG1_NAME,
                    r.T_LANG2_NAME,
                    r.BrandName,
                    r.T_ITEM_BRA_ID,
                    r.TypeName,
                    r.T_TYPE_ID,
                    r.ItemUmName,
                    r.T_ITEM_UM_ID
                         }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber <= (PageIndex + 1) * PageSize);
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
                    query = (from Ambu in obj.T74001
                        join ItemUM in obj.T74003 on Ambu.T_ITEM_UM_ID equals ItemUM.T_ITEM_UM_ID
                        join Type in obj.T74008 on Ambu.T_TYPE_ID equals Type.T_TYPE_ID
                        join Brand in obj.T74002 on Ambu.T_ITEM_BRA_ID equals Brand.T_ITEM_BRA_ID
                        where Ambu.T_ITEM_ID.ToString().Contains(searchValue) ||
                              Ambu.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Ambu.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              ItemUM.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Type.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Brand.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                        select Ambu).Count();
                }
                else
                {
                    query = (from Ambu in obj.T74001
                        join ItemUM in obj.T74003 on Ambu.T_ITEM_UM_ID equals ItemUM.T_ITEM_UM_ID
                        join Type in obj.T74008 on Ambu.T_TYPE_ID equals Type.T_TYPE_ID
                             join Brand in obj.T74002 on Ambu.T_ITEM_BRA_ID equals Brand.T_ITEM_BRA_ID
                             select Ambu).Count();
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
                query = (from Ambu in obj.T74001
                    join ItemUM in obj.T74003 on Ambu.T_ITEM_UM_ID equals ItemUM.T_ITEM_UM_ID
                    join Type in obj.T74008 on Ambu.T_TYPE_ID equals Type.T_TYPE_ID
                    join Brand in obj.T74002 on Ambu.T_ITEM_BRA_ID equals Brand.T_ITEM_BRA_ID
                         where Ambu.T_ITEM_ID.ToString().Contains(searchValue) ||
                          Ambu.T_LANG1_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          Ambu.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          ItemUM.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          Type.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          Brand.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) 
                    
                    select new
                    {
                        T_ITEM_ID = Ambu.T_ITEM_ID,
                        T_LANG1_NAME = Ambu.T_LANG1_NAME,
                        T_LANG2_NAME = Ambu.T_LANG2_NAME,
                        T_ITEM_BRA_ID = Ambu.T_ITEM_BRA_ID,
                        BrandName = Brand.T_LANG2_NAME,
                        T_TYPE_ID = Ambu.T_TYPE_ID,
                        TypeName = Type.T_LANG2_NAME,
                        T_ITEM_UM_ID = Ambu.T_ITEM_UM_ID,
                        ItemUmName = ItemUM.T_LANG2_NAME
                    }).AsEnumerable().Select((r, i) => new {
                    RowNumber = i,
                    r.T_ITEM_ID,
                    r.T_LANG1_NAME,
                    r.T_LANG2_NAME,
                    r.BrandName,
                    r.T_ITEM_BRA_ID,
                    r.TypeName,
                    r.T_TYPE_ID,
                    r.ItemUmName,
                    r.T_ITEM_UM_ID
                    }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber <= (PageIndex + 1) * PageSize);
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

        public IQueryable<T74001> All
        {
            get { return obj.T74001; }
        }
        public int GetNextSequenceValue()
        {
            var rawQuery = obj.Database.SqlQuery<int>("SELECT T74001_SEQ.NEXTVAL FROM DUAL").First();
            var nextVal = Convert.ToInt32(rawQuery);
            
            return nextVal;
        }
        public bool AddItem(T74001 _T74001)
        {
            try
            {
                
                bool Status = false;

                if (_T74001.T_ITEM_ID == 0)
                {
                    int val = GetNextSequenceValue();
                    _T74001.T_ITEM_ID = val;
                    obj.T74001.Add(_T74001);
                    if (_T74001.T_TYPE_ID == 101)
                    {
                        T74073 _t74073 = new T74073();
                        _t74073.T_ID = val;
                        _t74073.T_COST_TYPE_ID = 121;
                        _t74073.T_LANG1_NAME = _T74001.T_LANG1_NAME;
                        _t74073.T_LANG2_NAME = _T74001.T_LANG2_NAME;
                        _t74073.T_ACTIVE = 1;
                        _t74073.T_ENTRY_USER = _T74001.T_ENTRY_USER;
                        _t74073.T_ENTRY_DATE = DateTime.Now.Date;
                        obj.T74073.Add(_t74073);
                    }
                    
                    Status = true;
                    }
                else
                {

                    var data = obj.T74001.Where(p => p.T_ITEM_ID == _T74001.T_ITEM_ID).FirstOrDefault();
                    if (data.T_ITEM_ID.ToString()!="")
                    {
                        data.T_UPD_USER = _T74001.T_ENTRY_USER;
                        data.T_UPD_DATE = DateTime.Now.Date;
                        data.T_LANG1_NAME = _T74001.T_LANG1_NAME;
                        data.T_LANG2_NAME = _T74001.T_LANG2_NAME;
                        data.T_ITEM_BRA_ID = _T74001.T_ITEM_BRA_ID;
                        data.T_TYPE_ID = _T74001.T_TYPE_ID;
                        data.T_ITEM_UM_ID = _T74001.T_ITEM_UM_ID;
                       }
                }
                Save();
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
        
        public bool Delete(int id)
        {
            try
            {
                var T74001 = obj.T74001.Find(id);
                obj.T74001.Remove(T74001);
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
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}