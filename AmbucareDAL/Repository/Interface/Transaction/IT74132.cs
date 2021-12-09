using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
   public interface IT74132:IDisposable
   {
       IEnumerable GetRole(string P_USER_ID);
       IEnumerable GetUserType(string P_TYPE);
       IEnumerable GetUserList(string P_USER_TYPE , int P_EMP_TYP_ID, string P_USER_STATUS, int P_FORM_TYPE_ID,string siteCode);
       IEnumerable GetFormType(int? P_ROLE_CODE, string P_USER_ID);
       string GetFormAccess(string P_FORM_CODE, string P_USER_ID, int P_ROLE_CODE);
       IEnumerable GetFormList(int P_FORM_TYPE_ID,int P_ROLE_CODE, string P_USER_ID,string user);
       string Insert(List<T74066> t74066, string status);
       void Save();


       IEnumerable GetFdataByUser(string fmCode, string rCode, string uData);
       IEnumerable GetSiteData(string site);
   }
}
