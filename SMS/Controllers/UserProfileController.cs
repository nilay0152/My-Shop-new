using SMS.Helper;
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
    public class UserProfileController : BaseController
    {
        public readonly UserProfileService userProfileService;
        public UserProfileController()
        {
            userProfileService = new UserProfileService();
        }
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditUserProfile()
        {

            User userProfileModel = userProfileService.GetUserProfileById();
            return View(userProfileModel);
        }


        [HttpPost]
        public ActionResult EditUserProfile(User userProfileModel) 
        {
            //User obj = new User();
            //obj.profileImage = userProfileModel.Userid + Path.GetExtension(profileImage.FileName);
            //profileImage.SaveAs(Server.MapPath("~/Content/UserPics") + userProfileModel.profileImage + userProfileModel.Userid);
            //userProfileModel.profileImage = profileImage.FileName;
            userProfileService.UpdateUserProfile(userProfileModel);           
            return RedirectToAction("Index","Home");

        }
    }
}