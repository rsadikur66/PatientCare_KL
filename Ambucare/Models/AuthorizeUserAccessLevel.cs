using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using AmbucareDAL.Models;
using AmbucareDAL.Repository.Implementation.Menu;
using AmbucareDAL.Repository.Interface.Menu;

namespace Ambucare.Models
{
    public class AuthorizeUserAccessLevel: AuthorizeAttribute
    {
        public string UserRole { get; set; }
        private IMenu repo = new MenuRepository();
        protected override bool AuthorizeCore(HttpContextBase httpcontext)
        {
            var request = httpcontext.Request;
            string action = request.RequestContext.RouteData.Values["action"].ToString();
            string User = String.Empty;
            int Role = 0;
            if (HttpContext.Current.Session["T_USER_ID"] != null){User = HttpContext.Current.Session["T_USER_ID"].ToString();}
            if (HttpContext.Current.Session["T_ROLE_CODE"] != null) { Role = Int32.Parse(HttpContext.Current.Session["T_ROLE_CODE"].ToString()); }
            var dt=repo.FormAuthorization(action, User, Role).Count();           
            if (dt > 0){return true;}else{return false;}
        }

    }
}