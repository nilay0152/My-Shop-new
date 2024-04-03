﻿using SMS.Filters;
using System.Web;
using System.Web.Mvc;

namespace SMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthenticationFilter());
        }
    }
}
