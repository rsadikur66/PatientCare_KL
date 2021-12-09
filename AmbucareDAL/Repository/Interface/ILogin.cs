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
    public interface ILogin : IDisposable
    {
        IEnumerable GetUserInfo(string LOGIN_NAME, string PWD);
        void UpdateLogin(string userId);
        IEnumerable checkAmbulance(string USER_ID);
        void UpdateLogout();

    }
}
