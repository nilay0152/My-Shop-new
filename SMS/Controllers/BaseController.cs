using SMS.Model;
using SMS.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly FormRoleMappingService formRoleMappingService;

        public BaseController()
        {
            formRoleMappingService = new FormRoleMappingService();
        }
        // GET: Base
        public ActionResult AccessDenied()
        {
            return View();
        }
        public bool CheckPermission(string formCode, string formAction)
        {
            var checkPermission = formRoleMappingService.CheckFormAccess(formCode);
            if (checkPermission != null)
            {
                if (formAction == AcessPermission.IsAdd)
                {
                    return checkPermission.AllowInsert;
                }
                else if (formAction == AcessPermission.IsEdit)
                {
                    return checkPermission.AllowUpdate;
                }
                else if (formAction == AcessPermission.IsDelete)
                {
                    return checkPermission.AllowDelete;
                }
                else if (formAction == AcessPermission.IsMenu)
                {
                    return checkPermission.AllowMenu;
                }
                else if (formAction == AcessPermission.IsView)
                {
                    return checkPermission.AllowView;
                }
                else if (string.IsNullOrWhiteSpace(formAction))
                {
                    if (checkPermission.AllowInsert || checkPermission.AllowUpdate)
                    {
                        return true;
                    }
                }
                
            }
            
            return false;
        }
        public FormRoleMapping GetPermission(string formCode)
        {
            var checkPermission = formRoleMappingService.CheckFormAccess(formCode);
            return checkPermission;
        }
        public string RenderPartialViewToString(Controller controller, string viewName, object model = null)
        {
            if (model != null)
                controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult;
                viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                ViewContext viewContext;
                viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }
    }
}