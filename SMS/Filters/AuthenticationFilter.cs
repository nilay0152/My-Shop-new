using SMS.Controllers;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMS.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Controller controller = filterContext.Controller as Controller;
            if (controller != null && !(controller is AccountController)
                && SessionHelper.UserId == 0)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Account" },
                    { "Action", "Login"},
                        { "returnUrl", filterContext.HttpContext.Request.RawUrl}
                    });
            }
            base.OnActionExecuted(filterContext);
        }
    }
}