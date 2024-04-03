using SMS.Data;
using SMS.Data.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace SMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
            
        {
            //  AreaRegistration.RegisterAllAreas();
            //  FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //  RouteConfig.RegisterRoutes(RouteTable.Routes);
            ////  InitializeAuthenticationProcess();
            //  BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<StudentEntites>(null);
            //  BundleTable.EnableOptimizations = true;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BundleTable.EnableOptimizations = true;
        }
    }
        //private void InitializeAuthenticationProcess()
        //{
        //    if (!WebSecurity.Initialized)
        //    {
        //        WebSecurity.InitializeDatabaseConnection("StudentEntites", "User", "Userid", "UserName", true);
        //    }

        //}
    }

