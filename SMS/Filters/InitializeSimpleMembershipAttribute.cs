
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using SMS.Model;
using WebSecurity = WebMatrix.WebData.WebSecurity;

namespace SMS.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isIntialized;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isIntialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);
                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }
                    WebSecurity.InitializeDatabaseConnection("StudentEntites", "User", "Userid", "UserName", autoCreateTables: true);

                    string role = "sadmin";
                    string user = "sadmin";
                    string password = "sadmin@123";

                    if (!Roles.RoleExists(role))
                    {
                        Roles.CreateRole(role);
                    }
                    if (!WebSecurity.UserExists(user))
                    {
                        WebSecurity.CreateUserAndAccount(user, password/*, propertyValues: new { Name = "sadmin", Email = "sadmin" }*/);
                    }
                }
                
                catch (Exception Ex)
                {

                    throw new InvalidOperationException("", Ex);
                }
            }
        }
    }
}