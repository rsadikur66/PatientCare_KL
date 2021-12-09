using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Schema;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL.Transaction;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74135Repository: IT74135
    {
        private AmbucareContainer obj = new AmbucareContainer();
        public T74135Repository(AmbucareContainer _Obj)
        {
            _Obj.Configuration.ProxyCreationEnabled = false;
            obj = _Obj;
        }

        public void Dispose()
        {
            obj.Dispose();
        }
        public void Save()
        {
            obj.SaveChanges();
        }
    }
}