using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SMS.Helper;
using SMS.Model;

using SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class StudentController : BaseController
    {
        public readonly StudentService _studentService;


        public StudentController()
        {
            _studentService = new StudentService();


        }
        public ActionResult Index()
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Student.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            ViewBag.Permission = GetPermission(AuthorizeFormAccess.FormAccessCode.Student.ToString());
            return View();
        }
        public ActionResult Create()
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Student.ToString(), AcessPermission.IsAdd))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            StudentModel s1 = new StudentModel();
            return View(s1);
        }
        [HttpPost]
        public ActionResult Create(StudentModel student)
        {
            _studentService.CreateStudent(student);                      
            TempData["Message"] = Constants.EmailCodes.STUDENTADDED;
            return RedirectToAction("Index", "Student");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Student.ToString(), AcessPermission.IsEdit))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            StudentModel student = _studentService.GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(StudentModel student)
        {
            StudentModel student1 = _studentService.UpdateStudent(student);
            TempData["Message"] = Constants.EmailCodes.StudentEdit;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string StudentId)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Student.ToString(), AcessPermission.IsDelete))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            _studentService.DeleteStudent(StudentId);
            return RedirectToAction("Index");
        }
        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Student.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            
            var studentlist = _studentService.GetallStudent();
            return Json(studentlist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        } 
        
    }
}