using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface
{
    public interface IError
    {
        string SetServerErrorLog(string controller, string action, string user, string message);

    }
}
