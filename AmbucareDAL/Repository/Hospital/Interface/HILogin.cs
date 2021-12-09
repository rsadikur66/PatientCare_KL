using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Hospital.Interface
{
    public interface HILogin: IDisposable
    {
        IEnumerable GetUserInfo(string LOGIN_NAME, string PWD);
    }
}
