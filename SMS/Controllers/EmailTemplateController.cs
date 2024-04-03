using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SMS.Model;
using SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class EmailTemplateController : BaseController
    {
        private readonly EmailTemplateService emailTemplateService;
        public EmailTemplateController()
        {
            emailTemplateService = new EmailTemplateService();
        }
        public ActionResult Index()
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.EMAILTEMPLATE.ToString(), AcessPermission.IsView))
                return RedirectToAction("AccessDenied", "Base");
            ViewBag.Permission = GetPermission(AuthorizeFormAccess.FormAccessCode.EMAILTEMPLATE.ToString());

            return View();
        }
        public ActionResult Create(int? id)
        {
            string actionPermission = "";
            if (id == null)
                actionPermission = AcessPermission.IsAdd;
            else if ((id ?? 0) > 0)
                actionPermission = AcessPermission.IsEdit;

            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.EMAILTEMPLATE.ToString(), actionPermission)) 
                return RedirectToAction("AccessDenied", "Base");
           // ViewBag.Permission = GetPermission(AuthorizeFormAccess.FormAccessCode.EMAILTEMPLATE.ToString());

            EmailTemplate model = new EmailTemplate();
            if (id.HasValue)
            {
                var email = emailTemplateService.GetEmailTemplateById(id.Value);
                if (email != null)
                {
                    model.Id = id.Value;
                    model.BCC = email.BCC;
                    model.CC = email.CC;
                    model.Name = email.Name;
                    model.Subject = email.Subject;
                    model.TemplateCode = email.TemplateCode;
                    model.MailBody = email.MailBody;
                    model.IsActive = email.IsActive.Value;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EmailTemplate model)
        {
            string actionPermission = "";
            if (model.Id == 0)
                actionPermission = AcessPermission.IsAdd;
            else if (model.Id > 0)
                actionPermission = AcessPermission.IsEdit;

            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.EMAILTEMPLATE.ToString(), actionPermission))
                return RedirectToAction("AccessDenied", "Base");


            if (ModelState.IsValid)
            {
                SaveUpdateEmailTemplate(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public void SaveUpdateEmailTemplate(EmailTemplate model)
        {
            int userId = SessionHelper.UserId;
            EmailFP obj = new EmailFP();
            if (model.Id > 0)
            {
                obj = emailTemplateService.GetEmailTemplateById(model.Id);
            }
            obj.BCC = model.BCC;
            obj.CC = model.CC;
            obj.Id = model.Id;
            obj.Name = model.Name;
            obj.Subject = model.Subject;
            obj.MailBody = model.MailBody;
            obj.IsActive = model.IsActive;
            obj.IsDeleted = false;
            if (obj.Id == 0)
            {
                obj.CreatedBy = userId;
                obj.CreatedOn = DateTime.UtcNow;
                obj.TemplateCode = model.TemplateCode;
                model.Id = emailTemplateService.CreateEmailTemplate(obj);
            }
            else
            {
                obj.UpdatedBy = userId;
                obj.UpdatedOn = DateTime.UtcNow;
                emailTemplateService.UpdateEmailTemplates(obj);
            }
        }

        [HttpPost]
        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.EMAILTEMPLATE.ToString(), AcessPermission.IsView))
                return RedirectToAction("AccessDenied", "Base");

            var getemailtemplates = emailTemplateService.GetAllEmailTemplate();
            return Json(getemailtemplates.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
    }
}