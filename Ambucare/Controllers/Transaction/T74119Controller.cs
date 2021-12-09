using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74119Controller : Controller
    {
        private IT74119 repository;
        public T74119Controller(IT74119 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        [HttpPost]
        public ActionResult getSalesData(int T_SL_REQ_ID)
        {
            var data = repository.getSalesData(T_SL_REQ_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getEmpList(int T_STORE_ID)
        {
            var data = repository.getEmpList(T_STORE_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Insert(T74027 t74027,T74036 t74036, List<T74037> t74037List, T74048 t74048, List<T74049> t74049List)
        {
            repository.Insert(t74027,t74036, t74037List, t74048, t74049List);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}