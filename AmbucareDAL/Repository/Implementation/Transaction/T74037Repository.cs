using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace AmbucareDAL.Repository.Implementation.Transaction
{
    public class T74037Repository : IT74037
    {
        private AmbucareContainer obj = new AmbucareContainer();

        public IQueryable<T74054> GetCompanyData
        {

            get { return obj.T74054; }

        }

        public IQueryable<T74005> GetEmployeeData
        {

            get { return obj.T74005; }

        }
        public void Dispose()
        {
            obj.Dispose();
        }
    }
}