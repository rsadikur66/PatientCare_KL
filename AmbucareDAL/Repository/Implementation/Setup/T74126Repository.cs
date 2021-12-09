using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace AmbucareDAL.Repository.Implementation.Setup
{
    public class T74126Repository : IT74126
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public T74126Repository(AmbucareContainer _Obj)
        {
            //_Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public IEnumerable GetEmployee()
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {

                return obj.T74004.Where(s => s.T_EMP_TYP_ID ==22).Select(m => new {m.T_FIRST_LANG2_NAME, m.T_EMP_ID})
                    .ToList();
                //return db.mytable.Where(c => c.ID_fk == p_ID).ToList();
                //query = (from emp in obj.T74004
                //    select new
                //    {
                //        T_FIRST_LANG2_NAME = emp.T_FIRST_LANG2_NAME,
                //        T_EMP_ID = emp.T_EMP_ID

                //    }).ToList();
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

        public bool insert_T74126(T74004 t74004, List<T74093> t74093)
        {
            try
            {
                if (t74004.T_EMP_ID == 0)
                {
                    obj.T74004.Add(t74004);
                    Save();
                }
                else
                {
                    obj.Entry(t74004).State = System.Data.Entity.EntityState.Modified;
                    //var check = obj.T74004.Where(p => p.T_EMP_ID == t74004.T_EMP_ID).FirstOrDefault();
                    //if (check != null)
                    //{
                    //    check.T_UPD_DATE = t74004.T_ENTRY_DATE;
                    //}

                    Save();
                }

                if (t74004.T_EMP_ID == 0)
                {
                    int T_EMP_ID = obj.T74004.Max(a => a.T_EMP_ID); //Where(m => m.T_PR_RCV_ID == t74060.T_PR_RCV_ID)
                    foreach (var d in t74093)
                    {
                        if (d.T_ITEM_ID == 0)
                        {
                            var t93 = obj.T74093.Count();
                            if (t93 == 0)
                            {
                                d.T_ITEM_ID = t93 + 1;
                                d.T_EMP_ID = T_EMP_ID;
                                d.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                d.T_ENTRY_DATE = DateTime.Now;
                                obj.T74093.Add(d);
                                Save();
                            }
                            else
                            {
                                var max = obj.T74093.Max(a => a.T_ITEM_ID);
                                d.T_ITEM_ID = max + 1;
                                d.T_EMP_ID = T_EMP_ID;
                                d.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                d.T_ENTRY_DATE = DateTime.Now;
                                obj.T74093.Add(d);
                                Save();
                            }

                        }

                    }
                }
                else
                {
                    foreach (var i in t74093)
                    {
                        if (i.T_ITEM_ID != 0)
                        {
                            var abc = obj.T74093.Where(x => x.T_ITEM_ID == i.T_ITEM_ID).FirstOrDefault();
                            //abc.T_REGI_NO = i.T_REGI_NO;
                            abc.T_MODEL_NO = i.T_MODEL_NO;
                            abc.T_ENGIN_NO = i.T_ENGIN_NO;
                            abc.T_CHESES_NO = i.T_CHESES_NO;
                            abc.T_DESC = i.T_DESC;
                            Save();
                        }
                        else
                        {
                            /*int T_EMP_ID = obj.T74004.Max(a => a.T_EMP_ID);*/ //Where(m => m.T_PR_RCV_ID == t74060.T_PR_RCV_ID)
                            foreach (var d in t74093)
                            {
                                if (d.T_ITEM_ID == 0)
                                {
                                    var t93 = obj.T74093.Count();
                                    if (t93 == 0)
                                    {
                                        d.T_ITEM_ID = t93 + 1;
                                        //d.T_EMP_ID = T_EMP_ID;
                                        d.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                        d.T_ENTRY_DATE = DateTime.Now;
                                        obj.T74093.Add(d);
                                        Save();
                                    }
                                    else
                                    {
                                        var max = obj.T74093.Max(a => a.T_ITEM_ID);
                                        d.T_ITEM_ID = max + 1;
                                        //d.T_EMP_ID = T_EMP_ID;
                                        d.T_ENTRY_USER = HttpContext.Current.Session["T_USER_ID"].ToString();
                                        d.T_ENTRY_DATE = DateTime.Now;
                                        obj.T74093.Add(d);
                                        Save();
                                    }

                                }

                            }
                        }
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
            return true;
        }

        public void Dispose()
        {
            obj.Dispose();
        }
        public void Save()
        {
            obj.SaveChanges();
        }

        public IEnumerable GetEmProData(int T_EMP_ID)
        {
            IEnumerable query = Enumerable.Empty<object>();
            try
            {
                query = (from emp in obj.T74004
                         from pro in obj.T74093.Where(x => x.T_EMP_ID == emp.T_EMP_ID)
                         
                         where (emp.T_EMP_STATUS != null && emp.T_EMP_ID == T_EMP_ID )
                         select new
                         {
                             T_EMP_ID = emp.T_EMP_ID,
                             T_FIRST_LANG2_NAME = emp.T_FIRST_LANG2_NAME,
                             T_FATHER_LANG2_NAME = emp.T_FATHER_LANG2_NAME,
                             T_ADDRESS1 = emp.T_ADDRESS1,
                             T_ADDRESS2 = emp.T_ADDRESS2,
                             T_EMP_STATUS = emp.T_EMP_STATUS,
                             T_MOBILE_NO = emp.T_MOBILE_NO,
                             T_EMAIL_ID = emp.T_EMAIL_ID,
                             T_BARCODE = pro.T_BARCODE,
                             T_CHESES_NO = pro.T_CHESES_NO,
                             T_DESC = pro.T_DESC,
                             T_ENGIN_NO = pro.T_ENGIN_NO,
                             T_ITEM_ID = pro.T_ITEM_ID,
                             T_MODEL_NO = pro.T_MODEL_NO,
                             T_REGI_NO = pro.T_REGI_NO
                         }).ToList();

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
    }
}