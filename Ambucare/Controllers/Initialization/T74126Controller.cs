using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Setup;

namespace Ambucare.Controllers.Initialization
{
    public class T74126Controller : Controller
    {

        private IT74126 repository;

        public T74126Controller(IT74126 ObjectIRepository)
        {
            repository = ObjectIRepository;
        }
        // GET: T74126
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult insert_T74126( T74004 t74004, List<T74093> t74093)
        {
            var receiveBy = repository.insert_T74126(t74004, t74093);
            return Json(receiveBy, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEmployee()
        {
            var data = repository.GetEmployee();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmProData(int T_EMP_ID)
        {
            var list = repository.GetEmProData(T_EMP_ID);
            //string JSONString = string.Empty;
            //JSONString = JsonConvert.SerializeObject(list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}