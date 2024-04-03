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
    public class FormsController : BaseController
    {
        private readonly FormsService _formsService;

        public FormsController()
        {
            _formsService = new FormsService(); 
        }
        public ActionResult Index()
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Formmaster.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            ViewBag.Permission = GetPermission(AuthorizeFormAccess.FormAccessCode.Formmaster.ToString());
            return View();
        }
        public ActionResult Create(int? Id)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Formmaster.ToString(), AcessPermission.IsAdd))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            FormModel form = new FormModel();
            if (Id.HasValue)
            {
                var formdetail = _formsService.GetFormsById(Id.Value);
                if (formdetail != null)
                {
                    form.Id = Id.Value;
                    form.Id = formdetail.Id;
                    form.Name = formdetail.Name;
                    form.FormAcessCode = formdetail.FormAcessCode;
                    form.ParentForm = formdetail.ParentForm;
                    form.NavigateURL = formdetail.NavigateURL;
                    form.DisplayOrder = formdetail.DisplayOrder;
                    form.IsDisplayMenu = formdetail.IsDisplayMenu;
                    form.IsActive = formdetail.IsActive;

                }
            }
            BindDropdown(ref form); 
            return View(form);
        }
        [HttpPost]
        public ActionResult Create(FormModel form)
        {           
            _formsService.SaveUpdateForm(form);      
            TempData["Message"] = "Data Saved Successfully!!";           
            return RedirectToAction("Index");

        }
        private void BindDropdown(ref FormModel model)
        {
            BindParentForm(ref model);
        }
        public FormModel BindParentForm(ref FormModel model)
        {
            int formid = model.Id;
            var getparentform = _formsService.GetAllForms().Where(f => f.Id != formid).Select(a => new FormMst { Id = a.Id, Name = a.Name }).OrderBy(a => a.Name);
            model._parentFormList.Add(new SelectListItem() { Text = "select text", Value = "" });
            foreach (var item in getparentform)
            {
                model._parentFormList.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            return model;
        }
        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.Formmaster.ToString(), AcessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }

            List<FormModel> formlist = _formsService.GetAllForms();
            return Json(formlist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}