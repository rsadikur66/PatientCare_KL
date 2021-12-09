using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.DAL;
using AmbucareDAL.Repository.Interface;
using Newtonsoft.Json;


namespace AmbucareDAL.Repository
{
    public class ErrorRepository : CommonDAL,IError
    {
       
        public string SetServerErrorLog(string controller, string action, string user, string message)
        {
            return setServerErrorLog(controller, action, user, message);
        }
        
    }
}