using Kendo.Mvc.Extensions;
using SMS.Data.Database;
using SMS.Model;
using SMS.Service;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly StudentService studentService;
        private readonly TeacherService teacherService;
        private readonly AnnoucementService annoucementService;
        public StudentEntites _db = new StudentEntites();
        public HomeController()
        {
            studentService = new StudentService();
            teacherService = new TeacherService();
            annoucementService = new AnnoucementService();
        }
        public ActionResult Index()
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Home.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            ViewBag.TotalStudent = studentService.TotalStudent();
            ViewBag.TotalTeacher = teacherService.TotalTeacher();
            ViewBag.TotalAnnouncement = annoucementService.GetAllAnnoucement().Count();
            ViewBag.TotalUser = _db.usersProfile.Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Read()
        {
            List<BarChartModel> model = GetList();
            return Json(model);
        }

        
        public List<BarChartModel> GetList()
        {
            var list1 = new List<BarChartModel>();
            var allStudent = _db.students.Where(x => x.Status == true).ToList();

            var studentGroup = from student in allStudent
                               orderby student.Standard ascending
                               group student by student.Standard;
                               
            
            foreach (var student in studentGroup)
            {
                var list = new BarChartModel { Class = student.Key, Student = student.Count() };
                list1.Add(list);
            }
            return list1;
            
        }
    }
}