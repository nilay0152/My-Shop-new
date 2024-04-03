using Microsoft.Web.WebPages.OAuth;
using SMS.Data;
using SMS.Data.Database;
using SMS.Filters;
using SMS.Helper;
using SMS.Model;
using SMS.Service;
using System;

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using WebSecurity = WebMatrix.WebData.WebSecurity;

namespace SMS.Controllers
{

   [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        private readonly FormRoleMappingService formRoleMappingService;
        private readonly RoleMasterService roleMasterService;
        private readonly UserMasterService userMasterService;
        private readonly FormsService formsService;
        private readonly StudentService studentService;
        private readonly TeacherService teacherService;
        private readonly AnnoucementService annoucementService;
        private readonly EmailTemplateService emailTemplateService;
        public StudentEntites _db = new StudentEntites();
        
        public AccountController()
        {
            formRoleMappingService = new FormRoleMappingService();
            roleMasterService = new RoleMasterService();
            userMasterService = new UserMasterService();
            formsService = new FormsService();
            studentService = new StudentService();
            teacherService = new TeacherService();
            annoucementService = new AnnoucementService();
            emailTemplateService = new EmailTemplateService();
            
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { Email = model.Email });
                    WebSecurity.Login(model.UserName, model.Password);                                           
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError(" ", ErrorCodeToString(ex.StatusCode));
                    TempData["Message"] = Constants.EmailCodes.Useralreadyexist;
                }

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string provideruserId)
        {
            string owneraccount = OAuthWebSecurity.GetUserName(provider, provideruserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (owneraccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, provideruserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            LoginModel model = new LoginModel();
            if (SessionHelper.UserId > 0)
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl = "")
        {
           
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                var userID = WebSecurity.GetUserId(model.UserName);
                SessionHelper.UserId = userID;
                SessionHelper.IsAdmin = true;
                SessionHelper.UserName = model.UserName;
                SessionHelper.RoleName = Roles.GetRolesForUser(model.UserName).FirstOrDefault();
                SessionHelper.RoleId = roleMasterService.GetRolesByName(SessionHelper.RoleName).RoleId;
                SessionHelper.RoleCode = roleMasterService.GetRolesById(SessionHelper.RoleId).RoleCode;
                string returnUrl = Request.QueryString["ReturnUrl"];
                Session["UserName"] = model.UserName.ToString();
                SessionHelper.UserId = userID;
                Session["Menu"] = formRoleMappingService.GetMenu(userID);
                var sadmin = SessionHelper.UserId;
                
                //var all = _db.webpages_UsersInRoles.Where(x => x.UserId == sadmin).FirstOrDefault().RoleId;
                //if (sadmin == 1)
                //{
                //    var annoucement = from Annoucement in _db.annoucements
                //                      where Annoucement.Status == true
                //                      select Annoucement;

                //    var latest = annoucement.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
                //    TempData["announcement"] = latest.AnnoucementDetail;
                //    TempData["subject"] = latest.Subject;
                    
                //}
                //else
                //{
                //    var annoucement = from role in _db.annoucements
                //                      join anmct in _db.webpages_UsersInRoles on role.RoleId equals anmct.RoleId
                //                      into list
                //                      from announcement in list.DefaultIfEmpty()
                //                      where ((role.RoleId == all || role.RoleId == 0) && (role.Status==true))
                //                      select role;
                //    var latest = annoucement.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
                //    TempData["announcement"] = latest.AnnoucementDetail;
                //    TempData["subject"] = latest.Subject;


                //}
                return RedirectToAction("Index", "Home");

                

            }
            else
            {
                ModelState.AddModelError(" ", "The User name or password provided is incorrect.");
                TempData["Error"] = Constants.EmailCodes.ERRORMSG;
                return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
            
        }

        [HttpPost]

        public ActionResult Manage(ForgotPassword model)
        {
            bool haslocalaccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HaslocalPassword = haslocalaccount;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (haslocalaccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        TempData["Message"] = Constants.EmailCodes.PASSWORDCHANGESUCCESS;
                    }
                    catch (Exception)
                    {

                        changePasswordSucceeded = false;
                    }
                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Login", new { message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The Current Passsword is incorrect or the new password is invalid.");
                    }
                }
            }
            else {
                // oldpasssword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError(" ", ex);
                    }
                }
            }
            return View(model);
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,         
        }
       
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";
                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";
                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";
                case MembershipCreateStatus.InvalidEmail:
                    return "The E-Mail address provided is invalid. Please check value and try it again ";
                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            if (TempData["Error"] != null && TempData["Error"].ToString() != "")
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userexist = userMasterService.GetEmailById(model.EmailId);
                if (userexist != null)
                {
                    ThreadStart thrdStart = new ThreadStart(() => SendForgotPasswordMail(userexist.Email, userexist.UserName, userexist.UserName));
                    Thread mailThread = new Thread(thrdStart);
                    mailThread.IsBackground = true;
                    mailThread.Start();
                }
            }
            string passwowrdresetlink = Constants.EmailCodes.PASSWORDRESETEMAILSENT;
            passwowrdresetlink = passwowrdresetlink.Replace("@@Email@@", model.EmailId);
            ViewBag.MailSentSuccess = passwowrdresetlink;
            model.EmailId = "";
            return View(model);
        }
        public void SendForgotPasswordMail(string emailid, string name, string username)
        {
            MembershipUser user;

            var bodyVariables = new Dictionary<string, string>();
            var subjectVariables = new Dictionary<string, string>();

            bodyVariables.Add("@@User@@", name);
            try
            {
                var emailtemplate = emailTemplateService.GetEmailTemplateByCode(Constants.Codes.FORGOTPASSWORD.ToString());
                var newtemplate = emailTemplateService.ReplaceParameterValuesInEmailTemplate(emailtemplate, subjectVariables, bodyVariables);
                var body = newtemplate.MailBody;
                var subject = newtemplate.Subject;
                int templateId = emailtemplate.Id;

                string NavigateUrl = "javascript://";

                string WebPath = Convert.ToString(ConfigurationManager.AppSettings["BaseUrl"]);
                string userencrypt = CommonUtility.Encrypt(username);

                user = Membership.GetUser(username);
                int passExpHours = 120;
                var token = WebSecurity.GeneratePasswordResetToken(user.UserName, passExpHours);

                NavigateUrl = " " + WebPath + "/Account/ManageChangePassword?uid=" + userencrypt + "&token=" + token;
                body = body.Replace("@@PasswordLink@@", NavigateUrl);
                List<string> To = new List<string>();
                List<string> CC = new List<string>();
                List<string> BCC = new List<string>();

                To.Add(emailid);

                bool isSucess = true;
                isSucess = EmailHelper.SendEmail(To, CC, BCC, subject, body,OrgName:"SMS");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ManageChangePassword(string uid, string uname, string token, string eid)
        {
            ChangePassword_VM model = new ChangePassword_VM();
            model.Forgotuid = uid;

            if (uid != null)
            {
                string username = CommonUtility.Decrypt(model.Forgotuid);
                model.UserName = username;
                model.ReturnToken = token;

                var checktokeexist = userMasterService.GetWebpages_MembershipByUserId(WebSecurity.GetUserId(model.UserName));
                if (checktokeexist != null)
                {
                    if (!(checktokeexist.PasswordVerificationTokenExpirationDate != null && checktokeexist.PasswordVerificationToken == token && checktokeexist.PasswordVerificationTokenExpirationDate >= DateTime.UtcNow))
                    {
                        TempData["Error"] = Constants.EmailCodes.PASSWORDRESETLINKEXPIRED; 
                        return RedirectToAction("ForgotPassword");
                    }
                }
                else
                {
                    model.UserName = CommonUtility.Decrypt(uname);
                }
                
            }
            return View(model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ManageChangePassword(ChangePassword_VM model)
        {
            bool haslocalaccount = false;
            if (model.Forgotuid != null)
            {
                haslocalaccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(model.UserName));
            }
            else
            {
                haslocalaccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            }
            ViewBag.haslocalPassword = haslocalaccount;
            ViewBag.ReturnUrl = Url.Action("ManageChangePassword");
            if (haslocalaccount)
            {
                if (ModelState.IsValid)
                {
                    bool changepasswordsucceded = false;
                    try
                    {
                        if (model.Forgotuid != null)
                        {
                            changepasswordsucceded = WebSecurity.ResetPassword(model.ReturnToken, model.NewPassword);
                        }
                    }
                    catch (Exception)
                    {

                        changepasswordsucceded = false;
                    }
                    if (changepasswordsucceded)
                    {
                        TempData["Success"] =  Constants.EmailCodes.PASSWORDCHANGESUCCESS;
                        SessionHelper.UserId = 0;
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Error =  Constants.EmailCodes.PASSWORDRESETLINKEXPIRED;
                        return View(model);
                    }
                }
            }
            else {
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        TempData["Success"] = Constants.EmailCodes.PASSWORDCHANGESUCCESS;
                        SessionHelper.UserId = 0;
                        return RedirectToAction("Login");
                    }
                    catch (Exception)
                    {
                        TempData["ErrorMsg"] = Constants.EmailCodes.PASSWORDERRORMESSAGE;
                    }
                }
            }
            return View(model);
        }
    }
}

