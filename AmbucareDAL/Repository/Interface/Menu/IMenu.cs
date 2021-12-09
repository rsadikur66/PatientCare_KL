using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Menu
{
    public interface IMenu:IDisposable
    {
        IQueryable<T74066> GetAllData { get; }
        IEnumerable MenuData(string T_USER_ID, int T_ROLE_CODE, Int32 T_FORM_TYPE_ID,int lang);
        IEnumerable GetAllMenuData(string T_USER_ID, int T_ROLE_CODE,int lang);
        IEnumerable FormAuthorization(string T_USER_ID, string T_FORM_CODE, int T_ROLE_CODE);
        IQueryable<T74000> LabelData(string T_FORM_CODE, string T_LABEL_NAME);
        void GPS_Insert(decimal latitude, decimal longitude,string user);

        void Save_t74041(string userid,string lat,string lon,string address);
        DataTable GetLatLong(string userid);
        string SaveLatLong(T74026 t74026);
        DataTable GetAllUserMsg(string LANGUAGE,string T_FORM_CODE);
        DataTable getFormName(string lang, string code);
        string updateForm(string user, string form);
    }
}
