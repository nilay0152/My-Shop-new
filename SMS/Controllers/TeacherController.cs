using SMS.Model;
using SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web;
using System.Web.Mvc;
using SMS.Helper;

namespace SMS.Controllers
{
    public class TeacherController : BaseController
    {
        public readonly TeacherService teacherService;
        public TeacherController()
        {
            teacherService = new TeacherService();
        }
        // GET: Teacher
        public ActionResult Index(string msg)
        {

            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Teacher.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }

            
            ViewBag.Permission = GetPermission(AuthorizeFormAccess.FormAccessCode.Teacher.ToString());
            //List<TeacherModel> teachers = teacherService.GetAllTeacher();

            return View();
        }
        [HttpGet]
        public ActionResult AddTeacher()
        {
            Teacher T1 = new Teacher();
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Teacher.ToString(), AcessPermission.IsAdd))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            return View();
        }


        [HttpPost]
        public ActionResult AddTeacher(TeacherModel teacher)
        {
            teacherService.CreateTeacher(teacher);
            TempData["Message"] = Constants.EmailCodes.TeacherAdded;
            return RedirectToAction("Index","Teacher");
        }
        [HttpGet]
        public ActionResult EditTeacher(int id)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Teacher.ToString(), AcessPermission.IsEdit))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            TeacherModel teacherModel = teacherService.GetTeacherById(id);
            return View(teacherModel);
        }


        [HttpPost]
        public ActionResult EditTeacher(TeacherModel teacherModel)
        {
            TeacherModel teacherModel1 = teacherService.UpdateTeacher(teacherModel);
            TempData["Message"] = Constants.EmailCodes.TeacherEdit;
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int Id)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Teacher.ToString(), AcessPermission.IsDelete))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            teacherService.DeleteTeacher(Id);
            return RedirectToAction("Index");
        }
        //[HttpPost]
        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Teacher.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            var teacherDetails = teacherService.GetAllTeacher();
            return Json(teacherDetails.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        
    }
}