using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasVidaWebMVC.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ResourceAuthorizeAttribute : AuthorizeAttribute
    {
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            using (MasVidaDataContext db = new MasVidaDataContext())
            {
                var controller = (string)httpContext.Request.RequestContext.RouteData.Values["controller"];
                Resource r = db.Resources.FirstOrDefault(c => c.ControllerName == controller);
                string user = httpContext.User.Identity.Name;
                User user2 = db.Users.FirstOrDefault(u => u.UserName == user);
                if (user2 != null)
                {
                    var ut_r = db.UserTypes_Resources.Find(user2.UserType.UserTypeID, r.ControllerID);
                    if (ut_r.Permit)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public string RedirectUrl = "~/Error/Unauthorized";

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
                filterContext.Result = new RedirectResult(RedirectUrl);
        }
    }
}