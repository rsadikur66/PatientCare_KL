using AmbucareDAL.Repository.Interface.Transaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using AmbucareDAL.Repository;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74014Repository : IT74014
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74014Repository(AmbucareContainer _Obj)
        {
            obj = _Obj;
        }

        //For Find Method
        public T74014 Find(string id)
        {
            T74014 objEmployee = new T74014();
            try
            {
                objEmployee = obj.T74014.Where(p => p.T_AMBU_REG_ID == Convert.ToInt32(id)).FirstOrDefault();
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
            return objEmployee;
        }

        //For Insert and Update Method
        public void Insert_T74014(T74014 _T74014)
        {
            try
            {
                // var check = obj.T74014.Where(P => P.T_AMBU_REG_ID == _T74014.T_AMBU_REG_ID).FirstOrDefault();
                if (_T74014.T_AMBU_REG_ID == 0)
                {
                    obj.T74014.Add(_T74014);
                }
                else
                {
                    var UpD = obj.T74014.Where(P => P.T_AMBU_REG_ID == _T74014.T_AMBU_REG_ID).FirstOrDefault();
                    if (UpD != null)
                    {
                        UpD.T_AMBU_REG_NUM = _T74014.T_AMBU_REG_NUM;
                        UpD.T_CHASIS_NO = _T74014.T_CHASIS_NO;
                        UpD.T_ENGINE_NO = _T74014.T_ENGINE_NO;
                        UpD.T_WEIGHT = _T74014.T_WEIGHT;
                        UpD.T_REG_IMAGE = _T74014.T_REG_IMAGE;
                        UpD.T_BARCODE_ID = _T74014.T_BARCODE_ID;
                        UpD.T_YEAR_MODEL = _T74014.T_YEAR_MODEL;
                        UpD.T_ITEM_COLOR_ID = _T74014.T_ITEM_COLOR_ID;
                        UpD.T_AMB_TYPE_ID = _T74014.T_AMB_TYPE_ID;
                        UpD.T_BRAND_ID = _T74014.T_BRAND_ID;
                        UpD.T_REG_DATE = _T74014.T_REG_DATE;
                        UpD.T_UPD_USER = _T74014.T_ENTRY_USER;
                        UpD.T_UPD_DATE = DateTime.Now.Date; ;
                        UpD.T_AMBU_REG_NUM = _T74014.T_AMBU_REG_NUM;
                        UpD.T_EMP_ID = _T74014.T_EMP_ID;
                        UpD.T_SERIES = _T74014.T_SERIES;
                    }

                }
                Save();
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
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }

        }

        //For Deleted Method

        public bool Deleted_T74014(int T_AMBU_REG_ID)
        {
            try
            {
                var T74014 = obj.T74014.Find(T_AMBU_REG_ID);
                obj.T74014.Remove(T74014);
                Save();

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
            return true;
        }

        //For GridData Method
        public IEnumerable GetGridData(int PageIndex, int PageSize, string entryuser)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Ambu in obj.T74014
                    join Colorna in obj.T74011 on Ambu.T_ITEM_COLOR_ID equals Colorna.T_ITEM_COLOR_ID
                    join Type in obj.T74047 on Ambu.T_AMB_TYPE_ID equals Type.T_AMBU_TYPE_ID
                    join Brand in obj.T74002 on Ambu.T_BRAND_ID equals Brand.T_ITEM_BRA_ID
                    join Emp in obj.T74004 on Ambu.T_EMP_ID equals Emp.T_EMP_ID into emp_ambu
                    from a in emp_ambu.DefaultIfEmpty()
                    where Ambu.T_ENTRY_USER == entryuser
                    select new
                    {
                        ColorId = Ambu.T_ITEM_COLOR_ID,
                        ColorName = Colorna.T_LANG2_NAME,
                        BrandId = Ambu.T_BRAND_ID,
                        BrandName = Brand.T_LANG2_NAME,
                        AmbuId = Ambu.T_AMB_TYPE_ID,
                        AmbuName = Type.T_LANG2_NAME,
                        EmpId = Ambu.T_EMP_ID,
                        EmpName1 = a.T_FIRST_LANG2_NAME,

                        //EmpName2 = a.T_FATHER_LANG2_NAME,

                        EmpName3 = a.T_LAST_LANG2_NAME,
                        //EmpName4 = a.T_FAMILY_LANG2_NAME,
                        Series = Ambu.T_SERIES,
                        RegDate = Ambu.T_REG_DATE,
                        RegImage = Ambu.T_REG_IMAGE,
                        YearModel = Ambu.T_YEAR_MODEL,
                        AmbuRegId = Ambu.T_AMBU_REG_ID,
                        AmbuRegNo = Ambu.T_AMBU_REG_NUM,
                        BarcodeId = Ambu.T_BARCODE_ID,
                        ChasisNo = Ambu.T_CHASIS_NO,
                        EngineNo = Ambu.T_ENGINE_NO,
                        Weight = Ambu.T_WEIGHT
                    }).AsEnumerable().Select((r, i) => new
                {
                    RowNumber = i,
                    r.AmbuId,
                    r.AmbuName,
                    r.AmbuRegId,
                    r.AmbuRegNo,
                    r.BrandId,
                    r.BrandName,
                    r.ColorId,
                    r.ColorName,
                    r.RegDate,
                    r.RegImage,
                    r.YearModel,
                    r.BarcodeId,
                    r.ChasisNo,
                    r.EngineNo,
                    r.Weight,
                    r.EmpId,
                    EmpName = r.EmpName1 + " " + r.EmpName3,
                    r.Series
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 0 && x.RowNumber < (PageIndex + 1) * PageSize);
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
            return query;
        }

        public int GetRegNo(string regNo)
        {
            int queryResult = 0;
            try
            {
                queryResult = (from regisNo in obj.T74014 where regisNo.T_AMBU_REG_NUM == regNo select regisNo).Count();
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
            return queryResult;
        }

        public int GetChesisNo(string chesisNo)
        {
            int queryResult = 0;
            try
            {
                queryResult = (from chesNo in obj.T74014 where chesNo.T_CHASIS_NO == chesisNo select chesNo).Count();
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
            return queryResult;
        }
        public int GetEngineNo(string engineNo)
        {
            int queryResult = 0;
            try
            {
                queryResult = (from engnNo in obj.T74014 where engnNo.T_ENGINE_NO == engineNo select engnNo).Count();
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
            return queryResult;
        }

        public int GetGridData_Search_Count(string searchValue, string entryuser)
        {
            int query = 0;
            try
            {
                if (searchValue != "")
                {
                    query = (from Ambu in obj.T74014
                        join Colorna in obj.T74011 on Ambu.T_ITEM_COLOR_ID equals Colorna.T_ITEM_COLOR_ID
                        join Type in obj.T74047 on Ambu.T_AMB_TYPE_ID equals Type.T_AMBU_TYPE_ID
                        join Brand in obj.T74002 on Ambu.T_BRAND_ID equals Brand.T_ITEM_BRA_ID
                        join Emp in obj.T74004 on Ambu.T_EMP_ID equals Emp.T_EMP_ID into emp_ambu
                        from a in emp_ambu.DefaultIfEmpty()
                        where Ambu.T_ENTRY_USER == entryuser && (a.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                                                 || a.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                                                 || a.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                                                 || a.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                              Ambu.T_AMBU_REG_NUM.ToUpper().Contains(searchValue.ToUpper()) ||
                              Ambu.T_CHASIS_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                              Ambu.T_ENGINE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                              Colorna.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                              Ambu.T_WEIGHT.ToUpper().Contains(searchValue.ToUpper()) ||
                              Ambu.T_SERIES.ToUpper().Contains(searchValue.ToUpper()) ||
                              Brand.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                        select Ambu).Count();
                }
                else
                {
                    query = (from Ambu in obj.T74014
                        join Color in obj.T74011 on Ambu.T_ITEM_COLOR_ID equals Color.T_ITEM_COLOR_ID
                        where Ambu.T_ENTRY_USER == entryuser
                             select Ambu).Count();
                }
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
            return query;
        }

        //For Grid Data Search Method
        public IEnumerable GetGrid_Data_Search(string searchValue, Int32 PageIndex, Int32 PageSize, string entryuser)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from Ambu in obj.T74014
                    join Colorna in obj.T74011 on Ambu.T_ITEM_COLOR_ID equals Colorna.T_ITEM_COLOR_ID
                    join Type in obj.T74047 on Ambu.T_AMB_TYPE_ID equals Type.T_AMBU_TYPE_ID
                    join Brand in obj.T74002 on Ambu.T_BRAND_ID equals Brand.T_ITEM_BRA_ID
                    join Emp in obj.T74004 on Ambu.T_EMP_ID equals Emp.T_EMP_ID into emp_ambu
                    from a in emp_ambu.DefaultIfEmpty()
                    where Ambu.T_ENTRY_USER == entryuser && (a.T_FIRST_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                                             || a.T_FATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                                             || a.T_GFATHER_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                                                             || a.T_FAMILY_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())) ||
                          Ambu.T_AMBU_REG_NUM.ToUpper().Contains(searchValue.ToUpper()) ||
                          Ambu.T_CHASIS_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                          Ambu.T_ENGINE_NO.ToUpper().Contains(searchValue.ToUpper()) ||
                          Colorna.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper()) ||
                          Ambu.T_WEIGHT.ToUpper().Contains(searchValue.ToUpper()) ||
                          Ambu.T_SERIES.ToUpper().Contains(searchValue.ToUpper()) ||
                          Brand.T_LANG2_NAME.ToUpper().Contains(searchValue.ToUpper())
                    select new
                    {
                        ColorId = Ambu.T_ITEM_COLOR_ID,
                        ColorName = Colorna.T_LANG2_NAME,
                        BrandId = Ambu.T_BRAND_ID,
                        BrandName = Brand.T_LANG2_NAME,
                        AmbuId = Ambu.T_AMB_TYPE_ID,
                        AmbuName = Type.T_LANG2_NAME,
                        EmpId = Ambu.T_EMP_ID,
                        EmpName1 = a.T_FIRST_LANG2_NAME,
                        EmpName2 = a.T_FATHER_LANG2_NAME,
                        EmpName3 = a.T_GFATHER_LANG2_NAME,
                        EmpName4 = a.T_FAMILY_LANG2_NAME,
                        Series = Ambu.T_SERIES,
                        RegDate = Ambu.T_REG_DATE,
                        RegImage = Ambu.T_REG_IMAGE,
                        YearModel = Ambu.T_YEAR_MODEL,
                        AmbuRegId = Ambu.T_AMBU_REG_ID,
                        AmbuRegNo = Ambu.T_AMBU_REG_NUM,
                        BarcodeId = Ambu.T_BARCODE_ID,
                        ChasisNo = Ambu.T_CHASIS_NO,
                        EngineNo = Ambu.T_ENGINE_NO,
                        Weight = Ambu.T_WEIGHT
                    }).AsEnumerable().Select((r, i) => new
                {
                    RowNumber = i,
                    r.AmbuId,
                    r.AmbuName,
                    r.AmbuRegId,
                    r.AmbuRegNo,
                    r.BrandId,
                    r.BrandName,
                    r.ColorId,
                    r.ColorName,
                    r.RegDate,
                    r.RegImage,
                    r.YearModel,
                    r.BarcodeId,
                    r.ChasisNo,
                    r.EngineNo,
                    r.Weight,
                    r.EmpId,
                    EmpName = r.EmpName1 + " " + r.EmpName2 + " " + r.EmpName3 + " " + r.EmpName3,
                    r.Series
                }).ToList().Where(x => x.RowNumber >= PageIndex * PageSize + 1 &&
                                       x.RowNumber <= (PageIndex + 1) * PageSize);


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
            return query;
        }

        //For Dispose Method
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

        //Get Color Id Method Start
        public IQueryable<T74011> GetColorId
        {
            get { return obj.T74011; }
        }

        //Get Color Id Method End
        //Get Ambulance type Id Method Start
        public IQueryable<T74047> GetAmbulanceId
        {
            get { return obj.T74047; }
        }

        //Get Ambulance type Id Method End
        //Get Employee type Id Method Start
        public IQueryable<T74002> GetBrandId
        {
            get { return obj.T74002; }
        }

        //Get Employee type Id Method End
        //Get Employee type Id Method Pop up Start
        public IQueryable<T74004> GetEmployeeData
        {
            get { return obj.T74004; }
        }

        public IEnumerable getAmbulanceOwnerData( string user)
        {
            IEnumerable query = Enumerable.Empty<object>();
            query = (from t04 in obj.T74004
                join t05 in obj.T74005 on t04.T_EMP_TYP_ID equals t05.T_EMP_TYP_ID
                where t04.T_EMP_TYP_ID == 22 && t04.T_ENTRY_USER == user
                     select new
                {
                    T_EMP_ID= t04.T_EMP_ID,
                    T_ENTRY_USER= t04.T_ENTRY_USER,
                   T_NAMEFIRST= t04.T_FIRST_LANG2_NAME ,
                  T_NAMELEST= t04.T_LAST_LANG2_NAME,
                      
                     }).AsEnumerable().Select((r, i) => new
               {
                  // RowNumber = i,
                  T_EMP_ID = r.T_EMP_ID,
                  T_ENTRY_USER= r.T_ENTRY_USER ,
                  T_NAME = r.T_NAMEFIRST + " "+ r.T_NAMELEST,
                    
                     }).ToList();
            return query;
        }
        //Get Employee type Id Method End
    }
}