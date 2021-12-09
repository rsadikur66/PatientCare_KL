using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Queries
{
    public interface IQ74145
    {
        DataTable GetAllAcceptRequest(string userid,string from_date,string to_date);
    }
}
