using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class T74122Controller : Controller
    {
        private IT74122 repository;
        public T74122Controller(IT74122 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74122
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetTransferData(int T_TRANSFER_REQ_ID)
        {
            var data = repository.GetTransferData(T_TRANSFER_REQ_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
            
        }

        //public ActionResult GetTransferList_85(int T_TRANSFER_REQ_ID)
        //{
        //    var data = repository.GetTransferList_85(T_TRANSFER_REQ_ID);
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult GetReceiveBy(int T_RCV_TO_STR_ID)
        {
            var receiveBy = repository.GetReceiveBy(T_RCV_TO_STR_ID);

            return Json(receiveBy, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveData(List<T74080> t74080,T74084 t74084, List<T74085> t74085,List<T74027> t74027)
        {
           var receiveBy = repository.SaveData(t74080,t74084, t74085, t74027);

            return Json(receiveBy, JsonRequestBehavior.AllowGet);
        }


    }
}