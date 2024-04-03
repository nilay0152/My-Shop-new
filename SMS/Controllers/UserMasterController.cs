using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SMS.Data.Database;
using SMS.Helper;
using SMS.Model;
using SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class UserMasterController : BaseController
    {
        private readonly UserMasterService userMasterService;
        public UserMasterController()
        {
            userMasterService = new UserMasterService();
        }
        // GET: UserMaster
        public ActionResult DisplayUser()
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Usermaster.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            ViewBag.Permission = GetPermission(AuthorizeFormAccess.FormAccessCode.Usermaster.ToString());
            
            return View();
        }
        public ActionResult EditUserRoleMapping(int id)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Usermaster.ToString(), AcessPermission.IsEdit))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            StudentEntites _db = new StudentEntites();
            ViewBag.RoleList = userMasterService.BindRole();
            User user = userMasterService.GetUserById(id);
            return View(user);
        }


        [HttpPost]
        public ActionResult EditUserRoleMapping(User pur)
        {
            User objPurchaseModel = userMasterService.UpdateUsersRole(pur);
            TempData["Message"] = Constants.EmailCodes.rolenameupdated;
            return RedirectToAction("DisplayUser");
        }
    
        public ActionResult DeleteUser(int id/*, User user*/)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Usermaster.ToString(), AcessPermission.IsDelete))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            userMasterService.DeleteUser(id);
            return RedirectToAction("DisplayUser");
        }
        public ActionResult CreateUser()
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Usermaster.ToString(), AcessPermission.IsAdd))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            User obj = new User();
            
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            userMasterService.CreateUser(user);
            TempData["Message"] = Constants.EmailCodes.UserAdded;
            return RedirectToAction("DisplayUser");
        }
        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Usermaster.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }

            List<User> UserList = userMasterService.GetAllUser();
            return Json(UserList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}